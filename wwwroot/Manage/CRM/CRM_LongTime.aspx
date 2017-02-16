<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="CRM_LongTime.aspx.cs" Inherits="wwwroot.Manage.CRM.CRM_LongTime" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

<link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
<script type="text/javascript" src="../../App_Scripts/popup.js"></script>
<script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
<script type="text/javascript">
    function personView(ud) {
        var diag = new Dialog();
        diag.Width = 585;
        diag.Height = 765;
        diag.Title = "客户资料详细信息";
        diag.URL = 'Crm_ShowCustomerInfo.aspx?CustomerID=' + ud;
        diag.show();
    }
    function addContact(customerId) {
        var diag = new Dialog();
        diag.Width = 585;
        diag.Height = 465;
        diag.Title = "客户资料详细信息";
        diag.URL = 'Crm_AddContact.aspx?CustomerID=' + customerId;
        diag.show();
    }
    <%=mes %>
</script>
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
        .Stage0 td
        {
            color: #aaa;
        }
        .Stage1 td
        {
            color: Green;
        }
        .Stage2 td
        {
            color: Red;
        }
        .Stage3 td
        {
            color: Gold;
        }
        .Stage4 td
        {
            color: Fuchsia;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
  客户管理 >> 长期未维护客户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <table class="table" cellspacing="3px">
            <thead>
                <tr class="">
                    <td>
                        客户名称
                    </td>
                    <td width="80">
                        维护人
                    </td>
                    <td width="120">
                        部门
                    </td>
                    <td width="120">
                        最后变动时间
                    </td>
                    <td width="220">
                        管理
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="CustomerRepeater" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td>
                                <img alt="" src="/Images/Customer.Gif" />
                                    <%#Eval("CustomerName")%>
                            </td>
                            <td>
                                <%#Eval("RealName")%>
                            </td>
                            <td><%#Eval("deptName")%>
                            </td>
                            <td>
                                <%#Eval("UpTime")%>
                            </td>
                            <td class="manage">
                                <a class="show" onclick='personView("<%#Eval("ID") %>")' href="javascript:void(0)">快速浏览</a>
                                <a class="show" href="Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=manager&CustomerID=<%#Eval("ID") %>">客户跟踪</a>
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
            </tfoot>
        </table>
</asp:Content>
