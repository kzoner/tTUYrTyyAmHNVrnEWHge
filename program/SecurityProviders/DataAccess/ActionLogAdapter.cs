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
    public class ActionLogAdapter
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

        public void Save(ActionLog alObject)
        {
            try
            {
                // create sql parameters
                SqlParameter prmLogDate = new SqlParameter("@Date", SqlDbType.DateTime, 8);
                prmLogDate.Direction = ParameterDirection.Input;
                prmLogDate.Value = alObject.LogDate;

                SqlParameter prmIP = new SqlParameter("@IP", SqlDbType.VarChar, 19);
                prmIP.Direction = ParameterDirection.Input;
                prmIP.Value = alObject.IP;

                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Value = alObject.UserName;

                SqlParameter prmPath = new SqlParameter("@Path", SqlDbType.NVarChar, 100);
                prmPath.Direction = ParameterDirection.Input;
                prmPath.Value = alObject.Path;

                SqlParameter prmPageTitle = new SqlParameter("@PageTitle", SqlDbType.NVarChar, 100);
                prmPageTitle.Direction = ParameterDirection.Input;
                prmPageTitle.Value = alObject.PageTitle;

                SqlParameter prmOperation = new SqlParameter("@Operation", SqlDbType.NVarChar, 50);
                prmOperation.Direction = ParameterDirection.Input;
                prmOperation.Value = alObject.Operation;

                SqlParameter prmData = new SqlParameter("@Data", SqlDbType.NText);
                prmData.Direction = ParameterDirection.Input;
                prmData.Value = alObject.Data;

                // Execute procedure
                Database.ExecuteNonQuery("UspInsertActionLog", CommandType.StoredProcedure
                    , prmLogDate
                    , prmIP
                    , prmUserName
                    , prmPath
                    , prmPageTitle
                    , prmOperation
                    , prmData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetActionLogs(DateTime FromDate, DateTime ToDate, string IP, string UserName)
        {
            DataTable dtRet = new DataTable("ActionLogs");
            try
            {
                SqlParameter prmFromDate = new SqlParameter("@FromDate", SqlDbType.DateTime, 10);
                SqlParameter prmToDate = new SqlParameter("@ToDate", SqlDbType.DateTime, 10);
                SqlParameter prmIP = new SqlParameter("@IP", SqlDbType.VarChar, 19);
                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);

                prmFromDate.Value = FromDate;
                prmToDate.Value = ToDate;
                
                if (IP == "")
                    prmIP.Value = DBNull.Value;
                else
                    prmIP.Value = IP;

                if (UserName == "")
                    prmUserName.Value = DBNull.Value;
                else
                    prmUserName.Value = UserName;

                using (IDataReader dr = Database.ExecuteReader("UspGetActionLogs", CommandType.StoredProcedure, prmFromDate, prmToDate, prmIP, prmUserName))
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

        public DataTable SearchActionLogs(DateTime FromDate, DateTime ToDate, int SearchType, string Keyword)
        {
            DataTable dtRet = new DataTable("ActionLogs");
            try
            {
                SqlParameter prmFromDate = new SqlParameter("@FromDate", SqlDbType.DateTime, 10);
                SqlParameter prmToDate = new SqlParameter("@ToDate", SqlDbType.DateTime, 10);
                SqlParameter prmSearchType = new SqlParameter("@SearchType", SqlDbType.Int, 4);
                SqlParameter prmKeyword = new SqlParameter("@Keyword", SqlDbType.NVarChar, 100);

                prmFromDate.Value = FromDate;
                prmToDate.Value = ToDate;
                prmSearchType.Value = SearchType;
                prmKeyword.Value = Keyword;

                

                using (IDataReader dr = Database.ExecuteReader("Usp_ActionLogs_Search", CommandType.StoredProcedure, prmFromDate, prmToDate, prmSearchType, prmKeyword))
                {
                    //if ( dr. dr.Read())
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

        public DataTable GetLoginLogs(DateTime FromDate, DateTime ToDate, string UserName)
        {
            DataTable dtRet = new DataTable("LoginLogs");
            try
            {
                SqlParameter prmFromDate = new SqlParameter("@FromDate", SqlDbType.DateTime, 10);
                SqlParameter prmToDate = new SqlParameter("@ToDate", SqlDbType.DateTime, 10);                
                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 50);

                prmFromDate.Value = FromDate;
                prmToDate.Value = ToDate;
                prmUserName.Value = UserName;

                using (IDataReader dr = Database.ExecuteReader("UspGetLoginLogs", CommandType.StoredProcedure, prmFromDate, prmToDate, prmUserName))
                {
                    if (dr.Read())
                    {
                        dtRet.Load(dr);
                    }
                }

                return dtRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
