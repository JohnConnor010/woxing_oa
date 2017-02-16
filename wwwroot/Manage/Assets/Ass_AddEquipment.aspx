<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_AddEquipment.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_AddEquipment" ClientIDMode="Static" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" /> 
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datebox.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                if ($('#ddlDepartment').val() == "101") {
                    alert("请选择领用部门！");
                    return false;
                }
                if ($('#ddlUserID').val() == "") {
                    alert("请选择领用人！");
                    return false;
                }
                if ($('#txtQuantity').val() == "") {
                    alert("请输入领用数量！");
                    return false;
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
                $('#hidden_ddlUser').val(userId);
            });
            $('#txtQuantity').blur(function () {
                var number = new Number($('#txtQuantity').val());
                var total = $('#totalNumber').text();
                var a = parseInt(number);
                if (isNaN(number) == false) {
                    if (parseInt(number) <= 0) {
                        alert("录入数量不能为0");
                        $('#txtQuantity').val("");
                        return false;
                    }
                    else if (parseInt(number) > parseInt(total)) {
                        alert("录入数量必须小于库存数量！");
                        $('#txtQuantity').val("");
                        return false;
                    }
                } else {
                    alert("您录入的数量值不准确！");
                    $('#txtQuantity').val("");
                    return false;
                }
            });
            $('#txtInputDate').datebox({
                formatter: function (date) { return date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate(); },
                parser: function (date) { return new Date(Date.parse(date.replace(/-/g, "/"))); }
            });
        });
        function ViewAllProducts() {
            var diag = new Dialog();
            diag.Width = 1000;
            diag.Height = 500;
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
                        $('#txtProductID').removeAttr("readonly");
                        $('#txtUnit').val(item.UnitName);
                        $('#txtUnit').removeAttr("readonly");
                        $('#txtPrice').val(item.Price);
                        $('#txtPrice').removeAttr("readonly");
                        $('#totalNumber').html(item.Quantity);
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
    资产管理 >> 个人装备录入
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Assets" CurIndex="7" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="PID" runat="server" />
    <table class="table1">
        <thead>
            <tr>
                <td>
                    <a class="helpall" href="#">[?]</a>
                </td>
                <td>
                    &nbsp;个人装备录入</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 145px; font-weight: bold; color: #ff0000;">
                    &nbsp;* 使用人：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:HiddenField ID="hidden_ddlUser" runat="server" />
                    &nbsp;<asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
                    <select id="ddlUserID" disabled="disabled" runat="server">
                        <option value="">--请选择--</option>
                    </select></td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp; 录入时间：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;<asp:TextBox ID="txtInputDate" runat="server" class="easyui-datebox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;产品信息：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;编号：<asp:TextBox 
                        ID="txtProductID" runat="server" Columns="15"></asp:TextBox>
&nbsp;&nbsp; 单价：<asp:TextBox ID="txtPrice" runat="server" Columns="10"></asp:TextBox>
&nbsp;&nbsp; 单位：<asp:TextBox ID="txtUnit" runat="server" Columns="5"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                    <input id="Button1" class="toolBtnA" type="button" value="选择产品" onclick="ViewAllProducts();" /></td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    数量：[?]</th>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Columns="5"></asp:TextBox>
                &nbsp;&nbsp;&nbsp; 库存数量：<asp:Label ID="totalNumber" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    备注：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请输入 会议参与者.</span>
                    <asp:TextBox ID="txtRemark" runat="server" Columns="60" Rows="6" 
                        TextMode="MultiLine"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Enabled="False" 
                        Text="保存" onclick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
