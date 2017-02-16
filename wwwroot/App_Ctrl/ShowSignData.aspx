<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSignData.aspx.cs" Inherits="wwwroot.App_Ctrl.ShowSignData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>显示签名数据</title>
    <script type="text/javascript">
        function LoadSignData() {
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            var storeData = document.getElementById("signData").value;
            if (storeData == "") return;
            var strData = "我行信息技术有限公司";
            DWebSignSeal.SetStoreData(storeData);
            DWebSignSeal.ShowWebSeals();
            DWebSignSeal.SetSealSignData("SignInfoseal", strData);
            DWebSignSeal.SetSealSignData("SignInfohand", strData);
        }
    </script>
</head>
<body  onload="LoadSignData();">
    <form id="form1" runat="server">
    <div>
        <script type="text/javascript" src="/App_Scripts/Loadwebsign.js"></script>
        <input type="hidden" name="signData" id="signData" runat="server" value="" />
    </div>
    </form>
</body>
</html>
