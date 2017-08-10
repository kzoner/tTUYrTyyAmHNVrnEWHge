<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Users.Manage.ResetPwd.Default" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc2" %>

<%@ Register src="../../../../Controls/NotifyBox.ascx" tagname="NotifyBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">

<asp:Panel ID="pnlResetPass" runat="server">
    <table cellspacing="0" cellpadding="4" class="box">
        <tr>
            <td align="right">
                Tên tài khoản:</td>
            <td>
                <asp:TextBox ID="tbxUsername" runat="server" Width="250px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rvUsername" runat="server" 
                    ControlToValidate="tbxUsername" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">Mật khẩu:</td>
            <td>
                <asp:TextBox ID="tbxPassword" runat="server" TextMode="Password" Width="250px"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="rvPassword" runat="server" 
                    ControlToValidate="tbxPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">Xác nhận mật khẩu:</td>
            <td>
                <asp:TextBox ID="tbxCfPassword" runat="server" TextMode="Password" 
                    Width="250px"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="rvCfPassword" runat="server" 
                    ControlToValidate="tbxCfPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:CompareValidator ID="cvCfPassword" runat="server" 
                    ControlToCompare="tbxCfPassword" ControlToValidate="tbxPassword" 
                    ErrorMessage="Xác nhận mật khẩu và mật khẩu mới phải trùng nhau"></asp:CompareValidator>
            </td>       
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnChangePass" runat="server" Text="Đặt mật khẩu" 
                    onclick="btnChangePass_Click" /></td>
            <td align="right">
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnlNotify" runat="server">
    <uc1:NotifyBox ID="usrNotifyBox" OnNextClicked="btnNext_Click" runat="server" />    
</asp:Panel>

<asp:Panel ID="pnlErrorBox" runat="server">    
    <uc2:ErrorBox ID="usrErrorBox" runat="server" />
</asp:Panel>

</asp:Content>
