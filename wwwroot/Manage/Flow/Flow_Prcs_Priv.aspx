<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_Prcs_Priv.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_Priv" ClientIDMode="Static" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
<script type="text/javascript">
    $(function () {
        $('#ddlCope').change(function () {
            if ($('#ddlCope').val() == "CUSTOM") {
                $('#custom_dept').show();
            }
            else {
                $('#custom_dept').hide();
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="4" Param1="{Q:FlowId}" Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <table class="table">
        <tbody>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    设置经办权限
                </th>
                <td>
                    (经办权限为“部门”、“角色”、“人员”三者的合集) </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 授权范围(人员)：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:HiddenField ID="hidden_UserList" runat="server" />
                    <asp:TextBox ID="txtUserList" runat="server" Columns="40" Rows="5" 
                        TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','hidden_UserList','txtUserList',468,395);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 授权范围(部门)：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:HiddenField ID="hidden_DepartmentList" runat="server" />
                    <asp:TextBox ID="txtDepartmentList" runat="server" Columns="40" Rows="5" 
                        TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectDepartment.aspx?CompanyId=11','选择部门','hidden_DepartmentList','txtDepartmentList',420,310);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    *授权范围(角色)：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:HiddenField ID="hidden_RoleList" runat="server" />
                    <asp:TextBox ID="txtRoleList" runat="server" Columns="40" Rows="5" 
                        TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectRoles.aspx?CompanyId=11','选择角色','hidden_RoleList','txtRoleList',430,230);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    *状态改变：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                   <asp:DropDownList ID="drop_UpdateTable" runat="server">
                    <asp:ListItem Text="无" Value=""></asp:ListItem>
                    <asp:ListItem Text="员工考勤状态" Value="AT_Status"></asp:ListItem>
                    </asp:DropDownList>键值：<asp:TextBox ID="keyvalue" runat="server">State=1</asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th>
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text=" 保 存 " OnClick="btnSave_Click" />
                    &nbsp;&nbsp;&nbsp;<input type="reset" class="button" value=" 重 置 " />
                    &nbsp;
                </td>
            </tr>
        </tbody>
 </table>
</asp:Content>
