using Inside.InsideData.Business;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Inside.Function.Order.Detail.Print
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            DataTable dtAccount = new DataTable();

            decimal totalAmount = 0, totalQuanTity = 0;

            if (Request.QueryString["id"] != null)
            {
                int orderId = int.Parse(Request.QueryString["id"].ToString());

                dtOrder = orderManager.Order_GetList(orderId);
                if (dtOrder != null && dtOrder.Rows.Count > 0)
                {
                    DataRow drOrder = dtOrder.Rows[0];
                    int accountId = int.Parse(drOrder["AccountId"].ToString());

                    lblAccountName.Text = accountManager.Account_GetName(accountId);
                    lblNote.Text = drOrder["Note"].ToString();

                    dtAccount = accountManager.Account_GetList(accountId);
                    if (dtAccount != null && dtAccount.Rows.Count > 0)
                    {
                        lblAddress.Text = dtAccount.Rows[0]["Address"].ToString();
                    }

                    dtOrderDetail = orderDetailManager.OrderDetail_GetList_OrderId(orderId);
                    if (dtOrderDetail != null && dtOrderDetail.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtOrderDetail.Rows)
                        {
                            totalQuanTity += decimal.Parse(row["Quantity"].ToString());
                            totalAmount += decimal.Parse(row["Amount"].ToString());
                        }

                        //add row SUM
                        DataRow sumRow;
                        sumRow = dtOrderDetail.NewRow();
                        sumRow["OrderId"] = -1;
                        sumRow["OrderDetailId"] = -1;
                        sumRow["ProductId"] = -1;
                        sumRow["Price"] = -1;
                        sumRow["Quantity"] = totalQuanTity;
                        sumRow["Amount"] = totalAmount;
                        sumRow["UnitTypeId"] = -1;
                        sumRow["UnitValue"] = -1;
                        sumRow["UnitId"] = -1;
                        dtOrderDetail.Rows.Add(sumRow);

                        gvData.DataSource = dtOrderDetail;
                        gvData.DataBind();

                        lblNumberWord.Text = MoneyToString(totalAmount);

                        gvData.Visible = true;
                        gvData.CssClass = gvData.CssClass.Replace("Label", "");
                        gvData.CssClass = gvData.CssClass.Replace("colunmHeader", "");
                        gvData.CssClass = gvData.CssClass.Replace("HlightRow", "");
                        gvData.CssClass = gvData.CssClass.Replace("Label", "");
                    }
                    else
                    {
                        gvData.Visible = false;
                    }
                }
            }
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ProductManager productManager = new ProductManager();
            UnitManager unitManager = new UnitManager();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = productManager.Product_GetName(int.Parse(e.Row.Cells[1].Text));
                e.Row.Cells[5].Text = unitManager.UnitType_GetName(int.Parse(e.Row.Cells[5].Text));
                e.Row.Cells[7].Text = unitManager.Unit_GetName(int.Parse(e.Row.Cells[7].Text));
            }

            if (e.Row.Cells[1].Text.Trim() == "-1")
            {
                e.Row.Font.Bold = true;
                e.Row.ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "Tổng cộng";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].Text = "";
                e.Row.Cells[5].Text = "";
                e.Row.Cells[6].Text = "";
                e.Row.Cells[7].Text = "";
            }
        }

        private string MoneyToString(decimal Amount)
        {
            string Resp, Tien, DOC, Dem, Nhom, Chu, So1, So2, So3, Dich;
            int S;
            if (Amount == 0)
            {
                Resp = "Không đồng";
            }
            else
            {
                if (Amount > 999999999999)
                {
                    Resp = "Số quá lớn";
                }
                else
                {
                    if (Amount < 0)
                        Resp = "Trừ";
                    else
                    {
                        Resp = string.Empty;
                    }
                    Tien = string.Format("{0:###########0.00}", decimal.Parse(Amount.ToString()));
                    Tien = (Tien.PadLeft(15));
                    DOC = Dem = string.Empty;
                    DOC = DOC + "trăm  mươi  tỷ    ";
                    DOC = DOC + "trăm  mươi  triệu ";
                    DOC = DOC + "trăm  mươi  ngàn  ";
                    DOC = DOC + "trăm  mươi  đồng  ";
                    DOC = DOC + "trăm  mươi  xu    ";
                    Dem = Dem + "một  hai  ba   bốn  năm  ";
                    Dem = Dem + "sáu  bảy  tám  chín";


                    for (int i = 1; i < 6; i++)
                    {
                        Nhom = Tien.Substring(i * 3 - 3, 3);
                        if (Nhom != "".PadRight(3))
                        {
                            switch (Nhom)
                            {
                                case "000":
                                    Chu = (i == 4 ? "đồng " : "");
                                    break;
                                case ".00":
                                    Chu = "chẵn";
                                    break;

                                default:
                                    So1 = Nhom.Substring(0, 1);
                                    So2 = Nhom.Substring(1, 1);
                                    So3 = Nhom.Substring(Nhom.Length > 1 ? Nhom.Length - 1 : 0, 1);
                                    Chu = "";
                                    for (int j = 1; j < 4; j++)
                                    {
                                        Dich = "";
                                        if (int.TryParse(Nhom.Substring(j - 1, 1), out S) == false)
                                            S = -1;
                                        if (S > 0)
                                        {
                                            Dich = Dem.Substring(S * 5 - 5, 4).Trim() + " ";
                                            Dich += DOC.Substring((i - 1) * 18 + j * 6 - 5 - 1, 5) + " ";
                                        }
                                        switch (j)
                                        {
                                            case 2:
                                                if (S == 1)
                                                    Dich = "mười ";
                                                else if (S == 0 && So3 != "0")
                                                    if ((int.Parse(So1) >= 1 && int.Parse(So1) <= 9) ||
                                                        (So1 == "0" && i == 4))
                                                        Dich = "lẻ ";
                                                break;
                                            case 3:
                                                if (S == 0 && Nhom != "".PadRight(2) + "0")
                                                    Dich = DOC.Substring((i - 1) * 18 + j * 6 - 5 - 1, 5).Trim() + "".PadRight(1);
                                                else if (S == 5 && So2 != "".PadRight(1) && So2 != "0")
                                                    Dich = "l" + Dich.Substring(1);
                                                break;
                                            default:
                                                break;
                                        }
                                        Chu += Dich;
                                    }
                                    break;

                            }
                            Chu = Chu.Replace("  ", " ");
                            Resp += Chu.Replace("mươi một", "mươi mốt");
                        }
                    }
                }
            }
            return Resp.Substring(0, 1).ToUpper() + Resp.Substring(1) + "./.";
        }
    }
}