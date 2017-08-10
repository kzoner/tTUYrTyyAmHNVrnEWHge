<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="NotifyBox.ascx.cs" Inherits="WebAdmin.Controls.NotifyBox" %>
<table cellpadding="8" cellspacing="0">
    <tr>
        <td valign="top">
            <asp:Image ID="imgIcon" ImageUrl="~/Images/blue_error.gif" runat="server" ImageAlign="Middle" /></td>
        <td>
            <asp:Label ID="lblNotifyMessage" EnableViewState="false" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td valign="top">
            &nbsp;</td>
        <td align="right">
            <asp:Button ID="btnNext" runat="server" Text="Tiếp tục" Width="100px" CausesValidation="false"
                onclick="btnNext_Click" />
        </td>
    </tr>
</table>
