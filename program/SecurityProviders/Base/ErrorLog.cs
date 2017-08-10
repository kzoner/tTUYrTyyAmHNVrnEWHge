using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inside.SecurityProviders
{
    public class ErrorLog
    {
        #region Members
            private DateTime m_dateErrorDate;
            private string m_strCurrentUser;
            private string m_strPath;
            private Exception m_exException;
	    #endregion
        #region Properties

            /// <summary>
            /// thoi gian xay ra loi
            /// </summary>
            public DateTime ErrorDate { 
                get{
                    return this.m_dateErrorDate;
                } 
                set{
                    this.m_dateErrorDate = value;
                }
            }
            /// <summary>
            /// user hien tai khi trang xay ra loi
            /// </summary>
            public string CurrentUser { 
                get{
                    return this.m_strCurrentUser;
                } 
                set{
                    this.m_strCurrentUser = value;
                }
            }
            /// <summary>
            /// URL cua page xay ra loi
            /// </summary>
            public string Path { 
                get{
                    return this.m_strPath;
                } 
                set{
                    this.m_strPath = value;
                }
            }
            /// <summary>
            /// chi tiet loi
            /// </summary>
            public Exception Exception { 
                get{
                    return this.m_exException;
                } 
                set{
                    this.m_exException = value;
                }
            }
	    #endregion   
        #region Contructor
            public ErrorLog(string strCurrenrUser, string strPath, Exception exException)
            {
                this.m_dateErrorDate = DateTime.Now;
                this.m_strCurrentUser = strCurrenrUser;
                this.m_strPath = strPath;
                this.m_exException = exException;
            }
            public ErrorLog(DateTime dateErrorDate, string strCurrenrUser, string strPath, Exception exException)
            {
                this.m_dateErrorDate = dateErrorDate;
                this.m_strCurrentUser = strCurrenrUser;
                this.m_strPath = strPath;
                this.m_exException = exException;
            }
	    #endregion
    }
}
