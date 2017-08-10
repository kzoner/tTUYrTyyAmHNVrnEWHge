<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagerBox.ascx.cs" Inherits="WebAdmin.Controls.PagerBox" %>

<table border="0" cellpadding="0" cellspacing="0">
<tr style="white-space:nowrap" ><td>
    <asp:ImageButton ID="btnFirst" runat="server" AlternateText="|&lt;" 
        onclick="btnFirst_Click" ImageUrl="~/Images/first.png" />
    </td><td>
    <asp:ImageButton ID="btnPre" runat="server" AlternateText="&lt;" 
        onclick="btnPre_Click" ImageUrl="~/Images/previous.png" />
    </td><td>
    <asp:DropDownList ID="ddlPageNumber" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlPageNumber_SelectedIndexChanged">
        </asp:DropDownList>
    </td><td>
    <asp:ImageButton ID="btnNext" runat="server" AlternateText="&gt;" 
        onclick="btnNext_Click" ImageUrl="~/Images/next.png" Width="16px" />
    </td><td>
    <asp:ImageButton ID="btnLast" runat="server" AlternateText="&gt;|" 
        onclick="btnLast_Click" ImageUrl="~/Images/last.png" />
    </td></tr>
</table>
