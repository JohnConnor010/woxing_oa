<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Priv_CheckEmail.aspx.cs" ClientIDMode="Static" Inherits="wwwroot.Manage.Private.Priv_CheckEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"  media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <script type="text/javascript" src="/App_Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="/App_Scripts/QueryString.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="HiddenField1" runat="server" />
    邮&nbsp;&nbsp;&nbsp;&nbsp;箱：<asp:TextBox ID="ui_email" runat="server" Columns="30" MaxLength="100" ></asp:TextBox>
        <asp:Button ID="Button1"
        runat="server" Text="发送验证码" onclick="Button1_Click" /><br />
        验证码：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnSubmit" runat="server" Text="邮箱验证" 
            onclick="btnSubmit_Click"/>
    </div>
    </form>
    <script type="text/javascript">
    var hiddenField = GetQueryString("HiddenField");
    function load()
    {
        document.getElementById("ui_email").value = window.parent.document.getElementById(hiddenField).value;
    }
    function close() {
        var value = document.getElementById("ui_email").value;
        window.parent.document.getElementById(hiddenField).value = value;
        window.parent.document.getElementById("dialogCase").style.display = 'none';
    }
    <%=mes %>
</script>
</body>
</html>
