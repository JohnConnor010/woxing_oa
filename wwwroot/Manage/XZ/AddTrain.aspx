<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="AddTrain.aspx.cs" Inherits="wwwroot.Manage.XZ.AddTrain" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">考核培训管理 >> 新增信息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_train" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <table class="table3" style="line-height: 200%;">
            <tr>
                <td>
                    <b>标题:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_Title" Columns="80" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td width="80">
                    <b>类型:</b>
                </td>
                <td>
                    <asp:DropDownList ID="drop_type" runat="server">
                    <asp:ListItem Value="1" Text="考核"></asp:ListItem>
                    <asp:ListItem Value="2" Text="培训"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>流程:</b>
                </td>
                <td>
                    <asp:DropDownList ID="drop_flow" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>时间:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_RunTime" runat="server" class="easyui-datetimebox" required="true"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>地点:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_Addr" runat="server" Columns="80" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>参与人员:</b>
                </td>
                <td>
                    <asp:HiddenField ID="ui_Persons" runat="server" />
                         <asp:TextBox ID="li_Persons" runat="server" Columns="60" Rows="2" 
                        TextMode="MultiLine" CssClass="easyui-validatebox" required="true"></asp:TextBox>
&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','ui_Persons','li_Persons',468,395);">选择</a>
                </td>
            </tr>
            <tr>
                <td>
                    <b> 内容:</b>
                </td>
                <td>
                    <FCKeditorV2:FCKeditor ID="ui_content" ToolbarSet="Basic" runat="server" Height="300"
                        Width="900">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: left; padding: 5px;">
                    <asp:Button ID="Button1" runat="server" Text=" 保 存 " CssClass="button" OnClick="Button1_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
</asp:Content>
