<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Manage_AgreementList.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Manage_AgreementList" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
客户管理 >> 协议管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer" CurIndex="4" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="10000">
        <Columns>
            <asp:TemplateField HeaderText="签订人">
                <ItemTemplate>
                    <%# Eval("RealName").ToString()%>
                </ItemTemplate>
                <ItemStyle Width="80" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户名称">
                <ItemTemplate>
                    <a href=javascript:PopupIFrame('CRM_SingleM_ShowAgreement.aspx?AgreementID=<%# Eval("id")%>','协议内容','','',850,450)><%#Eval("CustomerName")%></a><%#Eval("TrackState").ToString() == "1" ? "" : "&nbsp;<a href=javascript:PopupIFrame('CRM_SingleM_EditTrack.aspx?CustomerID=" + Eval("CustomerID") + "&TrackID=" + Eval("TrackID") + "','提交跟踪信息','','',850,450)>>>执行</a>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="报价">
                <ItemTemplate>
                    ¥<%#Eval("AllFee")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="余额">
                <ItemTemplate>
                    ¥<%#Eval("OverFee")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="余额时间">
                <ItemTemplate><%#Eval("OverTime").ToString()!=""? Convert.ToDateTime(Eval("OverTime")).ToString("yyyy-MM-dd HH:ss:mm"):""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="审核人">
                <ItemTemplate>
                    <%#Eval("CheckUserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="开始时间">
                <ItemTemplate>
                    <%#Eval("StartTime").ToString() != "" ? Convert.ToDateTime(Eval("StartTime")).ToString("yyyy-MM-dd HH:ss:mm") : ""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="结束时间">
                <ItemTemplate>
                    <%#Eval("StopTime").ToString() != "" ? Convert.ToDateTime(Eval("StopTime")).ToString("yyyy-MM-dd HH:ss:mm") : ""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Visible='<%# Eval("IsCheck").ToString()=="0"?true:false %>' runat="server">企管审核</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" Visible='<%# Eval("IsCheck").ToString()=="2"?false:true %>' runat="server">企管登记</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
        CssClass="badoo">
    </webdiyer:AspNetPager>
</asp:Content>
