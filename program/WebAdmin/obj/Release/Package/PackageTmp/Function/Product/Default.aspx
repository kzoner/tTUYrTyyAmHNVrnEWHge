<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Product.Default" %>

<%@ Register Src="~/Controls/PagerBox.ascx" TagPrefix="uc1" TagName="PagerBox" %>
<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc2" TagName="ErrorBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table class="box" width="100%">
        <tr>
            <td>Loại:</td>
            <td>
                <asp:DropDownList ID="ddlProductType" runat="server" />
            </td>
            <td style="white-space: nowrap">Mã sản phẩm:</td>
            <td>
                <asp:TextBox ID="txtProductCode" runat="server" />
            </td>
            <td style="white-space: nowrap">Tên sản phẩm:</td>
            <td>
                <asp:TextBox ID="txtProductName" runat="server" />
            </td>
            <td style="white-space: nowrap">Trạng thái:</td>
            <td>
                <asp:DropDownList ID="ddlProductStatus" runat="server" />
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
        <asp:GridView ID="gvData" runat="server" Width="100%" OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="False" EnableModelValidation="True">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<% #Eval("ProductId") %>' OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProductTypeId" HeaderText="ProductTypeId" />
                <asp:BoundField DataField="ProductCode" HeaderText="ProductCode" />
                <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="UnitTypeId" HeaderText="UnitTypeId" />
                <asp:BoundField DataField="UnitValue" HeaderText="UnitValue" />
                <asp:BoundField DataField="UnitId" HeaderText="UnitId" />
                <asp:BoundField DataField="ImagePath" HeaderText="ImagePath" />
                <asp:BoundField DataField="Note" HeaderText="Note" />
                <asp:BoundField DataField="ProductStatus" HeaderText="ProductStatus" />
            </Columns>
        </asp:GridView>
        <table class="box" width="100%">
            <tr>
                <td align="right">
                    <uc1:PagerBox ID="uctPager" runat="server" OnSelectChange="uctPager_SelectChange" />
                </td>
            </tr>
        </table>
    </div>
    <uc2:ErrorBox runat="server" ID="ErrorBox" />
</asp:Content>
