<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_EditDelegate.aspx.cs" Inherits="wwwroot.Manage.Work.Work_EditDelegate" ClientIDMode="Static" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datetimebox.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                if ($('#txtPrincipal').val() == "") {
                    alert("委托人不能为空！");
                    return false;
                }
                if ($('#txtBeThePrincipal').val() == "") {
                    alert("被委托人不能为空！");
                    return false;
                }
                if ($('#txtPrincipal').val() == $('#txtBeThePrincipal').val()) {
                    alert("委托人与被委托人为同一人！");
                    return false;
                }
            });
            $('#chkAllFlow').click(function () {
                if ($('#chkAllFlow').attr("checked") == "checked") {
                    $('#ddlFlowName').attr("disabled", "disabled");
                } else {
                    $('#ddlFlowName').removeAttr("disabled");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    工作委托 >> 修改委托规则
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="work_delegate" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tbody>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 选择流程：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择流程名称</span>
                    <asp:DropDownList ID="ddlFlowName" runat="server">
                    </asp:DropDownList>
&nbsp;&nbsp;
                    <asp:CheckBox ID="chkAllFlow" runat="server" Text="全部流程" />
                </td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 委托人：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择委托人</span>
                        <asp:HiddenField ID="hidden_Principal" runat="server" />
                    <asp:TextBox ID="txtPrincipal" runat="server" TextMode="MultiLine" Width="320" Height="80" ReadOnly="true"></asp:TextBox>
                    &nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择人员','hidden_Principal','txtPrincipal',468,395);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 被委托人：<a class="help" href="#">[?]</a></th>
                <td>
                    <span class="note" style="display: none;">请选择被委托人</span>
                    <asp:HiddenField ID="hidden_BeThePrincipal" runat="server" />
                    <asp:TextBox ID="txtBeThePrincipal" runat="server" TextMode="MultiLine" Width="320" 
                        Height="80" ReadOnly="true"></asp:TextBox>
                &nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择人员','hidden_BeThePrincipal','txtBeThePrincipal',468,395);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    &nbsp;* 有效期：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">委托的有效期，如果为空则一直有效</span>
                    <input type="hidden" id="userlist" name="userlist">
                    开始：<asp:TextBox class="easyui-datetimebox" data-options="required:true,showSeconds:false"  style="width:150px" ID="txtStartTime" runat="server"></asp:TextBox>
&nbsp;&nbsp; 终止：<asp:TextBox class="easyui-datetimebox" data-options="required:true,showSeconds:false"  style="width:150px" ID="txtEndTime" runat="server"></asp:TextBox>
&nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 140px;">
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保存" 
                        onclick="btnSave_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <input type="reset" value="重置" class="button" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
