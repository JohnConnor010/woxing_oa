<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static"
    AutoEventWireup="true" CodeBehind="AddNotify.aspx.cs" Inherits="wwwroot.Manage.XZ.AddNotify" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
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
    行政管理 >> 公告管理 >> 创建公告
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_notify" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table width="100%" class="table"><tr>
                <th style="width: 120px; font-weight: bold;">
                    * 分类：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择公告分类</span>
                    <asp:DropDownList ID="ui_category" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width:350px;">
                </td>
            </tr>
            <tr>
                <th style=" font-weight: bold;">
                    * 标题：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请输入公告标题</span>
                    <asp:TextBox ID="ui_title" runat="server" Width="400"  dataType="Require"
                        require="true" msg="请输入公告标题"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <th style="font-weight: bold;">
                    * 按部门范围：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note">(发布范围取部门、角色和人员的并集)至少选一项</span>
                    <asp:HiddenField ID="hidden_DepartmentList" runat="server" />
                    <asp:TextBox ID="txtDepartmentList" runat="server" Columns="50" Rows="3" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectDepartment.aspx?CompanyId=11','选择部门','hidden_DepartmentList','txtDepartmentList',420,310);">选择</a>
                </td>
                <td rowspan="3" style="border: 1px solid #BDDBEF; vertical-align:top;">
                    <h3>选择规则</h3>
                    发送范围是部门、角色、人员的并集，不选择默认为全部。
                    <br />
                    1.如果只发送给<b>运营中心</b>，只需要在部门中选择<b>运营中心</b>即可。
                    <br />2.如果只想发送给某个人，只需要在人员中选择这个人即可。
                    <br />3.如果只想发送给<b>运营专员</b>,只需要在职务中选择<b>运营专员</b>即可。
                    <br />4.如果同时选择<b>部门</b>与<b>职务</b>，结果只会发送给这个部门的这个职务的所有人。
                    <br />5.如果同时选择<b>部门</b>与<b>职务</b>与<b>人员</b>，那么如果这个人员的部门或职务有变动，他将不能收到信息。
                </td>
            </tr>
            <tr class="">
                <th style="font-weight: bold;">
                    *按角色范围：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note">(发布范围取部门、角色和人员的并集)至少选一项</span>
                    <asp:HiddenField ID="hidden_RoleList" runat="server" />
                    <asp:TextBox ID="txtRoleList" runat="server" Columns="50" Rows="3" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectRoles.aspx?CompanyId=11','选择角色','hidden_RoleList','txtRoleList',430,320);">选择</a>
                </td>
            </tr>
            <tr class="">
                <th style="font-weight: bold;">
                    * 按人员范围：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note">(发布范围取部门、角色和人员的并集)至少选一项</span>
                    <asp:HiddenField ID="hidden_UserList" runat="server" />
                    <asp:TextBox ID="txtUserList" runat="server" Columns="50" Rows="3" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','hidden_UserList','txtUserList',468,380);">选择</a>
                </td>
            </tr>
            <tr>
                <th style="font-weight: bold;">
                    * 有效期：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">发布公告的起止时间</span>
                    <asp:TextBox ID="ui_starttime" runat="server" Width="90" class="easyui-datebox"></asp:TextBox> 至 <asp:TextBox class="easyui-datebox" ID="ui_stoptime" runat="server" Width="90" ></asp:TextBox>为空为手动终止
                </td>
                <td></td>
            </tr>
            <tr>
                <th style="font-weight: bold;">
                     弹窗提醒：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">如果需要弹出冒泡窗口提醒请勾选此项</span>
                    <asp:CheckBox ID="ui_ismes" runat="server" ToolTip="1" />发送弹窗提醒消息
                </td>
                <td></td>
            </tr>
            <tr>
                <th style="font-weight: bold;">
                     置顶显示：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">如果需要置顶显示请勾选此项</span>
                    <asp:CheckBox ID="ui_istop" runat="server" />使公告通知置顶
                </td>
                <td></td>
            </tr>
            <tr>
                <th style="font-weight: bold;">
                     附件：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">附件为PDF格式</span>
                    <asp:Literal ID="Annex_li" runat="server"></asp:Literal>
                    <asp:FileUpload ID="FileUpload1" runat="server" />格式：PDF
                </td>
                <td></td>
            </tr>
            <tr>
                <th style="font-weight: bold; vertical-align:top;">
                   <br /> * 内容：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td colspan="2">
                    <span class="note">公告内容</span>                    
                    <FCKeditorV2:FCKeditor ID="ui_content1" ToolbarSet="Basic" runat="server" Height="300"
                        Width="100%">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr>
                <th>
                </th>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                        OnClick="SubmitData" CssClass="button" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
