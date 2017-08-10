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
using Inside.SecurityProviders;

namespace WebAdmin
{
    public partial class WelcomeScreen : WebAdmin.Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "12F3EBC0-A61A-4B2A-849A-7E9A8CF7F3F1";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!this.Page.IsPostBack)
            {
                try 
	            {	        
                    //ApplicationCollection appCol = new ApplicationCollection();
                    //ApplicationManager appMan = new ApplicationManager();
                    //appCol = appMan.GetApplicationsByUser(this.User.Identity.Name);
                    //dtlApplication.DataSource = appCol;
                    //dtlApplication.DataBind();
                    Session["ApplicationID"] = 10000016;
                }
	            catch (Exception ex)
	            {	
		          this.SaveErrorLog(ex);
	            }

                if (Session["ApplicationID"] != null) Response.Redirect("~/Default.aspx", true);

            }
            //this.dtlApplication.ItemCreated += new DataListItemEventHandler(dtlApplicationID_ItemCreated);
        }

        private void lbtApplication_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn;
            try
            {
                lnkBtn = (LinkButton)sender;
                if (Session["ApplicationID"] == null)
                    Session.Add("ApplicationID", lnkBtn.CommandArgument);
                else
                    Session["ApplicationID"] = lnkBtn.CommandArgument;
                Response.Redirect("~/Default.aspx", true);
            }            
            catch (Exception ex)
            {
                if (ex.Message != "Thread was being aborted.")
                {
                    this.SaveErrorLog(ex);
                }                
            }   
        }

        protected void dtlApplicationID_ItemCreated(object sender, DataListItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lbt;
                lbt = (LinkButton)e.Item.FindControl("lbtApplication");
                lbt.Click += new EventHandler(lbtApplication_Click);
            }
        }


        
    }
}
