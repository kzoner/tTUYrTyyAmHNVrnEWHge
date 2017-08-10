using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Inside.SecurityProviders;
using System.Data;
using System.Collections;
//using Inside.SecurityProviders.Bussiness;

namespace WebAdmin.ChangePwd
{
    public partial class Default : WebAdmin.Base.BasePage //System.Web.UI.Page
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
                        pnlChangePass.Visible = true;
                        pnlNotify.Visible = false;
                        pnlErrorBox.Visible = false;
                        break;

                    case FormState.NotifyState:
                        pnlChangePass.Visible = false;
                        pnlNotify.Visible = true;
                        pnlErrorBox.Visible = false;
                        break;

                    case FormState.ErrorState:
                        pnlChangePass.Visible = true;
                        pnlNotify.Visible = false;
                        pnlErrorBox.Visible = true;
                        break;
                }
            }
        }
        
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "D154AF769FD446EABDB724FAB0D02DBF";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("Đổi mật khẩu");

            RequestAuthen = false;

            if (!IsPostBack)
            {
                tbxUsername.Text = User.Identity.Name;
                CurrentFormState = FormState.Default;
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            string strUsername = tbxUsername.Text;
            string strCurrentPassword = tbxPassword.Text;
            string strNewPassword = tbxCfPassword.Text;

            try
            {
                UserManager objUserMan = new UserManager();

                objUserMan.ChangePassword(strUsername, strCurrentPassword, strNewPassword);

                usrNotifyBox.Message = "Đổi mật khẩu thành công!";

                CurrentFormState = FormState.NotifyState;

                SaveActionLog("Đổi mật khẩu", tbxUsername.Text);
                
            }
            catch (Exception ex)
            {
                // show error message
                string strErrMsg = ex.Message;

                usrErrorBox.Message = strErrMsg;

                CurrentFormState = FormState.ErrorState;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }
    }
}
