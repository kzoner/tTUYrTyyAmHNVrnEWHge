<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.OperationCat.Create.Default" %>
<%@ Register src="~/Controls/ConfirmBox.ascx" tagname="ConfirmBox" tagprefix="uc1" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
<table cellpadding="4" cellspacing="0" class="box">
        <tr>
            <td>
                Tên</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvName" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Chưa nhập tên loại thao tác" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regExName" runat="server"
                        ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Tên thao tác không được có ký tự đặc biệt"
                        ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$">
                        </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Mô tả</td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Width="300px" Height="194px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Lưu"
                    Width="80px" />
                <asp:Button ID="btnBack" runat="server" Text="Trở lại" 
                    Width="80px" PostBackUrl="../Manage/Default.aspx" />
            </td>
        </tr>
    </table>
    <uc1:ConfirmBox id="uctConfirmBox" runat="server" 
        OnYesClicked="ClickedYes"
        OnNoClicked="ClickedNo" />
    <uc2:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>
