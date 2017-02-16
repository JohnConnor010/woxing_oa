<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Manage_ProgramList.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Manage_ProgramList" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
客户管理 >> 方案管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="10000">
        <Columns>
            <asp:TemplateField HeaderText="递送人">
                <ItemTemplate>
                    <%# Eval("RealName").ToString()%>
                </ItemTemplate>
                <ItemStyle Width="80" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="客户名称" DataField="CustomerName"></asp:BoundField>
            <asp:TemplateField HeaderText="方案标题">
                <ItemTemplate>
                    <a href=javascript:PopupIFrame('Crm_SingleM_ShowTrack.aspx?TrackID=<%# Eval("TrackID")%>','方案详细信息','','',850,450)><%#Eval("Title")%></a><%#Eval("TrackState").ToString() == "1" ? "" : "&nbsp;<a href=javascript:PopupIFrame('CRM_SingleM_EditTrack.aspx?CustomerID=" + Eval("CustomerID") + "&TrackID=" + Eval("TrackID") + "','提交跟踪信息','','',850,450)>>>执行</a>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="报价">
                <ItemTemplate>
                    ¥<%#Eval("ZDFee")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="递送时间">
                <ItemTemplate>
                    <%#Eval("ProgramTime").ToString()!=""?Convert.ToDateTime(Eval("ProgramTime")).ToString("yyyy-MM-dd HH:ss:mm"):""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成功时间">
                <ItemTemplate>
                    <%# Eval("Updatetime").ToString()!=""?Convert.ToDateTime(Eval("Updatetime")).ToString("yyyy-MM-dd HH:ss:mm"):""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
        CssClass="badoo">
    </webdiyer:AspNetPager>
</asp:Content>
