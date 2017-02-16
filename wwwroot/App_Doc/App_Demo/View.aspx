<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="wwwroot.App_Demo.View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:v="urn:schemas-microsoft-com:vml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        v\:*
        {
            behavior: url(#default#VML);
        }
    </style>
    <script language="javascript" src="Scripts/WorkFlow.js"></script>
    <link href="css/menu_top.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="buttons" style="float: right; margin-top: 2px">
            <input type="button" id="button0" value="新建步骤" class="SmallButton" onclick="window.open('AddNewGraphic.aspx?FlowID=<%=Request.QueryString["FlowID"] %>&GRAPH=1','','height=500,width=700,status=1,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes')">&nbsp;
            <input type="button" id="button1" value="保存布局" class="SmallButton" onclick="parent.set_main.SavePosition()">&nbsp;
            <input type="button" value="刷新" class="SmallButton" onclick="parent.set_main.location.reload();">&nbsp;
            <input type="button" value="打印" class="SmallButton" onclick="parent.set_main.document.execCommand('Print');"
                title="直接打印流程页面">&nbsp;
            <input type="button" value="复制" class="SmallButton" onclick="copy_main();" title="复制至剪贴版，可粘贴至Word">&nbsp;
            <input type="button" value="关闭" class="SmallButton" onclick="parent.close();">
        </div>
        <iframe src="FlowGraphic.aspx?FlowID=<%=Request.QueryString["FlowID"] %>" width="800"
            height="800" id="set_main" name="set_main"></iframe>
    </div>
    </form>
</body>
</html>
