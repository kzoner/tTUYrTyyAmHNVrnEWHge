<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Account.Add.Default" %>

<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc1" TagName="ErrorBox" %>
<%@ Register Src="~/Controls/NotifyBox.ascx" TagPrefix="uc2" TagName="NotifyBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table class="box">
        <tr>
            <td>Loại:
            </td>
            <td>
                <asp:DropDownList ID="ddlAccountType" runat="server" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Tên:
            </td>
            <td>
                <asp:TextBox ID="txtAccountName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvAccountType" runat="server" ErrorMessage="*" ControlToValidate="txtAccountName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Tên viết tắt:
            </td>
            <td>
                <asp:TextBox ID="txtAccountShortName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Cấp đại lý:
            </td>
            <td>
                <asp:DropDownList ID="ddlAccountLevel" runat="server" Width="405px"></asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Người liên hệ:
            </td>
            <td>
                <asp:TextBox ID="txtContactName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ErrorMessage="*" ControlToValidate="txtContactName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Địa chỉ:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="*" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Điện thoại 1:
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNumber1" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Điện thoại 2:
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Điện thoại 3:
            </td>
            <td>
                <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Email:
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Website:
            </td>
            <td>
                <asp:TextBox ID="txtWebsite" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td valign="top">Ghi chú:
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="5" Width="400px"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Trạng thái:
            </td>
            <td>
                <asp:DropDownList ID="ddlAccountStatus" runat="server" Width="405px"></asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click" />
                <asp:Button ID="btnBack" runat="server" Text="Quay lại" OnClick="btnBack_Click" CausesValidation="False" />
            </td>
            <td></td>
        </tr>
    </table>
    <uc1:ErrorBox runat="server" ID="ErrorBox" />
    <uc2:NotifyBox runat="server" ID="NotifyBox" OnNextClicked="NotifyBox_NextClicked" />
</asp:Content>
