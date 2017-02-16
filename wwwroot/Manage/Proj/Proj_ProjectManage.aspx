<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_ProjectManage.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectManage" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 项目列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj_manage" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td width="80">
                    申请人
                </td>
                <td>
                    项目名称
                </td>
                <td width="80">
                    预计天数
                </td>
                <td width="80">
                    预计人数
                </td>
                <td width="80">
                    预计投资
                </td>
                <td width="60">
                    状态</td>
                <td width="120">
                    申请时间</td>
                <td width="80">
                    管理
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
            <ItemTemplate>
            <tr class="">
               <td>
                    <%#Eval("RealName") %>
                </td>
                <td>
                    <a href='Proj_ProjectCheck.aspx?ProjectId=<%#Eval("ID")%>'><%#Eval("ProjectName")%></a>
                </td>
                <td>
                     <%#Eval("Days")%>
                </td>
                <td width="80">
                     <%#Eval("Persons")%>
                </td>
                <td width="80">
                     <%#Eval("Fee")%>
                </td>
                <td width="60">
                    <%# WX.PRO.Project.statearray[Convert.ToInt32(Eval("State").ToString())]%></td>
                <td width="120">
                    <%# Convert.ToDateTime(Eval("Addtime").ToString()).ToString("yyyy-MM-dd")%></td>
                <td class="manage">
                   <%# Eval("State").ToString() == "1" ? "<a class=\"show\" href='Proj_ProjectCheck.aspx?ProjectId=" + Eval("ID") + "'>审核</a>" : ""%>
                    <a href='Proj_ProjectCheck.aspx?ProjectId=<%#Eval("ID")%>'>查看</a>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="8">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
