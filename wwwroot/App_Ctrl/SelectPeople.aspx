<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPeople.aspx.cs" Inherits="wwwroot.App_Ctrl.SelectPeople" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    <!--
body {
    font-family: Arial, Helvetica, sans-serif;
    font-size:12px;
    color:#666666;
    background:#fff;
    text-align:center;

}

* {
    margin:0;
    padding:0;
}

a {
    color:#1E7ACE;
    text-decoration:none;    
}

a:hover {
    color:#000;
    text-decoration:underline;
}
h3 {
    font-size:14px;
    font-weight:bold;
}

pre,p {
    color:#1E7ACE;
    margin:4px;
}
input, select,textarea {
    padding:1px;
    margin:2px;
    font-size:12px/18px;
}
.buttom{
    padding:1px 3px;
    font-size:12px;
    border:1px #1E7ACE solid;
    background:#D0F0FF;
}
#formwrapper {
    width:450px;
    margin:15px auto;
    padding:20px;
    text-align:left;
    border:1px #1E7ACE solid;
}

fieldset {
    padding:10px;
    margin-top:5px;
    border:1px solid #1E7ACE;
    background:#fff;
}

fieldset legend {
    color:#1E7ACE;
    font-weight:bold;
    padding:3px 20px 3px 20px;
    border:1px solid #1E7ACE;    
    background:#fff;
}

fieldset label {
    float:left;
    width:90px;
    text-align:left;
    padding:4px;
    margin:1px;
}

fieldset div {
    clear:left;
    margin-bottom:2px;
}

.enter{ text-align:center;}
.clear {
    clear:both;
}


body,table{
	font-size:12px;
}
table{
	table-layout:fixed;
	empty-cells:show; 
	border-collapse: collapse;

}z
td{
	height:20px;
}
h1,h2,h3{
	font-size:12px;
	margin:0;
	padding:0;
}

.title { background: #FFF; border: 0px solid #9DB3C5; padding: 1px; width:420px;;margin-bottom:2px; }
	.title h1 { line-height: 31px; text-align:center;  background: #2F589C url(img/th_bg2.gif); background-repeat: repeat-x; background-position: 0 0; color: #FFF; }
		.title th, .title tr { border: 1px solid #CAD9EA; padding: 5px;}


/*这个是借鉴一个论坛的样式*/
table.t1{
	border:1px solid #cad9ea;
	color:#666;
}
table.t1 th {
	background-image: url(img/th_bg1.gif);
	background-repeat::repeat-x;
	height:25px;
}
table.t1 td,table.t1 th{
	border:1px solid #cad9ea;
	padding:0 1em 0;
            width: 100%;
        }
table.t1 tr.a1{
	background-color:#f5fafe;
}



table.t2{
	border:1px solid #9db3c5;
	color:#666;
}
table.t2 th {
	background-image: url(img/th_bg2.gif);
	background-repeat::repeat-x;
	height:25px;
	color:#fff;
}
table.t2 td{
	border:1px dotted #cad9ea;
	padding:0 2px 0;
}
table.t2 th{
	border:1px solid #a7d1fd;
	padding:0 2px 0;
}
table.t2 tr.a1{
	background-color:#e8f3fd;
}

table.t3{
	border:1px solid #fc58a6;
	color:#720337;
}
table.t3 th {
	background-image: url(img/th_bg3.gif);
	background-repeat::repeat-x;
	height:25px;
	color:#35031b;
}
table.t3 td{
	border:1px dashed #feb8d9;
	padding:0 1.5em 0;
}
table.t3 th{
	border:1px solid #b9f9dc;
	padding:0 2px 0;
}
table.t3 tr.a1{
	background-color:#fbd8e8;
}

.style1
    {
        width: 100%;
    }
input.SmallButtonA {
    background: url("/Manage/images/btn_a.png") no-repeat scroll 0 0 transparent;
    border: 0 none;
    color: #36434E;
    cursor: pointer;
    height: 21px;
    width: 50px;
}
-->
</style>
<script type="text/javascript" src="../App_Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="../App_Scripts/QueryString.js"></script>
<script type="text/javascript">
    $(function () {
        $('#btnSubmit').click(function () {
            var outputField = GetQueryString("OutputField");
            var hiddenField = GetQueryString("HiddenField");
            var length = $('#lbRight option').length;
            var value = new Array();
            var text = new Array();
            if (length == 0) {
                window.parent.document.getElementById(outputField).value = "";
                window.parent.document.getElementById(hiddenField).value = "";
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            } else {
                $('#lbRight option').each(function (i, selected) {
                    value.push($(selected).val());
                    text.push($(selected).text());
                });
                window.parent.document.getElementById(outputField).value = text;
                window.parent.document.getElementById(hiddenField).value = value;
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            }
        });
        $('#btnRight').click(function () {
            var model = GetQueryString("SelectModel");
            if (model == "Single") {
                var count = $("#lbLeft option:selected").length;
                var length = $('#lbRight option').length;
                if (count > 1) {
                    alert("当前模式下只能单选，不能多选！");
                    return false;
                }
                if (length == 1) {
                    alert("您已经选择了一人，不能多选！");
                    return false;
                }
            }
        });
        $('#btnClose').click(function () {
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        });
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left: 10px; text-align: left; margin-top: 5px;">
        <div id="panLogin">
            <p>
                <strong>您当前选择了 <span id="number" style="color: #ff0000; font-weight: bold;"></span><span
                    id="total" style="color: blue; font-weight: bold;"><%=count %></span> 个人员!</strong> &nbsp;&nbsp;
                <span><a href="javascript:;" onclick="clearAll()">清除已选</a>&nbsp;&nbsp;<asp:TextBox 
                    ID="txtKeyword" runat="server" MaxLength="10"></asp:TextBox>
&nbsp;</span><asp:Button ID="btnSearch" runat="server" Text="搜索" onclick="btnSearch_Click" />
&nbsp;</p>
        </div>
        <table width="430px" id="Table1" border="1" class="t1">
            <tr>
                <td colspan="3">
                部门：<asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <div style="width: 430px; height: 300px; border: 2 inset; scrollbar-face-color: #DBEBFE;
            scrollbar-shadow-color: #B8D6FA; scrollbar-highlight-color: #FFFFFF; scrollbar-3dlight-color: #DBEBFE;
            scrollbar-darkshadow-color: #458CE4; scrollbar-track-color: #FFFFFF; scrollbar-arrow-color: #458CE4;
            margin-top: 5px;">
            
            <table width="410px" id="mytab" border="1" class="style1">
                <tr>
                    <td align="right" width="40%">
                        <asp:ListBox ID="lbLeft" runat="server" Height="265px" Width="160px" ToolTip="按Ctrl键全选"
                            SelectionMode="Multiple"></asp:ListBox>
                    </td>
                    <td width="15%" align="center">
                        <asp:Button ID="btnRight" runat="server" Text=" → " CssClass="SmallButtonA" 
                            onclick="btnRight_Click" />
                        <br />
                        <asp:Button ID="btnLeft" runat="server" Text=" ← " CssClass="SmallButtonA" 
                            onclick="btnLeft_Click" />
                    </td>
                    <td width="40%">
                        <asp:ListBox ID="lbRight" runat="server" Height="265px" Width="160px" 
                            SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <input type="button" id="btnSubmit" value="确定" class="SmallButtonA" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="SmallButtonA" onclick="btnCancel_Click" Text="清除" />&nbsp;&nbsp;          
                        <input type="button" id="btnClose" value="取消" class="SmallButtonA" />
&nbsp;</td>
                </tr>
            </table>
            
        </div>
    </div>
    </form>
</body>
</html>
