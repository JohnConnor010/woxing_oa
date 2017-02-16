<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDepartment.aspx.cs"
    Inherits="wwwroot.App_Ctrl.SelectDepartment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    
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
input.BigButtonCHover
{
    width:125px;
    height:30px;
    height:27px !important;
    padding-bottom:3px;
    color:#000000;
    background:url("/Manage/images/big_btn_c.png") no-repeat;
    border:0px;
    cursor:pointer;
    font-size:12pt;
    background-position:0 -30px;
}
</style>
<script type="text/javascript" src="../App_Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="../App_Scripts/QueryString.js"></script>
<script type="text/javascript">
    $(function () {
        $('#chkAll').click(function () {
            $("input[name='chk_list']").attr("checked", $(this).attr("checked"));
            var arrChk = $("input[name='chk_list']:checked");
            $('#total').html(arrChk.length);

        });
        $("input[name='chk_list']").click(function () {
            var c = $(this).attr("checked");
            if (!c) $('#chkAll').attr("checked", false);
        });
        $('#btnSubmit').click(function () {
            var outputField = GetQueryString("OutputField");
            var hiddenField = GetQueryString("HiddenField");
            var arrChk = $("input[name='chk_list']:checked");
            if (arrChk.length == 0) {
                window.parent.document.getElementById(outputField).value = "";
                window.parent.document.getElementById(hiddenField).value = "";
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            }
            else if ($('#chkAll').attr("checked")) {
                window.parent.document.getElementById(outputField).value = "所有部门";
                window.parent.document.getElementById(hiddenField).value = "*";
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            }
            else {
                var value1 = new Array();
                var value2 = new Array();
                $.each(arrChk, function (index, item) {
                    var name = item.value.split("|")[0];
                    var userId = item.value.split("|")[1]
                    value1.push(name);
                    value2.push(userId);
                });
                window.parent.document.getElementById(outputField).value = value1;
                window.parent.document.getElementById(hiddenField).value = value2;
                window.parent.document.getElementById("dialogCase").style.display = 'none';
            }
        });
        $('#btnCancel').click(function () {
            $("input[name='chk_list']").attr("checked", false);
            $("input[name='chkAll']").attr("checked",false);
        });
        $('#btnClose').click(function () {
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        });
    });
</script>
</head>
<body>
    <form id="form1" runat="server" runat="server">
    <div style="margin-left: 10px; text-align: left; margin-top: 5px;">
        <p>
            <strong>您已选择了 <span
                id="total" style="color: blue; font-weight: bold;">0</span> 个部门！</strong> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <table width="400px" id="Table1" border="1" class="t1">
            <tr>
                <td title='选中/取消选中'>
                    <input name="chkAll" type="checkbox" id="chkAll" title='选中/取消选中' value="checkbox">
                    全选
                </td>
            </tr>
        </table>
        <div style="width: 400px; height: 230px;overflow:auto; overflow-y: scroll; overflow-x:hidden; border: 2 inset; scrollbar-face-color: #DBEBFE;
            scrollbar-shadow-color: #B8D6FA; scrollbar-highlight-color: #FFFFFF; scrollbar-3dlight-color: #DBEBFE;
            scrollbar-darkshadow-color: #458CE4; scrollbar-track-color: #FFFFFF; scrollbar-arrow-color: #458CE4;
            margin-top: 5px;">
            <table width="380px" id="mytab" border="1" class="t1">
                <thead>
                    <th width="11%">
                        选择
                    </th>
                    <th width="50%">
                        组织机构名称
                    </th>
                </thead>
                <asp:Repeater ID="DepartmentRepeater" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <input name="chk_list" type="checkbox" id="rpt_chk_<%#Eval("index") %>"  value="<%#Eval("DepartmentName") %>|<%#Eval("DepartmentID") %>" <%# GetCheckedString(Eval("DepartmentID")) %> />
                    </td>
                    <td>
                        <span style="color: #006600; font-weight: bold;"><strong>
                            <%#Eval("Node_Name")%></strong></span>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="text-align: center; width: 400px;">
            <input type="button" id="btnSubmit" value="提交" class="SmallButtonA" />&nbsp;&nbsp;<input
                type="button" id="btnCancel" value="清除" class="SmallButtonA" />&nbsp;&nbsp;
            <input type="button" id="btnClose" class="SmallButtonA" value="取消" />
        </div>
    </div>
    </form>
</body>
</html>
