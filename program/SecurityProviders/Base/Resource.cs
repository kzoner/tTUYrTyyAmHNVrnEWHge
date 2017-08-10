using System;

namespace Inside.SecurityProviders
{
    public class Resource
    {
        #region Member variables

        private int m_ResourceID = -1;
        private string m_ResourceTypeCode = string.Empty;
        private string m_path = string.Empty;
        private string m_fileName = string.Empty;
        private string m_link = string.Empty;
        private int m_ApplicationID = -1;
        private string m_ResourceName = string.Empty;
        private bool m_Status = false;
        private string m_Token = string.Empty;

        #endregion

        #region Constructors

        public Resource() { }

        public Resource(int resourceID, string resourceTypeCode, string path, string fileName, string link, int applicationID, string resourceName, bool status, string token)
        {
            m_ResourceID = resourceID;
            m_ResourceTypeCode = resourceTypeCode;
            m_path = path;
            m_fileName = fileName;
            m_link = link;
            m_ApplicationID = applicationID;
            m_ResourceName = resourceName;
            m_Status = status;
            m_Token = token;
        }
        public Resource(int resourceID, string resourceTypeCode, string path, string fileName, string link, int applicationID, string resourceName, bool status)
        {
            m_ResourceID = resourceID;
            m_ResourceTypeCode = resourceTypeCode;
            m_path = path;
            m_fileName = fileName;
            m_link = link;
            m_ApplicationID = applicationID;
            m_ResourceName = resourceName;
            m_Status = status;
        }

        #endregion

        #region Properties

        public int ResourceID
        {
            get
            {
                return m_ResourceID;
            }
        }

        public string ResourceTypeCode
        {
            get
            {
                return m_ResourceTypeCode;
            }

            set
            {
                m_ResourceTypeCode = value;
            }
        }

        public string Path
        {
            get
            {
                return m_path;
            }
            set
            {
                m_path = value;
            }
        }

        public string FileName
        {
            get
            {
                return m_fileName;
            }
            set
            {
                m_path = value;
            }
        }

        public string Link
        {
            get
            {
                return m_link;
            }
            set
            {
                m_link = value;
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

        public string ResourceName
        {
            get
            {
                return m_ResourceName;
            }

            set
            {
                m_ResourceName = value;
            }
        }

        public bool Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }

        public string Token
        {
            get
            {
                return m_Token;
            }
            set
            {
                this.m_Token = value;
            }
        }

        #endregion
    }
}
