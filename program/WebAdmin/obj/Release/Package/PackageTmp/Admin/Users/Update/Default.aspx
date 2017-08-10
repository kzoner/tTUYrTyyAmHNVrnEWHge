<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Users.Update.Default" %>

<%@ Register Src="../../../Controls/ErrorBox.ascx" TagName="ErrorBox" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/ApplicationRolesList.ascx" TagName="ApplicationRolesList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Library/tab.js" type="text/javascript"></script>
    <link href="/App_Themes/Default/Tab.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <%--<asp:Panel ID="pnlCreationBox" runat="server" Width="900">
</asp:Panel>
    --%>
    <table cellpadding="0" cellspacing="0">
        <td valign="top">
            <table cellpadding="4" class="box">
                <tr>
                    <td align="right" style="white-space:nowrap">Tên đăng nhập:</td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" Width="300px" MaxLength="128"
                            ReadOnly="True"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="rvUsername" runat="server"
                            ErrorMessage="*" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="white-space:nowrap">Họ tên:</td>
                    <td>
                        <asp:TextBox ID="txtFullName" runat="server" Width="300px" MaxLength="50"
                            TabIndex="3"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="rvFullName" runat="server"
                            ErrorMessage="*" ControlToValidate="txtFullName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="white-space:nowrap">E-mail:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="300px" MaxLength="128"
                            TabIndex="4"></asp:TextBox>
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
                            <asp:CheckBox ID="chkBlocked" runat="server" Text="Khóa tài khoản"
                                TabIndex="7" Checked="True" />
                        </div>
                        <div style="float: right; text-align: right">
                            <asp:Button ID="btnSubmit"
                                runat="server" Text="Lưu"
                                OnClick="btnSubmit_Click" TabIndex="8" Width="100px" />
                        </div>

                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="left">
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Địa chỉ email không hợp lệ"
                            SetFocusOnError="True" Display="Dynamic"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="regExFullName" runat="server"
                            ControlToValidate="txtFullName" Display="Dynamic" ErrorMessage="Họ tên không được có ký tự đặc biệt"
                            ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td align="right">&nbsp;</td>
                </tr>
            </table>
        </td>
        <td valign="top" style="white-space:nowrap">
            <uc3:ApplicationRolesList ID="ApplicationRolesList1" runat="server" />
        </td>
    </table>


    <uc1:ErrorBox ID="ucErrorBox" runat="server" />
</asp:Content>
