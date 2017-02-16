<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Plan_DeptSearch.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_DeptSearch" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register src="WUser_DeptPlan.ascx" tagname="WUser_DeptPlan" tagprefix="uc2" %>
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
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_dept" CurIndex="5" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table align="center" width="500">
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
    <input id="Hidmonth" name="Hidmonth" type="hidden" runat="server" />
    <input id="Hidweek" type="hidden" runat="server" />
    <input id="Hidday" type="hidden" runat="server" />
    <uc2:WUser_DeptPlan ID="WUser_DeptPlan1" runat="server" />
    <script type="text/javascript">
        var myDate = new Date();
        getdeptmonth(document.getElementById("Hidmonth").value,<%=type %>,<%=rtype %>);
    </script>
</asp:Content>

