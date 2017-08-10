using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin
{
    public partial class MenuTree : WebAdmin.Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "4EFE355D-A1A7-46AA-BAA8-6CCCB4F62179";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                try
                {
                    if (Session["ApplicationID"] != null && !string.IsNullOrEmpty(Session["ApplicationID"].ToString()))
                    {
                        this.LoadTreeViewMenu(int.Parse(Session["ApplicationID"].ToString()), this.Page.User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }
        }

        private void LoadTreeViewMenu(int iApplicationID, string strUserName)
        {
            try
            {
                Inside.SecurityProviders.MenuItemCollection mnuColection = new Inside.SecurityProviders.MenuItemCollection();
                Inside.SecurityProviders.MenuManager mnuManager = new Inside.SecurityProviders.MenuManager();
                mnuColection = mnuManager.GetUserMenus(iApplicationID, strUserName);

                string xmlMenu = "";

                if (mnuColection.Count > 0)
                {
                    xmlMenu = mnuColection.ToString();

                    this.xmlSrc.Data = xmlMenu;
                    this.xmlSrc.DataBind();

                    tvwMenu.DataSourceID = "xmlSrc";
                    this.tvwMenu.DataBind();
                }

            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
            }
        }

        protected void tvwMenu_SelectedNodeChanged(object sender, EventArgs e)
        {

            if (tvwMenu.SelectedNode.Expanded == true)
                tvwMenu.SelectedNode.Collapse();
            else
                tvwMenu.SelectedNode.Expand();
        }

    }
}
