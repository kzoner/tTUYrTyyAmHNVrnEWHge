using System;

namespace Inside.SecurityProviders
{
    public class Permission
    {
        #region Member variables

        private int m_PermissionID = -1;
        private int m_ResourceID = -1;
        private int m_OperationCode = -1;
        private string m_UserName = string.Empty;
        private int m_RoleID = -1;
        private bool m_Allow = false;

        #endregion

        #region Constructors

        public Permission() { }

        public Permission(int permissionID, int resourceID, int operationCode, string userName, int roleID, bool allow)
        {
            m_PermissionID = permissionID;
            m_ResourceID = resourceID;
            m_OperationCode = operationCode;
            m_UserName = userName;
            m_RoleID = roleID;
            m_Allow = allow;
        }

        #endregion

        #region Properties

        public int PermissionID
        {
            get
            {
                return m_PermissionID;
            }
        }

        public int ResourceID
        {
            get
            {
                return m_ResourceID;
            }

            set
            {
                m_ResourceID = value;
            }
        }

        public int OperationCode
        {
            get
            {
                return m_OperationCode;
            }
            set
            {
                m_OperationCode = value;
            }
        }

        public string UserName
        {
            get
            {
                return m_UserName;
            }

            set
            {
                m_UserName = value;
            }                
        }

        public int RoleID
        {
            get
            {
                return m_RoleID;
            }

            set
            {
                m_RoleID = value;
            }
        }

        public bool Allow
        {
            get
            {
                return m_Allow;
            }

            set
            {
                m_Allow = value;
            }
        }

        #endregion
    }
}
