using Inside.InsideData.Base;
using Inside.InsideData.Business;
using System;
using System.Data;

namespace WebAdmin.Function.Fee.Edit
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "4EC1BBAC-A226-4CB8-99FE-79521B45FCCD";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Chi phí] Cập nhật");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadFeeType();
                LoadFeeStatus();
                LoadData();
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

        private void LoadData()
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    FeeManager feeManager = new FeeManager();
                    int feeId = int.Parse(Request.QueryString["id"].ToString());
                    DataTable dt = new DataTable();

                    dt = feeManager.Fee_GetList(feeId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        ddlFeeType.SelectedValue = dr["FeeTypeId"].ToString();
                        txtAmount.Text = decimal.Parse(dr["Amount"].ToString()).ToString("#,##0");
                        txtUserName.Text = dr["UserName"].ToString();
                        txtNote.Text = dr["Note"].ToString();
                        ddlFeeStatus.SelectedValue = dr["FeeStatus"].ToString();
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
                    string msg = "";

                    FeeManager feeManager = new FeeManager();
                    FeeBase fee = new FeeBase();

                    fee.FeeId = id;
                    fee.FeeTypeId = int.Parse(ddlFeeType.SelectedValue);
                    fee.Amount = decimal.Parse(txtAmount.Text.Trim().Replace(",", ""));
                    fee.UserName = txtUserName.Text.Trim();
                    fee.Note = txtNote.Text.Trim();
                    fee.FeeStatus = int.Parse(ddlFeeStatus.SelectedValue);
                    fee.UpdateDate = DateTime.Now;
                    fee.UpdateUser = User.Identity.Name;

                    feeManager.Fee_Update(fee, ref code, ref msg);

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
            if (Permission.IsAllowedUpdate)
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
    }
}