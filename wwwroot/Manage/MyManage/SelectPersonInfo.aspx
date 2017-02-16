<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="SelectPersonInfo.aspx.cs" Inherits="wwwroot.Manage.MyManage.SelectPersonInfo" %>

<%@ Register Src="~/Manage/include/MenuBar.ascx" TagPrefix="uc1" TagName="MenuBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="images/someyongnew.css" />
    <script type="text/javascript" src="images/jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $controller = $(".personal_main").find("dt");
            $objecter = $(".personal_main").find("dd");
            $li = $(".personal_main").find("li")
            $controller.hover(function () {
                index = $controller.index(this);
                $(this).addClass("border").siblings("dt").removeClass("border");
                $objecter.eq(index).addClass("fcolor").siblings("dd").removeClass("fcolor");
            }, function () {
                index = $controller.index(this);
                $(this).removeClass("border");
                $objecter.eq(index).removeClass("fcolor")
            })
            $li.hover(function () {
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
    <div class="personal_main">
        <h3>员工信息（<b><asp:Literal ID="ltlName" runat="server"></asp:Literal></b>）<span><a href="">返回主页面</a></span></h3>
        <dl class="person_gn1">
            <dt><a>我的计划</a></dt>
            <dd>
                <ul>
                    <li><a href="PlanView.aspx?UserID=<%=Request.QueryString["UserID"] %>&DateTime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>&type=1">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="PlanView.aspx?UserID=<%=Request.QueryString["UserID"] %>&Year=<%=DateTime.Now.Year %>&Month=<%=DateTime.Now.Month %>&type=3">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn2">
            <dt><a>我的流程</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn3">
            <dt><a>我的客户</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn4">
            <dt><a>我的档案</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn5">
            <dt><a>工作日志</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn6">
            <dt><a>工作单</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn7">
            <dt><a>我的行政</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn8">
            <dt><a>我的项目</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <dl class="person_gn9">
            <dt><a>提成报表</a></dt>
            <dd>
                <ul>
                    <li><a href="">日计划</a></li>
                    <li><a href="">周计划</a></li>
                    <li><a href="">月计划</a></li>
                </ul>
            </dd>
        </dl>
        <p class="clear"></p>
    </div>
</asp:Content>
