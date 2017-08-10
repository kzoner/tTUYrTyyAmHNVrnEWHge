using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
namespace WebAdmin.Admin.Roles.Create
{
    public partial class Default : Base.BasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "82E6945F-D257-4A7D-AEA6-500114CC1BDA";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Tạo Role");
            }

            uctConfirmBox.Visible = false;
            uctErrorBox.Visible = false;

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {

            RoleManager roleManager = new RoleManager();
            try
            {
                roleManager.Create(txtRoleCode.Text.Trim(), txtRoleName.Text.Trim(),int.Parse(uctApplicationList.SelectedValue));

                this.SaveActionLog("Tạo Role mới", txtRoleName.Text.Trim());
                
                uctConfirmBox.Visible = true;
                uctConfirmBox.ConfirmMessage = "Tạo Role mới thành công. <br />Bạn có tiếp tục tạo Role mới không ?";
                
            }
            catch(Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Có lỗi khi tạo Role mới. Kiểm tra Mã role có thể bị trùng !";
                this.SaveErrorLog(ex);
            }
            
        }

        public void ClickedYes(object sender, EventArgs e)
        {
            txtRoleCode.Text = "";
            txtRoleName.Text = "";
        }

        public void ClickedNo(object sender, EventArgs e)
        {
            Response.Redirect("../Manage/");
        }

    }
}
