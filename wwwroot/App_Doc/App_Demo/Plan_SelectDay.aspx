<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_SelectDay.aspx.cs" Inherits="wwwroot.App_Demo.Plan_SelectDay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script src="../App_EasyUI/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.calendar.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="m1" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m2" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m3" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m4" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m5" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m6" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m7" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m8" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m9" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m10" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m11" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    <div id="m12" class="easyui-calendar" style="width:180px;height:180px;float:left;"></div>  
    </div>
    <script type="text/javascript">
        for (var i = 1; i <= 12; i++) {
            $('#m' + i).calendar('moveTo', new Date(2012, i - 1, 1));
        }
    </script>
    <asp:Calendar ID="Calendar1" runat="server" Height="146px" SelectionMode="None" 
        Width="309px"></asp:Calendar>
    </form>
</body>
</html>
