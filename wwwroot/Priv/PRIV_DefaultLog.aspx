<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRIV_DefaultLog.aspx.cs" Inherits="wwwroot.Priv.PRIV_DefaultLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        textarea.summaryText { border:solid 1px #aaa;overflow-y:hidden}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 3px; background-color: #000066; color: White; font-size: 28px;
        font-family: 微软雅黑;">
        每日默认日志---当前用户:<%=this.CurUserName %></div>
    <div>
        <p style="border-bottom: 1px solid #777; padding: 3px 3px 3px 3px;">
            <input type="button" onclick="location.href='PRIV_Default.aspx';" value="返回列表" />
            <asp:Button runat="server" ID="btnRefresh" Text="刷新" ToolTip="刷新" 
                onclick="btnRefresh_Click" />
            <asp:Button runat="server" ID="btnPreviousDay" Text="前一天" ToolTip="前一天" 
                onclick="btnPreviousDay_Click" />
            <asp:Label runat="server" ID="lblDate" Font-Size="18px" />
            <asp:Button runat="server" ID="btnToday" Text="今天" ToolTip="今天" 
                onclick="btnToday_Click" />
            <asp:Button runat="server" ID="btnNextDay" Text="后一天" ToolTip="后一天" 
                onclick="btnNextDay_Click" />            
            </p>
        <div>
            <asp:Repeater runat="server" ID="rptSummaryLog">
                <ItemTemplate>
                    <div style="padding:3px 3px 3px 3px;">
                    <div style=" font-style:italic; font-weight:bold;"><asp:Label runat="server" Text='<% #Eval("Name") %>'></asp:Label></div>
                    <div>
                    <asp:Literal runat="server" Text='<%#GetSummaryText(Eval("ID"))%>' ></asp:Literal>
                    </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <p style="border-top:1px solid #777; clear:both; text-align:center; padding:3px 3px 3px 3px;">
            <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="提交" 
                Height="21px" /></p>
    </div>
    </form>
</body>
</html>
