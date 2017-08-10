<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuTree.aspx.cs" Inherits="WebAdmin.MenuTree" Theme="Default" %>

<%@ Register Src="Controls/LoginView.ascx" TagName="LoginView" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:XmlDataSource ID="xmlSrc" EnableCaching="false" EnableViewState="false" runat="server"></asp:XmlDataSource>
        <uc1:LoginView ID="LoginView1" runat="server" />
        <div>
            <asp:TreeView ID="tvwMenu" runat="server" ExpandDepth="1" ShowLines="true"
                CollapseImageToolTip="Thu gọn {0}" ExpandImageToolTip="Mở rộng{0}"
                OnSelectedNodeChanged="tvwMenu_SelectedNodeChanged">

                <RootNodeStyle Font-Bold="True" />
                <ParentNodeStyle Font-Bold="True" />
                <SelectedNodeStyle CssClass="MenuNodeSelecting" />

                <DataBindings>
                    <asp:TreeNodeBinding ValueField="MenuID" TextField="DisplayName" NavigateUrlField="Link" Target="content" />
                </DataBindings>
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
