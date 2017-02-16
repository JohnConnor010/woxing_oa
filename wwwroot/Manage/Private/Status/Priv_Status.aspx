﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Priv_Status.aspx.cs" Inherits="wwwroot.Manage.Private.Status.Priv_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        *
        {
            margin: 0 auto;
            padding: 0;
            font-size: 14px;
        }
        .wrapper
        {
            width: 866px;
            height: 525px;
            background: url(000.jpg) no-repeat center 138px;
            position: relative;
        }
        .wrapper_name
        {
            font-size: 18px;
            font-weight: 600;
            margin: 22px 0 0 390px;
            height: 40px;
            width: 100px;
            border: solid 0px #000;
            text-align: center;
            line-height: 40px;
        }
        .clear
        {
            clear: both;
            overflow: hidden;
            margin: 0;
            padding: 0;
            height: 0;
        }
        .sharp
        {
            margin: 105px 0 0 52px;
            display: inline;
        }
        .content p
        {
            line-height: 19px;
        }
        .content p span
        {
            cursor: pointer;
            color: #999;
        }
        .content p span a:hover
        {
            color: #f00;
        }
        .sharp1
        {
            margin: 5px 0 0 80px;
        }
        .sharp2
        {
            margin: 95px 0 0 88px;
        }
        .sharp3
        {
            margin: 18px 0 0 45px;
            display: inline;
        }
        .sharp4
        {
            margin: 92px 0 0 100px;
        }
        .sharp5
        {
            margin: 10px 0 0 65px;
        }
        .sharp, .sharp1, .sharp2, .sharp3, .sharp4, .sharp5
        {
            width: 200px;
            height: 136px;
            float: left;
            background: url(xbg.gif) no-repeat 0 16px;
        }
        h3
        {
            height: 20px;
            line-height: 20px;
            font-size: 14px;
            margin: 14px 0 0 28px;
            font-style: italic;
            font-weight: normal;
            color: #aaa;
        }
        .content
        {
            overflow: hidden;
            height: 65px;
            padding: 10px 15px;
        }
        .wrapper_name a{ font-style:normal; color:yellow; text-decoration:none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="wrapper">
        <div class="sharp">
            <h3>项目状态</h3>
            <div class="content">  
                <p>公司OA项目（59/62）<br /><span><a>[查看][日志]</a></span></p>
            </div>
            </div>
        <div class="sharp1">
            <h3>人员状态</h3>
            <div class="content">  
                <p><asp:Literal runat="server" ID="lblDutyState"></asp:Literal></p>
            </div>
            </div>
        <div class="sharp2">
            <h3>考勤状态</h3>
            <div class="content">  
                <p>请假（59/62）<br /><span><a>[查看][日志]</a></span></p>
            </div>
            </div>
        <div class="clear"></div>
        <div class="wrapper_name">
        <a href="/Manage/Private/Priv_UserInfo.aspx">
        <asp:Image runat="server" BorderStyle="None" ID="imgFace" />
        <asp:Label runat="server" ID="lblName"></asp:Label></a></div>
        <div class="sharp3">
            <h3>支出状态</h3>
            <div class="content">  
                <p>今年消费：（11111元）<br />本月消费：（1111元）<br /><span><a>[查看][日志]</a></span></p>
            </div>
            </div>
        <div class="sharp4">
            <h3>收入状态</h3>
            <div class="content">  
                <p>今年收入：（22222元）<br />本月收入：（2222元）<br /><span><a>[查看][日志]</a></span></p>
            </div>
            </div>
        <div class="sharp5">
            <h3>个人装备</h3>
            <div class="content">  
                <p>低值易耗（200元/年）<br />固定资产（2222元）<br />虚拟物品（2222元）<br /><span><a>[查看][日志]</a></span></p>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
