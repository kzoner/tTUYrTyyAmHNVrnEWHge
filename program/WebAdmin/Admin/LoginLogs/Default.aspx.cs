using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
using System.Data;

namespace WebAdmin.Admin.LoginLogs
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "274D7D25-AD73-4621-915D-2E21DDBD0ECE";
        }

        protected string LoginResult(int ResultStatus)
        {
            if (ResultStatus == 0)
            {
                return "Thất bại";
            }
            else
            {
                return "Thành công";
            }
        }

        private void LoadLoginLogs()
        {
            try
            {
                ActionLogManager action = new ActionLogManager();
                DateTime FromDate = DateParse(txtFromDate.Text.Trim());
                DateTime ToDate = DateParse(txtToDate.Text.Trim());                
                string UserName = "";

                DataTable dt = action.GetLoginLogs(FromDate, ToDate, UserName);
                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    if(txtUserName.Text.Trim() != "")
                        dv.RowFilter = "UserName = '" + txtUserName.Text.Trim() + "'";

                    gvLoginLogs.DataSource = dv;
                    gvLoginLogs.DataBind();

                    gvLoginLogs.Visible = true;
                    tblNoData.Visible = false;
                    tblError.Visible = false;
                }
                else
                {
                    gvLoginLogs.Visible = false;
                    tblNoData.Visible = true;
                    tblError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gvLoginLogs.Visible = false;
                tblNoData.Visible = false;
                tblError.Visible = true;

                SaveErrorLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Login Log");

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                LoadLoginLogs();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadLoginLogs();
        }

        protected void gvLoginLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoginLogs.PageIndex = e.NewPageIndex;
            LoadLoginLogs();
        }
    }
}
