<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="validator_demo.aspx.cs" Inherits="wwwroot.App_Demo.validator_demo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="../Style/sys_ui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" src="../App_js/validator.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table class="style1">
      
       <tr>
            <td class="style2">
                不允许为空：</td>
            <td>
                <asp:TextBox ID="ui_nonull" runat="server"  dataType="Require" msg="此项不允许为空！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                电子邮件：</td>
            <td>
                <asp:TextBox ID="ui_email" runat="server" dataType="Email" msg="格式不正确，请重新输入！"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="style2">
                固定电话：</td>
            <td>
                <asp:TextBox ID="ui_phone" runat="server" dataType="Phone" msg="电话格式不正确，区号-号码-分机，区号号码-分机！"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="style2">
                手机：</td>
            <td>
                <asp:TextBox ID="ui_mobile" runat="server" dataType="Mobile" msg="手机格式不正确！"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td class="style2">
                网址：</td>
            <td>
                <asp:TextBox ID="ui_url" runat="server" dataType="Url" msg="网址输入错误！"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="style2">
                身份证：</td>
            <td>
                <asp:TextBox ID="ui_idcard" runat="server" dataType="IdCard" msg="身份证输入错误！"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="style2">
                数字：</td>
            <td>
                <asp:TextBox ID="ui_number" runat="server" dataType="Number" msg="此项需输入数字！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                邮编：</td>
            <td>
                <asp:TextBox ID="ui_zip" runat="server" dataType="Zip" msg="邮编错误！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                QQ：</td>
            <td>
                <asp:TextBox ID="ui_qq" runat="server" dataType="QQ" msg="QQ号码错误"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                正负整数：</td>
            <td>
                <asp:TextBox ID="ui_integer" runat="server" dataType="Integer" msg="数字前面可带+ -号"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td class="style2">
                小数：</td>
            <td>
                <asp:TextBox ID="ui_double" runat="server" dataType="Double" msg="些项可输入小数"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                字母：</td>
            <td>
                <asp:TextBox ID="ui_english" runat="server" dataType="English" msg="此项只允许输入字母"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                汉字：</td>
            <td>
                <asp:TextBox ID="ui_chinese" runat="server" dataType="Chinese" msg="此项只允许输入汉字"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                自定义表达式：</td>
            <td>
                <asp:TextBox ID="ui_custom" runat="server" dataType="Custom" msg="必须输入规定的内容/^[A-Za-z]+$/" regexp="/^[A-Za-z]+$/"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" OnClientClick="return Validator.Validate(this.form,3);"  Text="提交"  />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
