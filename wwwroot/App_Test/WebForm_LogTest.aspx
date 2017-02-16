<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_LogTest.aspx.cs" Inherits="wwwroot.App_Test.WebForm_LogTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 102px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style2">
                    LogType:</td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="0">Default</asp:ListItem>
                        <asp:ListItem Value="1">Account</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    UserId:</td>
                <td>
                    <asp:TextBox ID="txtUserId" runat="server" Width="397px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Title:</td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="410px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
