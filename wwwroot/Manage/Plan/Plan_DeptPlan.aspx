<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Plan_DeptPlan.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_DeptPlan" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register src="WUser_DeptPlan.ascx" tagname="WUser_DeptPlan" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/JS/iframe.js"></script>
    <link rel="stylesheet" type="text/css" href="/Manage/css/css.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的计划 >> 查看部门计划
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_dept" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="note"><asp:Literal runat="server" ID="liNote"></asp:Literal></div>
    <uc2:WUser_DeptPlan ID="WUser_DeptPlan1" runat="server" />
    
</asp:Content>
