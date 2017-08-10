using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Resources.Manage
{
    public partial class Default : WebAdmin.Base.BasePage
    {
        const int pagesize = 20;

        #region Properties

        public enum FormState
        {
            Default,
            NoDataState,
            ErrorState,
            ConfirmState
        }

        private FormState m_State = FormState.Default;

        public FormState CurrentFormState
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;

                switch (m_State)
                {
                    case FormState.Default:
                        pnlDataGridBox.Visible = true;
                        pnlErrorBox.Visible = false;
                        pnlNoDataBox.Visible = false;
                        break;

                    case FormState.NoDataState:
                        ucErrorBox.Message = "Chưa có dữ liệu";
                        pnlDataGridBox.Visible = false;
                        pnlErrorBox.Visible = false;
                        pnlNoDataBox.Visible = true;
                        break;

                    case FormState.ErrorState:
                        pnlDataGridBox.Visible = false;
                        pnlErrorBox.Visible = true;
                        pnlNoDataBox.Visible = false;
                        break;
                }
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "6041BDB8-377C-4A92-8EFF-E17AFB7BA862";
        }


        protected bool ShowDeleteButton(string resourceName, int applicationID)
        {
            string applicationName = "";
            ApplicationManager appMan = new ApplicationManager();
            Inside.SecurityProviders.Application app = appMan.GetApplication(applicationID);
            applicationName = app.Name;
            return (resourceName.ToLower() != applicationName.ToLower());
        }

        private void LoadApplications()
        {
            try
            {
                ApplicationManager appMan = new ApplicationManager();
                ApplicationCollection apps = appMan.GetAllApplications();

                if (apps.Count > 0)
                {
                    ddlApplications.DataSource = apps;
                    ddlApplications.DataValueField = "ApplicationID";
                    ddlApplications.DataTextField = "Name";
                    ddlApplications.DataBind();

                    if (Request["id"] != null)
                    {
                        ddlApplications.SelectedValue = Request["id"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Load applications: " + ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
            
        }

        public void PagerSelect(object sender, EventArgs e)
        {
            if (ViewState["Keyword"] == null)
                getResourceByApplication(int.Parse(ddlApplications.SelectedValue), Pager1.SelectedPageIndex);
            else
            {
                FindResource(ViewState["SearchBy"].ToString(), ViewState["Keyword"].ToString(), Pager1.SelectedPageIndex);
            }
            
        }

        private void 
            getResourceByApplication(int applicationID, int curPage)
        {
            try
            {
                ResourceManager rsMan = new ResourceManager();
                ResourceCollection resources = rsMan.FindResourcesByApplication(applicationID);

                PagedDataSource page = new PagedDataSource();
                page.AllowPaging = true;
                page.PageSize = pagesize;
                page.CurrentPageIndex = curPage;
                page.DataSource = resources;
                Pager1.SetPageNumber(page.PageCount, curPage);

                dgridResource.DataSource = page;
                dgridResource.DataBind();
                CurrentFormState = FormState.Default;
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Get resources by Application: " + ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }            
        }

        private void FindResource(string searchBy, string keyword, int curPage)
        {
            ResourceManager rsMan = new ResourceManager();
            ResourceCollection resources = new ResourceCollection();
            try
            {
                if (keyword == "")
                {
                    resources = rsMan.FindResourcesByApplication(int.Parse(keyword));
                }
                else
                {
                    switch (searchBy)
                    {
                        case "name":                        
                            resources = rsMan.FindResourcesByName(keyword);
                            break;                        
                        case "path":                                                
                            resources = rsMan.FindResourcesByPath(keyword);
                            break;
                        default:
                            break;
                    }
                    ResourceCollection resourcesTemp = resources;
                    resources = rsMan.FilterResourceByApplication(resourcesTemp, int.Parse(ddlApplications.SelectedValue));                
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "FindResource by " + searchBy + ": " + ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
            if (resources.Count > 0)
            {
                CurrentFormState = FormState.Default;

                PagedDataSource page = new PagedDataSource();
                page.AllowPaging = true;
                page.PageSize = pagesize;
                page.CurrentPageIndex = curPage;
                page.DataSource = resources;
                Pager1.SetPageNumber(page.PageCount, curPage);
                                
                dgridResource.DataSource = page;
                dgridResource.DataBind();
            }
            else
            {
                CurrentFormState = FormState.NoDataState;
            }
        }

        private void ChangeStatus(int resourceID, bool newStatus)
        {
            try
            {
                ResourceManager rsMan = new ResourceManager();
                rsMan.UpdateResourceStatus(resourceID, newStatus);

                getResourceByApplication(int.Parse(ddlApplications.SelectedValue), Pager1.SelectedPageIndex);
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Change status: " + ex.ToString();
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }            
        }

        private void DeleteResource(int resourceID)
        {
            try
            {
                ResourceManager rsMan = new ResourceManager();
                rsMan.Remove(resourceID);

                getResourceByApplication(int.Parse(ddlApplications.SelectedValue), Pager1.SelectedPageIndex);
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "Delete: " + ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Quản lý tài nguyên");                
                               
                LoadApplications();
                getResourceByApplication(int.Parse(ddlApplications.SelectedValue), 0);
            }
        }

        protected void ddlSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ViewState["SearchBy"] == null)
                ViewState.Add("SearchBy", ddlSearchBy.SelectedValue);
            else
                ViewState["SearchBy"] = ddlSearchBy.SelectedValue;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtKeyword.Text.Trim() == "")
            {
                getResourceByApplication(int.Parse(ddlApplications.SelectedValue), Pager1.SelectedPageIndex);
            }
            else
            {
                if (ViewState["SeachBy"] == null)
                    ViewState.Add("SearchBy", ddlSearchBy.SelectedValue.ToString());
                else
                    ViewState["SearchBy"] = ddlSearchBy.SelectedValue.ToString();

                if (ViewState["Keyword"] == null)
                    ViewState.Add("Keyword", txtKeyword.Text.Trim());
                else
                    ViewState["Keyword"] = txtKeyword.Text.Trim();

                FindResource(ViewState["SearchBy"].ToString(), txtKeyword.Text.Trim(), Pager1.SelectedPageIndex);
            }            
        }

        protected void ddlApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            getResourceByApplication(int.Parse(ddlApplications.SelectedValue), Pager1.SelectedPageIndex);

            if (ViewState["SearchBy"] == null)
            {
                ViewState.Add("SearchBy", "application");
            }
            else
            {                
                ViewState["SearchBy"] = "application";
                //if (ViewState["Keyword"] != null)
                //    getResourceByApplication(int.Parse(ddlApplications.SelectedValue));
                //else
                //    FindResource(ViewState["SearchBy"].ToString(), ViewState["Keyword"].ToString());                                
            }
        }

        protected void cbxStatus_CheckChanged(object sender, EventArgs e)
        {
            CheckBox cbxStatus = (CheckBox)(sender);                                     
            int resourceID = int.Parse(cbxStatus.Text);
            bool newStatus = cbxStatus.Checked;

            ChangeStatus(resourceID, newStatus);
        }

        protected void lbtnDelete_Clicked(object sender, EventArgs e)
        {
            LinkButton lbtnDelete = (LinkButton)(sender);
            int ResourceID = int.Parse(lbtnDelete.CommandArgument);

            DeleteResource(ResourceID);
        }
    }
}
