<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Resources.Edit.Default" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="errorbox" tagprefix="uc1" %>
<%@ Register src="~/Controls/NotifyBox.ascx" tagname="NotifyBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">

<asp:Panel ID="pnlCreationBox" runat="server" Width="800">
        <table cellpadding="4" cellspacing="1" class="box">
            <tr>
                <td>Ứng dụng</td>
                <td>
                    <asp:DropDownList ID="ddlApplications" runat="server" Enabled="False">
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Loại tài nguyên</td>
                <td>
                    <asp:DropDownList ID="ddlResourceType" runat="server" Enabled="False">                        
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    Tài nguyên rỗng</td>
                <td>
                    <asp:CheckBox ID="cbxIsParent" runat="server" 
                        oncheckedchanged="cbxIsParent_CheckedChanged" AutoPostBack="True" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Tên tài nguyên:</td>
                <td>
                    <asp:TextBox ID="txtResourceName" runat="server" Width="300"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rvResourceName" runat="server"
                        ControlToValidate="txtResourceName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExResourceName" runat="server"
                        ControlToValidate="txtResourceName" Display="Dynamic" ErrorMessage="Tên tài nguyên không được có ký tự đặc biệt"
                        ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$">
                        </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Đường dẫn:</td>
                <td>
                    <asp:TextBox ID="txtPath" runat="server" Width="300"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rvPath" runat="server"
                        ControlToValidate="txtPath" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExPath" runat="server"
                        ControlToValidate="txtPath" Display="Dynamic" ErrorMessage="Đường dẫn không được có ký tự đặc biệt"
                        ValidationExpression="^([a-zA-Z0-9_\\\-\/:.])*$">
                        </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Tên file: </td>
                <td>
                    <asp:TextBox ID="txtFileName" runat="server" Width="300"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rvFileName" runat="server"
                        ControlToValidate="txtFileName" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExFileName" runat="server"
                        ControlToValidate="txtFileName" Display="Dynamic" ErrorMessage="Tên file không được có ký tự đặc biệt"
                        ValidationExpression="^([a-zA-Z0-9_\.])*$">
                        </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Link</td>
                <td>
                    <asp:TextBox ID="txtLink" runat="server" Width="300"></asp:TextBox>
                </td>
                <td>
                    <asp:RegularExpressionValidator ID="regExLink" runat="server"
                        ControlToValidate="txtLink" Display="Dynamic" ErrorMessage="Link không được có ký tự đặc biệt"
                        ValidationExpression="^([a-zA-Z0-9_\\\-\/.:])*$">
                        </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Token</td>
                <td>
                    <asp:TextBox ID="txtToken" runat="server" Width="300"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rvToken" runat="server"
                        ControlToValidate="txtToken" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regExToken" runat="server"
                        ControlToValidate="txtToken" Display="Dynamic" ErrorMessage="Token không được có ký tự đặc biệt"
                        ValidationExpression="^([a-zA-Z0-9\-])*$">
                        </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" 
                        onclick="btnUpdate_Click" />
                    <asp:HiddenField ID="hfStatus" runat="server" />
                </td>
            </tr>            
        </table>        
    </asp:Panel>
    
    <asp:Panel ID="pnlErrorBox" runat="server">
        <uc1:ErrorBox ID="ucErrorBox" runat="server" />
    </asp:Panel>
    
    <asp:Panel ID="pnlNotifyBox" runat="server">
        <uc2:NotifyBox ID="ucNotifyBox" OnNextClicked="btnNext_Click" runat="server" />
    </asp:Panel>
    
</asp:Content>
