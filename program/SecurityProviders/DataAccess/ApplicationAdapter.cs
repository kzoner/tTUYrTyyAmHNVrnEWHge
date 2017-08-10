using System;
using System.Data;
using System.Data.SqlClient;
using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class ApplicationAdapter
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

        /// <summary>
        /// Tao ung dung moi
        /// </summary>
        /// <param name="name">Ten ung dung</param>
        /// <param name="description">Mo ta ung dung</param>
        /// <returns></returns>
        public Application Create(string name, string description)
        {
            try
            {
                // create INPUT SQL paramters
                SqlParameter prmName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = name;

                SqlParameter prmDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 250);
                prmDescription.Direction = ParameterDirection.Input;
                if (string.IsNullOrEmpty(description) == true)
                {
                    prmDescription.Value = DBNull.Value;
                }
                else
                {
                    prmDescription.Value = description;
                }

                // create OUTPUT SQL paramters
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Output;

                // execute stored procedure to create the applicationn
                Database.ExecuteNonQuery("UspCreateApplication", CommandType.StoredProcedure, prmName, prmDescription, prmApplicationID);
                
                int applicationID = int.Parse(prmApplicationID.Value.ToString());


                if (applicationID > 0)
                {
                    // create application success
                    Application objApplication = new Application(applicationID, name, description);

                    return objApplication;
                }
                else
                    // database error
                    throw new SecurityException("Create application fail. Database error!");

            }
            catch (Exception ex)
            {
                SecurityException customException = new SecurityException(ex.Message, ex.InnerException);

                throw ex;
            }
        }

        /// <summary>
        /// Cap nhat ung dung
        /// </summary>
        /// <param name="application">ID cua ung dung</param>
        public void Update(Application application)
        {
            try
            {
                // create SQL parameters
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = application.ApplicationID;

                SqlParameter prmName = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = application.Name;

                SqlParameter prmDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 250);
                prmDescription.Direction = ParameterDirection.Input;
                if (string.IsNullOrEmpty(application.Description))
                {
                    prmDescription.Value = DBNull.Value;
                }
                else
                {
                    prmDescription.Value = application.Description;
                }

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute update stored procedure
                Database.ExecuteNonQuery(
                    "UspUpdateApplication"
                    , CommandType.StoredProcedure
                    , prmApplicationID
                    , prmName
                    , prmDescription
                    , prmErrorNumber
                    , prmErrorMessage);

                // checking for output errors
                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());
                
                if (errorNumber > 0)
                {
                    string errorMessage = prmErrorMessage.Value.ToString();

                    SecurityException customEx = new SecurityException(errorMessage);

                    throw customEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Xoa mot ung dung
        /// </summary>
        /// <param name="application">Doi tuong ung dung muon xoa</param>
        public void Remove(Application application)
        {
            try
            {
                // Create SQL parameters
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = application.ApplicationID;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;


                // execute stored procedure to delete application
                Database.ExecuteNonQuery(
                     "UspRemoveApplication", 
                      CommandType.StoredProcedure
                    , prmApplicationID
                    , prmErrorNumber
                    , prmErrorMessage);


                // checking for output errors
                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                if (errorNumber > 0)
                {
                    string errorMessage = prmErrorMessage.Value.ToString();

                    SecurityException customEx = new SecurityException(errorMessage);

                    throw customEx;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Application GetApplication(int applicationId)
        {
            try
            {
                SqlParameter prmAppID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmAppID.Value = applicationId;
                using (IDataReader dr = Database.ExecuteReader("UspGetApplication", CommandType.StoredProcedure, prmAppID))
                {
                    if (dr.Read())
                    {
                        return Populate(dr);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lay danh sach ung dung
        /// </summary>
        /// <returns></returns>
        public ApplicationCollection GetAllApplications()
        {
            try
            {
                ApplicationCollection appCollection = new ApplicationCollection();
                
                appCollection.Clear();

                using (IDataReader dr = Database.ExecuteReader("UspGetAllApplications", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        Application app = Populate(dr);

                        appCollection.Add(app);
                    }                    
                }

                return appCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Application Populate(IDataReader dr)
        {
            try
            {
                int applicationId = int.Parse(dr["ApplicationID"].ToString());
                string name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString();
                string description = dr["Description"] == DBNull.Value ? "" : dr["Description"].ToString();

                Application app = new Application(applicationId, name, description);

                return app;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean Exists(string applicationName)
        {

            try
            {
                SqlParameter appName = new SqlParameter("@AppName", SqlDbType.NVarChar, 50);
                appName.Value = applicationName;
                appName.Direction = ParameterDirection.Input;

                SqlParameter result = new SqlParameter("@result", SqlDbType.Int, 4);
                result.Direction = ParameterDirection.ReturnValue;

                Database.ExecuteNonQuery("UspCheckExistApplication", CommandType.StoredProcedure,appName,result);

                return int.Parse(result.Value.ToString()) == 1;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUserName"></param>
        /// <returns></returns>
        public ApplicationCollection GetApplicationByUser(string strUserName)
        {
            try
            {
                ApplicationCollection appCollection = new ApplicationCollection();
                appCollection.Clear();

                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = strUserName;

                using (IDataReader dr = Database.ExecuteReader("UspGetApplicationByUser", CommandType.StoredProcedure, prmUsername))
                {
                    while (dr.Read())
                    {
                        Application app = Populate(dr);

                        appCollection.Add(app);
                    }
                }

                return appCollection;
            }
            catch (Exception ex)
            {throw ex;}
        }
    }
}
