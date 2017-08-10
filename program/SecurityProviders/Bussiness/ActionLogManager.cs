using System;
using System.Collections.Generic;
using System.Text;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
using System.Data;

namespace Inside.SecurityProviders
{
    public class ActionLogManager
    {
        private ActionLogAdapter m_alExceptionAdapter = null;

        private ActionLogAdapter Adapter
        {
            get
            {
                if (m_alExceptionAdapter == null)
                {
                    m_alExceptionAdapter = new ActionLogAdapter();
                }

                return m_alExceptionAdapter;
            }
        }
        /// <summary>
        /// ghi nhan loi vao database
        /// </summary>
        /// <param name="pageEx"></param>
        public void SaveActionLog(ActionLog alActionLog)
        {
            try
            {
                this.Adapter.Save(alActionLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetActionLogs(DateTime FromDate, DateTime ToDate, string IP, string UserName)
        {
            return Adapter.GetActionLogs(FromDate, ToDate, IP, UserName);
        }

        public DataTable SearchActionLogs(DateTime FromDate, DateTime ToDate, int SearchType, string Keyword)
        {
            return Adapter.SearchActionLogs(FromDate, ToDate, SearchType, Keyword);
        }

        public DataTable GetLoginLogs(DateTime FromDate, DateTime ToDate, string UserName)
        {
            return Adapter.GetLoginLogs(FromDate, ToDate, UserName);
        }
    }
}
