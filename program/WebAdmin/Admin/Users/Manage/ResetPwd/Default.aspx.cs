using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inside.SecurityProviders;

namespace WebAdmin.Admin.Users.Manage.ResetPwd
{
    public partial class Default : Base.BasePage
    {
        #region Properties

        public enum FormState
        {
            Default,
            NotifyState,
            ErrorState
        }

        private FormState m_FormState;

        public FormState CurrentFormState
        {
            get
            {
                return m_FormState;
            }
            set
            {
                m_FormState = value;

                switch (m_FormState)
                {
                    case FormState.Default:
                        pnlResetPass.Visible = true;
                        pnlNotify.Visible = false;
                        pnlErrorBox.Visible = false;
                        break;

                    case FormState.NotifyState:
                        pnlResetPass.Visible = false;
                        pnlNotify.Visible = true;
                        pnlErrorBox.Visible = false;
                        break;

                    case FormState.ErrorState:
                        pnlResetPass.Visible = true;
                        pnlNotify.Visible = false;
                        pnlErrorBox.Visible = true;
                        break;
                }
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "453B84E9-1BCE-44BD-A7E6-7F7EE579C118";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Đặt lại mật khẩu");
                CurrentFormState = FormState.Default;
            }
            //this.btnChangePass.Enabled = this.Permission.IsAllowedUpdate;
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                string strUsername = tbxUsername.Text.Trim();
                string strPassword = tbxPassword.Text;
                string strCfPassword = tbxCfPassword.Text;

                if (strPassword.CompareTo(strCfPassword) != 0)
                {
                    usrErrorBox.Message = "Mật khẩu và mật khẩu xác nhận không trùng nhau";
                    CurrentFormState = FormState.ErrorState;
                }
                else
                {
                    UserManager um = new UserManager();
                    um.SetPassword(strUsername, strPassword);
                    this.SaveActionLog("Change password", "Change password for user user:" + strUsername);
                    usrNotifyBox.Message = "Đặt mật khẩu thành công!";
                    CurrentFormState = FormState.NotifyState;
                }
            }
            catch (Exception ex)
            {
                usrErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // clear text
            tbxUsername.Text = string.Empty;
            tbxPassword.Text = string.Empty;
            tbxCfPassword.Text = string.Empty;

            // set form statr to default
            CurrentFormState = FormState.Default;
        }
    }
}
