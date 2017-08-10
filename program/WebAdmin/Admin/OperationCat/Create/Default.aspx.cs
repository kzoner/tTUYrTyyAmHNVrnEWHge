using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.OperationCat.Create
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "DA84FA7C-57C9-4BC8-9171-58A59F67F1A2";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Tạo loại thao tác");
                uctConfirmBox.Visible = false;
            }
            uctErrorBox.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            OperationCategoryManager operationManager = new OperationCategoryManager();
            try
            {
                operationManager.Create(txtName.Text.Trim(),txtDescription.Text.Trim());
                this.SaveActionLog("Tạo mới", "Tạo mới loại thao tác " + txtName.Text);
                uctConfirmBox.Visible = true;
                uctConfirmBox.ConfirmMessage = "Tạo thành công. <br/> Bạn tiếp tục tạo thao tác mới không ?";
            }
            catch(Exception ex)
            {
                this.uctErrorBox.Visible = true;
                this.uctErrorBox.Message = "Hệ thống có lỗi: " + ex.Message;
                this.SaveErrorLog(ex);
            }

        }

        public void ClickedYes(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtDescription.Text = "";
            this.uctConfirmBox.Visible = false;
        }

        public void ClickedNo(object sender, EventArgs e)
        {
            Response.Redirect("../Manage/");
        }
    }
}
