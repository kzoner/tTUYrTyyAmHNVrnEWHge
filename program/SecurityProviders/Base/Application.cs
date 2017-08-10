using System;

namespace Inside.SecurityProviders
{
    [Serializable]
    public class Application
    {
        #region Member variables

        private int m_ApplicationID = -1;
        private string m_Name = string.Empty;
        private string m_Description = string.Empty;

        #endregion

        #region Constructors

        public Application()
        {
        }

        public Application(int applicationId, string name, string description)
        {
            m_ApplicationID = applicationId;
            m_Name = name;
            m_Description = description;
        }

        #endregion

        #region Properties

        public int ApplicationID
        {
            get
            {
                return m_ApplicationID;
            }            
        }
        
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        #endregion

    }
}
