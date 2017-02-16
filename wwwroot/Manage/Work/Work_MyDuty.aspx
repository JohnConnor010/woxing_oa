<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_MyDuty.aspx.cs" Inherits="wwwroot.Manage.Work.Work_MyDuty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<br /><center><h1>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>工作职责</h1></center><br />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server"><br />
<div style=" padding:20px; line-height:200%; min-height:400px;">
    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
    </div>
</asp:Content>
