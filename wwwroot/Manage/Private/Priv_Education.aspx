﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Priv_Education.aspx.cs" Inherits="wwwroot.Manage.Sys.Priv_Education" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    个人资料
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="priv" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
    <table class="table" runat="server" id="t3">
            <tr>
                <td>
                    <center>
                        <iframe height="400" frameborder="0" width="90%" src="../include/KeyXmlEdit.aspx?table=TU_Employees&column=Education&appid=Priv-Edu&key=UserID&keyvalue=<%=WX.Main.CurUser.UserID %><%=WX.Main.CurUser.UserModel.ArchiveBySelf.ToBoolean()?"":"&ReadOnly=1" %>">
                        </iframe>
                    </center>
                </td>
            </tr>
        </table>
        <table class="table" runat="server" id="t1">
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    教育经历&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">修改个人教育经历。</span>
                    <FCKeditorV2:FCKeditor ID="ui_content" ToolbarSet="Basic" runat="server" Height="300"
                        Width="900">
                    </FCKeditorV2:FCKeditor>
                    &nbsp;
                </td>
            </tr>
            <tr runat="server" id="trSubmit">
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交保存" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="button" Text="取消重填" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table><br/>
        <table class="table" runat="server" id="t2" visible="false">
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    改换模板&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">修改个人教育经历。</span>
                    <div style="">
                        <asp:Literal runat="server" ID="lblContent"></asp:Literal>
                    </div>
                    <FCKeditorV2:FCKeditor ID="ui_content1" ToolbarSet="Basic" runat="server" Height="300"
                        Width="900">
                    </FCKeditorV2:FCKeditor>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="button" OnClientClick="return confirm('如果提交，原来信息将被新模板代替，确认提交？')"
                        Text="提交保存" OnClick="btnSubmit1_Click" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" CssClass="button" Text="取消重填" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
