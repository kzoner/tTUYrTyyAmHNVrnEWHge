<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Fee.Edit.Default" %>

<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc1" TagName="ErrorBox" %>
<%@ Register Src="~/Controls/NotifyBox.ascx" TagPrefix="uc2" TagName="NotifyBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $(".formatNumber").click(function () {
                this.select();
            });      //click select textbox
            $(".formatNumber").change(function () {
                if (this.value == '')
                    this.value = '0';
            });       //xóa trắng textbox tự nhập 0
            $(".formatNumber").keyup(function () {
                var currentId = $(this).attr('id');
                FormatInsertAmount(currentId);
            });       //bắt id để format
        });
        function FormatInsertAmount(costName) {
            var cost = document.getElementById(costName).value
            if (cost != '') {
                document.getElementById(costName).value = FormatN(cost) == 'NaN' ? '0' : FormatN(cost)
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table class="box">
        <tr>
            <td>Loại:
            </td>
            <td>
                <asp:DropDownList ID="ddlFeeType" runat="server" Width="405px"></asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Tiền:
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="*" ControlToValidate="txtAmount"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Người dùng:
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ErrorMessage="*" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
            </td>
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
                <asp:DropDownList ID="ddlFeeStatus" runat="server" Width="405px"></asp:DropDownList>
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

