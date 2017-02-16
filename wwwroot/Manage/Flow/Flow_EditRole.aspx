<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Flow_EditRole.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_EditRole" ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="../../App_Scripts/popup.js"></script>
<script type="text/javascript">
    $(function () {
        if ($('#ddlScope').val() == "CUSTOM") {
            $('#custom_dept').show();
        }
        $('#ddlScope').change(function () {
            if ($('#ddlScope').val() == "CUSTOM") {
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
    流程管理 >> 权限管理 >> 修改权限
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-modi" CurIndex="3" Param1="{Q:id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tbody>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 授权类型：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">管理权限（包含查询及监控权限），可执行的操作：转交、委托、结束、删除</span>
                    <asp:DropDownList ID="ddlManageType" runat="server">
                    </asp:DropDownList>
&nbsp;&nbsp; </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 授权范围(人员)：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">点击“选择”选择人员，支持多选</span>
                    <asp:HiddenField ID="hidden_UserList" runat="server" />
                    <asp:TextBox ID="txtUserList" runat="server" Columns="40" Rows="5" ReadOnly="true" 
                        TextMode="MultiLine"></asp:TextBox>
&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','hidden_UserList','txtUserList',468,395);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 授权范围(部门)：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">点击“选择”选择部门，支持多选</span>
                    <asp:HiddenField ID="hidden_DepartmentList" runat="server" />
                    <asp:TextBox ID="txtDepartmentList" runat="server" Columns="40" Rows="5" ReadOnly="true" 
                        TextMode="MultiLine"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectDepartment.aspx?CompanyId=11','选择部门','hidden_DepartmentList','txtDepartmentList',420,310);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    *授权范围(角色)：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">点击“选择”选择角色，支持多选</span>
                    <asp:HiddenField ID="hidden_RoleList" runat="server" />
                    <asp:TextBox ID="txtRoleList" runat="server" Columns="40" Rows="5" ReadOnly="true" 
                        TextMode="MultiLine"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectRoles.aspx?CompanyId=11','选择角色','hidden_RoleList','txtRoleList',430,230);">选择</a></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 管理范围：&nbsp;<a class="help" href="#">[?]</a> 
                </th>
                <td>
                    <span class="note" style="display: none;">此人所管辖的范围</span>
                    <asp:DropDownList ID="ddlScope" runat="server">
                        <asp:ListItem Value="SELF_ORG">本机构</asp:ListItem>
                        <asp:ListItem Value="ALL_DEPT">所有部门</asp:ListItem>
                        <asp:ListItem Value="SELF_DEPT">本部门及下属部门</asp:ListItem>
                        <asp:ListItem Value="SELF_BRANCH">本部门(不含下属部门)</asp:ListItem>
                        <asp:ListItem Value="CUSTOM">自定义部门</asp:ListItem>
                    </asp:DropDownList><br />
                    <div id="custom_dept" style="display:none" runat="server">
                        <asp:HiddenField ID="hidden_AllDepartment" runat="server" />
                        &nbsp;<asp:TextBox ID="txtAllDepartment" TextMode="MultiLine" Columns="40" Rows="5" runat="server"></asp:TextBox>
                    &nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectDepartment.aspx?CompanyId=11','选择部门','hidden_AllDepartment','txtAllDepartment',420,310);">选择</a></div>
                </td>
            </tr>
            <tr class="">
                <th>
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text=" 保 存 " 
                        onclick="btnSave_Click" />
&nbsp;&nbsp;&nbsp;<input ID="btnReset" type="reset" class="button" Value=" 重 置 " />
&nbsp;</td>
            </tr>
        </tbody>
    </table>
</asp:Content>
