﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="DefaultFormPage2.aspx.cs" Inherits="wwwroot.App_Demo.DefaultFormPage2" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
	<link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
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
    系统管理 >> 用户管理 >> 新增功能
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <a href="#">用户列表</a> <a href="#">新增用户</a> <a href="#">职务列表</a> <a href="#">新增职务</a>
    <a href="#">功能列表</a> <a href="#">新增功能</a>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        你正在添加一个职务&nbsp;<a href="#" title='查看所有帮助' class="helpall">[?]</a> &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="保存"
                            OnClick="SubmitData" CssClass="button" />
                        &nbsp;&nbsp;<input type="reset" class="button" value='重置' />
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务编号&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td style="width:700px;">
                    <span class="note">本系统职务编号 3位数字组成</span>
                    <asp:TextBox ID="DutyID" name="DutyID" runat="server" MaxLength="3" Width="200" class="easyui-validatebox"
                        validType="regex[/^\d{3}$/]" required="true" invalidMessage="本系统职务编号 3位数字组成"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务名称&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">职务名称由20个字以内的汉字组成</span>
                    <asp:TextBox ID="DutyName" name="DutyName" runat="server" Width="200" MaxLength="20" class="easyui-validatebox"
                     validType="chinese" required="true" invalidMessage="职务名称必须为汉字组成"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务类型&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">职务类型是公司内专门</span>
                    <asp:DropDownList ID="DutyType" name="DutyType" runat="server" Width="200" class="easyui-validatebox"
                        required="true" invalidMessage="职务类型不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>