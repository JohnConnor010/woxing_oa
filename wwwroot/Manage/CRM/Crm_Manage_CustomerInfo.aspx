<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Manage_CustomerInfo.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Manage_CustomerInfo" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<style type="text/css">
    caption{ text-align:left; font-style:italic;}
    a.customer{border:solid 1px #777; padding:2px 2px 2px 2px;}
</style>
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
</script></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
客户管理 >> 我的管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
    <caption>
        助手提示</caption>
       <%=tablestr%>
    </table><br/>
    <table class="table">
        <caption>
            阶段统计（目前你已经拥有正式客户<asp:Literal ID="limycount" runat="server"></asp:Literal>个）</caption>
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 150px;">
                目前有<asp:Literal ID="limyTempcount" runat="server"></asp:Literal>条信息等待审核
            </td>
            <td><asp:Literal ID="Literal1" runat="server"></asp:Literal>...
            </td>
            <td class="manage" style="width: 100px;">
                <a href="/Manage/CRM/Crm_Check_CheckCustomer.aspx">审核</a>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td>
                沟通阶段的客户<asp:Literal ID="Literal3" runat="server"></asp:Literal>个。
            </td>
            <td><asp:Literal ID="Literal4" runat="server"></asp:Literal>...
            </td>
            <td class="manage" style="width: 100px;">
                <a href="Crm_Manage_CustomerList.aspx?StageID=1">维护</a>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td>
                跟踪阶段的客户<asp:Literal ID="Literal6" runat="server"></asp:Literal>个。
            </td>
            <td><asp:Literal ID="Literal7" runat="server"></asp:Literal>...
            </td>
            <td class="manage" style="width: 100px;">
                <a href="Crm_Manage_CustomerList.aspx?StageID=2">维护</a>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td>
                签约阶段的客户<asp:Literal ID="Literal9" runat="server"></asp:Literal>个。
            </td>
            <td><asp:Literal ID="Literal10" runat="server"></asp:Literal>...
            </td>
            <td class="manage" style="width: 100px;">
                <a href="Crm_Manage_CustomerList.aspx?StageID=3">维护</a>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td>
                维护阶段的客户<asp:Literal ID="Literal12" runat="server"></asp:Literal>个。
            </td>
            <td><asp:Literal ID="Literal13" runat="server"></asp:Literal>...
            </td>
            <td class="manage" style="width: 100px;">
                <a href="Crm_Manage_CustomerList.aspx?StageID=4">维护</a>
            </td>
        </tr>
    </table>
    <br />
    <table class="table">
        <caption>
            其它统计</caption>
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                本月统计：
            </td>
            <td>
                总公司，发展新客户<asp:Literal ID="Literal15" runat="server"></asp:Literal>个，签约<asp:Literal ID="Literal18" runat="server"></asp:Literal>个，对<asp:Literal ID="Literal21" runat="server"></asp:Literal>个客户跟踪<asp:Literal ID="Literal24" runat="server"></asp:Literal>次，客户维护费<asp:Literal ID="Literal27" runat="server"></asp:Literal>元。
                <br/>
                <div id="monthdiv" runat="server">你部门，发展新客户<asp:Literal ID="Literal16" runat="server"></asp:Literal>个，签约<asp:Literal ID="Literal19" runat="server"></asp:Literal>个，对<asp:Literal ID="Literal22" runat="server"></asp:Literal>个客户跟踪<asp:Literal ID="Literal25" runat="server"></asp:Literal>次，客户维护费<asp:Literal ID="Literal28" runat="server"></asp:Literal>元。</div>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                本年统计：
            </td>
            <td>
                总公司，发展新客户<asp:Literal ID="Literal30" runat="server"></asp:Literal>个，签约<asp:Literal ID="Literal31" runat="server"></asp:Literal>个，对<asp:Literal ID="Literal32" runat="server"></asp:Literal>个客户跟踪<asp:Literal ID="Literal33" runat="server"></asp:Literal>次，客户维护费<asp:Literal ID="Literal34" runat="server"></asp:Literal>元。
                <br/>
                <div id="yeardiv" runat="server">你部门，发展新客户<asp:Literal ID="Literal35" runat="server"></asp:Literal>个，签约<asp:Literal ID="Literal36" runat="server"></asp:Literal>个，对<asp:Literal ID="Literal37" runat="server"></asp:Literal>个客户跟踪<asp:Literal ID="Literal38" runat="server"></asp:Literal>次，客户维护费<asp:Literal ID="Literal39" runat="server"></asp:Literal>元。</div>
            </td>
        </tr>        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                客户内部分类：
            </td>
            <td>
                <asp:Literal ID="Literal45" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                客户性质分类：
            </td>
            <td>
                <asp:Literal ID="Literal46" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                客户来源分类：
            </td>
            <td>
                <asp:Literal ID="Literal47" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                客户合作分类：
            </td>
            <td>
                <asp:Literal ID="Literal48" runat="server"></asp:Literal>
            </td>
        </tr>    
        <tr>
            <th style="width: 30px;">
            </th>
            <td style="width: 100px;">
                客户行业分类：
            </td>
            <td>
                <asp:Literal ID="Literal49" runat="server"></asp:Literal>
            </td>
        </tr>    
        </table>
</asp:Content>