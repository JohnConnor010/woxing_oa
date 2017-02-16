<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Flow_Priv.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Priv" ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    流程管理 >> 权限管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-modi" CurIndex="3" Param1="{Q:FlowID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <th width="50">
                    编号
                </th>
                <td width="140">
                    <span style="margin-left: 25px;">权限类型</span>
                </td>
                <td width="200">
                    授权范围
                </td>
                <td width="460">
                    部门范围
                </td>
                <td width="125">
                    操作
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>            
            <tr class="">
                <th width="50">
                    <%#Eval("ManageId") %>
                </th>
                <td>
                    <span style="margin-left: 25px;"><%#Eval("ManageType") %></span>
                </td>
                <td>
                    <%#Eval("DelegateScope") %>
                </td>
                <td class="vtip">
                    <%#Eval("Scope") %>
                </td>
                <td class="manage">
                    <a class="show" href="Flow_EditRole.aspx?Id=<%=Request.QueryString["Id"] %>&ManageId=<%#Eval("ManageId") %>">编辑</a>
                    <%--<a class="show" href="?action=delete">删除</a>--%>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("ManageId") %>' OnClientClick="return confirm('你真要删除这个定时器吗？')" CommandName="ManageId" OnCommand="btnDelete_Command"></asp:LinkButton>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>  
            <tr>
                <td colspan="5">
                    &nbsp;&nbsp;【<a href="Flow_AddRole.aspx?id=<%=Request.QueryString["Id"] %>">新建权限规则</a>】
                </td>
            </tr>          
        </tbody>
    </table>
</asp:Content>
