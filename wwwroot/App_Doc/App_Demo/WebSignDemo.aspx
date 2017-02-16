<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebSignDemo.aspx.cs" Inherits="wwwroot.App_Demo.WebSignDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点聚签章，签批示例</title>
    <script type="text/javascript">
        //印章文件
        function addseal(objectname) {
            var vSealName = "SignInfoseal";
            var vSealPostion = objectname + "Position";
            var vSealSignData = objectname;
            var strData = "我行信息技术有限公司";
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            DWebSignSeal.SetCurrUser("孙战平");
            DWebSignSeal.SetSignData("-");
            DWebSignSeal.SetSignData("+DATA:" + strData);
            DWebSignSeal.SetPosition(10, 10, vSealPostion);
            DWebSignSeal.addSeal("", vSealName);
            DWebSignSeal.SetMenuItem(vSealName, 5);
        }
        //手写签批
        function handwrite(objectname) {
            var vSealName = "SignInfohand";
            var vSealPostion = objectname + "Position";
            var vSealSignData = objectname;
            var strData = "我行信息技术有限公司";
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            DWebSignSeal.SetCurrUser("孙战平");
            DWebSignSeal.SetSignData("-");
            DWebSignSeal.SetSignData("+DATA:" + strData);
            DWebSignSeal.SetPosition(10, 10, vSealPostion);
            DWebSignSeal.HandWritePop(4, 255, 0, 0, 0, vSealName);
        }
        function GetValue_OnSubmit() {
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            var sing_info_str = "";
            var strObjectName = DWebSignSeal.FindSeal("", 0);
            while (strObjectName != "") {
                if (strObjectName.indexOf(strObjectName + ",") < 0) {
                    sing_info_str += strObjectName + ";";
                }
                strObjectName = DWebSignSeal.FindSeal(strObjectName, 0);
            }
            if (sing_info_str != "") {
                var value = DWebSignSeal.GetStoreDataEx(sing_info_str);
                document.getElementById("txtSealData").value = value;
                return true;
            }
            else {
                alert("提交印章失败");
                return false;
            }
        }
    </script>
</head>
<body>
    <script type="text/javascript" src="../App_Scripts/Loadwebsign.js"></script>
    <form id="form1" runat="server">
    <div>
        <table class="style1" style="border: thin groove #808080">
            <tr>
                <td>
                    领导意见：<br />
                    <input id="Button1" type="button" value=" 盖 章 " onclick="addseal('txtContent')" />
                    <input id="Button2" type="button" value=" 手 写 " onclick="handwrite('txtContent')" /></td>
                <td id="txtContentPosition">
                    <asp:TextBox ID="txtContent" runat="server" Columns="100" Rows="20" ClientIDMode="Static"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:HiddenField ID="txtSealData" runat="server" />
                    </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" 
                        OnClientClick="return GetValue_OnSubmit();" onclick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
