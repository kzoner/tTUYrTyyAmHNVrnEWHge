<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginView.ascx.cs" Inherits="WebAdmin.Controls.LoginView" %>
<div>
    <asp:LoginView ID="lgv" runat="server">
        <LoggedInTemplate>
            <table cellpadding="0" cellspacing="0" align="center">
                <tr align="center">
                   <td>
                       <asp:LoginName ID="lgn" runat="server" Font-Bold="true" Font-Size="14px" ForeColor="#d75600" />
                       <a href="/SignOut/Default.aspx" target="_parent" style="font-family:Arial; font-size:11px; font-weight:bold; text-decoration: none;">[Logout]</a>
                       <br />
                       <asp:HyperLink ID="hlkChangePass" runat="server" Text="Đổi mật khẩu" NavigateUrl="/Admin/Users/ChangePwd/Default.aspx" Target="content" Font-Bold="true" Font-Size="10px" Font-Names="Arial"/></td>
                </tr>
            </table>    
        </LoggedInTemplate>
    </asp:LoginView>
</div>