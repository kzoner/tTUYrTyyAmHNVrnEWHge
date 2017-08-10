using Inside.InsideData.Business;
using Inside.InsideData.Base;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Product
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "4212EE6F-E3FB-4C55-B874-F83B5700C8D5";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Sản phẩm] Tra cứu");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                LoadProductType();
                LoadProductStatus();
                LoadData(1, false);
            }
        }

        private void LoadProductType()
        {
            try
            {
                ProductManager productManager = new ProductManager();
                DataTable dt = productManager.ProductType_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlProductType.DataSource = dt;
                    ddlProductType.DataValueField = "ProductTypeId";
                    ddlProductType.DataTextField = "ProductTypeName";
                    ddlProductType.DataBind();

                    ddlProductType.Items.Insert(0, new ListItem("Tất cả", "0"));

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

        private void LoadProductStatus()
        {
            try
            {
                StatusManager statusManager = new StatusManager();
                DataTable dt = statusManager.Status_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlProductStatus.DataSource = dt;
                    ddlProductStatus.DataValueField = "StatusId";
                    ddlProductStatus.DataTextField = "StatusName";
                    ddlProductStatus.DataBind();

                    ddlProductStatus.Items.Insert(0, new ListItem("Tất cả", "0"));

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
                ProductManager productManager = new ProductManager();
                int productTypeId, productStatus, rowsPerPage, rowTotal;
                string productCode, productName;

                productTypeId = int.Parse(ddlProductType.SelectedValue);
                productCode = txtProductCode.Text.Trim();
                productName = txtProductName.Text.Trim();
                productStatus = int.Parse(ddlProductStatus.SelectedValue);
                rowsPerPage = 30;
                rowTotal = productManager.Product_RowTotal(productTypeId, productCode, productName, productStatus);


                if (rowTotal > 0)
                {
                    rowsPerPage = isExcel ? rowTotal : rowsPerPage;
                    uctPager.SetPageNumber(rowTotal, rowsPerPage, pageNumber);
                    DataTable dt = productManager.Product_Search(productTypeId, productCode, productName, productStatus, rowsPerPage, pageNumber);

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
                Excel(ConvertGVtoDT(gvData), "Danh sách sản phẩm");
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
            ProductManager productManager = new ProductManager();
            UnitManager unitManager = new UnitManager();
            StatusManager statusManager = new StatusManager();
            Format format = new Format();

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = format.RenameProductTitle(e.Row.Cells[i].Text);
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = productManager.ProductType_GetName(int.Parse(e.Row.Cells[1].Text));
                e.Row.Cells[4].Text = decimal.Parse(e.Row.Cells[4].Text).ToString("#,##0");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Text = unitManager.UnitType_GetName(int.Parse(e.Row.Cells[5].Text));
                e.Row.Cells[6].Text = decimal.Parse(e.Row.Cells[6].Text).ToString("#,##0");
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Text = unitManager.Unit_GetName(int.Parse(e.Row.Cells[7].Text));
                e.Row.Cells[8].Text = e.Row.Cells[8].Text == "&nbsp;" ? "" : string.Format("<img alt=\"{0}\" src=\"{1}\" height=\"70px\" />", "img", e.Row.Cells[8].Text);
                e.Row.Cells[10].Text = statusManager.Status_GetName(int.Parse(e.Row.Cells[10].Text));
            }
        }
    }
}