<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeBehind="Form_Preview.aspx.cs" EnableViewStateMac="false" Inherits="wwwroot.Manage.Flow.Form_Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
   <link rel="stylesheet" type="text/css" href="/App_EasyUI/themes/default/easyui.css" />
<link rel="stylesheet" type="text/css" href="/App_EasyUI/themes/icon.css" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet" media="all" />

<script type="text/javascript" src="/App_Scripts/popup.js"></script>
<script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
<script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
 <script type="text/javascript" src="/JS/utility.js"></script>
 <script language="javascript" type="text/javascript" src="/JS/jquery/jquery.min.js.gz"></script>
    <script type="text/javascript" src="/JS/jquery/jquery-ui.custom.min.js.gz"></script>
    <script type="text/javascript" src="/JS/jquery/jquery.ui.autocomplete.min.js.gz"></script>
    <script language="javascript" type="text/javascript" src="/JS/jquery/tooltip/jquery.tooltip.min.js"></script>
    
    <script src="/JS/form.js" type="text/javascript"></script>
    <script src="/JS/ccorrect_btn.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        jQuery.noConflict();
        jQuery(document).ready(function () {
            jQuery("[name^='DATA_']").tooltip();
        });

        //用户自定义js脚本
    </script>
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
    <script type="text/javascript" src="../App_Scripts/Loadwebsign.js"></script>
</head>
<body>
    <form enctype="multipart/form-data" name="form1" id="form1">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <div style="display: none;">
        <br />
        <input id="Submit1" type="submit" value="提交" name="sub_add" />
    </div>
    </form>
    <script language="javascript" type="text/javascript">

        // $("#dd").datetimepicker({ showSecond: true, timeFormat: 'hh:mm:ss' }); // 同时显示时间
        //  $("#fTime").datepicker(); //只显示日期

        jQuery(document).ready(function () {
            jQuery("img.DATE").bind("click", function () {
                var date_format = jQuery(this).attr("date_format");
                if (!date_format)
                    date_format = "yyyy-MM-dd";
                var des_obj = jQuery(this).attr("des");
                WdatePicker({ dateFmt: date_format, el: jQuery('input[name="' + des_obj + '"]').get(0) });
            });
            //处理图片上传控件
            jQuery(":file[name^=DATA_]").change(function () {
                var oldImg = jQuery(this).prev();
                oldImg.attr("src", jQuery(this).val());
            });
            //缓存下拉菜单为数组并初始化下拉菜单
            initSelect();
        });
    </script>
</body>
</html>
