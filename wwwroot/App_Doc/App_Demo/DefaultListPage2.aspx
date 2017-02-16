<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage3.Master"
    AutoEventWireup="true" CodeBehind="DefaultListPage2.aspx.cs" Inherits="wwwroot.App_Demo.DefaultListPage2" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage3.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        function checkHasSel() {
            var selCount = 0;
            $(".checkdelete").each(function () {
                if ($(this).attr("checked") == true) selCount++;
            });
            if (selCount == 0) {
                alert("你没有选择记录");
                return false;
            }
            else
                return true;
        }</script>
    <link href="../Manage/css/AspnetPager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 用户管理 >> 用户列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <a href="#">用户列表</a> <a href="#">新增用户</a> <a href="#">职务列表</a> <a href="#">新增职务</a>
    <a href="#">功能列表</a> <a href="#">新增功能</a>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 500px;">
            请输入关键字进行查询：<asp:TextBox ID="tbKeyWords" runat="server" Width="300" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;<asp:LinkButton Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="Query" Text="GO" />
        </div>
        <div style="text-align: right; float: right; width: 200px;">
            <asp:LinkButton runat="server" ID="lbDelSel" OnClick="DelSel" Font-Bold="true" ForeColor="#234323"
                OnClientClick="return checkHasSel()" Text="删除" /></div>
        <div style="clear: both;">
        </div>
    </div>
    <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField ItemStyle-Width="20">
                <HeaderTemplate>
                    <input class="checkall" type="checkbox" onclick='$(".checkdelete").attr("checked", $("input[class=checkall]").attr("checked"));' />
                </HeaderTemplate>
                <ItemTemplate>
                    <input name="checksel" type="checkbox" class="checkdelete" id="checksel" value='<%#Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="编号" ItemStyle-Width="40" ReadOnly="true">
                <ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="名称" ItemStyle-Width="90">
                <ItemStyle Width="90px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="tel" HeaderText="电话" ItemStyle-Width="130">
                <ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="address" HeaderText="地址" />
            <asp:TemplateField ShowHeader="False" HeaderText="操作" ItemStyle-Width="120">
                <ItemTemplate>
                    <asp:HyperLink runat="server" Text="查看" NavigateUrl='<% #String.Format("DefaultSinglePage.aspx?ID={0}",Eval("id")) %>'></asp:HyperLink>
                    <asp:Literal runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑" NavigateUrl='<% #String.Format("DefaultSingleEditPage.aspx?ID={0}",Eval("id")) %>'></asp:HyperLink>
                    <asp:Literal runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False" OnClick="Del"
                        OnClientClick="return confirm('是否真的要删除这条记录？');" CommandName='<% #Eval("id") %>'
                        Text="删除" />
                </ItemTemplate>
                <ItemStyle Width="120px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
