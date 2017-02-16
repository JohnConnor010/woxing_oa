<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notifyfilesshow.aspx.cs" Inherits="wwwroot.Manage.XZ.Notifyfilesshow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
</head>
<body>
    <div style="width:100%; height: 400px; overflow-y: auto;">
    <table class="table" style="line-height:200%;height:380px; width:96%;">
        <tr>
            <td style="background:#dddddd; font-weight:bold; text-align:center; height:25px;" width="80%">
               <asp:Literal ID="li_title" runat="server"></asp:Literal>
            </td>
            <td align="right" style="background:#dddddd; height:25px; "><a href="MyNotifyFiles.aspx">>>我的文件列表</a></td>
        </tr>
        <tr style="height:25px;">
            <td style="background:#eeeeee; text-align:right; height:20px;" colspan="2">
                发布人：<asp:Literal ID="li_user" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;发布于：<asp:Literal
                    ID="li_starttime" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="padding:10px; text-indent:24px; vertical-align:top;" colspan="2"> 
               <asp:Literal ID="li_content" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </div>
</body>
</html>
