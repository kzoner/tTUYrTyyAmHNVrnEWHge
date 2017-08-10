<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Roles.Manage.Default" Theme="Default" %>
<%@ Register src="../../../Controls/ApplicationList.ascx" tagname="ApplicationList" tagprefix="uc1" %>
<%@ Register src="../../../Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table cellpadding="0" cellspacing="0" border="0">
    <tr><td><div class="box divControl" >
        <table width="100%" cellpadding="0" cellspacing="0" >
            <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Ứng dụng"></asp:Label><uc1:ApplicationList ID="ucApplicationList" runat="server" OnSelectChange="OnSelectChange" /></td>
            <td align="right" > <asp:Button ID="btnCreate" runat="server" Text="Thêm mới" 
                    PostBackUrl="../Create/Default.aspx" /></td>
            </tr>
        </table>
    
       
 </div></td></tr>
    <tr><td><div>
        <asp:GridView ID="dgridRoles" runat="server" EnableTheming="True" 
            AutoGenerateColumns="False" onrowdatabound="dgridRoles_RowDataBound" 
            Width="100%" >
            <Columns>
                <asp:BoundField HeaderText="STT" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RoleCode" HeaderText="Mã Role" />
                <asp:TemplateField HeaderText="Tên">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink2" runat="server" 
                            
                            NavigateUrl='<%# Eval("RoleID","/Admin/Users/UserInRole/?roleID={0}") + Eval("ApplicationID","&appID={0}") %>' 
                            Text='<%# Eval("RoleName") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField >
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text="Sửa"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl='<%# Eval("RoleID","../Update/?id={0}") %>' 
                            Text='Sửa'></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" 
                            CommandArgument='<%# Eval("RoleID") %>' 
                            
                            onclientclick='<%# Eval("RoleName","return confirm(\"Bạn có chắc chắn muốn xóa Role [{0}]?\");") %>' 
                            onclick="lbtnDelete_Click">Xóa</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <uc2:ErrorBox ID="uctErrorBox" runat="server" />
    </div></td></tr>
    </table>
    
    
</asp:Content>
