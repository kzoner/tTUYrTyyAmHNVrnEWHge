using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Users.RemovePermission
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "DAC10BF3-FC7B-4851-92A2-89EC9F253269";
        }

        private void LoadApplications()
        {
            try
            {
                ApplicationManager appMan = new ApplicationManager();
                ApplicationCollection apps = appMan.GetAllApplications();

                if (apps.Count > 0)
                {
                    ddlApplications.DataSource = apps;
                    ddlApplications.DataValueField = "ApplicationID";
                    ddlApplications.DataTextField = "Name";
                    ddlApplications.DataBind();

                    if (Request["id"] != null)
                    {
                        ddlApplications.SelectedValue = Request["id"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Lỗi đọc danh sách ứng dụng.";
                this.SaveErrorLog(ex);
            }

        }

        private void LoadPermissions()
        {
            try
            {
                int applicationID = int.Parse(ddlApplications.SelectedValue);
                string userName = txtUserName.Text.Trim();

                PermissionManager perMan = new PermissionManager();
                DataTable dt = new DataTable("UserPermissions");
                dt = perMan.GetUserPermissions(applicationID, userName);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    gvPermissionList.DataSource = dt;
                    gvPermissionList.DataBind();

                    gvPermissionList.Visible = true;
                    ucErrorBox.Visible = false;

                    btnDeleteAll.Visible = true;                    
                }
                else
                {
                    ucErrorBox.Message = "Không có dữ liệu.";

                    gvPermissionList.Visible = false;
                    ucErrorBox.Visible = true;

                    btnDeleteAll.Visible = false;                    
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Có lỗi hệ thống.";
                
                gvPermissionList.Visible = false;
                ucErrorBox.Visible = true;

                btnDeleteAll.Visible = false;                

                SaveErrorLog(ex);
            }
        }

        private void LoadRolePermissions()
        {
            try
            {
                int applicationID = int.Parse(ddlApplications.SelectedValue);
                string userName = txtUserName.Text.Trim();

                PermissionManager perMan = new PermissionManager();
                DataTable dt = new DataTable("RolePermissions");
                dt = perMan.GetUserPermissionsByRole(applicationID, userName);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    gvRolePermissionList.DataSource = dt;
                    gvRolePermissionList.DataBind();

                    gvRolePermissionList.Visible = true;
                    ucErrorBox.Visible = false;

                    btnDeleteAll.Visible = true;
                }
                else
                {
                    ucErrorBox.Message += "<br />Không có quyền từ Role.";

                    gvRolePermissionList.Visible = false;
                    ucErrorBox.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Có lỗi hệ thống.";

                gvRolePermissionList.Visible = false;
                ucErrorBox.Visible = true;

                SaveErrorLog(ex);
            }
        }

        private void UpdatePermission(int permissionID, int allow)
        {
            try
            {
                PermissionManager perMan = new PermissionManager();
                perMan.UpdatePermission(permissionID, allow);

                ucErrorBox.Visible = false;                
            }
            catch (Exception)
            {
                ucErrorBox.Message = "Có lỗi cập nhật.";
                ucErrorBox.Visible = true;
            }
        }

        protected bool MapStatus(int iStatus)
        {
            bool bStatus = false;

            if (iStatus.ToString() == "1") 
                bStatus = true;
            return bStatus;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Xem quyền user");
                LoadApplications();
                ((ContentPage)Master).SetFormTitle("Xem quyền User");
            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow gvr in gvPermissionList.Rows)
        //    {
        //        int PermissionID = int.Parse(gvr.Cells[1].Text);
        //        CheckBox cbxAllow = (CheckBox)(gvr.FindControl("cbxAllow"));
        //        CheckBox cbxDeny = (CheckBox)(gvr.FindControl("cbxDeny"));
        //        int Allow = -1;
        //        if ((cbxAllow.Checked) || (cbxDeny.Checked))
        //        {
        //            if (cbxAllow.Checked)
        //            {
        //                Allow = 1;
        //            }
        //            else
        //            {
        //                Allow = 0;
        //            }
        //        }

        //        UpdatePermission(PermissionID, Allow);
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPermissions();
            LoadRolePermissions();
        }

        protected void Next_Clicked(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        //protected void cbxAllow_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox cbxAllow = (CheckBox)(sender);
        //    if (cbxAllow.Checked)
        //    {
        //        int index = int.Parse(cbxAllow.Text);
        //        GridViewRow gvr = gvPermissionList.Rows[index];
        //        CheckBox cbxDeny = (CheckBox)(gvr.FindControl("cbxDeny"));
        //        cbxDeny.Checked = false;
        //    }            
        //}

        //protected void cbxDeny_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox cbxDeny = (CheckBox)(sender);
        //    if (cbxDeny.Checked)
        //    {
        //        int index = int.Parse(cbxDeny.Text);
        //        GridViewRow gvr = gvPermissionList.Rows[index];
        //        CheckBox cbxAllow = (CheckBox)(gvr.FindControl("cbxDeny"));
        //        cbxAllow.Checked = false;
        //    }
        //}

        protected void lbtnDelete_Clicked(object sender, EventArgs e)
        {
            LinkButton lbtnDelete = (LinkButton)sender;
            int PermissionID = int.Parse(lbtnDelete.CommandArgument);
            UpdatePermission(PermissionID, -1);
            LoadPermissions();
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            int PermissionID;
            foreach (GridViewRow row in gvPermissionList.Rows)
            {
                LinkButton lbtnDelete = (LinkButton)(row.FindControl("lbtnDelete"));
                PermissionID = int.Parse(lbtnDelete.CommandArgument);
                UpdatePermission(PermissionID, -1);                
            }
            LoadPermissions();
        }

        protected void gvPermissionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPermissionList.PageIndex = e.NewPageIndex;
            LoadPermissions();
        }

        protected void gvRolePermissionList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRolePermissionList.PageIndex = e.NewPageIndex;
            LoadRolePermissions();
        }
    }
}
