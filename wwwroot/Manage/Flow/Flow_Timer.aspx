<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Flow_Timer.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Timer"
    ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.timespinner.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    流程管理 >> 流程定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-modi" CurIndex="6" Param1="{Q:FlowID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td width="140">
                    <span style="margin-left: 25px;">定时类型</span>
                </td>
                <td width="265">
                    流程发起人
                </td>
                <td width="210">
                    提醒时间
                </td>
                <td width="100">
                    最后时间
                </td>
                <td width="115">
                    操作
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="TimerRepeater" runat="server">
            <ItemTemplate>
            <tr class="">
                <td>
                    <span style="margin-left: 25px;"><%#Eval("RemindType") %></span>
                </td>
                <td>
                    <span style="color: #666; font-weight: bold;"><%#Eval("UserList") %></span>
                </td>
                <td>
                    <%#Eval("RemindTime") %>
                </td>
                <td>
                    <%#Eval("LastTime") %>
                </td>
                <td class="manage">
                    <a class="show" href="Flow_EditTimer.aspx?timerId=<%#Eval("Id") %>&id=<%=Request.QueryString["ID"] %>">编辑</a>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("Id") %>' OnCommand="btnDelete_Command"></asp:LinkButton>
                </td>
            </tr>            
            </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="4">&nbsp;&nbsp;【<a href="Flow_AddTimer.aspx?id=<%=Request.QueryString["ID"] %>">添加定时任务</a>】</td>
            </tr>
        </tbody>
    </table>
</asp:Content>
