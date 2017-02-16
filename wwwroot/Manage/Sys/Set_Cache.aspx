<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Set_Cache.aspx.cs" Inherits="wwwroot.Manage.Sys.Set_Cache" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
系统设置 >> 缓存更新
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="sysSet" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tr>
            <td style="width: 200px; font-weight:bold;">
                所有缓存(All)
            </td>
            <td style="width: 60px;">
                <asp:Button runat="server" ID="Button10" Text="更新" OnClick="Update" CommandName="All" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                公司库(Company)
            </td>
            <td>
                <asp:Button runat="server" ID="btnUpdate" Text="更新" OnClick="Update" CommandName="Company" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                部门缓存(Department)
            </td>
            <td>
                <asp:Button runat="server" ID="Button4" Text="更新" OnClick="Update" CommandName="Department" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                职务(Duty)
            </td>
            <td>
                <asp:Button runat="server" ID="Button5" Text="更新" OnClick="Update" CommandName="Duty" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                功能(Function)
            </td>
            <td>
                <asp:Button runat="server" ID="Button7" Text="更新" OnClick="Update" CommandName="Function" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                菜单(Menu)
            </td>
            <td>
                <asp:Button runat="server" ID="Button8" Text="更新" OnClick="Update" CommandName="Menu" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                等级(Grade)
            </td>
            <td>
                <asp:Button runat="server" ID="Button9" Text="更新" OnClick="Update" CommandName="Grade" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                用户(User)
            </td>
            <td>
                <asp:Button runat="server" ID="Button6" Text="更新" OnClick="Update" CommandName="User" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
