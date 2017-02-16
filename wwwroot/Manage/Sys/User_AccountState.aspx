<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="User_AccountState.aspx.cs" Inherits="wwwroot.Manage.Sys.User_AccountState" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 用户管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="account-Sys" CurIndex="3" Param1="{Q:CompanyId}" Param2="{Q:UserID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <div id="PanelManage">
        <table class="table">
            <caption style="text-align: left;">
                <asp:Literal runat="server" ID="liUserName"></asp:Literal></caption>
            <tr>
                <th style="width:40%; font-weight: bold;">
                    用户标识：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblUserId"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width:40%; font-weight: bold;">
                    用户名：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblUserName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style=" font-weight: bold;">
                    邮箱：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblEmail"></asp:Label>
                </td>
            </tr>        
            <tr>
                <th style=" font-weight: bold;">
                    创建时间：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblCreateDate"></asp:Label>
                </td>
            </tr>        
            <tr>
                <th style=" font-weight: bold;">
                    最后一次登录：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblLoginDate"></asp:Label>
                </td>
            </tr>        
            <tr>
                <th style=" font-weight: bold;">
                    最后一次修改密码：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblLastUpdatePwd"></asp:Label>
                </td>
            </tr>        
            <tr>
                <th style=" font-weight: bold;">
                    最后一次锁定密码：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblLastLock"></asp:Label>
                </td>
            </tr> 
            <tr>
                <th style=" font-weight: bold;">
                    是否在线：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblOnlineState"></asp:Label>
                </td>
            </tr> 
            <tr>
                <th style=" font-weight: bold;">
                    账户状态：
                </th>
                <td>
                    <asp:Label runat="server" ID="lblState"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" Text="锁定" ID="btnLock" OnClick="Lock" CssClass="button" />
                    <asp:Button runat="server" Text="解锁" ID="btnUnlock" OnClick="Unlock" CssClass="button" />
                </td>
            </tr>           
            </table>
    </div>
</asp:Content>
