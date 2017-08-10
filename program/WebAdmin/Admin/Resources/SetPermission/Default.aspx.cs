using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Inside.SecurityProviders;
using System.Collections;

namespace WebAdmin.Admin.Resources.SetPermission
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
            this.Token = "4336E033-865D-4E98-B1F8-33693CD67A90";
        }

        private void GetResourceDetail()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int rsID = int.Parse(Request["id"]);
                    ResourceManager rsMan = new ResourceManager();
                    Resource rs = rsMan.GetResource(rsID);

                    ApplicationManager appMan = new ApplicationManager();
                    Inside.SecurityProviders.Application app = appMan.GetApplication(rs.ApplicationID);

                    if (ViewState["ApplicationID"] != null)
                    {
                        ViewState["ApplicationID"] = rs.ApplicationID;
                    }
                    else
                    {
                        ViewState.Add("ApplicationID", rs.ApplicationID);
                    }

                    lblApplicationName.Text = app.Name;
                    lblResourceName.Text = rs.ResourceName;
                    lblResourceType.Text = rs.ResourceTypeCode;
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = "GetResourceDetail: " + ex.Message;
                CurrentFormState = FormState.ErrorState;
            }            
        }

        private void GetCurrentUsersAndRoles()
        {
            try
            {
                if (Request["id"] != null)
                {
                    int rsID = int.Parse(Request["id"]);
                    ResourceManager rsMan = new ResourceManager();
                    DataTable dtRet = rsMan.GetUsersAndRolesByResourceID(rsID);

                    if ((dtRet != null) && (dtRet.Rows.Count > 0))
                    {                        
                        //DataTable dt = (DataTable)(ViewState["dtOp"]);

                        lstCurrentUsers.Items.Clear();

                        lstCurrentUsers.SelectionMode = ListSelectionMode.Multiple;

                        foreach (DataRow dr in dtRet.Rows)
                        {
                            string valueText = dr["MemberID"].ToString();
                            string displayText = dr["MemberName"].ToString();
                            if (dr["MemberType"].ToString().ToLower() == "role")
                            {
                                displayText = "[" + displayText + "]";
                            }

                            //Add to DataTable
                            Table_AddMember(dr["MemberID"].ToString(), dr["MemberType"].ToString());

                            //Add to ListBox
                            ListItem item = new ListItem(displayText, valueText);
                            if (!lstCurrentUsers.Items.Contains(item))
                            {
                                lstCurrentUsers.Items.Add(item);
                            }                            
                            //lstCurrentUsers.SelectedIndex = 0;

                            //Set Operations columns in DataTable
                            DataTable dtOp = (DataTable)(ViewState["dtOp"]);
                            foreach (DataRow drOp in dtOp.Rows)
                            {
                                if ((drOp["MemberID"].ToString() == valueText) && (drOp["MemberType"].ToString() == dr["MemberType"].ToString()))
                                {
                                    drOp[dr["OperationCode"].ToString()] = bool.Parse(dr["Allow"].ToString()) ? "1" : "0";                                    
                                }
                            }
                            ViewState["dtOp"] = dtOp;
                        }

                        lstCurrentUsers.SelectionMode = ListSelectionMode.Single;                        
                        //lstCurrentUsers_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;                
            }
        }

        private void GetAllUsersAndRoles(int type)
        {
            /*
             * 0: All
             * 1: Users
             * 2: Roles
             * */
            try
            {
                UserManager userman = new UserManager();
                UserCollection users = new UserCollection();

                RoleManager roleMan = new RoleManager();
                RoleCollection roles = new RoleCollection();

                string keyWord = txtName.Text.Trim();
                int applicationID = Int32.Parse(ViewState["ApplicationID"].ToString());
                if (keyWord == "")
                {
                    users = userman.GetAllUsers();
                    roles = roleMan.GetRoleInApplication(applicationID);
                }
                else
                {
                    users = userman.FindUsersByUserName(keyWord);
                    roles = roleMan.FindRolesByCode(keyWord, applicationID);
                }

                lstNewUsers.Items.Clear();

                if ((type == 0) || (type == 1))
                {
                    foreach (Inside.SecurityProviders.User aUser in users)
                    {
                        ListItem item = new ListItem(aUser.UserName, aUser.UserName);
                        if (!lstCurrentUsers.Items.Contains(item))
                        {
                            lstNewUsers.Items.Add(item);
                        }
                    }    
                }

                if ((type == 0) || (type == 2))
                {
                    foreach (Role aRole in roles)
                    {
                        ListItem item = new ListItem("[" + aRole.RoleCode + "]", aRole.RoleID.ToString());
                        if (!lstCurrentUsers.Items.Contains(item))
                        {
                            lstNewUsers.Items.Add(item);
                        }
                    }
                }
                if (lstNewUsers.Items.Count > 0)
                    lstNewUsers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.ToString();
                CurrentFormState = FormState.ErrorState;
            }
        }

        private void GetOperations()
        {
            try
            {
                if (Request["id"] != null)
	            {
                    int resourceID = int.Parse(Request["id"]);
            	    OperationManager opMan = new OperationManager();
                    OperationCollection ops = opMan.FindOperationsByResources(resourceID);

                    cbxListOperation.Items.Clear();
                    foreach (Operation op in ops)
                    {
                        OperationCategoryManager opCatMan = new OperationCategoryManager();
                        OperationCategory opCat = opCatMan.GetOperationCategory(op.OperationCode);
                        ListItem cbxOperation = new ListItem(opCat.Name, op.OperationCode.ToString());
                        ListItem cbxDenyOperation = new ListItem(opCat.Name, op.OperationCode.ToString());
                        cbxListOperation.Items.Add(cbxOperation);
                        cbxListDenyOperation.Items.Add(cbxDenyOperation);
                    }
	            }               
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.Message;
                CurrentFormState = FormState.ErrorState;
            }
        }

        private void SavePermission()
        {
            try
            {
                PermissionManager permissionMan = new PermissionManager();

                int resourceID = Int32.Parse(Request["id"]);
                int operationCode = -1;
                string objectName = "";
                int objectType = 0;
                bool allow = false;

                DataTable dt = (DataTable)(ViewState["dtOp"]);

                foreach (DataRow dr in dt.Rows)
                {
                    objectName = dr["MemberID"].ToString();
                    objectType = dr["MemberType"].ToString().ToLower() == "user" ? 0 : 1;

                    //Remove selected user/role from this Resource
                    permissionMan.RemoveMemberFromResource(resourceID, objectName, objectType);                    

                    for (int i = 2; i < dt.Columns.Count; i++)
                    {
                        operationCode = Int32.Parse(dt.Columns[i].ColumnName);
                        if (dr[i].ToString() != "-1")
                        {
                            allow = dr[i].ToString() == "1" ? true : false;
                            permissionMan.SetPermission(resourceID, operationCode, objectName, objectType, allow);
                        }
                    }
                    CurrentFormState = FormState.NofityState;
                    ucNotifyBox.Message = "Phân quyền thành công.";
                }                
            }
            catch (Exception ex)
            {
                ucErrorBox.Message = ex.ToString() ;
                CurrentFormState = FormState.ErrorState;
            }            
        }

        #region DataTable

        private void Table_SetOperations(string memberID, string memberType)
        {
            DataTable dt = (DataTable)(ViewState["dtOp"]);

            //Find data row
            DataRow dr = dt.NewRow();
            foreach (DataRow aRow in dt.Rows)
            {
                if ((aRow["MemberID"].ToString() == memberID) && (aRow["MemberType"].ToString() == memberType))
                {
                    dr = aRow;
                    break;
                }
            }

            foreach (ListItem operationItem in cbxListDenyOperation.Items)
            {
                dr[operationItem.Value] = -1;
            }

            //Update values            
            foreach (ListItem denyItem in cbxListDenyOperation.Items)
            {                
                if (denyItem.Selected)
                {
                    dr[denyItem.Value] = 0;
                }
            }

            foreach (ListItem allowItem in cbxListOperation.Items)
            {                
                if (allowItem.Selected)
                {
                  dr[allowItem.Value] = 1;
                }
            }

            //Save data table
            ViewState["dtOp"] = dt;
        }

        private bool Table_MemberExists(string memberID, string memberType)
        {
            bool isExists = false;

            DataTable dt = (DataTable)(ViewState["dtOp"]);
            foreach (DataRow dr in dt.Rows)
            {
                if ((dr["MemberID"].ToString() == memberID) && (dr["MemberType"].ToString() == memberType))
                {
                    isExists = true;
                    break;
                }
            }

            return isExists;
        }
               
        private void Table_AddMember(string memberID, string memberType)
        {
            if (!Table_MemberExists(memberID, memberType))
            {
                DataTable dt = (DataTable)(ViewState["dtOp"]);
                DataRow dr = dt.NewRow();

                dr["MemberID"] = memberID;
                dr["MemberType"] = memberType;

                foreach (ListItem item in cbxListOperation.Items)
                {
                    dr[item.Value] = -1;
                }

                dt.Rows.Add(dr);
                ViewState["dtOp"] = dt;
            }
        }

        private void MakeOperationsTable()
        {
            DataTable dtUserOperations = new DataTable();
            dtUserOperations.Columns.Add("MemberID");
            dtUserOperations.Columns.Add("MemberType");
            foreach (ListItem item in cbxListOperation.Items)
            {
                dtUserOperations.Columns.Add(item.Value);
            }

            if (ViewState["dtOp"] != null)
            {
                ViewState["dtOp"] = dtUserOperations;
            }
            else
            {
                ViewState.Add("dtOp", dtUserOperations);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                //btnSave.Enabled = false;
                ((ContentPage)Master).SetFormTitle("Phân quyền cho tài nguyên.");

                GetResourceDetail();
                GetOperations();
                MakeOperationsTable();
                GetCurrentUsersAndRoles();
                if (lstCurrentUsers.Items.Count > 0)
                {
                    lstCurrentUsers.SelectedIndex = 0;
                    ListItem selectedMember = lstCurrentUsers.SelectedItem;
                    lstCurrentUsers.Focus();
                    string memberID = selectedMember.Value;
                    string memberType = ((selectedMember.Text.StartsWith("[")) && (selectedMember.Text.EndsWith("]"))) ? "role" : "user";
                    SetCheckBoxList(memberID, memberType);
                }

                CurrentFormState = FormState.Default;
            }
        }

        protected void btnBrowse_Click(object sender, EventArgs e)
        {
            pnlAddUser.Visible = true;
            GetAllUsersAndRoles(int.Parse(ddlType.SelectedValue));
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlAddUser.Visible = false;
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllUsersAndRoles(int.Parse(ddlType.SelectedValue));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {            
            lstCurrentUsers.SelectionMode = ListSelectionMode.Multiple;

            ArrayList selectedUsers = new ArrayList();
            ArrayList selectedRoles = new ArrayList();

            foreach (ListItem item in lstNewUsers.Items)
            {
                if (item.Selected)
                {
                    if ((item.Text.StartsWith("[")) && (item.Text.EndsWith("]")))
                    {
                        selectedRoles.Add(item);
                    }
                    else
                    {
                        selectedUsers.Add(item);
                    }
                }
            }

            foreach (object selectedUser in selectedUsers)
            {
                ListItem newUser = (ListItem)(selectedUser);

                //Add to data table
                Table_AddMember(newUser.Value, "user");

                //Add to ListBox
                lstCurrentUsers.Items.Add(newUser);

                //Remove from New Listbox
                lstNewUsers.Items.Remove(newUser);
            }

            foreach (object selectedRole in selectedRoles)
            {
                ListItem newRole = (ListItem)(selectedRole);

                //Add to data table
                Table_AddMember(newRole.Value, "role");

                //Add to ListBox
                lstCurrentUsers.Items.Add(newRole);

                //Remove from New ListBox
                lstNewUsers.Items.Remove(newRole);
            }

            if (lstCurrentUsers.Items.Count > 0)
            {
                lstCurrentUsers.SelectedIndex = 0;
                lstCurrentUsers.SelectionMode = ListSelectionMode.Single;
            }        
        }

        protected void btn_Click(object sender, EventArgs e)
        {            
            DataTable dt = (DataTable)(ViewState["dtOp"]);
            gv.DataSource = dt;
            gv.DataBind();

            DataTable dtRemove = (DataTable)(ViewState["dtRemove"]);
            gvRemove.DataSource = dtRemove;
            gvRemove.DataBind();
        }

        protected void cbxAllOperation_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem operation in cbxListOperation.Items)
            {
                operation.Selected = cbxAllOperation.Checked;
                cbxListOperation_SelectedIndexChanged(sender, e);
            }
        }

        protected void lstCurrentUsers_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ListItem selectedMember = lstCurrentUsers.SelectedItem;            
            string memberID = selectedMember.Value;
            string memberType = ((selectedMember.Text.StartsWith("[")) && (selectedMember.Text.EndsWith("]"))) ? "role" : "user";
            SetCheckBoxList(memberID, memberType);
            btnSave.Enabled = true;
        }

        private void SetCheckBoxList(string memberID, string memberType)
        {
            DataTable dt = (DataTable)(ViewState["dtOp"]);

            //Find data row
            DataRow dr = dt.NewRow();
            foreach (DataRow aRow in dt.Rows)
            {
                if ((aRow["MemberID"].ToString() == memberID) && (aRow["MemberType"].ToString() == memberType))
                {
                    dr = aRow;
                    break;
                }
            }

            //Set the operation checkboxes
            foreach (ListItem cbx in cbxListOperation.Items)
            {
                //cbx.Selected = dr[cbx.Value].ToString() == "1" ? true : false;
                string strAllow = dr[cbx.Value].ToString();
                switch (strAllow)
                {
                    case "-1":
                        cbxListOperation.Items.FindByValue(cbx.Value).Selected = false;
                        cbxListDenyOperation.Items.FindByValue(cbx.Value).Selected = false;
                        break;
                    case "0":
                        cbxListOperation.Items.FindByValue(cbx.Value).Selected = false;
                        cbxListDenyOperation.Items.FindByValue(cbx.Value).Selected = true;
                        break;
                    case "1":
                        cbxListOperation.Items.FindByValue(cbx.Value).Selected = true;
                        cbxListDenyOperation.Items.FindByValue(cbx.Value).Selected = false;
                        break;
                    default:
                        break;
                }
            }
            /*
            foreach (ListItem cbxDeny in cbxListDenyOperation.Items)
            {
                cbxDeny.Selected = dr[cbxDeny.Value].ToString() == "0" ? true : false;
            }
            */
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {            
            GetAllUsersAndRoles(Int32.Parse(ddlType.SelectedValue));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavePermission();
        }

        protected void cbxListOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                int idx = 0;
                foreach (ListItem item in cbxListOperation.Items)
                {
                    if (item.Selected)
                        cbxListDenyOperation.Items[idx].Selected = false;
                    idx++;
                }

                ListItem selectedMember = lstCurrentUsers.SelectedItem;
                string memberID = selectedMember.Value;
                string memberType = ((selectedMember.Text.StartsWith("[")) && (selectedMember.Text.EndsWith("]"))) ? "role" : "user";
                Table_SetOperations(memberID, memberType);
            }
        }

        protected void cbxListDenyOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                int idx = 0;
                foreach (ListItem item in cbxListDenyOperation.Items)
                {
                    if (item.Selected)
                        cbxListOperation.Items[idx].Selected = false;
                    idx++;
                }

                ListItem selectedMember = lstCurrentUsers.SelectedItem;
                string memberID = selectedMember.Value;
                string memberType = ((selectedMember.Text.StartsWith("[")) && (selectedMember.Text.EndsWith("]"))) ? "role" : "user";
                Table_SetOperations(memberID, memberType);
            }
        }
        
        protected void btnNext_Click(object sender, EventArgs e)
        {            
            Response.Redirect("../Manage/");
        }
    }
}
