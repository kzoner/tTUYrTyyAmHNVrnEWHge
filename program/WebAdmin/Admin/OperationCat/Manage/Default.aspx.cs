using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.OperationCat.Manage
{
    public partial class Default : Base.BasePage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "EC258DE0-2E43-4230-84CF-085F3CEBB876";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            uctErrorBox.Visible = false;
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Quản lý loại thao tác");
                LoadOperationCatList();
            }
        }

        private void LoadOperationCatList()
        {
            OperationCategoryCollection operationCats = new OperationCategoryCollection();
            OperationCategoryManager operationManager = new OperationCategoryManager();
             operationCats =  operationManager.GetAllOperationCategories();

             dgridOperationCatList.DataSource = operationCats;
             dgridOperationCatList.DataBind();
             if (dgridOperationCatList.Rows.Count <= 0)
             {
                 uctErrorBox.Visible = true;
                 uctErrorBox.Message = "Không có dữ liệu";
             }

        }

        protected void lBtnDelete_Click(object sender, EventArgs e)
        {

            LinkButton imgBtn = (LinkButton) sender;
            OperationCategoryManager operCatManager = new OperationCategoryManager();

            OperationCategory  operCat = new OperationCategory();
            try
            {
                operCat.OperationCode = int.Parse(imgBtn.CommandArgument);
                operCatManager.Remove(operCat);
                SaveActionLog("Xóa Loại thao tác", "OperationCode: " + imgBtn.CommandArgument);
                LoadOperationCatList();
            }
            catch (Exception ex)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Xóa không thành công ";
                this.SaveErrorLog(ex);
            }
        }

        protected void dgridApplicationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}
