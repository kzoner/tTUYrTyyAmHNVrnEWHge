using System;
using System.Data;
using System.Data.SqlClient;
using Inside.DataProviders;
using Inside.SecurityProviders;

namespace Inside.SecurityProviders.DataAccess
{
    internal class RoleAdapter
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
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Role Create(string roleCode, string roleName, int applicationID)
        {
            try
            {
                // create sql parameters

                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Output;

                SqlParameter prmRCode = new SqlParameter("@RoleCode", SqlDbType.NVarChar, 50);
                prmRCode.Direction = ParameterDirection.Input;
                prmRCode.Value = roleCode;

                SqlParameter prmName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = roleName;

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationID;
                
                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute procedure to create new role
                Database.ExecuteNonQuery("UspCreateRole", CommandType.StoredProcedure,prmRoleID,
                    prmRCode, prmName, prmApplicationID, prmErrorNumber, prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());
                
                switch (errorNumber)
                {
                    case 0:
                        int roleID = int.Parse(prmRoleID.Value.ToString());
                        Role role = new Role(roleID,roleCode, roleName, applicationID);

                        return role;

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


    //    @RoleID int
    //,@RoleCode varchar(15)
    //,@RoleName nvarchar(50)
    //, @ApplicationID int
    //,@ErrorNumber int output
    //,@ErrorMessage nvarchar(150) output

        /// <summary>
        /// Cap nhat Role
        /// </summary>
        /// <param name="role"></param>
        public void Update(Role role)
        {
            try
            {
                // Create SQL parameters

                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = role.RoleID;


                SqlParameter prmRoleCode = new SqlParameter("@RoleCode", SqlDbType.NVarChar, 50);
                prmRoleCode.Direction = ParameterDirection.Input;
                prmRoleCode.Value = role.RoleCode;

                SqlParameter prmName = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
                prmName.Direction = ParameterDirection.Input;
                prmName.Value = role.RoleName;

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = role.ApplicationID;
  
                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure to update role
                Database.ExecuteNonQuery("UspUpdateRole", CommandType.StoredProcedure
                    ,prmRoleID
                    , prmRoleCode
                    , prmName
                    ,prmApplicationID
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
        /// Xoa role
        /// </summary>
        /// <param name="role"></param>
        public void Remove( int roleID)
        {
            try
            {
                // Create SQL parameters
                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = roleID;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // Execute procedure to update role
                Database.ExecuteNonQuery("UspRemoveRole", CommandType.StoredProcedure
                    , prmRoleID
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
        /// <param name="code"></param>
        public Role GetRole(int roleID)
        {
            Role role = null;

            try
            {
                // Create SQL parameters
                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = roleID;

                // Execute procedure to update role
                using (IDataReader dr = Database.ExecuteReader("UspGetRole", CommandType.StoredProcedure, prmRoleID))
                {
                    if (dr.Read())
                    {
                        role = Populate(dr);
                    }
                }

                return role;
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
        public RoleCollection GetAllRoles()
        {
            RoleCollection collection = new RoleCollection();

            try
            {
                // Execute procedure to update role
                using (IDataReader dr = Database.ExecuteReader("UspGetAllRoles", CommandType.StoredProcedure))
                {
                    while (dr.Read())
                    {
                        Role role = Populate(dr);

                        collection.Add(role);
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
        /// Lay danh sach Role cua mot ung dung
        /// </summary>
        /// <param name="applicationID">ID cua Ung dung</param>
        /// <returns>tra ve danh sach dang RoleCollection</returns>
        public RoleCollection GetRoleInApplication(int applicationID)
        {
            SqlParameter prmApplicationID = new SqlParameter("@AppID", SqlDbType.Int, 4);
            prmApplicationID.Direction = ParameterDirection.Input;
            prmApplicationID.Value = applicationID;
            RoleCollection collection = new RoleCollection();
            try
            { 
            using (IDataReader dr = Database.ExecuteReader("UspGetRoles_In_App", CommandType.StoredProcedure, prmApplicationID))
            {
                while (dr.Read())
                {
                    Role role = Populate(dr);

                    collection.Add(role);
                }
            }
            }
            catch( Exception ex )
            {
                throw (ex);
            }
            return collection;
        }


        /// <summary>
        /// Lấy danh sách Role chứa userName
        /// </summary>
        /// <param name="userName">UserName cần lấy danh sách Role</param>
        /// <returns></returns>
        public RoleCollection GetRolesOfUser(string userName)
        {
            SqlParameter prmUerName = new SqlParameter("@UserName", SqlDbType.VarChar, 128);
            prmUerName.Direction = ParameterDirection.Input;
            prmUerName.Value = userName;

            RoleCollection collection = new RoleCollection();

            try
            {
                using(IDataReader dr = Database.ExecuteReader("UspGetRolesOfUser", CommandType.StoredProcedure, prmUerName))
                {
                    while (dr.Read())
                    {
                        Role role = Populate(dr);
                        collection.Add(role);
                        
                    }
                }
            }
            catch( Exception ex)
            {
                throw ex;
            }

            return collection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public RoleCollection FindRolesByCode(string roleCode, int applicationID)
        {
            SqlParameter prmRoleCode = new SqlParameter("@RoleCode", SqlDbType.VarChar, 128);
            SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
            prmRoleCode.Direction = ParameterDirection.Input;
            prmApplicationID.Direction = ParameterDirection.Input;
            prmRoleCode.Value = roleCode;
            prmApplicationID.Value = applicationID;

            RoleCollection collection = new RoleCollection();

            try
            {
                using (IDataReader dr = Database.ExecuteReader("UspFindRolesByCode", CommandType.StoredProcedure, prmRoleCode, prmApplicationID))
                {
                    while (dr.Read())
                    {
                        Role role = Populate(dr);
                        collection.Add(role);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return collection;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void AddUserToRole(string username, int roleID)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = roleID;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute procedure
                Database.ExecuteNonQuery("UspAddUserToRole", CommandType.StoredProcedure
                    , prmUsername, prmRoleID, prmErrorNumber, prmErrorMessage);

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
        /// <param name="users"></param>
        /// <param name="role"></param>
        public void AddUsersToRole(UserCollection users, int roleID)
        {
            foreach (User user in users)
            {
                try
                {
                    AddUserToRole(user.UserName, roleID);
                }
                catch { }
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void RemoveUserFromRole(string username, int roleID)
        {
            try
            {
                // create sql parameters
                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int,4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = roleID;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 150);
                prmErrorMessage.Direction = ParameterDirection.Output;

                // execute procedure
                Database.ExecuteNonQuery("UspRemoveUserFromRole", CommandType.StoredProcedure
                    , prmUsername
                    , prmRoleID
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
        /// <param name="users"></param>
        /// <param name="role"></param>
        public void RemoveUsersFromRole(UserCollection users, int roleID)
        {
            foreach (User user in users)
            {
                try
                {
                    RemoveUserFromRole(user.UserName, roleID);
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Role Populate(IDataReader dr)
        {
            try
            {
                int roleID = int.Parse(dr["RoleID"].ToString());
                string roleCode = dr["RoleCode"].ToString();
                string roleName = dr["RoleName"].ToString();
                int applicationID = int.Parse(dr["ApplicationID"].ToString());

                Role app = new Role(roleID, roleCode, roleName, applicationID);
                return app;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Set permission for role
        /// </summary>
        /// <param name="iApplicationID"></param>
        /// <param name="strRoleCode"></param>
        /// <param name="iResourceID"></param>
        /// <param name="strDataPermission"></param>
        /// <returns></returns>
        public ReturnObject SetPermission(int iApplicationID, string strRoleCode, int iResourceID, string strDataPermission)
        {
            try
            {
                ReturnObject objRet = new ReturnObject();

                // create sql parameters

                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = iApplicationID;

                SqlParameter prmRoleCode = new SqlParameter("@RoleCode", SqlDbType.VarChar, 50);
                prmRoleCode.Direction = ParameterDirection.Input;
                prmRoleCode.Value = strRoleCode;

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
                Database.ExecuteNonQuery("UspSetPermissionForRole", CommandType.StoredProcedure
                    , prmApplicationID
                    , prmRoleCode
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
            { throw ex; }
        }
    }
}
