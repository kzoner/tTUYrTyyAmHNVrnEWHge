using System;

namespace Inside.SecurityProviders
{
    public class Role
    {
        #region Member variables
        private int m_RoleID = 0;
        private string m_RoleCode = string.Empty;
        private string m_RoleName = string.Empty;
        private int m_ApplicationID = 0;

        #endregion

        #region Constructors

        public Role() { }

        public Role( int roleID, string roleCode, string roleName, int applicationID )
        {
            m_RoleID = roleID ;
            m_RoleCode = roleCode;
            m_RoleName = roleName;
            m_ApplicationID = applicationID;
        }

        #endregion

        #region Properties


        public int RoleID
        {
            get {
                return m_RoleID;            
            }
            set
            {
                m_RoleID = value;
            }
        }

        public string RoleCode
        {
            get
            {
                return m_RoleCode;
            }
        }

        public string RoleName
        {
            get
            {
                return m_RoleName;
            }

            set
            {
                m_RoleName = value;
            }
        }

        public int ApplicationID
        {
            get
            {
                return m_ApplicationID;
            }

            set
            {
                m_ApplicationID = value;
            }
        }

        #endregion
    }
}
