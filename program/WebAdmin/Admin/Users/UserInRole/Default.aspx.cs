using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
using System.Data;

namespace WebAdmin.Admin.Users.UserInRole
{
    public partial class Default : Base.BasePage
    {

        const int pageSize = 20;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "2FBC2869-A5F2-4A6F-813D-CD819989E109";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Người dùng theo nhóm");

                ApplicationList1.Autopostback = true;

                if (Request.QueryString["appID"] != null)
                {
                    ApplicationList1.SelectedValue = Request.QueryString["appID"];

                    LoadRoles(int.Parse(Request.QueryString["appID"]));
                }
                else
                {
                    LoadRoles( int.Parse(ApplicationList1.SelectedValue) );
                }
                if (cmbRoles.Items.Count > 0)
                {
                    if (Request.QueryString["roleID"] != string.Empty)
                    {
                        cmbRoles.SelectedValue = Request.QueryString["roleID"];
                    }
                    else
                    {
                        cmbRoles.SelectedIndex = 0;
                    }

                    LoadUsers(int.Parse(cmbRoles.SelectedValue), 0, int.Parse(cmbSearchBy.SelectedValue), txtKeyWord.Text.Trim());
                }
            }
            //uctErrorBox.Visible = false;
        }

        protected void LoadRoles( int appID  )
        {
            RoleManager roleManger = new RoleManager();
            RoleCollection roles = new RoleCollection();
            try
            {
                roles = roleManger.GetRoleInApplication(appID);

                cmbRoles.Items.Clear();
                
                if (roles.Count > 0)
                {
                    cmbRoles.DataSource = roles;
                    cmbRoles.DataTextField = "RoleName";
                    cmbRoles.DataValueField = "RoleID";
                    cmbRoles.DataBind();
                    cmbRoles.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Có lỗi hệ thống";
                this.SaveErrorLog(ex);
            }
        }

        protected void LoadUsers(int roleID, int curPage, int searchBy, string keyWord)
        {
            UserManager userManager = new UserManager();
            UserCollection users = new UserCollection();
            users = userManager.GetUsersInRole(roleID);
            try
            {
                
                if (users.Count <= 0)
                {
                    uctErrorBox.Message = "Không có dữ liệu";
                    uctErrorBox.Visible = true;
                    uctPager.Visible = false;
                    dgridUserList.Visible = false;
                }
                else
                {
                    DataTable dTable = new DataTable();

                    dTable.Columns.Add("UserName");
                    dTable.Columns.Add("FullName");
                    dTable.Columns.Add("Email");
                    dTable.Columns.Add("Blocked", Type.GetType("System.Boolean"));
                    DataRow row;

                    foreach(User userItem in users)
                    {
                        row = dTable.NewRow();
                        row["UserName"] = userItem.UserName;
                        row["FullName"] = userItem.FullName;
                        row["Email"] = userItem.Email;
                        row["Blocked"] =  userItem.Blocked;

                        dTable.Rows.Add(row);

                    }


                    if (keyWord != string.Empty)
                    {
                        switch (searchBy)
                        {
                            case 1:
                                dTable.DefaultView.RowFilter = "UserName like '%" + keyWord + "%'";
                                break;
                            case 2:
                                dTable.DefaultView.RowFilter = "Email like '%" + keyWord + "%'";
                                break;
                            case 3:
                                dTable.DefaultView.RowFilter = "FullName like'%" + keyWord + "%'";
                                break;
                        }
                    }

                    
                    PagedDataSource pageData = new PagedDataSource();

                    pageData.DataSource = dTable.DefaultView;

                    pageData.AllowPaging = true;
                    pageData.PageSize = pageSize;
                    pageData.CurrentPageIndex = curPage;

                    
                    uctPager.Visible = true;
                    dgridUserList.Visible = true;
                    dgridUserList.DataSource = pageData;
                    dgridUserList.DataBind();
                    
                    uctPager.SetPageNumber(pageData.PageCount, curPage);

                    uctErrorBox.Message = "";
                    uctErrorBox.Visible = false;
                }
            }
            catch (Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Hệ thống có lỗi";
                this.SaveErrorLog(ex);
            }
        }


        protected void lBtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;

            UserManager userManager = new UserManager();
            try
            {
                userManager.Remove(lnkBtn.CommandArgument);
                this.SaveActionLog("Xóa User", lnkBtn.CommandArgument);

                int page = int.Parse(cmbSearchBy.SelectedValue);

                if (dgridUserList.Rows.Count == 1)
                {
                    page--;
                }
                LoadUsers(int.Parse(cmbRoles.SelectedValue), uctPager.SelectedPageIndex, page, txtKeyWord.Text.Trim());

            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Xóa không thành công, Hệ thống có lỗi";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers(int.Parse(cmbRoles.SelectedValue), uctPager.SelectedPageIndex, int.Parse(cmbSearchBy.SelectedValue), txtKeyWord.Text.Trim());
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Create/?roleID=" + cmbRoles.SelectedValue + "&appID=" + ApplicationList1.SelectedValue );
        }

        protected void dgridUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = ((uctPager.SelectedPageIndex * pageSize) + e.Row.RowIndex + 1).ToString();
            }
        }

        public void SelectChange(object sender, EventArgs e)
        {
            LoadUsers(int.Parse(cmbRoles.SelectedValue),uctPager.SelectedPageIndex, int.Parse(cmbSearchBy.SelectedValue), txtKeyWord.Text.Trim());
        }


        protected void OnSelectChange(object sender, EventArgs e)
        {
            LoadRoles(int.Parse(ApplicationList1.SelectedValue));
            ResetForm();
            LoadUsers(int.Parse(cmbRoles.SelectedValue),0,int.Parse(cmbSearchBy.SelectedValue), "");
        }

        protected void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetForm();
            LoadUsers(int.Parse(cmbRoles.SelectedValue), 0, int.Parse(cmbSearchBy.SelectedValue), "");
        }

        protected void ResetForm()
        {
            cmbSearchBy.SelectedIndex = 0;
            txtKeyWord.Text = "";
        }
    }
}
