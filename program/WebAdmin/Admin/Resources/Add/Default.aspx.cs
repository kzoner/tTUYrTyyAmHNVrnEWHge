using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Inside.SecurityProviders;
using Inside.DataProviders;

namespace WebAdmin.Admin.Resources.Add
{
    public partial class Default : Base.BasePage
    {
        #region Properties

        public enum FormState
        {
            Default,            
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
                        pnlErrorBox.Visible = false;
                        pnlConfirmBox.Visible = false;
                        break;            

                    case FormState.ErrorState:
                        pnlErrorBox.Visible = true;
                        pnlConfirmBox.Visible = false;
                        break;
                    case FormState.ConfirmState:
                        pnlErrorBox.Visible = false;
                        pnlConfirmBox.Visible = true;
                        break;
                }
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "F4F6B131-6F12-4164-BBDD-7425021440AA";
        }

        private void GetNewToken()
        {
            try
            {
                SQLDatabase objSQL = new SQLDatabase(ConfigurationManager.AppSettings["AUTHDB_CONN"]);
                DataTable dt = new DataTable("Token");
                SqlDataReader reader = (SqlDataReader)(objSQL.ExecuteReader("UspGenerateToken", CommandType.StoredProcedure));
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    txtToken.Text = dt.Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                SaveErrorLog(ex);
                ucErrorBox.Message = "Không tạo được token.";
            }
        }
        

        private void LoadApplications()
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

        private void LoadReSourceTypes()
        {
            ResourceTypeManager rsTypeMan = new ResourceTypeManager();
            ResourceTypeCollection rsTypes = rsTypeMan.GetAllResourceTypes();

            if (rsTypes.Count > 0)
            {
                ddlResourceType.DataSource = rsTypes;
                ddlResourceType.DataValueField = "ResourceTypeCode";
                ddlResourceType.DataTextField = "Name";
                ddlResourceType.DataBind();

                ddlResourceType.SelectedIndex = 1;
            }
        }

        private void CreateResource()
        {
            try
            {
                ResourceManager rsMan = new ResourceManager();
                string resourceTypeCode = ddlResourceType.SelectedValue.ToString();
                string path = txtPath.Text.Trim();
                string fileName = txtFileName.Text.Trim();
                string link = txtLink.Text.Trim();
                int applicationID = Int32.Parse(ddlApplications.SelectedValue.ToString());
                string resourceName = txtResourceName.Text.Trim();
                bool status = true;
                bool isParent = cbxIsParent.Checked;
                string token = txtToken.Text.Trim();

                if (isParent)
                {
                    path = "";
                    fileName = "";
                    link = "";
                    resourceTypeCode = "DEFAULT";
                }

                rsMan.Create(resourceTypeCode, path, fileName, link, applicationID, resourceName, status, isParent, token);

                ucConfirmBox.ConfirmMessage = "Tạo tài nguyên thành công. Tiếp tục tạo tài nguyên khác?";
                CurrentFormState = FormState.ConfirmState;
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
            }

            if (CurrentFormState == FormState.Default) Response.Redirect("../Manage/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Tạo tài nguyên mới.");
                LoadApplications();
                LoadReSourceTypes();
                txtFileName.Text = "Default.aspx";
                CurrentFormState = FormState.Default;
                GetNewToken();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            CreateResource();
        }

        protected void cbxIsParent_CheckedChanged(object sender, EventArgs e)
        {
            rvPath.Enabled = !cbxIsParent.Checked;
            rvFileName.Enabled = !cbxIsParent.Checked;

            txtPath.Enabled = !cbxIsParent.Checked;
            txtFileName.Enabled = !cbxIsParent.Checked;
            txtLink.Enabled = !cbxIsParent.Checked;
            ddlResourceType.Enabled = !cbxIsParent.Checked;
            txtToken.Enabled = !cbxIsParent.Checked;
            if (cbxIsParent.Checked)
                txtToken.Text = "DEFAULT";
            else
                GetNewToken();
            //txtToken.Text = cbxIsParent.Checked ? "DEFAULT" : "";
        }

        protected void ClickedYes(object sender, EventArgs e)
        {
            //ddlApplications.SelectedIndex = 0;
            //ddlResourceType.SelectedIndex = 0;
            //cbxIsParent.Checked = false;
            //txtResourceName.Text = "";
            //txtPath.Text = "";
            //txtFileName.Text = "";
            //txtLink.Text = "";
            //txtToken.Text = "";
            //CurrentFormState = FormState.Default;

            Response.Redirect("Default.aspx");
        }

        protected void ClickedNo(object sender, EventArgs e)
        {
            Response.Redirect("../Manage/");
        }
    }
}
