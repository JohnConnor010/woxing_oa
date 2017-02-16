<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Duty_DutyGive.aspx.cs" Inherits="wwwroot.Manage.Sys.Duty_DutyGive" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />    
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 职务控制 >> 具体职务控制
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="duty_detail" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelDefault">
        <div style="width: 98%; ">
            <span style="float: left; font-style:italic;padding-left:30px; font-size:+1;">
                <asp:Literal ID="liCompanyName" runat="server">我行技术有限公司</asp:Literal></span> 
        </div>
        <div style="clear:both;"/>
        <asp:GridView ID="Gv_duty" runat="server" CssClass="table table3 tableview" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="200" 
                onrowdatabound="Gv_duty_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="编号" DataField="id" Visible="True" ItemStyle-Width="50" />
                <asp:BoundField HeaderText="部门" DataField="name" ItemStyle-Width="180" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

