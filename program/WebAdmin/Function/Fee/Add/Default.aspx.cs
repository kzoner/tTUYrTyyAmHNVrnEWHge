using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;

namespace WebAdmin.Function.Fee.Add
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "9B02B705-64B1-47B4-86BF-6F3DE95AA85E";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Chi phí] Thêm mới");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadFeeType();
                LoadFeeStatus();
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

        private void LoadFeeStatus()
        {
            try
            {
                StatusManager statusManager = new StatusManager();
                DataTable dt = statusManager.Status_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlFeeStatus.DataSource = dt;
                    ddlFeeStatus.DataValueField = "StatusId";
                    ddlFeeStatus.DataTextField = "StatusName";
                    ddlFeeStatus.DataBind();

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

                FeeManager feeManager = new FeeManager();
                FeeBase fee = new FeeBase();

                fee.FeeTypeId = int.Parse(ddlFeeType.SelectedValue);
                fee.Amount = decimal.Parse(txtAmount.Text.Trim().Replace(",", ""));
                fee.UserName = txtUserName.Text.Trim();
                fee.Note = txtNote.Text.Trim();
                fee.FeeStatus = int.Parse(ddlFeeStatus.SelectedValue);
                fee.CreateDate = DateTime.Now;
                fee.CreateUser = User.Identity.Name;

                feeManager.Fee_Insert(fee, ref code, ref msg);

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
    }
}