using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inside.SecurityProviders;

namespace WebAdmin.Base
{
    public class BaseWebservice : System.Web.Services.WebService
    {
        protected void SaveErrorLog(Exception ex)
        {
            ErrorLog pageEx = new ErrorLog("", this.Context.Request.RawUrl, ex);
            ErrorLogManager ErrManObj = new ErrorLogManager();
            ErrManObj.SavePageExeption(pageEx);
        }
    }
}
