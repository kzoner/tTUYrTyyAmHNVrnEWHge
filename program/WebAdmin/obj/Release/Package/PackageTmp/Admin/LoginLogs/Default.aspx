<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.LoginLogs.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table cellpadding="4" cellspacing="1" class="Box" width="98%">
        <tr style="white-space:nowrap;">
            <td>
                Từ ngày&nbsp;
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker" Width="70px" TabIndex="-1" onkeydown="return false;"></asp:TextBox>            
            </td>
            <td>
                Đến ngày&nbsp;
                <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker" Width="70px" TabIndex="-1" onkeydown="return false;"></asp:TextBox>
            </td>
            <td>
                User&nbsp;
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>            
            <td style="width:100%;">
                <asp:Button ID="btnSubmit" runat="server" Text="Xem" Width="80"
                    onclick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="gvLoginLogs" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="25" Width="70%"
        OnPageIndexChanging="gvLoginLogs_PageIndexChanging"
        CellPadding="4" CellSpacing="1">        
        <Columns>
            <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Ngày" DataField="Date" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
            <asp:BoundField HeaderText="User" DataField="UserName" />            
            <asp:BoundField HeaderText="IP" DataField="IP" />
            <%--<asp:BoundField HeaderText="Kết quả" DataField="LoginResult" />--%>
            <asp:TemplateField HeaderText="Kết quả">
                <ItemTemplate>
                    <%# LoginResult(int.Parse(Eval("LoginResult").ToString())) %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>        
    </asp:GridView>
    
    <table cellpadding="4" cellspacing="1" border="0" id="tblNoData" runat="server" class="Box">
        <tr class="Label">
            <td>Ngày</td>
            <td>User</td>
            <td>Đường dẫn</td>
            <td>Thao tác</td>
            <td>Mô tả</td>
            <td>IP</td>
        </tr>
        <tr class="SubCat">
            <td colspan="6" style="text-align:center;">Chưa có dữ liệu</td>
        </tr>
    </table>
    
    <table cellpadding="4" cellspacing="1" border="0" id="tblError" runat="server" class="Box">
        <tr class="Label">
            <td>Ngày</td>
            <td>User</td>
            <td>Đường dẫn</td>
            <td>Thao tác</td>
            <td>Mô tả</td>
            <td>IP</td>
        </tr>
        <tr class="SubCat">
            <td colspan="6" style="text-align:center;">Hệ thống có lỗi</td>
        </tr>
    </table>
    
    <script type="text/javascript">
        $(".datepicker").datepicker({
            dateFormat: "dd/mm/yy",
            changeYear: true,
            changeMonth: true,
            showButtonPanel: true
        });
    </script>
</asp:Content>
