using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAdmin.Base;
using Inside.SecurityProviders;
namespace WebAdmin.Admin.Users.Manage
{
    public partial class Default : Base.BasePage
    {
        const int pageSize = 100;

        protected void Page_Init(object sender, EventArgs e)
        {
            //this.SaveActionLog("test", "Page_Init");
            this.Token = "6970B412-C6B9-4150-9D0E-1C935F87F835";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.SaveActionLog("test", "Page_Load");
            //this.btnCreate.Enabled = this.Permission.IsAllowedAddnew;
            
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Quản lý người dùng");

                LoadUsers(0, 0, "");

                ApplicationRolesList1.Visible = false;
                btnUpdateRole.Visible = false;
            }

            uctErrorBox.Visible = false;
            //ApplicationRolesList1.Reset();

        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    this.SaveActionLog("test", "Page_PreRender");
        //    this.btnCreate.Enabled = this.Permission.IsAllowedAddnew;
        //}

        protected void LoadUsers(int curPage,int searchBy , string keyWord )
        {
            UserManager userManager = new UserManager();
            UserCollection users = new UserCollection();

            try
            {
                if (keyWord == string.Empty)
                {
                    users = userManager.GetAllUsers();
                }
                else
                {
                    switch (searchBy)
                    {
                        case 0:
                            users = userManager.GetAllUsers();
                            break;
                        case 1:
                            users = userManager.FindUsersByUserName(keyWord);
                            break;
                        case 2:
                            users = userManager.FindUsersByEmail(keyWord);
                            break;
                        case 3:
                            users = userManager.FindUsersByFullName(keyWord);
                            break;
                    }
                }
                if (users.Count == 0)
                {
                    uctErrorBox.Visible = true;
                    uctErrorBox.Message = "Không có dữ liệu";
                    uctPager.Visible = false;
                    dgridUserList.Visible = false;
                }
                else
                {
                    PagedDataSource pageData = new PagedDataSource();

                    pageData.DataSource = users;
                    pageData.AllowPaging = true;
                    pageData.PageSize = pageSize;
                    pageData.CurrentPageIndex = curPage;

                    uctPager.SetPageNumber(pageData.PageCount, curPage);

                    uctPager.Visible = true;
                    dgridUserList.Visible = true;
                    dgridUserList.DataSource = pageData;
                    dgridUserList.DataBind();
                    
                }
            }
            catch( Exception ex )
            {
                uctErrorBox.Visible = true;
                //uctErrorBox.Message = "Có lỗi";
                uctErrorBox.Message = ex.ToString() ;
                this.SaveErrorLog(ex);
            }
        }

        public void SelectChange(object sender, EventArgs e)
        {
            LoadUsers(uctPager.SelectedPageIndex, int.Parse(cmbSearchBy.SelectedValue) , txtKeyWord.Text.Trim());
            dgridUserList.SelectedIndex = -1;
        }

        protected void dgridUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (uctPager.SelectedPageIndex * pageSize + e.Row.RowIndex + 1).ToString();
            }
        }

        protected void dgridUserList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string userName = dgridUserList.SelectedValue.ToString();

            RoleManager roleManager = new RoleManager();
            RoleCollection roles = new RoleCollection();

            roles = roleManager.GetRolesOfUser(userName);

            List<int> roleIDs = new List<int>();

            foreach(Role roleItem in roles )
            {
                roleIDs.Add(roleItem.RoleID);
            }

            ApplicationRolesList1.Reset();
            ApplicationRolesList1.Visible = true;
            //if (roleIDs.Count > 0)
            //{
            ApplicationRolesList1.SelectedRoleID = roleIDs.ToArray();
            //}

            btnUpdateRole.Visible = true;
        }

        protected void lBtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtn = (LinkButton)sender;

            UserManager userManager = new UserManager();
            try
            {
                userManager.Remove(lnkBtn.CommandArgument);
                this.SaveActionLog("Xóa tài khoản", lnkBtn.CommandArgument);
                
                int page = int.Parse(cmbSearchBy.SelectedValue);
                
                if( dgridUserList.Rows.Count == 1 )
                {
                    page--;
                }

                LoadUsers(0, page , txtKeyWord.Text.Trim());
            }
            catch(Exception ex)
            {
                this.SaveErrorLog(ex);
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Xóa không thành công, Hệ thống có lỗi";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers(0, int.Parse(cmbSearchBy.SelectedValue), txtKeyWord.Text.Trim());
            dgridUserList.SelectedIndex = -1;
        }

        protected void CheckBox1_CheckChanged(object sender, EventArgs e)
        {
            CheckBox CheckBox1 = (CheckBox)(sender);
            string userName = CheckBox1.Text;
            bool isBlocked = CheckBox1.Checked;

            UserManager um = new UserManager();
            try
            {                
                if (isBlocked)
                {
                    um.BlockUser(userName);
                    SaveActionLog("Khóa tài khoản", userName);
                }
                else
                {
                    um.UnblockUser(userName);
                    SaveActionLog("Mở khóa tài khoản", userName);
                }
            }
            catch (Exception ex)
            {
                this.SaveErrorLog(ex);
                uctErrorBox.Visible = true;
                uctErrorBox.Message = ex.Message; //"Cập nhật trạng thái không thành công.";
            }            
        }

        protected void UpdateUserRoles(string userName)
        {
            try
            {
                int[] selectedRoleIDs = ApplicationRolesList1.SelectedRoleID;//Danh sach Role da chon cho User

                RoleManager roleManager = new RoleManager();
                RoleCollection roles = new RoleCollection();

                roles = roleManager.GetRolesOfUser(userName);
                
                if (selectedRoleIDs.Length > 0)
                {
                    List<int> selectedRoleList = selectedRoleIDs.ToList();
                    string roleIDList = "";
                    foreach (Role roleItem in roles)
                    {
                        if (selectedRoleList.IndexOf(roleItem.RoleID) < 0)
                        {
                            roleManager.RemoveUserFromRole(userName, roleItem.RoleID);
                            roleIDList = roleIDList + "," + roleItem.RoleName;
                            
                        }
                        else
                        {
                            selectedRoleList.Remove(roleItem.RoleID);
                        }
                    }
                    SaveActionLog("Xóa quyền User: " + userName, "Role: " + roleIDList);

                    if (selectedRoleList.Count > 0)
                    {
                        roleManager.AddUserToRoles(userName, selectedRoleList.ToArray());

                        roleIDList = "";
                        int count = selectedRoleList.Count;
                        for (int id = 0; id < count; id++)
                        {
                            roleIDList = roleIDList + "," + roleManager.GetRole(int.Parse(selectedRoleList[id].ToString())).RoleName;
                        }
                        SaveActionLog("Cấp quyền User: " + userName, "Role: " + roleIDList);

                    }                    
                }
                else // User khong thuoc role nao
                {
                    string roleIDList = "";

                    foreach (Role roleItem in roles)
                    {
                        roleManager.RemoveUserFromRole(userName, roleItem.RoleID);
                        roleIDList = roleIDList + "," + roleItem.RoleName;
                    }
                    if (roleIDList!="")
                        SaveActionLog("Xóa quyền user: " + userName, "RoleID: " + roleIDList);

                }
                uctErrorBox.Message = "";
                uctErrorBox.Visible = false;
            }
            catch (Exception ex)
            {
                uctErrorBox.Message = "Hệ thống có lỗi";
                uctErrorBox.Visible = true;
                this.SaveErrorLog(ex);
            }            
        }

        protected void btnUpdateRole_Click(object sender, EventArgs e)
        {
            string userName = dgridUserList.SelectedValue.ToString();
            UpdateUserRoles(userName);
            Response.Redirect("Default.aspx");
        }
    }
}
