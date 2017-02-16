<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Form_Modi.aspx.cs" Inherits="wwwroot.Manage.Flow.Form_Modi" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 表单定义 >> 表单修改
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="form-modi" CurIndex="2" Param1="{Q:FormId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        你正在修改一个表单&nbsp;<a href="#" title='查看所有帮助' class="helpall">[?]</a> &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                            OnClick="SubmitData" CssClass="button" />
                        &nbsp;&nbsp;<input type="reset" class="button" value='重置' />
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 表单名称&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td style="width: 700px;">
                    <span class="note">表单名称必须是汉字</span>
                    <asp:TextBox ID="Name" name="Name" runat="server" MaxLength="20" Width="100" dataType="Chinese"
                        require="true" msg="表单名称必须是汉字"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 表单类型&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">表单类型是表单的组织方式</span>
                    <asp:DropDownList ID="ddlCatagory" name="Type" runat="server" Width="200" dataType="Require"
                        require="true" msg="表单类型不能为空">
                    </asp:DropDownList>                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 所属部门&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">表单所属部门</span>
                    <asp:DropDownList ID="ddlDept" name="Dept" runat="server" Width="200" dataType="Require"
                        require="true" msg="所属部门不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
