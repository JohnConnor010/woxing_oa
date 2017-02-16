<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="TestMaster2.aspx.cs" Inherits="wwwroot.App_Demo.TestMaster2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
系统管理 >> 用户管理 >> 新增职务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <div style="float: left; width: 60%;">
        <a href="/Manage/sys/User_UserList.aspx">用户列表</a> <a href="/Manage/Sys/User_AddUser.aspx">
            新增用户</a> <a href="/Manage/Sys/Duty_List.aspx">职务列表</a> <a href="/Manage/Sys/Duty_Manage.aspx">
                新增职务</a> <a href="/Manage/Sys/Func_ListFunctions.aspx">功能列表</a> <a href="/Manage/Sys/Func_AddFunctions.aspx">
                    新增功能</a>
    </div>
    <div style="float: right; width: 35%; text-align: right;">
        <input type="text" name="keyword" runat="server" id="keyword" maxlength="10" />
        <asp:Button ID="Button1" runat="server" Text="搜索" CssClass="textbutton" /><asp:Button
            ID="Button3" runat="server" Text="删除" CssClass="textbutton" /></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        你正在添加一个职务&nbsp;<a href="#" title='查看所有帮助' class="helpall">[?]</a> &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                            CssClass="button" />
                        &nbsp;&nbsp;<input type="submit" name="Button2" value="生成HTML" onclick="return Validator.Validate(this.form,3);"
                            id="Submit1" class="button" />
                        &nbsp;&nbsp;<input type="reset" class="button" value='重置' />
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务编号&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">本系统职务编号 3位数字组成</span>
                    <asp:TextBox ID="DutyID" name="DutyID" runat="server" MaxLength="3" Width="200" dataType="Custom"
                        regexp="\d{3}" msg="本系统职务编号 3位数字组成"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 职务名称&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">职务名称由20个字以内的汉字组成</span>
                    <asp:TextBox ID="DutyName" name="DutyName" runat="server" Width="200" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
