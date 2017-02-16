<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="wwwroot.App_Ctrl.SelectArea.Test" %>

<%@ Register src="/App_Ctrl/SelectArea/SelectAreaCtrl.ascx" tagname="SelectAreaCtrl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:SelectAreaCtrl ID="SelectAreaCtrl1" ClientIDMode="Static" runat="server" ProvCode="370000" CityCode="370100" />
        <asp:Button runat="server" Text="Test" OnClick="TestR"  />
    </div>
    </form>
</body>
</html>
