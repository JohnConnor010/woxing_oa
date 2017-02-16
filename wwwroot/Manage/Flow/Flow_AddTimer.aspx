<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_AddTimer.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_AddTimer" ClientIDMode="Static" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.timespinner.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="../../App_js/validator.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#txtTimer').timespinner({
                min: "08:30",
                required: true,
                showSeconds: true
            });
            $('#ddlRemindType').change(function () {
                var value = $('#ddlRemindType').val();
                switch (value) {
                    case "1":
                        $('#tr0').show();
                        $('#tr1').hide();
                        $('#tr2').hide();
                        $('#tr3').hide();
                        $('#tr4').hide();
                        break;
                    case "2":
                        $('#tr1').show();
                        $('#tr0').hide();
                        $('#tr2').hide();
                        $('#tr3').hide();
                        $('#tr4').hide();
                        break;
                    case "3":
                        $('#tr2').show();
                        $('#tr0').hide();
                        $('#tr1').hide();
                        $('#tr3').hide();
                        $('#tr4').hide();
                        break;
                    case "4":
                        $('#tr3').show();
                        $('#tr0').hide();
                        $('#tr1').hide();
                        $('#tr2').hide();
                        $('#tr4').hide();
                        break;
                    case "5":
                        $('#tr4').show();
                        $('#tr0').hide();
                        $('#tr1').hide();
                        $('#tr2').hide();
                        $('#tr3').hide();
                        break;
                }
            });
            $('#form1').submit(function () {
                if ($('#hidden_SponsorList').val() == "") {
                    alert("请选择消息提醒的发起人！");
                    return false;
                }

            });
            /*
            $('#ddlMonth').change(function () {
                var month = new Date().getMonth() + 1;
                var selectMonth = $('#ddlMonth').val();
                if (parseInt(selectMonth) < month) {
                    alert("选择月份不能小于本月的月份！");
                    $('#ddlMonth').val(month);
                    return false;
                }
            });
            */
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    流程管理 >> 流程定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-modi" CurIndex="6" Param1="{Q:id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tbody>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 流程名称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择流程名称</span>
                    <asp:DropDownList ID="ddlFlowName" runat="server"></asp:DropDownList>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 140px; font-weight: bold;">
                    * 发起人&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请在这里选择发起人的姓名</span> <span
                        style="color: #ff0000">[提示] 发起人可以多选，不支持手动输入 </span><br/>
                    <asp:HiddenField ID="hidden_SponsorList" runat="server" />
                    <asp:TextBox ID="txtSponsorList" runat="server" TextMode="MultiLine" Width="504" Height="120" ReadOnly="true"></asp:TextBox>
                &nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','hidden_SponsorList','txtSponsorList',468,395);">选择</a></td>
            </tr>
            <tr>
                <th style="width: 140px; font-weight: bold;">
                    &nbsp;提醒类型：<a class="help" href="#">[?]</a>
                </th>
                <td>                    
                    <span class="note" style="display: none;">请在这里选择提醒类型</span>
                    <asp:DropDownList ID="ddlRemindType" runat="server">
                    </asp:DropDownList>
&nbsp;
                </td>
            </tr>
            <tr id="tr0" style="display:none">
                <th style="width: 140px; font-weight: bold;">
                    发起时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>                    
                    <span class="note" style="display: none;">发送提醒的时间</span>
                    <asp:TextBox  class="easyui-datetimebox" ID="txtTimer1" runat="server" data-options="required:true,showSeconds:false" style="width:150px"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr1">
                <th style="width: 140px; font-weight: bold;">
                    发起时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox  class="easyui-timespinner"  style="width:80px;" required="required" data-options="min:'08:30'" ID="txtTimer2" runat="server" ></asp:TextBox>
                    
                </td>
            </tr>
            <tr id="tr2" style="display:none">
                <th style="width: 140px; font-weight: bold;">
                    发起时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlWeekday" runat="server">
                    </asp:DropDownList>
&nbsp;<asp:TextBox  class="easyui-timespinner"  style="width:80px;" required="required" data-options="min:'08:30'" ID="txtTimer3" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr id="tr3" style="display:none">
                <th style="width: 140px; font-weight: bold;">
                    发起时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlDate" runat="server">
                    </asp:DropDownList>
&nbsp;<asp:TextBox ID="txtTimer4" runat="server" class="easyui-timespinner"  style="width:80px;"  required="required" data-options="min:'08:30'"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr4" style="display:none">
                <th style="width: 140px; font-weight: bold;">
                    发起时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlMonth" runat="server">
                    </asp:DropDownList>
&nbsp;<asp:DropDownList ID="ddlDay" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtTimer5" runat="server" class="easyui-timespinner"  style="width:80px;" required="required" data-options="min:'08:30'"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 140px;">
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text=" 保 存 " 
                        onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" CssClass="button" Text=" 重 置 " />
&nbsp;</td>
            </tr>
        </tbody>
    </table>
</asp:Content>

