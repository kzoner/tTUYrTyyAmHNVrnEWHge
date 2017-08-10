using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebAdmin.Base
{
    public class LocalEnum
    {
        /// <summary>
        /// cac trang thai cua form
        /// </summary>
        public enum FormState
        {
            Default,
            ErrorState,
            ConfirmState
        }


        /// <summary>
        /// Cac kieu tim kiem User : AllUser, UserName,Email,FullName
        /// </summary>
        public enum UserSearchBy
        {
            AllUsers = 0,
            UserName = 1,
            Email = 2,
            FullName = 3
        }
    }
}
