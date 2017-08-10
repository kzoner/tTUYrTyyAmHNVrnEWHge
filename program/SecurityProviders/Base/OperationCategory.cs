using System;
using System.Collections;

namespace Inside.SecurityProviders
{
    public class OperationCategory : CollectionBase
    {
        #region Member variables

        private int m_OperationCode = 0;
        private string m_Name = string.Empty;
        private string m_Description = string.Empty;

        #endregion

        #region Constructors

        public OperationCategory() { }

        public OperationCategory(int operationCode, string name,string description)
        {
            m_OperationCode = operationCode;
            m_Name = name;
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
            set
            {
                m_OperationCode = value;
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
            set
            {
                m_Description = value;
            }
            get
            {
                return m_Description;
            }
        }

        #endregion
    }
}
