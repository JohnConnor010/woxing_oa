<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Priv_Credentials.aspx.cs"
    Inherits="wwwroot.Manage.Sys.Priv_Credentials" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    个人资料
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="priv" CurIndex="6" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage" class="table">
    <div style="text-align:right;margin-right:10px;"><asp:HyperLink runat="server" ID="hlInit" href="Priv_EditCredentials.aspx">修改证书</asp:HyperLink></div>
        <asp:DataList ID="DataList1" BackColor="White" runat="server" Width="100%" OnItemCommand="DataList1_ItemCommand"
            RepeatColumns="4">
            <ItemTemplate>
                <a href="javascript:PopupIFrame('Priv_CredentialsDetail.aspx?Id=<%# Eval("Id") %>','查看详细','','',900,800)">
                    <img src="<%# Eval("Annex") %>" alt="<%# Eval("Content") %>" width="220" height="150" /></a>
                <br />
                <b>
                    <%# Eval("Name") %></b>&nbsp;[
                <%# ((DateTime)Eval("Ctime")).ToString("yyyy-MM-dd") %>]
            </ItemTemplate>
            <ItemStyle Width="250" Height="300" />
        </asp:DataList>
        &nbsp;
    </div>
</asp:Content>
