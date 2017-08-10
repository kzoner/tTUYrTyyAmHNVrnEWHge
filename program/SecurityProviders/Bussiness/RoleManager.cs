using System;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
namespace Inside.SecurityProviders
{
    public class RoleManager
    {
        private RoleAdapter m_roleAdapter = null;

        private RoleAdapter Adapter
        {
            get
            {
                if (m_roleAdapter == null)
                {
                    m_roleAdapter = new RoleAdapter();
                }

                return m_roleAdapter;
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
            // validate input parameters
            if (string.IsNullOrEmpty(roleCode)) throw new SecurityException("Create role fail. Role code can not empty");
            if (string.IsNullOrEmpty(roleName)) throw new SecurityException("Create role fail. Role name can not empty");

            return Adapter.Create(roleCode,roleName,applicationID );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        public void Update(Role role)
        {
            if (string.IsNullOrEmpty(role.RoleCode)) throw new SecurityException("Create role fail. Role code can not empty");
            if (string.IsNullOrEmpty(role.RoleName)) throw new SecurityException("Create role fail. Role name can not empty");

            Adapter.Update(role);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        public void Remove(int roleID)
        {
            Adapter.Remove(roleID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public Role GetRole(int roleID)
        {
            return Adapter.GetRole(roleID);
        }

        /// <summary>
        /// Lay danh sach Role trong mot ung dung
        /// </summary>
        /// <param name="applicationID">ID cua ung dung</param>
        /// <returns></returns>
        public RoleCollection GetRoleInApplication(int applicationID)
        {
            return Adapter.GetRoleInApplication(applicationID);
        }

        /// <summary>
        /// Tim role theo role code
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public RoleCollection FindRolesByCode(string roleCode, int applicationID)
        {
            return Adapter.FindRolesByCode(roleCode, applicationID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RoleCollection GetAllRoles()
        {
            return Adapter.GetAllRoles();
        }

        public RoleCollection GetRolesOfUser(string userName)
        {
            return Adapter.GetRolesOfUser(userName );
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void AddUserToRole(string userName, int roleID)
        {
            Adapter.AddUserToRole(userName, roleID);
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
                    Adapter.AddUserToRole(user.UserName, roleID);
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        /// <param name="role"></param>
        public void AddUserToRoles(string user, int[] roleIDs)
        {
            foreach (int role in roleIDs)
            {
                try
                {
                    Adapter.AddUserToRole(user, role);
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public void RemoveUserFromRole(string userName, int roleID)
        {
            Adapter.RemoveUserFromRole(userName, roleID);
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
                    Adapter.RemoveUserFromRole(user.UserName, roleID);
                }
                catch { }
            }
        }

        /// <summary>
        /// Set permission for Role
        /// </summary>
        /// <param name="iApplicationID"></param>
        /// <param name="strRoleCode"></param>
        /// <param name="iResourceID"></param>
        /// <param name="strDataPermission"></param>
        /// <returns></returns>
        public ReturnObject SetPermission(int iApplicationID, string strRoleCode, int iResourceID, string strDataPermission)
        {
            return Adapter.SetPermission(iApplicationID, strRoleCode, iResourceID, strDataPermission);
        }
    }
}
