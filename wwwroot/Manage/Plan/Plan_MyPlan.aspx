<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Plan_MyPlan.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_MyPlan" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
        .pageinit
        {
            line-height: 180%;
            padding-left: 15px;
            padding-right: 15px;
        }
        .table3 td{ padding:0px 0px 0px 0px;}
        td.title{font-weight:bold; text-align: left; background-color: #D8DCF1; color:#336;
                height: 20px; width:50%; padding-left:10px; background:url(/img/titlebg.png) repeat-x;}
        td.title a{color:#336;}
    </style>
    <script type="text/javascript" src="/JS/iframe.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的计划 >> 查看任务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_my" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 
    <table class="table3" id="plan1" runat="server">
        <tr>
            <td class="title">
                <div id="plantitle" style="float: left; width: 80%;">
                    <img alt="个人计划" src="/Images/UserPlan.gif" />今日计划&nbsp;&nbsp;【<asp:Literal runat="server" ID="liToday"></asp:Literal>】</div>
                <div style="float: left; text-align:right;">
                    <asp:Literal ID="dayedit" runat="server"></asp:Literal></div>
            </td>
            <td rowspan="2">
            &nbsp;
            </td>
        </tr>
        <tr>
            <td class="pageinit" valign="top">
                <iframe src='Plan_PlanDetail.aspx?UserID=<%=userid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=1&estate=1&rtype=1'
                    onload="Javascript:SetWinHeight(this,'0')" id="iframe4" width="100%" frameborder="no"
                    border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
                </iframe>
            </td>
        </tr>
        <tr>
            <td class="title">
                <div id="Div1" style="float: left; width: 80%;">
                    <img alt="个人计划" src="/Images/UserPlan.gif" />我的本周计划【<asp:Literal runat="server" ID="liThisWeek"></asp:Literal>】</div>
                <div style="float: left;text-align:right;">
                    <asp:Literal ID="weekedit" runat="server"></asp:Literal></div>
            </td>
            <td class="title">
                <div id="Div3" style="float: left; width: 80%;" runat="server">
                    <img alt="部门计划" src="/Images/DeptPlan.gif" />部门本周计划【<asp:Literal runat="server" ID="liThisWeek1"></asp:Literal>】</div>
                <div style="float: left;">
                    <asp:Literal ID="weekdept" runat="server"></asp:Literal></div>
            </td>
        </tr>
        <tr>
            <td class="pageinit" valign="top">
                <iframe src='Plan_PlanDetail.aspx?UserID=<%=userid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=2&estate=1&rtype=1'
                    onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="100%" frameborder="no"
                    border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
                </iframe>
            </td>
            <td class="pageinit" valign="top">
                <iframe src='Plan_PlanDetail.aspx?UserID=<%=deptuserid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=2&rtype=<%=rtype %>'
                    onload="Javascript:SetWinHeight(this,'0')" id="iframe3" width="100%" frameborder="no"
                    border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
                </iframe>
            </td>
        </tr>
        <tr>
            <td class="title">
                <div id="Div2" style="float: left; width: 80%;">
                    <img alt="个人计划" src="/Images/UserPlan.gif" />我的本月计划【<asp:Literal runat="server" ID="liThisMonth"></asp:Literal>】</div>
                <div style="float: left;text-align:right;">
                    <asp:Literal ID="monthedit" runat="server"></asp:Literal></div>
            </td>
            <td class="title">
                <div id="Div4" style="float: left; width: 90%;" runat="server">
                    <img alt="部门计划" src="/Images/DeptPlan.gif" />部门本月计划【<asp:Literal runat="server" ID="liThisMonth1"></asp:Literal>】</div>
                <div style="float: left;">
                    <asp:Literal ID="monthdept" runat="server"></asp:Literal></div>
            </td>
        </tr>
        <tr>
            <td class="pageinit" valign="top">
                <iframe src='Plan_PlanDetail.aspx?UserID=<%=userid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=3&rtype=1&estate=1'
                    onload="Javascript:SetWinHeight(this,'0')" id="iframe2" width="100%" frameborder="no"
                    border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
                </iframe>
            </td>
            <td class="pageinit" valign="top">
                <iframe src='Plan_PlanDetail.aspx?UserID=<%=deptuserid %>&starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=3&rtype=<%=rtype %>'
                    onload="Javascript:SetWinHeight(this,'0')" id="iframe5" width="100%" frameborder="no"
                    border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
                </iframe>
            </td>
        </tr>
    </table>
</asp:Content>
