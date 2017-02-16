<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_SingleM_ShowCustomerBusiness.aspx.cs"
    Inherits="wwwroot.Manage.CRM.Crm_SingleM_ShowCustomerBusiness" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 我的客户 >> 客户跟踪
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="MyCustomer-Modi" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="div2" runat="server" style="text-align: left; padding-left: 20px;">
        客户编号：<b><asp:Literal ID="Literal1" runat="server"></asp:Literal></b>&nbsp;&nbsp;&nbsp;&nbsp;客户名称：<b><asp:Literal
            ID="Literal2" runat="server"></asp:Literal></b>&nbsp;&nbsp;&nbsp;&nbsp;跟踪阶段：<b><asp:Literal
                ID="Literal3" runat="server"></asp:Literal></b></b></div>
    <div id="div1" runat="server" style="text-align: right; padding-right: 20px; font-weight: bold;">
        <a href="javascript:PopupIFrame('CRM_SingleM_EditTrack.aspx?CustomerID=<%=WX.Request.rCustomerID %>','提交跟踪信息','','',850,450)">
            添加跟踪</a></div>
    <asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" OnRowCommand="Gv_customer_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="详细">
                <ItemTemplate>
                    <fieldset style="padding: 10px; line-height: 180%;">
                        <legend style="font-weight: bold;"><img src='/images/procstate<%# Eval("ProcessState")%>.bmp' />
                        <a href="javascript:PopupIFrame('Crm_SingleM_ShowTrack.aspx?TrackID=<%# Eval("Id")%>','预览跟踪信息','','',850,450)">
                            <%# WX.CRM.Track.ProcessState[Convert.ToInt32(Eval("ProcessState"))]%></a><font color="gray">(<%# WX.Main.GetTimeEslapseStr(Convert.ToDateTime(Eval("TrackTime")),"","") %>)</font>
                           <%# Request["CustomerID"] == null || Request["CustomerID"] == "" ? "——" + Eval("CustomerName") : ""%>——<%# Eval("RealName")%>
                        </legend>
                        <asp:Literal runat="server" ID="liMemo" Text='<%#GetMemo(Eval("Remarks")) %>'></asp:Literal>
                        <%# String.IsNullOrEmpty(Convert.ToString(Eval("LogParaments"))) ? "": "<br/>跟踪详情：" + Eval("LogParaments").ToString() %>
                    </fieldset>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <div style="line-height: 180%; vertical-align: top; padding-top: 10px; color: #666666;">
                      
                        <%# Eval("Type").ToString() == "1"?"" : "<a href=\"javascript:PopupIFrame('CRM_SingleM_EditTrack.aspx?CustomerID=" + (Request["CustomerID"] == ""||Request["CustomerID"] ==null ? Eval("CustomerID") : Request["CustomerID"]) + "&TrackID=" + Eval("Id") + "','提交跟踪信息','','',850,450)\">提交&执行</a>"%>
                        <asp:LinkButton ID="LinkButton1" Visible='<%# Eval("State").ToString()=="1"?false:true%>'
                            OnClientClick="return confirm('删除后不可恢复，确定要删除吗？')" CommandArgument='<%# Eval("Id")%>'
                            CommandName="del" runat="server">删除</asp:LinkButton><br />
                        状态：<%# Eval("State").ToString()=="1"?"已执行":"未执行"%>  &nbsp;&nbsp;<%# Eval("Type").ToString()=="0"?"等待审核":(Eval("Type").ToString()=="1"?"有效":"无效")%><br />
                        花销：<%# string.Format("{0:C2}",Eval("Fee"))%><br />
                        跟踪次数：<%# Eval("TrackNo").ToString()%><br />
                        跟踪时间：<%# Convert.ToDateTime(Eval("TrackTime")).ToString("yyyy-MM-dd HH:mm:ss")%></div>
                </ItemTemplate>
                <ItemStyle Width="180px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
        CssClass="badoo">
    </webdiyer:AspNetPager>
</asp:Content>
