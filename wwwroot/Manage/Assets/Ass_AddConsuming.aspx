<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_AddConsuming.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_AddConsuming"
    ClientIDMode="Static" %>

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
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                if ($('#ddlDepartment').val() == "101") {
                    alert("请选择领用部门！");
                    return false;
                }
                if ($('#ddlUser').val() == "") {
                    alert("请选择领用人！");
                    return false;
                }
                if ($('#txtQuantity').val() == "") {
                    alert("请输入领用数量！");
                    return false;
                }
            });
            $('#OpTime').datebox({
                dateFormat: 'yyyy-mm-dd',
            });
            $('#txtDeadline').numberspinner();
            $('#txtDeadline').numberspinner({
                onSpinUp: function () {
                    var value = $('#txtDeadline').numberspinner('getValue');
                    var today = showdate(value);
                    $('#txtMaturityDate').datebox('setValue', today);
                    $('#hidden_MaturityDate').val(today);
                },
                onSpinDown: function () {
                    var value = $('#txtDeadline').numberspinner('getValue');
                    if (value < 0) {
                        alert("归还期限不能小于0");
                        $('#txtDeadline').numberspinner('setValue', "0");
                        return false;
                    }
                    else {
                        var today = showdate(value);
                        $('#txtMaturityDate').datebox('setValue', today);
                        $('#hidden_MaturityDate').value(today);
                    }
                }
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
                                $('#ddlUser').empty();
                                $('#ddlUser').removeAttr("disabled");
                                $("<option value=''>--请选择--</option>").appendTo("#ddlUser");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.UserID + "'>" + item.UserName + "</option>").appendTo("#ddlUser");
                                });
                            } else {
                                $('#ddlUser').empty();
                                $("<option value=''>--请选择--</option>").appendTo("#ddlUser");
                                $('#ddlUser').attr("disabled", "disabled");
                            }
                        }
                    });
                } else {
                    $('#ddlUser').empty();
                    $("<option value=''>--请选择--</option>").appendTo("#ddlUser");
                    $('#ddlUser').attr("disabled", "disabled");
                }
            });
            $('#ddlUser').change(function () {
                var userId = $('#ddlUser').val();
                $('#hidden_ddlUser').val(userId);
            });
            $('#txtQuantity').blur(function () {
                var number = new Number($('#txtQuantity').val());
                var total = $('#totalNumber').text();
                var a = parseInt(number);
                if (isNaN(number) == false) {
                    if (parseInt(number) <= 0) {
                        alert("领用数量不能为0");
                        $('#txtQuantity').val("");
                        return false;
                    }
                    else if (parseInt(number) > parseInt(total)) {
                        alert("领用数量必须小于库存数量！");
                        $('#txtQuantity').val("");
                        return false;
                    }
                } else {
                    alert("您输入的领用数量值不准确！");
                    $('#txtQuantity').val("");
                    return false;
                }
            });
        });
        function showdate(n) {
            var uom = new Date(new Date() - 0 + n * 86400000);
            uom = uom.getFullYear() + "-" + (uom.getMonth() + 1) + "-" + uom.getDate();
            return uom;
        }
        function ViewAllProducts() {
            var diag = new Dialog();
            diag.Width = 1000;
            diag.Height = 407;
            diag.Title = "选择已有产品";
            diag.URL = "Ass_SelectAssets.aspx";
            diag.OKEvent = function () {
                var text = diag.innerFrame.contentWindow.document.getElementById('hidden_json').value;
                if (text == "") {
                    alert("请选择要添加的产品！");
                    return false;
                }
                else {
                    var json = eval(text);
                    $.each(json, function (index, item) {
                        $('#PID').val(item.ID);
                        $('#txtProductID').val(item.ProductID);
                        $('#txtProductName').val(item.ProductName);
                        $('#txtUnit').val(item.UnitName);
                        $('#txtPrice').val(item.Price);
                        $('#totalQuantity').html(item.Quantity);
                        $('#btnSave').removeAttr("disabled");
                    });
                }
                diag.close();
            }
            diag.show();

        }
    </script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    资产管理 >> 产品领用
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Assets" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="PID" runat="server" Value="0" />
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    产品领用单
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 领用部门：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    领用人：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:HiddenField ID="hidden_ddlUser" runat="server" />
                    <select id="ddlUser" runat="server" disabled="disabled">
                        <option value="">--请选择--</option>
                    </select>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    领用日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtOpTime" runat="server" Columns="30" class="easyui-datebox"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 经办人：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择该组织机构 的上级部门/职位 </span>
                    <asp:TextBox ID="txtOpUserName" runat="server"></asp:TextBox>
                    &nbsp;<asp:HiddenField ID="txtOpUserID" runat="server" />
                </td>
            </tr>
            <%--<tr class="">
                <th style="width: 145px; font-weight: bold;">
                    归还期限：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;<asp:TextBox ID="txtDeadline" Text="0" runat="server" class="easyui-numberspinner"
                        data-options="min:1,max:100,required:true" Style="width: 40px;"></asp:TextBox>
                    &nbsp;天 (&quot;0&quot;天表示不用归还)
                </td>
            </tr>--%>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    归还期至：[?]
                </th>
                <td>
                    <asp:HiddenField ID="hidden_MaturityDate" runat="server" />
                    <asp:TextBox ID="txtMaturityDate" runat="server" class="easyui-datebox" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    领用产品信息：[?]
                </th>
                <td>
                    编号：<asp:TextBox ID="txtProductID" runat="server" Columns="6"></asp:TextBox>
                    &nbsp; 名称：<asp:TextBox ID="txtProductName" runat="server" Columns="15"></asp:TextBox>
                    &nbsp; 单位：<asp:TextBox ID="txtUnit" runat="server" Columns="4"></asp:TextBox>
                    &nbsp; 单价：<asp:TextBox ID="txtPrice" runat="server" Columns="6"></asp:TextBox>
                    &nbsp;
                    <input id="Button1" type="button" value="选择产品" class="toolBtnA" onclick="ViewAllProducts();" />
                </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    领用数量：[?]
                </th>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Columns="6"></asp:TextBox>
                    &nbsp;&nbsp; 库存数量：<span id="totalNumber"><asp:Label ID="totalQuantity" runat="server"
                        Text="0"></asp:Label></span>
                </td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    备注&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" Columns="60" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Enabled="false" OnClick="btnSave_Click"
                        Text="保存" />
                    &nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
