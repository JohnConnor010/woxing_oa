<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"  ClientIDMode="Static" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Func_EditFunctions.aspx.cs" Inherits="wwwroot.Manage.Sys.Func_EditFunctions" %>

<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../Style/sys_ui.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 功能管理 >> 修改功能
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="func-modi" CurIndex="2" Param1="{Q:FunctionId}" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        <a href="#" title='查看所有帮助' class="helpall">你正在修改一个功能&nbsp;[?]</a>
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 上级功能&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">功能所在的菜单项</span>
                    <asp:DropDownList ID="ui_ParentID" AppendDataBoundItems="true"
                        runat="server" CssClass="easyui-combobox" Width="150">
                    </asp:DropDownList>
                    <input id="ui_degree" type="hidden" value="1" runat="server" />
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 功能名称&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">功能显示名称10个汉字以内</span> &nbsp;<input runat="server" id="ui_Name"
                        type="text" maxlength="10" datatype="Require" msg="功能名称不能为空，请输入10个以内汉字" />
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 分类&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">功能分类</span>
                    <asp:DropDownList ID="DropType" runat="server">
                    </asp:DropDownList>
                    <asp:CheckBox ID="CheckBox1" runat="server" />应用于子功能
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 状态&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">状态为打开时功能可用，为关闭时功能不可用</span>
                    <select runat="server" id="ui_State" class="easyui-combobox" panelheight="auto" style="width: 150px">
                        <option value="0">关闭</option>
                        <option value="1" selected>打开</option>
                    </select>
                </td>
            </tr>
             <tr id="tr3">
                <th style="width: 145px; font-weight: bold;">
                    * 功能页面&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">此功能所影响到的页面，每行一个，使用绝对地址，如： /Manage/Sys/Func_AddFunctions.aspx</span>
                    &nbsp;<textarea id="ui_Urls" runat="server" cols="60" rows="5" msg="功能Urls不能为空，至少同页面URL地址保持一致"></textarea>
                </td>
            </tr>
             <tr>
                <th style="width: 145px; font-weight: bold;">
                    排序&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单正序排序，数字小的排在前面</span> &nbsp;<input id="ui_OrderID" runat="server"
                        type="text" maxlength="20" dataType="Number" require="false" />
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" OnClientClick="return Validator.Validate(this.form,3);"
                        OnClick="btnSave_Click" Text="保存" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
