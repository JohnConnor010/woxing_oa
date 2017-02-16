<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Proj_Addproject.aspx.cs" ClientIDMode="Static"
    Inherits="wwwroot.Manage.Proj.Proj_Addproject" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
<script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
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
    项目管理 >> 项目申请
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table3" style="line-height: 200%;">
            <tr>
                <td>
                    <b>项目名称:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_Name" Columns="60" runat="server" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td width="100">
                    <b>预计完成天数:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_days" Columns="8" runat="server" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>&nbsp;天
                </td>
            </tr>
            <tr>
                <td>
                    <b>预计参与人员:</b>
                </td>
                <td>
                    <asp:HiddenField ID="ui_Persons" runat="server" />
                         <asp:TextBox ID="li_Persons" runat="server" Columns="60" Rows="2" 
                        TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','ui_Persons','li_Persons',468,395);">选择</a>
                </td>
            </tr>
            <tr>
                <td>
                    <b>预计投入资金:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_fee" Columns="8" runat="server"></asp:TextBox>&nbsp;万元
                </td>
            </tr>
            <tr>
                <td>
                    <b>预计到达效果:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_Imagine" runat="server" TextMode="MultiLine" Rows="4" Columns="110"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>项目方案:</b>
                </td>
                <td>
                    <FCKeditorV2:FCKeditor ID="ui_content" ToolbarSet="Basic" runat="server" Height="300"
                        Width="900">
                    </FCKeditorV2:FCKeditor>
                    <asp:Literal ID="li_annex" runat="server"></asp:Literal>
                    附件：<asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: left; padding: 5px;">
                    <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="button" OnClick="Button1_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text=" 保存并申请 " CssClass="button" Visible="false"
                        OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
