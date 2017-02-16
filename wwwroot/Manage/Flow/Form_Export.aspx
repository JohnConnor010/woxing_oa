<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Form_Export.aspx.cs" Inherits="wwwroot.Manage.Flow.Form_Export" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 表单定义 >> 表单导出
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="form-modi" CurIndex="5" Param1="{Q:FormId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        你正在导出一个表单..&nbsp;
                    </td>
                </tr>
            </thead>
            <tr>
                <td colspan="2" style="text-align:center;">
                    最后一次生成时间：<asp:Literal runat="server" ID="liLastUpdateTime"></asp:Literal>
                    <br/>
                    <asp:Button runat="server" ID="btnDownLoadFile" OnClick="DownloadFile" Text="下载文件" />
                    <asp:Button runat="server" ID="btnReBuildFile" OnClick="ReBuildFile" Text="生成最新" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
