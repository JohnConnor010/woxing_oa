<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_CompanyInfo.aspx.cs" Inherits="wwwroot.Manage.CompanyInfo"
    ClientIDMode="Static" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content0" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />    
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 编辑基本信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="comp" CurIndex="2" Param1="{Q:CompanyId}" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- 内容模块 -->
    <table class="table">
    <caption style="text-align:left; font-style:italic;"><asp:Literal runat="server" ID="liTitle"></asp:Literal></caption>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 公司编号：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">您所在公司编号 2位数字组成</span>
                <asp:TextBox ID="txtCompanyNO" runat="server" MaxLength="2" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;<font
                    color="red"><b>数字2位，从11开始编号</b></font>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 单位名称：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">您所在公司的名称，比如：我行信息技术有限公司</span>
                <asp:TextBox ID="txtCompanyName" runat="server" Columns="40" MaxLength="48" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 维护责任人：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司信息的维护人员，由指定人员负责修改</span>
                <asp:TextBox ID="li_Manage" runat="server" Width="60" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="txtManage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','txtManage','li_Manage',468,395);" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 公司类型：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">属于哪种类型的公司</span>
                <asp:TextBox ID="txtCtype" runat="server" Columns="40" MaxLength="48" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 成立时间：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司成立时间</span>
                <asp:TextBox ID="txtSetuptime" runat="server" Columns="20" MaxLength="48" CssClass="easyui-datebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
         <tr>
            <th style="width: 140px; font-weight: bold;">
                * 经营期限：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司经营有效期</span>
                <asp:TextBox ID="txtOperatetime" runat="server" Columns="20" MaxLength="48" CssClass="easyui-datebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
          <tr>
            <th style="width: 140px; font-weight: bold;">
                经营范围：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司的经营范围</span>
                <asp:TextBox ID="txtOperate" runat="server" Columns="50" Rows="6" TextMode="MultiLine"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                单位简介：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">对您所在的单位的简要介绍</span>
                <asp:TextBox ID="txtIntroduction" runat="server" Columns="50" Rows="6" TextMode="MultiLine"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                单位电话：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司联系电话，区号-8位号码的形式填写</span>
                <asp:TextBox ID="txtTelephone" runat="server" Columns="40" MaxLength="48" CssClass="easyui-validatebox" required="true" validType="phone" invalidMessage="电话号码不正确！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                <a href="#">单位传真：</a><a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司传真号码</span>
                <asp:TextBox ID="txtFax" runat="server" Columns="40" MaxLength="48" CssClass="easyui-validatebox" required="true" invalidMessage="传真号码不正确！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                邮政编码：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">您所在地区的邮编</span>
                <asp:TextBox ID="txtZip" runat="server" Columns="40" MaxLength="48" CssClass="easyui-validatebox" required="true" validType="zip" invalidMessage="邮编格式不正确！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                公司网址：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司网站主页，以http开始</span>
                <asp:TextBox ID="txtSite" runat="server" Columns="50" MaxLength="48" CssClass="easyui-validatebox" required="true" invalidMessage="公司网站主页为必填项！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                公司邮箱：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司的邮箱地址</span>
                <asp:TextBox ID="txtEmail" runat="server" Columns="40" MaxLength="48"  CssClass="easyui-validatebox" required="true" validType="email" invalidMessage="电子邮件格式不正确！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                公司地址：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">您所在公司的详细地址</span>
                <asp:TextBox ID="txtAddress" runat="server" Columns="80" MaxLength="48"  CssClass="easyui-validatebox" required="true" invalidMessage="公司地址为必填项！"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <th style="width: 140px; font-weight: bold;">
                银行户名：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司的常用银行帐号户名</span>
                <asp:TextBox ID="txtBankname" runat="server" Columns="40" MaxLength="48"  CssClass="easyui-validatebox" required="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                银行账号：<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司的常用银行帐号</span>
                <asp:TextBox ID="txtAccount" runat="server" Columns="40" MaxLength="48"  CssClass="easyui-validatebox" required="true" validType="regex[/^\d{10,20}$/g]" invalidMessage="您输入的帐户不正确，银行账户数字组成，请重新输入！"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                乘车路线：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">达到公司的乘车路线</span>
                <asp:TextBox ID="txtRoute" runat="server" Columns="50" Rows="6" TextMode="MultiLine"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
       </table>
       <table class="table">
       <caption style="text-align:left; font-style:italic; font-size:13px;">本次变更必填</caption>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                变更责任人：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更的责任人</span>
                <asp:TextBox ID="li_logmanage" runat="server" Width="60" CssClass="easyui-validatebox" required="true"></asp:TextBox>
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
                <asp:TextBox ID="ui_logcontent" runat="server" Columns="50"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                &nbsp;
                <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                    Text="提交" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" Visible="false" runat="server" CssClass="button" OnClick="btnSave_Click"
                    Text="删除" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="button" Text="重置" />
                &nbsp;
            </td>
        </tr>
    </table>
    <!-- 内容模块 -->
</asp:Content>
