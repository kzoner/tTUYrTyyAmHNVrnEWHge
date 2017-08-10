<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Function.Order.Add.Default" %>

<%@ Register Src="~/Controls/NotifyBox.ascx" TagPrefix="uc1" TagName="NotifyBox" %>
<%@ Register Src="~/Controls/ErrorBox.ascx" TagPrefix="uc2" TagName="ErrorBox" %>
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
            <td valign="top">
                <table>
                    <tr>
                        <td>Ngày tạo:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreateDate" runat="server" Width="200px" CssClass="datetimepicker"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvCreateDate" runat="server" ErrorMessage="*" ControlToValidate="txtCreateDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Loại:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAccountType" runat="server" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountType_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Mã đơn hàng:
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrderCode" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvOrderCode" runat="server" ErrorMessage="*" ControlToValidate="txtOrderCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAccount" runat="server" Text="Đối tác"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAccount" runat="server" Width="205px"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Phí vận chuyển:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTransportFee" runat="server" Width="200px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTransportFee" runat="server" ErrorMessage="*" ControlToValidate="txtTransportFee" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Ghi chú:
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="5" Width="200px"></asp:TextBox>
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
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>Sản phẩm:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProductType" runat="server" Width="105px" AutoPostBack="true" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProduct" runat="server" Width="205px" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>Số lượng:
                        </td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" Width="76px" Style="text-align: right" Text="0" CssClass="formatNumber"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="*" ControlToValidate="txtQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Đơn vị tính:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUnitType" runat="server" Width="105px" Enabled="False"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUnitValue" runat="server" Width="200px" Style="text-align: right" CssClass="formatNumber"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUnitValue" runat="server" ErrorMessage="*" ControlToValidate="txtUnitValue" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUnit" runat="server" Enabled="False"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Thêm" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvData" runat="server" Width="100%"
                    AutoGenerateColumns="false" OnRowCreated="gv_RowCreated" OnRowDataBound="gvData_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="Sản phẩm" DataField="ProductID" />
                        <asp:BoundField HeaderText="Mệnh giá" DataField="Price" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Số lượng" DataField="Quantity" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Thành tiền" DataField="Amount" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Đơn vị tính" DataField="UnitTypeId" />
                        <asp:BoundField HeaderText="Quy đổi" DataField="UnitValue" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField HeaderText="Đơn vị" DataField="UnitId" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" ForeColor="Red" Text="Xóa"
                                    OnClick="lbtnDelete_Click" CommandArgument='<%#Container.DataItemIndex %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfPrice" runat="server" />
    <uc1:NotifyBox runat="server" ID="NotifyBox" OnNextClicked="NotifyBox_NextClicked" />
    <uc2:ErrorBox runat="server" ID="ErrorBox" />

    <script type="text/javascript">
        $(".datetimepicker").datetimepicker({
            dateFormat: "dd/mm/yy",
            timeFormat: "HH:mm:ss",
            minDate: new Date(2017, 1 - 1, 1),
            changeYear: true,
            changeMonth: true,
            showButtonPanel: true
        });
    </script>
</asp:Content>
