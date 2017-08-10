<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Fee.Default" %>

<%@ Register Src="~/Controls/PagerBox.ascx" TagPrefix="uc1" TagName="PagerBox" %>
<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc2" TagName="ErrorBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table class="box" width="100%">
        <tr>
            <td style="white-space: nowrap">Từ ngày:
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" Width="70px" CssClass="datepicker" />
            </td>
            <td style="white-space: nowrap">Đến ngày:
            </td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" Width="70px" CssClass="datepicker" />
            </td>
            <td>Loại:
            </td>
            <td>
                <asp:DropDownList ID="ddlFeeType" runat="server" />
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
            <td>
                <asp:Button ID="btnAddFeeType" runat="server" Text="Thêm loại chi phí" OnClick="btnAddFeeType_Click" />
            </td>
            <td style="width: 100%"></td>
        </tr>
    </table>
    <div id="divData" runat="server" visible="false">
        <asp:GridView ID="gvData" runat="server" Width="100%" OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<% #Eval("FeeId") %>' OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="CreateDate" />
                <asp:BoundField DataField="FeeTypeId" HeaderText="FeeTypeId" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" />
                <asp:BoundField DataField="Note" HeaderText="Note" />
            </Columns>
        </asp:GridView>
        <table class="box" width="100%">
            <tr>
                <td align="right">
                    <uc1:PagerBox runat="server" ID="uctPager" OnSelectChange="uctPager_SelectChange" />
                </td>
            </tr>
        </table>
    </div>
    <uc2:ErrorBox runat="server" ID="ErrorBox" />
    <script type="text/javascript">
        $(".datepicker").datepicker({
            dateFormat: "dd/mm/yy",
            minDate: new Date(2017, 1 - 1, 1),
            changeYear: true,
            changeMonth: true,
            showButtonPanel: true
        });
    </script>
</asp:Content>
