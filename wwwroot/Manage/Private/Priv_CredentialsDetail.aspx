<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Priv_CredentialsDetail.aspx.cs" ClientIDMode="Static"
    Inherits="wwwroot.Manage.Sys.Priv_CredentialsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <table class="table">
        <tr>
            <td> <b>证件名称：</b>
                <asp:Literal ID="ui_name" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;&nbsp;<b>发证单位：</b>&nbsp;
                <asp:Literal ID="ui_unit" runat="server"></asp:Literal>
                &nbsp;&nbsp;&nbsp;&nbsp;<b>发证时间：</b>&nbsp;
                <asp:Literal ID="ui_ctime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td valign="top"> <b>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</b>
                <asp:Literal ID="ui_content" runat="server"></asp:Literal>
            </td>
        </tr>
        
        <tr>
            <td>
                <asp:Image ID="Image1" runat="server"/>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        if (document.getElementById("Image1").clientWidth > 1050) {
            document.getElementById("Image1").style.width = "1050px";
        }
    </script>
</body>
</html>
