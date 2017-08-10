using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;
using System.Data;

namespace WebAdmin.Admin.ErrorLog
{
    public partial class Default : Base.BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "5AFF9742-390A-4E49-BC39-1BDA4B7EBEBB";
        }

        private void LoadErrorLog()
        {
            DataTable dtErrorLog = new DataTable("ErrorLog");
            try
            {                
                IFormatProvider cul = new System.Globalization.CultureInfo("VI-vn");
                DateTime fromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", cul); ;
                DateTime toDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", cul); ;
                string currentUser = txtUserName.Text.Trim();

                ErrorLogManager errMan = new ErrorLogManager();
                dtErrorLog = errMan.GetErrorLog(fromDate, toDate, currentUser);

                if (dtErrorLog.Rows.Count > 0)
                {
                    gvErrorLog.DataSource = dtErrorLog;
                    gvErrorLog.DataBind();
                    gvErrorLog.Visible = true;
                    tblNoData.Visible = false;
                    uctErrorBox.Visible = false;    
                }
                else
                {
                    tblNoData.Visible = true;
                }

                
            }
            catch (Exception ex)
            {
                gvErrorLog.Visible = false;
                tblNoData.Visible = false;
                uctErrorBox.Visible = true;
                this.SaveErrorLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ContentPage)Master).SetFormTitle("Error Log");

            if (!Page.IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.Day == 1 ? new DateTime(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month, 1).ToString("dd/MM/yyyy") : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");                                
                LoadErrorLog();                
            }                        
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadErrorLog();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                SaveActionLog("Clear", "");
                ErrorLogManager errorMan = new ErrorLogManager();
                errorMan.ClearErrorLog();
                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                SaveErrorLog(ex);
            }
            
        }

        protected void gvErrorLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvErrorLog.PageIndex = e.NewPageIndex;
            LoadErrorLog();
        }
    }
}
