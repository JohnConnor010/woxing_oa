<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="UpLoad.aspx.cs" Inherits="wwwroot.Manage.DownLoad.UpLoad" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
     表格工具下载 >> 文件上传
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="DownLoad" CurIndex="2"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 
    <table class="table">
            <tr class="">
                <td style="padding-left: 15px;">
                    文件：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr class="">
                <td style="padding-left: 15px;" valign="top">
                    描述：
                </td>
                <td>
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="6" Columns="50"></asp:TextBox>                   
                </td>
            </tr>
            <tr class="">
                <td style="padding-left: 15px;" valign="top">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="提交" onclick="Button1_Click" />                
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

