using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Order
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "566A63D5-6429-47AE-A94C-4D54DF3B74AC";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Xuất - Nhập] Tra cứu");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                LoadAccountType();
                txtFromDate.Text = DateTime.Now.Day == 1 ? new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, 1).ToString("dd/MM/yyyy") : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
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
                    ddlAccountType.DataTextField = "OrderTypeName";
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

        private void LoadData(int pageNumber, bool isExcel)
        {
            try
            {
                OrderManager orderManager = new OrderManager();
                DateTime fromDate, toDate;
                int accountTypeId, rowsPerPage, rowTotal;
                string accountName;

                fromDate = DateParse(txtFromDate.Text.Trim());
                toDate = DateParse(txtToDate.Text.Trim());
                accountTypeId = int.Parse(ddlAccountType.SelectedValue);
                accountName = txtAccountName.Text.Trim();
                rowsPerPage = 30;
                rowTotal = orderManager.Order_RowTotal(fromDate, toDate, accountTypeId, accountName);

                if (rowTotal > 0)
                {
                    rowsPerPage = isExcel ? rowTotal : rowsPerPage;
                    uctPager.SetPageNumber(rowTotal, rowsPerPage, pageNumber);
                    DataTable dt = orderManager.Order_Search(fromDate, toDate, accountTypeId, accountName, rowsPerPage, pageNumber);

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
            Format format = new Format();
            AccountManager accountManager = new AccountManager();

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = format.RenameOrderTitle(e.Row.Cells[i].Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = accountManager.AccountType_GetName(int.Parse(e.Row.Cells[2].Text));
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = decimal.Parse(e.Row.Cells[5].Text).ToString("#,##0");
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Text = decimal.Parse(e.Row.Cells[6].Text).ToString("#,##0");
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = decimal.Parse(e.Row.Cells[7].Text).ToString("#,##0");
            }
        }
    }
}