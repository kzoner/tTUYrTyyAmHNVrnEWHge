using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Users.Update
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "36E6991C-0347-44CC-A8E0-E7D8E3288530";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Cập nhật người dùng");

                LoadUser();
            }
            ucErrorBox.Visible = false;
        }

        protected void LoadUser()
        {
            if (Request.QueryString["un"].Trim() != string.Empty)
            {
                //Load thong tin User
                UserManager userManager = new UserManager();
                User user = new User();

                user =  userManager.GetUser(Request.QueryString["un"]);

                txtUsername.Text = user.UserName;
                txtFullName.Text = user.FullName;
                txtEmail.Text = user.Email;
                chkBlocked.Checked = user.Blocked;
                Session["blocked"] = user.Blocked;
                
                //Load danh sach Role cua User
                RoleManager roleManger = new RoleManager();
                RoleCollection roles = new RoleCollection();

                roles = roleManger.GetRolesOfUser(user.UserName);

                List<int> roleIDs = new List<int>();

                foreach (Role roleItem in roles)
                {
                    roleIDs.Add(roleItem.RoleID);
                }

                ApplicationRolesList1.Reset();
                //if (roleIDs.Count > 0)
                //{
                ApplicationRolesList1.SelectedRoleID = roleIDs.ToArray();
                //}

            }
            else
                return;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UserManager userManager = new UserManager();
            User user = new User();
            
            user.UserName = txtUsername.Text;
            user.FullName = txtFullName.Text;
            user.Email = txtEmail.Text;
            user.Blocked = chkBlocked.Checked;

            try
            {
                userManager.Update(user);
                UpdateUserRoles(user);
               
                this.SaveActionLog("Cập nhật User: ", txtUsername.Text);
                if (chkBlocked.Checked && !(bool)Session["blocked"])
                    this.SaveActionLog("Khóa tài khoản: ", txtUsername.Text);
                else if (!chkBlocked.Checked && (bool)Session["blocked"])
                    this.SaveActionLog("Mở khóa tài khoản: ", txtUsername.Text);

                if (Request.QueryString["appID"] == null)
                    Response.Redirect("../Manage/");
                else
                    Response.Redirect("../UserInRole/?appID=" + Request.QueryString["appID"] + "&roleID=" + Request.QueryString["roleID"]);
            }
            catch (Exception ex)
            {
                ucErrorBox.Visible = true;
                ucErrorBox.Message = "Cập nhật không thành công ";
                this.SaveErrorLog(ex);
            }
        }

        protected void UpdateUserRoles(User user)
        {
            int[] selectedRoleIDs = ApplicationRolesList1.SelectedRoleID;//Danh sach Role da chon cho User

            RoleManager roleManager = new RoleManager();
            RoleCollection roles = new RoleCollection();

            roles = roleManager.GetRolesOfUser(user.UserName);

            if (selectedRoleIDs.Length > 0)
            {
                List<int> selectedRoleList = selectedRoleIDs.ToList();
                string roleIDList = "";
                foreach (Role roleItem in roles)
                {
                    if (selectedRoleList.IndexOf(roleItem.RoleID) < 0)
                    {
                        roleManager.RemoveUserFromRole(user.UserName, roleItem.RoleID);

                        roleIDList = roleIDList + "," + roleItem.RoleName;
                        
                    }
                    else
                    {
                        selectedRoleList.Remove(roleItem.RoleID);
                    }
                }
                if (roleIDList != "")
                    SaveActionLog("Xóa quyền User: " + user.UserName, "Role: " + roleIDList);
                if (selectedRoleList.Count > 0)
                {
                    
                    roleManager.AddUserToRoles(user.UserName, selectedRoleList.ToArray());
                     roleIDList = "";
                    int count = selectedRoleList.Count;
                    for(int id = 0; id < count; id ++)                        
                    {
                        roleIDList = roleIDList + "," + roleManager.GetRole(selectedRoleList[id]).RoleName;
                    }
                    
                    if (roleIDList != "")
                        SaveActionLog("Cấp quyền User: " + user.UserName, "Role: " + roleIDList);
                }
                

            }
            else // User khong thuoc role nao
            {
                string roleIDList = "";

                foreach (Role roleItem in roles)
                {
                    roleManager.RemoveUserFromRole(user.UserName, roleItem.RoleID);
                    roleIDList = roleIDList + "," + roleItem.RoleName;
                }
                if(roleIDList!="")
                    SaveActionLog("Xóa quyền user: " + user.UserName, "Role: " + roleIDList);
            }
        }
    }
}
