<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="User_UrgentLink.aspx.cs" Inherits="wwwroot.Manage.HR.User_UrgentLink" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 员工档案
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="user-detail" CurIndex="7" Param1="{Q:UserID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        
    <table class="table" runat="server" id="t3">
            <tr>
                <td>
                    <center>
                        <iframe height="400" frameborder="0" width="90%" src="../include/KeyXmlEdit.aspx?table=TU_Employees&column=UrgentLink&appid=Priv-UrgentLink&key=UserID&keyvalue=<%=WX.Request.rUserId %>">
                        </iframe>
                    </center>
                </td>
            </tr>
        </table>
        <table class="table" runat="server" id="t1">
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    紧急联络人&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">修改员工紧急联络人。</span>
                    <FCKeditorV2:FCKeditor ID="ui_content" ToolbarSet="Basic" runat="server" Height="300"
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
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交保存" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="button" Text="取消重填" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br/>
        <table class="table" runat="server" id="t2" visible="false">
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    改换模板&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">修改员工紧急联络人。</span>
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
                    <asp:Button ID="Button1" runat="server" OnClientClick="return confirm('如果提交，原来信息将被新模板代替，确认提交？')"
                        CssClass="button" Text="提交保存" OnClick="btnSubmit1_Click" />
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