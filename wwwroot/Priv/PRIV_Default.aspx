<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRIV_Default.aspx.cs" Inherits="wwwroot.Priv.PRIV_Default" %>

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
    <div style="padding: 3px; background-color:#000066; color:White; font-size:28px; font-family: 微软雅黑;">每日默认日志---当前用户:<%=this.CurUserName %></div>
    <br/>
    <div>
    <div style="width: 1300px">
        <div style="font-size: 12px; padding: 3px 3px 3px 3px; margin:3px 3px 3px 3px; width: 290px; height:450px; float: left; border: 1px dashed #aaa;">
            <div>
                <asp:HyperLink runat="server" ID="lblCol0"></asp:HyperLink></div>
            <asp:Repeater runat="server" ID="rptSummary0">
                <ItemTemplate>
                    <div><asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Name")%>'></asp:Label></div>
                    <div style="padding: 3px 3px 3px 3px;">
                        <asp:TextBox runat="server" Width="100%" TextMode="MultiLine" Height="60" Text='<%#Eval("SummaryText") %>'>
                        </asp:TextBox>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="font-size: 12px; padding: 3px 3px 3px 3px; margin:3px 3px 3px 3px; width: 290px;height:450px; float: left; border: 1px dashed #aaa;">
            <div>
                <asp:HyperLink runat="server" ID="lblCol1"></asp:HyperLink></div>
            <asp:Repeater runat="server" ID="rptSummary1">
                <ItemTemplate>
                    <div><asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Name")%>'></asp:Label></div>
                    <div style="padding: 3px 3px 3px 3px;">
                        <asp:TextBox ID="TextBox1" runat="server" Width="100%" TextMode="MultiLine" Height="60" Text='<%#Eval("SummaryText") %>'>
                        </asp:TextBox>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="font-size: 12px; padding: 3px 3px 3px 3px; margin:3px 3px 3px 3px; width: 290px;height:450px; float: left; border: 1px dashed #aaa;">
            <div>
                <asp:HyperLink runat="server" ID="lblCol2"></asp:HyperLink></div>
            <asp:Repeater runat="server" ID="rptSummary2">
                <ItemTemplate>
                    <div><asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Name")%>'></asp:Label></div>
                    <div style="padding: 3px 3px 3px 3px;">
                        <asp:TextBox ID="TextBox2" runat="server" Width="100%" TextMode="MultiLine" Height="60" Text='<%#Eval("SummaryText") %>'>
                        </asp:TextBox>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div style="font-size: 12px; padding: 3px 3px 3px 3px; margin:3px 3px 3px 3px; width: 290px;height:450px; float: left; border: 1px dashed #aaa;">
            <div>
                <asp:HyperLink runat="server" ID="lblCol3"></asp:HyperLink></div>
            <asp:Repeater runat="server" ID="rptSummary3">
                <ItemTemplate>
                    <div><asp:Label runat="server" ID="lblTitle" Text='<%#Eval("Name")%>'></asp:Label></div>
                    <div style="padding: 3px 3px 3px 3px;">
                        <asp:TextBox ID="TextBox3" runat="server" Width="100%" TextMode="MultiLine" Height="60" Text='<%#Eval("SummaryText") %>'>
                        </asp:TextBox>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div style="clear:both;"></div>
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
