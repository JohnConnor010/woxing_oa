<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Duty_Manage.aspx.cs" Inherits="wwwroot.Manage.Sys.Func_Manage" %>

<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 新增职务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="duty" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td style="width: 160px;">
                        <a href="#" title='查看所有帮助' class="helpall">你正在添加一个职务&nbsp;[?]</a>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                            CssClass="button" OnClick="Button2_Click" />
                        &nbsp;&nbsp;<input type="reset" class="button" value='重置' />
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 分类&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择职务分类</span>
                    <asp:DropDownList ID="DutyCatagory" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务编号&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">本系统职务编号 3位数字组成</span>
                    <asp:TextBox ID="DutyNO" name="DutyNO" runat="server" MaxLength="3" Width="200" dataType="Custom"
                        regexp="/^\d{3}$/" msg="本系统职务编号 3位数字组成"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务名称&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">职务名称由20个字以内的汉字组成</span>
                    <asp:TextBox ID="DutyName" name="DutyName" runat="server" Width="200" MaxLength="20"
                        dataType="Chinese" msg="本系统职务名称不能为空，且必须为汉字"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 默认级别&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                <asp:DropDownList ID="ddlGradeId" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职责&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                <FCKeditorV2:FCKeditor ID="FORM_CONTENT" ToolbarSet="Basic" runat="server" Height="300" Width="850px">
                </FCKeditorV2:FCKeditor>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
