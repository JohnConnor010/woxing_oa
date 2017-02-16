<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="SendNotifyFiles.aspx.cs" Inherits="wwwroot.Manage.XZ.SendNotifyFiles" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    行政管理 >> 文件通知 >> 我撰写的文件
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_notify" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="拟写人">
                <ItemTemplate><%# Eval("CategoryName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="分类" Visible="false">
                <ItemTemplate>
                    <%# WX.XZ.NotifyFiles.Areaarry[Convert.ToInt32(Eval("Area"))]%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="接收范围" Visible="false">
                <ItemTemplate>
                    <%# (Eval("Depms").ToString() != "" ? "<b style='color:blue;'>部门：</b>" + WX.CommonUtils.GetDeptNameListByDeptIdList(Eval("Depms").ToString()) + "<br/>" : "")  + (Eval("Users").ToString() != "" ? "<b style='color:blue;'>人员：</b>" + WX.CommonUtils.GetRealNameListByUserIdList(Eval("Users").ToString()) : "")%>
                </ItemTemplate>
                <ItemStyle Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a style='<%# Eval("Istop").ToString()=="1"?"color:Red;font-weight:bold;":"color:Blue;" %>' href="javascript:PopupIFrame('NotifyFileDetail.aspx?NotifyFileId=<%# Eval("id") %>','文件审批','','',900,420)"><%# Eval("Title") %></a><%# Eval("Istop").ToString() == "1" ? "<img src='/images/arrow_up.gif' alt='置顶'/>" : ""%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate><%# WX.XZ.NotifyFiles.Statearry[Convert.ToInt32(Eval("State").ToString())]%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态时间">
                <ItemTemplate><%#(Eval("PublishTime").ToString() == "" ? "" : Convert.ToDateTime(Eval("PublishTime")).ToString("yyyy-MM-dd HH:mm:ss"))%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间">
                <ItemTemplate><%# Convert.ToDateTime(Eval("Addtime")).ToString("yyyy-MM-dd HH:mm:ss")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                  <a href="javascript:PopupIFrame('NotifyFileDetail.aspx?NotifyFileId=<%# Eval("id") %>','文件审批','','',900,420)">发布</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>