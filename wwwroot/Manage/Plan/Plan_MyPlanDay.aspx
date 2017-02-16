<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Plan_MyPlanDay.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_MyPlanDay" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/JS/iframe.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的计划 >> 日计划
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_my" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<%DateTime nowd = Request["nowdate"]!=null&&Request["nowdate"]!=""?Convert.ToDateTime(Request["nowdate"]): DateTime.Now; %>
<div style=" text-align:right; padding-right:100px; font-weight:bold;"><a href="?nowdate=<%=nowd.AddDays(-7).ToString("yyyy-MM-dd") %>">上一周</a>  <a href="?nowdate=<%=nowd.AddDays(7).ToString("yyyy-MM-dd") %>">下一周</a></div>
    <iframe src='Plan_TempEditPlan.aspx?UserID=<%=userid %>&starttime=<%=nowd.Day<7&&Convert.ToInt32(Convert.ToDateTime(nowd.ToString("yyyy-MM-01")).DayOfWeek.ToString("d"))>1?nowd.AddDays(-7).ToString("yyyy-MM-dd"):nowd.ToString("yyyy-MM-dd") %>&type=2&estate=1&rtype=1'
        onload="Javascript:SetWinHeight(this,'0')" id="iframe6" width="100%" frameborder="no"
        border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
    </iframe>
    <%
        int n=0;
        nowd = nowd.AddDays(-((Convert.ToInt32(nowd.DayOfWeek.ToString("d")) == 0 ? 7 : Convert.ToInt32(nowd.DayOfWeek.ToString("d"))) - 1));
        for (int i = nowd.Day; i < nowd.Day+7; i++)
      { 
            %>
    <iframe src='Plan_TempEditPlan.aspx?UserID=<%=userid %>&starttime=<%=nowd.AddDays(n).ToString("yyyy-MM-dd") %>&type=1&estate=1&rtype=1'
        onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="100%" frameborder="no"
        border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
    </iframe>
    <%n++;
      } %>
</asp:Content>
