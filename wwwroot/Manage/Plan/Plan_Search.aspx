<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Plan_Search.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_Search" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
        .stylese
        {
            text-align: center;
            font-weight: bold;
            width: 200px;
            height: 145px;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript" src="/JS/iframe.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的任务 >> 查看任务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_my" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table align="center" width="500" style="margin:0 auto;>
        <tr style="font-weight: bold; text-align: center; background-color: #dddddd; height: 30px;">
            <td>
                按月查看
            </td>
            <td>
                按周查看
            </td>
            <td>
                按日查看
            </td>
        </tr>
        <tr>
            <td>
                <div id="table1">
                </div>
            </td>
            <td>
                <div id="table2">
                </div>
            </td>
            <td>
                <div id="table3">
                </div>
            </td>
        </tr>
    </table>
    <input id="Hidmonth" name="Hidmonth" type="hidden" />
    <input id="Hidweek" type="hidden" />
    <input id="Hidday" type="hidden" />
    <table class="table3">
        <tr>
            <td colspan="20" style="font-weight: bold; text-align: center; background-color: #ccc;
                height: 30px;">
                <div id="plantitle">
                    我的计划</div>
            </td>
        </tr>
        <tr style="font-weight: bold; text-align: center; height: 26px;">
            <td colspan="2" width="50%">
                <img alt="个人计划" src="/Images/UserPlan.gif" />我的计划
            </td>
            <td colspan="2" width="50%">
                <img alt="部门计划" src="/Images/DeptPlan.gif" />部门计划
            </td>
        </tr>
        <tr>
            <td colspan="2" style="line-height: 180%;" valign="top">
                <iframe src="Plan_PlanDetail.aspx" onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="98%" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes"></iframe> 
            </td>
            <td colspan="2" style="line-height: 180%;" valign="top">
                <iframe src="Plan_PlanDetail.aspx" onload="Javascript:SetWinHeight(this,'0')" id="iframe2" width="98%" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes"></iframe> 
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var myDate = new Date();
        var month = parseInt(myDate.getMonth().toString()) + 1;
        var userid = "<%=userid %>";
        var deptid = "<%=deptid %>";
        var deptuserid="<%=deptuserid %>";
        getmonth(month);
    </script>
</asp:Content>

