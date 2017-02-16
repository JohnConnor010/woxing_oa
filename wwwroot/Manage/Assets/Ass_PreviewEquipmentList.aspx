<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ass_PreviewEquipmentList.aspx.cs"
    Inherits="wwwroot.Manage.Assets.Ass_PreviewEquipmentList" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
</head>
<body id="C_News">
    <form runat="server" id="form1">
    <div id="PanelShow">
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
                        领用数量
                    </td>
                    <td>
                        领用时间
                    </td>
                    <td>
                        产品单价
                    </td>
                    <td>
                        计量单位
                    </td>
                    <td>
                        备注信息
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="EquipmentRepeater" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("ProductID") %>
                    </td>
                    <td>
                        <%#Eval("ProductName") %>
                    </td>
                    <td>
                        <%#Eval("Quantity") %>
                    </td>
                    <td>
                        <%#Eval("AddDate") %>
                    </td>
                    <td>
                        <%#Eval("Price") %>
                    </td>
                    <td>
                        <%#Eval("UnitName") %>
                    </td>
                    <td>
                        <%#Eval("Remark") %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="7">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="badoo">
                    </webdiyer:AspNetPager>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" align="center">
                        <a href="javascript:parentDialog.close()">关闭窗口</a>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
    </form>
</body>
</html>
