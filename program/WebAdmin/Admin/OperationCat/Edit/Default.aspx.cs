using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.OperationCat.Edit
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "9B1F492D-698F-4DD7-9EF4-ADFF69D73606";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Cập nhật loại thao tác");
                LoadOperationCat();
            }
            uctErrorBox.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            OperationCategoryManager OperationCatManager = new OperationCategoryManager();
            try
            {
                OperationCategory operationCat =
                new OperationCategory(int.Parse(Request.QueryString["code"]), txtName.Text.Trim(), txtDescription.Text.Trim());
                OperationCatManager.Update(operationCat);
                Response.Redirect("../Manage/");
            }
            catch (Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Hệ thống có lỗi: " + ex.Message;

                this.SaveErrorLog(ex);
            }
        }

        protected void LoadOperationCat()
        {
            if(Request.QueryString["code"] == string.Empty)
                return;
            OperationCategoryManager operationCatManager = new OperationCategoryManager();
            OperationCategory operationCat = new OperationCategory();
             operationCat = operationCatManager.GetOperationCategory(int.Parse(Request.QueryString["code"]));
             txtName.Text = operationCat.Name;
             txtDescription.Text = operationCat.Description;
        }
    }
}
