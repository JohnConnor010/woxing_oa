<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_EditDepartment.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_EditDepartment"
    ClientIDMode="Static" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 部门管理 >> 修改部门
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="dept-modi" CurIndex="2" Param1="{Q:companyID}" Param2="{Q:DepartmentId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tr>
            <th style="width: 145px; font-weight: bold;">
                所属公司或单位：<a href="#" class="help">[?]</a></th>
            <td>
                <asp:Label ID="CompanyName" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                * 上级部门：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">部门所属的单位</span>
                <asp:DropDownList ID="ddlParentId" runat="server" CssClass="formElentment" Width="250px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                &nbsp;部门编号：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">部门编号，有三位数字组成</span>
                <asp:TextBox ID="txtDepartmentNO" runat="server" MaxLength="3" required="true" validType="number"></asp:TextBox>
                &nbsp;&nbsp;<span class="red"><span id="indicator"></span></span>
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                * 部门名称：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司部门的名称，比如：财务部...</span>
                <asp:TextBox ID="txtDepartmentName" runat="server" Columns="30" MaxLength="48" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                是否为分支机构：&nbsp;<a href="#" class="help">[?]</a></th>
            <td>
                <span class="note">当前部门是否为分支机构，如果是，请选择</span>
                <asp:CheckBox ID="chkIsSubOrgan" runat="server" Text="作为分支机构" />
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                部门主管：&nbsp;<a href="#" class="help">[?]</a></th>
            <td>
                <span class="note">如果参与流程，请选择部门主管</span>
                <asp:HiddenField ID="hidden_txtHost" runat="server" />
                <asp:TextBox ID="txtHost" runat="server" Columns="30" Rows="2" 
                    TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择部门主管','hidden_txtHost','txtHost',468,395);">选择</a></td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                部门副经理：&nbsp;<a href="#" class="help">[?]</a></th>
            <td>
                <span class="note">请选择部门副经理</span>
                <asp:HiddenField ID="hidden_txtSubHosts" runat="server" />
                <asp:TextBox ID="txtSubHosts" runat="server" Columns="30" Rows="2" 
                    TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择部门副经理','hidden_txtSubHosts','txtSubHosts',468,395);">选择</a></td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                部门助理：&nbsp;<a href="#" class="help">[?]</a></th>
            <td>
                <span class="note">请选择部门助理</span>
                <asp:HiddenField ID="hidden_txtAssistants" runat="server" />
                <asp:TextBox ID="txtAssistants" runat="server" Columns="30" Rows="2" 
                    TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择部门助理','hidden_txtAssistants','txtAssistants',468,395);">选择</a></td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                上级主管领导：&nbsp;<a href="#" class="help">[?]</a></th>
            <td>
                <span class="note">请选择上级主管领导</span>
                <asp:HiddenField ID="hidden_txtUpHost" runat="server" />
                <asp:TextBox ID="txtUpHost" runat="server" Columns="30" Rows="2" 
                    TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','上级主管领导','hidden_txtUpHost','txtUpHost',468,395);">选择</a></td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                上级分管领导：&nbsp;<a href="#" class="help">[?]</a></th>
            <td>
                <span class="note">请选择上级分管领导</span>
                <asp:HiddenField ID="hidden_txtUpSubHosts" runat="server" />
                <asp:TextBox ID="txtUpSubHosts" runat="server" Columns="30" Rows="2" 
                    TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','上级分管领导','hidden_txtUpSubHosts','txtUpSubHosts',468,395);">选择</a></td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                联系电话：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">部门联系电话</span>
                <asp:TextBox ID="txtPhone" runat="server" MaxLength="48"  CssClass="easyui-validatebox" required="true" validType="phone"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                传真号码：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">公司或部门传真机号码</span>
                <asp:TextBox ID="txtFax" runat="server" CssClass="easyui-validatebox" required="true" validType="regex[/^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/]" invalidMessage="请输入正确的传真号码"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                部门地址：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">部门地址：适用于异地办公的部门</span>
                <asp:TextBox ID="txtAddress" runat="server" Columns="50" MaxLength="48" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;&nbsp;<span
                    class="red"></span>
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                部门机构排序：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">部门机构的排序号</span>
                <asp:TextBox ID="txtSort" runat="server" class="easyui-numberbox" min="0" max="100"
                    required="true" MaxLength="3" CssClass="easyui-validatebox" validType="number">0</asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 145px; font-weight: bold;">
                备注：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">部门的备注信息</span>
                <asp:TextBox ID="txtContent" runat="server" Columns="60" Rows="6" TextMode="MultiLine"></asp:TextBox>
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
                    Text="保存更改" />
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="button" Text="重置"
                    OnClick="btnReset_Click" />
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
