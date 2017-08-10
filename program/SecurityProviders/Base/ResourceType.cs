using System;

namespace Inside.SecurityProviders
{
    public class ResourceType
    {
        #region Member variables

        private string m_ResourceTypeCode = string.Empty;
        private string m_Name = string.Empty;        

        #endregion

        #region Constructors

        public ResourceType() { }

        public ResourceType(string resourceTypeCode, string name)
        {
            m_ResourceTypeCode = resourceTypeCode;
            m_Name = name;
        }

        #endregion

        #region Properties

        public string ResourceTypeCode
        {
            get
            {
                return m_ResourceTypeCode;
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
       
        #endregion
    }
}
