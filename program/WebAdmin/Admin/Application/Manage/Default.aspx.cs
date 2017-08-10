using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Application.Manage
{
    public partial class Default : Base.BasePage
    {



        const int pagesize = 3;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "007947F0-DDCA-4354-AFE5-2FAFA27CC8DD";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            uctErrorBox.Visible = false;
            if (!IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Quản lý ứng dụng");
                LoadApplicationList(0);
            }
            
        }

        private void LoadApplicationList(int curPage )
        {
            ApplicationManager app = new ApplicationManager() ;
            ApplicationCollection apps = new ApplicationCollection();
            apps = app.GetAllApplications();
            if (apps.Count == 0)
            {
                uctErrorBox.Visible = true;
                uctErrorBox.Message = "Không có dữ liệu";
                return;
            }

            PagedDataSource page = new PagedDataSource();
            page.AllowPaging = true;
            page.PageSize = pagesize;
            page.CurrentPageIndex = curPage;
            page.DataSource = apps;

            //if (!IsPostBack)
            //{
            Pager1.SetPageNumber(page.PageCount, curPage);
            //}
            
            dgridApplicationList.DataSource = page;
            dgridApplicationList.DataBind();
        }

        protected void dgridApplicationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (Pager1.SelectedPageIndex * pagesize + e.Row.RowIndex + 1).ToString();
            }
        }

        protected void PagerSelect(object sender, EventArgs e)
        {
            LoadApplicationList(Pager1.SelectedPageIndex);
        }

        protected void lBtnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            
            ApplicationManager appManager = new ApplicationManager();

            Inside.SecurityProviders.Application app = 
                new Inside.SecurityProviders.Application(int.Parse(btn.CommandArgument), "", "");

            //Xoa ung dung
            appManager.Remove(app);
            LoadApplicationList(0);

        }

    }
}
