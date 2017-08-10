using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
namespace WebAdmin.Admin.Application.Update
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "7A9B00AE-B85B-4DD1-B4EC-6548C6562B50";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Cập nhật ứng dụng");
                LoadAppData();
            }
            uctErrorBox.Visible = false;
            
        }

        private void LoadAppData()
        {
            if (Request.QueryString["id"] == null)
                return;
            ApplicationManager appManager = new ApplicationManager();

            Inside.SecurityProviders.Application app = new Inside.SecurityProviders.Application();

            app = appManager.GetApplication(int.Parse(Request.QueryString["id"]));
            txtAppName.Text = app.Name;
            txtAppDescription.Text = app.Description;
            if (ViewState["app"] == null)
            {
                ViewState.Add("app", app);
            }
            else
            {
                ViewState["app"] = app;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ApplicationManager appManager = new ApplicationManager();
            Inside.SecurityProviders.Application app = 
                new Inside.SecurityProviders.Application(int.Parse(Request.QueryString["id"]), 
                    txtAppName.Text.Trim(), txtAppDescription.Text.Trim());
            try
            {
                appManager.Update(app);

                //xu ly ghi log
                Inside.SecurityProviders.Application oldApp;
                oldApp = (Inside.SecurityProviders.Application)ViewState["app"];
                this.SaveActionLog("Update", Request.QueryString["id"] + "::" + oldApp.Name + "-->" + txtAppName.Text.Trim());
                Response.Redirect("../Manage/");
            }
            catch(Exception ex)
            {
                this.SaveErrorLog(ex);
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Cập nhật ứng dụng không thành công.<br /> Có thể tên ứng dụng bị trùng ";
            }
            
        }
    }
}
