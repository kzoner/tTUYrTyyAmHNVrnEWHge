<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Users.UserInRole.Default" %>
<%@ Register src="../../../Controls/Pager.ascx" tagname="Pager" tagprefix="uc1" %>
<%@ Register src="../../../Controls/ApplicationList.ascx" tagname="ApplicationList" tagprefix="uc2" %>
<%@ Register src="../../../Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <div class="box">
        <table border="0" cellpadding="4" cellspacing="0">
            <tr>
                <td>Ứng dụng 
                    <uc2:ApplicationList ID="ApplicationList1" runat="server" OnSelectChange="OnSelectChange" />
                </td>
                <td>Nhóm 
                    <asp:DropDownList ID="cmbRoles" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="cmbRoles_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td> Tìm theo
                    <asp:DropDownList ID="cmbSearchBy" runat="server" >
                        <asp:ListItem Selected="True" Value="0">All User</asp:ListItem>
                        <asp:ListItem Value="1">User Name</asp:ListItem>
                        <asp:ListItem Value="2">Email</asp:ListItem>
                        <asp:ListItem Value="3">Full Name</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtKeyWord" runat="server" Width="200px"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm" Width="90px" onclick="btnSearch_Click" 
                        /></td>
                <td align="right">
                    <asp:Button ID="btnCreate" runat="server" Text="Thêm mới" 
               Width="90px" onclick="btnCreate_Click" /></td>
            </tr>
        </table>
    </div>
    
    <div>
    <asp:GridView ID="dgridUserList" runat="server" EnableTheming="True" Width="100%" 
                    AutoGenerateColumns="False" onrowdatabound="dgridUserList_RowDataBound"
                    DataKeyNames="UserName" 
                    >
           <Columns>
               <asp:BoundField HeaderText="STT" >
               <ItemStyle HorizontalAlign="Center" />
               </asp:BoundField>
               <asp:BoundField HeaderText="UserName" DataField="UserName" />
               <asp:TemplateField HeaderText="Họ Tên">
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("FullName") %>' 
                           NavigateUrl='<%# Eval("UserName","../Update/?un={0}") + "&appID=" + Request.QueryString["appID"] + "&roleID=" + Request.QueryString["roleID"] %>'></asp:HyperLink>
                   </ItemTemplate>
                   <ItemStyle Wrap="False" />
               </asp:TemplateField>
               <asp:BoundField HeaderText="Email" DataField="Email" />
               <asp:TemplateField HeaderText="Đã khóa">
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Blocked") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("Blocked") %>' 
                           Enabled="False" />
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" />
               </asp:TemplateField>
               <asp:TemplateField>
                   <EditItemTemplate>
                       <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ApplicationID") %>'></asp:TextBox>
                   </EditItemTemplate>
                   <ItemTemplate>
                       <asp:LinkButton ID="lBtnDelete" runat="server" 
                           onclientclick='<%# Eval("FullName","return confirm(\"Bạn có chắc  chắn muốn xóa người dùng [{0}]?\");") %>' 
                           CommandArgument='<%# Eval("UserName") %>' onclick="lBtnDelete_Click"
                           >Xóa</asp:LinkButton>
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" />
               </asp:TemplateField>
           </Columns>

                <SelectedRowStyle BackColor="#FFAEAE" />

        </asp:GridView>
        
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr><td align="right" >
            <div class="box" style="padding:4px;">
                <uc1:pager ID="uctPager" runat="server" OnSelectChange="SelectChange" />
                </div>
            </td></tr>
        </table>
    </div>
    <uc3:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>
