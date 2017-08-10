<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Inside Web Management System - GATE</title>
</head>

<frameset cols="235,8,*" frameborder="no" border="0" framespacing="0"  id="main">
      <frame src="MenuTree.aspx" name="menu" id="menu" scrolling="auto">
      <frame name="spacer" id="spacer"  src="resizer.htm">
      <frame src="Home.aspx" name="content" id="content">  
      <noframe>a
      Sorry, your browser does not handle frames!
      </noframe>
</frameset>
<body>
</body>
</html>
