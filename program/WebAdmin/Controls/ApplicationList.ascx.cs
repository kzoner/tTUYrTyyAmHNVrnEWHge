using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
namespace WebAdmin.Controls
{
    public partial class ApplicationList : System.Web.UI.UserControl
    {
        public event EventHandler SelectChange;

        protected override void OnInit(EventArgs e)
        {
            LoadAppList();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoadAppList()
        {
            ApplicationManager appManager = new ApplicationManager();

            Inside.SecurityProviders.ApplicationCollection apps = new ApplicationCollection();

            apps = appManager.GetAllApplications();

            cmbAppList.DataSource = apps;
            cmbAppList.DataTextField = "Name";
            cmbAppList.DataValueField = "ApplicationID";
            cmbAppList.DataBind();
            if(cmbAppList.Items.Count > 0)
            {
                cmbAppList.SelectedIndex = 0;
            }
        }

        protected void cmbAppList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectChange != null)
            {
                SelectChange(sender, EventArgs.Empty  );
            }
        }

        
        public bool Autopostback
        {
            set
            {
                cmbAppList.AutoPostBack = value;
            }
        }

        public string GetFirstValue()
        {
            return cmbAppList.Items[1].Value;
        }

        public string SelectedValue
        {
            set 
            {
                cmbAppList.SelectedValue = value;    
            }
            get
            {
                return cmbAppList.SelectedValue;
            }
        }

    }
}