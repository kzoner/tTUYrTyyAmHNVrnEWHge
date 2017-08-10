<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Roles.Create.Default" %>
<%@ Register src="../../../Controls/ApplicationList.ascx" tagname="ApplicationList" tagprefix="uc1" %>
<%@ Register src="../../../Controls/ConfirmBox.ascx" tagname="ConfirmBox" tagprefix="uc2" %>
<%@ Register src="../../../Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
<table class="box" cellpadding="4px" cellspacing="0" border="0" >
<tr><td>Ứng dụng</td><td>
    <uc1:ApplicationList ID="uctApplicationList" runat="server" />
    </td><td>
        &nbsp;</td></tr>
<tr><td>Mã Role</td><td>
    <asp:TextBox ID="txtRoleCode" runat="server" Width="300px"></asp:TextBox>
    </td><td>
        <asp:RequiredFieldValidator ID="vldRoleCode" runat="server" 
            ControlToValidate="txtRoleCode" Display="Dynamic" 
            ErrorMessage="Chưa nhập Mã Role"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regExRoleCode" runat="server"
            ControlToValidate="txtRoleCode" Display="Dynamic" ErrorMessage="Mã Role không được có ký tự đặc biệt"
            ValidationExpression="^([a-zA-Z0-9_])*$">
            </asp:RegularExpressionValidator>
    </td></tr>
<tr><td>Tên Role</td><td>
    <asp:TextBox ID="txtRoleName" runat="server" Width="300px"></asp:TextBox>
    </td><td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="txtRoleName" Display="Dynamic" 
            ErrorMessage="Chưa nhập tên Role"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="regExRoleName" runat="server"
            ControlToValidate="txtRoleName" Display="Dynamic" ErrorMessage="Tên Role không được có ký tự đặc biệt"
            ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$">
            </asp:RegularExpressionValidator>
    </td></tr>
<tr><td></td><td>
    <asp:Button ID="btnCreate" runat="server" Text="Lưu" style=" min-width:80px;"
        onclick="btnCreate_Click" />
    </td><td>
        &nbsp;</td></tr>
</table>
    <uc2:ConfirmBox ID="uctConfirmBox" runat="server" 
        OnYesClicked="ClickedYes"
        OnNoClicked="ClickedNo" />
    <uc3:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>
