<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="User_ResetPwd.aspx.cs" Inherits="wwwroot.Manage.Sys.User_ResetPwd" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
系统管理 >> 用户管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="account-Sys" CurIndex="2" Param1="{Q:CompanyId}" Param2="{Q:UserID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
    <table class="table">
        <caption style="text-align: left;">
            <asp:Literal runat="server" ID="liUserName"></asp:Literal></caption>
        <tr>
            <td style="height: 200px; text-align: center; vertical-align: middle;">
                <div style="width: 400px; margin: 0 auto; padding: 10px 10px 10px 10px; text-align: left; font-size:14px;">
                    <p>如果密码已经丢失，只有通过重置密码设置新密码才能使用.<p>
                    <p>此用户的新密码为：<asp:Label runat="server" ID="lblNewPwd" Text="******"></asp:Label></p>
                    <div style=" text-align:center; border-top:dashed 1px #999; margin-top:10px; padding-top:10px;">
                    <asp:Button ID="Button2" runat="server" Text="重置密码" CssClass="button" OnClick="Button2_Click" /></div>
                 </div>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
