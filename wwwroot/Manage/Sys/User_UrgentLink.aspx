<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="User_UrgentLink.aspx.cs" Inherits="wwwroot.Manage.Sys.User_UrgentLink" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 员工管理 >> 紧急联络人
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server"  Key="com_p" CurIndex="7" Param1="{Q:UserID}" Param2="{Q:companyid}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
              <tr>
                <th style="width: 135px; font-weight: bold;">
                    紧急联络人&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">修改员工紧急联络人。</span>
                    <FCKeditorV2:FCKeditor ID="ui_content" ToolbarSet="Basic" runat="server" Height="300" Width="900">
                </FCKeditorV2:FCKeditor>
                    &nbsp;
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
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交保存" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="button" Text="取消重填" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>