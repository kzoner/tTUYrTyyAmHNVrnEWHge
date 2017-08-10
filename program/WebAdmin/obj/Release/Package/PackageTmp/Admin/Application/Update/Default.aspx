<%@ Page Title="" Language="C#" MasterPageFile="~/ContentPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdmin.Admin.Application.Update.Default" %>
<%@ Register src="~/Controls/ConfirmBox.ascx" tagname="ConfirmBox" tagprefix="uc1" %>
<%@ Register src="~/Controls/ErrorBox.ascx" tagname="ErrorBox" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <script type="text/javascript" language=javascript>

        function validate() { 
            
            var sData;
            sData = document.getElementById("ctl00_MasterPlaceHolder_txtAppName").value;
            if (sData == "") {
                alert("Chưa nhập tên ứng dụng");
                document.getElementById("ctl00_MasterPlaceHolder_txtAppName").focus();
                return false;
            }
            return true;
        }
    
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MasterPlaceHolder" runat="server">
    <table cellpadding="4" cellspacing="0" class="box">
        <tr>
            <td>
                Tên ứng dụng</td>
            <td>
                <asp:TextBox ID="txtAppName" runat="server" Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvAppName" runat="server" ControlToValidate="txtAppName"
                    ErrorMessage="Chưa nhập tên ứng dụng" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="regExAppName" runat="server"
                    ControlToValidate="txtAppName" Display="Dynamic" ErrorMessage="Tên ứng dụng không được có ký tự đặc biệt"
                    ValidationExpression="^([a-zA-Z0-9_\u00A1-\uFFFF\s])*$">
                    </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Mô tả</td>
            <td>
                <asp:TextBox ID="txtAppDescription" runat="server" Width="300px" Height="194px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Lưu" OnClientClick="return validate();" 
                    Width="80px" />
            </td>
        </tr>
    </table>
    <uc2:ErrorBox ID="uctErrorBox" runat="server" />
</asp:Content>