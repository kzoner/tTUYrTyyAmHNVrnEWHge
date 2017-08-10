using System;

namespace Inside.SecurityProviders
{
    public class User
    {
        #region Member variables

        private string m_UserName = string.Empty;
        private string m_FullName = string.Empty;
        private string m_Email = string.Empty;
        private bool m_Blocked = false;
        private DateTime m_BlockedDate = new DateTime(1900, 1, 1);
        private DateTime m_CreatedTime = new DateTime(1900, 1, 1);
        private DateTime m_LastLogin = new DateTime(1900, 1, 1);
        private string m_LastIP = string.Empty;

        #endregion

        #region Constructors

        public User() { }

        public User(string userName, string fullName, string email, bool blocked, DateTime blockedDate, DateTime createdTime, DateTime lastLogin, string lastIP)
        {
            m_UserName = userName;
            m_FullName = fullName;
            m_Email = email;
            m_Blocked = blocked;
            m_BlockedDate = blockedDate;
            m_CreatedTime = createdTime;
            m_LastLogin = lastLogin;
            m_LastIP = lastIP;
        }

        #endregion

        #region Properties

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


        public string FullName 
        {
            get
            {
                return m_FullName;
            }

            set
            {
                m_FullName = value;
            }
        }
        public string Email 
        {
            get
            {
                return m_Email;
            }

            set
            {
                m_Email = value;
            }
        }
        public bool Blocked 
        {
            get
            {
                return m_Blocked;
            }

            set
            {
                m_Blocked = value;
            }
        }
        public DateTime BlockedDate 
        {
            get
            {
                return m_BlockedDate;
            }

            set
            {
                m_BlockedDate = value;
            }
        }
        public DateTime CreatedTime 
        {
            get
            {
                return m_CreatedTime;
            }

            set
            {
                m_CreatedTime = value;
            }
        }
        public DateTime LastLogin 
        {
            get
            {
                return m_LastLogin;
            }

            set
            {
                m_LastLogin = value;
            }
        }
        public string LastIP 
        {
            get
            {
                return m_LastIP;
            }

            set
            {
                m_LastIP = value;
            }
        }


        #endregion
    }
}
