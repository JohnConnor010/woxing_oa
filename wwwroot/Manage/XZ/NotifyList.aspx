<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="NotifyList.aspx.cs" Inherits="wwwroot.Manage.XZ.NotifyList" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    行政管理 >> 公告管理 >> 公告列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_notify" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="发布人">
                <ItemTemplate><%# Eval("RealName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="分类">
                <ItemTemplate><%# Eval("CategoryName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发布范围" Visible="false">
                <ItemTemplate>
                    <%# (Eval("Depms").ToString() != "" ? "<b style='color:blue;'>部门：</b>" + WX.CommonUtils.GetDeptNameListByDeptIdList(Eval("Depms").ToString()) + "<br/>" : "") + (Eval("Dutys").ToString() != "" ? "<b style='color:blue;'>职务：</b>" + WX.CommonUtils.GetDutyNameListByDutyIdList(Eval("Dutys").ToString()) + "<br/>" : "") + (Eval("Users").ToString() != "" ? "<b style='color:blue;'>人员：</b>" + WX.CommonUtils.GetRealNameListByUserIdList(Eval("Users").ToString()) : "")%>
                </ItemTemplate>
                <ItemStyle Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a style='<%# Eval("Istop").ToString()=="1"?"color:Red;font-weight:bold;":"color:Blue;" %>' href="javascript:PopupIFrame('NotifyDetail.aspx?NotifyID=<%# Eval("id") %>','查看详细','','',900,800)"><%# Eval("Title") %></a><%# Eval("Istop").ToString() == "1" ? "<img src='/images/arrow_up.gif' alt='置顶'/>" : ""%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建时间">
                <ItemTemplate><%# Convert.ToDateTime(Eval("Addtime")).ToString("yyyy-MM-dd HH:mm:ss")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="生效时间">
                <ItemTemplate><%#  Convert.ToDateTime(Eval("Starttime")).ToString("yyyy-MM-dd")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="终止时间">
                <ItemTemplate><%# Eval("Stoptime").ToString() == "" ? "" : Convert.ToDateTime(Eval("Stoptime")).ToString("yyyy-MM-dd")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%# Convert.ToDateTime(Eval("Starttime")) > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) ? "待生效" : (Eval("Stoptime").ToString() != "" && new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) >= Convert.ToDateTime(Eval("Stoptime")) ? "<font color='red'>终止</font>" : "<font color='green'>生效</font>")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑"
                        NavigateUrl='<% #String.Format("AddNotify.aspx?NotifyID={0}",Eval("ID")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal2" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton2" runat="server" Visible='<% #this.Master.A_Edit %>' CausesValidation="False"
                        OnClick="edittime" CommandArgument='<% #Eval("id") %>'
                        Text=' <%# Convert.ToDateTime(Eval("Starttime"))>new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day)?"立即生效":(Eval("Stoptime").ToString()!=""&&DateTime.Now>Convert.ToDateTime(Eval("Stoptime"))?"生效":"终止") %>' />
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton3" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False"
                        OnClick="Del" OnClientClick="return confirm('是否真的要删除这条流程吗？');" CommandName='<% #Eval("id") %>'
                        Text="删除" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>