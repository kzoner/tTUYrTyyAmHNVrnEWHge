using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Resources.Edit
{
    public partial class Default : Base.BasePage
    {
        #region Properties

        public enum FormState
        {
            Default,
            ErrorState,
            NofityState
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
                        pnlErrorBox.Visible = false;
                        pnlNotifyBox.Visible = false;
                        break;

                    case FormState.ErrorState:
                        pnlErrorBox.Visible = true;
                        pnlNotifyBox.Visible = false;
                        break;
                    case FormState.NofityState:
                        pnlErrorBox.Visible = false;
                        pnlNotifyBox.Visible = true;
                        break;
                }
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "6184317B-6C05-471B-8CFE-0F5ED50A9879";
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
                }
            }
            catch (Exception ex)
            {
                CurrentFormState = FormState.ErrorState;
                ucErrorBox.Message = ex.Message;
                this.SaveErrorLog(ex);
            }            
        }

        private void LoadReSourceTypes()
        {
            try
            {
                ResourceTypeManager rsTypeMan = new ResourceTypeManager();
                ResourceTypeCollection rsTypes = rsTypeMan.GetAllResourceTypes();

                if (rsTypes.Count > 0)
                {
                    ddlResourceType.DataSource = rsTypes;
                    ddlResourceType.DataValueField = "ResourceTypeCode";
                    ddlResourceType.DataTextField = "Name";
                    ddlResourceType.DataBind();
                }
            }
            catch (Exception ex)
            {
                CurrentFormState = FormState.ErrorState;
                ucErrorBox.Message = ex.Message;
                this.SaveErrorLog(ex);
            }
            
        }

        private void LoadResourceInfo()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int resourceID = Int32.Parse(Request["id"].ToString());
                    ResourceManager rsMan = new ResourceManager();
                    Resource resource = rsMan.GetResource(resourceID);

                    ddlApplications.SelectedValue = resource.ApplicationID.ToString();
                    ddlResourceType.SelectedValue = resource.ResourceTypeCode;
                    txtResourceName.Text = resource.ResourceName;
                    txtPath.Text = resource.Path;
                    txtFileName.Text = resource.FileName;
                    txtLink.Text = resource.Link;
                    hfStatus.Value = resource.Status.ToString();
                    txtToken.Text = resource.Token.ToString();

                    if ((txtPath.Text.Trim() == "") && (txtFileName.Text.Trim() == "") && (ddlResourceType.SelectedValue == "DEFAULT"))
                    {
                        cbxIsParent.Checked = true;
                        ddlResourceType.Enabled = false;
                        txtPath.Enabled = false;
                        txtFileName.Enabled = false;
                        txtLink.Enabled = false;
                    }
                    if (cbxIsParent.Checked)
                    {
                        rvPath.ControlToValidate = "txtResourceName";
                        rvFileName.ControlToValidate = "txtResourceName";
                        rvToken.ControlToValidate = "txtResourceName";

                    }
                    else
                    {
                        rvPath.ControlToValidate = "txtPath";
                        rvFileName.ControlToValidate = "txtFileName";
                        rvToken.ControlToValidate = "txtToken";
                    }
                    CurrentFormState = FormState.Default;
                }
            }
            catch (Exception ex)
            {
                CurrentFormState = FormState.ErrorState;
                ucErrorBox.Message = ex.Message;
                this.SaveErrorLog(ex);
            }            
        }

        private void UpdateResource()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int resourceID = int.Parse(Request["id"].ToString());
                    int applicationID = int.Parse(ddlApplications.SelectedValue.ToString());
                    string resourceTypeCode = ddlResourceType.SelectedValue.ToString();
                    string resourceName = txtResourceName.Text.Trim();
                    string path = txtPath.Text.Trim();
                    string fileName = txtFileName.Text.Trim();
                    string link = txtLink.Text.Trim();
                    bool status = bool.Parse(hfStatus.Value);
                    string token = txtToken.Text.Trim();

                    if (cbxIsParent.Checked)
                    {
                        path = "";
                        fileName = "";
                        link = "";
                        resourceTypeCode = "DEFAULT";
                    }

                    ResourceManager rsMan = new ResourceManager();
                    Resource resource = new Resource(resourceID, resourceTypeCode, path, fileName, link, applicationID, resourceName, status, token);
                    rsMan.Update(resource);

                    CurrentFormState = FormState.NofityState;
                    ucNotifyBox.Message = "Cập nhật thành công.";
                }
            }
            catch (Exception ex)
            {
                CurrentFormState = FormState.ErrorState;
                ucErrorBox.Message = ex.Message;
                this.SaveErrorLog(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Cập nhật thông tin tài nguyên.");

                LoadApplications();
                LoadReSourceTypes();
                LoadResourceInfo();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateResource();
        }


        protected void cbxIsParent_CheckedChanged(object sender, EventArgs e)
        {
            rvPath.Enabled = !cbxIsParent.Checked;
            rvFileName.Enabled = !cbxIsParent.Checked;

            txtPath.Enabled = !cbxIsParent.Checked;
            txtFileName.Enabled = !cbxIsParent.Checked;
            txtLink.Enabled = !cbxIsParent.Checked;
            ddlResourceType.Enabled = !cbxIsParent.Checked;
            if (cbxIsParent.Checked)
            {
                rvPath.ControlToValidate = "txtResourceName";
                rvFileName.ControlToValidate = "txtResourceName";
                rvToken.ControlToValidate = "txtResourceName";

            }
            else
            {
                rvPath.ControlToValidate = "txtPath";
                rvFileName.ControlToValidate = "txtFileName";
                rvToken.ControlToValidate = "txtToken";
            }




        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //ddlApplications.SelectedIndex = 0;
            //ddlResourceType.SelectedIndex = 0;
            //cbxIsParent.Checked = false;
            //txtResourceName.Text = "";
            //txtPath.Text = "";
            //txtFileName.Text = "";
            //txtLink.Text = "";
            
            //CurrentFormState = FormState.Default;
            Response.Redirect("../Manage/");
        }
    }
}
