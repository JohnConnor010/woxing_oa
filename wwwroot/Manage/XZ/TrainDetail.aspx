<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="TrainDetail.aspx.cs" Inherits="wwwroot.Manage.XZ.TrainDetail" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavigationHolder" runat="server">我的考核培训记录
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_mytrain" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<table class="table3" style="line-height: 200%;">
            <tr>
                <td>
                    <b>标题:</b>
                </td>
                <td>
                    <asp:Literal ID="li_title" runat="server" Text=""></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td width="80">
                    <b>类型:</b>
                </td>
                <td>
                    <asp:DropDownList ID="drop_type" runat="server">
                    <asp:ListItem Value="1" Text="考核"></asp:ListItem>
                    <asp:ListItem Value="2" Text="培训"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <b>时间:</b>
                </td>
                <td>
                    <asp:Literal ID="li_runtime" runat="server" Text=""></asp:Literal>&nbsp;&nbsp;<b>地点:</b><asp:Literal ID="li_addr" runat="server" Text=""></asp:Literal>
                </td>
            </tr>
          
            <tr>
                <td>
                    <b>参与人员:</b>
                </td>
                <td>
                    <asp:Literal ID="li_usersname" runat="server" Text=""></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b> 内容:</b>
                </td>
                <td>
                    <asp:Literal ID="li_content" runat="server" Text=""></asp:Literal>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <b>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal></b>
                </td>
                <td>
                    <asp:Literal ID="li_formcontent" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: left; padding: 5px;">
                    <asp:Button ID="Button1" runat="server" Text=" 提 交 " Visible="false" CssClass="button" OnClick="Button1_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
</asp:Content>

