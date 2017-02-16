<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="AddContract.aspx.cs" Inherits="wwwroot.Manage.CTR.AddContract"
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
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript">
        $(function () {
            $.ajaxSetup({ cache: false });
            $('#ddlDepartment').change(function () {
                var departmentId = $('#ddlDepartment').val();
                if (departmentId != "0") {
                    $.ajax({
                        type: "get",
                        url: "/App_Services/GetJsonOfEmployeeByDepartmentId.ashx?DepartmentID=" + departmentId,
                        dataType: "json",
                        success: function (result) {
                            if (result != "") {
                                $('#ddlEmployee').empty();
                                $('#ddlEmployee').removeAttr("disabled");
                                $("<option value=''>--请选择--</option>").appendTo("#ddlEmployee");
                                $.each(result, function (index, item) {
                                    $("<option value='" + item.UserID + "'>" + item.UserName + "</option>").appendTo("#ddlEmployee");
                                });
                            } else {
                                $('#ddlEmployee').empty();
                                $("<option value=''>--请选择--</option>").appendTo("#ddlEmployee");
                                $('#ddlEmployee').attr("disabled", "disabled");
                            }
                        }
                    });
                } else {
                    $('#ddlEmployee').empty();
                    $("<option value=''>--请选择--</option>").appendTo("#ddlUser");
                    $('#ddlEmployee').attr("disabled", "disabled");
                }
            });
            $('#ddlEmployee').change(function () {
                var userId = $('#ddlEmployee').val();
                $('#hidden_ddlEmployee').val(userId);
            });
        });
    </script>
    <script type="text/javascript">
        function ViewSuppliers(param) {
            var diag = new Dialog();
            diag.Width = 367;
            diag.Height = 280;
            diag.Title = "选择合同的甲乙双方";
            diag.URL = "SelectPartyAB.aspx";
            diag.OKEvent = function () {
                var title = diag.innerFrame.contentWindow.document.getElementById('hidden_title').value;
                var value = diag.innerFrame.contentWindow.document.getElementById('hidden_value').value;
                switch (title) {
                    case "选择人员":
                        $('#' + param).val(value);
                        break;
                    case "选择供应商":
                        $('#' + param).val(value);
                        break;
                    case "选择客户":
                        $('#' + param).val(value);
                        break;
                    default:
                        $('#' + param).val(value);
                        break;
                }
                diag.close();
            }
            diag.show();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    合同管理 >> 添加合同
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Contract" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td colspan="2">
                    <a class="helpall" href="#">[?]</a>
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    * 合同编号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请输入合同编号 </span>
                    <asp:TextBox ID="txtContractID" runat="server" ReadOnly="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr id="zjss" class="">
                <th style="width: 135px; font-weight: bold; color: #0055ff;">
                    合同名称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                <span class="note" style="display: none;">请输入合同名称 </span>
                    <asp:TextBox ID="txtContractName" runat="server" Columns="30" dataType="Require" msg="合同名称为必填项！"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    * 合同分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择合同分类 </span>
                    <asp:DropDownList ID="ddlCategory" runat="server" dataType="Require" msg="合同分类为必选项！">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    * 所属项目：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择合同所属的项目 </span>
                    <asp:DropDownList ID="ddlProject" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    * 合同金额：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请输入合同金额，必须为数字形式 </span>
                    <cc1:CurrencyTextBox ID="txtPrice" runat="server" NumberOfDecimals="2" dataType="Require" msg="合同金额为必填项！"></cc1:CurrencyTextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    币种：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择币种，默认为人民币 </span>
                    <asp:DropDownList ID="ddlCurrency" runat="server">
                        <asp:ListItem>人民币</asp:ListItem>
                        <asp:ListItem>美元</asp:ListItem>
                        <asp:ListItem>英镑</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    签订日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择合同签订日期，默认为当前日期 </span>
                    <asp:TextBox ID="txtSignedDate" runat="server" class="easyui-datebox"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    业务部门：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择业务员所属的部门 </span>
                    <asp:DropDownList ID="ddlDepartment" runat="server" dataType="Require" msg="业务部门为必选项！">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    业务员：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择业务员 </span>
                    <asp:HiddenField ID="hidden_ddlEmployee" runat="server" />
                    <select id="ddlEmployee" disabled="disabled" name="D1" dataType="Require" msg="业务员为必选项！">
                        <option value="">--请选择--</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    支付方式：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择支付方式 </span>
                    <asp:DropDownList ID="ddlPaymentType" runat="server">
                        <asp:ListItem>现金</asp:ListItem>
                        <asp:ListItem>信用卡</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    开始日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同的开始日期，默认为当前日期 </span>
                    <asp:TextBox ID="txtStartDate" runat="server" class="easyui-datebox"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
            <span class="note" style="display: none;">合同的开截至日期，默认为当前日期 </span>
                <th style="width: 135px; font-weight: bold;">
                    截止日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同截至日期 </span>
                    <asp:TextBox ID="txtEndDate" runat="server" class="easyui-datebox"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    收付类型：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同是收款还是付款</span>
                    <asp:DropDownList ID="ddlReceivablesPayment" runat="server">
                        <asp:ListItem>收款</asp:ListItem>
                        <asp:ListItem>付款</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    合同内容：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同的详细内容 </span>
                    <asp:TextBox ID="txtContractContent" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    合同异常处理情况：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同异常处理情况 </span>
                    <asp:TextBox ID="txtContractAbnormal" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    合同甲方：<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">签订合同的甲方 </span>
                    <asp:HiddenField ID="hidden_PartA" runat="server" />
                    <asp:TextBox ID="txtPartyA" runat="server" Columns="30" dataType="Require" msg="合同甲方为必填项！"></asp:TextBox>
                    &nbsp; 负责人：<asp:TextBox ID="txtPartyAPerson" runat="server" Columns="10"></asp:TextBox>
                    &nbsp;&nbsp; ╋<a href="javascript:void(0)" onclick="ViewSuppliers('txtPartyA');">选择</a>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    合同乙方：<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">签订合同的乙方</span>
                    <asp:TextBox ID="txtPartyB" runat="server" Columns="30" dataType="Require" msg="合同乙方为必填项！"></asp:TextBox>
                    &nbsp; 负责人：<asp:TextBox ID="txtPartyBPerson" runat="server" Columns="10"></asp:TextBox>
                    &nbsp;&nbsp; ╋<a href="javascript:void(0)" onclick="ViewSuppliers('txtPartyB');">选择</a>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    电子合同路径：<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">电子合同的上传路径</span>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" />
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    执行情况：<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">所签合同的执行情况</span>
                    <asp:DropDownList ID="ddlImplementation" runat="server">
                        <asp:ListItem>未执行</asp:ListItem>
                        <asp:ListItem Selected="True">执行中</asp:ListItem>
                        <asp:ListItem>终止搁置</asp:ListItem>
                        <asp:ListItem>已完成</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    录入日期：<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同的录入日期</span>
                    <asp:TextBox ID="txtInputDate" runat="server" class="easyui-datebox"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    经办人：<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">合同的经办人</span>
                    <asp:TextBox ID="txtManager" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="确定添加" 
                        onclick="btnSave_Click" OnClientClick="return Validator.Validate(this.form,3);" />
                    &nbsp;&nbsp;
                    <input id="Reset1" class="button" type="reset" value="取消添加" />
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
