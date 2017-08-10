using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebAdmin.SignOut
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookieUserName = Request.Cookies.Get("INSUserName");
            if (cookieUserName != null)
            {
                cookieUserName.Expires = DateTime.Now.AddYears(-100);
                Response.Cookies.Add(cookieUserName);            
            }            

            Response.Redirect("../SignIn/");
        }
    }
}
