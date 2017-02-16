<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Plan_ManagerUserSearch.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_ManagerUserSearch" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_cmp" CurIndex="6" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:DataList ID="DataList1" runat="server" RepeatColumns="5" CssClass="table3">
        <ItemTemplate><a href="Plan_PlanUserList.aspx?UserID=<%#Eval("UserID")%>&type=1"><%#Eval("RealName")%></a>
        <a href="Plan_PlanUserList.aspx?UserID=<%#Eval("UserID")%>&type=1"><image src="/images/type1.png"></image></a>
           <a href="Plan_PlanUserList.aspx?UserID=<%#Eval("UserID")%>&type=2"><image src="/images/type2.png"></image></a>
           <a href="Plan_PlanUserList.aspx?UserID=<%#Eval("UserID")%>&type=3"><image src="/images/type3.png"></image></a>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:DataList>
</asp:Content>
