<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Plan_MyPlanDay.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_MyPlanDay" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/JS/iframe.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的计划 >> 月计划
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_my" CurIndex="4" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <iframe src='Plan_TempEditPlan.aspx?UserID=<%=userid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=3&estate=1&rtype=1'
        onload="Javascript:SetWinHeight(this,'0')" id="iframe6" width="100%" frameborder="no"
        border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
    </iframe>
    <%for (int i = 1; i <= System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
      {%>
    <iframe src='Plan_TempEditPlan.aspx?UserID=<%=userid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-"+i) %>&type=1&estate=1&rtype=1'
        onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="100%" frameborder="no"
        border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
    </iframe>
    <% } %>
</asp:Content>


