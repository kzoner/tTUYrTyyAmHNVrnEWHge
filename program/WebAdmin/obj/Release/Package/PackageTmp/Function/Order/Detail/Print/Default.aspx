<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Inside.Function.Order.Detail.Print.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
</head>
<body style="background-color: White; font-size: 12px" onload="print();">
    <form id="form1" runat="server">
        <center>
            <div style="width: 100%">
                <table width="100%">
                    <tr>
                        <td style="white-space: nowrap; text-align: center">
                            <b>CTY TNHH ĐT-TM-DV-XNK HIỆP PHÁT</b>
                        </td>
                        <td style="white-space: nowrap; text-align: center">
                            <b>Mẫu số 02 - VT</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap; text-align: center">
                            <b>Địa chỉ: 1494/13/11A Quốc lộ 1A</b></td>
                        <td style="white-space: nowrap; text-align: center">
                            <b>(Ban hành theo QĐ số: 15/2006/QĐ-BTC</b></td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap; text-align: center">
                            <b>P.Tân Thới Hiệp, Q.12, TP.HCM</b>
                        </td>
                        <td style="white-space: nowrap; text-align: center">
                            <b>ngày 20/03/2006 của Bộ trưởng BTC)</b></td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap; text-align: center">
                            <b>ĐT: 0914799977 - 0938005523</b></td>
                        <td style="white-space: nowrap; text-align: center">&nbsp;</td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td align="center" style="font-size: 14px">
                            <b>PHIẾU XUẤT KHO</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            Ngày.......tháng.......năm.......
                        </td>
                    </tr>
                    <tr>
                        <td align="center">Số:........................................
                        </td>
                    </tr>
                </table>
                <div style="width: 100%; text-align: left">
                    <table width="100%">
                        <tr>
                            <td style="white-space:nowrap">
                                 - Họ và tên người nhận hàng:
                            </td>
                            <td style="white-space:nowrap">
                                <asp:Label ID="lblAccountName" runat="server" ></asp:Label>
                            </td>
                            <td style="width:100%"></td>
                        </tr>
                        <tr>
                            <td style="white-space:nowrap">
                                 - Địa chỉ (bộ phận):
                            </td>
                            <td style="white-space:nowrap">
                                <asp:Label ID="lblAddress" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space:nowrap">
                                 - Lý do xuất kho:
                            </td>
                            <td style="white-space:nowrap">
                                <asp:Label ID="lblNote" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="white-space:nowrap">
                                 - Xuất tại kho (ngăn lô):
                            </td>
                            <td style="white-space:nowrap">
                                ..........................................................................Địa điểm..........................................................................
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="gvData" runat="server" Width="100%" CellPadding="4" CellSpacing="0" AutoGenerateColumns="False" EnableModelValidation="True" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" OnRowDataBound="gvData_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="STT">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </AlternatingItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProductID" HeaderText="Tên sản phẩm">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Price" DataFormatString="{0:N0}" HeaderText="Mệnh giá">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" DataFormatString="{0:N0}" HeaderText="Số lượng">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Amount" DataFormatString="{0:N0}" HeaderText="Thành tiền">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnitTypeId" HeaderText="Đơn vị tính">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnitValue" DataFormatString="{0:N0}" HeaderText="Quy đổi">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnitId" HeaderText="Đơn vị">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    - Tổng số tiền (viết bằng chữ):
                        <asp:Label ID="lblNumberWord" runat="server"></asp:Label></b><br />
                    - Số chứng từ gốc kèm theo: ...........................................................................................................................
                    <br />
                </div>
            </div>
        </center>
        <br />
        <div style="text-align: right">
            Ngày...........tháng...........năm...........
        </div>
        <table width="100%">
            <tr>
                <td style="text-align: center">Người lập phiếu
                </td>
                <td style="text-align: center">Người nhận hàng
                </td>
                <td style="text-align: center">Thủ kho
                </td>
                <td style="text-align: center">Kế toán trưởng
                </td>
                <td style="text-align: center">Giám đốc
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td style="text-align: center">(Hoặc bộ phận có nhu cầu nhập)
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: center">(Ký, họ tên)
                </td>
                <td style="text-align: center">(Ký, họ tên)
                </td>
                <td style="text-align: center">(Ký, họ tên)
                </td>
                <td style="text-align: center">(Ký, họ tên)
                </td>
                <td style="text-align: center">(Ký, họ tên)
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
