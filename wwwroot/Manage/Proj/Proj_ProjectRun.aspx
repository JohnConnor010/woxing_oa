<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_ProjectRun.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectRun" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 运行中项目
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj_manage" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td style="width:80px;">
                    申请人
                </td>
                <td>
                    项目名称
                </td>
                <td style="width:80px;">
                    当前步骤
                </td>
                <td style="width:80px;">
                    项目负责人
                </td>
                <td style="width:240px;">
                    进度
                </td>
                <td style="width:80px; text-align:center;">
                    已用时间
                </td>
                <td style="width:60px;">
                    状态</td>
                <td style="width:120px;">
                    启动时间</td>
                <td style="width:120px;">
                    审批时间</td>
                <td style="width:80px;">
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
                    <a href='Proj_ProjectCheck.aspx?ProjectId=<%#Eval("ProjID")%>'><%#Eval("ProjectName")%></a>
                </td>
                <td>
                     第<%#Eval("ProcID")%>步
                </td>
                <td>
                     <%#Eval("ManageName")%>
                </td>
                <td style="font-size:10px;line-height:12px;">
                  <%#Eval("Percnt")%>%
                  <div style="width:205px; height:13px; float:left; background:url(/images/back.gif) no-repeat;"> <img src="/images/jindu.gif" width='<%#Eval("Percnt")%>%' height="10" /></div>
                </td>
                <td align="center">
                     <%#Eval("Percnttime")%>%
                </td>
                <td>
                    <%# WX.PRO.State.statearray[Convert.ToInt32(Eval("State").ToString())]%></td>
                <td>
                    <%# Convert.ToDateTime(Eval("Starttime").ToString()).ToString("yyyy-MM-dd")%></td>
                <td>
                    <%# Convert.ToDateTime(Eval("Addtime").ToString()).ToString("yyyy-MM-dd")%></td>
                <td class="manage">
                    <a href='Proj_ProjectCheck.aspx?ProjectId=<%#Eval("ProjID")%>'>查看</a>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="11">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
