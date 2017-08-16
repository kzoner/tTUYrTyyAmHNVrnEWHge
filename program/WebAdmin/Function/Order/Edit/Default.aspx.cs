using Inside.InsideData.Business;
using Inside.InsideData.Base;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Order.Edit
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "C461C88F-79D8-425A-824E-3ED16E711D27";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Xuất - Nhập] Cập nhật");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;

                DataTable dt = new DataTable("dtOrderDetail");
                dt.Columns.Add("ProductID", Type.GetType("System.Int32"));
                dt.Columns.Add("Price", Type.GetType("System.Decimal"));
                dt.Columns.Add("Quantity", Type.GetType("System.Int32"));
                dt.Columns.Add("Amount", Type.GetType("System.Decimal"));
                dt.Columns.Add("UnitTypeId", Type.GetType("System.Int32"));
                dt.Columns.Add("UnitValue", Type.GetType("System.Single"));
                dt.Columns.Add("UnitId", Type.GetType("System.Int32"));
                ViewState["dtOrderDetail"] = dt;

                LoadAccountType();
                LoadData();
                LoadProductType();
                LoadProduct(int.Parse(ddlProductType.SelectedValue));
                LoadUnitType(int.Parse(ddlProductType.SelectedValue));
                LoadUnit(int.Parse(ddlUnitType.SelectedValue));
                LoadProductInfo(int.Parse(ddlProduct.SelectedValue));
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

        private void LoadAccount(int accountTypeId)
        {
            try
            {
                AccountManager accountManager = new AccountManager();
                DataTable dt = accountManager.Account_GetList_AccountType(accountTypeId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlAccount.DataSource = dt;
                    ddlAccount.DataValueField = "AccountId";
                    ddlAccount.DataTextField = "AccountName";
                    ddlAccount.DataBind();

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

        private void LoadProduct(int productTypeId)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                DataTable dt = productManager.Product_GetList_ProductType(productTypeId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlProduct.DataSource = dt;
                    ddlProduct.DataValueField = "ProductId";
                    ddlProduct.DataTextField = "ProductCode";
                    ddlProduct.DataBind();

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

        private void LoadUnitType(int productTypeId)
        {
            try
            {
                UnitManager unitManager = new UnitManager();
                DataTable dt = unitManager.UnitType_GetList(productTypeId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlUnitType.DataSource = dt;
                    ddlUnitType.DataValueField = "UnitTypeId";
                    ddlUnitType.DataTextField = "UnitTypeName";
                    ddlUnitType.DataBind();

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

        private void LoadUnit(int unitTypeId)
        {
            try
            {
                UnitManager unitManager = new UnitManager();
                DataTable dt = unitManager.Unit_GetList(unitTypeId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlUnit.DataSource = dt;
                    ddlUnit.DataValueField = "UnitId";
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataBind();

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

        private void LoadProductInfo(int productId)
        {
            try
            {
                ProductManager productManager = new ProductManager();
                DataTable dt = productManager.Product_GetList(productId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    ddlUnitType.SelectedValue = dr["UnitTypeId"].ToString();
                    LoadUnit(int.Parse(ddlUnitType.SelectedValue));
                    txtUnitValue.Text = dr["UnitValue"].ToString();
                    ddlUnit.SelectedValue = dr["UnitId"].ToString();
                    hfPrice.Value = dr["Price"].ToString();

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

        private void LoadData()
        {
            OrderManager orderManager = new OrderManager();
            OrderDetailManager orderDetailManager = new OrderDetailManager();

            DataTable dtOrder = new DataTable("dtOrder");
            DataTable dtOrderDetail = new DataTable("dtOrderDetail");
            DataTable dt = new DataTable("dt");

            try
            {
                if (Request.QueryString["id"] != null)
                {
                    int orderId = int.Parse(Request.QueryString["id"].ToString());

                    dtOrder = orderManager.Order_GetList(orderId);
                    if (dtOrder != null && dtOrder.Rows.Count > 0)
                    {
                        DataRow drOrder = dtOrder.Rows[0];

                        ddlAccountType.SelectedValue = drOrder["AccountTypeId"].ToString();
                        LoadAccount(int.Parse(ddlAccountType.SelectedValue));
                        txtOrderCode.Text = drOrder["OrderCode"].ToString();
                        ddlAccount.SelectedValue = drOrder["AccountId"].ToString();
                        txtTransportFee.Text = decimal.Parse(drOrder["TransportFee"].ToString()).ToString("#,##0");
                        txtNote.Text = drOrder["Note"].ToString();

                        dtOrderDetail = orderDetailManager.OrderDetail_GetList_OrderId(orderId);
                        if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                        {
                            dt = (DataTable)ViewState["dtOrderDetail"];
                            DataRow row;
                            foreach (DataRow item in dtOrderDetail.Rows)
                            {
                                row = dt.NewRow();
                                row["ProductID"] = item["ProductID"];
                                row["Price"] = item["Price"];
                                row["Quantity"] = item["Quantity"];
                                row["Amount"] = item["Amount"];
                                row["UnitTypeId"] = item["UnitTypeId"];
                                row["UnitValue"] = item["UnitValue"];
                                row["UnitId"] = item["UnitId"];
                                dt.Rows.Add(row);
                            }

                            gvData.DataSource = dtOrderDetail;
                            gvData.DataBind();

                            gvData.Visible = true;
                            ErrorBox.Message = string.Empty;
                            NotifyBox.Message = string.Empty;
                        }
                        else
                        {
                            gvData.Visible = false;
                            ErrorBox.Message = "Không có dữ liệu";
                            NotifyBox.Message = string.Empty;
                        }
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
            OrderBase orderBase = new OrderBase();
            OrderManager orderManager = new OrderManager();

            OrderDetailBase orderDetailBase = new OrderDetailBase();
            OrderDetailManager orderDetailManager = new OrderDetailManager();

            DataTable dtOrder = new DataTable("dtOrder");
            DataTable dtOrderDetail = new DataTable("dtOrderDetail");

            int code = -1, orderId = 0; string msg = "";

            try
            {
                if (ViewState["dtOrderDetail"] != null || Request.QueryString["id"] != null)
                {
                    orderId = int.Parse(Request.QueryString["id"].ToString());
                    orderBase.AccountTypeId = int.Parse(ddlAccountType.SelectedValue);
                    orderBase.AccountId = int.Parse(ddlAccount.SelectedValue);
                    orderBase.OrderCode = txtOrderCode.Text.Trim();
                    orderBase.TransportFee = decimal.Parse(txtTransportFee.Text.Trim().Replace(",", ""));
                    orderBase.Note = txtNote.Text.Trim();
                    orderBase.UpdateDate = DateTime.Now;
                    orderBase.UpdateUser = User.Identity.Name;

                    orderManager.Order_Update(orderBase, ref code, ref msg);
                    SaveActionLog("Cập nhật Order", string.Format("code: {0}; msg: {1}; orderId: {2}", code, msg, orderId));

                    if (code == 0)
                    {
                        dtOrderDetail = (DataTable)ViewState["dtOrderDetail"];
                        orderDetailManager.OrderDetail_Delete_OrderId(orderId, ref code, ref msg);

                        foreach (DataRow item in dtOrderDetail.Rows)
                        {
                            orderDetailBase.OrderId = orderId;
                            orderDetailBase.ProductId = int.Parse(item["ProductID"].ToString());
                            orderDetailBase.UnitTypeId = int.Parse(item["UnitTypeId"].ToString());
                            orderDetailBase.UnitId = int.Parse(item["UnitId"].ToString());
                            orderDetailBase.UnitValue = float.Parse(item["UnitValue"].ToString().Replace(",", ""));
                            orderDetailBase.Quantity = int.Parse(item["Quantity"].ToString().Replace(",", ""));
                            orderDetailBase.Price = decimal.Parse(item["Price"].ToString().Replace(",", ""));
                            orderDetailBase.Amount = decimal.Parse(item["Amount"].ToString().Replace(",", ""));

                            orderDetailManager.OrderDetail_Insert(orderDetailBase, ref code, ref msg);

                            if (code != 0)
                            {
                                orderDetailManager.OrderDetail_Delete_OrderId(orderId, ref code, ref msg);
                                orderManager.Order_Delete(orderId, ref code, ref msg);
                                SaveActionLog("Xóa Order", string.Format("code: {0}; msg: {1}; orderId: {2}", code, msg, orderId));

                                ErrorBox.Message = "Lưu dữ liệu thất bại";
                                NotifyBox.Message = string.Empty;

                                break;
                            }
                        }

                        if (ErrorBox.Message != "Lưu dữ liệu thất bại")
                        {
                            ErrorBox.Message = string.Empty;
                            NotifyBox.Message = "Lưu dữ liệu thành công";
                        }
                    }
                    else
                    {
                        ErrorBox.Message = "Lưu dữ liệu thất bại";
                        NotifyBox.Message = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                if (orderId != 0)
                {
                    orderDetailManager.OrderDetail_Delete_OrderId(orderId, ref code, ref msg);
                    orderManager.Order_Delete(orderId, ref code, ref msg);
                }

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
            LoadAccount(int.Parse(ddlAccountType.SelectedValue));
        }

        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProduct(int.Parse(ddlProductType.SelectedValue));
            LoadUnitType(int.Parse(ddlProductType.SelectedValue));
            LoadProductInfo(int.Parse(ddlProduct.SelectedValue));
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProductInfo(int.Parse(ddlProduct.SelectedValue));
        }

        #region ---------------------------Add Gridview---------------------------
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ProductManager productManager = new ProductManager();
            UnitManager unitManager = new UnitManager();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = productManager.Product_GetName(int.Parse(e.Row.Cells[0].Text));
                e.Row.Cells[4].Text = unitManager.UnitType_GetName(int.Parse(e.Row.Cells[4].Text));
                e.Row.Cells[6].Text = unitManager.Unit_GetName(int.Parse(e.Row.Cells[6].Text));
            }
        }

        private void Add(int productID, decimal price, int quantity, decimal amount, int unitTypeId, float unitValue, int unitId)
        {
            DataTable dt;
            if (ViewState["dtOrderDetail"] != null)
            {
                dt = (DataTable)ViewState["dtOrderDetail"];
            }
            else
            {
                dt = new DataTable("dtOrderDetail");
                dt.Columns.Add("ProductID", Type.GetType("System.Int32"));
                dt.Columns.Add("Price", Type.GetType("System.Decimal"));
                dt.Columns.Add("Quantity", Type.GetType("System.Int32"));
                dt.Columns.Add("Amount", Type.GetType("System.Decimal"));
                dt.Columns.Add("UnitTypeId", Type.GetType("System.Int32"));
                dt.Columns.Add("UnitValue", Type.GetType("System.Single"));
                dt.Columns.Add("UnitId", Type.GetType("System.Int32"));
                ViewState["dtOrderDetail"] = dt;
            }

            DataRow dr = dt.NewRow();
            dr[0] = productID;
            dr[1] = price;
            dr[2] = quantity;
            dr[3] = amount;
            dr[4] = unitTypeId;
            dr[5] = unitValue;
            dr[6] = unitId;
            dt.Rows.Add(dr);

            gvData.DataSource = dt;
            gvData.DataBind();

            ErrorBox.Message = string.Empty;
            NotifyBox.Message = string.Empty;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.CommandName == "Add")
                {
                    int productID = int.Parse(ddlProduct.SelectedValue);
                    decimal price = decimal.Parse(hfPrice.Value);
                    int quantity = int.Parse(txtQuantity.Text.Trim().Replace(",", ""));
                    decimal amount = quantity * price;
                    int unitTypeId = int.Parse(ddlUnitType.SelectedValue);
                    float unitValue = float.Parse(txtUnitValue.Text.Trim().Replace(",", ""));
                    int unitId = int.Parse(ddlUnit.SelectedValue);

                    Add(productID, price, quantity, amount, unitTypeId, unitValue, unitId);
                }
                else
                {
                    int index = int.Parse(hfIndex.Value);
                    int productID = int.Parse(ddlProduct.SelectedValue);
                    decimal price = decimal.Parse(hfPrice.Value);
                    int quantity = int.Parse(txtQuantity.Text.Trim().Replace(",", ""));
                    decimal amount = quantity * price;
                    int unitTypeId = int.Parse(ddlUnitType.SelectedValue);
                    float unitValue = float.Parse(txtUnitValue.Text.Trim().Replace(",", ""));
                    int unitId = int.Parse(ddlUnit.SelectedValue);

                    Edit(index, productID, price, quantity, amount, unitTypeId, unitValue, unitId);
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                NotifyBox.Message = string.Empty;
                SaveErrorLog(ex);
            }
        }

        private void Edit(int index, int productID, decimal price, int quantity, decimal amount, int unitTypeId, float unitValue, int unitId)
        {
            DataTable dt;
            if (ViewState["dtOrderDetail"] != null)
            {
                dt = (DataTable)ViewState["dtOrderDetail"];

                dt.Rows[index]["ProductID"] = productID;
                dt.Rows[index]["Price"] = price;
                dt.Rows[index]["Quantity"] = quantity;
                dt.Rows[index]["Amount"] = amount;
                dt.Rows[index]["UnitTypeId"] = unitTypeId;
                dt.Rows[index]["UnitValue"] = unitValue;
                dt.Rows[index]["UnitId"] = unitId;

                gvData.DataSource = dt;
                gvData.DataBind();

                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
            }

            btnAdd.Text = "Thêm";
            btnAdd.CommandName = "Add";
        }

        protected void lbtnEdit_Clicked(object sender, EventArgs e)
        {
            ProductManager productManager = new ProductManager();

            LinkButton lbtnEdit = (LinkButton)sender;
            int index = int.Parse(lbtnEdit.CommandArgument);

            DataTable dt = new DataTable();
            if (ViewState["dtOrderDetail"] != null)
            {
                dt = (DataTable)ViewState["dtOrderDetail"];
                if (dt.Rows.Count > 0)
                {
                    int productId = int.Parse(dt.Rows[index][0].ToString());
                    DataTable dtProduct = productManager.Product_GetList(productId);
                    DataRow drProduct = dtProduct.Rows[0];

                    ddlProductType.SelectedValue = drProduct["ProductTypeId"].ToString();
                    LoadProduct(int.Parse(ddlProductType.SelectedValue));
                    LoadUnitType(int.Parse(ddlProductType.SelectedValue));
                    ddlProduct.SelectedValue = drProduct["ProductId"].ToString();
                    txtQuantity.Text = decimal.Parse(dt.Rows[index][2].ToString()).ToString("#,##0");
                    ddlUnitType.SelectedValue = drProduct["UnitTypeId"].ToString();
                    LoadUnit(int.Parse(ddlUnitType.SelectedValue));
                    txtUnitValue.Text = decimal.Parse(dt.Rows[index][5].ToString()).ToString("#,##0");
                    ddlUnit.SelectedValue = drProduct["UnitId"].ToString();
                    hfIndex.Value = index.ToString();
                    hfPrice.Value = drProduct["Price"].ToString();

                    btnAdd.Text = "Cập nhật";
                    btnAdd.CommandName = "Update";
                }
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lbtnDelete = (LinkButton)sender;
            int index = int.Parse(lbtnDelete.CommandArgument);

            DataTable dt;
            if (ViewState["dtOrderDetail"] != null)
            {
                dt = (DataTable)ViewState["dtOrderDetail"];
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(index);

                    gvData.DataSource = dt;
                    gvData.DataBind();
                }
            }
        }

        protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string oldClass = e.Row.RowIndex % 2 == 1 ? "HlightRow" : "SubCat";
                e.Row.Attributes.Add("onmouseover", "this.className='HlightRow1'");
                e.Row.Attributes.Add("onmouseout", "this.className='" + oldClass + "'");
            }
        }
        #endregion
    }
}