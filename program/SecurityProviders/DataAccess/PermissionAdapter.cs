using System;
using System.Data;
using System.Data.SqlClient;

using Inside.DataProviders;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders.DataAccess
{
    internal class PermissionAdapter
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
        /// <param name="resourceId"></param>
        /// <param name="operationCode"></param>
        /// <param name="objectName"></param>
        /// <param name="objectType"></param>
        /// <param name="allow"></param>
        /// <returns></returns>
        public Permission SetPermission(int resourceId, int operationCode, string objectName, int objectType,  bool allow)
        {
            try
            {
                // create sql param
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;

                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;

                SqlParameter prmObjectName = new SqlParameter("@ObjectName", SqlDbType.VarChar, 128);
                prmObjectName.Direction = ParameterDirection.Input;

                SqlParameter prmObjectType = new SqlParameter("@ObjectType", SqlDbType.Int, 4);
                prmObjectType.Direction = ParameterDirection.Input;

                SqlParameter prmAllow = new SqlParameter("@Allow", SqlDbType.Bit, 1);
                prmAllow.Direction = ParameterDirection.Input;

                SqlParameter prmPermissionID = new SqlParameter("@PermissionID", SqlDbType.Int, 4);
                prmPermissionID.Direction = ParameterDirection.Output;

                SqlParameter prmErrorNumber = new SqlParameter("@ErrorNumber", SqlDbType.Int, 4);
                prmErrorNumber.Direction = ParameterDirection.Output;

                SqlParameter prmErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 100);
                prmErrorMessage.Direction = ParameterDirection.Output;


                prmResourceID.Value = resourceId;

                if (operationCode <= 0)
                {
                    prmOperationCode.Value = DBNull.Value;
                }
                else
                {
                    prmOperationCode.Value = operationCode;
                }

                if (string.IsNullOrEmpty(objectName))
                {
                    prmObjectName.Value = DBNull.Value;
                }
                else
                {
                    prmObjectName.Value = objectName;
                }

                prmObjectType.Value = objectType;
                prmAllow.Value = allow;

                // execute procedure
                Database.ExecuteNonQuery("UspSetPermission", CommandType.StoredProcedure
                    , prmResourceID
                    , prmOperationCode
                    , prmObjectName
                    , prmObjectType
                    , prmAllow
                    , prmPermissionID
                    , prmErrorNumber
                    , prmErrorMessage);

                int errorNumber = int.Parse(prmErrorNumber.Value.ToString());

                switch (errorNumber)
                {
                    case 0:
                        int permissionId = int.Parse(prmPermissionID.Value.ToString());
                        string username = (objectType == 0 ? objectName : string.Empty);
                        int roleID = (objectType == 1 ? Int32.Parse(objectName) : 0);                        
                        Permission permission = new Permission(permissionId, resourceId, operationCode, username, roleID, allow);
                        return permission;

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
        /// <param name="applicationId"></param>
        /// <returns></returns>
        public PermissionCollection GetPermissions(int applicationId)
        {
            try
            {
                PermissionCollection collection = new PermissionCollection();

                // create sql param
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationId;

                using (IDataReader dr = Database.ExecuteReader("UspGetPermission", CommandType.StoredProcedure, prmApplicationID))
                {
                    while (dr.Read())
                    {
                        Permission permission = Populate(dr);
                        collection.Add(permission);
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
        /// <param name="applicationId"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public PermissionCollection GetRolePermissions(int applicationId, int roleID)
        {
            try
            {
                PermissionCollection collection = new PermissionCollection();

                // create sql param
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationId;

                SqlParameter prmRoleID = new SqlParameter("@RoleID", SqlDbType.Int, 4);
                prmRoleID.Direction = ParameterDirection.Input;
                prmRoleID.Value = roleID;

                using (IDataReader dr = Database.ExecuteReader("UspGetRolePermissions", CommandType.StoredProcedure, prmApplicationID, prmRoleID))
                {
                    while (dr.Read())
                    {
                        Permission permission = Populate(dr);
                        collection.Add(permission);
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
        /// <param name="applicationId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetUserPermissions(int applicationId, string username)
        {
            try
            {
                //PermissionCollection collection = new PermissionCollection();
                DataTable dt = new DataTable("UserPermission");

                // create sql param
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationId;

                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                using (IDataReader dr = Database.ExecuteReader("UspGetUserPermissions", CommandType.StoredProcedure, prmApplicationID, prmUsername))
                {                    
                    //while (dr.Read())
                    //{
                    //    Permission permission = Populate(dr);
                    //    collection.Add(permission);
                    //}
                    if (dr.Read())
                    {
                        dt.Load(dr);
                    }
                }

                return dt;
                //return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetUserPermissionsByRole(int applicationId, string username)
        {
            try
            {
                //PermissionCollection collection = new PermissionCollection();
                DataTable dt = new DataTable("UserPermission");

                // create sql param
                SqlParameter prmApplicationID = new SqlParameter("@ApplicationID", SqlDbType.Int, 4);
                prmApplicationID.Direction = ParameterDirection.Input;
                prmApplicationID.Value = applicationId;

                SqlParameter prmUsername = new SqlParameter("@Username", SqlDbType.VarChar, 128);
                prmUsername.Direction = ParameterDirection.Input;
                prmUsername.Value = username;

                using (IDataReader dr = Database.ExecuteReader("UspGetUserPermissionsByRole", CommandType.StoredProcedure, prmApplicationID, prmUsername))
                {
                    //while (dr.Read())
                    //{
                    //    Permission permission = Populate(dr);
                    //    collection.Add(permission);
                    //}
                    if (dr.Read())
                    {
                        dt.Load(dr);
                    }
                }

                return dt;
                //return collection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePermission(int PermissionID, int Allow)
        {
            try
            {
                // create sql param
                SqlParameter prmPermissionID = new SqlParameter("@PermissionID", SqlDbType.Int, 4);
                prmPermissionID.Direction = ParameterDirection.Input;
                prmPermissionID.Value = PermissionID;

                SqlParameter prmAllow = new SqlParameter("@Allow", SqlDbType.Int, 4);
                prmAllow.Direction = ParameterDirection.Input;
                prmAllow.Value = Allow;

                Database.ExecuteNonQuery("UspUpdatePermission", CommandType.StoredProcedure
                    , prmPermissionID                    
                    , prmAllow);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="operationCode"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUserPermission(int resourceId, int operationCode, string username)
        {
            try
            {
                PermissionCollection collection = new PermissionCollection();

                // create sql param
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = resourceId;

                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operationCode;
                
                SqlParameter prmObjectName = new SqlParameter("@ObjectName", SqlDbType.VarChar, 128);
                prmObjectName.Direction = ParameterDirection.Input;
                prmObjectName.Value = username;

                SqlParameter prmObjectType = new SqlParameter("@ObjectType", SqlDbType.Int, 4);
                prmObjectType.Direction = ParameterDirection.Input;
                prmObjectType.Value = 0;  // username

                SqlParameter prmAllow = new SqlParameter("@Allow", SqlDbType.Bit, 1);
                prmAllow.Direction = ParameterDirection.Output;

                Database.ExecuteNonQuery("UspCheckPermission", CommandType.StoredProcedure
                     , prmResourceID
                     , prmOperationCode
                     , prmObjectName
                     , prmObjectType
                     , prmAllow);

                bool isAllow = bool.Parse(prmAllow.Value.ToString());

                return isAllow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationId"></param>
        /// <param name="operationCode"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckRolePermission(int resourceId, int operationCode, int roleID)
        {
            try
            {
                PermissionCollection collection = new PermissionCollection();

                // create sql param
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = resourceId;

                SqlParameter prmOperationCode = new SqlParameter("@OperationCode", SqlDbType.Int, 4);
                prmOperationCode.Direction = ParameterDirection.Input;
                prmOperationCode.Value = operationCode;

                SqlParameter prmObjectName = new SqlParameter("@ObjectName", SqlDbType.VarChar, 128);
                prmObjectName.Direction = ParameterDirection.Input;
                prmObjectName.Value = roleID.ToString();

                SqlParameter prmObjectType = new SqlParameter("@ObjectType", SqlDbType.Int, 4);
                prmObjectType.Direction = ParameterDirection.Input;
                prmObjectType.Value = 1;  // roleID

                SqlParameter prmAllow = new SqlParameter("@Allow", SqlDbType.Bit, 1);
                prmAllow.Direction = ParameterDirection.Output;

                Database.ExecuteNonQuery("UspCheckPermission", CommandType.StoredProcedure
                     , prmResourceID
                     , prmOperationCode
                     , prmObjectName
                     , prmObjectType
                     , prmAllow);

                bool isAllow = bool.Parse(prmAllow.Value.ToString());

                return isAllow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		/// <summary>
        /// Remove member from resource
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="objectName"></param>
        /// <param name="objectType"></param>
        public void RemoveMemberFromResource(int resourceId, string objectName, int objectType)
        {
            try
            {
                // create sql param
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;

                SqlParameter prmObjectName = new SqlParameter("@ObjectName", SqlDbType.VarChar, 128);
                prmObjectName.Direction = ParameterDirection.Input;

                SqlParameter prmObjectType = new SqlParameter("@ObjectType", SqlDbType.Int, 4);
                prmObjectType.Direction = ParameterDirection.Input;

                prmResourceID.Value = resourceId;

                if (string.IsNullOrEmpty(objectName))
                {
                    prmObjectName.Value = DBNull.Value;
                }
                else
                {
                    prmObjectName.Value = objectName;
                }

                prmObjectType.Value = objectType;

                Database.ExecuteNonQuery("UspRemoveUserFromResource", CommandType.StoredProcedure
                    , prmResourceID                    
                    , prmObjectName
                    , prmObjectType);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iResourceID"></param>
        /// <param name="strUserName"></param>
        /// <param name="strRoleCode"></param>
        /// <returns></returns>
        public PermissionCollection GetPermissionOnResourceByUserRole(int iResourceID, string strUserName, string strRoleCode, bool bIsAllowed)
        {
            try
            {
                PermissionCollection collection = new PermissionCollection();

                // create sql param
                SqlParameter prmResourceID = new SqlParameter("@ResourceID", SqlDbType.Int, 4);
                prmResourceID.Direction = ParameterDirection.Input;
                prmResourceID.Value = iResourceID;

                SqlParameter prmUserName = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                prmUserName.Direction = ParameterDirection.Input;
                prmUserName.Value = strUserName;
                if(string.IsNullOrEmpty(strUserName))
                    prmUserName.Value = DBNull.Value;
                    

                SqlParameter prmRoleCode = new SqlParameter("@RoleCode", SqlDbType.VarChar, 50);
                prmRoleCode.Direction = ParameterDirection.Input;
                prmRoleCode.Value = strRoleCode;

                SqlParameter prmIsAllowed = new SqlParameter("@IsAllowed", SqlDbType.Bit);
                prmIsAllowed.Direction = ParameterDirection.Input;
                prmIsAllowed.Value = bIsAllowed;

                if (string.IsNullOrEmpty(strRoleCode))
                    prmRoleCode.Value = DBNull.Value;

                using (IDataReader dr = Database.ExecuteReader("UspGetPermissionOnResourceByUserRole", CommandType.StoredProcedure
                                            , prmResourceID, prmUserName, prmRoleCode, prmIsAllowed))
                {
                    while (dr.Read())
                    {
                        Permission permission = Populate(dr);
                        collection.Add(permission);
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
        private Permission Populate(IDataReader dr)
        {
            try
            {
                int permissionId;
                int resourceId;
                int operationCode;
                string username;
                int roleID;
                bool isAllow;

                permissionId = dr.GetInt32(dr.GetOrdinal("PermissionID"));
                resourceId = dr.GetInt32(dr.GetOrdinal("ResourceID"));
                operationCode = dr.GetInt32(dr.GetOrdinal("OperationCode"));
                username = (dr["Username"] == DBNull.Value) ? string.Empty : dr.GetString(dr.GetOrdinal("Username"));
                roleID = (dr["RoleID"] == DBNull.Value) ? 0 : dr.GetInt32(dr.GetOrdinal("RoleID"));
                isAllow = dr.GetBoolean(dr.GetOrdinal("Allow"));

                Permission permission = new Permission(permissionId, resourceId, operationCode, username, roleID, isAllow);

                return permission;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
