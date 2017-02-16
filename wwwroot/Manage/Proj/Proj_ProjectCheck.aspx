<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="Proj_ProjectCheck.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectCheck" %>
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
    <style type="text/css">
tr.selectproc td
{
	font-weight:bold;
	 color:Red;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 项目申请
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj_manage" CurIndex="1" Param1="{Q:ProjectId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
    <center><b>基本信息</b></center>
        <table class="table3" style="line-height: 200%;">
            <tr>
                <td>
                    <b>项目名称:</b>
                </td>
                <td>
                    <div style="float:left; width:480px;"><asp:Literal ID="li_name" runat="server"></asp:Literal></div>
                    <div style="float:left;"><b>状态：</b><asp:Literal ID="li_state" runat="server"></asp:Literal>&nbsp;</div>
                </td>
            </tr>
            <tr>
                <td width="100">
                    <b>预计完成天数:</b>
                </td>
                <td>
                   <div style="float:left; width:480px;"> <asp:Literal ID="li_days" runat="server"></asp:Literal>&nbsp;天</div>
                    <div style="float:left; width:240px;"><b>预计投入资金：</b><asp:Literal ID="li_fee" runat="server"></asp:Literal>&nbsp;万元</div>
                </td>
            </tr> 
            <tr runat="server" id="tr1">
                <td>
                    <b>项目负责人:</b>
                </td>
                <td>
                    <asp:Literal ID="li_projmanage" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>项目参与人员:</b>
                </td>
                <td>
                    <asp:Literal ID="li_persons" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>预计到达效果:</b>
                </td>
                <td>
                    <asp:Literal ID="li_Imagine" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>项目方案:</b>
                </td>
                <td>
                <asp:Literal ID="li_content" runat="server"></asp:Literal><br />
                    <b>附件：</b><asp:Literal ID="li_annex" runat="server"></asp:Literal>
                </td>
            </tr>           
            <tr runat="server" id="tr2">
                <td>
                    <b>审批相关:</b>
                </td>
                <td>
                <div style="float:left; width:240px;"><b>审批人：</b><asp:Literal ID="li_checkmanage" runat="server"></asp:Literal></div>
                <div style="float:left; width:240px;"><b>审批资金：</b><asp:Literal ID="li_checkfee" runat="server"></asp:Literal>万元</div>
                    <div><b>审批时间：</b><asp:Literal ID="li_checktime" runat="server"></asp:Literal>&nbsp;</div>
                    <div><b>审批意见：</b><asp:Literal ID="li_checkopinion" runat="server"></asp:Literal>&nbsp;</div>
                    <asp:HiddenField ID="hi_procid" runat="server" />
                </td>
            </tr>
        </table><br /><center><b>步骤信息</b></center>
        <table class="table">
        <thead>
             <tr>
                <td style="width:20px;">
                &nbsp;
                    </td>
               <td style="width:80px;">
                    排序
                </td>
                <td>
                    参与人数
                </td>
                <td>
                    占用天数
                </td>
                <td style="width:100px;">
                    开始时间
                </td>
                <td style="width:100px;">
                    结束时间
                </td>
                <td>
                    占项目总体百分比
                </td>
                <td>
                    占时间总体百分比</td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
            <ItemTemplate>
            <tr class="<%# getclass(Eval("NO")) %>">
                <td style=" background:#eeeeee;">
                    <asp:Image ID="Image1" Visible='<%# getimg(Eval("NO")) %>' ImageUrl="/images/green_arrow.gif" runat="server" />
                    </td>
               <td style=" background:#eeeeee;">
                    第<%#Eval("NO") %>步
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Persons")%>
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Days")%>
                </td>
                <td style=" background:#eeeeee;">
                     <%#Eval("Starttime").ToString() != "" ? Convert.ToDateTime(Eval("Starttime").ToString()).ToString("yyyy-MM-dd") : ""%>
                </td>
                <td style=" background:#eeeeee;">
                      <%# Eval("Stoptime").ToString()!=""?Convert.ToDateTime(Eval("Stoptime").ToString()).ToString("yyyy-MM-dd"):"" %>
                </td>
                <td style=" background:#eeeeee;">
                     <%#Eval("Percnt")%>%
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Percnttime")%>%</td>                
            </tr>
            <tr> <td>
                &nbsp;
                    </td><td height="35"> &nbsp;&nbsp;<b>详细部署：</b></td><td colspan="6"><%#Eval("Demo")%></td></tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div id="checkdata" runat="server">
    <br /><center><b>审批信息</b></center>
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
                    <b>完成天数:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_days" Columns="8" runat="server" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>&nbsp;天
                </td>
            </tr>
             <tr>
                <td>
                    <b>项目负责人:</b>
                </td>
                <td>
                   <asp:TextBox ID="li_manage" runat="server" Width="60" Enabled="false" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="ui_manage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_manage','li_manage',468,395);" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>参与人员:</b>
                </td>
                <td>
                    <asp:HiddenField ID="ui_Persons" runat="server" />
                         <asp:TextBox ID="liu_Persons" runat="server" Columns="60" Rows="2" 
                        TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','ui_Persons','liu_Persons',468,395);">选择</a>
                </td>
            </tr>
            <tr>
                <td>
                    <b>投入资金:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_fee" Columns="8" runat="server"></asp:TextBox>&nbsp;万元
                </td>
            </tr>
            <tr>
                <td>
                    <b>预计开始时间:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_yjstarttime" runat="server" CssClass="easyui-datebox" required="true"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <b>审批意见:</b>
                </td>
                <td>
                    <asp:TextBox ID="ui_Opinion" runat="server" TextMode="MultiLine" Rows="4" Columns="110" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: left; padding: 5px;">
                    <asp:Button ID="Button1" runat="server" Text=" 通 过 " CssClass="button" OnClick="Button1_Click" />&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text=" 退 回 " CssClass="button" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        </div>
    </div>
</asp:Content>
