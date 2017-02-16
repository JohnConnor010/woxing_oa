<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowGraphic.aspx.cs" Inherits="wwwroot.App_Demo.AppCtrl_FlowGraphic" %>

<html xmlns:v="urn:schemas-microsoft-com:vml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        v\:*
        {
            behavior: url(#default#VML);
        }
    </style>    
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_Scripts/WorkFlow.js"></script>
    <script type="text/javascript">
        //编辑节点        
        function Edit_Process(id) {
            window.open("/Manage/Flow/Flow_Prcs_Modi.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }
        //添加借点
        function Add_Process() {
            window.open("/Manage/Flow/Flow_Prcs_New.aspx?id=<%=FlowId %>&index=4", "_parent");
        }
        function Del_Process(id) {
            if (confirm("你真要删除这个步骤吗？")) {
                $.ajax({
                    type: "get",
                    url: "/App_Services/SavePosition.ashx?sql=Delete From FL_Process where Id="+id,
                    success: function (result) {
                        if (result > 0) {
                            alert("删除步骤成功！");
                            window.location.reload();
                        }
                        else {
                            alert("删除步骤失败！");
                        }
                    }
                });
            }
        }
        function EditP_opSet(id) {
            window.open("/Manage/Flow/Flow_Prcs_OpSet.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }
        function EditP_passSet(id) {
            window.open("/Manage/Flow/Flow_Prcs_PassSet.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }
        function EditP_editableFlds(id) {
            window.open("/Manage/Flow/Flow_Prcs_EditableFlds.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }
        function EditP_hiddenFlds(id) {
            window.open("/Manage/Flow/Flow_Prcs_HiddenFlds.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }
        function EditP_condition(id) {
            window.open("/Manage/Flow/Flow_Prcs_Conditions.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }
        function EditP_plugs(id) {
            window.open("/Manage/Flow/Flow_Prcs_Plugs.aspx?FlowID=<%=FlowId %>&id=" + id, "_parent");
        }    
        </script>
    <script type="text/javascript">
        //-- 保存布局 --
        function getSql(obj) {
            var strSql = '';
            var mf_pixel_left = 0;
            var mf_pixel_top = 0;
            var flowId = <%=FlowId %> ;
            for (var i = 0; i < obj.length; i++) {
                table_id = eval(obj[i].getAttribute('table_id'));
                mf_pixel_left = obj[i].style.pixelLeft;
                mf_pixel_top = obj[i].style.pixelTop;
                if (table_id > 0) {
                    strSql += "UPDATE FL_Process SET [VML_Left]=" + mf_pixel_left + ",[VML_Top]=" + mf_pixel_top + " WHERE ID=" + table_id + " AND FlowID=" + flowId + ";"
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
                url: "/App_Services/SavePosition.ashx?sql=" + strSql,
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
