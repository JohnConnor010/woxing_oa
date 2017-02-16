<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Dept_CompanyslicenseEdit.aspx.cs"
    Inherits="wwwroot.Manage.Sys.Dept_CompanyslicenseEdit" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
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

        function setspan(span) {
            document.getElementById(span).innerHTML = "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 资料编辑
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="comp" CurIndex="5" Param1="{Q:CompanyId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table" width="600" align="center">
        <tr>
            <td style="font-weight: bold; width: 100px;">
                类型:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">企业资料分类</span>
                <asp:DropDownList ID="ui_type" runat="server" Enabled="false">
                    <asp:ListItem Value="1" Text="经营性证件"></asp:ListItem>
                    <asp:ListItem Value="2" Text="银行帐户"></asp:ListItem>
                    <asp:ListItem Value="3" Text="法人/股东证件"></asp:ListItem>
                    <asp:ListItem Value="4" Text="资质证件"></asp:ListItem>
                    <asp:ListItem Value="5" Text="验资报告"></asp:ListItem>
                    <asp:ListItem Value="6" Text="公司章程"></asp:ListItem>
                    <asp:ListItem Value="7" Text="其它证件"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr>
            <td style="font-weight: bold;">
                证件名称:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件名称</span> &nbsp;<asp:TextBox ID="ui_title" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>
            </td>
        </tr>
        </table>
        <table class="table" width="600" align="center" id="table3" runat="server" visible="false">   
        <tr>
            <td style="font-weight: bold; width: 100px;">
                证件号码:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件号码</span> &nbsp;<asp:TextBox ID="ui_LNO" CssClass="easyui-validatebox" runat="server" required="true"></asp:TextBox>
            </td>
        </tr> 
        </table>
        <table class="table" width="600" align="center" id="table1" runat="server" visible="false">       
        <tr runat="server" id="tr_time" visible="false">
            <td style="font-weight: bold;">
                年审情况:<a href="#" class="help">[?]</a>
            </td>
            <td style="line-height:240%;">
                <span class="note">证件年审情况</span>
                <b>是否年审：</b><asp:DropDownList ID="ui_Ischeck" runat="server">
                    <asp:ListItem Value="0" Text="未审"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已审"></asp:ListItem>
                </asp:DropDownList><br />
                <b>年审时间：</b><asp:TextBox CssClass="easyui-datebox" ID="ui_Checktime" runat="server" required="true"></asp:TextBox>-<asp:TextBox CssClass="easyui-datebox" ID="ui_Checkstoptime" runat="server" required="true"></asp:TextBox><br />
                <b>有&nbsp;&nbsp;效&nbsp;&nbsp;期：</b><asp:TextBox CssClass="easyui-datebox" ID="ui_Valid" runat="server" required="true"></asp:TextBox>-<asp:TextBox CssClass="easyui-datebox" ID="ui_Validstop" runat="server" required="true"></asp:TextBox><br />
                <b>所需材料：</b><asp:TextBox ID="ui_Checkdata" runat="server"
                    TextMode="MultiLine" Rows="2" Columns="50"></asp:TextBox>注：证件年审所需材料备注<br />
                     <b>提醒时间：</b><asp:TextBox ID="ui_Warn" runat="server" CssClass="easyui-validatebox" Width="80" required="true"></asp:TextBox>（天）。注：提前多少天提醒年审
                    <br />
                    <b>责任部门：</b><asp:DropDownList ID="ui_CheckDepartentID" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <b>责任人：</b> <asp:TextBox ID="li_CheckManage" runat="server" required="true" Width="60" Enabled="false"  CssClass="easyui-validatebox"></asp:TextBox>
                        <asp:HiddenField ID="ui_CheckManage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_CheckManage','li_CheckManage',468,395);" />
            </td>
        </tr>
         <tr>
            <td style="font-weight: bold; width: 100px;">
                发证日期:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件颁发日期</span> &nbsp;<asp:TextBox CssClass="easyui-datebox" required="true" ID="ui_Addtime" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                发证单位:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件颁发单位 </span> &nbsp;<asp:TextBox ID="ui_Unit" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>
            </td>
        </tr>
        </table>
        <table class="table" width="600" align="center" id="table2" runat="server" visible="false">
         <tr>
            <td style="font-weight: bold; width: 100px;">
                法人/股东:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">选择当前信息是法人还是股东</span> &nbsp;<asp:DropDownList ID="ui_LorS" runat="server">
                 <asp:ListItem Value="1" Text="法人" Selected></asp:ListItem>
                <asp:ListItem Value="2" Text="股东"></asp:ListItem>
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                姓名:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">真实姓名</span> &nbsp;<asp:TextBox ID="ui_RealName" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                性别:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">性别 </span><asp:RadioButtonList ID="ui_sex" runat="server" RepeatColumns="2" Width="150">
                <asp:ListItem Value="1" Text="男" Selected></asp:ListItem>
                <asp:ListItem Value="0" Text="女"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>        
        <tr>
            <td style="font-weight: bold;">
                政治面貌:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">政治面貌</span> &nbsp;<asp:TextBox ID="ui_PoliticalL" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>
            </td>
        </tr>  
        <tr>
            <td style="font-weight: bold;">
                学历:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">最高学历</span><asp:DropDownList ID="ui_edu" runat="server">
                    </asp:DropDownList>
            </td>
        </tr>  
        </table>
        <table class="table" width="600" align="center">
        
        <tr>
            <td style="font-weight: bold; width: 100px;">
                其它:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">资料内容，介绍</span> &nbsp;<asp:TextBox ID="ui_content" runat="server"
                    TextMode="MultiLine" Rows="5" Columns="60"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td style="font-weight: bold;">
                原件保存部门:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件原件保存部门</span> &nbsp;<asp:DropDownList ID="ui_DepartentID" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <b>责任人：</b> <asp:TextBox ID="li_Manage" runat="server" Width="60" Enabled="false"  CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="ui_Manage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_Manage','li_Manage',468,395);" />
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                附件:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">资料相关附件，目前支持上传5个，较多资料请打包上传</span>
                <asp:FileUpload ID="FileUpload0" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal0"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but0" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal1"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but1" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload2" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal2"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but2" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload3" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal3"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but3" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload4" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal4"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but4" runat="server" Text="删除"
                        OnClick="but2_Click" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
        
             <tr>
            <th style="width: 140px; font-weight: bold;">
                变更责任人：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更的责任人</span>
                <asp:TextBox ID="li_logmanage" runat="server" Width="60" Enabled="false"  CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="ui_logmanage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择责任人','ui_logmanage','li_logmanage',468,395);" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                变更备注：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更简单说明：如变更的原因、谁提出的变更、变更内容一句话描述等</span>
                <asp:TextBox ID="ui_logcontent" runat="server" Columns="150"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />&nbsp;&nbsp;<asp:Button Visible="false" ID="Button2" runat="server" Text="删除" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
