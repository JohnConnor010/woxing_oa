<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_Signin.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Signin" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 考勤管理 >> 今日签到
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-kq" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <br />
    <center>
        <asp:Button ID="Button1" runat="server" Text="上班签到" onclick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
           ID="Button2" runat="server" Text="下班签到" onclick="Button2_Click" />
        <br /><br /><br />
        <table align="center" class="table">
            <tr style="font-weight:bold;">
                <td>上班签到
                </td>
                <td>下班签到
                </td>
                 <td>其它
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                </td>
                <td>
                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                </td>
                 <td>
                     <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </center>
    <div style="padding:20px; line-height:200%;">
    <h4>签到说明：</h4>
    1、上班签到时间：每天<%=WX.AT.Signin.OnStart.ToString("HH:mm")%>开始，<%=WX.AT.Signin.OnStop.ToString("HH:mm")%>结束。<br />
    2、下班签到时间：每天<%=WX.AT.Signin.OffStart.ToString("HH:mm")%>开始，<%=WX.AT.Signin.OffStop.ToString("HH:mm")%>结束。<br />    
    3、非签到时间：非签到时间系统关闭签到功能，员工自己不可再签，如迟到或早退须报到行政部，由行政人员代签，如不报系统自动判为“旷工”处理。<br />
    4、迟到签到：<%=WX.AT.Signin.OnStop.ToString("HH:mm")%>上班以后到<%=WX.AT.Signin.OnStop.AddMinutes(WX.AT.Signin.KGMinutes).ToString("HH:mm")%>须到行政部报“迟到”，超过<%=WX.AT.Signin.OnStop.AddMinutes(WX.AT.Signin.KGMinutes).ToString("HH:mm")%>以“旷工”处理。<br />
    5、旷工签到：超过<%=WX.AT.Signin.OnStop.AddMinutes(WX.AT.Signin.KGMinutes).ToString("HH:mm")%>须到行政部报“旷工”，旷工签到后为“旷工半天”，否则视为“旷工一天”。<br />
    6、返回：请假回公司后须到行政办“销假”、外出回公司后须到行政办“返回”、出差回公司后须到行政办“返差”，如超过截止日期不办理系统自动按“请假”或“旷工”处理。<br />
    7、超期返回：请假、外出、出差都有相对应的截止日期，如超过截止日期<%=WX.AT.Signin.KGMinutes%>分钟以内系统自动按“请假”处理，如超过截止日期<%=WX.AT.Signin.KGMinutes%>分钟以后系统自动按“旷工”处理。<br />
    <% DateTime dtime = new DateTime(2012,8,15,9,0,0); %>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如：请假时间<%=dtime.ToString("yyyy-MM-dd HH:mm") %>截止时间为<%=dtime.AddDays(1).ToString("yyyy-MM-dd HH:mm")%>，如果<%=dtime.AddDays(1).ToString("yyyy-MM-dd HH:mm")%>之前没有到行政办销假视为“请假”，如果<%=dtime.AddDays(1).AddMinutes(WX.AT.Signin.KGMinutes).ToString("yyyy-MM-dd HH:mm")%>之前还没有到行政办销假视为“旷工”。<br />
    注意：一天中上班签到或下班签到一项为空当天视为“旷工”，非签到时间或“迟到”或“请假”或“外出”或“出差”请务必报行政。
    </div>
</asp:Content>
