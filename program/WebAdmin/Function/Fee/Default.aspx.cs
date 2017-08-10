using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Fee
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "F813CD4B-8A45-4A99-8802-986E8F2DE642";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Chi phí] Tra cứu");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                LoadFeeType();
                txtFromDate.Text = DateTime.Now.Day == 1 ? new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, 1).ToString("dd/MM/yyyy") : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                LoadData(1, false);
            }
        }

        private void LoadFeeType()
        {
            try
            {
                FeeManager feeManager = new FeeManager();
                DataTable dt = feeManager.FeeType_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlFeeType.DataSource = dt;
                    ddlFeeType.DataValueField = "FeeTypeId";
                    ddlFeeType.DataTextField = "FeeTypeName";
                    ddlFeeType.DataBind();

                    ddlFeeType.Items.Insert(0, new ListItem("Tất cả", "0"));

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
                FeeManager feeManager = new FeeManager();
                DateTime fromDate, toDate;
                int feeTypeId, rowsPerPage, rowTotal;

                fromDate = DateParse(txtFromDate.Text.Trim());
                toDate = DateParse(txtToDate.Text.Trim());
                feeTypeId = int.Parse(ddlFeeType.SelectedValue);
                rowsPerPage = 30;
                rowTotal = feeManager.Fee_RowTotal(fromDate, toDate, feeTypeId);

                if (rowTotal > 0)
                {
                    rowsPerPage = isExcel ? rowTotal : rowsPerPage;
                    uctPager.SetPageNumber(rowTotal, rowsPerPage, pageNumber);
                    DataTable dt = feeManager.Fee_Search(fromDate, toDate, feeTypeId, rowsPerPage, pageNumber);

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
                Excel(ConvertGVtoDT(gvData), "Danh sách chi phí");
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
            FeeManager feeManager = new FeeManager();

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = format.RenameFeeTitle(e.Row.Cells[i].Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd/MM/yyyy");
                e.Row.Cells[2].Text = feeManager.FeeType_GetName(int.Parse(e.Row.Cells[2].Text));
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Text = decimal.Parse(e.Row.Cells[3].Text).ToString("#,##0");
            }
        }

        protected void btnAddFeeType_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedAddnew)
            {
                Response.Redirect("FeeType/");
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
            }
        }
    }
}