<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="AddSupplier.aspx.cs" Inherits="wwwroot.Manage.CTR.AddSupplier" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    合同管理 >> 添加供应商
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Supplier" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table1">
        <thead>
            <tr>
                <td>
                    <a class="helpall" href="#">[?]</a>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 供应商名称：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtCompanyName" runat="server" Columns="30" dataType="Require" msg="供应商名称为必填项！"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 联系电话：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox 
                        ID="txtTelephone" runat="server" require="true" dataType="Phone" msg="联系电话格式不正确！" ></asp:TextBox>
&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    传真：[?]</th>
                <td>
                    <asp:TextBox ID="txtFax" runat="server" Require="false" dataType="Phone" msg="传真号码格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    邮编：[?]</th>
                <td>
                    <asp:TextBox ID="txtZipCode" runat="server" Require="false" dataType="Zip" msg="邮政编码格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    QQ：[?]</th>
                <td>
                    <asp:TextBox ID="txtQQNumber" runat="server" Require="false" dataType="QQ" msg="QQ号码格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    电子邮件：[?]</th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Require="false" dataType="Email" msg="电子邮件格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    企业网站：[?]</th>
                <td>
                    <asp:TextBox ID="txtWebSite" runat="server" Columns="40" Require="false" dataType="Url" msg="网站网址格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    *
                    手机号码：[?]</th>
                <td>
                    <asp:TextBox ID="txtMobilePhone" runat="server" require="true" dataType="Mobile" msg="手机号码不正确！"></asp:TextBox>
                    &nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 供应商地址：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Columns="60"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 主要联系人：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtContactName" runat="server" require="true" dataType="Chinese" msg="联系人姓名必须为中文"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;供应商类别：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlCateogryID" runat="server" dataType="Require" msg="请选择供应商"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    备注&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请输入 该供应商的备注信息. </span>
                    <asp:TextBox TextMode="MultiLine" Columns="83" Rows="7" ID="txtRemark" runat="server" style="background-image: url(/img/line.gif); line-height: 22px;
                        padding-left: 3px; background-repeat: repeat;"></asp:TextBox>
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
