<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Resources.ResourceTypes.Default" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="errorbox" tagprefix="uc1" %>
<%@ Register src="~/Controls/NotifyBox.ascx" tagname="NotifyBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">

    <asp:Panel ID="pnlAddNew" runat="server">
        <table cellpadding="4" cellspacing="1">            
            <tr>
                <td style="white-space:nowrap;">
                    Mã loại:</td>
                <td style="white-space:nowrap;">
                    <asp:TextBox ID="txtRsTypeCode" runat="server" Width="150"></asp:TextBox>
                </td>
                <td style="white-space:nowrap;">
                    <asp:RequiredFieldValidator ID="rvRsTypeCode" runat="server"
                    ControlToValidate="txtRsTypeCode" ErrorMessage="*" Display="Dynamic" 
                        ValidationGroup="AddNewGroup"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExRsTypeCode" runat="server"
                        ControlToValidate="txtRsTypeCode" Display="Dynamic" ErrorMessage="*"
                        ValidationExpression="^([a-zA-Z0-9\-_])*$" ValidationGroup="AddNewGroup">
                        </asp:RegularExpressionValidator>                    
                </td>
                <td style="white-space:nowrap;">
                    Tên loại:</td>
                <td style="white-space:nowrap;">
                    <asp:TextBox ID="txtRsTypeName" runat="server" Width="200" 
                        ValidationGroup="AddNewGroup"></asp:TextBox>
                </td>
                <td style="white-space:nowrap;">
                    <asp:RequiredFieldValidator ID="rvRsTypeName" runat="server"
                    ControlToValidate="txtRsTypeName" ErrorMessage="*" Display="Dynamic" 
                        ValidationGroup="AddNewGroup"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExRsTypeName" runat="server"
                        ControlToValidate="txtRsTypeName" Display="Dynamic" ErrorMessage="*"
                        ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$" ValidationGroup="AddNewGroup">
                        </asp:RegularExpressionValidator>
                </td>
                <td style="white-space:nowrap;">
                    <asp:Button ID="btnCreate" runat="server" Text="Tạo" Width="80" 
                    onclick="btnCreate_Click" ValidationGroup="AddNewGroup" />
                </td>
                <td style="white-space:nowrap; width:100%;">
                    <asp:Button ID="btnCancel" runat="server" Text="Hủy" Width="80" 
                    onclick="btnCancel_Click" CausesValidation="False" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDataGridBox" runat="server" Width="800">
        <asp:GridView ID="dgridRsType" runat="server" Width="100%" 
            AutoGenerateColumns="False" onrowcancelingedit="dgridRsType_RowCancelingEdit" 
            onrowediting="dgridRsType_RowEditing" 
            onrowupdating="dgridRsType_RowUpdating" CellPadding="4" CellSpacing="1">
            <Columns>                
                <asp:TemplateField HeaderText="Mã loại">
                    <ItemTemplate>
                        <asp:Label ID="lblTypeCode" runat="server" Text='<%# Bind("ResourceTypeCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="Tên loại">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                    
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditName" runat="server" Text='<%# Bind("Name") %>' 
                            ValidationGroup="UpdateGroup" Width="300"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvEditName" runat="server" 
                            ControlToValidate="txtEditName" ErrorMessage="RequiredFieldValidator" 
                            ValidationGroup="UpdateGroup"></asp:RequiredFieldValidator>                        
                        <asp:RegularExpressionValidator ID="regExEditName" runat="server"
                            ControlToValidate="txtEditName" Display="Dynamic" ErrorMessage="*"
                            ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$" ValidationGroup="UpdateGroup"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Xóa" CommandArgument='<%#Bind("ResourceTypeCode") %>'
                            OnClick="lbtnDelete_Clicked" OnClientClick="return confirm('Bạn muốn xóa loại tài nguyên này?');">
                            </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true" EditText="Sửa" CancelText="Cancel" 
                    UpdateText="OK" ValidationGroup="UpdateGroup" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnSetOperations" runat="server" Text="Quản lý thao tác" CommandArgument='<%#Bind("ResourceTypeCode") %>'
                            OnClick="lbtnSetOperations_Clicked">
                            </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlOperations" runat="server" Width="800">
        <table cellpadding="4" cellspacing="1">
            <tr>
                <td style="vertical-align:top;" class="colunmHeader">Loại tài nguyên</td>
                <td style="white-space:nowrap" class="SubCat">
                    <asp:Label ID="lblResourceType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top; height:25px;" class="colunmHeader">Thao tác</td>
                <td class="HlightRow" rowspan="2">
                    <asp:CheckBoxList ID="cbxListOperations" runat="server"></asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
                        Text="Đồng ý" />                    
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlNoDataBox" runat="server" Width="800">
        <table cellpadding="4" cellspacing="1" width="100%">
            <tr class="rowHeader">
                <td style="width:25%;">
                    Mã loại</td>
                <td style="width:25%;">
                    Tên loại</td>
                <td style="width:25%;">
                </td>
                <td style="width:25%;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlErrorBox" runat="server" Width="800">
        <uc1:ErrorBox ID="ucErrorBox" runat="server" Message="test" />
    </asp:Panel>
    
    <asp:Panel ID="pnlNotifyBox" runat="server">
        <uc2:NotifyBox ID="ucNotifyBox" OnNextClicked="btnNext_Click" runat="server" />
    </asp:Panel>

</asp:Content>
