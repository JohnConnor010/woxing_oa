<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Ass_EditAssets.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_EditAssets" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
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
    资产管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="AssetsList-Details" CurIndex="2" Param1="{Q:WarehouseID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="SelectedProduct" runat="server" Value="false" />
    <asp:HiddenField ID="SelectedID" runat="server" Value="0" />
    <table class="table">
        <thead>
            <tr><td>产品修改</td></tr>
        </thead>
    </table>
    <table class="table1">
        <tbody>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品编号：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtProductID" runat="server" ReadOnly="True"></asp:TextBox>
                    &nbsp;</td>
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
                    <asp:TextBox ID="txtQuantity" runat="server" require="true" dataType="Number" msg="产品数量只能为数字！"></asp:TextBox></td>
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
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保存修改"  OnClientClick="return Validator.Validate(this.form,3);"
                        onclick="btnSave_Click" />
&nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

