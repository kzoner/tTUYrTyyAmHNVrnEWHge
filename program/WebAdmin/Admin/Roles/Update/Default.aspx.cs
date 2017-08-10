using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;


namespace WebAdmin.Admin.Roles.Update
{
    public partial class Default : Base.BasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "0647FF94-92C0-4E7E-A32D-D49C50839BB0";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Cập nhật Role");
                LoadRole();
            }
            uctErrorBox.Visible = false;

        }

        protected void LoadRole()
        {
            if(Request.QueryString["id"] == string.Empty)
                return;

            RoleManager roleManage = new RoleManager();
            Role role = new Role();
            try
            {
                role = roleManage.GetRole(int.Parse(Request.QueryString["id"]));
                uctApplicationList.SelectedValue = role.ApplicationID.ToString();

                txtRoleCode.Text = role.RoleCode;
                txtRoleName.Text = role.RoleName;
            }
            catch (Exception ex)
            {
                uctErrorBox.Message = "Có lỗi hệ thống";
                uctErrorBox.Visible = true;
                this.SaveErrorLog(ex);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            RoleManager roleManager = new RoleManager();
            Role role = new Role(int.Parse(Request.QueryString["id"]), txtRoleCode.Text.Trim(), txtRoleName.Text.Trim(), int.Parse(uctApplicationList.SelectedValue));
            try
            {
                roleManager.Update(role);
                Response.Redirect("../Manage/");
            }
            catch (Exception ex)
            {
                uctErrorBox.Message = "Cập nhật Role không thành công.";
                uctErrorBox.Visible = true;
                this.SaveErrorLog(ex);
            }
        }
        
    }
}
