<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="DefaultListPage1.aspx.cs" Inherits="wwwroot.App_Demo.DefaultListPage1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
系统管理 >> 用户管理 >> 用户列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <a href="#">用户列表</a> <a href="#">新增用户</a> <a href="#">职务列表</a> <a href="#">新增职务</a>
    <a href="#">功能列表</a> <a href="#">新增功能</a>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
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
                    <asp:Literal ID="Literal2" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑" NavigateUrl='<% #String.Format("DefaultSingleEditPage.aspx?ID={0}",Eval("id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal1" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False" OnClick="Del"
                        OnClientClick="return confirm('是否真的要删除这条记录？');" CommandName='<% #Eval("id") %>'
                        Text="删除" />
                </ItemTemplate>               
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
