<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_Project.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_Project" %>
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
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    项目名称
                </td>
                <td width="80">
                    天数
                </td>
                <td width="80">
                    人数
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
                    <a href='Proj_ProjectDetail.aspx?ProjectId=<%#Eval("ID")%>'><%#Eval("ProjectName")%></a>
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
                   <%# Eval("State").ToString() == "0" ? "<a class=\"show\" href='Proj_Addproject.aspx?id=" + Eval("ID") + "'>编辑</a><a class=\"show\" href='Proj_Process.aspx?ProjectId=" + Eval("ID") + "'>步骤</a>" : ""%>
                    <asp:LinkButton ID="btnDelete" Visible='<%# Eval("State").ToString()=="0" %>' CommandName="del" runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command" OnClientClick="return confirm('删除后信息不可恢复，确定要删除吗？')"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" Visible='<%# Eval("State").ToString()=="2" %>' runat="server" CommandName="qidong" Text="启动项目" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"></asp:LinkButton>
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
