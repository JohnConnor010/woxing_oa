<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    ClientIDMode="Static" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Menu_Add.aspx.cs"
    Inherits="wwwroot.Manage.Sys.Menu_Add" %>

<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../Style/sys_ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_js/sys_js.js"></script>
    <script type="text/javascript">
        var nowimg = null;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 菜单管理
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="menu" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        <a href="#" title='查看所有帮助' class="helpall">你正在添加一个菜单&nbsp;[?]</a>
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 上级菜单&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单所在的菜单项</span>
                    <asp:DropDownList ID="ui_ParentID" AppendDataBoundItems="true"
                        runat="server" CssClass="easyui-combobox" Width="120">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 菜单名称&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单显示名称10个汉字以内</span> &nbsp;<input runat="server" id="ui_Name"
                        type="text" maxlength="10" datatype="Require" msg="菜单名称不能为空，请输入10个以内汉字" />
                </td>
            </tr>
            <tr id="tr1">
                <th style="width: 145px; font-weight: bold;">
                    标题&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单显示标题20个汉字以内,如果有菜单标题，将代替菜单名称显示！</span> &nbsp;<input id="ui_Title" runat="server"
                        type="text" maxlength="20" />
                </td>
            </tr>            
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 状态&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">状态为打开时菜单可用，为关闭时菜单不可用</span>
                    <select runat="server" id="ui_State" class="easyui-combobox" panelheight="auto" style="width: 120px">
                        <option value="0">关闭</option>
                        <option value="1" selected="selected">打开</option>
                    </select>
                </td>
            </tr>

            <tr id="tr2">
                <th style="width: 145px; font-weight: bold;">
                    * 页面URL&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单所指向的页面地址，即点击菜单名称跳转到的页面</span> &nbsp;<input id="ui_Url" runat="server"
                        type="text" style="width:400px" maxlength="100" msg="页面URL不能为空，请输入菜单所指向的页面地址" />
                </td>
            </tr>
             <tr>
                <th style="width: 145px; font-weight: bold;">
                    排序&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单正序排序，数字小的排在前面</span> &nbsp;<input id="ui_OrderID" runat="server"
                        type="text" maxlength="2" style="width:20px" dataType="Number" require="false" />
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    * 选择图标&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">菜单名称前的小图标</span>
                    <img runat="server" src="/Manage/icon/031.gif" id="nowimg" alt="菜单图标" />
                    <input id="ui_icon" type="hidden" runat="server" value="/Manage/icon/ico_home.gif" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                <div style="width:580px;">
                    <%=imagesstr %>
                </div>
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
    <script language="javascript">
        //checkDegree(document.getElementById("ui_ParentID"));
    </script>
</asp:Content>
