<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RSSroot.Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>《干部读网》在线订阅 </title>
    <link rel="stylesheet" type="text/css" href="/Style/images/skin.css" />
    <link rel="stylesheet" type="text/css" href="/Style/images/style_login.css" />
    <script type="text/javascript" src="Style/CityScript.js"></script>
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
    </script>
</head>
<body onload='getPX()'>
    <div id="dxbbs_div">
    </div>
    <form id="form1" runat="server">
    <input type="hidden" id="px" name="px" value="" />
    <div>
        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%; height: 100%;">
            <tbody>
                <tr>
                    <td style="background: url(images/index_3.gif); vertical-align: top; height: 50;">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tbody>
                                <tr>
                                    <td valign="top" rowspan="2" width="289">
                                    </td>
                                    <td class="cc03" height="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td height="50" valign="bottom">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="cc01" style="vertical-align: top; text-align: center;">
                        <table border="0" cellspacing="0" cellpadding="0" style="margin: 0 auto; width: 1000;
                            text-align: center; height: 100%;">
                            <tbody>
                                <tr>
                                    <td height="20">
                                        &nbsp;
                                    </td>
                                    <td class="cc05" rowspan="2" width="2">
                                    </td>
                                    <td height="20">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="background: url(/img/logo.jpg) no-repeat right top" valign="top" width="319">
                                    </td>
                                    <td valign="top" style='border-left: dotted thin #cccccc;'>
                                        <table class="cc06" border="0" cellspacing="0" cellpadding="0" width="95%">
                                            <caption style="text-align: left; color: Red; padding-bottom: 10px;">
                                                <b><font size="4">《干部读网》在线订阅</font>——客服电话0531-82068731</b></caption>
                                            <tbody>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>地区：
                                                    </td>
                                                    <td colspan="2">
                                                        <select id="s_province" name="s_province">
                                                        </select>&nbsp;&nbsp;
                                                        <select id="s_city" name="s_city">
                                                        </select>&nbsp;&nbsp;
                                                        <select id="s_county" name="s_county">
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30" width="80">
                                                        <span style="color: #ff0000">*</span>公司名称：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>公司简称：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>订阅数量：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>联系人：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>联系电话：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>邮寄地址：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>邮编：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        支付金额：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                        <span style="color: #ff0000">*</span>支付方式：
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="RadioButtonList1" RepeatColumns="3" runat="server">
                                                            <asp:ListItem Value="支付宝" Text="支付宝（在线支付）"></asp:ListItem>
                                                            <asp:ListItem Value="银行打款" Text="银行打款"></asp:ListItem>
                                                            <asp:ListItem Value="现金支付"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30">
                                                    </td>
                                                    <td colspan="2">
                                                        <input type="submit" runat="server" name="Button1" value="订阅" onclick="alert(document.getElementById('s_province').value);"
                                                            id="Button1" class="button" />&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" height="50" colspan="3" align="middle">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <script type="text/javascript">
        _init_area();
    </script>
    </form>
</body>
</html>
