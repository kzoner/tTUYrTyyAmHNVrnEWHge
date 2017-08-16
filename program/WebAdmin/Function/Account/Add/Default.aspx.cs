using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;

namespace WebAdmin.Function.Account.Add
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "24D92393-704E-4F24-9C94-0274FCB0DDBA";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Đối tác - Khách hàng] Thêm mới");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadAccountType();
                LoadAccountLevel(int.Parse(ddlAccountType.SelectedValue));
                LoadAccountStatus();
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

        private void SaveData()
        {
            try
            {
                int code = -1;
                string msg = "";

                AccountManager accountManager = new AccountManager();
                AccountBase account = new AccountBase();

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
                account.CreateDate = DateTime.Now;
                account.CreateUser = User.Identity.Name;

                accountManager.Account_Insert(account, ref code, ref msg);

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

                SaveActionLog("Thêm mới", string.Format("code: {0}; msg: {1}", code, msg));
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
            if (Permission.IsAllowedAddnew)
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
            Response.Redirect("../Add/");
        }

        protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccountLevel(int.Parse(ddlAccountType.SelectedValue));
        }
    }
}