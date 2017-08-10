<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Resources.Manage.Default" Debug="true" %>

<%@ Register Src="~/Controls/ErrorBox.ascx" TagName="errorbox" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">

    <asp:Panel ID="pnlSearchBox" runat="server">
        <table cellpadding="4" cellspacing="1" width="100%" class="box">
            <tr>
                <td style="white-space: nowrap;">Chọn ứng dụng: 
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlApplications" runat="server" AutoPostBack="true" CausesValidation="false"
                        OnSelectedIndexChanged="ddlApplications_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td style="white-space: nowrap">Tra cứu</td>
                <td style="white-space: nowrap">
                    <asp:DropDownList ID="ddlSearchBy" runat="server"
                        OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="name" Text="Tên tài nguyên"></asp:ListItem>
                        <asp:ListItem Value="path" Text="Đường dẫn"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="white-space: nowrap">
                    <asp:TextBox ID="txtKeyword" runat="server" Width="586px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm"
                        OnClick="btnSearch_Click" />
                </td>
                <td style="width: 100%;">
                    <input type="button" id="btnAdd" value="Thêm mới" style="width: 80px;" onclick="location.href = '../Add/'" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="pnlDataGridBox" runat="server">
        <asp:GridView ID="dgridResource" runat="server" AutoGenerateColumns="False"
            Width="100%" CellPadding="4" CellSpacing="1" EnableTheming="True">
            <Columns>
                <asp:TemplateField HeaderText="Tên tài nguyên">
                    <ItemTemplate>
                        <a title="Cập nhật" href="../Edit/?id=<%#Eval("ResourceID") %>"><%#Eval("ResourceName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Mã loại" DataField="ResourceTypeCode"
                    ItemStyle-Wrap="false">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Đường dẫn" DataField="Path"
                    ItemStyle-Wrap="false">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Tên file" DataField="FileName"
                    ItemStyle-Wrap="false">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Enabled" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxEnabled" runat="server" Checked='<%#Eval("Status") %>' Text='<%# Eval("ResourceID") %>'
                            OnCheckedChanged="cbxStatus_CheckChanged" AutoPostBack="true" CssClass="hideCheckBoxLabel"
                            Visible='<%#ShowDeleteButton(Eval("ResourceName").ToString(), int.Parse(Eval("ApplicationID").ToString())) %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="../SetPermission/?id=<%#Eval("ResourceID") %>">Phân quyền</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ForeColor="Red" ID="lbtnDelete" runat="server" Text="Xóa" CommandArgument='<%#Eval("ResourceID") %>'
                            OnClick="lbtnDelete_Clicked" OnClientClick='<%#Eval("ResourceName", "return confirm(\"Bạn muốn xóa tài nguyên [{0}]?\");") %>'
                            Visible='<%#ShowDeleteButton(Eval("ResourceName").ToString(), int.Parse(Eval("ApplicationID").ToString())) %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="box" style="padding: 4px; text-align: right;">
            <uc2:Pager ID="Pager1" runat="server" OnSelectChange="PagerSelect" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNoDataBox" runat="server" Width="800">
        <%--<table cellpadding="4" cellspacing="1" width="100%">
            <tr class="Label">
                <td>Tên tài nguyên</td>
                <td>Mã loại</td>
                <td>Ứng dụng</td>
                <td>Đường dẫn</td>
                <td>Tên file</td>                
            </tr>
            <tr>
                <td colspan="5" class="DarkContent">Chưa có dữ liệu</td>
            </tr>
        </table>--%>

        <div class="DarkContent">Chưa có dữ liệu.</div>
    </asp:Panel>

    <asp:Panel ID="pnlErrorBox" runat="server" Width="800">
        <uc1:errorbox ID="ucErrorBox" runat="server" Message="" />
    </asp:Panel>

</asp:Content>
