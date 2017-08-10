<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Resources.SetPermission.Default" %>

<%@ Register Src="~/Controls/ErrorBox.ascx" TagName="errorbox" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/NotifyBox.ascx" TagName="NotifyBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <asp:ScriptManager ID="scriptMan" runat="server" AllowCustomErrorsRedirect="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="updPanel" runat="server" EnableViewState="true">
        <ContentTemplate>
            <asp:Panel ID="pnlResourceDetail" runat="server">
                <table cellpadding="4" cellspacing="1" class="box" width="50%">
                    <tr>
                        <td style="white-space: nowrap" align="right">Ứng dụng:</td>
                        <td style="white-space:nowrap">
                            <asp:Label ID="lblApplicationName" runat="server" CssClass="boldText" ForeColor="Blue"></asp:Label>
                        </td>
                        <td style="white-space: nowrap" align="right">Tên tài nguyên:</td>
                        <td style="white-space:nowrap">
                            <asp:Label ID="lblResourceName" runat="server" CssClass="boldText" ForeColor="Blue"></asp:Label>
                        </td>
                        <td style="white-space: nowrap" align="right">Loại:</td>
                        <td style="white-space:nowrap">
                            <asp:Label ID="lblResourceType" runat="server" CssClass="boldText" ForeColor="Blue"></asp:Label>
                            <asp:Button ID="btn" runat="server" OnClick="btn_Click" Text="Test" Visible="false" />
                        </td>
                        <td align="right" style="white-space: nowrap">
                            <asp:UpdateProgress ID="udp" runat="server" AssociatedUpdatePanelID="updPanel"
                                DisplayAfter="200">
                                <ProgressTemplate>
                                    <span style="background-color: Yellow;">...Đang xử lý...</span>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlCurrentUser" runat="server">
                <table cellpadding="4" cellspacing="1" class="box" width="50%">
                    <tr>
                        <td style="white-space: nowrap; width: 50%;">User/nhóm cho phép</td>
                        <td style="white-space: nowrap; width: 50%;">Thao tác</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:ListBox ID="lstCurrentUsers" runat="server"
                                Rows="10" Width="100%" AutoPostBack="True"
                                OnSelectedIndexChanged="lstCurrentUsers_SelectedIndexChanged"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td valign="top" rowspan="2">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="white-space:nowrap" colspan="2" style="width: 50%; font-weight: bold; text-align: center;">Cho phép</td>
                                    <td style="width: 50%; font-weight: bold; text-align: center;">Cấm</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="white-space:nowrap">
                                        <asp:CheckBox ID="cbxAllOperation" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="cbxAllOperation_CheckedChanged" />
                                        Tất cả
                                    </td>
                                    <td style="width: 100%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">&nbsp;</td>
                                    <td style="white-space:nowrap">
                                        <asp:CheckBoxList ID="cbxListOperation" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cbxListOperation_SelectedIndexChanged">
                                        </asp:CheckBoxList>
                                    </td>
                                    <td style="white-space:nowrap">
                                        <asp:CheckBoxList ID="cbxListDenyOperation" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="cbxListDenyOperation_SelectedIndexChanged">
                                        </asp:CheckBoxList></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="right">
                                        <asp:Button ID="btnSave" runat="server" Text="Lưu"
                                            OnClick="btnSave_Click" />
                                    </td>
                                    <td align="right">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap" align="right">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBrowse" runat="server" Text="Browse"
                            OnClick="btnBrowse_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlAddUser" runat="server" Visible="false">
                <table cellpadding="4" cellspacing="1" class="box" width="50%">
                    <tr>
                        <td colspan="2">Thêm user / nhóm được sử dụng tài nguyên</td>
                    </tr>
                    <tr>
                        <td style="white-space:nowrap; width: 50%;" valign="top">
                            <asp:ListBox ID="lstNewUsers" runat="server" SelectionMode="Multiple" Height="150" Width="100%"></asp:ListBox>
                        </td>
                        <td style="width: 50%;" valign="top">
                            <table>
                                <tr>
                                    <td style="white-space:nowrap">Tìm theo</td>
                                    <td>
                                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlType_SelectedIndexChanged" Width="130">
                                            <asp:ListItem Value="0" Text="Tất cả" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="User"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Nhóm"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Tên:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                        <asp:Button ID="btnFind" runat="server" Text="Tìm" Width="35px"
                                            OnClick="btnFind_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnAdd" runat="server" Text="Thêm" OnClick="btnAdd_Click" />
                            &nbsp;
                        <asp:Button ID="btnClose" runat="server" Text="Đóng" OnClick="btnClose_Click" />
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlErrorBox" runat="server">
                <uc1:errorbox ID="ucErrorBox" runat="server" Message="" />
            </asp:Panel>
            <asp:Panel ID="pnlNotifyBox" runat="server">
                <uc2:NotifyBox ID="ucNotifyBox" OnNextClicked="btnNext_Click" runat="server" />
            </asp:Panel>
            <asp:GridView ID="gv" runat="server"></asp:GridView>
            <br />
            <asp:GridView ID="gvRemove" runat="server"></asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
