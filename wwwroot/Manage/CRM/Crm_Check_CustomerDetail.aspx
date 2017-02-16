<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crm_Check_CustomerDetail.aspx.cs"
    Inherits="wwwroot.Manage.CRM.Crm_Check_CustomerDetail" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <style type="text/css">
        .style1
        {
            width: 265px;
        }
        .style2
        {
            width: 119px;
            background-color: #fff;
            border-width: 1px;
        }
        tr.title
        {
            font-style: italic;
            background-color: #eee;
        }
        .txtinput
        {
            border: none;
        }
    </style>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datebox.js"></script>
    <script type="text/javascript">
    function butsumit()
        {
        alert("信息审核完成！");
            window.parent.location="Crm_Check_CheckCustomer.aspx";
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        }
        function butnull(str)
        {
        alert(str);
            window.parent.location="Crm_Check_CheckCustomer.aspx";
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        }
        <%=mes %>
        $(function () {
            $('#txtProvince').change(function () {
                var code = $('#txtProvince').val();
                if (code != "0") {
                    $.ajax({
                        url: "/App_Services/GetRegionByCode.ashx?Region=Province&code=" + code,
                        type: "get",
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#txtCity').removeAttr("disabled");
                                $('#txtCity').empty();
                                $('#txtCity').append("<option value='0'>--请选择--</option>");
                                $('#txtArea').empty();
                                $('#txtArea').append("<option value='0'>--请选择--</option>");
                                $('#txtArea').attr("disabled", "disabled");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.Code + "'>" + item.Name + "</option>").appendTo('#txtCity');
                                });
                            }
                        }
                    });
                } else {
                    $('#txtCity').empty();
                    $('#txtCity').append("<option value='0'>--请选择--</option>");
                    $('#txtCity').attr("disabled", "disabled");
                    $('#txtArea').empty();
                    $('#txtArea').append("<option value='0'>--请选择--</option>");
                    $('#txtArea').attr("disabled", "disabled");
                }
            });
            $('#txtCity').change(function () {
                var code = $('#txtCity').val();
                $('#hidden_city').val(code);
                if (code != "0") {
                    $.ajax({
                        url: "/App_Services/GetRegionByCode.ashx?Region=City&code=" + code,
                        type: "get",
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#txtArea').removeAttr("disabled");
                                $('#txtArea').empty();
                                $('#txtArea').append("<option value='0'>--请选择--</option>");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.Code + "'>" + item.Name + "</option>").appendTo('#txtArea');
                                });
                            } else {
                                $('#txtArea').empty();
                                $('#txtArea').append("<option value='0'>--请选择--</option>");
                                $('#txtArea').attr("disabled", "disabled");
                            }
                        }
                    });
                } else {
                    $('#txtArea').empty();
                    $('#txtArea').append("<option value='0'>--请选择--</option>");
                    $('#txtArea').attr("disabled", "disabled");
                }
            });
            $('#txtArea').change(function () {
                var code = $('#txtArea').val();
                $('#hidden_area').val(code);
            });
            $("a.help").hide();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="table3" width="100%">
        <thead>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <b>
                        <center>
                            原始数据</center>
                    </b>
                </td>
                <td>
                    <b>
                        <center>
                            审核数据</center>
                    </b>
                </td>
            </tr>
        </thead>
        <tr>
            <td style="width: 150px;">
                <b>客户编号：</b>
            </td>
            <td style="width: 400px;">
                <asp:Label ID="liCustomerID" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustomerID" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>客户简称：</b>
            </td>
            <td>
                <asp:Label ID="liCustomerZJM" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustomerZJM" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>客户全称：</b>
            </td>
            <td>
                <asp:Label ID="liCustomerName" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCustomerName" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>企业性质：</b>
            </td>
            <td>
                <asp:Label ID="liNatureID" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="txtNatureID" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>客户行业：</b>
            </td>
            <td>
                <asp:Label ID="liIndustry" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="txtIndustry" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>客户分类：</b>
            </td>
            <td>
                <asp:Label ID="liCategory" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="txtCategory" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>客户来源：</b>
            </td>
            <td>
                <asp:Label ID="liSource" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="txtSource" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>所在区域：</b>
            </td>
            <td>
                <asp:Label ID="liAddress" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="txtProvince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtProvince_SelectedIndexChanged"
                    Width="80">
                </asp:DropDownList>
                <asp:DropDownList ID="txtCity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtCity_SelectedIndexChanged"
                    Width="50">
                </asp:DropDownList>
                <asp:DropDownList ID="txtArea" runat="server" dataType="Require" msg="必填!" Width="50">
                </asp:DropDownList>
                <asp:TextBox ID="txtAddress" runat="server" Columns="15" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>公司网址：</b>
            </td>
            <td>
                <asp:Label ID="liWebSite" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWebSite" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <%--<tr>
            <td>
                <b>企业照片：</b>
            </td>
            <td>
                <asp:Label ID="liimagePath" runat="server"></asp:Label>
            </td>
            <td>
                    <asp:HiddenField ID="hidden_imagePath" runat="server" />
                    <asp:TextBox ID="txtImagePath" runat="server" Columns="40" ReadOnly="true"></asp:TextBox>
                    &nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SingleFileUpload.aspx','上传图片','hidden_imagePath','txtImagePath',530,180);">上传</a>
            </td>
        </tr>--%>
        <tr>
            <td>
                <b>成立时间：</b>
            </td>
            <td>
                <asp:Label ID="liEstablishmentDate" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEstablishmentDate" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>法人代表：</b>
            </td>
            <td>
                <asp:Label ID="liRealName" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRealName" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>开户银行：</b>
            </td>
            <td>
                <asp:Label ID="liBankName" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBankName" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>银行账户：</b>
            </td>
            <td>
                <asp:Label ID="liBankAccount" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBankAccount" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>税务登记：</b>
            </td>
            <td>
                <asp:Label ID="liBusinessCircles" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBusinessCircles" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>主营产品：</b>
            </td>
            <td>
                <asp:Label ID="liProducts" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtProducts" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>业务合作分类：</b>
            </td>
            <td>
                <asp:Label ID="liCoop" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlCoop" runat="server" dataType="Require" msg="必填!">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>业务跟踪阶段：</b>
            </td>
            <td>
                <asp:Label ID="liStage" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlStage" runat="server" dataType="Require" msg="必填!">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>近期消费总额：</b>
            </td>
            <td>
                <asp:Label ID="liLastConsumptionMoney" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLastConsumptionMoney" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>近期合作时间：</b>
            </td>
            <td>
                <asp:Label ID="liCoolRecentStart" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCoolRecentStart" runat="server" Width="120"></asp:TextBox>&nbsp;-&nbsp;
                <asp:TextBox ID="txtCoolRecentEnd" runat="server" Width="120"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>累计消费总额：</b>
            </td>
            <td>
                <asp:Label ID="liConsumptionMoney" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConsumptionMoney" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>近期维护费用：</b>
            </td>
            <td>
                <asp:Label ID="liLastMaintainMoney" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLastMaintainMoney" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>应收账款：</b>
            </td>
            <td>
                <asp:Label ID="liPreMoney" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPreMoney" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>催缴时间：</b>
            </td>
            <td>
                <asp:Label ID="liAskPreMoneyDate" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAskPreMoneyDate" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>累计维护费用：</b>
            </td>
            <td>
                <asp:Label ID="liMaintainMoney" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMaintainMoney" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>公司简介：</b>
            </td>
            <td colspan="2" width="720">
                <div style="height: 80px; width: 100%; overflow-y: scroll">
                    <asp:Label ID="liRemarks" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>特殊说明：</b>
            </td>
            <td colspan="2" width="720">
                <div style="height: 80px; width: 100%; overflow-y: scroll">
                <asp:Label ID="liSpecialDesc" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtSpecialDesc" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <center>
                    <asp:Button ID="Button1" runat="server" Text="通过" OnClick="Button1_Click" />&nbsp;&nbsp;<asp:Button
                        ID="Button2" runat="server" Text="未通过" OnClick="Button2_Click" /></center>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
