<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Single_EditCustomer.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Single_EditCustomer" ClientIDMode="Static"%>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register assembly="CurrencyTextBox" namespace="SKP.ASP.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="../../App_Scripts/popup.js"></script>
<link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />    
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datebox.js"></script>
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
    <%=mes %>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
客户管理 >> <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Modi" CurIndex="4" Param1="{Q:CustomerID}" />
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
                    <asp:Label ID="Label0" runat="server" Text="" CssClass="tip"></asp:Label>
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
                <td style="width: 430px;">
                    <asp:TextBox ID="txtCustomerName" runat="server" Columns="30" dataType="Require"
                        msg="必填！" MaxLength="25"></asp:TextBox> <asp:Label ID="Label1" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    &nbsp;客户简称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtCustomerZJM" runat="server" MaxLength="10"></asp:TextBox> <asp:Label ID="Label2" runat="server" Text="" CssClass="tip"></asp:Label>
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
                    &nbsp;<asp:Label ID="Label3" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    客户行业：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">客户行业 </span>
                    <asp:DropDownList ID="ddlIndustry" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="Label4" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    客户分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlCustomerCategory" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="Label5" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    客户来源：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlSource" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="Label6" runat="server" Text="" CssClass="tip"></asp:Label>
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
                    <select id="ddlCity" runat="server" disabled="disabled">
                        <option value="0">--请选择--</option>
                    </select>
                    &nbsp;
                    <asp:HiddenField ID="hidden_area" runat="server" Value="0" />
                    <select id="ddlArea" runat="server" disabled="disabled" dataType="Require" msg="必填!">
                        <option value="0">--请选择--</option>
                    </select>
                    <asp:TextBox ID="txtAddress" runat="server" Columns="60" MaxLength="50" dataType="Require" msg="必填!"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    公司网址：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtWebSite" runat="server" Columns="40" require="false" dataType="Url"
                        msg="网站地址格式不正确！" Text="http://"></asp:TextBox>
                    &nbsp;<asp:Label ID="Label8" runat="server" Text="" CssClass="tip"></asp:Label>
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
                    <asp:Label ID="Label9" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    法人代表：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择员工 相应的职能部门 </span>
                    <asp:TextBox ID="txtRealName" runat="server" Columns="30" MaxLength="20"></asp:TextBox>
                    &nbsp;<asp:Label ID="Label10" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    开户银行：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBankName" runat="server" MaxLength="30"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    银行账户：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBankAccount" runat="server" Columns="30" MaxLength="30"></asp:TextBox>
                    <asp:Label ID="Label12" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    工商税务登记号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtBusinessCircles" runat="server" MaxLength="50"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    主营产品：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtProducts" runat="server" Columns="70" MaxLength="50"></asp:TextBox>(以逗号隔开)
                    <asp:Label ID="Label14" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
        </tbody>
        <thead>
            <tr style="height: 40px; vertical-align: bottom;">
                <td colspan="4">
                    客户业务信息
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    业务合作分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlCoop" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList><asp:Label ID="Label15" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <td colspan="2">
                   <%-- <asp:CheckBoxList runat="server" ID="cblCoop" RepeatLayout="Table" RepeatDirection="Horizontal"
                        RepeatColumns="6" CellPadding="6" BorderStyle="Dashed">
                    </asp:CheckBoxList>--%>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    客户跟踪阶段：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:DropDownList ID="ddlStage" runat="server" dataType="Require" msg="必填!">
                    </asp:DropDownList><asp:Label ID="Label16" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <td colspan="2">
                   <%-- <asp:CheckBoxList runat="server" ID="cblStage" RepeatLayout="Table" RepeatDirection="Horizontal"
                        RepeatColumns="6" CellPadding="6" BorderStyle="Dashed">
                    </asp:CheckBoxList>--%>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    近期消费总额：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtLastConsumptionMoney1" runat="server" dataType="Require" msg="近期消费总额必须为数字！" MaxLength="10" Columns="10"></asp:TextBox>
                    &nbsp;元<asp:Label ID="Label17" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    <a style=" color: #008800;" href="/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=my&CustomerID=<%=WX.Request.rCustomerID  %>&fee=1">近期维护费用：</a>&nbsp;
                </th>
                <td>
                    <asp:TextBox ID="txtLastMaintainMoney1" runat="server" dataType="Require" msg="近期维护费用必须为数字！" MaxLength="10" Columns="10"></asp:TextBox>
                    &nbsp;元<asp:Label ID="Label18" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    近期合作时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;从&nbsp;<asp:TextBox ID="tCoolRecentStart" runat="server" Columns="30" class="easyui-datebox"></asp:TextBox>&nbsp;到&nbsp;
                    <asp:TextBox ID="tCoolRecentEnd" runat="server" Columns="30" class="easyui-datebox"></asp:TextBox>
                    &nbsp;<asp:Label ID="Label19" runat="server" Text="" CssClass="tip"></asp:Label>
                    </td>
                    <th style="width: 135px; font-weight: bold; color: #008800;">
                    应收账款：
                </th>
                <td><asp:TextBox ID="txtPreMoney1" runat="server" NumberOfDecimals="2" MaxLength="10" Columns="10"></asp:TextBox>&nbsp;元 
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <span  style="width: 135px; font-weight: bold; color: #008800;">催缴时间：</span><asp:TextBox ID="tAskPreMoneyDate" runat="server"
                            Columns="30" class="easyui-datebox"></asp:TextBox>
                            <asp:Label ID="Label20" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    累计消费总额：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                <asp:TextBox ID="txtConsumptionMoney1" runat="server" NumberOfDecimals="2" dataType="Require" msg="累计消费总额必须为数字！" MaxLength="10" Columns="10"></asp:TextBox>
                    &nbsp;元<asp:Label ID="Label21" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    <a style=" color: #008800;" href="/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=my&CustomerID=<%=WX.Request.rCustomerID  %>&fee=1">累计维护费用：</a>&nbsp;
                </th>
                <td><asp:TextBox ID="txtMaintainMoney1" runat="server" NumberOfDecimals="2" dataType="Require" msg="累计维护费用必须为数字！" MaxLength="10" Columns="10"></asp:TextBox>
                    &nbsp;元<asp:Label ID="Label22" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
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
                    &nbsp;<asp:Label ID="Label23" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    特殊说明&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtSpecialDesc" runat="server" Columns="70" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;<asp:Label ID="Label24" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td style="" colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交审核" Width="120px"
                        OnClick="btnSubmit_Click" OnClientClick="return Validator.Validate(this.form,3);" />
                    &nbsp;&nbsp;&nbsp;<input id="Button2" class="button" style="width: 120px" type="button"
                        value="取消重填" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
