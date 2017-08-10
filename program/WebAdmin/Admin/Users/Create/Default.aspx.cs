using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inside.SecurityProviders;
//using Inside.SecurityProviders.Bussiness;
//using Inside.SecurityProviders.Base;

namespace WebAdmin.Admin.Users.Create
{
    public partial class Default : Base.BasePage
    {
        #region Properties

        public enum FormState
        {
            Default,
            ErrorState,
            ConfirmState
        }

        private FormState m_State = FormState.Default;

        public FormState CurrentFormState
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;

                switch (m_State)
                {
                    case FormState.Default:
                        pnlCreationBox.Visible = true;
                        pnlErrorBox.Visible = false;
                        pnlConfirmBox.Visible = false;
                        break;

                    case FormState.ErrorState:
                        pnlCreationBox.Visible = true;
                        pnlErrorBox.Visible = true;
                        pnlConfirmBox.Visible = false;
                        break;

                    case FormState.ConfirmState:
                        pnlCreationBox.Visible = false;
                        pnlErrorBox.Visible = false;
                        pnlConfirmBox.Visible = true;
                        break;
                }
            }
        }

        #endregion


        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "C2EAF301-7607-4A42-A971-A4F6FDDCFE7F";
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Tạo tài khoản");

                //Load_Questions();
                //Load_Roles();
                CurrentFormState = FormState.Default;

                if (Request.QueryString["roleID"] != null)
                {
                    int[] roleID = new int[] { int.Parse(Request.QueryString["roleID"])};

                    ApplicationRolesList1.SelectedRoleID = roleID;
                }

            }           
        }

        ///// <summary>
        ///// Hàm nạp danh sách câu hỏi
        ///// </summary>
        //private void Load_Questions()
        //{
        //    try
        //    {
        //        QuestionManager qm = new QuestionManager();
        //        QuestionCollection questions = qm.GetAllQuestions();                

        //        ddlQuestion.DataValueField = "QuestionID";
        //        ddlQuestion.DataTextField = "Description";
                
        //        ddlQuestion.DataSource = questions;
        //        ddlQuestion.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        ucErrorBox.Message = ex.Message;

        //        CurrentFormState = FormState.ErrorState;
        //    }
        //}

        
        /// <summary>
        /// Reset control's values
        /// </summary>
        public void Reset()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;

            ApplicationRolesList1.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {            
            try
            {
                UserManager um = new UserManager();
                string sUsername = txtUsername.Text.Trim();
                string sPassword = txtPassword.Text;
                string sCfPassword = txtCfPassword.Text;
                string sFullName = txtFullName.Text.Trim();
                string sEmail = txtEmail.Text.Trim();
                bool bBlocked = chkBlocked.Checked;

                ucErrorBox.Message = string.Empty;

                if (sPassword.Length < 6)
                {
                    ucErrorBox.Message = "Mật khẩu ít nhất phải 6 ký tự.";
                    CurrentFormState = FormState.ErrorState;
                }
                else if (um.FindUsersByEmail(sEmail).Count > 0)
                {
                    ucErrorBox.Message = "Email đã tồn tại trong hệ thống.";
                    CurrentFormState = FormState.ErrorState;
                }
                else
                {
                    // Tao user
                    um.CreateUser(sUsername, sPassword, sFullName, sEmail, bBlocked);


                    // Add user to roles
                    RoleManager roleManager = new RoleManager();
                    roleManager.AddUserToRoles(sUsername, ApplicationRolesList1.SelectedRoleID);


                    ucConfirmBox.ConfirmMessage = "Tạo tài khoản thành công!<br />Bạn có muốn tiếp tục tạo tài khoản không?";
                    CurrentFormState = FormState.ConfirmState;

                    SaveActionLog("Tạo tài khoản", txtUsername.Text.Trim());
                }                
            }
            catch ( Exception ex )
            {
                this.SaveErrorLog(ex);

                ucErrorBox.Message = "Có lỗi hệ thống : " + e.ToString();

                CurrentFormState = FormState.ErrorState;
            }

        }


        public void ucConfirmBox_YesClicked(object sender, EventArgs e)
        {
            // reset values
            Reset();

            CurrentFormState = FormState.Default;           
        }

        public void ucConfirmBox_NoClicked(object sender, EventArgs e)
        {
            if (Request.QueryString["roleID"] == null)
                Response.Redirect("../Manage/");
            else
                Response.Redirect("../UserInRole/?appID=" + Request.QueryString["appID"] + "&roleID=" + Request.QueryString["roleID"]);
        }
    }
}
