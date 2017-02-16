<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JqueryValidateBoxDemo.aspx.cs" Inherits="wwwroot.App_Demo.JqueryValidateBoxDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="../App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="style1">
        <tr>
            <td class="style2">
                正则格式验证（不能解决）：</td>
            <td>
                <asp:TextBox ID="txtRegex" runat="server" class="easyui-validatebox" validType="regex[/^\d{1,10}$/]" required="true" invalidMessage="1-10位数字！"></asp:TextBox>
            </td>
        </tr>        <tr>
            <td class="style2">
                不允许为空：</td>
            <td>
                <asp:TextBox ID="txtNoNull" runat="server" class="easyui-validatebox" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                电子邮件：</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" class="easyui-validatebox" validType="email" required="true" invalidMessage="格式不正确，请重新输入！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                真实姓名：</td>
            <td>
                <asp:TextBox ID="txtRealName" runat="server" CssClass="easyui-validatebox" validType="chinese" required="true" invalidMessage="用户姓名不正确！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                手机号码：</td>
            <td>
                <asp:TextBox ID="txtTelephone" runat="server" CssClass="easyui-validatebox" validType="mobile" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                邮政编码;</td>
            <td>
                <asp:TextBox ID="txtZip" runat="server" CssClass="easyui-validatebox" validType="zip" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                固定电话：</td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="easyui-validatebox" validType="phone" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                身份证号码：</td>
            <td>
                <asp:TextBox ID="txtIDCard" runat="server" CssClass="easyui-validatebox" validType="idcard" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                排序号;</td>
            <td>
                <asp:TextBox ID="txtSort" runat="server" CssClass="easyui-validatebox" validType="number" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                QQ号码：</td>
            <td>
                <asp:TextBox ID="txtQQ" runat="server" CssClass="easyui-validatebox" validType="qq" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                字符长度(10位到100位)</td>
            <td>
                <asp:TextBox ID="txtNumber" runat="server" CssClass="easyui-validatebox" validType="length[10,100]" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                数字大小限制(从10到100)</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="easyui-validatebox" validType="between[10,100]" required="true" invalidMessage="10位到100位！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                数字范围限制(从2,3,4,5,6中选一个)</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="easyui-validatebox" validType="among[2,3,4,5,6]" required="true" invalidMessage="从（2,3,4,5,6）数字中取一个！"></asp:TextBox>
            </td>
        </tr>        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  Text="提交"  />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
