<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewContractInfo.aspx.cs"
    Inherits="wwwroot.Manage.CTR.ViewContractInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 135px;
            height: 130px;
        }
        .style3
        {
            height: 130px;
        }
        </style>
</head>
<body id="C_News">
    <form id="form1" runat="server">
    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                    <td>
                        合同名称
                    </td>
                    <td>
                        <span id="UUserName"></span>
                        <asp:Literal ID="ltlContractName" runat="server"></asp:Literal>
                        <span id="URealName"><span id="lblCustomerName">&nbsp;</span>&nbsp;</span>
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px;">
                    合同编号：
                </th>
                <td class="style1">
                    <asp:Literal ID="ltlContractID" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    合同分类：
                </th>
                <td class="style1">
                    <asp:Literal ID="ltlCateogry" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    所属项目： 
                </th>
                <td class="style1">
                    <asp:Literal ID="ltlProject" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    合同金额：
                </th>
                <td class="style1">
                    <asp:Literal ID="ltlAmount" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    币种：
                </th>
                <td class="style1">
                    <asp:Literal ID="ltlCurrency" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    签订日期：
                </th>
                <td class="style1">
                    <p>
                        <asp:Literal ID="ltlSignedDate" runat="server"></asp:Literal>
                    </p>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    业务部门：
                </th>
                <td>
                    <asp:Literal ID="ltlDepartment" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    业务员：
                </th>
                <td>
                    <asp:Literal ID="ltlEmployee" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    支付方式：
                </th>
                <td>
                    <asp:Literal ID="ltlPaymentType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    开始日期：
                </th>
                <td>
                    <asp:Literal ID="ltlStartDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    截至日期：</th>
                <td>
                    <asp:Literal ID="ltlEndDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    收付类型：</th>
                <td>
                    <asp:Literal ID="ltlReceivablesPayment" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th class="style2">
                    合同内容：</th>
                <td class="style3">
                    <asp:TextBox ID="txtContractContent" runat="server" Height="130px" 
                        ReadOnly="True" TextMode="MultiLine" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style2">
                    合同异常处理情况：</th>
                <td class="style3">
                    <asp:TextBox ID="txtContractAbnormal" runat="server" Height="130px" 
                        ReadOnly="True" TextMode="MultiLine" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    合同甲方：</th>
                <td>
                    <asp:Literal ID="ltlPartyA" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    甲方负责人：</th>
                <td>
                    <asp:Literal ID="ltlPartyAPerson" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    合同乙方：</th>
                <td>
                    <asp:Literal ID="ltlPartyB" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    乙方负责人：</th>
                <td>
                    <asp:Literal ID="ltlPartyBPerson" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    电子合同路径：</th>
                <td>
                    <asp:Literal ID="ltlDigitPath" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    执行情况：</th>
                <td>
                    <asp:Literal ID="ltlImplementation" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    录入日期：</th>
                <td>
                    <asp:Literal ID="ltlInputDate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    经办人：</th>
                <td>
                    <asp:Literal ID="ltlManager" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td class="manage">
                    <a href="javascript:parentDialog.close()">关闭本页</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
