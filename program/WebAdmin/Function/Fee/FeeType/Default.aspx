<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Fee.FeeType.Default" %>

<%@ Register Src="~/Controls/PagerBox.ascx" TagPrefix="uc1" TagName="PagerBox" %>
<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc2" TagName="ErrorBox" %>
<%@ Register Src="~/Controls/NotifyBox.ascx" TagPrefix="uc3" TagName="NotifyBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table class="box" width="100%">
        <tr>
            <td style="white-space: nowrap">Loại chi phí:
            </td>
            <td style="white-space: nowrap">
                <asp:TextBox ID="txtFeeTypeName" runat="server" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvfeeTypeName" runat="server" ErrorMessage="*" ControlToValidate="txtfeeTypeName" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td style="white-space: nowrap">
                <asp:Label ID="lblFeeTypeStatus" runat="server" Text="Trạng thái:" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFeeTypeStatus" runat="server" Visible="false"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Tra cứu" OnClick="btnSearch_Click" CausesValidation="false" />
            </td>
            <td>
                <asp:Button ID="btnExcel" runat="server" Text="Xuất Excel" OnClick="btnExcel_Click" CausesValidation="false" />
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="Thêm mới" OnClick="btnAdd_Click" CommandName="Add" />
            </td>
            <td style="width: 100%"></td>
        </tr>
    </table>
    <div id="divData" runat="server" visible="false">
        <asp:GridView ID="gvData" runat="server" Width="100%" OnRowDataBound="gvData_RowDataBound" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<% #Eval("FeeTypeId") %>' OnClick="lbtnEdit_Click" CausesValidation="false">Edit</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="FeeTypeName" HeaderText="Loại" />
                <asp:BoundField DataField="FeeTypeStatus" HeaderText="Trạng thái" />
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
    <uc3:NotifyBox runat="server" ID="NotifyBox" OnNextClicked="NotifyBox_NextClicked" />
    <asp:HiddenField ID="hfFeeTypeId" runat="server" />
</asp:Content>
