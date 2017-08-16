using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Product.Add
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "5C716BEB-9647-43AB-BC92-216B676ED2C9";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Sản phẩm] Thêm mới");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadProductType();
                LoadUnitType(int.Parse(ddlProductType.SelectedValue));
                LoadUnit(int.Parse(ddlUnitType.SelectedValue));
                LoadProductStatus();
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

        private void SaveData()
        {
            try
            {
                int code = -1;
                string msg = "", fileName = "", filePath = "";

                ProductManager productManager = new ProductManager();
                ProductBase product = new ProductBase();

                product.ProductTypeId = int.Parse(ddlProductType.SelectedValue);
                product.ProductCode = txtProductCode.Text.Trim();
                product.ProductName = txtProductName.Text.Trim();
                product.Price = decimal.Parse(txtPrice.Text.Trim().Replace(",",""));
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
                product.CreateDate = DateTime.Now;
                product.CreateUser = User.Identity.Name;

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
                    product.ImageName = "";
                    product.ImagePath = "";

                    productManager.Product_Insert(product, ref code, ref msg);
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