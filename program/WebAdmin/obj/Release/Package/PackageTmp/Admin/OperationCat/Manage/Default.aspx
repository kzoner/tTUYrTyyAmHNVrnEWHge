<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.OperationCat.Manage.Default" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc1" %>
<%@ Register src="~/Controls/Pager.ascx" tagname="Pager" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
<table cellpadding="0" cellspacing="0">
   <tr><td>
   <div class="box" style="padding:4px; text-align:right;">
    <asp:Button ID="btnCreate" runat="server" Text="Thêm mới" 
           PostBackUrl="../Create/" />
    </div>
   </td></tr>
    <tr>
        <td><div>
        <asp:GridView ID="dgridOperationCatList" runat="server" EnableTheming="True" 
        onrowdatabound="dgridApplicationList_RowDataBound" AutoGenerateColumns="False">
       <Columns>
           <asp:BoundField HeaderText="STT" >
           <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>
           <asp:TemplateField HeaderText="Tên">
               <EditItemTemplate>
                   <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:HyperLink ID="HyperLink1" runat="server" 
                       NavigateUrl='<%# Eval("OperationCode","../Edit/?code={0}") %>' 
                       Text='<%# Eval("Name") %>'></asp:HyperLink>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:BoundField DataField="Description" HeaderText="Mô tả" />
           <asp:TemplateField>
               <EditItemTemplate>
                   <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ApplicationID") %>'></asp:TextBox>
               </EditItemTemplate>
               <ItemTemplate>
                   <asp:LinkButton ID="lBtnDelete" runat="server" 
                       CommandArgument='<%# Eval("OperationCode") %>' 
                       onclientclick='<%# Eval("Name","return confirm(\"Bạn có chắc  chắn muốn xóa loại thao tác [{0}]?\");") %>' onclick="lBtnDelete_Click" 
                       >Xóa</asp:LinkButton>
                   <%--<asp:ImageButton ID="imgBtnDelete" runat="server" AlternateText="Xóa" 
                   CommandArgument='<%# Eval("OperationCode") %>'  ToolTip="Xóa"
                       onclientclick='<%# Eval("Name","return confirm(\"Bạn có chắc  chắn muốn xóa loại thao tác [{0}]?\");") %>' onclick="imgBtnDelete_Click"
                   />--%>
               </ItemTemplate>
           </asp:TemplateField>
       </Columns>
            
            
    </asp:GridView>
    </div>
        
    </td>
    </tr>
    <%--<tr>
        <td align="right"><div class="box" style="padding:4px;"><uc2:Pager ID="Pager1" runat="server" OnSelectChange="PagerSelect" /></div></td>
    </tr>--%>
   </table>
   
    
    
    <uc1:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>
