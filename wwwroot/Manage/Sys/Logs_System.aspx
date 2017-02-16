<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Logs_System.aspx.cs" Inherits="wwwroot.Manage.Sys.Logs_System" %>    
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
	<link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 登录记录
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="sysLogs" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 700px;">
            请输入员工姓名：<asp:TextBox ID="tbKeyWords" runat="server" Width="100" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;&nbsp;开始时间：<asp:TextBox ID="txtBeginTime" class="easyui-datebox" runat="server" Width="100" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;&nbsp;结束时间：<asp:TextBox ID="txtEndTime" class="easyui-datebox" runat="server" Width="100" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="Query" Text="查询" />&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="QueryAll" Text="查询所有" />
        </div>
        <div style="text-align: right; float: right; width: 200px;">
            <asp:LinkButton runat="server" ID="lbClearLogs" OnClick="ClearLogs" Font-Bold="true" ForeColor="#234323" Text="清除日志" OnClientClick="return confirm('你真要清除所有日志吗？')" /></div>
        <div style="clear: both;">
        </div>
    </div>
    <table class="table">
        <thead>
            <tr class="">
                <td style="padding-left: 15px;">
                    员工姓名
                </td>
                <td>
                    <span>记录</span>
                </td>
                <td width="150">
                    登录IP
                </td>
                <td width="150">
                    登录时间
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
                <ItemTemplate>
                    <tr class="">
                        <td style="padding-left: 15px;">
                            <strong>
                                <img src="/Images/sign.gif" alt=""><a href='?userid=<%#Eval("UserID") %>'><%#Eval("RealName") %></a></strong>
                        </td>
                        <td style='<%#Eval("Title").ToString().Trim()=="登录OA系统"?"color: Green;":"color:Red;" %>'>
                                <%#Eval("Title") %>-<%#Eval("LogParaments")%>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# getIP(Eval("LogIP")) %>' />
                        </td>
                        <td>
                            <%#Eval("LogTime") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="badoo">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
