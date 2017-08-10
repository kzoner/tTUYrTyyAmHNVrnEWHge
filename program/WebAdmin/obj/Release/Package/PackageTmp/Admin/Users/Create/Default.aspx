<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Users.Create.Default" %>

<%@ Register Src="../../../Controls/ErrorBox.ascx" TagName="ErrorBox" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/ConfirmBox.ascx" TagName="ConfirmBox" TagPrefix="uc2" %>
<%@ Register Src="../../../Controls/ApplicationRolesList.ascx" TagName="ApplicationRolesList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Library/tab.js" type="text/javascript"></script>
    <link href="/App_Themes/Default/Tab.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">

    <asp:Panel ID="pnlCreationBox" runat="server" Width="900">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <table cellpadding="4px" class="box">
                        <tr>
                            <td align="right" style="white-space:nowrap">Tên đăng nhập:</td>
                            <td>
                                <asp:TextBox ID="txtUsername" runat="server" Width="300px" MaxLength="128"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="rvUsername" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                            </td>
                            <%--<td rowspan="10" valign="top">&nbsp;</td>--%>
                        </tr>
                        <tr>
                            <td align="right" style="white-space:nowrap">Mật khẩu:</td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="300px"
                                    MaxLength="64"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rvPassword" runat="server" Display="Dynamic"
                                    ErrorMessage="*" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="white-space:nowrap">Xác nhận mật khẩu:</td>
                            <td>
                                <asp:TextBox ID="txtCfPassword" runat="server" TextMode="Password"
                                    Width="300px" MaxLength="64"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rvCfPassword" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtCfPassword"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="white-space:nowrap">Họ tên:</td>
                            <td>
                                <asp:TextBox ID="txtFullName" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rvFullName" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtFullName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="white-space:nowrap">E-mail:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="300px" MaxLength="128"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rvEmail" runat="server"
                                    ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <div style="float: left;">
                                    <asp:CheckBox ID="chkBlocked" runat="server" Text="Khóa tài khoản" /></div>
                                <div style="float: right; text-align: right">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Tạo tài khoản"
                                        OnClick="btnSubmit_Click" />
                                </div>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td align="left">
                                <asp:CompareValidator ID="cvPassword" runat="server"
                                    ControlToCompare="txtCfPassword" ControlToValidate="txtPassword"
                                    ErrorMessage="Mật khẩu và xác nhận mật khẩu phải trùng nhau"
                                    SetFocusOnError="True"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="regMinPassword" runat="server"
                                    ControlToValidate="txtPassword"
                                    ErrorMessage="Mật khẩu ít nhất 6 ký tự"
                                    ValidationExpression=".{6}.*"></asp:RegularExpressionValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td align="left" style="white-space:nowrap">
                                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                    ControlToValidate="txtEmail" ErrorMessage="Địa chỉ email không hợp lệ"
                                    SetFocusOnError="True" Display="Dynamic"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                <asp:RegularExpressionValidator ID="regExUserName" runat="server"
                                    ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Tên đăng nhập không được có ký tự đặc biệt"
                                    ValidationExpression="^([a-zA-Z0-9_])*$">
                                </asp:RegularExpressionValidator>

                                <asp:RegularExpressionValidator ID="regExFullName" runat="server"
                                    ControlToValidate="txtFullName" Display="Dynamic" ErrorMessage="Họ tên không được có ký tự đặc biệt"
                                    ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td valign="top" style="white-space:nowrap">
                    <uc3:ApplicationRolesList ID="ApplicationRolesList1" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>


    <asp:Panel ID="pnlErrorBox" runat="server" Width="800">



        <uc1:ErrorBox ID="ucErrorBox" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlConfirmBox" runat="server" Width="800">
        <uc2:ConfirmBox ID="ucConfirmBox" runat="server"
            OnYesClicked="ucConfirmBox_YesClicked"
            OnNoClicked="ucConfirmBox_NoClicked" />
    </asp:Panel>


</asp:Content>
