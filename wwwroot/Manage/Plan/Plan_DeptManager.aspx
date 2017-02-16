<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Plan_DeptManager.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_DeptManager" %>
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
        div.divv a{font-weight:normal;}
    </style>
    <script type="text/javascript" src="/JS/iframe.js"></script>
    <link rel="stylesheet" type="text/css" href="/Manage/css/css.css" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
   部门管理 >> 部门计划
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_dept" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <div class='divv' id="plan2" runat="server">
        <table style="margin-top: 9px;" cellspacing="0" cellpadding="0" width="100%" align="center">
            <tbody>
                <tr>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td valign="top" width="50%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr class="info2" style="height:25px;color:Yellow;">
                                                    <td background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; float: left;">&nbsp;审核提醒</span> <span
                                                            style="float: right;"></span>
                                                    </td>
                                                    <td background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td height="170" style="vertical-align: top; padding-top: 8px; padding-bottom: 8px;">
                                                        <div style="border-bottom:dashed 1px #777; margin-bottom:5px; color:Red;">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;（注）作为部门主管，需要负责本部门计划审核(开始)、任务审核(执行)、计划评价(结束)三项计划管理工作！
                                                        </div>
                                                        <div>
                                                            <asp:Repeater runat="server" ID="rptCheckPlan">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink runat="server" ID="hlCheckPlan" NavigateUrl='<% #GetCheckPlanUrl(Eval("id")) %>'
                                                                        Text='<% #GetCheckPlanText(Eval("RealName"),Eval("Type")) %>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <SeparatorTemplate>
                                                                    &nbsp;
                                                                </SeparatorTemplate>
                                                            </asp:Repeater>
                                                            <asp:Repeater runat="server" ID="rptCheckTask">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink runat="server" ID="hlCheckPlan" NavigateUrl='<% #GetCheckTaskUrl(Eval("PlanID")) %>'
                                                                        Text='<% #GetCheckTaskText(Eval("RealName"),Eval("Type")) %>'></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <SeparatorTemplate>
                                                                    &nbsp;
                                                                </SeparatorTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                        
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" width="50%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr class="info2" style="height:25px;color:Yellow;">
                                                    <td background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold;  float: left;">&nbsp;员工今日计划</span><span
                                                            style="float: left;">&nbsp;&nbsp;【<asp:Literal runat="server" ID="liToday"></asp:Literal>】</span>
                                                    </td>
                                                    <td background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td height="170" style="vertical-align: top; padding-top:8px; padding-bottom:8px; line-height:180%; font-weight:bold;">
                                                        <asp:Literal ID="userday" runat="server"></asp:Literal>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="8" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" width="50%">
                                       <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr class="info2" style="height:25px;color:Yellow;">
                                                    <td background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; float: left;">&nbsp;员工本周计划</span><span
                                                            style="float: left;">&nbsp;&nbsp;【<asp:Literal runat="server" ID="liThisWeek"></asp:Literal>】</span>
                                                    </td>
                                                    <td background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td height="170" style="vertical-align: top; padding-top:8px; padding-bottom:8px; line-height:180%; font-weight:bold;">
                                                       <asp:Literal ID="userweek" runat="server"></asp:Literal>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                                   
                                    </td>
                                    <td valign="top" width="50%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr class="info2" style="height:25px;color:Yellow;">
                                                    <td background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; float: left;">&nbsp;员工本月计划</span> <span
                                                            style="float: left;">&nbsp;&nbsp;【<asp:Literal runat="server" ID="liThisMonth"></asp:Literal>】</span>
                                                    </td>
                                                    <td background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td height="170" style="vertical-align: top; padding-top:8px; padding-bottom:8px; line-height:180%; font-weight:bold;">
                                                    <asp:Literal ID="usermonth" runat="server"></asp:Literal>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
