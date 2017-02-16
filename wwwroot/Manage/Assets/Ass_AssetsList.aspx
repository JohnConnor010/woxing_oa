<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_AssetsList.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_AssetsList" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <style type="text/css">
        table select{border:solid 1px #777;}
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
    <script type="text/javascript">
        function PreviewUsedList(ud) {
            var diag = new Dialog();
            diag.Width = 800;
            diag.Height = 400;
            diag.Title = "物品使用记录";
            diag.URL = 'Ass_ProductUseList.aspx?ProductID=' + ud;
            diag.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    资产管理 >> 物品仓库查询
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="AssetsList" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr>
                <td>物品类别：<asp:DropDownList ID="ddlCategory" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;单价范围：
                 
                    <asp:DropDownList ID="ddlPriceScope" runat="server">
                    </asp:DropDownList>
                &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜索物品" CssClass="toolBtnC" 
                        onclick="btnSearch_Click" />
                </td>
            </tr>
        </thead>
    </table>
    <table class="table">
        <thead>
            <tr class="">
                <th width="100">
                    产品编号</th>
                <td width="90">
                    产品名称</td>
                <td width="80">
                    类别
                </td>
                <td width="100">
                    已使用数量
                </td>
                <td width="55">
                    单价
                </td>
                <td width="100">
                    &nbsp;规格</td>
                <td width="180">
                    操作</td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="WarehouseRepeater" runat="server">
            <ItemTemplate>
                <tr class="">
                    <td>
                        <img src='<%#Eval("ProductPhoto") %>' alt="" width="28" height="28" />&nbsp;&nbsp;<%#Eval("ProductID") %>
                    </td>
                    <td>
                        <%#Eval("ProductName") %>
                    </td>
                    <td>
                        <%#Eval("CategoryName") %>
                    </td>
                    <td>
                        <%#Eval("Quantity") %><%#Eval("Unit") %>&nbsp;<span style="font-weight: bold; font-size: 16px;">/</span>&nbsp;<%#Eval("UsedQuantity")%><%#Eval("Unit") %>
                    </td>
                    <td>
                        <%#Eval("Price") %>
                    </td>
                    <td>
                        <%#Eval("Specification")%>
                    </td>
                    <td class="manage">
                        <a href="javascript:void(0)" onclick='PreviewUsedList("<%#Eval("ProductID") %>")'>使用记录</a>
                        <a href="Ass_EditAssets.aspx?WarehouseID=<%#Eval("ID") %>">修改</a>
                        <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>'
                            OnCommand="btnDelete_Command" OnClientClick="return confirm('确定要删除这个产品？')"></asp:LinkButton>
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
</asp:Content>
