<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Plan_CheckPlan.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_CheckPlan" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/JS/iframe.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    计划管理 >> 计划审核
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_dept" CurIndex="7" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:GridView ID="Gv_duty" runat="server" CssClass="table tableview" AutoGenerateColumns="False"
        PageSize="200" DataKeyNames="id">
        <Columns>
            <asp:TemplateField HeaderText="计划">
                <ItemTemplate>
                    <div title='描述：<%# Eval("Content")%>&#13预计完成数：'>
                       <b> <a href="javascript:PopupIFrame('Plan_CheckPlanDetail.aspx?PlanId=<%# Eval("id") %>','任务审核','','',600,400)"><%# Eval("Title")%></a></b></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                   <%# Eval("RealName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="预计完成数">
                <ItemTemplate>
                   <%# Eval("Total")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类型">
                <ItemTemplate>
                   <%# Eval("Type").ToString() == "3" ? "月计划" : (Eval("Type").ToString() == "2" ? "周计划" : "日计划")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="开始时间">
                <ItemTemplate>
                    <%#  Convert.ToDateTime(Eval("Starttime")).ToString("yyyy-MM-dd HH:mm")%></ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
