<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Config.aspx.cs" Inherits="Install.Install.Config" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table style="margin:0 auto;">
        <tr>
            <td>
                <h1>
                    第二步&nbsp;&nbsp;数据库配置</h1>
            </td>
        </tr>
        <tr>
            <td>
                服务器：<asp:TextBox ID="TextBox1" runat="server">127.0.0.1</asp:TextBox>数据库名：<asp:TextBox
                    ID="TextBox2" runat="server"></asp:TextBox>UserID：<asp:TextBox ID="TextBox3" runat="server">sa</asp:TextBox>PassWord：<asp:TextBox
                        ID="TextBox4" runat="server"></asp:TextBox>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
