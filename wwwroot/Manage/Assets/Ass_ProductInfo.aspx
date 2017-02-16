<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ass_ProductInfo.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_ProductInfo" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
<script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
<script type="text/javascript">
    function ViewContact(contactId) {
        var diag = new Dialog();
        diag.Width = 600;
        diag.Height = 500;
        diag.Title = "联系人详细信息";
        diag.URL = 'Crm_ShowContact.aspx?ContactID=' + contactId;
        diag.show();
    }
    </script>
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
    </style>
</head>
<body id="C_News">
    <form id="form1" runat="server">

    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                    <td>
                        产品名称
                    </td>
                    <td colspan="3">
                        <span id="UUserName"></span><span id="URealName"><span id="lblCustomerName">
                        <asp:Literal 
                            ID="lblProductName" runat="server"></asp:Literal>
                        </span>&nbsp;</span></td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px;">
                    产品编号：
                </th>
                <td class="style1">
                    <span id="UDepName">
                    <asp:Literal ID="lblProductID" runat="server"></asp:Literal>
                    </span>
                </td>
                <td class="style2" rowspan="6">
                    <asp:Image  runat="server" ID="Image1" style="height: 138px; width: 118px;border-width: 0px; border: solid 1px #999999"/>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    数量：
                </th>
                <td class="style1">
                    <span id="PositionName">
                    
                    </span>
                    <asp:Literal ID="lblQuantity" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    产品类型：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblProductType" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    单位：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblUnit" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    单价：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblPrice" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    规格：
                </th>
                <td class="style1">
                    <p>
                        <asp:Literal ID="lblSpecification" runat="server"></asp:Literal>
                    </p>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    供应商：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblSupplier" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    颜色：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblColor" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    品牌：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblBrand" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    型号：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblModel" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    备注：
                </th>
                <td colspan="3">
                    <p style="height: 61px">
                        <span id="Notes"></span>
                        
                        <asp:Literal ID="lblRemark" runat="server"></asp:Literal>
                        
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3" class="manage">
                    <a href="javascript:parentDialog.close()">关闭本页</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
