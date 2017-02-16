<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectOurProduct.aspx.cs"
    Inherits="wwwroot.Manage.CTR.SelectOurProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
    <link type="text/css" rel="Stylesheet" href="../css/RepeaterTable.css" />
    <script type="text/javascript" src="../../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        function SetTableColor(TableID) {
            var clickClass = "";        //点击样式名
            var moveClass = "";         //鼠标经过样式名
            var clickTR = null;         //点击的行
            var moveTR = null;          //鼠标经过行
            var Ptr = document.getElementById(TableID).getElementsByTagName("tr");
            for (i = 1; i < Ptr.length + 1; i++) {
                Ptr[i - 1].className = (i % 2 > 0) ? "Rep_Tab_EvenTr" : "Rep_Tab_OddTr";
            }
            //设置鼠标的动作事件
            for (var i = 1; i < Ptr.length; i++) {
                var Owner = Ptr[i].item;
                //鼠标经过事件
                Ptr[i].onmouseover = function Move() {
                    if (clickTR != this) {
                        if (moveTR != this) {
                            moveClass = this.className;
                            moveTR = this;
                            this.className = "Rep_Tr_Move";
                        }
                    }
                }
                //鼠标离开事件
                Ptr[i].onmouseout = function Out() {
                    if (clickTR != this) {
                        moveTR = null;
                        this.className = moveClass;
                    }
                }
                //鼠标单击事件
                Ptr[i].onclick = function Ck() {
                    if (clickTR != this) {
                        if (clickTR) {
                            clickTR.className = clickClass;
                        }
                        clickTR = this;
                        clickClass = moveClass;
                    }
                    this.className = "Rep_Tr_Click";
                }
            }
        }    
    </script>    
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
                cache: false,
                url: "/App_Services/GetContractProductByID.ashx?ID=" + ProductID,
                success: function (result) {
                    $('#hidden_json').val(result);
                }
            });
        }
    </script>
</head>
<body id="C_News" onload="SetTableColor('table')">
    <form id="form1" runat="server">
    <input type="hidden" id="hidden_json" />
    <div id="PanelShow">
        <table class="Rep_tab" id="table">
            <thead>
                <tr>
                    <td>
                        编号
                    </td>
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
                        规格
                    </td>
                    <td>
                        销售单价
                    </td>
                    <td>
                        优惠价格
                    </td>
                    <td>
                        成本价格
                    </td>
                    <td>
                        产品类别
                    </td>
                    <td>
                        备注
                    </td>
                    <td>
                        选择
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="ProductRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("ID") %>
                            </td>
                            <td>
                                <%#Eval("ProductID") %>
                            </td>
                            <td>
                                <%#Eval("ProductName") %>
                            </td>
                            <td>
                                <%#Eval("Units") %>
                            </td>
                            <td>
                                <%#Eval("Specification") %>
                            </td>
                            <td>
                                <%#Eval("SalesPrice") %>
                            </td>
                            <td>
                                <%#Eval("DiscountedPrice") %>
                            </td>
                            <td>
                                <%#Eval("CostPrice") %>
                            </td>
                            <td>
                                <%#Eval("Remark") %>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <span>
                                    <input type="checkbox" value='<%#Eval("ID") %>' name="c1" onclick="CheckSelect(this);" /></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
