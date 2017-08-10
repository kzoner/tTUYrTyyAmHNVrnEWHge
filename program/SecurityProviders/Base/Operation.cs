using System;

namespace Inside.SecurityProviders
{
    public class Operation
    {
        #region Member variables

        private int m_OperationCode = 0;
        private string m_ResourceTypeCode = string.Empty;
        private string m_Description = string.Empty;

        #endregion

        #region Constructors

        public Operation() { }

        public Operation(int operationCode, string resourceTypeCode, string description)
        {
            m_OperationCode = operationCode;
            m_ResourceTypeCode = resourceTypeCode;
            m_Description = description;
        }

        #endregion

        #region Properties

        public int OperationCode
        {
            get
            {
                return m_OperationCode;
            }
        }

        public string ResourceTypeCode
        {
            get
            {
                return m_ResourceTypeCode;
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
