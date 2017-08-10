using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
using System.Data;

namespace WebAdmin.Admin.ActionLogs
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "C2ABB748-FF39-4FFF-BA3C-AB024367E99C";
        }

        private void LoadActionLogs()
        {
            try
            {
                ActionLogManager action = new ActionLogManager();
                DateTime FromDate = DateParse(txtFromDate.Text.Trim());
                DateTime ToDate = DateParse(txtToDate.Text.Trim());
                int SearchType = int.Parse(ddlSearchType.SelectedValue);
                string Keyword = txtKeyword.Text.Trim();
                
                DataTable dt = action.SearchActionLogs(FromDate, ToDate, SearchType, Keyword);
                if (dt.Rows.Count > 0)
                {
                    gvActionLogs.DataSource = dt;
                    gvActionLogs.DataBind();

                    gvActionLogs.Visible = true;
                    tblNoData.Visible = false;
                    tblError.Visible = false;
                }
                else
                {
                    gvActionLogs.Visible = false;
                    tblNoData.Visible = true;
                    tblError.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gvActionLogs.Visible = false;
                tblNoData.Visible = false;
                tblError.Visible = true;

                SaveErrorLog(ex);
            }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Actions Logs");

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlSearchType.Items.Add(new ListItem("Username", "2"));
                ddlSearchType.Items.Add(new ListItem("IP", "1"));
                ddlSearchType.Items.Add(new ListItem("Đường dẫn", "3"));

                LoadActionLogs();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadActionLogs();
        }

        protected void gvActionLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvActionLogs.PageIndex = e.NewPageIndex;
            LoadActionLogs();
        }
    }
}
