<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Ass_ReturnAssets.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_ReturnAssets" ClientIDMode="Static" %>
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
            $('#form1').submit(function(){
                if($('#ddlDepartment').val() == "101"){
                    alert("请选择领用部门！");
                    return false;
                }
                if($('#ddlUser').val() == ""){
                    alert("请选择领用人！");
                    return false;
                }
                if($('#txtQuantity').val() == ""){
                    alert("请输入领用数量！");
                    return false;
                }
            });
            $('#OpTime').datebox({
                dateFormat: 'yyyy-mm-dd'
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
            $('#ddlUser').change(function(){
                var userId = $('#ddlUser').val();
                $('#hidden_ddlUser').val(userId);
            });
            $('#txtQuantity').blur(function(){
                var number = new Number($('#txtQuantity').val());
                var total = $('#totalNumber').text();
                var a = parseInt(number);
                if(isNaN(number) == false){
                    if(parseInt(number) <= 0){
                        alert("归还数量不能为0或空");
                        $('#txtQuantity').val("");
                        return false;
                    }
                    else if(parseInt(number) > parseInt(total)){
                        alert("归还数量必须小于领用数量！");
                        $('#txtQuantity').val("");
                        return false;
                    }
                }else{
                    alert("您输入的归还数量值不准确！");
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
            if($('#ddlUser').val() == ""){
                alert("请选择归还人！");
                return false;
            }
            var userId = $('#ddlUser').val();
            var diag = new Dialog();
            diag.Width = 1000;
            diag.Height = 407;
            diag.Title = "选择已有产品";
            diag.URL = "Ass_SelectConsuming.aspx?UserID=" + userId;
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
                        $('#txtUnit').val(item.Unit);
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
资产管理 >> 产品归还
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Assets" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:HiddenField ID="PID" runat="server" Value="0" />
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    产品归还
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    * 归还部门：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server">
                    </asp:DropDownList>
&nbsp;</td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    归还人：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:HiddenField ID="hidden_ddlUser" runat="server" />
                    <select id="ddlUser" runat="server" disabled="disabled">
                        <option value="">--请选择--</option>
                    </select>
&nbsp;</td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    归还日期：&nbsp;<a class="help" href="#">[?]</a>
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
                    归还产品信息：[?]</th>
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
                    归还数量：[?]</th>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Columns="6"></asp:TextBox>
                    &nbsp;&nbsp; 领用数量：<span id="totalNumber"><asp:Label ID="totalQuantity" runat="server" Text="0"></asp:Label></span></td>
            </tr>
            <tr class="">
                <th style="width: 145px; font-weight: bold;">
                    归还原因&nbsp;<a class="help" href="#">[?]</a>
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
                        onclick="btnSave_Click1" Text="确定归还" Enabled="False" />
&nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
