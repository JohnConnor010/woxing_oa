<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Ass_Statistics.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_Statistics" %>
     <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style4
        {
            width: 37px;
        }
        .style5
        {
            width: 61px;
        }
        .style8
        {
            width: 55px;
        }
        .style9
        {
            width: 65px;
        }
        .style10
        {
            width: 70px;
        }
        .style11
        {
            width: 95px;
        }
        .style12
        {
            width: 53px;
        }
        .style13
        {
            width: 113px;
        }
        .style14
        {
            width: 53px;
            height: 69px;
        }
        .style15
        {
            width: 95px;
            height: 69px;
        }
        .style16
        {
            width: 37px;
            height: 69px;
        }
        .style17
        {
            width: 61px;
            height: 69px;
        }
        .style18
        {
            width: 70px;
            height: 69px;
        }
        .style19
        {
            width: 65px;
            height: 69px;
        }
        .style20
        {
            width: 55px;
            height: 69px;
        }
        .style21
        {
            height: 69px;
        }
        .style22
        {
            width: 113px;
            height: 69px;
        }
        .style23
        {
            height: 11px;
        }
        .style24
        {
            height: 4px;
        }
    </style>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    资产管理 >> 资产统计分析
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="AssetsStat" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table" border="1">
        <thead>
            <tr class="">
                <td>
                    【统计选项】年：<asp:DropDownList ID="ddlYear" runat="server">
                        <asp:ListItem>所有年</asp:ListItem>
                        <asp:ListItem Selected="True">2012</asp:ListItem>
                        <asp:ListItem>2013</asp:ListItem>
                        <asp:ListItem>2014</asp:ListItem>
                    </asp:DropDownList>
&nbsp;月：<asp:DropDownList ID="ddlMonth" runat="server">
                    </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="toolBtnA" 
                        onclick="btnSearch_Click" />
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <td>
                    <table border="1" cellspacing="0" style="border-collapse: collapse;" id="ContentPlaceHolder_GridView1"
                        rules="all" width="100%">
                        <tbody>                            
                            <tr>
                                <td class="style14" rowspan="2" align="center">
                                    产品编号
                                </td>
                                <td class="style15" rowspan="2" align="center">
                                    产品名称</td>
                                <td class="style16" rowspan="2" align="center">
                                    单位
                                </td>
                                <td class="style17" rowspan="2" align="center">
                                    单价
                                </td>
                                <td class="style18" rowspan="2" align="center">
                                    规格
                                </td>
                                <td class="style19" rowspan="2" align="center">
                                    型号
                                </td>
                                <td class="style20" rowspan="2" align="center">
                                    颜色
                                </td>
                                <td class="style21" rowspan="2" align="center">
                                    品牌
                                </td>
                                <td class="style22" rowspan="2" align="center">
                                    供应商</td>
                                <td class="style24" colspan="2" align="center">
                                    入库
                                </td>
                                <td class="style24" colspan="2" align="center">
                                    领用</td>
                                <td class="style24" colspan="2" align="center">
                                    出售</td>
                                <td class="style24" colspan="2" align="center">
                                    销毁</td>
                            </tr>
                            <tr>
                                <td class="style23" align="center">
                                    数量</td>
                                <td class="style23" align="center">
                                    金额</td>
                                <td class="style23" align="center">
                                    数量</td>
                                <td class="style23" align="center">
                                    金额</td>
                                <td class="style23" align="center">
                                    数量</td>
                                <td class="style23" align="center">
                                    金额</td>
                                <td class="style23" align="center">
                                    数量</td>
                                <td class="style23" align="center">
                                    金额</td>
                            </tr>
                            <asp:Repeater ID="ProductRepeater" runat="server">
                            <ItemTemplate>                            
                            <tr>
                                <td class="style12" align="center">
                                    <%#Eval("ProductID") %>
                                </td>
                                <td class="style11" align="center">
                                    <%#Eval("ProductName") %>
                                </td>
                                <td class="style4" align="center">
                                <%#Eval("Unit") %>
                                </td>
                                <td class="style5" align="center">
                                    <%#Eval("Price") %>
                                </td>
                                <td class="style10" align="center">
                                    <%#Eval("Specification") %>
                                </td>
                                <td class="style9" align="center">
                                    <%#Eval("Model") %>
                                </td>
                                <td class="style8" align="center">
                                    <%#Eval("Color") %>
                                </td>
                                <td align="center">
                                    <%#Eval("Brand") %>
                                </td>
                                <td class="style13" align="center">
                                    <%#Eval("CompanyName") %>
                                </td>
                                <td align="center">
                                    <%#GetValue(Eval("入库数量"))%>       
                                    </td>
                                <td align="center">
                                    <%#GetValue(Eval("入库价格"))%></td>
                                <td align="center">
                                    <%#GetValue(Eval("领用数量"))%></td>
                                <td align="center">
                                    <%#GetValue(Eval("领用价格"))%></td>
                                <td align="center">
                                    <%#GetValue(Eval("出售数量")) %></td></td>
                                <td align="center">
                                    <%#GetValue(Eval("出售价格"))%></td>
                                <td align="center">
                                    <%#GetValue(Eval("销毁数量")) %></td></td>
                                <td align="center">
                                    <%#GetValue(Eval("销毁价格"))%></td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="17">
                                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged" CssClass="badoo">
                </webdiyer:AspNetPager>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
