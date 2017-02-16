<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterE.aspx.cs" Inherits="WX.Web.RegisterE" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>我行OA 新员工注册 </title>
    <link rel="stylesheet" type="text/css" href="/Manage/images/skin.css" />
    <link rel="stylesheet" type="text/css" href="/Manage/images/style_login.css"/>
    <script type="text/javascript" src="/App_js/validator.js"></script>
    <script type="text/javascript">
        function getPX() {
            document.getElementById("px").value = window.screen.width + "?" + window.screen.height;
        }
        function AddFavorite(sURL, sTitle) {
            try {
                window.external.addFavorite(sURL, sTitle);
            }
            catch (e) {
                try {
                    window.sidebar.addPanel(sTitle, sURL, "");
                }
                catch (e) {
                    alert("加入收藏失败，请使用Ctrl+D进行手工设置");
                }
            }
        }
        function SetHome(obj, url) {
            try {
                obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(url);
            }
            catch (e) {
                if (window.netscape) {
                    try {
                        netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                    }
                    catch (e) {
                        alert("您的浏览器不支持设为首页，请手工设置");
                    }
                    var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
                    prefs.setCharPref('browser.startup.homepage', url);
                }
            }
        }
        function Pre_Validate(f, l) {
            var p1 = document.getElementById("PassWord").value;
            var p2 = document.getElementById("PassWord1").value;
            if (p1 != p2) {
                alert("你输入的两次密码不一样!");
                return false;
            }
            else {
                return Validator.Validate(f, l)
            }
        }
    
    </script>
</head>
<body onload='getPX()'>
    <div id="dxbbs_div">
    </div>
    <form id="form1" runat="server">
    <input type="hidden" id="px" name="px" value="" />
    <div>
        <table border="0" cellspacing="0" cellpadding="0" style="width:100%;height:100%;">
            <tbody>
                <tr>
                    <td style="background:url(images/index_3.gif); vertical-align:top; height:69;">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" rowspan="2" width="289">
                                    </td>
                                    <td class="cc03" height="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="67" valign="bottom">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="cc01" style=" vertical-align:top; text-align:center; ">
                        <table border="0" cellspacing="0" cellpadding="0" style="margin:0 auto;width:700; text-align:center;height:100%;">
                            <tbody>
                                <tr>
                                    <td height="95">
                                        &nbsp;
                                    </td>
                                    <td class="cc05" rowspan="2" width="2">
                                    </td>
                                    <td height="95">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: url(/img/logo.jpg) no-repeat right top" height="285" valign="top"
                                        width="319">
                                    </td>
                                    <td valign="top" style='border-left: dotted thin #cccccc;'>
                                        <table class="cc06" border="0" cellspacing="0" cellpadding="0">
                                            <caption style="text-align: left; color: Red; padding-bottom: 10px;">
                                                【新员工注册】<br/>注册成功后，进入个人资料中完善自己档案，<br/>完善后交相关负责人审核后，方可正式使用！</caption>
                                            <tbody>
                                                <tr>
                                                    <td height="30" width="53">
                                                        姓 名：
                                                    </td>
                                                    <td colspan="2">
                                                        <input name="UserName" value='' style="color: #ff0000; font-weight: bold; padding-left: 5px; width: 125px; height: 25px; vertical-align: middle;" type="text" id="UserName" dataType="Chinese" require="true" msg="必须输入汉字，真实姓名" maxlength="16" runat="server" />
                                                        <img src="/Manage/images/ico_user.gif" alt="" width="19" height="18" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        密 码：
                                                    </td>
                                                    <td colspan="2">
                                                        <input type="password" value='' style="color: #ff0000; font-weight: bold; padding-left: 5px;
                                                            width: 125px; height: 25px; vertical-align: middle;" name="PassWord" id="PassWord"
                                                            datatype="Require" msg="密码不能为空" maxlength="16" runat="server" />
                                                        <img src="/Manage/images/luck.gif" alt="" width="19" height="18" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        再次密码：
                                                    </td>
                                                    <td colspan="2">
                                                        <input type="password" value='' style="color: #ff0000; font-weight: bold; padding-left: 5px;
                                                            width: 125px; height: 25px; vertical-align: middle;" name="PassWord1" id="PassWord1"
                                                            datatype="Require" msg="密码不能为空" maxlength="16" runat="server" />
                                                        <img src="/Manage/images/luck.gif" alt="" width="19" height="18" />
                                                    </td>
                                                </tr>
                                                <div id="ValidCodeState">
                                                    <tr>
                                                        <td height="30">
                                                            验证码：
                                                        </td>
                                                        <td>
                                                            <input type="text" style="color: #ff0000; font-weight: bold; padding-left: 5px; width: 125px;
                                                                height: 25px; vertical-align: middle;" title="看不清楚?点击图片切换" id="GetCode" maxlength="5"
                                                                name="GetCode" datatype="Require" msg="验证码不能为空" runat="server" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                            <img id="vcodeImg" src="about:blank" onerror="this.onerror=null;this.src='/App_Services/GetCode.ashx?s='+Math.random();"
                                                                alt="验证码" title="看不清楚?点击换一张" style="margin-left: 8px; cursor: pointer; width: 75px;
                                                                height: 25px; border-width: 0px; border: solid 1px #999999; vertical-align: middle;"
                                                                onclick="src='/App_Services/GetCode.ashx?s='+Math.random();" />
                                                        </td>
                                                    </tr>
                                                </div>
                                                <tr>
                                                    <td height="60">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <input type="submit" runat="server" onserverclick="RegisterUser" name="Button1" value="注册" onclick="return Pre_Validate(this.form,1);"
                                                            id="Button1" class="button" />&nbsp;&nbsp;<input name="Regist" id="Regist" value="1" type="hidden" />
                                                        <input type="button" runat="server" name="Button1" value="返回登录窗口" onclick="location.href='Login.aspx'"
                                                            id="Submit1" class="button" />
                                                    </td>
                                                    <td>
                                                        <div style="display:none;">
                                                        <!--由于本系统采用即时登录制，故不采集记住密码的方式！-->
                                                        &nbsp;
                                                        <input id="chkRemember" type="checkbox" name="chkRemember" style="margin-left: 8px;
                                                            vertical-align: middle;" />
                                                        <label for="chkRemember" style="vertical-align: middle;">
                                                            记住本次登录</label></div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" colspan="3" align="middle">
                                        <table class="cc11" border="0" cellspacing="0" cellpadding="0" width="70%">
                                            <tbody>
                                                <tr>
                                                    <td height="20" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="8%" align="right">
                                                        <img src="/Manage/images/ico_settings.gif" width="18" height="18" />
                                                    </td>
                                                    <td class="cc09" width="92%">
                                                        &nbsp; &nbsp; <a href="/App_Doc/我行OA自动化管理系统用户手册.doc" target="_blank" class="left_txt3"><span style="font-weight: bold;
                                                            color: #ff0000">用户使用手册(点击下载)</span></a> - (推荐1280*800以上分辨率)
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="1" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="24" valign="top" background="images/index_15.gif">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td class="cc02" height="2" valign="top">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="aa13" height="24" align="middle">
                                        Copyright © &nbsp;<span style="font-weight: bold;">&nbsp;<a class="aaa" href='http://www.job18.net'
                                            target="_blank">某单位</a> &nbsp; </span>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; <strong><a class="aaa" href='javascript:;' onclick="AddFavorite('http://localhost:10001','协同管理系统_某单位')">
                                            >>加入收藏</a> &nbsp; <a class="aaa" href='javascript:;' onclick="SetHome(this,'http://localhost:10001')">
                                                >>设为首页</a></strong>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
