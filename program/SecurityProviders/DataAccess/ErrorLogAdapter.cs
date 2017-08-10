using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    public class ErrorLogAdapter
    {
        private SQLDatabase m_db = null;

        protected SQLDatabase Database
        {
            get
            {
                if (m_db == null)
                {
                    m_db = new SQLDatabase(ConfigurationHelper.ReadKey(Constants.STR_AUTHDB_CONN_APPSETTING_KEY));
                }
                return m_db;
            }
        }
        public void Save(ErrorLog pageEx) {
            try
            {
                // create sql parameters
                SqlParameter prmErrorDate = new SqlParameter("@Date", SqlDbType.DateTime, 8);
                prmErrorDate.Direction = ParameterDirection.Input;
                prmErrorDate.Value = pageEx.ErrorDate;

                SqlParameter prmCurrentUser = new SqlParameter("@CurrentUser", SqlDbType.NVarChar, 50);
                prmCurrentUser.Direction = ParameterDirection.Input;
                prmCurrentUser.Value = pageEx.CurrentUser;

                SqlParameter prmPath = new SqlParameter("@Path", SqlDbType.NVarChar, 200);
                prmPath.Direction = ParameterDirection.Input;
                prmPath.Value = pageEx.Path;

                SqlParameter prmMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 4000);
                prmMessage.Direction = ParameterDirection.Input;
                prmMessage.Value = pageEx.Exception.Message;

                SqlParameter prmStackTrace = new SqlParameter("@StackTrace", SqlDbType.NText);
                prmStackTrace.Direction = ParameterDirection.Input;
                prmStackTrace.Value = pageEx.Exception.ToString();

                SqlParameter prmTargetFunction = new SqlParameter("@TargetFunction", SqlDbType.NVarChar, 1000);
                prmTargetFunction.Direction = ParameterDirection.Input;
                prmTargetFunction.Value = pageEx.Exception.TargetSite.Name;

                // Execute procedure
                Database.ExecuteNonQuery("UspInsertErrorLogs", CommandType.StoredProcedure
                    , prmErrorDate
                    , prmCurrentUser
                    , prmMessage
                    , prmStackTrace
                    , prmTargetFunction
                    , prmPath);
            }
            catch (Exception) 
            { 
                //throw ex;
            }
        }

        public DataTable GetErrorLog(DateTime FromDate, DateTime ToDate, string CurrentUser)
        {
            DataTable dtRet = new DataTable("ErrorLog");
            try
            {
                SqlParameter prmFromDate = new SqlParameter("@FromDate", SqlDbType.DateTime, 4);
                SqlParameter prmToDate = new SqlParameter("@ToDate", SqlDbType.DateTime, 4);
                SqlParameter prmCurrentUser = new SqlParameter("@CurrentUser", SqlDbType.NVarChar, 50);

                prmFromDate.Value = FromDate;
                prmToDate.Value = ToDate;
                prmCurrentUser.Value = CurrentUser;

                using (IDataReader dr = Database.ExecuteReader("UspGetErrorLog", CommandType.StoredProcedure, prmFromDate, prmToDate, prmCurrentUser))
                {
                    //if (dr.Read())
                    //{
                    dtRet.Load(dr);
                    //}
                }

                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearErrorLog()
        {
            try
            {
                Database.ExecuteNonQuery("UspClearErrorLog", CommandType.StoredProcedure);                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ErrorLog Populate(IDataReader dr)
        {
            try
            {
                DateTime errorDate = DateTime.Parse(dr["Date"].ToString());
                string currentUser = dr["CurrentUser"].ToString();
                string path = dr["Path"].ToString();
                Exception exception = new Exception(dr["Message"].ToString());

                ErrorLog errorLog = new ErrorLog(errorDate, currentUser, path, exception);

                return errorLog;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
