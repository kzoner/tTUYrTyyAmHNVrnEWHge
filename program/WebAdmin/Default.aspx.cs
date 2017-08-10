using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Inside.SecurityProviders;

namespace WebAdmin
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ApplicationID"] == null || string.IsNullOrEmpty(Session["ApplicationID"].ToString()))
            {
                Response.Redirect("~/WelcomeScreen.aspx", true);
            }
        }
    }
}