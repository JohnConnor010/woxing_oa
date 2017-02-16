<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showmessage.aspx.cs" Inherits="wwwroot.Manage.Main.showmessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>查看邮件 </title>
    <link type="text/css" href="/Manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/Style/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <script type="text/javascript" src="/Manage/include/common.js"></script>
    <script type="text/javascript" src="/App_Js/jquery.js"></script>
    <script type="text/javascript" src="/App_Js/jquery.easyui.min.js"></script>
    <script type="text/javascript" src='/App_Js/outlook2.js'> </script>
    <script type="text/javascript" src="/App_Js/zDialog/zDialog.js"></script>
    <style type="text/css">
     .xxsjborder{ border-top-width:0px; border-left-width:0px; border-right-width:0px;
                                                        border-bottom-width:0px; }
    </style>
    <script type="text/javascript">

        window.onload = function () {
var obj = document.getElementById("htmlbody");
obj.scrollTop= obj.scrollHeight;
}
    </script>

</head>
<body>
    <form name="form1" method="post" action="" id="form1" runat="server">
    <div id="interface_inside">
        <div id="interface_quick">
            <div class="interface_quick_left">
                您现在的操作 >> 查阅消息内容</div>
            <div class="interface_quick_right">
                <a href="javascript:void(0)" onclick="javascript:window.location.replace(window.location.href);">
                    <img alt="" style="vertical-align: middle;" src="/manage/images/reload.png" /><strong>刷新</strong></a>
                &nbsp; &nbsp; <a href="javascript:history.back();">
                    <img alt="" style="vertical-align: middle;" src="/manage/images/ico_up1.gif" /><strong>后退</strong></a>
            </div>
            <div class="clearboth">
            </div>
        </div>
        <div id="interface_main">
            <div id="tabs_config" class="tabsbox">
                <div class="clearboth">
                </div>
                <!-- 模块 -->
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <div id="config_basic1" class="tabs_wrapper">
                                <div class="tabs_main" align="left">
                                    <div id="PanelConfig">
                                        <table class="table subsubmenu">
                                            <thead>
                                                <tr>
                                                    <td>
                                                        <a href='messagelist.aspx'>消息列表</a> >>  <span id="Subject" style="color: #ff0000; font-size: 12pt; font-weight: bolder;">
                                                            <asp:Label ID="lab_typename" runat="server" Text=""></asp:Label></span>
                                                    </td>
                                                    <td style="text-align: right">
                                                    </td>
                                                </tr>
                                            </thead>
                                        </table>
                                        <br />
                                        <table width="100%">
                                            <thead>
                                                <tr>
                                                    <td style="width: 10px;">
                                                    </td>
                                                    <td>
                                                       
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tr>
                                                <th style="width: 10px;">
                                                </th>
                                                <td style="color: #333333; ">
                                                <div style="height:400px; width:800px; border:1px #aaa solid; overflow-x:hidden; overflow-y:auto;" id="htmlbody">
                                                    <asp:DataList ID="DataList1" runat="server" Width="100%">
                                                        <ItemTemplate>
                                                        <div style='float:<%# Eval("txtalign")%>;'>
                                                            <div style=" padding-right:20px; padding-top:10px;"><div style="font-weight: bold; color: #006600;float:<%# Eval("txtalign")%>;"><%# Eval("Role").ToString() == "0" ? "系统" : WX.CommonUtils.GetRealNameListByUserIdList(Eval("FromUserId").ToString())%></div><div style="float:<%# Eval("txtalign")%>;">&nbsp;&nbsp;&nbsp;&nbsp;</div><div style="float:<%# Eval("txtalign")%>;"><%# Eval("SendTime")%></div>
                                                                </div>
                                                            <div id="bodys" style=" clear:both;min-height: 15px; width:400px; _height: 15px; margin: 10px 10px 1px 1px;
                                                                border: 1px solid #E3E197; padding: 10px 10px 10px 10px; background-color: #<%# Eval("txtalign").ToString()=="right"?"fff":"FFFFDD"%>;float:<%# Eval("txtalign")%>;">
                                                                <p style="text-indent: 2em;">
                                                                    <%# Eval("Title")%><%# Eval("Annex").ToString() != "" ? "<br><a href=" + Eval("Annex") + ">查看附件</a>" : ""%>
                                                            </div>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="xxsjborder" />   
                                                    </asp:DataList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 10px;">
                                                </th>
                                                <td>
                                                <div id="senddiv" runat="server" visible="false">
                                                    <div style="float:left;"><asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Rows="2" Style="width: 700px;"></asp:TextBox></div><div style="float:left;padding:10px;">
                                                    <asp:Button
                                                        ID="Button1" runat="server" Text=" 发 送 " onclick="Button1_Click" /></div>
                                                </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                <!-- 模块 -->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
