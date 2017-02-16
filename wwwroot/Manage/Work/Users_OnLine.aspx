<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Users_OnLine.aspx.cs" Inherits="wwwroot.Manage.Work.Users_OnLine" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../Manage/css/AspnetPager.css" rel="stylesheet" type="text/css" />
    <script src="../../App_Scripts/popup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    在线用户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="online" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:GridView ID="GridView1" DataKeyNames="UserId" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="员工">
                <ItemTemplate>
                    <img alt="" src="../images/ico_user.gif" /><asp:Label ID="Label1" runat="server" Text='<%# Bind("RealName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="登录时间">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" ToolTip='<%# Eval("LoginTime") %>' Text='<%# getEslapseStr(Eval("LoginTime")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="130px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="登录IP">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# getIpStr(Eval("LoginIp")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作" ItemStyle-Width="120">
                <ItemTemplate>
                    <a href="<%#getSendBox(Eval("UserId"),Eval("RealName")) %>">发信息</a>
                </ItemTemplate>
                <ItemStyle Width="120px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>