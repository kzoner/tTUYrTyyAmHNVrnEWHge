<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.OperationCat.Edit.Default" %>

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
                <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="Chưa nhập tên loại thao tác" Display="Dynamic" 
                    ControlToValidate="txtName"></asp:RequiredFieldValidator>
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
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Lưu" OnClientClick="return validate();"
                    Width="80px" />
                <asp:Button ID="btnBack" runat="server" Text="Trở lại" 
                    Width="80px" PostBackUrl="../Manage/Default.aspx" />
            </td>
        </tr>
    </table>
    <uc2:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>
