using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Inside.SecurityProviders;
using Inside.DataProviders;
using System.Xml;
namespace WebAdmin.Admin.Menus.Manage
{
    public partial class Default : Base.BasePage
    {
        #region Members
            public enum FormState
            { 
                MenuTreeSelectedChanged,
                BeforeAddNewResource,
                AfterAddNewResource,
                CancelAddNewResource,
                BeforeSetPermission,
                AfterSetPermission,
                BeforeAddNewMenu,
                AfterAddNewMenu,
                Error,
                Notify,
                Default
            }

            private FormState m_State = FormState.Default;
        #endregion

        #region Properties
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
                            multView.SetActiveView(multView.Views[0]);
                            this.pnlError.Visible = false;
                            this.pnlNotify.Visible = false;
                            break;
                        case FormState.BeforeSetPermission:
                            multView.SetActiveView(multView.Views[3]);
                            break;
                        case FormState.Error:
                            pnlError.Visible = true;
                            pnlNotify.Visible = false;
                            break;
                        case FormState.Notify:
                            pnlError.Visible = false;
                            pnlNotify.Visible = true;
                            break;
                        case FormState.AfterSetPermission:
                            pnlError.Visible = false;
                            pnlNotify.Visible = false;                            
                            break;
                    }
                }
            }

            private string CollectUserPermissionData(string strUserName, int iResourceID)
            {
                try
                {
                    int idx = 0;
                        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
                        XmlWriterSettings xwSet = new XmlWriterSettings();
                        xwSet.OmitXmlDeclaration = true;
                        XmlWriter xw = XmlWriter.Create(strBuilder, xwSet);
                        xw.WriteStartElement("Permissions");
                        foreach (ListItem item in cbx_SetPermission_ListOperation.Items)
                        {
                            if(item.Selected)
                            {

                                xw.WriteStartElement("Permission");
                                xw.WriteAttributeString("UserName", strUserName);
                                xw.WriteAttributeString("ResourceID", iResourceID.ToString());
                                xw.WriteAttributeString("OperationCode", item.Value);
                                xw.WriteAttributeString("IsAllowed", "1");
                                xw.WriteEndElement();
                            }
                            if(cbx_SetPermission_ListDenyOperation.Items[idx].Selected)
                            {

                                xw.WriteStartElement("Permission");
                                xw.WriteAttributeString("UserName", strUserName);
                                xw.WriteAttributeString("ResourceID", iResourceID.ToString());
                                xw.WriteAttributeString("OperationCode", cbx_SetPermission_ListDenyOperation.Items[idx].Value);
                                xw.WriteAttributeString("IsAllowed", "0");
                                xw.WriteEndElement();
                            }
                            idx++;
                        }
                        xw.WriteEndElement();
                        xw.Flush();
                        xw.Close();
                        return strBuilder.ToString();
                }
                catch (Exception ex)
                {throw ex;}
            }
            private string CollectRolePermissionData(string strRoleCode, int iResourceID)
            {
                try
                {
                    System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
                    XmlWriterSettings xwSet = new XmlWriterSettings();
                    xwSet.OmitXmlDeclaration = true;
                    XmlWriter xw = XmlWriter.Create(strBuilder, xwSet);
                    xw.WriteStartElement("Permissions");
                    foreach (ListItem item in cbx_SetPermission_ListOperation.Items)
                    {
                        if (item.Selected)
                        {
                            xw.WriteStartElement("Permission");
                            xw.WriteAttributeString("RoleCode", strRoleCode);
                            xw.WriteAttributeString("ResourceID", iResourceID.ToString());
                            xw.WriteAttributeString("OperationCode", item.Value);
                            xw.WriteAttributeString("IsAllowed", "1");
                            xw.WriteEndElement();
                        }
                    }
                    xw.WriteEndElement();
                    xw.Flush();
                    xw.Close();
                    return strBuilder.ToString();
                }
                catch (Exception ex)
                { throw ex; }
            }
        #endregion

        #region Methods

            private void LoadResourceToDropdownlist(int ApplicationID, int CurrentResourceID, DropDownList ddl)
            {
                try
                {
                    ResourceCollection reSrcCol = new ResourceCollection();
                    ResourceManager reSrcMan = new ResourceManager();
                    reSrcCol = reSrcMan.GetAllOrphanResource(ApplicationID);

                    Resource currentResource = reSrcMan.GetResource(CurrentResourceID);
                    reSrcCol.Add(currentResource);

                    ddl.DataSource = reSrcCol;
                    ddl.DataTextField = "ResourceName";
                    ddl.DataValueField = "ResourceID";
                    ddl.DataBind();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }

            private void LoadResourceToDropdownlist(int ApplicationID, DropDownList ddl)
            {
                try
                {
                    ResourceCollection reSrcCol = new ResourceCollection();
                    ResourceManager reSrcMan = new ResourceManager();
                    reSrcCol = reSrcMan.GetAllOrphanResource(ApplicationID);
                    ddl.DataSource = reSrcCol;
                    ddl.DataTextField = "ResourceName";
                    ddl.DataValueField = "ResourceID";
                    ddl.DataBind();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }
            private void LoadApplications()
            {
                try
                {
                    ApplicationManager appManObj = new ApplicationManager();
                    ApplicationCollection appColObj = new ApplicationCollection();
                    appColObj = appManObj.GetAllApplications();
                    this.ddlApplication.DataSource = appColObj;
                    this.ddlApplication.DataTextField = "Name";
                    this.ddlApplication.DataValueField = "ApplicationID";
                    this.ddlApplication.DataBind();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }

            private void LoadResourceType(DropDownList ddl)
            {
                try
                {
                    ResourceTypeManager reSrcTypeMan = new ResourceTypeManager();
                    ResourceTypeCollection reSrcTypeCol = new ResourceTypeCollection();
                    reSrcTypeCol = reSrcTypeMan.GetAllResourceTypes();
                    ddl.DataSource = reSrcTypeCol;
                    ddl.DataTextField = "Name";
                    ddl.DataValueField = "ResourceTypeCode";
                    ddl.DataBind();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }


            private void LoadMenuItemToDropdownlist(int ApplicationID, int CurrentMenuItemID, DropDownList ddlMenuItem, DropDownList ddlResource)
            {
                try
                {
                    Inside.SecurityProviders.MenuItem mnuCurrentItem;
                    
                    Inside.SecurityProviders.MenuItemCollection mnuItemCol = new Inside.SecurityProviders.MenuItemCollection();
                    Inside.SecurityProviders.MenuManager mnuItemMan = new Inside.SecurityProviders.MenuManager();
                    mnuItemCol = mnuItemMan.GetApplicationMenus(ApplicationID);

                    mnuCurrentItem = mnuItemCol.Find(CurrentMenuItemID);

                    mnuItemCol.Remove(mnuCurrentItem);
                    if (mnuCurrentItem.ParentMenuID != 0)
                    {
                        ddlMenuItem.DataSource = mnuItemCol;
                        ddlMenuItem.DataTextField = "FunctionName";
                        ddlMenuItem.DataValueField = "MenuID";
                        ddlMenuItem.DataBind();
                    }
                    else
                    {
                        ddlMenuItem.Items.Clear();
                    }
                    ddlMenuItem.SelectedValue = mnuCurrentItem.ParentMenuID.ToString();
                    LoadResourceToDropdownlist(ApplicationID, mnuCurrentItem.ResourceID, ddlResource);
                    ddlResource.SelectedValue = mnuCurrentItem.ResourceID.ToString();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }

            private void LoadMenuItemToDropdownlist(int ApplicationID, DropDownList ddlMenuItem)
            {
                try
                {
                    Inside.SecurityProviders.MenuItemCollection mnuItemCol = new Inside.SecurityProviders.MenuItemCollection();
                    Inside.SecurityProviders.MenuManager mnuItemMan = new Inside.SecurityProviders.MenuManager();
                    mnuItemCol = mnuItemMan.GetApplicationMenus(ApplicationID);

                        ddlMenuItem.DataSource = mnuItemCol;
                        ddlMenuItem.DataTextField = "FunctionName";
                        ddlMenuItem.DataValueField = "MenuID";
                        ddlMenuItem.DataBind();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }
            private void LoadTreeViewMenu(int ApplicationID)
            {
                try
                {
                    Inside.SecurityProviders.MenuItemCollection mnuColection = new Inside.SecurityProviders.MenuItemCollection();
                    MenuManager mnuManager = new MenuManager();
                    mnuColection = mnuManager.GetApplicationMenus(ApplicationID);
                    this.xmlSrc.Data = mnuColection.ToString();
                    this.xmlSrc.DataBind();
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }

            private void ResetForm()
            {
                this.multView.SetActiveView(this.multView.Views[0]);
                this.ddl_MenuDetail_ParentMenu.Items.Clear();
                this.ddl_MenuDetail_Resource.Items.Clear();
                this.SwitchButton(false);
            }

            private void SwitchButton(bool bIsAddNewMenuItem)
            {
                btn_MenuDetail_AddNewMenu.Visible = !bIsAddNewMenuItem;
                btn_MenuDetail_Update.Visible = !bIsAddNewMenuItem;
                btn_MenuDetail_Delete.Visible = !bIsAddNewMenuItem;
                btn_MenuDetail_SetPermisson.Visible = !bIsAddNewMenuItem;
                btn_MenuDetail_SaveNewMenuItem.Visible = bIsAddNewMenuItem;
            }
         
            private Resource CreateNewResource(int iApplicationID, string strResourceTypeCode, string strResourceName, string strPath, string strFileName,
                                                 string strLink, bool bStatus, bool IsParent, string strToken)
            {
                Resource reSrc = null;
                try
                {
                    ResourceManager rsMan = new ResourceManager();
                    
                    reSrc = rsMan.Create(strResourceTypeCode, strPath, strFileName, strLink, iApplicationID, strResourceName, bStatus, IsParent, strToken);
                    if (reSrc != null)
                        this.SaveActionLog("Add new resource", reSrc.ResourceID.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return reSrc;
            }

            private void UpdateMenuItem(int iMenuItemID, string strDisplayName, int iResourceID, int iParentMenuID)
            {
                try
                {
                    Inside.SecurityProviders.MenuManager mnuItemMan = new MenuManager();
                    mnuItemMan.Update(iMenuItemID, strDisplayName, iResourceID, iParentMenuID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private void TreeviewMenu_SelectedNode(int iMenuID)
            {
                foreach (TreeNode node in tvwMenu.Nodes)
                {
                    if(node.Value == iMenuID.ToString())
                    {
                        //node.Select();
                        //break;
                    }
                }
            }

            private void LoadMenuItemInfo(int iMenuID, ref int iResourceID)
            {
                try
                {
                        MenuManager mnuMan = new MenuManager();
                        DataTable dtMenuItemInfo = mnuMan.GetMenuItem(iMenuID);
                        if (dtMenuItemInfo.Rows.Count == 0) { throw new Exception("Menu Item cannot be found"); }
                        //this.lbl_SetPermission_ApplicationName.Text = dtMenuItemInfo.Rows[0]["ApplicationName"].ToString();
                        this.lbl_SetPermission_ResourceName.Text = dtMenuItemInfo.Rows[0]["ResourceName"].ToString(); ;
                        this.lbl_SetPermission_ResourceType.Text = dtMenuItemInfo.Rows[0]["ResourceTypeCode"].ToString();
                        iResourceID = int.Parse(dtMenuItemInfo.Rows[0]["ResourceID"].ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private void GetCurrentUsersAndRoles(int iResourceID)
            {
                try
                {
                    ResourceManager rsMan = new ResourceManager();
                    //DataTable dtRet = rsMan.GetRolesUsersByResource(iResourceID);
                    DataTable dtRet = rsMan.GetAllRolesUsersByResource(iResourceID);
                    lst_SetPermission_CurrentUsers.Items.Clear();
                    if ((dtRet != null) && (dtRet.Rows.Count > 0))
                    {
                        foreach (DataRow row in dtRet.Rows)
                        {
                            ListItem lstItem = new ListItem();

                            if (row["Type"].ToString() == "Role")
                            {
                                lstItem.Text = row["RoleCode"].ToString();
                                lstItem.Value = row["Type"].ToString() + "$" + row["RoleCode"].ToString(); 
                            }
                            else
                            {
                                //lstItem.Text = row["FullName"].ToString();
                                lstItem.Text = row["UserName"].ToString();
                                lstItem.Value = row["Type"].ToString() + "$" + row["UserName"].ToString(); 
                            }
                            lst_SetPermission_CurrentUsers.Items.Add(lstItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private void GetOperations(int resourceID)
            {
                try
                {                    
                    OperationManager opMan = new OperationManager();
                    OperationCollection ops = opMan.FindOperationsByResources(resourceID);

                    cbx_SetPermission_ListOperation.Items.Clear();
                    cbx_SetPermission_ListDenyOperation.Items.Clear();
                    foreach (Operation op in ops)
                    {
                        OperationCategoryManager opCatMan = new OperationCategoryManager();
                        OperationCategory opCat = opCatMan.GetOperationCategory(op.OperationCode);
                        ListItem cbxOperation = new ListItem(opCat.Name, op.OperationCode.ToString());
                        ListItem cbxDenyOperation = new ListItem(opCat.Name, op.OperationCode.ToString());
                        cbx_SetPermission_ListOperation.Items.Add(cbxOperation);
                        cbx_SetPermission_ListDenyOperation.Items.Add(cbxDenyOperation);
                    }
                }
                catch (Exception ex)
                {
                    this.SaveErrorLog(ex);
                }
            }

            private void GetOperation(string ResourceTypeCode)
            {                
                try
                {
                    OperationCategoryManager opMan = new OperationCategoryManager();
                    OperationCategoryCollection opCol = new OperationCategoryCollection();
                    opCol = opMan.GetOperationCategoriesByResourceTypeCode(ResourceTypeCode);
                    Response.Write("[" + ResourceTypeCode + "]<br />");
                    Response.Write("[" + opCol.Count.ToString()  + "]");
                    if(opCol.Count > 0)
                    {
                        cbx_SetPermission_ListOperation.DataSource = opCol;
                        cbx_SetPermission_ListOperation.DataTextField = "Name";
                        cbx_SetPermission_ListOperation.DataValueField = "OperationCode";
                        cbx_SetPermission_ListOperation.DataBind();

                        cbx_SetPermission_ListDenyOperation.DataSource = opCol;
                        cbx_SetPermission_ListDenyOperation.DataTextField = "Name";
                        cbx_SetPermission_ListDenyOperation.DataValueField = "OperationCode";
                        cbx_SetPermission_ListDenyOperation.DataBind();
                        //foreach (OperationCategory item in opCol)
                        //{
                        //    cbx_SetPermission_ListOperation.Items.Add(new ListItem(item.OperationCode.ToString(), item.Name));
                        //}
                    }
                }
                catch (Exception ex)
                {                    
                    this.SaveErrorLog(ex);
                }
            }

            private PermissionCollection GetPermissionOnResourceByUserRole(int iResourceID, string strUserName, string strRoleCode, bool bIsAllowed)
            {
                try
                {
                    PermissionManager perMan = new PermissionManager();
                    PermissionCollection perCol = new PermissionCollection();
                    perCol = perMan.GetPermissionOnResourceByUserRole(iResourceID, strUserName, strRoleCode, bIsAllowed);
                    return perCol;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private UserCollection FindUserByUserName(string strUserName)
            {
                try
                {
                    UserManager usrMan = new UserManager();
                    UserCollection usrCol = new UserCollection();
                    usrCol = usrMan.FindUsersByUserName(strUserName);
                    return usrCol;
                }
                catch (Exception ex)
                { throw new Exception("FindUserByUserName" + ex.Message); }
                //{ throw ex; }
            }
            private RoleCollection FindRoleByRoleCode(string strRoleCode)
            {
                try
                {
                    RoleManager roleMan = new RoleManager();
                    RoleCollection roleCol = new RoleCollection();
                    roleCol = roleMan.FindRolesByCode(strRoleCode, int.Parse(ddlApplication.SelectedValue));
                    return roleCol;
                }
                catch (Exception ex)
                { throw new Exception("FindRoleByRoleCode" + ex.Message); }
                //{ throw ex; }
            }
        #endregion

        #region Events
            #region Page

                protected void Page_Init(object sender, EventArgs e)
                {
                    this.Token = "18A1C5D8-7FF7-4126-937E-572BDC508C47";
                }


                protected void Page_Load(object sender, EventArgs e)
                {
                    this.ddlApplication.SelectedIndexChanged += new EventHandler(ddlApplication_SelectedIndexChanged);
                    this.btn_MenuDetail_AddNewResource.Click += new EventHandler(btn_MenuDetail_AddNewResource_Click);
                    this.btn_AddResource_AddResource.Click += new EventHandler(btn_AddResource_AddResource_Click);
                    this.tvwMenu.SelectedNodeChanged += new EventHandler(tvwMenu_SelectedNodeChanged);
                    this.cbx_AddResource_IsParent.CheckedChanged += new EventHandler(cbx_AddResource_IsParent_CheckedChanged);
                    this.btn_MenuDetail_AddNewMenu.Click += new EventHandler(btn_MenuDetail_AddNewMenu_Click);
                    this.btn_MenuDetail_SaveNewMenuItem.Click += new EventHandler(btn_MenuDetail_SaveNewMenuItem_Click);
                    this.btn_MenuDetail_SetPermisson.Click += new EventHandler(btn_MenuDetail_SetPermisson_Click);
                    this.btn_MenuDetail_Delete.Click += new EventHandler(btn_MenuDetail_Delete_Click);
                    this.btn_MenuDetail_Update.Click += new EventHandler(btn_MenuDetail_Update_Click);
                    this.btn_SetPermission_Browse.Click  += new EventHandler(btn_SetPermission_Browse_Click);
                    this.btn_SetPermission_Close.Click += new EventHandler(btn_SetPermission_Close_Click);
                    this.lst_SetPermission_CurrentUsers.SelectedIndexChanged += new EventHandler(lst_SetPermission_CurrentUsers_SelectedIndexChanged);
                    this.btn_SetPermission_Find.Click += new EventHandler(btn_SetPermission_Find_Click);
                    this.btn_SetPermission_Add.Click += new EventHandler(btn_SetPermission_Add_Click);
                    this.cbx_SetPermission_ListDenyOperation.SelectedIndexChanged += new EventHandler(cbx_SetPermission_ListDenyOperation_SelectedIndexChanged);
                    this.cbx_SetPermission_ListOperation.SelectedIndexChanged += new EventHandler(cbx_SetPermission_ListOperation_SelectedIndexChanged);
                    this.btn_SetPermission_Accept.Click += new EventHandler(btn_SetPermission_Accept_Click);
                    this.cbx_SetPermission_AllOperation.CheckedChanged += new EventHandler(cbx_SetPermission_AllOperation_CheckedChanged);

                    if (!this.Page.IsPostBack)
                    {
                        ((ContentPage)Master).SetFormTitle("Danh sách menu");
                        CurrentFormState = FormState.Default;
                        //Load danh sách các ứng dụng
                        this.LoadApplications();
                        ddlApplication.SelectedValue = "10000016";    //inside system
                        this.LoadTreeViewMenu(int.Parse(ddlApplication.SelectedValue));
                    }
                    else
                    {
                        int applicationID = int.Parse(ddlApplication.SelectedValue);
                        if (ViewState["ApplicationID"] != null)
                            applicationID = int.Parse(ViewState["ApplicationID"].ToString());

                        ddlApplication.SelectedValue = applicationID.ToString();
                    }
                }

                private void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
                {
                    if (ViewState["ApplicationID"] != null)
                        ViewState["ApplicationID"] = ddlApplication.SelectedValue;
                    else
                        ViewState.Add("ApplicationID", ddlApplication.SelectedValue);
                    this.LoadTreeViewMenu(int.Parse(ddlApplication.SelectedValue));
                    this.lst_SetPermission_NewUsers.Items.Clear();
                    this.ResetForm();
                }
                private void tvwMenu_SelectedNodeChanged(object sender, EventArgs e)
                {
                    multView.SetActiveView(viewStateMenuDetail);
                    lbl_MenuDetail_MenuID.Text = tvwMenu.SelectedNode.Value;
                    tbx_MenuDetail_DisplayName.Text = tvwMenu.SelectedNode.Text;
                    LoadMenuItemToDropdownlist(int.Parse(ddlApplication.SelectedValue), int.Parse(tvwMenu.SelectedNode.Value), ddl_MenuDetail_ParentMenu, ddl_MenuDetail_Resource);
                    this.SwitchButton(false);
                    this.lst_SetPermission_NewUsers.Items.Clear();
                }

                protected void btnMoveMenu_Click(object sender, EventArgs e)
                {
                    try
                    {
                        if (tvwMenu.SelectedNode != null)
                        {
                            int menuID = int.Parse(tvwMenu.SelectedNode.Value);
                            int direction = 1;
                            int step = int.Parse(txtMoveStep.Text.Trim());
                            if (step < 0) direction = 0;

                            SQLDatabase objSQL = new SQLDatabase(ConfigurationManager.AppSettings["AUTHDB_CONN"]);

                            SqlParameter paramMenuID = new SqlParameter("@MenuID", menuID);
                            SqlParameter paramDirection = new SqlParameter("@Direction", direction);
                            SqlParameter paramStep = new SqlParameter("@Step", Math.Abs(step));

                            if (objSQL.ExecuteNonQuery("UspMoveMenu_Step", CommandType.StoredProcedure, paramMenuID, paramDirection, paramStep) > 0)
                            {
                                ddlApplication_SelectedIndexChanged(null, null);
                            }
                            else
                            {
                                ucEBX.Message = "Không sắp xếp được menu.";
                                CurrentFormState = FormState.Error;
                            }
                        }
                        else
                        {
                            ucEBX.Message = "Chưa chọn menu.";
                            CurrentFormState = FormState.Error;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.SaveErrorLog(ex);
                        ucEBX.Message = "Có lỗi khi sắp xếp menu.";
                        CurrentFormState = FormState.Error;
                    }
                }

                //protected void ibtnUp_Click(object sender, ImageClickEventArgs e)
                //{
                //    if (tvwMenu.SelectedNode != null)
                //    {
                //        try
                //        {
                //            int MenuID = Int32.Parse(tvwMenu.SelectedNode.Value);
                //            MenuManager menuMan = new MenuManager();
                //            menuMan.MoveMenu(MenuID, 1);
                //            ddlApplication_SelectedIndexChanged(null, null);
                //            CurrentFormState = FormState.Default;
                //        }
                //        catch (Exception ex)
                //        {
                //            this.SaveErrorLog(ex);
                //            ucEBX.Message = "Có lỗi xảy ra. Không sắp xếp được menu.";
                //            CurrentFormState = FormState.Error;
                //        }                        
                //    }
                //}

                //protected void ibtnDown_Click(object sender, ImageClickEventArgs e)
                //{
                //    {
                //        try
                //        {
                //            int MenuID = Int32.Parse(tvwMenu.SelectedNode.Value);
                //            MenuManager menuMan = new MenuManager();
                //            menuMan.MoveMenu(MenuID, 0);
                //            ddlApplication_SelectedIndexChanged(null, null);
                //            CurrentFormState = FormState.Default;
                //        }
                //        catch (Exception ex)
                //        {
                //            this.SaveErrorLog(ex);
                //            ucEBX.Message = "Có lỗi xảy ra. Không sắp xếp được menu.";
                //            CurrentFormState = FormState.Error;
                //        }
                //    }
                //}
            #endregion

            #region viewStateMenuDetail
                private void btn_MenuDetail_AddNewResource_Click(object sender, EventArgs e)
                {
                    this.LoadResourceType(ddl_AddResource_ResourceType);
                    multView.SetActiveView(viewStateAddResource);
                    this.cbx_AddResource_IsParent.Checked = false;
                    this.txt_AddResource_ResourceName.Text = "";
                    this.txt_AddResource_Path.Text = "";
                    this.txt_AddResource_FileName.Text = "";
                    this.txt_AddResource_Link.Text = "";
                    this.txt_AddResource_Token.Text = "";
                }
                private void btn_MenuDetail_AddNewMenu_Click(object sender, EventArgs e)
                {
                    //an cac button, chi hien button btnSaveNewMenuItem
                    this.SwitchButton(true);

                    //chọn menuItem hien tai trong ddlParentMenuItem

                    //clear lblMenuID
                    
                    LoadResourceToDropdownlist(int.Parse(ddlApplication.SelectedValue), ddl_MenuDetail_Resource);
                    //thiet lap menu Item dang duoc chon lam menu cha cho menu moi them vao (mac dinh)
                    LoadMenuItemToDropdownlist(int.Parse(ddlApplication.SelectedValue), ddl_MenuDetail_ParentMenu);
                    ddl_MenuDetail_ParentMenu.SelectedValue = lbl_MenuDetail_MenuID.Text;// tvwMenu.SelectedNode.Value;
                    lbl_MenuDetail_MenuID.Text = "";
                    tbx_MenuDetail_DisplayName.Text = "";
                }
            
                
                private void btn_MenuDetail_Delete_Click(object sender, EventArgs e)
                {
                    try
                    {
                        //xoa menu
                        int iResult;
                        MenuManager mnuMan = new MenuManager();
                        iResult = mnuMan.Remove(int.Parse(lbl_MenuDetail_MenuID.Text.Trim()));
                        if (iResult == 0)
                        {
                            //load lai treeview
                            LoadTreeViewMenu(int.Parse(ddlApplication.SelectedValue));
                        }
                        else
                        {
                            this.ucEBX.Message = "Bạn phải xóa menu con trước.";
                            this.CurrentFormState = FormState.Error;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }
                
                private void btn_MenuDetail_SaveNewMenuItem_Click(object sender, EventArgs e)
                {
                    try
                    {
                        //thuc hien them menu item
                        MenuManager mnuItemMan = new MenuManager();
                        int iMenuID = mnuItemMan.Create(int.Parse(ddl_MenuDetail_Resource.SelectedValue.ToString())
                                                        , tbx_MenuDetail_DisplayName.Text.Trim(), int.Parse(ddl_MenuDetail_ParentMenu.SelectedValue.ToString()));
                        //load lai treeview
                        LoadTreeViewMenu(int.Parse(ddlApplication.SelectedValue));
                        //chon menuitem item vua them vao
                        TreeviewMenu_SelectedNode(iMenuID);
                        //hien lai cac button Update, Delete... Ẩn button btnSaveNewMenuItem
                        this.lbl_MenuDetail_MenuID.Text = iMenuID.ToString();
                        this.SwitchButton(false);
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }
                
                private void btn_MenuDetail_Update_Click(object sender, EventArgs e)
                {
                    try
                    {
                        //cap nhat thong tin

                        int menuID  = int.Parse(lbl_MenuDetail_MenuID.Text.Trim());
                        string displayName = tbx_MenuDetail_DisplayName.Text.Trim();
                        int resourceID = int.Parse(ddl_MenuDetail_Resource.SelectedValue);
                        int parentID;
                        if (ddl_MenuDetail_ParentMenu.SelectedItem != null)
                        {
                            parentID = int.Parse(ddl_MenuDetail_ParentMenu.SelectedValue);
                        }
                        else
                        {
                            parentID = 0;
                        }

                        MenuManager mnuMan = new MenuManager();                        
                        mnuMan.Update(menuID, displayName, resourceID, parentID);                                               

                        //load lai treeview
                        LoadTreeViewMenu(int.Parse(ddlApplication.SelectedValue));    
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }

                private void btn_MenuDetail_SetPermisson_Click(object sender, EventArgs e)
                {
                    try
                    {
                        int iResourceID = 0;
                        this.CurrentFormState = FormState.BeforeSetPermission;
                        this.LoadMenuItemInfo(int.Parse(lbl_MenuDetail_MenuID.Text), ref iResourceID);
                        hdf_SetPermission_ResourceID.Value = iResourceID.ToString();
                        this.GetCurrentUsersAndRoles(iResourceID);
                        this.GetOperations(iResourceID);
                        //this.GetOperation(lbl_SetPermission_ResourceType.Text);                        
                        if (lst_SetPermission_CurrentUsers.Items.Count > 0)
                        {
                            lst_SetPermission_CurrentUsers.SelectedIndex = 0;
                            lst_SetPermission_CurrentUsers_SelectedIndexChanged(sender, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }
            #endregion

            #region viewStateAddResource
                private void btn_AddResource_AddResource_Click(object sender, EventArgs e)
                {
                    try
                    {
                        int iApplication = int.Parse(ddlApplication.SelectedValue);
                        string strResourceTypeCode = ddl_AddResource_ResourceType.SelectedValue.ToString();
                        string strResourceName = txt_AddResource_ResourceName.Text.Trim();
                        string strPath = txt_AddResource_Path.Text.Trim().ToLower();
                        string strLink = txt_AddResource_Link.Text.Trim().ToLower();
                        string strFileName = txt_AddResource_FileName.Text.Trim();
                        string strToken = txt_AddResource_Token.Text.Trim();
                        bool bIsParent = cbx_AddResource_IsParent.Checked;
                        if (bIsParent)
                        {
                            strPath = "";
                            strFileName = "";
                            strLink = "";
                        }
                        
                        Resource reSrc = this.CreateNewResource(iApplication, strResourceTypeCode, strResourceName, strPath, strFileName, strLink, true, bIsParent, strToken);
                        if (reSrc != null)
                        {
                            this.LoadResourceToDropdownlist(iApplication,ddl_MenuDetail_Resource);
                            ddl_MenuDetail_Resource.SelectedValue = reSrc.ResourceID.ToString();
                            multView.SetActiveView(viewStateMenuDetail);
                        }
                        else
                        {
                            this.ucEBX.Message = "Thêm Resource mới không thành công";
                            this.CurrentFormState = FormState.Error;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }
                private void cbx_AddResource_IsParent_CheckedChanged(object sender, EventArgs e)
                {
                    rvPath.Enabled = !cbx_AddResource_IsParent.Checked;
                    rvFileName.Enabled = !cbx_AddResource_IsParent.Checked;
                    rvToken.Enabled = !cbx_AddResource_IsParent.Checked;

                    txt_AddResource_Path.Enabled = !cbx_AddResource_IsParent.Checked;
                    txt_AddResource_FileName.Enabled = !cbx_AddResource_IsParent.Checked;
                    txt_AddResource_Link.Enabled = !cbx_AddResource_IsParent.Checked;
                    txt_AddResource_Token.Enabled = !cbx_AddResource_IsParent.Checked;
                    ddl_AddResource_ResourceType.Enabled = !cbx_AddResource_IsParent.Checked;
                    ddl_AddResource_ResourceType.SelectedValue = "DEFAULT";

                    if (cbx_AddResource_IsParent.Checked)
                    {
                        this.txt_AddResource_Path.Text = "";
                        this.txt_AddResource_FileName.Text = "";
                        this.txt_AddResource_Link.Text = "";
                        this.txt_AddResource_Token.Text = "";
                    }
                    
                }
            #endregion
            #region viewStateSetPermission
                private void btn_SetPermission_Browse_Click(object sender, EventArgs e)
                {
                    this.pnlAddUser.Visible = !this.pnlAddUser.Visible;

                    if (pnlAddUser.Visible)
                    {
                        try
                        {
                            UserCollection usrCol = new UserCollection();
                            RoleCollection roleCol = new RoleCollection();
                            switch (ddl_SetPermission_Type.SelectedValue)
                            {
                                case "1":
                                    usrCol = this.FindUserByUserName(txt_SetPermission_Name.Text.Trim());
                                    break;
                                case "2":
                                    roleCol = this.FindRoleByRoleCode(txt_SetPermission_Name.Text.Trim());
                                    break;
                                default:
                                    usrCol = this.FindUserByUserName(txt_SetPermission_Name.Text.Trim());
                                    roleCol = this.FindRoleByRoleCode(txt_SetPermission_Name.Text.Trim());
                                    break;
                            }
                            if (lst_SetPermission_NewUsers.Items.Count > 0)
                                lst_SetPermission_NewUsers.Items.Clear();
                            if (usrCol.Count > 0)
                                foreach (User usr in usrCol)
                                    if (lst_SetPermission_CurrentUsers.Items.FindByValue("User$" + usr.UserName) == null)
                                        //lst_SetPermission_NewUsers.Items.Add(new ListItem(usr.FullName, "User$" + usr.UserName));
                                        lst_SetPermission_NewUsers.Items.Add(new ListItem(usr.UserName, "User$" + usr.UserName));
                            if (roleCol.Count > 0)
                                foreach (Role role in roleCol)
                                    if (lst_SetPermission_CurrentUsers.Items.FindByValue("Role$" + role.RoleCode) == null)
                                        lst_SetPermission_NewUsers.Items.Add(new ListItem(role.RoleCode, "Role$" + role.RoleCode));

                        }
                        catch (Exception ex)
                        {
                            this.ucEBX.Message = "Hệ thống có lỗi.";
                            this.CurrentFormState = FormState.Error;
                            this.SaveErrorLog(ex);
                        }
                    }
                }
                private void btn_SetPermission_Close_Click(object sender, EventArgs e)
                {
                    this.pnlAddUser.Visible = false;
                }

                private void lst_SetPermission_CurrentUsers_SelectedIndexChanged(object sender, EventArgs e)
                {
                    try
                    {
                        int iResourceID;
                        string strRoleCode="";
                        string strUserName="";
                        string[] valu = lst_SetPermission_CurrentUsers.SelectedValue.Split('$');
                        //cbx_SetPermission_ListDenyOperation.Visible = false;
                        iResourceID = int.Parse(hdf_SetPermission_ResourceID.Value);
                        if (valu.Length == 2 && valu[0] == "Role")
                            strRoleCode = valu[1];
                        if (valu.Length == 2 && valu[0] == "User")
                        {
                            strUserName = valu[1];
                            cbx_SetPermission_ListDenyOperation.Visible = true;
                            foreach (ListItem item in cbx_SetPermission_ListDenyOperation.Items)
                            {
                                item.Selected = false;
                                foreach (Permission perItem in GetPermissionOnResourceByUserRole(iResourceID, strUserName, strRoleCode, false))
                                {
                                    if (int.Parse(item.Value) == perItem.OperationCode)
                                        item.Selected = true;
                                }
                            }    
                        }
                        
                        //Set các Checkbox Allow
                        foreach (ListItem item in cbx_SetPermission_ListOperation.Items)
                        {
                            item.Selected = false;
                            foreach (Permission perItem in GetPermissionOnResourceByUserRole(iResourceID, strUserName, strRoleCode, true))
                            {
                                if (int.Parse(item.Value) == perItem.OperationCode)
                                    item.Selected = true;
                            }
                        }

                        //Set các Checkbox Deny
                        foreach (ListItem item in cbx_SetPermission_ListDenyOperation.Items)
                        {
                            item.Selected = false;
                            foreach (Permission perItem in GetPermissionOnResourceByUserRole(iResourceID, strUserName, strRoleCode, false))
                            {
                                if (int.Parse(item.Value) == perItem.OperationCode)
                                    item.Selected = true;
                            }
                        }
                        
                        
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }

                private void btn_SetPermission_Find_Click(object sender, EventArgs e)
                {
                    try
                    {
                        UserCollection usrCol = new UserCollection();
                        RoleCollection roleCol = new RoleCollection();
                        switch (ddl_SetPermission_Type.SelectedValue)
                        {
                            case "1":
                                usrCol = this.FindUserByUserName(txt_SetPermission_Name.Text.Trim());
                                break;
                            case "2":
                                roleCol = this.FindRoleByRoleCode(txt_SetPermission_Name.Text.Trim());
                                break;
                            default:
                                usrCol = this.FindUserByUserName(txt_SetPermission_Name.Text.Trim());
                                roleCol = this.FindRoleByRoleCode(txt_SetPermission_Name.Text.Trim());
                                break;
                        }
                        if (lst_SetPermission_NewUsers.Items.Count> 0)
                            lst_SetPermission_NewUsers.Items.Clear();
                        if(usrCol.Count > 0)
                            foreach (User usr in usrCol)
                                if(lst_SetPermission_CurrentUsers.Items.FindByValue("User$" + usr.UserName) == null)
                                    //lst_SetPermission_NewUsers.Items.Add(new ListItem(usr.FullName, "User$" + usr.UserName));
                                    lst_SetPermission_NewUsers.Items.Add(new ListItem(usr.UserName, "User$" + usr.UserName));
                        if (roleCol.Count > 0)
                            foreach (Role role in roleCol)
                                if (lst_SetPermission_CurrentUsers.Items.FindByValue("Role$" + role.RoleCode) == null)
                                    lst_SetPermission_NewUsers.Items.Add(new ListItem(role.RoleCode, "Role$" + role.RoleCode));
                         
                    }
                    catch (Exception ex )
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }
                private void btn_SetPermission_Add_Click(object sender, EventArgs e)
                {
                    try
                    {
                        ListItemCollection lstSelectedItemsCol = new ListItemCollection();
                        foreach (ListItem item in lst_SetPermission_NewUsers.Items)
                            if (item.Selected)
                            {
                                lst_SetPermission_CurrentUsers.Items.Add(item);
                                item.Selected = false;
                                lstSelectedItemsCol.Add(item);
                            }
                        //neu la user thi cho hien lai cac checkbox denied permission
                        
                        foreach (ListItem selectedItem in lstSelectedItemsCol)
                            lst_SetPermission_NewUsers.Items.Remove(selectedItem);
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }

                private void cbx_SetPermission_ListOperation_SelectedIndexChanged(object sender, EventArgs e)
                {
                    int idx = 0;
                    foreach (ListItem item in cbx_SetPermission_ListOperation.Items)
                    {
                        if (item.Selected)
                            cbx_SetPermission_ListDenyOperation.Items[idx].Selected = false;
                        idx++;
                    }
                }

                private void cbx_SetPermission_ListDenyOperation_SelectedIndexChanged(object sender, EventArgs e)
                {
                    int idx = 0;
                    foreach (ListItem item in cbx_SetPermission_ListDenyOperation.Items)
                    {
                        if (item.Selected)
                            cbx_SetPermission_ListOperation.Items[idx].Selected = false;
                        idx++;
                    }
                }

                private void btn_SetPermission_Accept_Click(object sender, EventArgs e)
                {
                    try
                    {
                        string[] valu = lst_SetPermission_CurrentUsers.SelectedValue.Split('$');
                        if (valu.Length == 2 && valu[0] == "Role")
                        {
                            int iApplicationID;
                            string strRoleCode;
                            int iResourceID;
                            iApplicationID = int.Parse(ddlApplication.SelectedValue);
                            strRoleCode = valu[1];
                            iResourceID = int.Parse(hdf_SetPermission_ResourceID.Value);

                            string strDataPermission = "";
                            strDataPermission = CollectRolePermissionData(strRoleCode, iResourceID);
                            RoleManager roleMan = new RoleManager();
                            ReturnObject objRet = new ReturnObject();
                            objRet = roleMan.SetPermission(iApplicationID, strRoleCode, iResourceID, strDataPermission);
                            this.SaveActionLog("Set permission for Role", objRet.ErrorMessage);
                        }
                        if (valu.Length == 2 && valu[0] == "User")
                        {
                            string strUserName;
                            int iResourceID;
                            strUserName= valu[1];
                            iResourceID = int.Parse(hdf_SetPermission_ResourceID.Value);

                            string strDataPermission = "";
                            strDataPermission = CollectUserPermissionData(valu[1], int.Parse(hdf_SetPermission_ResourceID.Value));
                            UserManager usrMan = new UserManager();
                            ReturnObject objRet = new ReturnObject();
                            objRet = usrMan.SetPermission(strUserName, iResourceID, strDataPermission);
                            this.SaveActionLog("Set permission for User", objRet.ErrorMessage);
                        }

                        //Notify
                        this.ucNBX.Message = "Phân quyền thành công";
                        this.CurrentFormState = FormState.Notify;

                        cbx_SetPermission_ListOperation.Items.Clear();
                        cbx_SetPermission_ListDenyOperation.Items.Clear();                        
                    }
                    catch (Exception ex)
                    {
                        this.ucEBX.Message = "Hệ thống có lỗi.";
                        this.CurrentFormState = FormState.Error;
                        this.SaveErrorLog(ex);
                    }
                }

                private void cbx_SetPermission_AllOperation_CheckedChanged(object sender, EventArgs e)
                {
                    foreach (ListItem item in cbx_SetPermission_ListOperation.Items)
                    {
                        item.Selected = cbx_SetPermission_AllOperation.Checked;
                    }
                }

                protected void btnNext_Click(object sender, EventArgs e)
                {
                    CurrentFormState = FormState.AfterSetPermission;
                    tvwMenu_SelectedNodeChanged(null, null);
                    //Response.Redirect("Default.aspx");
                }
            #endregion

                
                
        #endregion

                
    }
}
