<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="NotifyFiles.aspx.cs" Inherits="wwwroot.Manage.XZ.NotifyFiles" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
<script type="text/javascript">
    $(function () {
        $('#ui_Area').change(function () {
            if ($('#ui_Area').val() == "1") {
                $('#trdept').show();
                $('#truser').hide();
            }
            else if ($('#ui_Area').val() == "5") {
                $('#truser').show();
                $('#trdept').hide();
            } else {
                $('#truser').hide();
                $('#trdept').hide();
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    行政管理 >> 公告管理 >> 创建公告
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_notifyfiles" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table width="100%" class="table"><tr>
                <th style="width: 120px; font-weight: bold;">
                    * 编号：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">文件编号</span>
                    <asp:TextBox ID="ui_Code" runat="server" Width="400" Text="济行政字"  dataType="Require"
                        require="true" msg="请输入文件编号"></asp:TextBox>
                </td>
                <td style="width:350px;">
                </td>
            </tr><tr>
                <th style="width: 120px; font-weight: bold;">
                    * 范围：&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择接收人范围</span>
                     <asp:DropDownList ID="ui_Area" runat="server">
                     <asp:ListItem Value="1" Text="公司层面"></asp:ListItem>
                     <asp:ListItem Value="2" Text="部门内部"></asp:ListItem>
                     <asp:ListItem Value="3" Text="上级部门内部"></asp:ListItem>
                     <asp:ListItem Value="4" Text="顶级部门内部"></asp:ListItem>
                     <asp:ListItem Value="5" Text="文件上报"></asp:ListItem>
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
                        require="true" msg="请输入文件标题"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr id="trdept">
                <th style="font-weight: bold;">
                    * 按部门范围：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note">(发布范围取部门、角色和人员的并集)至少选一项</span>
                    <asp:HiddenField ID="hidden_DepartmentList" runat="server" />
                    <asp:TextBox ID="txtDepartmentList" runat="server" Columns="50" Rows="3" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectDepartment.aspx?CompanyId=11','选择部门','hidden_DepartmentList','txtDepartmentList',420,310);">选择</a>
                </td>
                <td style="border: 1px solid #BDDBEF; vertical-align:top;">
                    <h3>选择规则</h3>
                    发送范围是部门,不选择默认为全部。
                </td>
            </tr>
           
            <tr class="" id="truser" style="display:none;">
                <th style="font-weight: bold;">
                    * 按人员范围：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note">(发布范围取部门、角色和人员的并集)至少选一项</span>
                    <asp:HiddenField ID="hidden_UserList" runat="server" />
                    <asp:TextBox ID="txtUserList" runat="server" Columns="50" Rows="3" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                    &nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','hidden_UserList','txtUserList',468,380);">选择</a>
                </td>
                <td style="border: 1px solid #BDDBEF; vertical-align:top;">
                    <h3>选择规则</h3>
                    上报给指定人，不选择默认为部门主管。
                </td>
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
                    <asp:Button ID="Button1" runat="server" Text="保存拟写" OnClientClick="return Validator.Validate(this.form,3);"
                        OnClick="SubmitData1" CssClass="button" />
                    <asp:Button ID="Button2" runat="server" Text="保存并审批" OnClientClick="return Validator.Validate(this.form,3);"
                        OnClick="SubmitData3" CssClass="button" />
                    <asp:Button ID="Button3" runat="server" Text="直接发送" Visible="false" OnClientClick="return Validator.Validate(this.form,3);"
                        OnClick="SubmitData4" CssClass="button" />
                </td>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
