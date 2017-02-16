<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="User_Set.aspx.cs" Inherits="wwwroot.Manage.HR.User_Set" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 员工档案 >> 员工设置
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
  <uc1:MenuBar ID="MenuBar1" runat="server" Key="user-detail" CurIndex="9" Param1="{Q:UserID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table" style="line-height: 180%;">
        <tr>
            <th style="width: 200px;">
                授权用户自己修改档案：
            </th>
            <td>
                <asp:CheckBox ID="cbArchiveBySelf" runat="server" />
            </td>
            <td style="width:130px;">
                <asp:Button runat="server" OnClick="ModiArchiveBySelf" Text="设置" />
            </td>        
            </tr>
    </table>
</asp:Content>
