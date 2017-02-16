<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeskTop.aspx.cs" Inherits="LazyOA.DeskTop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>我的桌面 </title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <link rel="stylesheet" type="text/css" href="css/css.css" />
    <script type="text/javascript" src="/App_Js/jquery.js"></script>
    <script type="text/javascript" src="include/common.js"></script>
    <script type="text/javascript" src="/App_Js/zDialog/zDialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class='divv'>
        <table cellspacing="0" cellpadding="0" width="100%" align="center" style="border-bottom: #bbdde5 1px solid;
            border-left: #bbdde5 1px solid; border-top: #bbdde5 1px solid; border-right: #bbdde5 1px solid;">
            <tbody>
                <tr class="info1">
                    <td style="padding-left: 8px" height="30" width="58%">
                        您好,<strong>
                            <span runat="server" id="lblUserName"></span>
                        </strong><span style="padding-left: 8px">欢迎您!</span> <span runat="server" id="spanEmployee" style="padding-left: 8px">
                            [ <span style="color: #2a7aca; font-weight: bold; cursor: hand;"><a href="/Manage/Private/Priv_UserInfo.aspx">
                                查看个人资料</a></span> ][ <span style="color: #2a7aca; font-weight: bold; cursor: hand;">
                                    <a href="/Manage/Private/Priv_ModiPwd.aspx">修改密码</a></span> ]
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    [ <span style="color: #2a7aca; font-weight: bold; cursor: hand;"><a title="查看个人状态" href="javascript:PopupIFrame('Private/Status/Priv_Status.aspx','我的状态',null,null,850,550)">
                                <asp:Literal runat="server" ID="lblPrivateState"></asp:Literal></a></span> ]
                            <!--[ <span style="color: #2a7aca; font-weight: bold; cursor: hand;"><a href="#">
                                部门导航</a></span> ]-->
                        </span>
                    </td>
                    <td style="width: 42%;">
                        <div id="TipsState">
                            <script type="text/javascript">                                /*
                                var marqueecontent = new Array();
                                marqueecontent[0] = '<span><a href=#>学会"偷懒"，"懒"出境界，提高工作效率最有效的方法！</a></span>'; marqueecontent[1] = '<span><a href=#>您的系统没有设置滚动公告，这里显示的是默认信息</a></span>'; marqueecontent[2] = '<span><a href=#>滚动公告在 系统管理=>资讯管理 中设置. 谢谢使用</a></span>';
                                var marqueeInterval = new Array(); var marqueeId = 0; var marqueeDelay = 3000; var marqueeHeight = 17;
                                function initMarquee() {
                                var str = marqueecontent[0];
                                document.write('<div id="marqueeBox" style="float:left;margin: 0px; font-weight:bold; line-height: 140%; text-align:center;overflow:hidden;width:98%;height:' + marqueeHeight + 'px" onmouseover="clearInterval(marqueeInterval[0])" onmouseout="marqueeInterval[0]=setInterval(\'startMarquee()\',marqueeDelay)"><div>' + str + '</div></div>'); marqueeId++;
                                marqueeInterval[0] = setInterval("startMarquee()", marqueeDelay);
                                }
                                function startMarquee() {
                                var str = marqueecontent[marqueeId]; marqueeId++;
                                if (marqueeId >= marqueecontent.length) marqueeId = 0;
                                if (document.getElementById("marqueeBox").childNodes.length == 1) {
                                var nextLine = document.createElement('DIV'); nextLine.innerHTML = str;
                                document.getElementById("marqueeBox").appendChild(nextLine);
                                } else {
                                document.getElementById("marqueeBox").childNodes[0].innerHTML = str;
                                document.getElementById("marqueeBox").appendChild(document.getElementById("marqueeBox").childNodes[0]);
                                document.getElementById("marqueeBox").scrollTop = 0;
                                }
                                clearInterval(marqueeInterval[1]); marqueeInterval[1] = setInterval("scrollMarquee()", 20);
                                }
                                function scrollMarquee() {
                                document.getElementById("marqueeBox").scrollTop++;
                                if (document.getElementById("marqueeBox").scrollTop % marqueeHeight == (marqueeHeight - 1)) { clearInterval(marqueeInterval[1]); }
                                } initMarquee(); */
                            </script>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <table style="margin-top: 9px;" cellspacing="0" cellpadding="0" width="100%" align="center">
            <tbody>
                <tr>
                    <td valign="top">
                        <table cellspacing="0" cellpadding="0" width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td valign="top" width="51%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000; float:left;">&nbsp;公告</span>
                                                        <span style="float:right;">&nbsp;<a href="javascript:PopupIFrame('XZ/MyNotify.aspx','我的公告','','',800,450)" style="color: #555555; font-weight:normal;">>>更多</a></span>
                                                    </td>
                                                    <td height="21" background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td height="186" style="vertical-align: top;">
                                                        <asp:GridView ID="GridView1" DataKeyNames="id" Width="100%" CssClass="table" runat="server"
                                                            AutoGenerateColumns="False">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="发布人">
                                                                    <ItemTemplate>
                                                                        <%# Eval("RealName") %></ItemTemplate>
                                                                    <ItemStyle Height="24" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="分类">
                                                                    <ItemTemplate>
                                                                        <%# Eval("CategoryName")%></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="标题">
                                                                    <ItemTemplate>
                                                                        <a title='<%# Eval("Title") %>' style='<%# Eval("Istop").ToString()=="1"?"color:Red;font-weight:bold;": "" %>'
                                                                            href="javascript:PopupIFrame('XZ/NotifyDetail.aspx?NotifyID=<%# Eval("id") %>','查看详细','','',600,400)">
                                                                            <%# (Eval("Title").ToString().Length > 20 ? Eval("Title").ToString().Substring(0, 20) : Eval("Title").ToString())%></a><%# Eval("Istop").ToString() == "1" ? "<img src='/images/arrow_up.gif' alt='置顶'/>" : ""%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="时间">
                                                                    <ItemTemplate>
                                                                        <%#  Convert.ToDateTime(Eval("Starttime")).ToString("yyyy-MM-dd")%></ItemTemplate>
                                                                    <ItemStyle width="80" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="Desktop/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" width="49%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000; float:left;">&nbsp;文件通知</span>
                                                        <span style="float:right;">&nbsp;<a href="javascript:PopupIFrame('/Manage/XZ/MyNotifyFiles.aspx','文件通知','','',800,450)" style="color: #555555; font-weight:normal;">>>更多</a></span>
                                                    </td>
                                                    <td height="21" background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td height="186px" valign="top">
                                                        <asp:GridView ID="GridView2" DataKeyNames="id" Width="100%" CssClass="table" runat="server"
                                                            AutoGenerateColumns="False">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="发布人">
                                                                    <ItemTemplate>
                                                                        <%# Eval("RealName") %></ItemTemplate>
                                                                    <ItemStyle Height="24" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="分类">
                                                                    <ItemTemplate>
                                                                        <%# WX.XZ.NotifyFiles.Areaarry[Convert.ToInt32(Eval("Area"))]%></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="标题">
                                                                    <ItemTemplate>
                                                                    <a title='<%# Eval("Title") %>' style='<%# Eval("Istop").ToString()=="1"?"color:Red;font-weight:bold;": "" %>'
                                                                            href="javascript:PopupIFrame('XZ/Notifyfilesshow.aspx?NotifyFileId=<%# Eval("id") %>','查看详细','','',600,400)">
                                                                            <%# (Eval("Title").ToString().Length > 20 ? Eval("Title").ToString().Substring(0, 20) : Eval("Title").ToString())%></a><%# Eval("Istop").ToString() == "1" ? "<img src='/images/arrow_up.gif' alt='置顶'/>" : ""%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="时间">
                                                                    <ItemTemplate>
                                                                        <%#  Convert.ToDateTime(Eval("PublishTime")).ToString("yyyy-MM-dd")%></ItemTemplate>
                                                                    <ItemStyle width="80" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="Desktop/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="8" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" width="51%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="98%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000;">&nbsp;我的工作</span>
                                                    </td>
                                                    <td height="21" background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                        <div style="height: 1px">
                                                        </div>
                                                    </td>
                                                    <td valign="top">
                                                        <table style="padding-left: 4px;" border="0" cellspacing="0" cellpadding="0" width="100%"
                                                            align="center">
                                                            <tbody>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="t2" style="display: none;">
                                                                        <span style="margin-right: 15px;"><a href="/manage/Common/Mail_List.aspx?fid=0">收件箱</a>&nbsp;&nbsp;
                                                                            <a href="/manage/Common/Mail_List.aspx?fid=1">草稿箱</a>&nbsp;&nbsp; <a href="/manage/Common/Mail_List.aspx?fid=2">
                                                                                发件箱</a>&nbsp;&nbsp; <a href="/manage/Common/Mail_Manage.aspx">发送新邮件</a>&nbsp;&nbsp;
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="Desktop/a.gif" width="1" height="1">
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                    <td valign="top" width="49%">
                                        <table class="tx" border="0" cellspacing="0" cellpadding="0" width="100%">
                                            <tbody>
                                                <tr>
                                                    <td height="21" background="Desktop/link_3.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                    <td background="Desktop/linkbg2.gif" width="100%">
                                                        <span style="font-weight: bold; color: #ff0000;">&nbsp;工作流程</span>
                                                    </td>
                                                    <td height="21" background="Desktop/link_4.gif" width="7" nowrap>
                                                        <div style="width: 7px">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td background="Desktop/link_3_1.gif">
                                                    </td>
                                                    <td valign="top">
                                                        <table style="padding-left: 4px;" border="0" cellspacing="0" cellpadding="0" width="100%"
                                                            align="center">
                                                            <tbody>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="24">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" class="t2" style="display: none;">
                                                                        <span style="margin-right: 15px;"><a href="/manage/flow/Flow_List.aspx?action=verify">
                                                                            我的批阅</a>&nbsp;&nbsp; <a href="/manage/flow/Flow_List.aspx?action=verified">已经批阅</a>&nbsp;&nbsp;
                                                                            <a href="/manage/flow/Flow_List.aspx?action=apply">我的申请</a>&nbsp;&nbsp; <a href="/manage/flow/Flow_List.aspx?action=view">
                                                                                抄送呈报</a> </span>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                    <td background="Desktop/link_4_1.gif">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="info2" height="1" colspan="3">
                                                        <img src="Desktop/a.gif" width="1" height="1">
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
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
