<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebAdmin.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background: url(/images/bg.jpg) #DDDDDD; background-repeat: repeat-y; background-position: center top;">
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellspacing="0" cellpadding="8">
                <tr>
                    <td align="center">
                        <img src="Images/logo_fpt.png" alt="logo_fpt" width="320" />
                    </td>
                </tr>
                <tr>
                    <td align="center"><span style="font-size: 20pt; color: red; font-weight: bold;">HỆ THỐNG THÔNG TIN</span></td>
                </tr>
                <tr>
                    <td align="center"><span style="font-size: 14pt; font-weight: bold;">CÔNG TY CỔ PHẦN DỊCH VỤ TRỰC TUYẾN GATE</span></td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>

                                <td><strong>Liên hệ:</strong></td>
                            </tr>
                            <tr>
                                <td>Địa chỉ liên hệ: 141 Lý Chính Thắng, Phường 7, Quận 3, Tp. HCM<br />
                                    Điện thoại: (84.8) 3526 8994<br />
                                    IP Phone: 755<br />
                                    Fax: (84.8) 3526 8998<br />
                                    Email: inside@gate.vn
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <img src="Images/map.jpg" class="box" />
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
                <tr>
                    <td align="center">Xin chào 
                        <asp:Label ID="lblUserName" runat="server" Font-Bold="true" Text="{Username}" CssClass="errorText"></asp:Label>,
                        <br />
                        Bạn đăng nhập lần cuối vào lúc
                        <asp:Label ID="lblLastLogin" runat="server" Font-Bold="true" Text="{LastLogin}" CssClass="errorText"></asp:Label>
                        tại địa chỉ IP
                        <asp:Label ID="lblLastIP" runat="server" Font-Bold="true" Text="{LastIP}" CssClass="errorText"></asp:Label>.
                        <br />
                        <br />
                        Chúc bạn một ngày làm việc vui vẻ!
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>