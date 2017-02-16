<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_EquipmentList.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_EquipmentList" ClientIDMode="Static" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link rel="Stylesheet" href="../css/AspnetPager.css" type="text/css" />
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <style type="text/css">
        #norSearch, #advSearch
        {
            background: url("../../images/search_button.png") no-repeat scroll 0 0 transparent;
            height: 33px;
            margin: 3px;
            width: 107px;
        }
        input.toolBtnA, input.toolBtnB, input.toolBtnC
        {
            background: url("../../images/m_button.png") repeat scroll 0 0 transparent;
            border: 0 none;
            color: #1866F4;
            cursor: pointer;
            font-family: 微软雅黑,宋体,sans-serif;
            font-size: 11pt;
            height: 23px;
            text-decoration: none;
            width: 114px;
        }
    </style>
    <script type="text/javascript">
        function PreviewEquipmentList(name,ud) {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 400;
            diag.Title = name + "的个人装备记录";
            diag.URL = 'Ass_PreviewEquipmentList.aspx?UserID=' + ud;
            diag.show();
        }
        function PreviewLogList(name,type,ud) {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 400;
            diag.Title = name + "的物品" + type + "记录";
            diag.URL = "Ass_PreviewLogsList.aspx?type=" + type + "&UserID=" + ud;
            diag.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    资产管理 >> 查询个人装备
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="AssetsPriv" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr>
                <td>
                    所在部门：<asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="toolBtnA" Text="搜索" 
                        onclick="btnSearch_Click" />
                </td>
            </tr>
        </thead>
    </table>
    <table class="table">
        <thead>
            <tr class="">
                <td width="100"  style="margin:left 20px">
                    姓名
                </td>
                <td width="100">
                    性别
                </td>
                <td width="138">
                    手机号码
                </td>
                <td width="250">
                    电子邮件</td>
                <td width="130">
                    QQ号码
                </td>
                <td width="120" colspan="3">
                    个人装备/领用/归还记录
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="EquipmentRepeater" runat="server">
                <ItemTemplate>
                    <tr class="">
                        <td style="margin:left 20px">
                            <%#Eval("RealName") %>
                        </td>
                        <td style="font-weight: bold;">
                            <%#Eval("Sex") %>
                        </td>
                        <td>
                            <%#Eval("Mobile") %>
                        </td>
                        <td>
                            <%#Eval("Email") %>
                        </td>
                        <td>
                            <%#Eval("QQ") %>
                        </td>
                        <td>
                        【<a href="javascript:void(0)" onclick="PreviewEquipmentList('<%#Eval("RealName") %>','<%#Eval("UserID") %>')">个人装备</a>】
                        【<a href="javascript:void(0)" onclick="PreviewLogList('<%#Eval("RealName") %>','领用','<%#Eval("UserID") %>')">领用记录</a>】
                        【<a href="javascript:void(0)" onclick="PreviewLogList('<%#Eval("RealName") %>','归还','<%#Eval("UserID") %>')">归还记录</a>】
                        </td>
                        
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="8">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="badoo">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
