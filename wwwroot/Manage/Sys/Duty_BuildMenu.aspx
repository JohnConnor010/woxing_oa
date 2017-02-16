<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Duty_BuildMenu.aspx.cs" Inherits="wwwroot.Manage.Sys.Duty_BuildMenu" %>

<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="../../App_Scripts/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidateJson() {
            var json_str = $("#ui_Htmls").val();
            var json_o = json_str.parseJSON();
            alert(json_o==null);
            //alert($("#ui_Htmls").val());
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 职务管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="duty-modi" CurIndex="5" Param1="{Q:companyID}" Param2="{Q:DutyId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
  
    <div id="PanelManage">
        <table class="table">
            <tr>
                <td>
                    &nbsp; <span>
                        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                            CssClass="button" OnClick="Button2_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="重新生成"
                            CssClass="button" OnClick="Button3_Click" />&nbsp;&nbsp;&nbsp;</span><span><a
                                href="#" title='查看帮助' class="helpall">查看说明[?]</a></span>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <span class="note">系统根据本人职务和系统功能自动生成左侧菜单，可以手动修改以下菜单，如对菜单模块不熟悉请不要修改以下内容。</span>
                    <textarea id="ui_Htmls" runat="server" cols="80" clientidmode="Static" rows="15" datatype="Require" msg="内容不能为空，如不需要更改直接关闭标签窗口或点击左侧菜单切换到其它页。"></textarea>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
