<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultFormView.aspx.cs" Inherits="wwwroot.App_Demo.DefaultFormView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="View.aspx?FlowID=1">第一个流程图</a>
        <br />
        <a href="View.aspx?FlowID=2">第二个流程图</a>
        <br />
        <a href="View.aspx?FlowID=3">第三个流程图</a>
    </div>
    </form>
</body>
</html>
