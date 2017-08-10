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
using Inside.SecurityProviders;
namespace WebAdmin.Base
{
    public class BaseControl : System.Web.UI.UserControl
    {
        protected override void OnError(EventArgs e)
        {
            Exception curException = Server.GetLastError().GetBaseException();
            ErrorLog errObj = new ErrorLog(this.Page.User.Identity.Name, this.Request.RawUrl, curException);
            ErrorLogManager ErrManObj = new ErrorLogManager();
            ErrManObj.SavePageExeption(errObj);
            base.OnError(e);
            //this.Server.ClearError();
        }

        protected void SaveErrorLog(Exception ex)
        {
            ErrorLog pageEx = new ErrorLog(this.Page.User.Identity.Name, this.Request.RawUrl, ex);
            ErrorLogManager ErrManObj = new ErrorLogManager();
            ErrManObj.SavePageExeption(pageEx);
        }

        protected void SaveActionLog(string strOperation, string strData)
        {
            ActionLog al = new ActionLog();
            al.LogDate = DateTime.Now;
            al.IP = this.Request.ServerVariables["REMOTE_ADDR"];
            al.PageTitle = this.Page.Title;
            al.Path = this.Request.RawUrl;
            al.UserName = this.Page.User.Identity.Name;
            al.Operation = strOperation;
            al.Data = strData;
            ActionLogManager alManager = new ActionLogManager();
            alManager.SaveActionLog(al);
        }
    }
}
