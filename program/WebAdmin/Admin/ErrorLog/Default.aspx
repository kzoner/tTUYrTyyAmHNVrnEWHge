<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.ErrorLog.Default" %>
<%@ Register src="../../Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table cellpadding="4" cellspacing="1" border="0" width="100%" class="Box">
        <tr>
            <td style="white-space:nowrap;">
                Từ ngày
            </td>
            <td>
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker" Width="70px"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;">
                Đến ngày
            </td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker" Width="70px"></asp:TextBox>
            </td>
            <td style="white-space:nowrap;">
                Username
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            </td>
            <td style="white-space:nowrap; width:100%;">
                <asp:Button ID="btnSubmit" runat="server" Text="Xem" width="80"
                    onclick="btnSubmit_Click" /> &nbsp;
                <asp:Button ID="btnClear" runat="server" Text="Clear" width="80"
                    onclick="btnClear_Click" OnClientClick="return confirm('Clear error log?');" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="gvErrorLog" runat="server" CellPadding="4" CellSpacing="1" 
        AutoGenerateColumns="false" AllowPaging="true" PageSize="25" 
        onpageindexchanging="gvErrorLog_PageIndexChanging" CssClass="Box">
        <Columns>
            <asp:BoundField HeaderText="Ngày" DataField="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="User" DataField="CurrentUser" />
            <asp:BoundField HeaderText="Chức năng" DataField="Path" />
            <asp:BoundField HeaderText="Method" DataField="TargetFunction" />
            <asp:BoundField headertext="Lỗi" DataField="Message" />
            <asp:BoundField headertext="StackTrace" DataField="StackTrace" />
        </Columns>
    </asp:GridView>
    
    <table runat="server" id="tblNoData" cellpadding="4" cellspacing="1" border="0" width="100%" visible="false" class="Box">
        <tr>
            <td class="DarkContent" style="font-weight:bold; text-align:center;">Chưa có dữ liệu</td>
        </tr>
    </table>
    <uc2:ErrorBox ID="uctErrorBox" runat="server" />
    
    <script type="text/javascript">
        $(".datepicker").datepicker({
            dateFormat: "dd/mm/yy",
            changeYear: true,
            changeMonth: true,
            showButtonPanel: true
        });
    </script>
</asp:Content>
