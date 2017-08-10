<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Product.Edit.Default" %>

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
            })       //xóa trắng textbox tự nhập 0
            $(".formatNumber").keyup(function () {
                var currentId = $(this).attr('id');
                FormatInsertAmount(currentId);
            })       //bắt id để format
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
                <asp:DropDownList ID="ddlProductType" runat="server" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Mã sản phẩm:
            </td>
            <td>
                <asp:TextBox ID="txtProductCode" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvProductCode" runat="server" ErrorMessage="*" ControlToValidate="txtProductCode" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Tên sản phẩm:
            </td>
            <td>
                <asp:TextBox ID="txtProductName" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ErrorMessage="*" ControlToValidate="txtProductName" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Đơn giá:
            </td>
            <td>
                <asp:TextBox ID="txtPrice" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="*" ControlToValidate="txtPrice"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Chiểu dài:
            </td>
            <td>
                <asp:TextBox ID="txtLength" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvLength" runat="server" ErrorMessage="*" ControlToValidate="txtLength"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Chiểu rộng:
            </td>
            <td>
                <asp:TextBox ID="txtWidth" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvWidth" runat="server" ErrorMessage="*" ControlToValidate="txtWidth"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Chiểu sâu:
            </td>
            <td>
                <asp:TextBox ID="txtDepth" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvDepth" runat="server" ErrorMessage="*" ControlToValidate="txtDepth"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Chiểu cao:
            </td>
            <td>
                <asp:TextBox ID="txtHeight" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvHeight" runat="server" ErrorMessage="*" ControlToValidate="txtHeight"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Khối lượng:
            </td>
            <td>
                <asp:TextBox ID="txtWeigh" runat="server" Width="400px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvWeigh" runat="server" ErrorMessage="*" ControlToValidate="txtWeigh"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Đơn vị tính:
            </td>
            <td>
                <asp:DropDownList ID="ddlUnitType" runat="server" Width="405px" AutoPostBack="true" OnSelectedIndexChanged="ddlUnitType_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Quy đổi:
            </td>
            <td>
                <asp:TextBox ID="txtUnitValue" runat="server" Width="300px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
                <asp:DropDownList ID="ddlUnit" runat="server" Width="98px"></asp:DropDownList>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfvUnitValue" runat="server" ErrorMessage="*" ControlToValidate="txtUnitValue" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Hình:
            </td>
            <td>
                <asp:FileUpload ID="fuImage" runat="server" Width="400px" />
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
                <asp:DropDownList ID="ddlProductStatus" runat="server" Width="405px">
                </asp:DropDownList>
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
    <asp:HiddenField ID="hfImagePath" runat="server" />
    <asp:HiddenField ID="hfImageName" runat="server" />
</asp:Content>
