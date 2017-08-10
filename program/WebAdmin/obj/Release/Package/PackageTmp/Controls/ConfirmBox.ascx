<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmBox.ascx.cs" Inherits="WebAdmin.Controls.ConfirmBox" %>
<table cellpadding="8" cellspacing="0" class="box">
    <tr>
        <td valign="top">
            <asp:Image ID="imgIcon" ImageUrl="~/Images/blue_error.gif" runat="server" ImageAlign="Middle" /></td>
        <td>
            <asp:Label ID="lblConfirmMessage" EnableViewState="false" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td></td>
        <td align="right">
            <asp:Button ID="btnYes" Width="75" runat="server" Text="Có" 
                onclick="btnYes_Click" />&nbsp;
            <asp:Button ID="btnNo" Width="75" runat="server" Text="Không" 
                onclick="btnNo_Click" /></td>
    </tr>
</table>