<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductShow.aspx.cs" Inherits="wwwroot.Manage.CTR.ProductShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        .contentcss
        {        	
            background-color: #eee;
        	}
    </style>
</head>
<body id="C_News">
    <form id="form1" runat="server">
    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr class="">
                    <td colspan="2">
                        产品基本信息&nbsp;
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        &nbsp;* 产品编号：
                    </th>
                    <td>
                        <div style="float: left; padding-top: 10px;">
                            &nbsp;<asp:Literal ID="txtProductID" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;</div>
                        <div style="float: left">
                            <asp:RadioButtonList ID="rIsEnable" runat="server" RepeatColumns="2">
                                <asp:ListItem Value="1" Text="启用" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        &nbsp;* 产品名称：
                    </th>
                    <td>
                        <asp:Literal ID="txtProductName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        &nbsp;* 产品分类：
                    </th>
                    <td>
                        <asp:DropDownList ID="ddlProductCategory" runat="server">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        &nbsp;产品规格：
                    </th>
                    <td>
                        <div style="float: left; width: 80px">
                            <asp:Literal ID="txtSpecification" runat="server"></asp:Literal></div>
                        &nbsp;&nbsp;&nbsp;&nbsp;<b>计量单位：</b><asp:DropDownList ID="ddlUnits" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        销售价格：
                    </th>
                    <td>
                        <div style="float: left; width: 80px">
                            <asp:Literal ID="txtSalesPrice" runat="server"></asp:Literal></div>
                        &nbsp;&nbsp;&nbsp;&nbsp;<b>优惠价格：</b><asp:Literal ID="txtDiscountedPrice" runat="server"></asp:Literal>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>成本价格：</b><asp:Literal ID="txtCostPrice"
                            runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        产品说明：
                    </th>
                    <td style="width: 460px;">
                        <asp:Literal ID="txtRemark" runat="server"></asp:Literal>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <th style="width: 80px; font-weight: bold;">
                        服务内容：
                    </th>
                    <td style="width: 460px;">
                        <asp:Literal ID="txtServices" runat="server"></asp:Literal>
                        &nbsp;
                    </td>
                </tr>
            </tbody>
            <body id="pdept" runat="server" visible="false">
                <thead>
                    <tr class="">
                        <td colspan="2">
                            产品部门信息&nbsp;
                        </td>
                    </tr>
                </thead>
                    <tr>
                        <td colspan="2">
                            <asp:DataList ID="DataList1" BackColor="White" runat="server" Style="width: 100%">
                                <HeaderTemplate>
                                                    部门 
                                                </td>
                                                <td style="width: 200px; font-weight:bold;">
                                                    月维护费用
                                                </td>
                                                <td style="width:200px; font-weight:bold;">
                                                    制作费用
                                </HeaderTemplate>
                                <HeaderStyle Font-Bold="true" />
                                <ItemTemplate>
                                                <%# Eval("DeptName")%>
                                                </td>
                                                <td style="background-color:#eee;">
                                                    <%# WX.PDT.ProductDept.FeeTypestr[Convert.ToInt32(Eval("MonthFeeType"))] + (Eval("MonthFeeType").ToString() == "0" ? "：" : "的") + Eval("MonthFee") + (Eval("MonthFeeType").ToString() == "0" ? "元" : "%")%>
                                                </td>
                                                <td style="background-color:#eee;">
                                                    <%# WX.PDT.ProductDept.FeeTypestr[Convert.ToInt32(Eval("FeeType"))] + (Eval("FeeType").ToString() == "0" ? "：" : "的") + Eval("Fee") + (Eval("FeeType").ToString() == "0" ? "元" : "%")%>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height:30px; text-indent:2em;">
                                                部门服务内容： <%# Eval("Remarks")%> 
                                </ItemTemplate>
                                <ItemStyle CssClass="contentcss" />
                            </asp:DataList>
                        </td>
                    </tr>
            </body>
        </table>
           </div>
    </form>
</body>
</html>
