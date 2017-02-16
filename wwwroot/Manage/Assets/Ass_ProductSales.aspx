<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Ass_ProductSales.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_ProductSales" ClientIDMode="Static" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" /> 
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
                if ($('#txtQuantity').val() == "") {
                    alert("请输入出售数量！");
                    return false;
                }
            });
            $('#OpTime').datebox({
                dateFormat: 'yyyy-mm-dd'
            });

            $('#txtQuantity').blur(function () {
                var number = new Number($('#txtQuantity').val());
                var total = $('#totalNumber').text();
                var a = parseInt(number);
                if (isNaN(number) == false) {
                    if (parseInt(number) <= 0) {
                        alert("出售数量不能为0");
                        $('#txtQuantity').val("");
                        return false;
                    }
                    else if (parseInt(number) > parseInt(total)) {
                        alert("出售数量必须小于领用数量！");
                        $('#txtQuantity').val("");
                        return false;
                    }
                } else {
                    alert("您输入的出售数量值不准确！");
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
#norSearch, #advSearch {
    background: url("../../images/search_button.png") no-repeat scroll 0 0 transparent;
    height: 33px;
    margin: 3px;
    width: 107px;
}
input.toolBtnA, input.toolBtnB, input.toolBtnC {
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
资产管理 >> 产品销毁
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Assets" CurIndex="5" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:HiddenField ID="PID" runat="server" Value="0" />
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    产品出售
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    出售日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtOpTime" runat="server" Columns="30" class="easyui-datebox"></asp:TextBox>
&nbsp;</td>
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
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    出售产品信息：[?]</th>
                <td>
                    编号：<asp:TextBox 
                        ID="txtProductID" runat="server" Columns="6"></asp:TextBox>
&nbsp;
                    名称：<asp:TextBox ID="txtProductName" runat="server" Columns="15"></asp:TextBox>
&nbsp;
                    单位：<asp:TextBox ID="txtUnit" runat="server" Columns="4"></asp:TextBox>
&nbsp;
                    单价：<asp:TextBox ID="txtPrice" runat="server" Columns="6"></asp:TextBox>
&nbsp;
                    <input id="Button1" type="button" value="选择产品" class="toolBtnA" onclick="ViewAllProducts();" /></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    出售数量：[?]</th>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Columns="6"></asp:TextBox>
                    &nbsp;&nbsp; 库存数量：<span id="totalNumber"><asp:Label ID="totalQuantity" runat="server" Text="0"></asp:Label></span></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    出售原因&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" Columns="60" Rows="6" 
                        TextMode="MultiLine"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="button" 
                         Text="确定出售" onclick="btnSave_Click" />
&nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

