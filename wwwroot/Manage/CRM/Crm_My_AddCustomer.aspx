<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_My_AddCustomer.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_My_AddCustomer"
    ClientIDMode="Static" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register assembly="CurrencyTextBox" namespace="SKP.ASP.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datebox.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ddlProvince').change(function () {
                var code = $('#ddlProvince').val();
                if (code != "0") {
                    $.ajax({
                        url: "../../App_Services/GetRegionByCode.ashx?Region=Province&code=" + code,
                        type: "get",
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#ddlCity').removeAttr("disabled");
                                $('#ddlCity').empty();
                                $('#ddlCity').append("<option value='0'>--请选择--</option>");
                                $('#ddlArea').empty();
                                $('#ddlArea').append("<option value='0'>--请选择--</option>");
                                $('#ddlArea').attr("disabled", "disabled");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.Code + "'>" + item.Name + "</option>").appendTo('#ddlCity');
                                });
                            }
                        }
                    });
                } else {
                    $('#ddlCity').empty();
                    $('#ddlCity').append("<option value='0'>--请选择--</option>");
                    $('#ddlCity').attr("disabled", "disabled");
                    $('#ddlArea').empty();
                    $('#ddlArea').append("<option value='0'>--请选择--</option>");
                    $('#ddlArea').attr("disabled", "disabled");
                }
            });
            $('#ddlCity').change(function () {
                var code = $('#ddlCity').val();
                $('#hidden_city').val(code);
                if (code != "0") {
                    $.ajax({
                        url: "../../App_Services/GetRegionByCode.ashx?Region=City&code=" + code,
                        type: "get",
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#ddlArea').removeAttr("disabled");
                                $('#ddlArea').empty();
                                $('#ddlArea').append("<option value='0'>--请选择--</option>");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.Code + "'>" + item.Name + "</option>").appendTo('#ddlArea');
                                });
                            } else {
                                $('#ddlArea').empty();
                                $('#ddlArea').append("<option value='0'>--请选择--</option>");
                                $('#ddlArea').attr("disabled", "disabled");
                            }
                        }
                    });
                } else {
                    $('#ddlArea').empty();
                    $('#ddlArea').append("<option value='0'>--请选择--</option>");
                    $('#ddlArea').attr("disabled", "disabled");
                }
            });
            $('#ddlArea').change(function () {
                var code = $('#ddlArea').val();
                $('#hidden_area').val(code);
            });
            $("a.help").hide();
        });
        <%=mess %>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 我的客户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="MyCustomer" CurIndex="5" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    客户基本信息&nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    * 客户编号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtCustomerID" runat="server" Columns="20" MaxLength="20" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    * 客户全称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td style="width: 250px;">
                    <asp:TextBox ID="txtCustomerName" runat="server" Columns="30" dataType="Require"
                        msg="必填！" MaxLength="25"></asp:TextBox>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    &nbsp;客户简称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtCustomerZJM" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    客户企业性质：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">客户企业性质 </span>
                    <asp:DropDownList ID="ddlCompanyNature" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <th style="width: 135px; font-weight: bold;">
                    客户行业：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">客户行业 </span>
                    <asp:DropDownList ID="ddlIndustry" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    客户分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlCustomerCategory" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;
                </td>
                <th style="width: 135px; font-weight: bold;">
                    客户来源：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlSource" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    所在区域：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlProvince" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:HiddenField ID="hidden_city" runat="server" Value="0" />
                    <select id="ddlCity" disabled="disabled">
                        <option value="0">--请选择--</option>
                    </select>
                    &nbsp;
                    <asp:HiddenField ID="hidden_area" runat="server" Value="0" />
                    <select id="ddlArea" disabled="disabled" dataType="Require" msg="必填!">
                        <option value="0">--请选择--</option>
                    </select>
                    <asp:TextBox ID="txtAddress" runat="server" Columns="60" MaxLength="50" dataType="Require" msg="必填!"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    公司网址：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtWebSite" runat="server" Columns="40" require="false" dataType="Url"
                        msg="网站地址格式不正确！" Text="http://"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
           <%--<tr>
                <th style="width: 135px; font-weight: bold;">
                    客户企业照片：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <span class="note" style="display: none;">员工的个人照片 </span>
                    <asp:HiddenField ID="hidden_imagePath" runat="server" />
                    <asp:TextBox ID="txtImagePath" runat="server" Columns="50" ReadOnly="true"></asp:TextBox>
                    &nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SingleFileUpload.aspx','上传图片','hidden_imagePath','txtImagePath',530,180);">上传</a>
                </td>
            </tr>--%>
        </tbody>
        <thead>
            <tr style="height: 40px; vertical-align: bottom;">
                <td colspan="4">
                    客户身份信息
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    公司成立日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtEstablishmentDate" runat="server" Columns="30" class="easyui-datebox"></asp:TextBox>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    法人代表：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择员工 相应的职能部门 </span>
                    <asp:TextBox ID="txtRealName" runat="server" Columns="30" MaxLength="20"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    开户银行：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBankName" runat="server" MaxLength="30"></asp:TextBox>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    银行账户：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBankAccount" runat="server" Columns="30" MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    工商税务登记号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBusinessCircles" runat="server" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    主营产品：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtProducts" runat="server" Columns="70" MaxLength="50"></asp:TextBox>(以逗号隔开)
                </td>
            </tr>
        </tbody>
        <tbody>
            <thead>
                <tr style="height: 40px; vertical-align: bottom;">
                    <td colspan="4">
                        其它信息
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    公司简介&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" Columns="70" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    特殊说明&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtSpecialDesc" runat="server" Columns="70" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td style="" colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交信息" Width="120px"
                        OnClick="btnSubmit_Click" OnClientClick="return Validator.Validate(this.form,3);" />
                    &nbsp;&nbsp;&nbsp;<input id="Button2" class="button" style="width: 120px" type="button"
                        value="取消重填" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
