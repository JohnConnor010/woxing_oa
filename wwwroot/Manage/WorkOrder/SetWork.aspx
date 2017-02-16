<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="SetWork.aspx.cs" Inherits="wwwroot.Manage.WorkOrder.SetWork" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
        .content
        {
            width: 400px;
            height: 370px;
            background-image: url(/images/mywork8.jpg);
        }
        .YM
        {
            color: #5C1400;
            border: 1px solid #5C1400;
            padding: 4px;
            width: 75px;
            text-align: center;
            float: left;
            font-weight: bold;
        }
        .days
        {
        	padding-top:15px;
            background-color: White;
            height: 280px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    工作单管理 >> 排班设置
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="WorkOrder_Set" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table align="center" width="500" style="margin: 0 auto;">
        <tr>
            <td>
                <div class="content">
                    <div style="height: 100px; padding: 10px;">
                        <div style="clear: both; height: 5px;">
                        </div>
                        <div style="float: left;">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/images/work_left.gif"
                                OnClick="ImageButton1_Click" />&nbsp;</div>
                        <div class="YM">
                            <asp:HiddenField ID="HiddenYear" runat="server" />
                            <asp:HiddenField ID="HiddenMonth" runat="server" />
                            <asp:Literal ID="Literal1" runat="server" Text="2012年04月"></asp:Literal></div>
                        <div style="float: left;">
                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/images/work_right.gif"
                                OnClick="ImageButton2_Click" /></div>
                                <div style=" width:200px; text-align:right;">
                            &nbsp;<asp:Button ID="Button1" Visible="false" runat="server" Text="添加这个月" 
                                        onclick="Button1_Click" /></div>
                        <div style="clear: both; height: 10px;">
                        </div>
                        <div class="days">
                            <asp:DataList ID="DataList1" runat="server" RepeatColumns="7" RepeatDirection="Horizontal" CssClass="table1">
                                <ItemTemplate>
                                <%#Eval("sHours").ToString()!="-1"?"<a href='?sdate="+Eval("sDate").ToString()+"'>":"" %>
                                    <div style='background-color: <%#Eval("sColor")%>; text-align:center;'>
                                        <div style="font-weight:bold; font-size:14px; line-height:200%;">
                                            <%#Eval("sDay")%>
                                        </div>
                                        <div>
                                            <span style="color:#666; font-size:10px;"><%#Eval("sWeekNum")%></span><%#Eval("sHours").ToString()=="0"?"休":(Eval("sHours").ToString()=="-1"?"无":"班")%>
                                        </div>
                                    </div>
                                    <%#Eval("sHours").ToString()!="-1"?"</a>":"" %>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
