using System;

namespace Inside.SecurityProviders
{
    class Membership
    {
        #region Member variables
        private string m_UserName = string.Empty;
        private string m_RoleCode = string.Empty;
        #endregion

        #region Constructors

        public Membership() { }

        public Membership(string userName, string roleCode)
        {
            m_UserName = userName;
            m_RoleCode = roleCode;
        }

        #endregion

        #region Properties

        public string UserName
        {
            get
            {
                return m_UserName;
            }
        }

        public string RoleCode
        {
            get
            {
                return m_RoleCode;
            }
        }

        #endregion
    }
}
