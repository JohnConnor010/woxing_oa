<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LazyOA.Manage.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> <%=UserTitle%> </title>
    <link href="css/style_d.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="/App_Js/themes/default/easyui.css" />
    <script src="../App_EasyUI/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/App_Js/jquery.easyui.min.js"></script>
    <script type="text/javascript" src='/App_Js/outlook2.js'> </script>
    <script type="text/javascript" src="/App_Js/zDialog/zDialog.js"></script>
    <style type="text/css">
    .spanmes a{color:White;}
    </style>
    <script type="text/javascript">
     var message = {  
            time: 0,  
            title: document.title,  
            timer: null,  
            // 显示新消息提示  
            show: function () {  
                var title = message.title.replace("【　　　】", "").replace("【新消息】", "");  
                // 定时器，设置消息切换频率闪烁效果就此产生  
                message.timer = setTimeout(function () {  
                    message.time++;  
                    message.show();  
                    if (message.time % 2 == 0) {  
                        document.title = "【新消息】" + title  
                    }  
  
                    else {  
                        document.title = "【　　　】" + title  
                    };  
                }, 600);  
                return [message.timer, message.title];  
            },  
            // 取消新消息提示  
            clear: function () {  
                clearTimeout(message.timer);  
                document.title = message.title;  
            }  
        };  
        var ShowMessageNum = 0;
        $("document").ready(function () {
            //初始化菜单
            InitLeftMenu();
            //初始化Tab
            $('#tabs').tabs('add', {
                title: '我的工作台',
                iconCls: 'icon icon-home',
                content: createFrame('DeskTop.aspx')
            });
            tabClose();
            tabCloseEven();
            //初始化在线状态
            setInterval("$('#timebox').html(new Date().toLocaleString())", 500);
            showOnline();
            setInterval(showOnline, 10000);
            //var t = $("#notice_time").val();
            //初始化提醒状态
            showMessage();
            setInterval(showMessage, 10000);
        });
        var _menus = {
            "menus":<% =MenuList %>
        };
        function hwsx() {
            var diag = new Dialog();
            diag.Width = 520;
            diag.Height = 400;
            diag.Title = "手写板 - 汉王手写输入";
            diag.URL = "/App_Js/sx.htm";
            diag.show();
        }

        function showOnline() {
            $.ajax({
                type: "GET",
                url: "/App_Services/online.ashx",
                dataType: 'html',
                data: "s=" + Math.random(),
                success: function (data) {
                    if (data == "LOGIN_OUT")//不在线
                    {
                        location.href = "/Login.aspx";
                    }
                    else if(data=="CN_ERROR")//不在线
                    {
                        $("#imgUserState").attr("src","images/user_notOnline.png");
                        $("#span_offline_state").css("display","inline");
                        $("#span_online_state").css("display","none");
                    }
                    else if (data.length>20) {
                        $("#imgUserState").attr("src","images/user_logo.png");
                        $("#span_offline_state").css("display","none");
                        $("#span_online_state").css("display","inline");
                        $('#online').html(data);
                    }
                }
            });
        }
        
        function showMessage() {
            var p = $("#stay_time").val();
            $.ajax({
                type: "GET",
                url: "/App_Services/message.ashx",
                dataType: 'html',
                data: 's=' + Math.random(),
                success: function (data) {
                    var msg="";
                    switch(data)
                    {
                        case "CN_ERROR":msg="你已经处于离线状态，请联系管理员检查网络情况!";break;
                        case "NONE":msg="";break;
                        case "LOGIN_OUT":msg="你已经登出，页面错误！";break;
                        case "TYPE_ERROR":msg="输入参数出错！";break;
                        default:msg="MSG";break;//"你已经处于离线状态，请联系管理员检查网络情况，并排除故障!";break;
                    }
                    if(msg=="")
                    {//没有收到消息
                        ;
                    }
                    document.getElementById('newmes').innerHTML="";
                     message.clear();  
                    if(msg=="MSG")
                    {//收到消息
                   document.getElementById('newmes').innerHTML="【"+data.substr(8,data.indexOf("</a>")-4)+"】";
                    message.show();  
        // 页面加载时绑定点击事件，单击取消闪烁提示  
        //function bind() {  
          //  document.onclick = function () {  
        //        message.clear();  
         //   };  
      //  }  
                         $.messager.show({
                            title: '小秘书 信息提示',
                            msg: data,
                            timeout: p,
                            width: 245,
                            height: 130,
                            showType: 'slide'
                         });
                         if (ShowMessageNum % 6 == 0) {
                             document.all.music.src = "images/msg.wav";
                         }
                         ShowMessageNum++;                    
                     }
                     else if(msg!="")
                     {//收到错误信息
                         $.messager.show({
                            title: '小秘书 信息提示',
                            msg: msg,
                            timeout: p,
                            width: 245,
                            height: 130,
                            showType: 'slide'
                         });
                         if (ShowMessageNum % 6 == 0) {
                             document.all.music.src = "images/msg.wav";
                         }
                         ShowMessageNum++; 
                     }
                }
            });
        }




       
       
    </script>
</head>
<body class="easyui-layout" style="overflow-y:hidden" fit="true" scroll="no" runat="server">
    <bgsound src="#" id="music" loop="1" autostart="true" />
    <input name="uid" type="hidden" id="uid" value="31" />
    <input name="stay_time" type="hidden" id="stay_time" value="5000" />
    <input name="notice_time" type="hidden" id="notice_time" value="10000" />
    <div id="dxbbs_div">
    </div>
    <input type="hidden" id="sxtmp" />
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px;
            width: 100%; background: white; text-align: center;">
            <img src="images/noscript.gif" alt='抱歉，请开启脚本支持！' />
        </div>
    </noscript>
    <div id="loading-mask" style="position: absolute; top: 0px; left: 0px; width: 100%;
        height: 100%; background: #D2E0F2; z-index: 20000">
        <div id="pageloading" style="position: absolute; top: 50%; left: 50%; margin: -120px 0px 0px -120px;
            text-align: center; border: 2px solid #8DB2E3; width: 200px; height: 40px; font-size: 14px;
            padding: 10px; font-weight: bold; background: #fff; color: #15428B;">
            <img src="images/loading.gif" align="middle" />
            网络加载中,请稍候...
        </div>
    </div>
    <div region="north" split="true" border="false" style="vertical-align: middle; overflow: hidden;
        height: 30px; background: url(images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;
        line-height: 20px; color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span id="interface_bt1"><a href='?logout=1'>
            <img alt="" src="images/logout.gif" onclick="return confirm('确认：您确认要安全注销，退出登录吗？')" border="0"
                title="安全注销，退出登陆" align="middle" /></a></span> <span id="interface_bt2"><a onclick="addTab('我的资料','/Manage/Private/Priv_ModiPwd.aspx','icon-user2')"
                    href='#'>
                    <img alt="" src="images/menubox_memberico.gif" border="0" title="个人设置" align="middle" /></a></span>
        <!--span id="interface_bt8"><a onclick="addTab('我的日程','','icon-calendar')" href='#'>
            <img alt="" src="images/calendar.png" border="0" title="我的日程" align="middle" /></a></span-->
        <!--span id="interface_bt9"><a onclick="addTab('即时通讯','','icon-phone')" href='#'>
            <img alt="" src="images/phone.gif" border="0" title="即时通讯" align="middle" /></a></span-->
        <span id="interface_bt4"><a onclick="addTab('我的资料','Main/messagelist.aspx','icon-data3')"
            href='#'>
            <img alt="" src="images/mail.gif" border="0" title="我的消息" align="middle" /></a></span>
        <!--span id="interface_bt7"><a onclick="addTab('所有资讯','','icon-paste')" href='#'>
            <img alt="" src="images/news.gif" border="0" title="我的资讯" align="middle" /></a></span-->
        <span id="interface_bt5"><a onclick="addTab('我的资料','/Manage/XZ/MyTrainList.aspx','icon-home')"
            href='#'>
            <img alt="" src="/Manage/Icon/do.gif" border="0" title="我的培训考核记录" align="middle" /></a></span>
        <span id="interface_bt5"><a onclick="addTab('我的资料','DeskTop.aspx','icon-home')"
            href='#'>
            <img alt="" src="images/ico_home.gif" border="0" title="我的工作台" align="middle" /></a></span>
        <span style="padding-left: 10px; font-size: 14px; font-weight: bold;">
            <img id="imgUserState" alt="" src="images/user_logo.png" width="20" height="20" align="middle" />
            &nbsp;
            <%=UserTitle%></span>&nbsp;<span id="span_online_state" style=" font-weight:bold;color:yellow;display:inline;">(在线)</span>
            &nbsp;<span id="span_offline_state" style="color:Red;display:none; font-weight:bold;">(离线)</span>
            &nbsp;<span style="font-weight:bold;" id="newmes" class="spanmes"></span>

    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2; ">
        <div class="footer">
		<div id="timebox" style='float:right;text-align:right;width:385px;padding-right:20px;'>&nbsp;</div>
		<div id="online" style='float:left;text-align:left;width:160px;padding-left:20px;'><a onclick=addTab('在线用户','Common/User_OnLine.aspx','icon-home') href='#'>在线用户：<strong>0</strong> 人</a></div>
		</div>
    </div>
    <div region="west" split="true"  title="功能菜单" style="width:170px;" id="west">
			<div id="nav">
		<!--  导航内容 -->
			</div>
    </div>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden">
        <div id="tabs" class="easyui-tabs"  fit="true" border="false" >
        <!--<div title='隐藏层(勿删)'></div>-->
		</div>
    </div>
	<div id="mm" class="easyui-menu" style="width:150px;">
		<div id="mm-tabupdate">刷新选项卡</div>
		<div class="menu-sep"></div>
		<div id="mm-tabclose">关闭</div>
		<div id="mm-tabcloseall">全部关闭</div>
		<div id="mm-tabcloseother">除此之外全部关闭</div>
		<div class="menu-sep"></div>
		<div id="mm-tabcloseright">当前页右侧全部关闭</div>
		<div id="mm-tabcloseleft">当前页左侧全部关闭</div>
		<div class="menu-sep"></div>
		<div id="mm-exit">退出</div>
	</div>
</body>
</html>