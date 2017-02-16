<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_Slider.aspx.cs" Inherits="wwwroot.App_Test.WebForm_Slider" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="../App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../App_EasyUI/plugins/jquery.slider.js"></script>
    <script type="text/javascript" src="../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" padding-left:20px; padding-right:20px;font-size:12px;">
    <div class="easyui-slider" data-options="min:0,max:50,step:10,value:10,width:200" disabled="true" 
      rule="['创建','审核中','执行中','总结','评价','○']" style="width:300px; height:100px;padding-right:20px;" mode="h"></div>  
    </div>
    </form>
</body>
</html>
