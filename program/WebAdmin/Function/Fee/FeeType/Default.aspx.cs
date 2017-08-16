using Inside.InsideData.Business;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAdmin.Function.Fee.FeeType
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "9A000FD6-D756-46BC-A761-27BB6633DB38";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("[Loại chi phí]");
            if (!IsPostBack)
            {
                ErrorBox.Message = string.Empty;
                NotifyBox.Message = string.Empty;
                LoadData(1, false);
            }
        }

        private void LoadFeeTypeStatus()
        {
            try
            {
                StatusManager statusManager = new StatusManager();
                DataTable dt = statusManager.Status_GetList();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ddlFeeTypeStatus.DataSource = dt;
                    ddlFeeTypeStatus.DataValueField = "StatusId";
                    ddlFeeTypeStatus.DataTextField = "StatusName";
                    ddlFeeTypeStatus.DataBind();

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
                lblFeeTypeStatus.Visible = false;
                ddlFeeTypeStatus.Visible = false;
                btnAdd.Text = "Thêm mới";
                btnAdd.CommandName = "Add";

                FeeManager feeManager = new FeeManager();
                int rowsPerPage, rowTotal;
                string feeTypeName;

                feeTypeName = txtFeeTypeName.Text.Trim();
                rowsPerPage = 30;
                rowTotal = feeManager.FeeType_RowTotal(feeTypeName);

                if (rowTotal > 0)
                {
                    rowsPerPage = isExcel ? rowTotal : rowsPerPage;
                    uctPager.SetPageNumber(rowTotal, rowsPerPage, pageNumber);
                    DataTable dt = feeManager.FeeType_Search(feeTypeName, rowsPerPage, pageNumber);

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
                Excel(ConvertGVtoDT(gvData), "Danh sách loại chi phí");
                LoadData(uctPager.SelectedPageIndex, false);

                SaveActionLog("Xuất Excel", "");
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
            }
        }

        private void SaveData(int action)
        {
            try
            {
                int code = -1, id, feeTypeStatus;
                string msg = "", feeTypeName;

                FeeManager feeManager = new FeeManager();
                feeTypeName = txtFeeTypeName.Text.Trim();

                if (action == 1) //Add
                {
                    feeManager.FeeType_Insert(feeTypeName, ref code, ref msg);

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
                else if (action == 2) //Edit
                {
                    feeTypeStatus = int.Parse(ddlFeeTypeStatus.SelectedValue);
                    id = int.Parse(hfFeeTypeId.Value);

                    feeManager.FeeType_Update(id, feeTypeName, feeTypeStatus, ref code, ref msg);

                    if (code == 0)
                    {
                        ErrorBox.Message = string.Empty;
                        NotifyBox.Message = "Lưu dữ liệu thành công";

                        btnAdd.Text = "Thêm mới";
                        btnAdd.CommandName = "Add";
                    }
                    else
                    {
                        ErrorBox.Message = "Lưu dữ liệu thất bại";
                        NotifyBox.Message = string.Empty;
                    }

                    SaveActionLog("Cập nhật", string.Format("code: {0}; msg: {1}; id: {2}", code, msg, id));
                }
            }
            catch (Exception ex)
            {
                ErrorBox.Message = "Lỗi chức năng";
                NotifyBox.Message = string.Empty;
                SaveErrorLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedAddnew)
            {
                Button btnAdd = (Button)sender;
                string commandName = btnAdd.CommandName;
                if (commandName == "Add")
                {
                    SaveData(1);
                }
                else if (commandName == "Edit")
                {
                    SaveData(2);
                }
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
                NotifyBox.Message = string.Empty;
            }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            if (Permission.IsAllowedUpdate)
            {
                FeeManager feeManager = new FeeManager();
                LinkButton lbtnEdit = (LinkButton)sender;
                int feeTypeId = int.Parse(lbtnEdit.CommandArgument);

                DataTable dt = feeManager.FeeType_GetList_FeeTypeId(feeTypeId);

                if (dt != null && dt.Rows.Count > 0)
                {
                    lblFeeTypeStatus.Visible = true;
                    ddlFeeTypeStatus.Visible = true;
                    LoadFeeTypeStatus();

                    DataRow dr = dt.Rows[0];
                    txtFeeTypeName.Text = dr["FeeTypeName"].ToString();
                    ddlFeeTypeStatus.SelectedValue = dr["FeeTypeStatus"].ToString();
                    hfFeeTypeId.Value = feeTypeId.ToString();

                    btnAdd.Text = "Cập nhật";
                    btnAdd.CommandName = "Edit";
                }
                else
                {
                    ErrorBox.Message = "Không load được dữ liệu";
                    NotifyBox.Message = string.Empty;
                }
            }
            else
            {
                ErrorBox.Message = "Không có quyền thực hiện thao tác này";
            }
        }

        protected void NotifyBox_NextClicked(object sender, EventArgs e)
        {
            Response.Redirect("../FeeType");
        }

        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            StatusManager statusManager = new StatusManager();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[2].Text = statusManager.Status_GetName(int.Parse(e.Row.Cells[2].Text));
            }
        }
    }
}