<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_SelectDay.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_SelectDay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.calendar.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="cc" class="easyui-calendar" style="width:180px;height:180px;"></div>  
    </div>
    </form>
</body>
</html>
