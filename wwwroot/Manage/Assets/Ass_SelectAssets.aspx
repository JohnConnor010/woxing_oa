<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ass_SelectAssets.aspx.cs"
    Inherits="wwwroot.Manage.Assets.Ass_SelectAssets" %>
      <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
    <style type="text/css">
        #norSearch, #advSearch
        {
            background: url("../../images/search_button.png") no-repeat scroll 0 0 transparent;
            height: 33px;
            margin: 3px;
            width: 107px;
        }
        input.toolBtnA, input.toolBtnB, input.toolBtnC
        {
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
    <script type="text/javascript" src="../../App_Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function CheckSelect(sender) {
            var container = document.getElementsByName("c1");
            $.each(container, function (i, item) {
                container[i].checked = false;
            });
            sender.checked = true;
            var ProductID = $('input[@name=c1]:checked').val();
            $.ajax({
                type: "get",
                cache:false,
                url: "/App_Services/GetJsonOfProductByProductID.ashx?ProductID=" + ProductID,
                success: function (result) {
                    $('#hidden_json').val(result);
                }
            });
        }
    </script>
</head>
<body id="C_News">
    <form id="form1" runat="server">
    <input type="hidden" id="hidden_json" />
    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="14">产品名称：<asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 产品类型：<asp:DropDownList
                        ID="ddlCategory" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" CssClass="toolBtnA" 
                            onclick="btnSearch_Click" Text=" 搜 索 " />
                    </td>
                </tr>
            </thead>
        </table>
        <table class="table">
            <thead>
                <tr>
                    <td>
                        产品编号
                    </td>
                    <td>
                        产品名称
                    </td>
                    <td>
                        单位
                    </td>
                    <td>
                        数量
                    </td>
                    <td>
                        已使用数量
                    </td>
                    <td>
                        单价
                    </td>
                    <td>
                        供应商
                    </td>
                    <td>
                        规格
                    </td>
                    <td>
                        颜色
                    </td>
                    <td>
                        品牌
                    </td>
                    <td>
                        型号
                    </td>
                    <td>
                        最后时间
                    </td>
                    <td>
                        图片
                    </td>
                    <td>
                        选择
                    </td>
                </tr>
            </thead>
            <tbody>
            <asp:Repeater ID="AssetsRepeater" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("ProductID") %>
                    </td>
                    <td>
                        <%#Eval("ProductName") %>
                    </td>
                    <td>
                        <%#Eval("Unit") %>
                    </td>
                    <td>
                        <%#Eval("Quantity") %>
                    </td>
                    <td>
                        <%#Eval("UsedQuantity") %>
                    </td>
                    <td>
                        <%#Eval("Price") %>
                    </td>
                    <td>
                        <%#Eval("CompanyName") %>
                    </td>
                    <td>
                        <%#Eval("Specification")%>
                    </td>
                    <td>
                        <%#Eval("Color")%>
                    </td>
                    <td>
                        <%#Eval("Brand")%>
                    </td>
                    <td>
                        <%#Eval("Model") %>
                    </td>
                    <td>
                        <%#Eval("LastTime") %>
                    </td>
                    <td>
                        <img src="<%#Eval("ProductPhoto") %>" width="24" height="24" />
                    </td>
                    <td>                        
                        <span><input type="checkbox" value='<%#Eval("ID") %>' name="c1" onclick="CheckSelect(this);" /></span>
                    </td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="14">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
