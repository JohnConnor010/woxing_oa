<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Ass_AssetsUsedList.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_AssetsUsedList" ClientIDMode="Static" %>
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
            $('#ddlCategoryID').change(function () {
                var categoryId = $('#ddlCategoryID').val();
                if (categoryId != "1") {
                    $.ajax({
                        type: "get",
                        url: "/App_Services/GetJsonOfProductOfCategoryID.ashx?CategoryID=" + categoryId,
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#ddlProductList').empty();
                                $('#ddlProductList').removeAttr("disabled");
                                $("<option value=''>--请选择--</option>").appendTo("#ddlProductList");
                                $.each(result, function (index, item) {

                                    $("<option value='" + item.ProductID + "'>" + item.ProductName + "</option>").appendTo("#ddlProductList");
                                });
                            } else {
                                $('#ddlProductList').empty();
                                $("<option value=''>--请选择--</option>").appendTo("#ddlProductList");
                                $('#ddlProductList').attr("disabled", "disabled");
                            }
                        }
                    });
                } else {
                    $('#ddlProductList').empty();
                    $("<option value=''>--请选择--</option>").appendTo("#ddlProductList");
                    $('#ddlProductList').attr("disabled", "disabled");
                }
            });
            $('#ddlProductList').change(function () {
                var userId = $('#ddlProductList').val();
                $('#hidden_ddlProductList').val(userId);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    资产管理 >> 物品日志查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Assets" CurIndex="9" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr>
                <td>
                    产品分类：<asp:DropDownList ID="ddlCategoryID" runat="server">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hidden_ddlProductList" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 产品名称：<select id="ddlProductList" disabled="disabled">
                        <option value="0">--请选择--</option>
                    </select> &nbsp;&nbsp;&nbsp;
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
                        <td width="138">
                            <asp:Label ID="lblUser" runat="server" Text="用户名"></asp:Label>
                        </td>
                        <td width="55">
                            部门名称
                        </td>
                        <td width="130">
                            数量
                        </td>
                        <td width="60">
                            产品编号
                        </td>
                        <td width="80">
                            单价
                        </td>
                        <td width="80">
                            单位
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
                    <td>
                        <%#Eval("UserName") %>
                    </td>
                    <td>
                        <%#Eval("DepartmentName")%>
                    </td>
                    <td>
                        <%#Eval("Quantity") %>
                    </td>
                    <td>
                        <span>
                            <%#Eval("ProductID") %></span>
                    </td>
                    <td>
                        <%#Eval("Price") %>
                    </td>
                    <td>
                        <%#Eval("UnitName") %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <tfood>
            <tr>
                <td colspan="10">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfood>
    </table>
</asp:Content>

