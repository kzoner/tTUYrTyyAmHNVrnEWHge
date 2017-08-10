using System;
using System.Collections.Generic;
using System.Text;
using Inside.SecurityProviders.DataAccess;
using Inside.SecurityProviders;
using System.Data;

namespace Inside.SecurityProviders
{
    public class ErrorLogManager
    {
        private ErrorLogAdapter m_pageExceptionAdapter = null;

        private ErrorLogAdapter Adapter
        {
            get
            {
                if (m_pageExceptionAdapter == null)
                {
                    m_pageExceptionAdapter = new ErrorLogAdapter();
                }

                return m_pageExceptionAdapter;
            }
        }
        /// <summary>
        /// ghi nhan loi vao database
        /// </summary>
        /// <param name="pageEx"></param>
        public void SavePageExeption(ErrorLog pageEx)
        {
            try
            {
                this.Adapter.Save(pageEx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetErrorLog(DateTime FromDate, DateTime ToDate, string CurrentUser)
        {
            DataTable dtRet = new DataTable();
            dtRet = this.Adapter.GetErrorLog(FromDate, ToDate, CurrentUser);
            return dtRet;
        }

        public void ClearErrorLog()
        {
            this.Adapter.ClearErrorLog();
        }
    }
}
