<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Run_PassForm.aspx.cs" Inherits="wwwroot.Manage.Work.Run_PassForm" ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript">
        function RemoveItem1(id) {
            $('#a' + id).remove();
            $("#hidden_organizer").val("");
            
        }
        function RemoveItem2(id) {
            $('#b' + id).remove();
            var str = "," + $('#hidden_transactor').val();
            str = str.replace("," + id, "");
            $('#hidden_transactor').val(str);

        }
        function RemoveAll() {
            $('#hidden_organizer').val("");
            $('#organizer').html("");
            $('#hidden_transactor').val("");
            $('#transactor').html("");
        }
        function CheckSelect(sender) {
            var container = sender.parentNode;
            if (container.tagName.toLowerCase() == "span") {
                container = container.parentNode.parentNode;
                var chkList = container.getElementsByTagName("input");
                $.each(chkList, function (i, checkbox) {
                    if (checkbox == event.srcElement) {
                        checkbox.checked = true;
                    } else {
                        checkbox.checked = false;
                    }
                });
            }
        }
    </script>
    <style type="text/css">
        input.SmallButtonA
        {
            background: url("/Manage/images/btn_a.png") no-repeat scroll 0 0 transparent;
            border: 0 none;
            color: #36434E;
            cursor: pointer;
            height: 21px;
            width: 50px;
        }
        input.SmallButtonB
        {
            background: url("/Manage/images/btn_b.png") no-repeat scroll 0 0 transparent;
            border: 0 none;
            color: #36434E;
            cursor: pointer;
            height: 21px;
            width: 74px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    新建工作测试 >> 转交工作
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="run" CurIndex="3" Param1="flow_id" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td colspan="2">
                    当前步骤为第【<asp:Literal ID="liStepId" runat="server"></asp:Literal>】步<span class="red"><asp:Literal ID="liFlowName" runat="server" /></span></td>
            </tr>
            <tr class="">
                <td>
                    【<asp:Literal ID="liFlowName1" runat="server" />】
                </td>
                <td align="center">
                    流水号:<asp:Literal ID="liRunId" runat="server" /> - <asp:Literal ID="liRunName" runat="server" />
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 下一步骤：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">下一步骤</span>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server"
                        RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 承办人办理：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <input type="hidden" id="hidden_organizer" runat="server" />
                    主办人：<span id="organizer" runat="server" class="underline"></span><br />
                    <input type="hidden" id="hidden_transactor" runat="server" />
                    经办人：<span id="transactor" runat="server" class="underline"></span><br />
                    <input type="button" class="SmallButtonB" value="选择主办" 
                        onclick="PopupIFrame('/App_Ctrl/SelectPeopleByFilter.aspx?CompanyId=11&RunId=<%=rRunId %>&StepId=<%=SelNextStep %>&SelectModel=Single','选择主办人员','hidden_organizer','organizer',468,395);" />
                    &nbsp;&nbsp;
                    <input type="button" class="SmallButtonB" value="选择经办" 
                        onclick="PopupIFrame('/App_Ctrl/SelectPeopleByFilter.aspx?CompanyId=11&RunId=<%=rRunId %>&StepId=<%=SelNextStep %>','选择经办人员','hidden_transactor','transactor',468,395);" />
                    &nbsp;&nbsp; <a href="javascript:RemoveAll();">清空</a>
                </td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    &nbsp;投票发送事务提醒：：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    【下一步】：<asp:CheckBox 
                        ID="chkNextTip" runat="server" 
                        Text="<a href='#' title='事务提醒'><img src='../images/sms.gif' /></a>" />
                    &nbsp;&nbsp;
                    【发起人】：<asp:CheckBox ID="chkSponsor" runat="server" 
                        Text="<a href='#' title='事务提醒'><img src='../images/sms.gif' /></a>" />
                    &nbsp;&nbsp;
                    【全部经办人】：<asp:CheckBox ID="chkAllTransactor" runat="server" 
                        Text="<a href='#' title='事务提醒'><img src='../images/sms.gif' /></a>" />
                    <br />
                    提醒内容：<asp:TextBox ID="txtTipContent" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 180px;">
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="确认转交" 
                        onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" CssClass="button" Text="继续办理" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button4" runat="server" CssClass="button" Text="取消转交并返回" Width="120px" />
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="button">
                        <asp:ListItem Value="0">其他操作</asp:ListItem>
                        <asp:ListItem Value="1">公司通告</asp:ListItem>
                        <asp:ListItem Value="2">内部右键</asp:ListItem>
                        <asp:ListItem Value="2">转存</asp:ListItem>
                        <asp:ListItem Value="4">归档</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
