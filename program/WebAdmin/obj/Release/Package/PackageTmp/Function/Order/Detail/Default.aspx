<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Order.Detail.Default" %>

<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc1" TagName="ErrorBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table>
        <tr>
            <td align="right">Loại:
            </td>
            <td>
                <asp:Label ID="lblOrderType" runat="server" Text="" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblAccountType" runat="server" Text=""></asp:Label>:
            </td>
            <td>
                <asp:Label ID="lblAccountName" runat="server" Text="" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">Mã đơn hàng:
            </td>
            <td>
                <asp:Label ID="lblOrderCode" runat="server" Text="" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">Phí vận chuyển:
            </td>
            <td>
                <asp:Label ID="lblTransportFee" runat="server" Text="" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">Ghi chú:
            </td>
            <td>
                <asp:Label ID="lblNote" runat="server" Text="" Style="font-weight: 700"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound">
        <Columns>
            <asp:BoundField HeaderText="Sản phẩm" DataField="ProductID" />
            <asp:BoundField HeaderText="Mệnh giá" DataField="Price" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Số lượng" DataField="Quantity" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Thành tiền" DataField="Amount" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Đơn vị tính" DataField="UnitTypeId" />
            <asp:BoundField HeaderText="Quy đổi" DataField="UnitValue" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField HeaderText="Đơn vị" DataField="UnitId" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="btnPrint" runat="server" Text="Bản in" OnClick="btnPrint_Click" />
    <uc1:ErrorBox runat="server" ID="ErrorBox" />
</asp:Content>
