<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_DepartmentList.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_DepartmentList" %>

<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function Confirm() {
            $.messager.confirm("删除确认信息", "您确定要删除此部门吗？", function (result) {
                if (result) {
                    var node = $('#tt').treegrid('getSelected');
                    var b = $('#tt').treegrid('isLeaf', node.id);
                    if (b) {
                        if (node) {
                            $.ajax({
                                type: "get",
                                url: "/App_Services/OperatingDepartment.ashx?CompanyId=<%=companyId %>&action=delete&DepartmentID=" + node.id,
                                success: function (result) {
                                    if (result == "-1") {
                                        $.messager.alert("提示信息", "此功能没打开！", "warning");
                                        return false;
                                    }
                                    else if (result == "-2") {
                                        $.messager.alert("提示信息", "您没有删除的权限！", "warning");
                                        return false;
                                    }
                                    else if (result == "-3") {
                                        $.messager.alert("提示信息", "请先删除或转移部门下的员工！", "warning");
                                        return false;
                                    }
                                    if (result == "0") {
                                        $('#tt').treegrid('reload');
                                    }
                                    else {
                                        $.messager.alert("提示信息", "删除失败！", "warning");
                                        return false;
                                    }
                                }
                            });
                        }
                    } else {
                        $.messager.alert("提示信息", "请先删除此部门下的子部门！", "warning");
                        $('#tt').treegrid('reload');
                        return false;
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 部门管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="dept" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="width: 98%;">
        <span style="float: left; font-style: italic; padding-left: 30px; font-size: +1;">
            <asp:Literal ID="liCompanyName" runat="server">我行技术有限公司</asp:Literal></span>
        <span style="float: right;"><a href="Dept_AddDepartment.aspx" style="font-weight: bolder;
            color: #336;">添加部门</a></span>
    </div>
    <div style="clear: both;" />
    <div id="PanelManage" style="width: 98%; margin: 0 auto; border: 1px solid #bddbef;
        border-collapse: collapse;">
        <table id="tt" width="600" class="easyui-treegrid" style="height: 380px" url="/App_Services/GetJsonOfAllDepartment.ashx?companyId=<%=companyId %>&s=1"
            rownumbers="true" idfield="id" treefield="name" loadmsg="数据加载中请稍后..." animate="true">
            <thead>
                <tr>
                    <th field="deptid" width="60" align="center">部门编号</th>
                    <th field="name" width="300" align="left">部门名称</th>
                    <th field="telephone" width="120" align="left">联系电话</th>
                    <th field="manager" width="100" align="left">部门主管</th>
                    <th field="personcount" width="80" align="left">部门人数</th>
                    <%
                        if (edit == true)
                        {
                            %>
                    <th field="edit" width="60" align="center">编辑</th>
                            <%
                        }
                        if (delete == true)
                        {
                            %>
                    <th field="delete" width="60" align="center">删除</th>
                            <%
                        }
                        if (edit == true)
                        {
                            %>
                    <th field="addDept" width="90" align="center">添加子部门</th>
                            <%
                        }
                    %>
                </tr>
            </thead>
        </table>
    </div>
</asp:Content>
