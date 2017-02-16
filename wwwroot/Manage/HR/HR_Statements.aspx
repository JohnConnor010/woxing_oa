<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_Statements.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Statements" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 考勤管理 >> 月报表管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <div style="float: left;"><uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-kq" CurIndex="3" />
    </div>
    <div style="float: right;">
        <font color="red" id="mes" runat="server"></font>&nbsp;&nbsp;年月：<asp:TextBox ID="ui_ctime"
            runat="server" style="width:80"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="搜索" OnClick="Button1_Click" /></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <center>
        <h2>
            中国经济网山东频道 考勤表</h2>
    </center>
    <br />
    <asp:DataList ID="DataList1" BackColor="White" runat="server" style="width:100%">
    <HeaderTemplate>
    <table class="table3">
    <thead>
        <tr>
            <td rowspan="2" style="width:30px;">
                序号
            </td>
            <td rowspan="2" style="width:50px">
                姓名
            </td>
            <td style="width:30px">
                日期
            </td>
            <% for (i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month); i++)
               { %>
            <td style="width:18px">
                <%=i %>
            </td>
            <%} %>
            <td colspan="7">
                出勤情况 （单位：天）
            </td>
            <td rowspan="2">
                出勤
            </td>
        </tr>
        <tr>
            <td>
                星期
            </td>
           <% for (i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month); i++)
               { %>
            <td>
                <%=Day[Convert.ToInt32(new DateTime(DateTime.Now.Year,DateTime.Now.Month,i).DayOfWeek)] %>
            </td>
            <%} %>
            <td style="width:30px">
                迟到
            </td>
            <td style="width:30px">
                早退
            </td>
            <td style="width:30px">
                事假
            </td>
            <td style="width:30px">
                病假
            </td>
            <td style="width:30px">
                旷工
            </td>
            <td style="width:30px">
                其它
            </td>
            <td style="width:55px">
                累计存假
            </td>
        </tr>
        </thead>
    </table>
    </HeaderTemplate>
            <ItemTemplate>
               <table class="table3" style="text-align:center;">
               <tr>
               <td style="width:30px">
              <%# Container.ItemIndex+1 %>
            </td>
            <td style="width:50px" >
                <%# Eval("RealName")%>
            </td>
            <td style="width:30px">
                &nbsp;&nbsp;
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day1").ToString()) %>
            </td>
            
            <td style="width:18px">
            <%# GetState(Eval("Day2").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day3").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day4").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day5").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day6").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day7").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day8").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day9").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day10").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day11").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day12").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day13").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day14").ToString())%>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day15").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day16").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day17").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day18").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day19").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day20").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day21").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day22").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day23").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day24").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day25").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day26").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day27").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day28").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day29").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day30").ToString()) %>
            </td>
            <td style="width:18px">
            <%# GetState(Eval("Day31").ToString()) %>
            </td>
            <td style="width:30px">
                <%# Eval("LCD") %>
            </td>
            <td style="width:30px">
                <%# Eval("LZT") %>
            </td>
            <td style="width:30px">
                <%# Eval("LSJ") %>
            </td>
            <td style="width:30px">
                <%# Eval("LBJ") %>
            </td>
            <td style="width:30px">
                <%# Eval("LKG") %>
            </td>
            <td style="width:30px">
                <%# Eval("other") %>
            </td>
            <td style="width:55px">
                0
            </td>
            <td>
                0
            </td>
        </tr>
    </table>
            </ItemTemplate>
        </asp:DataList><center>考勤符号：<%=WX.AT.Signin.statearray[0]+WX.AT.Signin.statesign[0] %>&nbsp;&nbsp;&nbsp;&nbsp;
    <%for(int i=2;i<WX.AT.Signin.statearray.Length;i++){ %>
   <%=WX.AT.Signin.statearray[i]+WX.AT.Signin.statesign[i] %>&nbsp;&nbsp;&nbsp;&nbsp;
    <%} %>
   </center>
</asp:Content>
