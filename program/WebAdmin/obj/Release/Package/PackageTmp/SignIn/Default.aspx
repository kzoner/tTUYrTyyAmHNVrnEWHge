<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.SignIn.Default" ValidateRequest="false" %>

<%@ Register src="../Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hệ thống quản lý thông tin GATE.VN</title>
    <link href="../App_Themes/Default/Default.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="frmLogin" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0" height="100%">
            <tr align="center">                
                <td>
                    <table width="98%" cellpadding="4" cellspacing="0">
                        <tr>
                            <td>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />                                
                                <br />
                                <br />                                
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table class="box" cellpadding="4" cellspacing="0">            
                                    <tr class="rowHeader">
                                        <td colspan="2">Đăng nhập hệ thống</td>
                                    </tr>
                                    <tr>
                                        <td><strong>Tài khoản</strong></td>
                                        <td>
                                            <asp:TextBox ID="tbxAccount" runat="server" MaxLength="30" Width="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ControlToValidate="tbxAccount" Display="Static" ErrorMessage="(*)" ValidationGroup="Validation"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong>Mật khẩu</strong></td>
                                        <td>
                                            <asp:TextBox ID="tbxPassword" runat="server" MaxLength="30" Width="200" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbxPassword" Display="Static" ErrorMessage="(*)" ValidationGroup="Validation"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td><asp:CheckBox ID="chkRemember" runat="server" Checked="false" TextAlign="Right" Text="Ghi nhớ tài khoản" /></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td><asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" ValidationGroup="Validation"/></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <uc1:ErrorBox ID="ebx" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
