using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inside.SecurityProviders
{
    public class ActionLog
    {
        #region Members
        private DateTime m_dateLogDate;
        private string m_strIP;
        private string m_strUserName;
        private string m_strPath;
        private string m_strPageTitle;
        private string m_strOperation;
        private string m_strData;
        #endregion

        #region Properties
        /// <summary>
        /// thoi gian ghi nhan
        /// </summary>
        public DateTime LogDate 
        { 
            get{
                return m_dateLogDate;    
            }
            set{
                this.m_dateLogDate = value;
            }
        }

        /// <summary>
        /// IP cua nguoi dung
        /// </summary>
        public string IP
        {
            get
            {
                return m_strIP;
            }
            set
            {
                this.m_strIP = value;
            }
        }

        /// <summary>
        /// nguoi dung
        /// </summary>
        public string UserName
        {
            get
            {
                return m_strUserName;
            }
            set
            {
                this.m_strUserName = value;
            }
        }

        /// <summary>
        /// URL cua page dang su dung
        /// </summary>
        public string Path
        {
            get
            {
                return m_strPath;
            }
            set
            {
                this.m_strPath = value;
            }
        }

        /// <summary>
        /// chuc nang su dung
        /// </summary>
        public string PageTitle 
        { 
            get
            {
                return this.m_strPageTitle;
            }
            set
            {
                this.m_strPageTitle = value;
            }
        }

        /// <summary>
        /// thao tac tren du lieu
        /// </summary>
        public string Operation
        { 
            get
            {
                return this.m_strOperation;
            }
            set
            {
                this.m_strOperation = value;
            }
        }

        /// <summary>
        /// cac du lieu khac
        /// </summary>
        public string Data
        { 
            get
            {
                return this.m_strData;
            }
            set
            {
                this.m_strData = value;
            }
        }
        #endregion

        #region Contructor
        public ActionLog() { }

        #endregion
    }
}
