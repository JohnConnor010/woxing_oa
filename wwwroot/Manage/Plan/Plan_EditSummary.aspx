﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_EditSummary.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_EditSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <b>您现在操作的是：工作总结提交操作</b>
    <table class="table">
        <tr>
            <td>
                <b>计划类型：</b><asp:Literal ID="litype" runat="server"></asp:Literal>
            </td>
            <td>
                <b>开始时间：</b><asp:Literal ID="liStarttime" runat="server"></asp:Literal>
            </td>
            <td>
                <b>结束时间：</b><asp:Literal ID="liStoptime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                <b>员工姓名：</b><asp:Literal ID="lirealname" runat="server"></asp:Literal>
            </td>
            <td>
                <b>预期数量：</b><asp:Literal ID="litotal" runat="server"></asp:Literal>
            </td>
            <td>
                <b>完成数量：</b><asp:Literal ID="licurr" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <b>计划标题：</b><asp:Literal ID="lititle" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <b>描述：</b><asp:Literal ID="licontent" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <b>总结：</b><asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="4" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="Button1" runat="server" Text="提交" onclick="Button1_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>