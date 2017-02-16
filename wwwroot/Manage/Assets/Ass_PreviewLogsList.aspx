<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ass_PreviewLogsList.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_PreviewLogsList" %>

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
                        编号
                    </td>
                    <td>
                        经办人</td>
                    <td>
                        经办时间
                    </td>
                    <td>
                        归还日期
                    </td>
                    <td>
                        <%=Request.QueryString["type"] %>数量
                    </td>
                    <td>
                        产品名称</td>
                    <td>
                        产品编号
                    </td>
                    <td>
                        计量单位
                    </td>
                    <td>
                        单价</td>
                </tr>
            </thead>
            <tbody> 
                <asp:Repeater ID="LogsRepeater" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("ID") %>
                    </td>
                    <td>
                        <%#Eval("RealName") %>
                    </td>
                    <td>
                        <%#Eval("OpTime") %>
                    </td>
                    <td>
                        <%#Eval("MaturityDate")%>
                    </td>
                    <td>
                        <%#Eval("Quantity") %>
                    </td>
                    <td>
                        <%#Eval("ProductName") %>
                    </td>
                    <td>
                        <%#Eval("ProductID") %>
                    </td>
                    <td>
                        <%#Eval("UnitName") %>
                    </td>
                    <td><%#Eval("Price") %>
                        
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8">
                        
<webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>


                    </td>
                    <td>
                        
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8" align="center">
                        <a href="javascript:parentDialog.close()">关闭窗口</a>
                    </td>
                    <td align="center">
                        &nbsp;</td>
                </tr>
            </tfoot>
        </table>
    </div>
   

</form>
</body>
</html>
