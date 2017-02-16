<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="SelectDepartmentInfo.aspx.cs" Inherits="wwwroot.Manage.MyManage.SelectDepartmentInfo" %>

<%@ Register Src="~/Manage/include/MenuBar.ascx" TagPrefix="uc1" TagName="MenuBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="images/someyongnew.css" />
    <script type="text/javascript" src="images/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $dd = $(".someleft").find("dd");
            $dd.hover(function () {
                $(this).addClass("smbg").siblings("dd").removeClass("smbg");
            }, function () {
                $(this).removeClass("smbg")
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar runat="server" ID="MenuBar" Key="MyManage" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="somecon">
        <h5 class="dh_h5">
            <%:departmentName %>
            <span><a href="">返回主页面</a></span>
        </h5>
        <div class="someleft">
            <dl>
                <dt><a href="">部门计划</a></dt>
                <dd><a href="DepartmentDayPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&Starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>">日计划</a></dd>
                <dd><a href="DepartmentWeekPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&WeekIndex=<%=weekIndex %>">周计划</a></dd>
                <dd><a href="DepartmentMonthPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&Month=<%=DateTime.Now.Month %>">月计划</a></dd>
                <div class="clear"></div>
            </dl>
            <dl>
                <dt>客户管理</dt>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
            <dl>
                <dt>流程管理</dt>
                <dd><a href="">dsfdsfdsds</a></dd>
                <dd><a href="">审批的流程审批的流程</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">dsfdsfdsds</a></dd>
                <dd><a href="">审批的流程审批的流程</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
            <dl>
                <dt>数据报表</dt>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
        </div>
        <div class="someleft">
            <dl>
                <dt>工作日志</dt>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
            <dl>
                <dt>工作单</dt>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
            <dl>
                <dt>行政管理</dt>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
            <dl>
                <dt>项目管理</dt>
                <dd><a href="">日计划</a></dd>
                <dd><a href="">周计划</a></dd>
                <dd><a href="">月计划</a></dd>
                <div class="clear"></div>
            </dl>
        </div>
        <div class="someright">
            <h5><%:departmentName %></h5>
            <p>
                <%:content %>
            </p>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <dl>
                        <dt><%#Eval("RealName") %></dt>
                        <dd><%#Eval("PositionName") %>(<%#Eval("Grade") %>)</dd>
                    </dl>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
