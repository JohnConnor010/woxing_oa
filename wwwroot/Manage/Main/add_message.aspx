<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_message.aspx.cs" Inherits="wwwroot.Manage.add_message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/App_Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        body{ font-size:12px;}
        .aa img:hover
        {
            border: 1px #dddddd solid;
        }
        table{ background-color:#777;}
        td{ background-color:#fff;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSend').click(function () {
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            });
        });
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;" cellspacing="1">
            <tr style="display: none;">
                <td>
                    类型：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="1" Text="短消息"></asp:ListItem>
                        <asp:ListItem Value="2" Text="审核信息"></asp:ListItem>
                        <asp:ListItem Value="3" Text="催办信息"></asp:ListItem>
                        <asp:ListItem Value="4" Text="提醒信息"></asp:ListItem>
                        <asp:ListItem Value="5" Text="公告提醒"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 70px; text-align: right;">
                    发送给：
                </td>
                <td>
                    <asp:Label runat="server" ID="lbSendTo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    消息内容：
                </td>
                <td>
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="5" Style="width: 98%;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    附件：
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" /><br />文件类型：zip,rar,doc,xdoc,lsx,xlsx,ppt,jpg,gif,png
                </td>
            </tr>
            <tr style="display: none;">
                <td>
                    详细页面：
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div style="width: 100%; text-align: center; padding-top:5px;">
            <asp:Button ID="btnSend" runat="server" Text="发送" OnClick="BtnSend_Click" />
        </div>
    </div>
    </form>
</body>
</html>
