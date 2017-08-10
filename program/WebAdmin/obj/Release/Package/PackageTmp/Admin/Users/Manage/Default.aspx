<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Users.Manage.Default" %>

<%@ Register Src="../../../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Src="../../../Controls/ErrorBox.ascx" TagName="ErrorBox" TagPrefix="uc2" %>
<%@ Register Src="../../../Controls/ApplicationRolesList.ascx" TagName="ApplicationRolesList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Library/tab.js" type="text/javascript"></script>
    <style type="text/css">
        .selected {
            background-color: #FFD4C4;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $("#ctl00_MasterPlaceHolder_dgridUserList tr:gt(0)").click(
                function () {
                    $(this).addClass("selected");
                }
            );

        });

    </script>
    <link href="/App_Themes/Default/Tab.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table cellpadding="0" cellspacing="0">
        <tr>

            <td valign="top">
                <div class="box">
                    <table cellpadding="4px" cellspacing="0" border="0">
                        <tr>
                            <td style="white-space:nowrap">Tìm theo</td>
                            <td>
                                <asp:DropDownList ID="cmbSearchBy" runat="server">
                                    <asp:ListItem Value="1">User Name</asp:ListItem>
                                    <asp:ListItem Value="2">Email</asp:ListItem>
                                    <asp:ListItem Value="3">Full Name</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtKeyWord" runat="server" Width="200px"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Tìm" Width="90px" OnClick="btnSearch_Click" /></td>
                            <td align="right">
                                <asp:Button ID="btnCreate" runat="server" Text="Thêm mới"
                                    PostBackUrl="../Create/" Width="90px" /></td>
                        </tr>
                    </table>
                </div>
                <div>


                    <asp:GridView ID="dgridUserList" runat="server" EnableTheming="True" Width="100%"
                        AutoGenerateColumns="False" OnRowDataBound="dgridUserList_RowDataBound"
                        OnSelectedIndexChanged="dgridUserList_SelectedIndexChanged"
                        DataKeyNames="UserName">
                        <Columns>
                            <asp:BoundField HeaderText="STT">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Họ Tên">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("FullName") %>'
                                        NavigateUrl='<%# Eval("UserName","../Update/?un={0}") %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="UserName" DataField="UserName" />
                            <asp:BoundField HeaderText="Email" DataField="Email" />
                            <asp:TemplateField HeaderText="Đã khóa">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Blocked") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("Blocked") %>' Text='<%# Eval("UserName") %>'
                                        OnCheckedChanged="CheckBox1_CheckChanged" CssClass="hideCheckBoxLabel" AutoPostBack="true" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField SelectText="Roles" ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ApplicationID") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lBtnDelete" runat="server"
                                        OnClientClick='<%# Eval("FullName","return confirm(\"Bạn có chắc  chắn muốn xóa người dùng [{0}]?\");") %>'
                                        CommandArgument='<%# Eval("UserName") %>' OnClick="lBtnDelete_Click">Xóa</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                        <SelectedRowStyle BackColor="#FFAEAE" />

                    </asp:GridView>



                </div>

                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right">
                            <div class="box" style="padding: 4px;" runat="server" id="divPager">
                                <uc1:Pager ID="uctPager" runat="server" OnSelectChange="SelectChange" />
                            </div>
                        </td>
                    </tr>
                </table>

            </td>
            <td valign="top" align="left" width="100%" style="white-space:nowrap">
                <uc3:ApplicationRolesList ID="ApplicationRolesList1" runat="server" />
                <br />
                <asp:Button ID="btnUpdateRole" runat="server" Text="Cập nhật" OnClick="btnUpdateRole_Click" />
            </td>
        </tr>
    </table>

    <uc2:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>
