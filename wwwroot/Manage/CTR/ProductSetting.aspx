<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="ProductSetting.aspx.cs" Inherits="wwwroot.Manage.CTR.ProductSetting" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    销售管理 >> 产品管理 >> 环境设置
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="sysSet" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table1">
        <tbody>
            <tr>
                <th style="width: 200px; font-weight: bold;">
                    &nbsp;* 是否启用产品部门：
                </th>
                <td>
                    &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                    <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                    </asp:RadioButtonList>&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 200px; font-weight: bold;">
                    &nbsp;* 产品部门是否只有一个：
                </th>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatColumns="2">
                    <asp:ListItem Value="1" Text="一个"></asp:ListItem>
                    <asp:ListItem Value="0" Text="多个"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保存" onclick="btnSave_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
