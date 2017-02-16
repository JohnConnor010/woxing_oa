<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Plan_PlanUserList.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_PlanUserList" %>
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
<asp:DataList ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <iframe src='Plan_TempEditPlan.aspx?UserID=<%#Eval("UserID") %>&starttime=<%#Eval("date") %>&type=<%#Eval("type") %>&rtype=1'
            onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="98%" frameborder="no"
            border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes" height="300">
        </iframe>
    </ItemTemplate>
    <ItemStyle VerticalAlign="Top" />
</asp:DataList>
</asp:Content>
