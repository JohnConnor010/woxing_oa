<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="WorkOrder_AssignDeptCount.aspx.cs" Inherits="wwwroot.Manage.WorkOrder.WorkOrder_AssignDeptCount" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
        .content
        {
            height: 20px; padding-left:20px;
            
        }
        .YM
        {
            color: #5C1400;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    工作单管理 >> 工作量统计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="WorkOrder_Assign" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="content">
        <div style="float: left;">
            <asp:ImageButton ID="ImageButton1" runat="server" Height="21" ImageUrl="/images/work_left.gif"
                OnClick="ImageButton1_Click" />&nbsp;</div>
        <div style="float:left;">
            <asp:DropDownList ID="DropDownList1" Height="130px" runat="server"  AutoPostBack="true"
                CssClass="YM" onselectedindexchanged="DropDownList1_SelectedIndexChanged">
            </asp:DropDownList>
            </div>
        <div style="float: left;">
            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" Height="21" ImageUrl="/images/work_right.gif"
                OnClick="ImageButton2_Click" /></div>
    </div>
    <div style="clear: both; height: 10px;">
    </div>
    <table class="table1">
        <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
                <thead>
                    <td style="width: 100px;">
                        姓名
                    </td>
                    <td>
                        工作量
                    </td>
                </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="padding-left: 10px;">
                       &nbsp;&nbsp;<%#Eval("RealName")%></td>
                    <td >
                        <%# Eval("WorkCount").ToString() == "0" ? "<font color='red'>0</font>" : Eval("WorkCount").ToString()%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
