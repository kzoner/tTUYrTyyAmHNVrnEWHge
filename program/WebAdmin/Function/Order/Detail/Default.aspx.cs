using Inside.InsideData.Business;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Order.Detail
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "7CCF3109-8182-4541-9439-813C731B251C";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Xuất - Nhập] Chi tiết");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                LoadData();
            }
        }

        private void LoadData()
        {
            OrderManager orderManager = new OrderManager();
            OrderDetailManager orderDetailManager = new OrderDetailManager();
            AccountManager accountManager = new AccountManager();

            DataTable dtOrder = new DataTable();
            DataTable dtOrderDetail = new DataTable();

            try
            {
                if (Request.QueryString["id"] != null)
                {
                    int orderId = int.Parse(Request.QueryString["id"].ToString());

                    dtOrder = orderManager.Order_GetList(orderId);
                    if (dtOrder != null && dtOrder.Rows.Count > 0)
                    {
                        DataRow drOrder = dtOrder.Rows[0];

                        lblOrderType.Text = accountManager.OrderType_GetName(int.Parse(drOrder["AccountTypeId"].ToString()));
                        lblAccountType.Text = accountManager.AccountType_GetName(int.Parse(drOrder["AccountTypeId"].ToString()));
                        lblAccountName.Text = accountManager.Account_GetName(int.Parse(drOrder["AccountId"].ToString()));
                        lblOrderCode.Text = drOrder["OrderCode"].ToString();
                        lblTransportFee.Text = decimal.Parse(drOrder["TransportFee"].ToString()).ToString("#,##0");
                        lblNote.Text = drOrder["Note"].ToString();

                        dtOrderDetail = orderDetailManager.OrderDetail_GetList_OrderId(orderId);
                        if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                        {
                            //add row SUM
                            DataRow sumRow;
                            sumRow = dtOrderDetail.NewRow();
                            sumRow["OrderId"] = -1;
                            sumRow["OrderDetailId"] = -1;
                            sumRow["ProductId"] = -1;
                            sumRow["Price"] = -1;
                            sumRow["Quantity"] = dtOrderDetail.Compute("SUM(Quantity)", null);
                            sumRow["Amount"] = dtOrderDetail.Compute("SUM(Amount)", null);
                            sumRow["UnitTypeId"] = -1;
                            sumRow["UnitValue"] = -1;
                            sumRow["UnitId"] = -1;

                            dtOrderDetail.Rows.Add(sumRow);

                            gvData.DataSource = dtOrderDetail;
                            gvData.DataBind();

                            gvData.Visible = true;
                            ErrorBox.Message = string.Empty;
                        }
                        else
                        {
                            gvData.Visible = false;
                            ErrorBox.Message = "Không có dữ liệu";
                        }
                    }
                    else
                    {
                        ErrorBox.Message = "Không có dữ liệu";
                    }
                }
                else
                {
                    ErrorBox.Message = "Không lấy được dữ liệu";
                }
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex);
                ErrorBox.Message = "Lỗi chức năng";
            }
        }

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

            if (e.Row.Cells[0].Text.Trim() == "-1")
            {
                e.Row.CssClass = "DarkContent";
                e.Row.Font.Bold = true;
                e.Row.ForeColor = System.Drawing.Color.Green;
                e.Row.Cells[0].Text = "Tổng cộng";
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].Text = "";
                e.Row.Cells[4].Text = "";
                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int orderId = int.Parse(Request.QueryString["id"].ToString());
                string url = string.Format("Print/?id={0}", orderId);

                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + url + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            }
        }
    }
}