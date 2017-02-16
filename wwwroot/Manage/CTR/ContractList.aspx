<%@ Page Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true"
    CodeBehind="ContractList.aspx.cs" Inherits="wwwroot.Manage.CTR.ContractList" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
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
    <script type="text/javascript">
        function PreviewContractInfo(ud) {
            var diag = new Dialog();
            diag.Width = 700;
            diag.Height = 800;
            diag.Title = "预览合同信息";
            diag.URL = "ViewContractInfo.aspx?ContractID=" + ud;
            diag.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    合同管理 >> 合同列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Contract" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tr>
            <td>合同分类：<asp:DropDownList ID="ddlCategory" runat="server">
                </asp:DropDownList>
            &nbsp;&nbsp;所属项目：<asp:DropDownList ID="ddlProject" runat="server">
                </asp:DropDownList>
            &nbsp;&nbsp;执行情况：<asp:DropDownList ID="ddlImplementation" runat="server">
                    <asp:ListItem Value="">所有状态</asp:ListItem>
                    <asp:ListItem>未执行</asp:ListItem>
                    <asp:ListItem>执行中</asp:ListItem>
                    <asp:ListItem>终止搁置</asp:ListItem>
                    <asp:ListItem>已完成</asp:ListItem>
                </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" CssClass="toolBtnB" 
                    onclick="btnSearch_Click" Text="查询" />
            </td>
        </tr>
    </table>    
    <table class="table">
    <asp:ListView ID="ContractListView" runat="server">
    <LayoutTemplate>
        <thead>
            <tr class="">
                <td width="105">
                    <span style="margin-left: 15px;">合同编号</span>
                </td>
                <td>
                    合同名称
                </td>
                <td width="90">
                    所属项目
                </td>
                <td width="80">
                    合同分类
                </td>
                <td width="100">
                    合同金额
                </td>
                <td width="60">
                    币种
                </td>
                <td width="70">
                    签订日期
                </td>
                <td width="70">
                    结束日期
                </td>
                <td width="60">
                    支付方式
                </td>
                <td width="60">
                    执行情况
                </td>
                <td width="180">
                    管理
                </td>
            </tr>
        </thead>
        <tbody>
            <div id="ItemPlaceHolder" runat="server"></div>
        </tbody>
    </LayoutTemplate>
    <ItemTemplate>
        <tr class="">
                <td>
                    <span style="margin-left: 15px;"><%#Eval("ContractID") %></span>
                </td>
                <td class="vtip">
                    <strong><%#Eval("ContractName") %></strong>
                </td>
                <td>
                    <%#Eval("ProjectName") %>
                </td>
                <td>
                    <%#Eval("CategoryName") %>
                </td>
                <td>
                    <%#Eval("Amount") %>
                </td>
                <td>
                    <%#Eval("Currency") %>
                </td>
                <td class="vtip">
                    <%#Eval("SignedDate") %>
                </td>
                <td class="vtip">
                    <%#Eval("EndDate") %>
                </td>
                <td class="vtip">
                    <%#Eval("ReceivablesPayment")%>
                </td>
                <td>
                    <%#Eval("Implementation") %>
                </td>
                <td class="manage">
                    <a href="AddContractProduct.aspx?ContractID=<%#Eval("ID") %>">添加产品</a>
                    <a href="javascript:void(0)" onclick='PreviewContractInfo(<%#Eval("ID") %>)'>详细</a> 
                    <a class="show" href='EditContract.aspx?ContractID=<%#Eval("ID") %>'>
                        编辑</a>
                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("ID") %>' Text="删除" CssClass="delete" OnCommand="btnDelete_Command"></asp:LinkButton>
                </td>
            </tr>
    </ItemTemplate>
    </asp:ListView>
        <tfoot>
            <tr>
                <td colspan="11">
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="badoo">
                        </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>  
    </table>
</asp:Content>
