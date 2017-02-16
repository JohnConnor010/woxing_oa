<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="User_UserList.aspx.cs" Inherits="wwwroot.Manage.HR.User_UserList"
    ClientIDMode="Static" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#tt').datagrid({
                rowStyler: function (index, row) {
                    if (row.State == "0") {
                        return 'color:red';
                    }
                },
                onRowContextMenu: function (e, rowIndex, rowData) {
                    e.preventDefault();
                    $(this).datagrid('unselectAll');
                    $(this).datagrid('selectRow', rowIndex);
                    if (!$('#tmenu').length) {
                        var tmenu = $('<div id="tmenu" style="width:120px;height:auto"></div>').appendTo('body');
                        <%
                        if (edit == true)
                        {
                            %>                        
                        $("<div id='edit' iconCls='icon-edit' />").html('编辑员工信息').appendTo(tmenu);
                        $("<div id='edit_skill' iconCls='icon-edit' />").html('编辑个人技能').appendTo(tmenu);
                        $("<div id='edit_edu' iconCls='icon-edit' />").html('编辑教育经历').appendTo(tmenu);
                        $("<div id='edit_work' iconCls='icon-edit' />").html('编辑工作经历').appendTo(tmenu);
                        $("<div id='edit_fla' iconCls='icon-edit' />").html('编辑家庭成员').appendTo(tmenu);
                        $("<div id='edit_urg' iconCls='icon-edit' />").html('编辑紧急联络人').appendTo(tmenu);
                        $("<div id='edit_cre' iconCls='icon-edit' />").html('编辑员工证书').appendTo(tmenu);
                                <%
                            }
                        %>
                        <%
                            if (delete == true)
                            {
                                %>
                        $("<div id='delete' iconCls='icon-delete' />").html('删除员工信息').appendTo(tmenu);
                                <%
                            }
                        %>  
                        tmenu.menu({
                            onClick: function (item) {
                                if (item.id == "edit") {
                                    window.location.href = "User_EditUser.aspx?CompanyID=<%=companyId %>&UserID=" + rowData.UserID;
                                } else if (item.id == "edit_skill") {
                                   window.location.href = "User_Skill.aspx?CompanyID=<%=companyId %>&UserId=" + rowData.UserID;
                                } 
                                 else if (item.id == "edit_edu") {
                                   window.location.href = "User_Education.aspx?CompanyID=<%=companyId %>&UserId=" + rowData.UserID;
                                } else if (item.id == "edit_work") {
                                   window.location.href = "User_Work.aspx?CompanyID=<%=companyId %>&UserId=" + rowData.UserID;
                                } else if (item.id == "edit_fla") {
                                   window.location.href = "User_Family.aspx?CompanyID=<%=companyId %>&UserId=" + rowData.UserID;
                                }else if (item.id == "edit_urg") {
                                   window.location.href = "User_UrgentLink.aspx?CompanyID=<%=companyId %>&UserId=" + rowData.UserID;
                                }  else if (item.id == "edit_cre") {
                                   window.location.href = "User_Credentials.aspx?CompanyID=<%=companyId %>&UserId=" + rowData.UserID;
                                } else if (item.id == "delete") {
                                    OnSingleDelete();
                                } else if (item.id == "reset") {
                                    window.location.href = "User_ResetPwd.aspx?UserId=" + rowData.UserID;
                                } else if (item.id == "state") {
                                    window.location.href = "User_AccountState.aspx?UserId=" + rowData.UserID;
                                }
                            }
                        });
                    }
                    $('#tmenu').menu('show', {
                        left: e.pageX,
                        top: e.pageY
                    });
                }
            });
            $('#ss').searchbox({
                width: 200,
                searcher: function (value, name) {
                    $('#tt').datagrid('load', {
                        word: value,
                        key: name
                    });
                    $('ss').val("");
                },
                menu: '#mm',
                prompt: '请输入要搜索关键字'
            });
            var p = $('#tt').datagrid('getPager');
            $(p).pagination({
                pageSize: 10, //每页显示的记录条数，默认为10 
                pageList: [1,5, 10, 15, 20], //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录'
            });
        });
        function OnDelete() {
            var rows = $('#tt').datagrid('getSelections');
            var ids = [];
            var param = "";
            var names = [];
            var realname = "";
            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    ids.push(rows[i].UserID);
                    names.push(rows[i].RealName);
                }
                param = ids.join(",");
                realname = names.join(",");
                $.messager.confirm("提示信息", "您选择了" + rows.length + "位要删除的员工，分别是：<br/>【" + realname + "】是否继续？", function (result) {
                    if (result) {
                        $.ajax({
                            type: "get",
                            url: "/App_Services/DeleteEmployee.ashx?companyId=<%=companyId %>&param=" + param,
                            success: function (result) {
                                if (result == "-1") {
                                    $.messager.Alert("提示信息", "您没有开启此功能！", "warning");
                                    return false;
                                } else if (result == "-2") {
                                    $.messager.Alert("提示信息", "您没有此权限！", "warning");
                                    return false;
                                } else if (result == "1") {
                                    $('#tt').datagrid("reload");
                                } else if (result == "0") {
                                    $.messager.alert("提示信息", "删除失败", "warning");
                                    return false;
                                }
                            }
                        });
                    }
                });
            } else {
                $.messager.alert("提示信息", "请选择要删除的员工！", "warning");
                return;
            }
        }
        function OnSingleDelete() {
            var row = $('#tt').datagrid('getSelected');
            $.messager.confirm("提示信息", "您确定要删除姓名为【" + row.RealName + "】的员工吗？", function (result) {
                if (result == true) {
                    $.ajax({
                        type: "get",
                        url: "/App_Services/DeleteEmployee.ashx?companyId=<%=companyId %>&param=" + row.UserID,
                        success: function (result) {
                            if (result == "-1") {
                                $.messager.Alert("提示信息", "您没有开启此功能！", "warning");
                                return false;
                            } else if (result == "-2") {
                                $.messager.Alert("提示信息", "您没有此权限！", "warning");
                                return false;
                            } else if (result == "1") {
                                $('#tt').datagrid("reload");
                            } else if (result == "0") {
                                $.messager.alert("提示信息", "删除失败", "warning");
                                return false;
                            }
                        }
                    });
                }
            });
            return;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 员工档案
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="user" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage" style="width: 98%; margin: 0 auto; border: 1px solid #bddbef;
        border-collapse: collapse;">
        <table id="tt" class="easyui-datagrid" width="1000" style="height: 440px" url="/App_Services/GetAllEmployee.ashx?companyId=<%=companyId %>"
            title="员工信息列表" iconcls="icon-save" rownumbers="true" pagination="true" toolbar="#tb"
            loadmsg="加载中请稍等...">
            <thead>
                <tr>
                    <th field="UserID" checkbox="true">
                    </th>
                    <th field="RealName" width="80">
                        真实姓名
                    </th>
                    <th field="Sex" width="60">
                        性别
                    </th>
                    <th field="DepartmentName" width="100">
                        所在部门
                    </th>
                    <th field="DutyName" width="180">
                        所属职位
                    </th>
                    <th field="Grade" width="150">
                        级别
                    </th>
                    <th field="Birthday" width="150">
                        出生日期
                    </th>
                    <th field="Mobile" width="100">
                        手机号码
                    </th>
                    <th field="QQ" width="100">
                        QQ号码
                    </th>
                    <th field="Email" width="200">
                        电子邮件
                    </th>
                </tr>
            </thead>
        </table>
        <div id="tb" style="padding: 3px">
            <!--a href="User_AddUser.aspx" class="easyui-linkbutton" iconcls="icon-add" plain="true">
                员工入职</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" plain="true" onclick="OnDelete();">
                    删除</a-->
            <input id="ss"></input>
            <div id="mm" style="width: 200px; height:auto">
                <div name="RealName" iconCls="icon-user">
                    真实姓名</div>
                <div name="DepartmentName" iconCls="icon-department">
                    部门</div>
            </div>
        </div>
    </div>
</asp:Content>
