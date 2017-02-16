<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleFileUpload.aspx.cs"
    Inherits="wwwroot.App_Ctrl.SingleFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
        function UploadCompleted(msg) {
            var outputField = GetQueryString("OutputField");
            var hiddenField = GetQueryString("HiddenField");
            if (msg == "0") {
                var path = $('#imagePath').val();
                window.parent.document.getElementById(outputField).value = path;
                window.parent.document.getElementById(hiddenField).value = path;
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            } else {
                alert("上传失败");
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            }
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
    <div style="margin-left: 10px; text-align: left; margin-top: 5px;">
        <div id="panLogin" onkeypress="javascript:return WebForm_FireDefaultButton(event, &#39;search_bt&#39;)">
            <p>
                <strong>您当前选择文件上传</strong>&nbsp;</p>
        </div>
        <div style="width: 500px; height: 130px; overflow-y: scroll; border: 2 inset; scrollbar-face-color: #DBEBFE;
            scrollbar-shadow-color: #B8D6FA; scrollbar-highlight-color: #FFFFFF; scrollbar-3dlight-color: #DBEBFE;
            scrollbar-darkshadow-color: #458CE4; scrollbar-track-color: #FFFFFF; scrollbar-arrow-color: #458CE4;
            margin-top: 5px;">
            <table width="480px" id="mytab" border="1" class="t1">
                <thead>
                    <th>
                        请选择要上传的文件</th>
                    </thead>
                <tr>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="400"/>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnUpload" runat="server" Text=" 上 传 " CssClass="SmallButtonA" 
                            onclick="btnUpload_Click" />&nbsp;&nbsp;<input
                            type="button" id="btnCancel" value="清除" class="SmallButtonA" />
                        &nbsp;&nbsp<input type="button" value="取消" class="SmallButtonA" id="btnClose" />
                    <input type="hidden" id="imagePath" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
