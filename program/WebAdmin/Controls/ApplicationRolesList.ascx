<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApplicationRolesList.ascx.cs" Inherits="WebAdmin.Controls.ApplicationRolesList" %>
<!-- This is the box that all of the tabs and contents of 
         the tabs will reside -->
    
<%--    <link href="../App_Themes/Default/Tab.css" rel="stylesheet" 
    type="text/css" />--%>
    
    <div id="tabs_container" >
    
      <!-- These are the tabs -->
      <%--<ul class="tabs">
        <li class="active"><a href="#" rel="#tab_1_contents" class="tab">Report</a></li>
        <li><a href="#" rel="#tab_2_contents" class="tab">Editor</a></li>
        <li><a href="#" rel="#tab_3_contents" class="tab">Developer</a></li>
        <li><a href="#" rel="#tab_4_contents" class="tab">Aministrators</a></li>     
      </ul>--%>
        
        <asp:Repeater ID="rptTabTitle" runat="server">
        <HeaderTemplate><ul class="tabs"></HeaderTemplate>
        <ItemTemplate><li <%# Eval("TitleActive") %>><a href="#" rel="#tab_<%# Eval("Index") %>_contents" class="tab"><%#Eval("ApplicationName") %></a></li></ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
      <!-- This is used so the contents don't appear to the 
           right of the tabs -->
      <div class="clear"></div>
      
     <!-- This is a div that hold all the tabbed contents -->
      <div class="tab_contents_container">
    
        <!-- Tab 1 Contents -->
        <%--<div id="tab_1_contents" class="tab_contents tab_contents_active">
          I'm Good Enough!
        </div>--%>
    
        <!-- Tab 2 Contents -->
        <%--<div id="tab_2_contents" class="tab_contents">
          I'm Smart Enough!
        </div>--%>
    
          <asp:Repeater ID="rptTabContent" runat="server" >
            <HeaderTemplate></HeaderTemplate>
            <ItemTemplate>
                <div id="tab_<%# Eval("Index") %>_contents" <%# Eval("ContentActive") %>>
                
                    <%--<asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Text="Role"  /><br />--%>
                    
                    <asp:CheckBoxList ID="cblRoles" runat="server">
          <%--<asp:ListItem Value="1212">Role 1</asp:ListItem>
          <asp:ListItem Value="1414">Role 2</asp:ListItem>--%>
                        
                    </asp:CheckBoxList>
                </div>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
          </asp:Repeater>
      </div>      
    </div>





