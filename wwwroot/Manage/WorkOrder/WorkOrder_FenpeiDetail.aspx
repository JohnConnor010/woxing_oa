<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="WorkOrder_FenpeiDetail.aspx.cs"
    Inherits="wwwroot.Manage.WorkOrder.WorkOrder_FenpeiDetail" %>    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div style="width: 850px; height: 440px; overflow-y: auto; padding-left:5px;">
        
        <div style="width: 815px; border: 1px solid #aaa; padding: 10px;">
        <table>
            <tr>
                <td width="80">
                    <b>任务名称：</b>
                </td>
                <td>
                    <asp:TextBox ID="Title_txt" runat="server" Width="500"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                        ID="Button1" runat="server" Text="提交" onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>执&nbsp;行&nbsp;人：</b>
                </td>
                <td>
                    <asp:Label ID="ExecUserID_li" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>任务期限：</b><asp:TextBox ID="YJTime_txt"
                        runat="server" CssClass="easyui-datebox" Width="100"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <b>任&nbsp;务&nbsp;量：</b><asp:TextBox ID="Count_txt" runat="server" Width="60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>任务要求：</b>
                </td>
                <td>
                    <asp:TextBox ID="Remarks_txt" runat="server" TextMode="MultiLine" Rows="5" Columns="80"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>
