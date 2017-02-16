<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_LogsList.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_LogsList"
    ClientIDMode="Static" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <link href="../../App_EasyUI/themes/default/spinner.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datebox.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.spinner.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        #norSearch, #advSearch
        {
            background: url("../../images/search_button.png") no-repeat scroll 0 0 transparent;
            height: 33px;
            margin: 3px;
            width: 107px;
        }
        input.toolBtnA, input.toolBtnB, input.toolBtnC
        {
            background: url("../../images/m_button.png") repeat scroll 0 0 transparent;
            border: 0 none;
            color: #1866F4;
            cursor: pointer;
            font-family: 微软雅黑,宋体,sans-serif;
            font-size: 11pt;
            height: 23px;
            text-decoration: none;
            width: 114px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#txtDateTime').datebox({
                formatter: function (date) { return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate(); },
                parser: function (date) { return new Date(Date.parse(date.replace(/-/g, "/"))); }
            });
            $('#ddlDepartment').change(function () {
                var departmentId = $('#ddlDepartment').val();
                if (departmentId != "101") {
                    $.ajax({
                        type: "get",
                        url: "/App_Services/GetJsonOfEmployeeByDepartmentId.ashx?DepartmentID=" + departmentId,
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#ddlUserID').empty();
                                $('#ddlUserID').removeAttr("disabled");
                                $("<option value=''>--请选择--</option>").appendTo("#ddlUserID");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.UserID + "'>" + item.UserName + "</option>").appendTo("#ddlUserID");
                                });
                            } else {
                                $('#ddlUserID').empty();
                                $("<option value=''>--请选择--</option>").appendTo("#ddlUserID");
                                $('#ddlUserID').attr("disabled", "disabled");
                            }
                        }
                    });
                } else {
                    $('#ddlUserID').empty();
                    $("<option value=''>--请选择--</option>").appendTo("#ddlUserID");
                    $('#ddlUserID').attr("disabled", "disabled");
                }
            });
            $('#ddlUserID').change(function () {
                var userId = $('#ddlUserID').val();
                $('#hidden_ddlUserID').val(userId);
            });
        });
    </script>
    <script type="text/javascript">
        function PreviewProductInfo(ud) {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 430;
            diag.Title = "预览产品信息";
            diag.URL = 'Ass_ProductInfo.aspx?ProductID=' + ud;
            diag.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    资产管理 >> 物品日志查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="AssetsLogs" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr>
                <td>
                    分类：<asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem>所有类型</asp:ListItem>
                        <asp:ListItem>入库</asp:ListItem>
                        <asp:ListItem>领用</asp:ListItem>
                        <asp:ListItem>归还</asp:ListItem>
                        <asp:ListItem>销毁</asp:ListItem>
                        <asp:ListItem>出售</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp; 开始：<asp:TextBox ID="txtStartDate" runat="server" class="easyui-datebox"></asp:TextBox>
&nbsp; 结束：<asp:TextBox ID="txtEndDate" runat="server" class="easyui-datebox"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:HiddenField ID="hidden_ddlUserID" runat="server" />
                    所在部门：<asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; 用户名：<select id="ddlUserID" disabled="disabled" runat="server">
                        <option value=''>--请选择--</option>
                    </select>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="toolBtnA" Text="搜索" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </thead>
    </table>
    <table class="table">
        <asp:ListView ID="LogsView" runat="server">
            <LayoutTemplate>
                <thead>
                    <tr class="">
                        <td width="40">
                            <span style="margin-left: 25px;">ID</span>
                        </td>
                        <td width="100">
                            类型
                        </td>
                        <td width="100">
                            经办人
                        </td>
                        <td width="100">
                            经办时间
                        </td>
                        <td width="138" colspan="2">
                            用户信息
                        </td>
                        <td width="130" colspan="4">
                            经办产品信息
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr id="ItemPlaceHolder" runat="server">
                    </tr>
                </tbody>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="">
                    <td>
                        <span style="margin-left: 25px;">
                            <%#Eval("ID") %></span>
                    </td>
                    <td>
                        <%#Eval("TypeName") %>
                    </td>
                    <td>
                        <%#Eval("Manager") %>
                    </td>
                    <td>
                        <%#Eval("OpTime") %>
                    </td>
                    <td colspan="2">
                        <%#Eval("UserInfo")%></td>
                    <td>
                        <%#Eval("ProductInfo") %>
                    </td>
                    
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <tfood>
            <tr>
                <td colspan="6">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfood>
    </table>
</asp:Content>
