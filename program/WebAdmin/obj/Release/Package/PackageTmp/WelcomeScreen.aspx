 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelcomeScreen.aspx.cs" Inherits="WebAdmin.WelcomeScreen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>GATE - HỆ THỐNG QUẢN TRỊ ỨNG DỤNG</title>
</head>
<body style="background:url(/images/bg.jpg) #CCCCCC; background-repeat:repeat-y; background-position:center top;">
    <form id="form1" runat="server">
        <table width="779" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td><img src="/images/logo_fpt.png" alt="logo_fpt" width="231" /></td>
                <td align="center"><span style="font-size:20pt; font-weight:bold;">HỆ THỐNG QUẢN TRỊ ỨNG DỤNG</span></td>
            </tr>
        </table>
        <div style="height:100px;"></div>
        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
           
          <tr>
            <td width="20%">&nbsp;</td>
            <td width="60%">
                <asp:DataList ID="dtlApplication" OnItemCreated="dtlApplicationID_ItemCreated" runat="server" RepeatLayout="Table" RepeatColumns="1" CellPadding="4" CellSpacing="1">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtApplication" runat="server" CommandArgument='<%#Eval("ApplicationID")%>' Font-Size="Medium"><%#Eval("Name")%></asp:LinkButton>
                        <%--<asp:ImageButton ID="ibtApplication" AlternateText='<%#Eval("Name")%>' ImageUrl="~/Images/logo_fpt.gif" runat="server" CommandArgument='<%#Eval("ApplicationID")%>' />--%>
                    </ItemTemplate>
                </asp:DataList>
            </td>
            <td width="20%">&nbsp;</td>
          </tr>
          <tr>
            <td></td>            
            <td>
                <a href="/SignOut/Default.aspx" style="font-family:Arial; font-size:11px; font-weight:bold; text-decoration: none;">[Logout]</a>
            </td>
            <td></td>
          </tr>
        </table>
    </form>
</body>
</html>
