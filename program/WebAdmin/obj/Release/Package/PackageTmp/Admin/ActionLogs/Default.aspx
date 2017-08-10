<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.ActionLogs.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table cellpadding="4" cellspacing="1" class="Box" width="98%">
        <tr style="white-space:nowrap;">
            <td style="white-space:nowrap;">
                Từ ngày&nbsp;
                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="-1" onkeydown="return false;" CssClass="datepicker" Width="70px"></asp:TextBox>            
            </td>
            <td style="white-space:nowrap;">
                Đến ngày&nbsp;
                <asp:TextBox ID="txtToDate" runat="server" TabIndex="-1" onkeydown="return false;" CssClass="datepicker" Width="70px"></asp:TextBox>
            </td>            
            <td style="white-space:nowrap;">
                Tìm theo&nbsp;
                <asp:DropDownList ID="ddlSearchType" runat="server">
                </asp:DropDownList>
            </td>
            <td style="white-space:nowrap;">
                Từ khóa&nbsp;
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
            </td>
            <td style="width:100%;">
                <asp:Button ID="btnSubmit" runat="server" Text="Xem" Width="80"
                    onclick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    
    <asp:GridView ID="gvActionLogs" runat="server" AutoGenerateColumns="false" Width="100%"
        AllowPaging="true" PageSize="25"
        CellPadding="4" CellSpacing="1" 
        onpageindexchanging="gvActionLogs_PageIndexChanging">        
        <Columns>
            <asp:BoundField HeaderText="Ngày" DataField="Date" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField HeaderText="User" DataField="UserName" />
            <asp:BoundField HeaderText="Đường dẫn" DataField="Path" />
            <asp:BoundField HeaderText="Thao tác" DataField="Operation" />
            <asp:BoundField HeaderText="Mô tả" DataField="Data" />
            <asp:BoundField HeaderText="IP" DataField="IP" />
        </Columns>        
    </asp:GridView>        
    <table cellpadding="4" cellspacing="1" border="0" id="tblNoData" runat="server" class="Box" width="100%">
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
    
    <table cellpadding="4" cellspacing="1" border="0" id="tblError" runat="server" class="Box" width="100%">
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
