<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Work_Apply.aspx.cs" Inherits="wwwroot.Manage.Work.Work_Apply" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<style type="text/css">
.but{ width:100px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的申请
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="run_apply_list" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table align="center" width="600" height="200" style="text-align:center;">
       
        <tr style="font-weight: bold; text-align:center;">
            <td>
                办公区类<br />|<hr />
            </td>
            <td>
                普通类<br />|<hr />
            </td>
            <td>
                人事变动<br />|<hr />
            </td>
            <td>
                办公用品<br />|<hr />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Button ID="Button1" runat="server" CssClass="but" Text="会议室申请" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=10" />
                <asp:Button ID="Button5" runat="server" CssClass="but" Text="微电影室申请" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=11" />
                <asp:Button ID="Button9" runat="server" CssClass="but" Text="多功能厅申请" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=12" />
                
                
                
            </td>
            <td valign="top">
                <asp:Button ID="Button6" runat="server" CssClass="but" Text="请假申请" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=5" />
                <asp:Button ID="Button2" runat="server" CssClass="but" Text="加班申请" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=8" />
                <asp:Button ID="Button10" runat="server" CssClass="but" Text="外出登记" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=9" />
                
                
                
            </td>
            <td valign="top">
                <asp:Button ID="Button11" runat="server" CssClass="but" Text="转正申请" PostBackUrl="/Manage/Work/Work_ApplyOfficial.aspx" /> 
                <asp:Button ID="Button7" runat="server" CssClass="but" Text="调岗申请" PostBackUrl="/Manage/Work/Work_ApplyTransferKong.aspx?type=1" />
                <asp:Button ID="Button3" runat="server" CssClass="but" Text="升职申请" PostBackUrl="/Manage/Work/Work_ApplyTransferKong.aspx?type=2" />
                <asp:Button ID="Button13" runat="server" CssClass="but" Text="离职申请" PostBackUrl="/Manage/Work/Work_ApplyLeavejobs.aspx" />
                
                
            </td>
            <td valign="top">
                <asp:Button ID="Button8" runat="server" CssClass="but" Text="公章申请" PostBackUrl="/Manage/Work/Work_FillIn.aspx?Flow_Id=15" />
                <asp:Button ID="Button4" runat="server" CssClass="but" Text="办公用品申请" PostBackUrl="" />
                <asp:Button ID="Button12" runat="server" CssClass="but" Text="易耗品申请" PostBackUrl="" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
