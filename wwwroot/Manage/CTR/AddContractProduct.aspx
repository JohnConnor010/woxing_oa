<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="AddContractProduct.aspx.cs" Inherits="wwwroot.Manage.CTR.AddContractProduct"
    ClientIDMode="Static" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="CurrencyTextBox" Namespace="SKP.ASP.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript">
        function ViewOurProduct() {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 400;
            diag.Title = "选择服务产品";
            diag.URL = "SelectOurProduct.aspx";
            diag.OKEvent = function () {
                var text = diag.innerFrame.contentWindow.document.getElementById('hidden_json').value;
                if (text == "") {
                    alert("请选择要添加的产品！");
                    return false;
                }
                else {
                    var json = eval(text);
                    $.each(json, function (index, item) {
                        $('#txtProductName').val(item.ProductName);
                        $('#ddlUnits').val(item.Units);
                        $('#txtSpecification').val(item.Specification);
                        $('#txtRemarks').val(item.Remark);


                    });
                }
                diag.close();
            }
            diag.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    合同管理 >> 添加产品
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Contract_Modi" CurIndex="3" Param1="{Q:ContractID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td width="45">
                    <span style="margin-left: 15px;">序</span>
                </td>
                <td>
                    合同编号
                </td>
                <td width="100">
                    产品名称
                </td>
                <td width="100">
                    规格
                </td>
                <td width="160">
                    单位
                </td>
                <td width="50">
                    单价
                </td>
                <td width="50">
                    数量
                </td>
                <td width="50">
                    金额
                </td>
                <td width="190">
                    管理
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
            <tr class="">
                <td>
                    <span style="margin-left: 15px;"><%#Eval("ID") %></span>
                </td>
                <td>
                    <strong><%#Eval("ContractID") %></strong>
                </td>
                <td style="color: Blue;">
                    <%#Eval("ProductName") %>
                </td>
                <td>
                    <%#Eval("Specification") %>
                </td>
                <td>
                    <%#Eval("Units") %>
                </td>
                <td>
                    <%#Eval("Price") %>
                </td>
                <td>
                    <%#Eval("Quantity") %>
                </td>
                <td>
                    <%#Eval("Amount") %>
                </td>
                <td class="manage">
                    <asp:LinkButton ID="BtnDelete" runat="server" CommandArgument='<%#Eval("ID") %>' Text="删除" OnClientClick="return confirm('确定要删除此产品吗？')" OnCommand="btnDelete_Command"></asp:LinkButton>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <hr />
    <table class="table">
        <tbody>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 合同编号：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Literal ID="lblContractID" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品名称：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtProductName" runat="server" dataType="Require" msg="产品名称为必填项！"></asp:TextBox>
                    &nbsp;&nbsp; ╋<a href="javascript:void(0)" onclick="ViewOurProduct();">选择产品</a>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;产品规格：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:TextBox ID="txtSpecification" runat="server" Columns="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 计量单位：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlUnits" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 单价：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;<cc1:CurrencyTextBox ID="txtPrice" runat="server" dataType="Require" msg="产品单价为必填项！"></cc1:CurrencyTextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;数量：<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;<asp:TextBox ID="txtQuantity" runat="server" Columns="10" Text="0" require="true"
                        dataType="Number" msg="数量必须为数字！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    备注&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请输入 该客户的备注信息. </span>
                    <asp:TextBox TextMode="MultiLine" Columns="83" Rows="4" Style="background-image: url(/img/line.gif);
                        line-height: 22px; padding-left: 3px; background-repeat: repeat;" ID="txtRemark"
                        runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" OnClick="btnSave_Click"
                        Text="保存" OnClientClick="return Validator.Validate(this.form,3);" />
                    &nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
