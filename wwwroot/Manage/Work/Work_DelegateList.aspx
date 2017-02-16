<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Work_DelegateList.aspx.cs" Inherits="wwwroot.Manage.Work.Work_DelegateList"
    ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    工作委托 >> 我的委托
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="work_delegate" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr>
                <td colspan="7">
                    状态：<asp:DropDownList 
                        ID="DropDownList1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        <asp:ListItem Value="0">委托状态</asp:ListItem>
                        <asp:ListItem Value="1">被委托状态</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="">
                <td width="50">
                    <span style="margin-left: 15px;">序</span>
                </td>
                <td style="width: 18%">
                    流程名称
                </td>
                <td style="width: 19%">
                    委托发起人
                </td>
                <td style="width: 15%">
                    被委托人
                </td>
                <td style="width: 15%">
                    委托时间
                </td>
                <td width="17%">
                    状态
                </td>
                <td style="width: 20">
                    操作
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="DelegateRepeater" runat="server">
            <ItemTemplate>            
            <tr class="">
                <td>
                    <span style="margin-left: 15px;"><%#Eval("Id") %></span>
                </td>
                <td>
                    <span style="color: #006600; font-weight: bold;"><%#Eval("FlowName") %></span>
                </td>
                <td><%#Eval("Principal")%></td>
                <td><%#Eval("BeThePrincipal")%></td>
                <td><%#Eval("StartTime") %></td>
                <td><%#Eval("ImageUrl") %></td>
                
                <td class="manage">
                    <a href="Work_EditDelegate.aspx?Id=<%#Eval("Id") %>">修改</a> 
                    <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("Id") %>' OnCommand="btnDelete_Command" />
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
