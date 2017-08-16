using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Product.Edit
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "145F1D7E-A756-4B4B-9582-6F25F41AC56B";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Sản phẩm] Thêm mới");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadProductType();
                LoadProductStatus();
                LoadData();
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

        private string CheckImg(FileUpload fuImg)
        {
            string result = "";

            if (fuImg.PostedFile.ContentType != "image/jpeg")
            {
                result = "Only JPEG files are accepted!";
            }
            else if (fuImg.PostedFile.ContentLength > 625000)
            {
                result = "The file has to be less than 5 mb!";
            }
            else
            {
                result = "done";
            }

            return result;
        }

        private void LoadData()
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    ProductManager productManager = new ProductManager();
                    int productId = int.Parse(Request.QueryString["id"].ToString());
                    DataTable dt = new DataTable();

                    dt = productManager.Product_GetList(productId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ddlProductType.SelectedValue = dr["ProductTypeId"].ToString();
                        LoadUnitType(int.Parse(ddlProductType.SelectedValue)); //LoadUnitType
                        txtProductCode.Text = dr["ProductCode"].ToString();
                        txtProductName.Text = dr["ProductName"].ToString();
                        txtPrice.Text = decimal.Parse(dr["Price"].ToString()).ToString("#,##0");
                        txtLength.Text = float.Parse(dr["Length"].ToString()).ToString("#,##0");
                        txtWidth.Text = float.Parse(dr["Width"].ToString()).ToString("#,##0");
                        txtDepth.Text = float.Parse(dr["Depth"].ToString()).ToString("#,##0");
                        txtHeight.Text = float.Parse(dr["Height"].ToString()).ToString("#,##0");
                        txtWeigh.Text = float.Parse(dr["Weigh"].ToString()).ToString("#,##0");
                        ddlUnitType.SelectedValue = dr["UnitTypeId"].ToString();
                        LoadUnit(int.Parse(ddlUnitType.SelectedValue)); //LoadUnit
                        txtUnitValue.Text = float.Parse(dr["UnitValue"].ToString()).ToString("#,##0");
                        ddlUnit.SelectedValue = dr["UnitId"].ToString();
                        hfImagePath.Value = dr["ImagePath"].ToString();
                        hfImageName.Value = dr["ImageName"].ToString();
                        txtNote.Text = dr["Note"].ToString();
                        ddlProductStatus.SelectedValue = dr["ProductStatus"].ToString();
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
                    string msg = "", fileName = "", filePath = "";

                    ProductManager productManager = new ProductManager();
                    ProductBase product = new ProductBase();

                    product.ProductId = id;
                    product.ProductTypeId = int.Parse(ddlProductType.SelectedValue);
                    product.ProductCode = txtProductCode.Text.Trim();
                    product.ProductName = txtProductName.Text.Trim();
                    product.Price = decimal.Parse(txtPrice.Text.Trim().Replace(",", ""));
                    product.Length = float.Parse(txtLength.Text.Trim().Replace(",", ""));
                    product.Width = float.Parse(txtWidth.Text.Trim().Replace(",", ""));
                    product.Depth = float.Parse(txtDepth.Text.Trim().Replace(",", ""));
                    product.Height = float.Parse(txtHeight.Text.Trim().Replace(",", ""));
                    product.Weigh = float.Parse(txtWeigh.Text.Trim().Replace(",", ""));
                    product.UnitTypeId = int.Parse(ddlUnitType.SelectedValue);
                    product.UnitId = int.Parse(ddlUnit.SelectedValue);
                    product.UnitValue = float.Parse(txtUnitValue.Text.Trim().Replace(",", ""));
                    product.Note = txtNote.Text.Trim();
                    product.ProductStatus = int.Parse(ddlProductStatus.SelectedValue);
                    product.UpdateDate = DateTime.Now;
                    product.UpdateUser = User.Identity.Name;

                    if (fuImage.HasFile)
                    {
                        string result = CheckImg(fuImage);
                        if (result == "done")
                        {
                            FileInfo fi = new FileInfo(fuImage.FileName);
                            fileName = RandomString(30) + fi.Extension;
                            filePath = Server.MapPath(@"~\Images\Product\" + fileName);

                            product.ImageName = fileName;
                            product.ImagePath = "/Images/Product/" + fileName;

                            productManager.Product_Insert(product, ref code, ref msg);
                        }
                        else
                        {
                            ErrorBox.Message = result;
                            NotifyBox.Message = string.Empty;
                        }
                    }
                    else
                    {
                        product.ImageName = hfImageName.Value;
                        product.ImagePath = hfImagePath.Value;

                        productManager.Product_Update(product, ref code, ref msg);
                    }

                    if (code == 0)
                    {
                        if (fuImage.HasFile)
                        {
                            fuImage.SaveAs(filePath);
                        }

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

        protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUnitType(int.Parse(ddlProductType.SelectedValue));
            LoadUnit(int.Parse(ddlUnitType.SelectedValue));
        }

        protected void ddlUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUnit(int.Parse(ddlUnitType.SelectedValue));
        }
    }
}