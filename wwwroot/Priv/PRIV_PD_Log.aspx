<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PRIV_PD_Log.aspx.cs" Inherits="wwwroot.Priv.PRIV_PD_Log" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../App_Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function display_desc_by_cb(id) {
            var checked = $('#cb_{0}').attr('checked');
            var display = checked ? 'block' : 'none';
            $('#div_{0}').css('display', display);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="padding: 3px; background-color: #000066; color: White; font-size: 28px;
            font-family: 微软雅黑;">
            每日被动日记---当前用户:<%=this.CurUserName %></div>
        <p style="border-bottom: 1px solid #777; padding: 3px 3px 3px 3px;">
            <asp:Button runat="server" ID="btnPreviousDay" Text="前一天" ToolTip="前一天" OnClick="btnPreviousDay_Click" />
            <asp:Label runat="server" ID="lblDate" Font-Size="18px" />
            <asp:Button runat="server" ID="btnToday" Text="今天" ToolTip="今天" OnClick="btnToday_Click" />
            <asp:Button runat="server" ID="btnNextDay" Text="后一天" ToolTip="后一天" OnClick="btnNextDay_Click" />
        </p>
        <div>
            <div>
                <asp:Repeater runat="server" ID="rpt_PD">
                <ItemTemplate>
                    <div style="border-bottom: dashed 1px #555; padding: 5px 5px 5px 5px; margin: 15px 15px 15px 15px;">
                        <div style=" margin-left:20px; font-size:11px;"><asp:Literal runat="server" Text='<% #GetNotice(Eval("id"),Eval("NoticeCriterionValueScript"),Eval("NoticeCriterion"),Eval("NoticeFormat")) %>'></asp:Literal></div>
                        <div>
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("RowNo","{0}.")%>'></asp:Label>
                            <asp:Label runat="server" Text='<%#Eval("Title")%>'></asp:Label></div>
                        <div style="margin-left: 20px;">
                            <asp:Literal runat="server" Text='<%#GetItemHtml(Eval("id"),Eval("Type"),Eval("Items"),Eval("descFld")) %>'></asp:Literal>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="width: 500px; text-align: center;">
                <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClick="btnSubmit_Click" /></div>
        </div>
    </div>
    </form>
</body>
</html>
