<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_GradeBind.aspx.cs" Inherits="wwwroot.Manage.HR.HR_GradeBind" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
        table.table input[type='text'], select
        {
            width: 98%;
        }
        table.table input[type='text'], select
        {
            border: solid 1px #767676;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >> 级别管理 >> 绑定职务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-GradeBind" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td style="width: 240px;">
                        <a href="#" title='查看所有帮助' class="helpall">你正在进行级别与职务的绑定&nbsp;[?]</a>
                    </td>
                    <td>
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                            CssClass="button" OnClick="Button2_Click" />
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                   &nbsp;
                </th>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
