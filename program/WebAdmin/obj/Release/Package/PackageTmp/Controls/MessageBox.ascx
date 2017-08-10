<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="MessageBox.ascx.cs" Inherits="WebAdmin.Controls.MessageBox" %>
<table cellpadding="8" cellspacing="0">
    <tr>
        <td valign="top">
            <asp:Image ID="imgIcon" ImageUrl="~/Images/blue_error.gif" runat="server" ImageAlign="Middle" /></td>
        <td>
            <asp:Label ID="lblErrorMessage" EnableViewState="false" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
