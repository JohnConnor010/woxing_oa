<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ass_ProductUseList.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_ProductUseList" %>
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
</head>
<body id="C_News">
    <form id="form1" runat="server">
    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                    <td>
                        使用数量
                    </td>
                    <td>
                        类型</td>
                    <td>
                        产品单价
                    </td>
                    <td>
                        使用部门
                    </td>
                    <td>
                        使用人
                    </td>
                    <td>
                        经办人
                    </td>
                    <td>
                        经办时间
                    </td>
                </tr>
            </thead>
            <tbody> 
                <asp:Repeater ID="ProductRepeater" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("Quantity")%><%#Eval("UnitName") %>
                    </td>
                    <td>
                        <%#Eval("TypeName")%>
                    </td>
                    <td>
                        <%#Eval("Price") %>
                    </td>
                    <td>
                        <%#Eval("DepartmentName")%>
                    </td>
                    <td>
                        <%#Eval("UserName")%>
                    </td>
                    <td>
                        <%#Eval("Manager")%>
                    </td>
                    <td>
                        <%#Eval("OpTime") %>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="9">
                        
<webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>


                    </td>
                </tr>
                <tr>
                    <td colspan="9" align="center">
                        <a href="javascript:parentDialog.close()">关闭窗口</a>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
   

</form>
</body>
</html>
