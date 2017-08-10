using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inside.SecurityProviders
{
    public class GlobalEnum
    {
        /// <summary>
        /// cac trang thai co the co khi dang nhap
        /// </summary>
        public enum LoginStatus
        {
            OK = 1,
            FailPassword = 4,
            AccountNotExist = 3,
            AccountIsLocked = 2,
            Error = 5
        }
    }
    public struct ReturnObject
    {
        public int ErrorNumber;
        public string ErrorMessage;
    }
}
