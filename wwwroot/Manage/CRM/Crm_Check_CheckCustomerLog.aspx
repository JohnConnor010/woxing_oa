<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Check_CheckCustomerLog.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Check_CheckCustomerLog" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
客户管理 >> 客户审核日志
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Check" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="10000">
        <Columns>
            <asp:TemplateField HeaderText="操作人">
                <ItemTemplate>
                    <%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>
                </ItemTemplate>
                <ItemStyle Width="80" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="客户名称" DataField="Title"></asp:BoundField>
            <asp:TemplateField HeaderText="类型">
                <ItemTemplate>
                    <%#WX.CRM.Customer.logstype[Convert.ToInt32(Eval("LogType"))]%>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="描述" DataField="LogParaments"></asp:BoundField>
            <asp:BoundField HeaderText="IP" DataField="LogIP">
                <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="时间">
                <ItemTemplate>
                    <%#Convert.ToDateTime(Eval("LogTime")).ToString("yyyy-MM-dd HH:ss:mm")%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
        CssClass="badoo">
    </webdiyer:AspNetPager>
</asp:Content>
