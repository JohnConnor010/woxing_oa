<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="SetPOP3.aspx.cs" Inherits="wwwroot.Manage.Email.SetPOP3" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="easyui1.4/jquery.min.js"></script>
    <script src="easyui1.4/jquery.easyui.min.js"></script>
    <link href="easyui1.4/themes/default/easyui.css" rel="stylesheet" />
    <link href="easyui1.4/themes/icon.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    邮件管理 >> 配置POP3邮箱    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="send_email" CurIndex="2" Param1="{Q:Run_Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tr>
            <td colspan="2">
                <h3>请仔细填写邮箱配置信息</h3>
                <h6>QQ邮箱的POP3配置信息请参阅<a href="http://kf.qq.com/faq/120322fu63YV130422aIrqAF.html" target="_blank">如何开启QQ邮箱的POP服务？</a></h6>
                <h6>163邮箱的POP3配置信息请参阅<a href="http://help.163.com/09/1221/09/5R20H8L100753VB9.html" target="_blank">什么是POP3、SMTP及IMAP？</a></h6>
            </td>
        </tr>
        <tr>
            <td style="width:100px"><b>POP3主机地址：</b></td>
            <td>
                <asp:TextBox ID="txtHostAddress" CssClass="easyui-textbox" runat="server" Width="200" Height="28"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px"><b>用户名：</b></td>
            <td>
                <asp:TextBox ID="txtUserName" CssClass="easyui-textbox" runat="server" Width="200" Height="28"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px"><b>登录密码：</b></td>
            <td>
                <asp:TextBox ID="txtPassword" CssClass="easyui-textbox" TextMode="Password" runat="server" Width="200" Height="28"></asp:TextBox>
                    
            </td>
        </tr>
        <tr>
            <td style="width:100px"><b>邮箱端口：</b></td>
            <td>
                <asp:TextBox ID="txtPort" CssClass="easyui-textbox" runat="server" Width="60" Height="28"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:100px"><b>启用SSL加密：</b></td>
            <td>
                <asp:CheckBox ID="chkSSL" runat="server" Text="SSL加密" Checked="true" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Button ID="btnSave" runat="server" Text="保存配置信息" OnClick="btnTestConnection_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
