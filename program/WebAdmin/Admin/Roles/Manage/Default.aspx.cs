using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
namespace WebAdmin.Admin.Roles.Manage
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "1F6835C4-796C-465C-9EB0-95949AE157E1";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Quản lý Roles");

                ucApplicationList.Autopostback = true;

                LoadRoleList();
            }
            uctErrorBox.Visible = false;
        }

        protected void OnSelectChange(object sender, EventArgs e)
        {
            LoadRoleList();
        }

        private void LoadRoleList()
        {
            RoleManager roleManager = new RoleManager();
            RoleCollection roles = new RoleCollection();
            try
            {
                roles = roleManager.GetRoleInApplication(int.Parse(ucApplicationList.SelectedValue));

                dgridRoles.DataSource = roles;
                dgridRoles.DataBind();
                
                if (roles.Count <= 0)
                {
                    uctErrorBox.Visible = true;
                    uctErrorBox.Message = "Không có dữ liệu";
                }
            }
            catch (Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Hệ thống bị lỗi";
                this.SaveErrorLog(ex);
            }
        }

        protected void dgridRoles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = new LinkButton();
            lbtn = (LinkButton)sender;

            RoleManager roleManage = new RoleManager();
            try
            {
                roleManage.Remove(int.Parse(lbtn.CommandArgument));
                this.SaveActionLog("Xóa role", "RoleID=" + lbtn.CommandArgument);
                LoadRoleList();
            }
            catch (Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Lỗi xóa xóa Role ";
                this.SaveErrorLog(ex);
            }
            
        }

                
    }
}
