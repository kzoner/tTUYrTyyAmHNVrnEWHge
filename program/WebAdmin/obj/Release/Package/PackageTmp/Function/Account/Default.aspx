<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Account.Default" %>

<%@ Register Src="~/Controls/PagerBox.ascx" TagPrefix="uc1" TagName="PagerBox" %>
<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc2" TagName="ErrorBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table class="box" width="100%">
        <tr>
            <td>Loại:</td>
            <td>
                <asp:DropDownList ID="ddlAccountType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged" />
            </td>
            <td>Tên:</td>
            <td>
                <asp:TextBox ID="txtAccountName" runat="server" />
            </td>
            <td style="white-space: nowrap">Cấp đại lý:</td>
            <td>
                <asp:DropDownList ID="ddlAccountLevel" runat="server" />
            </td>
            <td style="white-space: nowrap">Trạng thái:</td>
            <td>
                <asp:DropDownList ID="ddlAccountStatus" runat="server" />
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Tra cứu" OnClick="btnSearch_Click" />
            </td>
            <td>
                <asp:Button ID="btnExcel" runat="server" Text="Xuất Excel" OnClick="btnExcel_Click" />
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" OnClick="btnAdd_Click" />
            </td>
            <td style="width: 100%"></td>
        </tr>
    </table>
    <div id="divData" runat="server" visible="false">
        <asp:GridView ID="gvData" runat="server" Width="100%" OnRowDataBound="gvData_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<% #Eval("AccountId") %>' OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table class="box" width="100%">
            <tr>
                <td align="right">
                    <uc1:PagerBox runat="server" ID="uctPager" OnSelectChange="uctPager_SelectChange"/>
                </td>
            </tr>
        </table>
    </div>
    <uc2:ErrorBox runat="server" ID="ErrorBox" />
</asp:Content>
