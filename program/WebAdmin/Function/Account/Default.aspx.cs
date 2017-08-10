using Inside.InsideData.Business;
using Inside.InsideData.Base;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Account
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "493925DE-175A-4811-9FC5-766D94868753";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Đối tác - Khách hàng] Tra cứu");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                LoadAccountType();
                LoadAccountLevel(int.Parse(ddlAccountType.SelectedValue));
                LoadAccountStatus();
                LoadData(1, false);
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

                    ddlAccountType.Items.Insert(0, new ListItem("Tất cả", "0"));

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

                    ddlAccountLevel.Items.Insert(0, new ListItem("Tất cả", "0"));

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

                    ddlAccountStatus.Items.Insert(0, new ListItem("Tất cả", "0"));

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

        private void LoadData(int pageNumber, bool isExcel)
        {
            try
            {
                AccountManager accountManager = new AccountManager();
                int accountTypeId, accountLevelId, accountStatus, rowsPerPage, rowTotal;
                string accountName;

                accountTypeId = int.Parse(ddlAccountType.SelectedValue);
                accountLevelId = int.Parse(ddlAccountLevel.SelectedValue);
                accountStatus = int.Parse(ddlAccountStatus.SelectedValue);
                accountName = txtAccountName.Text.Trim();
                rowsPerPage = 30;
                rowTotal = accountManager.Account_RowTotal(accountTypeId, accountName, accountLevelId, accountStatus);

                if (rowTotal > 0)
                {
                    rowsPerPage = isExcel ? rowTotal : rowsPerPage;
                    uctPager.SetPageNumber(rowTotal, rowsPerPage, pageNumber);
                    DataTable dt = accountManager.Account_Search(accountTypeId, accountName, accountLevelId, accountStatus, rowsPerPage, pageNumber);

                    gvData.DataSource = dt;
                    gvData.DataBind();

                    divData.Visible = true;
                    ErrorBox.Message = string.Empty;
                }
                else
                {
                    divData.Visible = false;
                    ErrorBox.Message = "Không có dữ liệu";
                }
            }
            catch (Exception ex)
            {
                divData.Visible = false;
                ErrorBox.Message = "Lỗi chức năng";
                SaveErrorLog(ex);
            }
        }

        protected void uctPager_SelectChange(object sender, EventArgs e)
        {
            LoadData(uctPager.SelectedPageIndex, false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(1, false);
            SaveActionLog("Tra cứu", "");
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedExport)
            {
                LoadData(1, true);
                Excel(ConvertGVtoDT(gvData), "Danh sách Đối tác - Khách hàng");
                LoadData(uctPager.SelectedPageIndex, false);

                SaveActionLog("Xuất Excel", "");
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
            }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedUpdate)
            {
                LinkButton lbtnEdit = (LinkButton)sender;
                Response.Redirect(string.Format("Edit/?id={0}", lbtnEdit.CommandArgument));
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedAddnew)
            {
                Response.Redirect("Add/");
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            AccountManager accountManager = new AccountManager();
            StatusManager statusManager = new StatusManager();
            Format format = new Format();

            for (int i = 15; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = format.RenameAccountTitle(e.Row.Cells[i].Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text = accountManager.AccountType_GetName(int.Parse(e.Row.Cells[2].Text));
                e.Row.Cells[5].Text = accountManager.AccountLevel_GetName(int.Parse(e.Row.Cells[5].Text));
                e.Row.Cells[14].Text = statusManager.Status_GetName(int.Parse(e.Row.Cells[14].Text));
            }
        }

        protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccountLevel(int.Parse(ddlAccountType.SelectedValue));
        }
    }
}