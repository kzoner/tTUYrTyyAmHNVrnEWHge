<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Users.RemovePermission.Default" %>
<%@ Register src="../../../Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc1" %>
<%@ Register src="../../../Controls/NotifyBox.ascx" tagname="NotifyBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    td label
    {
        display:none;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
<asp:ScriptManager ID="scriptMan" runat="server" AllowCustomErrorsRedirect="true"></asp:ScriptManager>

<asp:UpdatePanel ID="updPanel" runat="server" EnableViewState="true">
    <ContentTemplate>
        <table cellpadding="4" cellspacing="1" class="Box">
            <tr style="white-space:nowrap;">
                <td>
                    Ứng dụng:
                </td>
                <td>
                    <asp:DropDownList ID="ddlApplications" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Username:
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td style="width:100%;">
                    <asp:Button ID="btnSearch" runat="server" Text="Tìm" Width="80" 
                        onclick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <div style="width:70%;">
            <div style="text-align:right;">                
                <asp:Button ID="btnDeleteAll" runat="server" Text="Xóa toàn bộ" Visible="false" OnClientClick="return confirm('Xóa toàn bộ quyền của user này?');"
                    onclick="btnDeleteAll_Click" />
            </div>
            <div>
                <asp:GridView ID="gvPermissionList" runat="server" AutoGenerateColumns="false" Width="100%"
                    RowStyle-Wrap="false" AlternatingRowStyle-Wrap="false" AllowPaging="true" 
                    PageSize="25" onpageindexchanging="gvPermissionList_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Tài nguyên">
                            <ItemTemplate>
                                <%# Eval("FullPath").ToString() == "" ? Eval("ResourceName") : "<a href=" + Eval("FullPath") + " target='_blank'>" + Eval("ResourceName") + "</a>" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Thao tác" DataField="OperationName" />
                        <asp:TemplateField HeaderText="Cho phép" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="40">
                            <ItemTemplate>  
                                <asp:CheckBox ID="cbxAllow" runat="server" Text='<%# Container.DataItemIndex %>' Enabled="false"
                                    Checked='<%# MapStatus(int.Parse(Eval("Allow").ToString()))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>                    
                        <asp:TemplateField HeaderText="Cấm" ItemStyle-HorizontalAlign="Center" ControlStyle-Width="40">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbxDeny" runat="server"  Text='<%# Container.DataItemIndex %>' Enabled="false"
                                    Checked='<%# MapStatus(int.Parse(Eval("Deny").ToString()))%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument=<%# Eval("PermissionID") %> ForeColor="Red"
                                    OnClientClick='<%# Eval("ResourceName", "return confirm(\"Gỡ bỏ user khỏi tài nguyên [{0}]?\");") %>'
                                    OnClick="lbtnDelete_Clicked">Xóa</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                Quyền theo Role:
                <br />
                <asp:GridView ID="gvRolePermissionList" runat="server" AutoGenerateColumns="false" Width="100%"
                    RowStyle-Wrap="false" AlternatingRowStyle-Wrap="false" AllowPaging="true" 
                    PageSize="25" onpageindexchanging="gvRolePermissionList_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="STT" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Tài nguyên">
                            <ItemTemplate>
                                <%# Eval("FullPath").ToString() == "" ? Eval("ResourceName") : "<a href=" + Eval("FullPath") + " target='_blank'>" + Eval("ResourceName") + "</a>" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Thao tác" DataField="OperationName" />                        
                    </Columns>
                </asp:GridView>
            </div>            
            <div style="text-align:right; display:none;">
                <%--<asp:Button ID="btnSubmit" runat="server" Text="Cập nhật" Visible="false" />--%>
            </div>
        </div>
        <uc1:ErrorBox ID="ucErrorBox" runat="server" Visible="false" />
        <uc2:NotifyBox ID="ucNotifyBox" runat="server" Visible="false" OnNextClicked="Next_Clicked" />
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
