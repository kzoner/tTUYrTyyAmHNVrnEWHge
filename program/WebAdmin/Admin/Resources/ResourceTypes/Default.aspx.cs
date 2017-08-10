using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Inside.SecurityProviders;

namespace WebAdmin.Admin.Resources.ResourceTypes
{
    public partial class Default : Base.BasePage
    {
        #region Properties

        public enum FormState
        {
            Default,
            NoDataState,
            ErrorState,
            NofityState,
            SetOperationState
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
                        pnlOperations.Visible = false;
                        pnlNotifyBox.Visible = false;
                        break;

                    case FormState.NoDataState:
                        ucErrorBox.Message = "Chưa có dữ liệu";
                        pnlDataGridBox.Visible = false;
                        pnlErrorBox.Visible = false;
                        pnlNoDataBox.Visible = true;
                        pnlOperations.Visible = false;
                        pnlNotifyBox.Visible = false;
                        break;

                    case FormState.ErrorState:
                        pnlDataGridBox.Visible = false;
                        pnlErrorBox.Visible = true;
                        pnlNoDataBox.Visible = false;
                        pnlOperations.Visible = false;
                        pnlNotifyBox.Visible = false;
                        break;
                    case FormState.NofityState:
                        pnlDataGridBox.Visible = true;
                        pnlErrorBox.Visible = false;
                        pnlNoDataBox.Visible = false;
                        pnlOperations.Visible = true;
                        pnlNotifyBox.Visible = true;
                        break;
                    case FormState.SetOperationState:
                        pnlDataGridBox.Visible = true;
                        pnlErrorBox.Visible = false;
                        pnlNoDataBox.Visible = false;
                        pnlOperations.Visible = true;
                        pnlNotifyBox.Visible = false;
                        break;
                }
            }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Token = "540B8997-7DA3-44E5-A2B0-544DAB42FD24";
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ((ContentPage)Master).SetFormTitle("Quản lý loại tài nguyên.");

                LoadResourceType();
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string code = txtRsTypeCode.Text.Trim();
            string name = txtRsTypeName.Text.Trim();

            CreateResourceType(code, name);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRsTypeCode.Text = string.Empty;
            txtRsTypeName.Text = string.Empty;
        }

        private void LoadResourceType()
        {
            try
            {
                ResourceTypeManager rsTypeMan = new ResourceTypeManager();
                ResourceTypeCollection rsTypes = rsTypeMan.GetAllResourceTypes();
                //Thêm

                if (rsTypes.Count > 0)
                {
                    dgridRsType.DataSource = rsTypes;
                    dgridRsType.DataBind();

                    CurrentFormState = FormState.Default;
                }
                else
                {
                    CurrentFormState = FormState.NoDataState;
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
        }

        private void CreateResourceType(string code, string name)
        {
            try
            {
                ResourceTypeManager rsTypeMan = new ResourceTypeManager();
                rsTypeMan.Create(code, name);
                LoadResourceType();
                CurrentFormState = FormState.Default;
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
            }
        }

        private void UpdateResourceType(string code, string name)
        {
            try
            {
                ResourceTypeManager rsTypeMan = new ResourceTypeManager();
                ResourceType rsType = new ResourceType(code, name);
                rsTypeMan.Update(rsType);
                CurrentFormState = FormState.Default;
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
        }

        private void DeleteResourceType(string code)
        {
            try
            {
                ResourceTypeManager rsTypeMan = new ResourceTypeManager();
                ResourceType rsType = new ResourceType(code, "");
                rsTypeMan.Remove(rsType);
                CurrentFormState = FormState.Default;
            }
            catch (Exception ex)
            {

                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
        }

        private void LoadOperations(string resourceTypeCode)
        {
            try
            {
                OperationCategoryManager opCatMan = new OperationCategoryManager();
                OperationCategoryCollection opCats = opCatMan.GetAllOperationCategories();

                OperationManager opMan = new OperationManager();
                OperationCollection ops = opMan.FindOperationsByResourceType(resourceTypeCode);

                cbxListOperations.Items.Clear();
                foreach (OperationCategory opCat in opCats)
                {
                    ListItem cbxOperation = new ListItem(opCat.Name, opCat.OperationCode.ToString());
                    foreach (Operation op in ops)
                    {
                        if (op.OperationCode == opCat.OperationCode)
                        {
                            cbxOperation.Selected = true; 
                        }
                    }
                    cbxListOperations.Items.Add(cbxOperation);
                }               
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
        }

        protected void dgridRsType_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgridRsType.EditIndex = e.NewEditIndex;
            LoadResourceType();
        }

        protected void dgridRsType_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {         
            GridViewRow CurrentRow = (GridViewRow)dgridRsType.Rows[e.RowIndex];
            
            Label lblTypeCode = (Label)(CurrentRow.FindControl("lblTypeCode"));
            string editCode = lblTypeCode.Text.Trim();

            TextBox txtEditName = (TextBox)(CurrentRow.FindControl("txtEditName"));
            string newName = txtEditName.Text.Trim();

            UpdateResourceType(editCode, newName);

            dgridRsType.EditIndex = -1;
            LoadResourceType();                       
        }

        protected void dgridRsType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            dgridRsType.EditIndex = -1;
            LoadResourceType();
        }

        protected void lbtnDelete_Clicked(object sender, EventArgs e)
        {
            LinkButton lbtnDelete = (LinkButton)(sender);
            string deleteCode = lbtnDelete.CommandArgument;
            DeleteResourceType(deleteCode);            
            LoadResourceType();
        }

        protected void lbtnSetOperations_Clicked(object sender, EventArgs e)
        {
            LinkButton lbtnSetOperations = (LinkButton)(sender);
            string selectedTypeCode = lbtnSetOperations.CommandArgument;
            lblResourceType.Text = selectedTypeCode;
            LoadOperations(selectedTypeCode);
            pnlOperations.Visible = true;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                OperationManager opMan = new OperationManager();

                string resourceTypeCode = lblResourceType.Text;
                foreach (ListItem operation in cbxListOperations.Items)
                {
                    int operationCode = int.Parse(operation.Value);
                    if (operation.Selected)
                    {
                        opMan.Create(operationCode, resourceTypeCode, "");
                    }
                    else
                    {
                        Operation op = new Operation(operationCode, resourceTypeCode, "");
                        opMan.Remove(op);
                    }
                }

                ucNotifyBox.Message = "Cập nhật thao tác thành công.";
                CurrentFormState = FormState.NofityState;
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.ToString();
                CurrentFormState = FormState.ErrorState;
                this.SaveErrorLog(ex);
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            CurrentFormState = FormState.Default;
        }
    }
}
