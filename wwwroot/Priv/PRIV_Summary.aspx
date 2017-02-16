<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRIV_Summary.aspx.cs" Inherits="wwwroot.Priv.PRIV_Summary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body{ font-size:12px;}
        a.date{text-decoration:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="padding: 3px; background-color:#000066; color:White; font-size:28px; font-family: 微软雅黑;">每日总结日志---当前用户:<%=this.CurUserName %></div>
    <br/>
    <div>
    <asp:DataList runat="server" ID="rptNav" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="3">
    <ItemTemplate>
        <div style="float:left;width:240px; font-size:14px; padding:3px 3px 3px 3px; overflow:hidden;">
        <asp:HyperLink ID="HyperLink1" runat="server" ForeColor='<% #Convert.ToInt32(Eval("ProgramId"))==this.rID?System.Drawing.Color.Red:System.Drawing.Color.Black %>' Text='<%#String.Format("{0}({1}条)",Eval("Name"),Eval("CC")==Convert.DBNull?"0":Eval("CC"))%>' NavigateUrl='<% #Eval("ProgramID","?ID={0}") %>'></asp:HyperLink>
        (<asp:Label runat="server" Font-Size="11px" ForeColor="#cccccc" ID="Label1" Text='<% #GetRelativeDateStr(Eval("MaxDate")) %>'></asp:Label>)
        </div>
    </ItemTemplate>
    </asp:DataList>
    <div style="clear:both; border-bottom:solid 1px #ccc; margin-bottom:3px;"></div>
    <asp:Repeater runat="server" ID="rptSummary">
    <ItemTemplate>
        <div style="font-size:12px; padding:3px 3px 3px 3px;">
           <div style="float:left;width:120px; text-align:right;"><asp:HyperLink runat="server" CssClass="date" Text='<% #String.Format("{0:yyyy年MM月dd日}：",Convert.ToDateTime(Eval("Date"))) %>' NavigateUrl='<% #Eval("Date","PRIV_SummaryLog.aspx?Date={0}") %>'></asp:HyperLink></div>
           <div style="float:left;width:600px; text-align:left;"><asp:Label runat="server" Text='<% #Eval("SummaryText") %>'></asp:Label></div>
           <div style="clear:both;"></div>
        </div>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </div>
    <div style="position:fixed; top:50px; right:20px;">
    <asp:calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC" 
            BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" 
            Font-Size="8pt" ForeColor="#003399" Height="200px" 
            ondayrender="Calendar1_DayRender" Width="220px" 
            onselectionchanged="Calendar1_SelectionChanged">
        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
        <WeekendDayStyle BackColor="#CCCCFF" />
        </asp:calendar>
    </div>
    </form>
</body>

</html>
