using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;

namespace WebAdmin.Function.Account.Edit
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "77B8FCDB-D981-4C0C-8D91-B862FBDB1F13";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Đối tác - Khách hàng] Cập nhật");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadAccountType();
                LoadAccountStatus();
                LoadData();
            }
        }

        private void LoadAccountType()
        {
            try
            {
                AccountManager accountManager = new AccountManager();
                DataTable dt = accountManager.AccountType_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAccountType.DataSource = dt;
                    ddlAccountType.DataValueField = "AccountTypeId";
                    ddlAccountType.DataTextField = "AccountTypeName";
                    ddlAccountType.DataBind();

                    ErrorBox.Message = string.Empty;
                    NotifyBox.Message = string.Empty;
                }
                else
                {
                    ErrorBox.Message = "Không có dữ liệu";
                    NotifyBox.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                NotifyBox.Message = string.Empty;
                SaveErrorLog(ex);
            }
        }

        private void LoadAccountLevel(int accountTypeId)
        {
            try
            {
                AccountManager accountManager = new AccountManager();
                DataTable dt = accountManager.AccountLevel_GetList(accountTypeId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAccountLevel.DataSource = dt;
                    ddlAccountLevel.DataValueField = "AccountLevelId";
                    ddlAccountLevel.DataTextField = "AccountLevelName";
                    ddlAccountLevel.DataBind();

                    ErrorBox.Message = string.Empty;
                    NotifyBox.Message = string.Empty;
                }
                else
                {
                    ErrorBox.Message = "Không có dữ liệu";
                    NotifyBox.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                NotifyBox.Message = string.Empty;
                SaveErrorLog(ex);
            }
        }

        private void LoadAccountStatus()
        {
            try
            {
                StatusManager statusManager = new StatusManager();
                DataTable dt = statusManager.Status_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAccountStatus.DataSource = dt;
                    ddlAccountStatus.DataValueField = "StatusId";
                    ddlAccountStatus.DataTextField = "StatusName";
                    ddlAccountStatus.DataBind();

                    ErrorBox.Message = string.Empty;
                }
                else
                {
                    ErrorBox.Message = "Không có dữ liệu";
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                SaveErrorLog(ex);
            }
        }

        private void LoadData()
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    AccountManager accountManager = new AccountManager();
                    int accountId = int.Parse(Request.QueryString["id"].ToString());
                    DataTable dt = new DataTable();

                    dt = accountManager.Account_GetList(accountId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ddlAccountType.SelectedValue = dr["AccountTypeId"].ToString();
                        LoadAccountLevel(int.Parse(ddlAccountType.SelectedValue));
                        txtAccountName.Text = dr["AccountName"].ToString();
                        txtAccountShortName.Text = dr["AccountShortName"].ToString();
                        ddlAccountLevel.SelectedValue = dr["AccountLevelId"].ToString();
                        txtContactName.Text = dr["ContactName"].ToString();
                        txtAddress.Text = dr["Address"].ToString();
                        txtPhoneNumber1.Text = dr["PhoneNumber1"].ToString();
                        txtPhoneNumber2.Text = dr["PhoneNumber2"].ToString();
                        txtPhoneNumber3.Text = dr["PhoneNumber3"].ToString();
                        txtEmail.Text = dr["Email"].ToString();
                        txtWebsite.Text = dr["Website"].ToString();
                        txtNote.Text = dr["Note"].ToString();
                        ddlAccountStatus.SelectedValue = dr["AccountStatus"].ToString();
                    }
                    else
                    {
                        ErrorBox.Message = "Không có dữ liệu";
                        NotifyBox.Message = string.Empty;
                    }
                }
                else
                {
                    ErrorBox.Message = "Không lấy được dữ liệu";
                    NotifyBox.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                NotifyBox.Message = string.Empty;
                SaveErrorLog(ex);
            }
        }

        private void SaveData()
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"].ToString());
                    int code = -1;
                    string msg = "";

                    AccountManager accountManager = new AccountManager();
                    AccountBase account = new AccountBase();

                    account.AccountId = id;
                    account.AccountTypeId = int.Parse(ddlAccountType.SelectedValue);
                    account.AccountName = txtAccountName.Text.Trim();
                    account.AccountShortName = txtAccountShortName.Text.Trim();
                    account.AccountLevelId = int.Parse(ddlAccountLevel.SelectedValue);
                    account.ContactName = txtContactName.Text.Trim();
                    account.Address = txtAddress.Text.Trim();
                    account.PhoneNumber1 = txtPhoneNumber1.Text.Trim();
                    account.PhoneNumber2 = txtPhoneNumber2.Text.Trim();
                    account.PhoneNumber3 = txtPhoneNumber3.Text.Trim();
                    account.Email = txtEmail.Text.Trim();
                    account.Website = txtWebsite.Text.Trim();
                    account.Note = txtNote.Text.Trim();
                    account.AccountStatus = int.Parse(ddlAccountStatus.SelectedValue);
                    account.UpdateDate = DateTime.Now;
                    account.UpdateUser = User.Identity.Name;

                    accountManager.Account_Update(account, ref code, ref msg);

                    if (code == 0)
                    {
                        ErrorBox.Message = string.Empty;
                        NotifyBox.Message = "Lưu dữ liệu thành công";
                    }
                    else
                    {
                        ErrorBox.Message = "Lưu dữ liệu thất bại";
                        NotifyBox.Message = string.Empty;
                    }

                    SaveActionLog("Cập nhật", string.Format("code: {0}; msg: {1}; id: {2}", code, msg, id));
                }
                else
                {
                    ErrorBox.Message = "Không lấy được dữ liệu";
                    NotifyBox.Message = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                NotifyBox.Message = string.Empty;
                SaveErrorLog(ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedUpdate)
            {
                SaveData();
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
                NotifyBox.Message = string.Empty;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../");
        }

        protected void NotifyBox_NextClicked(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                Response.Redirect(string.Format("../Edit/?id={0}", Request.QueryString["id"]));
            }
            else
            {
                Response.Redirect("../");
            }
        }

        protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccountLevel(int.Parse(ddlAccountType.SelectedValue));
        }
    }
}