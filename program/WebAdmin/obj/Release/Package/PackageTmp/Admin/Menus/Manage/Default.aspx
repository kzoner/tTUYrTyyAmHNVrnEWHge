<%@ Page Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Menus.Manage.Default" Title="Untitled Page" EnableEventValidation="false" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc1" %>
<%@ Register src="../../../Controls/NotifyBox.ascx" tagname="NotifyBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <asp:ScriptManager ID="scriptMan" runat="server" AllowCustomErrorsRedirect="true"></asp:ScriptManager>
    <asp:XmlDataSource ID="xmlSrc" EnableCaching="false" EnableViewState="false" runat="server"></asp:XmlDataSource>
    <table width="100%">
        <tr>
            <td width="30%" valign="top">
                <asp:Panel ID="pnlApplication" runat="server" Width="312">
                    <table class="box">
                        <tr>
                            <td style="white-space:nowrap"><strong>Ứng dụng: </strong></td>
                            <td><asp:DropDownList ID="ddlApplication" runat="server" Width="200" AutoPostBack="true"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="white-space:nowrap"><strong>Move menu: </strong></td>
                            <td>
                                <asp:TextBox ID="txtMoveStep" runat="server" Width="20"></asp:TextBox> step &nbsp;
                                <asp:Button ID="btnMoveMenu" runat="server" Text="Move"
                                    onclick="btnMoveMenu_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--<asp:Panel ID="pnlMoveMenu" runat="server">
                    <asp:ImageButton ID="ibtnUp" runat="server" ImageUrl="~/Images/arrow_up.gif" 
                        ToolTip="Di chuyển lên" Height="23" onclick="ibtnUp_Click" />
                    <asp:ImageButton ID="ibtnDown" runat="server" 
                        ImageUrl="~/Images/arrow_down.gif" ToolTip="Di chuyển xuống" Height="23" 
                        onclick="ibtnDown_Click" />
                </asp:Panel>--%>
                <asp:Panel CssClass="box" ID="pnlTreeView" runat="server">
                    <asp:TreeView ID="tvwMenu" DataSourceID="xmlSrc" runat="server" ExpandDepth="1" 
                        ShowLines="true">                    
                        <SelectedNodeStyle Font-Bold="true"/>
                        <DataBindings>
                            <asp:TreeNodeBinding ValueField="MenuID" TextField="DisplayName"/>                                                        
                        </DataBindings>                        
                    </asp:TreeView>
                </asp:Panel>
            </td>
            <td width="70%" valign="top">
                <asp:Panel ID="pnlContent" runat="server">
                    <asp:MultiView ID="multView" runat="server" ActiveViewIndex="0">
                        <asp:View ID="viewStateEmpty" runat="server"></asp:View>
                        <asp:View ID="viewStateMenuDetail" runat="server">
                            <table cellpadding="4">
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>ID:</strong></td>
                                    <td><asp:Label ID="lbl_MenuDetail_MenuID" runat="server" Font-Bold="True"></asp:Label></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Tên hiển thị:</strong></td>
                                    <td><asp:TextBox ID="tbx_MenuDetail_DisplayName" runat="server" Width="293" 
                                            MaxLength="30" ValidationGroup="Validation"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="tbx_MenuDetail_DisplayName" Display="Dynamic" ErrorMessage="(*)" ValidationGroup="Validation"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Resource:</strong></td>
                                    <td><asp:DropDownList ID="ddl_MenuDetail_Resource" runat="server" Width="298"></asp:DropDownList></td>
                                    <td><asp:Button ID="btn_MenuDetail_AddNewResource" runat="server" 
                                            Text="Thêm mới Resource" /></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Thuộc:</strong></td>
                                    <td><asp:DropDownList ID="ddl_MenuDetail_ParentMenu" runat="server" Width="298"></asp:DropDownList></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Button ID="btn_MenuDetail_AddNewMenu" runat="server" Text="Thêm Menu con" />
                                        <asp:Button ID="btn_MenuDetail_Update" runat="server" Text="Lưu thay đổi" ValidationGroup="Validation" />
                                        <asp:Button ID="btn_MenuDetail_Delete" runat="server" Text="Xóa menu" OnClientClick="return confirm('Bạn chắc chắn muốn xóa menu?');" />
                                        <asp:Button ID="btn_MenuDetail_SetPermisson" runat="server" 
                                            Text="Cấp quyền truy cập" />
                                        <asp:Button ID="btn_MenuDetail_SaveNewMenuItem" runat="server" Text="Lưu" ValidationGroup="Validation" />
                                    </td> 
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="viewStateAddResource" runat="server">
                            <table cellpadding="4" cellspacing="1">
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Loại Resource:</strong></td>
                                    <td><asp:DropDownList ID="ddl_AddResource_ResourceType" runat="server" Width="200"></asp:DropDownList></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Tài nguyên rỗng:</strong></td>
                                    <td><asp:CheckBox ID="cbx_AddResource_IsParent" runat="server" 
                                            AutoPostBack="True" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Tên tài nguyên:</strong></td>
                                    <td><asp:TextBox ID="txt_AddResource_ResourceName" runat="server" Width="200" ValidationGroup="ValidateAddNewResource"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rvResourceName" runat="server" ValidationGroup="ValidateAddNewResource" ControlToValidate="txt_AddResource_ResourceName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Đường dẫn:</strong></td>
                                    <td><asp:TextBox ID="txt_AddResource_Path" runat="server" Width="200" ValidationGroup="ValidateAddNewResource"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rvPath" runat="server" ValidationGroup="ValidateAddNewResource" ControlToValidate="txt_AddResource_Path" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Tên file: </strong></td>
                                    <td><asp:TextBox ID="txt_AddResource_FileName" runat="server" Width="200" ValidationGroup="ValidateAddNewResource"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rvFileName" ValidationGroup="ValidateAddNewResource" runat="server" ControlToValidate="txt_AddResource_FileName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Link</strong></td>
                                    <td><asp:TextBox ID="txt_AddResource_Link" runat="server" Width="200"></asp:TextBox></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="white-space:nowrap" align="right"><strong>Token: </strong></td>
                                    <td><asp:TextBox ID="txt_AddResource_Token" runat="server" Width="200" ValidationGroup="ValidateAddNewResource"></asp:TextBox></td>
                                    <td><asp:RequiredFieldValidator ID="rvToken" runat="server" ValidationGroup="ValidateAddNewResource" ControlToValidate="txt_AddResource_Token" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:Button ID="btn_AddResource_AddResource" runat="server" Text="Tạo mới" ValidationGroup="ValidateAddNewResource" /></td>
                                </tr>   
                            </table>
                        </asp:View>
                        <asp:View ID="viewStateSetPermission" runat="server">
                            <asp:UpdatePanel ID="updPanel" runat="server" ChildrenAsTriggers="true" EnableViewState="true">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlResourceDetail" runat="server">
                                        <table cellpadding="4" cellspacing="1" class="box" width="100%">
                                            <tr>
                                                <%--<td style="white-space:nowrap" align="right">Ứng dụng:</td>
                                                <td style="white-space:nowrap">
                                                    <asp:Label ID="lbl_SetPermission_ApplicationName" runat="server" 
                                                        CssClass="boldText" ForeColor="Blue"></asp:Label>
                                                </td>--%>
                                                <td style="white-space:nowrap" align="right">Tên tài nguyên:</td>
                                                <td style="white-space:nowrap">
                                                    <asp:Label ID="lbl_SetPermission_ResourceName" runat="server" 
                                                        CssClass="boldText" ForeColor="Blue"></asp:Label>
                                                    <asp:HiddenField ID="hdf_SetPermission_ResourceID" runat="server" Value="" />
                                                </td>
                                                <td style="white-space:nowrap" align="right">Loại:</td>
                                                <td style="white-space:nowrap">
                                                    <asp:Label ID="lbl_SetPermission_ResourceType" runat="server" 
                                                        CssClass="boldText" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td width="100%" align="right">
                                                    <asp:UpdateProgress ID="udp" runat="server" AssociatedUpdatePanelID="updPanel">
                                                        <ProgressTemplate>
                                                            <span style="background-color:Yellow; white-space:nowrap;">...Đang xử lý...</span>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>        
                                    </asp:Panel>
                                    <asp:Panel ID="pnlCurrentUser" runat="server">
                                        <table cellpadding="4" cellspacing="1" class="box" width="100%">
                                            <tr>
                                                <td style="white-space:nowrap; width:50%;">User/nhóm cho phép</td>
                                                <td style="white-space:nowrap; width:50%;">Thao tác</td>
                                            </tr>
                                            <tr>
                                                <td>    
                                                    <asp:ListBox ID="lst_SetPermission_CurrentUsers" runat="server" SelectionMode="Single" Rows="15" Width="100%" AutoPostBack="True"></asp:ListBox>
                                                </td>
                                                <td valign="top" rowspan="2">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td width="10%">&nbsp;</td>
                                                            <td width="60%"><strong>Cho phép</strong></td>
                                                            <td><strong>Cấm</strong></td>
                                                        </tr>
                                                        <tr>
                                                            <td style="white-space:nowrap" colspan="2">
                                                                <asp:CheckBox ID="cbx_SetPermission_AllOperation" runat="server" AutoPostBack="True" Text="Cho phép tất cả" TextAlign="Right"/>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td style="white-space:nowrap">
                                                                <asp:CheckBoxList ID="cbx_SetPermission_ListOperation" AutoPostBack="true" runat="server"></asp:CheckBoxList>
                                                            </td>
                                                            <td style="white-space:nowrap">
                                                                <asp:CheckBoxList ID="cbx_SetPermission_ListDenyOperation" AutoPostBack="true" runat="server"></asp:CheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space:nowrap" align="right">
                                                    <%--<asp:Button ID="btn_SetPermission_Save" runat="server" Text="Lưu"/>
                                                    &nbsp;&nbsp;<asp:Button ID="btn_SetPermission_Remove" runat="server" Text="Xoá" 
                                                        OnClientClick="return confirm('Gỡ bỏ quyền truy cập của những user đã chọn?');" /> &nbsp;--%>
                                                    <asp:Button ID="btn_SetPermission_Browse" runat="server" Text="Browse"/>
                                                </td> 
                                                <td align="center">                                                    
                                                    <asp:Button ID="btn_SetPermission_Accept" runat="server" Text="Cập nhật"/>
                                                </td>               
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="pnlNotify" runat="server" >
                                                        <uc2:NotifyBox ID="ucNBX" runat="server" OnNextClicked="btnNext_Click" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAddUser" runat="server" Visible="false">
                                        <table cellpadding="4" cellspacing="1" class="box" width="100%">
                                            <tr>
                                                <td>
                                                    <strong>Tìm theo:</strong>
                                                    <asp:DropDownList ID="ddl_SetPermission_Type" runat="server" Width="130">
                                                        <asp:ListItem Value="0" Text="Tất cả" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="User"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Nhóm"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <strong>Từ khóa:</strong>
                                                    <asp:TextBox ID="txt_SetPermission_Name" runat="server" Width="250"></asp:TextBox>
                                                    <asp:Button ID="btn_SetPermission_Find" runat="server" Text="Tìm" Width="35px"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="white-space:nowrap" valign="top">
                                                    <asp:ListBox ID="lst_SetPermission_NewUsers" runat="server" SelectionMode="Multiple" Rows="15" Width="100%"></asp:ListBox>
                                                </td>                
                                            </tr>            
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="btn_SetPermission_Add" runat="server" Text="Thêm"/> &nbsp;
                                                    <asp:Button ID="btn_SetPermission_Close" runat="server" Text="Đóng"/>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:View>
                    </asp:MultiView>
                    <asp:Panel ID="pnlError" runat="server">
                        <uc1:ErrorBox ID="ucEBX" runat="server" />                        
                    </asp:Panel>                    
                </asp:Panel>                
            </td>
        </tr>
    </table>
</asp:Content>
