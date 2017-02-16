<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_CompanyWebSiteEdit.aspx.cs" ClientIDMode="Static" Inherits="wwwroot.Manage.Sys.Dept_CompanyWebSiteEdit" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 域名编辑
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="com" CurIndex="3" Param1="{Q:companyID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table" width="600" align="center">
        <tr>
            <td style="font-weight: bold;">
                网站名称:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">网站名称</span> &nbsp;<asp:TextBox ID="ui_name" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                域名:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">网站域名</span> &nbsp;<asp:TextBox ID="ui_Url" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                IP:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">域名指向的网站IP地址</span> &nbsp;<asp:TextBox ID="ui_IP" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                备案号:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">ICP备案号</span> &nbsp;<asp:TextBox ID="ui_RecordNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                域名到期时间:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">域名到期时间</span> &nbsp;<asp:TextBox ID="ui_Valid" runat="server"
                    CssClass="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                上次续费时间:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">上次续费时间</span> &nbsp;<asp:TextBox ID="ui_Feetime" runat="server"
                    CssClass="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; width: 100px;">
                续费提醒:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">提前多少天提醒负责人续费</span> &nbsp;<asp:TextBox ID="ui_Warn" runat="server"
                    CssClass="easyui-validatebox" required="true"></asp:TextBox>天
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; width: 100px;">
                责任人:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">责任人</span> &nbsp;
                <asp:TextBox ID="li_Manage" runat="server" Width="60" Enabled="false" CssClass="easyui-validatebox"
                    required="true"></asp:TextBox>
                <asp:HiddenField ID="ui_Manage" runat="server" />
                <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_Manage','li_Manage',468,395);" />
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; width: 100px;">
                状态:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">当前状态</span> &nbsp;
                <asp:CheckBoxList ID="ui_state" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="1" Text="使用中" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="0" Text="暂停"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        
             <tr>
            <th style="width: 140px; font-weight: bold;">
                变更责任人：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更的责任人</span>
                <asp:TextBox ID="li_logmanage" runat="server" Width="60" Enabled="false"  CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="ui_logmanage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择责任人','ui_logmanage','li_logmanage',468,395);" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                变更备注：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更简单说明：如变更的原因、谁提出的变更、变更内容一句话描述等</span>
                <asp:TextBox ID="ui_logcontent" runat="server" Columns="150"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
