<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowGraphic.aspx.cs" Inherits="wwwroot.App_Demo.FlowGraphic" %>

<html xmlns:v="urn:schemas-microsoft-com:vml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        v\:*
        {
            behavior: url(#default#VML);
        }
    </style>    
    <script type="text/javascript" src="../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script language="javascript" src="../App_Scripts/WorkFlow.js"></script>
    <script type="text/javascript">
        //编辑节点
        function Edit_Process(a, b) {
            window.open("EditGraphic.aspx?FlowID=" + b + "&NodeId=" + a, "修改节点", "height=500,width=700,status=1,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes");
        }
        //添加借点
        function Add_Process() {
            window.open('AddNewGraphic.aspx?FlowID=<%=Request.QueryString["FlowID"] %>', '', 'height=500,width=700,status=1,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes')
        }
    </script>
    <script type="text/javascript">
        //-- 保存布局 --
        function getSql(obj) {
            var strSql = '';
            var mf_pixel_left = 0;
            var mf_pixel_top = 0;
            var flowId = <%=Request.QueryString["FlowID"] %>;
            for (var i = 0; i < obj.length; i++) {
                table_id = eval(obj[i].getAttribute('table_id'));
                mf_pixel_left = obj[i].style.pixelLeft;
                mf_pixel_top = obj[i].style.pixelTop;
                
                if (table_id > 0) {
                    strSql += "UPDATE FlowView SET [Left]=" + mf_pixel_left + ",[Top]=" + mf_pixel_top + " WHERE ID=" + table_id + " AND FlowID=" + flowId + ";"
                }
            }
            return strSql;
        }

        function SavePosition() {
            var strSql = '';
            a = document.getElementsByTagName('roundrect');
            b = document.getElementsByTagName('shape');
            c = document.getElementsByTagName('oval');

            strSql = getSql(a) + getSql(b) + getSql(c);
            $.ajax({
                type: "get",
                url: "../App_Services/SavePosition.ashx?sql=" + strSql,
                success: function (result) {
                    if (result > 0) {
                        alert("布局保存成功！");
                        window.location.reload();
                    }
                    else {
                        alert("布局保存失败！");
                    }
                }
            });
        }
    </script>
</head>
<body oncontextmenu="nocontextmenu();" onmousedown="DoRightClick();" leftmargin="2"
    opmargin="2">
    <form id="form1" runat="server">
    <div>        
        <%=lineString %>
        <%=vmlString %>     
    </div>
    </form>
</body>
</html>
