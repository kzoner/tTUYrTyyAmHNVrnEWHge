using System;
using System.Data;
using System.Data.SqlClient;
using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class UserAdapter
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
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fullname"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public User Create(string username, string password, string fullname, string email, bool blocked)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 64);
                prmPassword.Direction = ParameterDirection.Input;
                prmPassword.Value = password;

                SqlParameter prmFullName = new SqlParameter("@Fullname", SqlDbType.NVarChar, 64);
                prmFullName.Direction = ParameterDirection.Input;
                prmFullName.Value = fullname;

                SqlParameter prmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 128);
                prmEmail.Direction = ParameterDirection.Input;
                prmEmail.Value = email;

                SqlParameter prmBlocked = new SqlParameter("@Blocked", SqlDbType.Bit, 1);
                prmBlocked.Direction = ParameterDirection.Input;
                prmBlocked.Value = blocked;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspCreateUser", CommandType.StoredProcedure
                    , prmUsername
                    , prmFullName
                    ,prmPassword
                    , prmEmail
                    , prmBlocked
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        User user = new User(username, fullname, email, blocked, DateTime.MinValue, DateTime.Today, DateTime.MinValue, "");
                        return user;

                    default:
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
        /// <param name="user"></param>
        public void Update(User user)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = user.UserName;

                SqlParameter prmFullName = new SqlParameter("@Fullname", SqlDbType.NVarChar, 64);
                prmFullName.Direction = ParameterDirection.Input;
                prmFullName.Value = user.FullName;

                SqlParameter prmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 128);
                prmEmail.Direction = ParameterDirection.Input;
                prmEmail.Value = user.Email;

               SqlParameter prmBlocked = new SqlParameter("@Blocked", SqlDbType.Bit, 1);
                prmBlocked.Direction = ParameterDirection.Input;
                prmBlocked.Value = user.Blocked;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspUpdateUser", CommandType.StoredProcedure
                    , prmUsername
                    , prmFullName
                    , prmEmail
                    , prmBlocked
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// <param name="user"></param>
        public void Remove(string username)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspRemoveUser", CommandType.StoredProcedure
                    , prmUsername
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// <param name="user"></param>
        public void BlockUser(string username)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspBlockUser", CommandType.StoredProcedure
                    , prmUsername
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// <param name="user"></param>W
        public void UnblockUser(string username)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspUnblockUser", CommandType.StoredProcedure
                    , prmUsername
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// <param name="username"></param>
        /// <param name="currentPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(string username, string currentPassword, string newPassword)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmCurrentPassword = new SqlParameter("@CurrentPassword", SqlDbType.VarChar, 64);
                prmCurrentPassword.Direction = ParameterDirection.Input;
                prmCurrentPassword.Value = currentPassword;

                SqlParameter prmNewPassword = new SqlParameter("@NewPassword", SqlDbType.VarChar, 64);
                prmNewPassword.Direction = ParameterDirection.Input;
                prmNewPassword.Value = newPassword;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspChangeUserPassword", CommandType.StoredProcedure
                    , prmUsername
                    , prmCurrentPassword
                    , prmNewPassword
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// <param name="username"></param>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public string RecoveryPassword(string username)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 64);
                prmPassword.Direction = ParameterDirection.Output;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspResetUserPassword", CommandType.StoredProcedure
                    , prmUsername
                   , prmPassword
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                if (errorNumber == 0)
                {
                    string password = prmPassword.Value.ToString();

                    return password;
                }
                else
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

        public void SetPassword(string username, string password)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 64);
                prmPassword.Direction = ParameterDirection.Input;
                prmPassword.Value = password;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspSetUserPassword", CommandType.StoredProcedure
                    , prmUsername
                    , prmPassword
                    , prmErrorNumber
                    , prmErrorMessage);

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
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmPassword = new SqlParameter("@QuestionID", SqlDbType.VarChar, 64);
                prmPassword.Direction = ParameterDirection.Input;
                prmPassword.Value = password;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure
                Database.ExecuteNonQuery("UspValidateUser", CommandType.StoredProcedure
                    , prmUsername
                    , prmPassword
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        return true;

                    default:
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
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                // Execute procedure
                using (IDataReader dr = Database.ExecuteReader("UspGetUser", CommandType.StoredProcedure, prmUsername))
                {
                    while (dr.Read())
                    {
                        User user = Populate(dr);

                        return user;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public UserCollection GetAllUsers()
        {
            try
            {
                UserCollection collection = new UserCollection();

                collection.Clear();

                // Execute procedure
                using (IDataReader dr = Database.ExecuteReader("UspGetAllUsers", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        User user = Populate(dr);

                        collection.Add(user);
                    }
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserCollection FindUsersByEmail(string email)
        {
            try
            {
                UserCollection collection = new UserCollection();

                collection.Clear();

                // create sql parameters
                SqlParameter prmEmail = new SqlParameter("@Email", SqlDbType.VarChar, 128);
                prmEmail.Direction = ParameterDirection.Input;
                prmEmail.Value = email;

                // Execute procedure 
                using (IDataReader dr = Database.ExecuteReader("UspFindUsersByEmail", CommandType.StoredProcedure, prmEmail))
                {
                    while (dr.Read())
                    {
                        User user = Populate(dr);

                        collection.Add(user);
                    }
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserCollection FindUsersByFullName(string fullName)
        {
            try
            {
                UserCollection collection = new UserCollection();

                collection.Clear();

                // create sql parameters
                SqlParameter prmFullName = new SqlParameter("@FullName", SqlDbType.NVarChar, 64);
                prmFullName.Direction = ParameterDirection.Input;
                prmFullName.Value = fullName;

                // Execute procedure 
                using (IDataReader dr = Database.ExecuteReader("UspFindUsersByFullName", CommandType.StoredProcedure, prmFullName))
                {
                    while (dr.Read())
                    {
                        User user = Populate(dr);

                        collection.Add(user);
                    }
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserCollection FindUsersByUserName(string userName)
        {
            try
            {
                UserCollection collection = new UserCollection();

                collection.Clear();
                // create sql parameters
                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 128);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Value = userName;

                // Execute procedure
                using (IDataReader dr = Database.ExecuteReader("UspFindUserByUserName", CommandType.StoredProcedure, prmUserName))
                {
                    while (dr.Read())
                    {
                        User user = Populate(dr);
                        collection.Add(user);
                    }
                }

                return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public bool IsUserInRole(string username, string rolecode)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmRoleCode = new SqlParameter("@RoleCode", SqlDbType.VarChar, 15);
                prmRoleCode.Direction = ParameterDirection.Input;
                prmRoleCode.Value = rolecode;

                SqlParameter prmReturnCode = new SqlParameter("@ReturnCode", SqlDbType.Int, 4);
                prmRoleCode.Direction = ParameterDirection.Output;

                // execute procedure
                Database.ExecuteNonQuery("UspIsUserInRole", CommandType.StoredProcedure
                    , prmUsername
                    , prmRoleCode
                    , prmReturnCode);

                int returnCode = int.Parse(prmReturnCode.Value.ToString());

                switch (returnCode)
                {
                    case 1:
                        return true;

                    default:
                        return false;
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
        /// <param name="roleName"></param>
        /// <returns></returns>
        public UserCollection GetUsersInRole(int roleID)
        {
            UserCollection collection = new UserCollection();
            collection.Clear();
            try
            {
                // Create SQL parameters
                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = roleID;

                // Execute procedure to update role
                using (IDataReader dr = Database.ExecuteReader("UspGetUsersInRole", CommandType.StoredProcedure, prmRoleID))
                {
                    while (dr.Read())
                    {
                        User user = Populate(dr);

                        collection.Add(user);
                    }
                }

                return collection;
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
        private User Populate(IDataReader dr)
        {
            try
            {
                string username = dr["Username"].ToString();
                string fullName = dr["FullName"].ToString();
                string email = dr["Email"].ToString();
                bool blocked = dr.GetBoolean(dr.GetOrdinal("Blocked"));
                DateTime blockedDate = dr["BlockedDate"] == DBNull.Value? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("BlockedDate"));
                DateTime createdTime = dr["CreatedTime"] == DBNull.Value ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("CreatedTime"));
                DateTime lastLogin = dr["LastLogin"] == DBNull.Value ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("LastLogin"));
                string lastIP = dr["LastIP"].ToString();

                User user = new User(username, fullName, email, blocked, blockedDate, createdTime, lastLogin, lastIP);

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Kiem tra dang nhap
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="IP"></param>
        /// <returns></returns>
        public GlobalEnum.LoginStatus CheckLogin(string UserName, string Password, string IP)
        {
            GlobalEnum.LoginStatus result;
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = UserName;

                SqlParameter prmPassword = new SqlParameter("@Password", SqlDbType.VarChar, 64);
                prmPassword.Direction = ParameterDirection.Input;
                prmPassword.Value = Password;

                SqlParameter prmIP = new SqlParameter("@IP", SqlDbType.VarChar, 19);
                prmIP.Direction = ParameterDirection.Input;
                prmIP.Value = IP;

                SqlParameter prmResult = new SqlParameter("@Result", SqlDbType.Int, 4);
                prmResult.Direction = ParameterDirection.ReturnValue;

                // Execute procedure
                Database.ExecuteNonQuery("UspCheckUserLogin", CommandType.StoredProcedure
                    , prmUsername
                    , prmPassword
                    , prmIP
                    , prmResult);

                result = (GlobalEnum.LoginStatus)Enum.Parse(typeof(GlobalEnum.LoginStatus),prmResult.Value.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// Set permission for user
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="iResourceID"></param>
        /// <param name="strDataPermission"></param>
        /// <returns></returns>
        public ReturnObject SetPermission(string strUserName, int iResourceID, string strDataPermission)
        {
            try
            {
                ReturnObject objRet = new ReturnObject();

                // create sql parameters
                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Value = strUserName;

                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = iResourceID;

                SqlParameter prmDataPermission = new SqlParameter("@DataPermission", SqlDbType.VarChar,1000);
                prmDataPermission.Direction = ParameterDirection.Input;
                prmDataPermission.Value = strDataPermission;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar,1000);
                prmErrorMessage.Direction = ParameterDirection.Output;


                // Execute procedure
                Database.ExecuteNonQuery("UspSetPermissionForUser", CommandType.StoredProcedure
                    , prmUserName
                    , prmResourceID
                    , prmDataPermission
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());
                if (errorNumber != 0)
                {
                    string errorMessage = prmErrorMessage.Value.ToString();
                    SecurityException customEx = new SecurityException(errorMessage);
                    throw customEx;
                }
                else
                {
                    objRet.ErrorNumber = errorNumber;
                    objRet.ErrorMessage = prmErrorMessage.Value.ToString();
                    return objRet;
                }
            }
            catch (Exception ex)
            {throw ex;}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strToken"></param>
        /// <returns></returns>
        public DataTable CheckAuthorization(string strUserName, string strToken)
        {
            DataTable dtPermissionList = new DataTable("PermissionList");
            try
            {                
                // create sql parameters
                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Value = strUserName;

                SqlParameter prmToken = new SqlParameter("@Token", SqlDbType.VarChar, 50);
                prmToken.Direction = ParameterDirection.Input;
                prmToken.Value = strToken;

                // execute query
                using (DataSet ds = Database.FillDataSet("UspCheckAuthorization", CommandType.StoredProcedure, prmUserName, prmToken))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dtPermissionList = ds.Tables[0];
                    }
                }
                return dtPermissionList;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
