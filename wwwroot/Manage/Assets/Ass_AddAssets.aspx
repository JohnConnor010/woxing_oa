<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_AddAssets.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_AddAssets" ClientIDMode="Static" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript">
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
                        $('#SelectedProduct').val("true");
                        $('#SelectedID').val(item.ID);
                        $('#txtProductID').val(item.ProductID);
                        $('#txtProductName').val(item.ProductName);
                        $('#ddlCategory').val(item.CategoryID);
                        $('#totalQuantity').html(item.Quantity);
                        $('#ddlUnit').val(item.UnitID);
                        $('#txtPrice').val(item.Price);
                        $('#ddlSuppliers').val(item.Suppliers);
                        $('#txtSpecification').val(item.Specification);
                        $('#txtColor').val(item.Color);
                        $('#txtBrand').val(item.Brand);
                        $('#txtModel').val(item.Model);
                        $('#txtProductPhoto').val(item.ProductPhoto);
                        $('#txtRemarks').val(item.Remark);

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
    资产管理 >> 产品入库
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Assets" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="SelectedProduct" runat="server" Value="false" />
    <asp:HiddenField ID="SelectedID" runat="server" Value="0" />
    <table class="table">
        <thead>
            <tr><td>产品入库</td></tr>
        </thead>
    </table>
    <table class="table1">
        <tbody>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品编号：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtProductID" runat="server"></asp:TextBox>
&nbsp;&nbsp; 
                    <input id="btnSelect" type="button" value="已有产品" class="toolBtnB" onclick="ViewAllProducts();" /></td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品名称：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtProductName" runat="server" dataType="Require" msg="产品名称不能为空！"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品数量：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" require="true" dataType="Number" msg="产品数量只能为数字！"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp; 库存数量：<span id="totalQuantity">0</span></td>
</tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    产品类型：[?]</th>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp; 单位：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlUnit" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品单价：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtPrice" runat="server" require="true" dataType="Currency" msg="产品单价不能为空！"></asp:TextBox>元
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;供应商：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlSuppliers" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">--不选择供应商--</asp:ListItem>
                    </asp:DropDownList>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;产品规格：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtSpecification" runat="server"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;颜色：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtColor" runat="server"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;品牌：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBrand" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;型号：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    产品图片：[?]</th>
                <td>
                    <asp:HiddenField ID="hidden_ProductPhoto" runat="server" />
                    <asp:TextBox ID="txtProductPhoto" runat="server" Columns="40"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SingleFileUpload.aspx','上传产品图片','hidden_ProductPhoto','txtProductPhoto',430,110);">上传</a></td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    备注&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请输入 该客户的备注信息. </span>
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Columns="83" Rows="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保存"  OnClientClick="return Validator.Validate(this.form,3);"
                        onclick="btnSave_Click" />
&nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
