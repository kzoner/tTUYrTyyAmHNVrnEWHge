using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
using System.Data;

namespace Inside.SecurityProviders
{
    public class PermissionManager
    {

        private PermissionAdapter m_permissionAdapter = null;

        private PermissionAdapter Adapter
        {
            get
            {
                if (m_permissionAdapter == null)
                {
                    m_permissionAdapter = new PermissionAdapter();
                }
                return m_permissionAdapter;
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
        public Permission SetPermission(int resourceId, int operationCode, string objectName, int objectType, bool allow)
        {
            try
            {
                if ((string.IsNullOrEmpty(operationCode.ToString())) || (operationCode <= 0)) throw new SecurityException("Set permission fail. Operation code must not empty");
                if (string.IsNullOrEmpty(objectName)) throw new SecurityException("Set permission fail. Role or username must not empty");

                return Adapter.SetPermission(resourceId, operationCode, objectName, objectType, allow);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Remove user from Resource
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="objectName"></param>
        /// <param name="objectType"></param>
        public void RemoveMemberFromResource(int resourceId, string objectName, int objectType)
        {
            try
            {
                Adapter.RemoveMemberFromResource(resourceId, objectName, objectType);
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
                return Adapter.GetPermissions(applicationId);
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
                if ((string.IsNullOrEmpty(roleID.ToString())) || (roleID == 0)) throw new SecurityException("Invalid role ID!");

                return Adapter.GetRolePermissions(applicationId, roleID);
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
                if (string.IsNullOrEmpty(username)) throw new SecurityException("Invalid username!");

                return Adapter.GetUserPermissionsByRole(applicationId, username);
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
                if (string.IsNullOrEmpty(username)) throw new SecurityException("Invalid username!");

                return Adapter.GetUserPermissions(applicationId, username);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePermission(int permissionID, int allow)
        {
            try
            {
                Adapter.UpdatePermission(permissionID, allow);
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
                if ((string.IsNullOrEmpty(operationCode.ToString())) || (operationCode <= 0)) throw new SecurityException("Invalid operation code");
                if (string.IsNullOrEmpty(username)) throw new SecurityException("Invalid username!");

                return Adapter.CheckUserPermission(resourceId, operationCode, username);
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
                if ((string.IsNullOrEmpty(operationCode.ToString())) || (operationCode <= 0)) throw new SecurityException("Invalid operation code");
                if ((string.IsNullOrEmpty(roleID.ToString())) || (roleID == 0)) throw new SecurityException("Invalid role ID!");

                return Adapter.CheckRolePermission(resourceId, operationCode, roleID);
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
            return Adapter.GetPermissionOnResourceByUserRole(iResourceID, strUserName, strRoleCode, bIsAllowed);
        }
    }
}
