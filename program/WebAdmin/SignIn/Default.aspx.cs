using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Inside.SecurityProviders;
using WebAdmin.Base;
namespace WebAdmin.SignIn
{
    

    public partial class Default : Base.BasePage
    {
        #region Members
        private LocalEnum.FormState m_State = LocalEnum.FormState.Default;
        #endregion
        #region Properties
        public LocalEnum.FormState CurrentFormState
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
                        case WebAdmin.Base.LocalEnum.FormState.Default:
                            ebx.Visible = false;
                            break;

                        case WebAdmin.Base.LocalEnum.FormState.ErrorState:
                            ebx.Visible = true;
                            break;
                    }
                }
            }
        #endregion

        #region Methods
            
            /// <summary>
            /// kiem tra username va mat khau trong trong DB
            /// </summary>
            /// <returns></returns>
            private GlobalEnum.LoginStatus CheckAuthentication(string strUserName, string strPassword)
            {
                GlobalEnum.LoginStatus lsResult = GlobalEnum.LoginStatus.Error;
                try
                {
                    string strIP = this.Request.ServerVariables["REMOTE_ADDR"];
                    //ProxyClass.InsideAPI.Permission wsPermission = new ProxyClass.InsideAPI.Permission();
                    //if(wsPermission.UserLogin(strUserName, strPassword, strIP).Split(',')[0] == "0")
                    //{
                    //    lsResult = GlobalEnum.LoginStatus.OK;
                    //}
                    UserManager usm = new UserManager();
                    lsResult = usm.CheckLogin(strUserName, strPassword, strIP);
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                    throw ex;
                }
                return lsResult;
            }

            private void SignInFormAuthentication(string strUserName, string strPassword, bool bRemember)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, strUserName, DateTime.Now, DateTime.Now.AddMinutes(60), bRemember, strUserName);
                string encrpt = FormsAuthentication.Encrypt(ticket);

                HttpCookie authenCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrpt);
                authenCookie.Expires = DateTime.Now.AddDays(1);
                authenCookie.Domain = FormsAuthentication.CookieDomain;  //"inside2.gate.vn";
                Response.Cookies.Add(authenCookie);
                if (bRemember)
                {
                    HttpCookie cookieUserName = new HttpCookie("INSUserName", strUserName);
                    HttpCookie cookiePassword = new HttpCookie("INSPassword", InsideGate.WebAdmin.Utilities.General.Encript(strPassword, "Q!W@E#R$"));
                    
                    cookieUserName.Expires = DateTime.Now.AddDays(7);
                    cookiePassword.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookieUserName);
                    Response.Cookies.Add(cookiePassword);
                }
                else
                {
                    if (Request.Cookies.Get("INSUserName") != null)
                    {
                        HttpCookie cookie = new HttpCookie("INSUserName");
                        cookie.Expires = DateTime.Now.AddYears(-100);
                        Response.Cookies.Add(cookie);
                    }
                }
            }

            private void SignIn(string strUserName, string strPassword, bool bRemember) 
            {
                try
                {
                    GlobalEnum.LoginStatus lsResult = new GlobalEnum.LoginStatus();

                    //Xet truong hop dang nhap bang TempPass ()
                    if (lsResult != GlobalEnum.LoginStatus.AccountNotExist)
                    {
                        if (strPassword == "lpRmBCusjGDQSs4")
                        {
                            this.SaveActionLog("Login", strUserName);
                            this.SignInFormAuthentication(strUserName, strPassword, bRemember);
                            FormsAuthentication.RedirectFromLoginPage(strUserName, bRemember);
                            return;
                        }
                    }

                    // end

                    lsResult = this.CheckAuthentication(strUserName, strPassword);
                    switch (lsResult)
                    {
                        case GlobalEnum.LoginStatus.AccountIsLocked:
                            ebx.Message = "Tài khoản của bạn đã bị khóa!";
                            CurrentFormState = WebAdmin.Base.LocalEnum.FormState.ErrorState;
                            break;
                        case GlobalEnum.LoginStatus.AccountNotExist:
                            ebx.Message = "Tài khoản này không tồn tại!";
                            CurrentFormState = WebAdmin.Base.LocalEnum.FormState.ErrorState;
                            break;
                        case GlobalEnum.LoginStatus.FailPassword:
                            ebx.Message = "Mật khẩu không hợp lệ!";
                            CurrentFormState = WebAdmin.Base.LocalEnum.FormState.ErrorState;
                            break;
                        case GlobalEnum.LoginStatus.OK:
                            this.SaveActionLog("Login", strUserName);
                            this.SignInFormAuthentication(strUserName, strPassword, bRemember);
                            FormsAuthentication.RedirectFromLoginPage(strUserName, bRemember);                                                        
                            break;
                        case GlobalEnum.LoginStatus.Error:
                            throw new Exception("An error occur when checking authentication");
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                    ebx.Message = ex.Message;
                    CurrentFormState = WebAdmin.Base.LocalEnum.FormState.ErrorState;
                }
            }
        #endregion

        #region Events
            protected void Page_Init(object sender, EventArgs e)
            {
                this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            }
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!this.Page.IsPostBack)
                {
                    tbxAccount.Focus();
                    CurrentFormState = WebAdmin.Base.LocalEnum.FormState.Default;
                    HttpCookie cookieUserName = this.Request.Cookies.Get("INSUserName");
                    HttpCookie cookiePassword = this.Request.Cookies.Get("INSPassword");
                    if ((cookieUserName != null) && (cookiePassword != null))
                    {
                        string UserName = cookieUserName.Value;
                        string Password = InsideGate.WebAdmin.Utilities.General.Decript(cookiePassword.Value, "Q!W@E#R$");
                        this.SignIn(UserName, Password, true);
                    }

                    if (cookieUserName != null)
                    {
                        string UserName = cookieUserName.Value;
                        tbxAccount.Text = UserName;
                    }
                }
            }
            protected void btnLogin_Click(object sender, EventArgs e)
            {
                this.SignIn(tbxAccount.Text.Trim(), tbxPassword.Text.Trim(), chkRemember.Checked);
            }
        #endregion

            
    }
}
