<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Form_Import.aspx.cs" Inherits="wwwroot.Manage.Flow.Form_Import" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 表单定义 >> 表单导入
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="form-modi" CurIndex="4" Param1="{Q:FormId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        你正在导入一个表单，选择你制作的或者用于恢复的表单文件：
                    </td>
                </tr>
            </thead>
            <tr>
                <td colspan="2" style=" text-align:center;">
                    <asp:FileUpload runat="server" ID="fuForm" Width="400" />&nbsp;<asp:Button 
                        Text="上传" runat="server" ID="btnUpload" 
                        style="height:20px;padding:2px 2px 2px 2px;" onclick="btnUpload_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
