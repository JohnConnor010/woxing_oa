<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="wwwroot.Manage.CTR.SupplierList" %>

    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    合同管理 >> 供应商列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Supplier" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td width="45">
                    <span style="margin-left: 15px;">序</span>
                </td>
                <td>
                    供应商名称
                </td>
                <td width="100">
                    联系人
                </td>
                <td width="100">
                    联系电话
                </td>
                <td width="260">
                    联系地址
                </td>
                <td width="80">
                    联系手机
                </td>
                <td width="150">
                    电子邮件</td>
                <td width="80">
                    管理
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
            <ItemTemplate>
            <tr class="">
                <td>
                    <span style="margin-left: 15px;"><%#Eval("SupplierID") %></span>
                </td>
                <td title='<%#Eval("CompanyName") %>' class="vtip">
                    <strong><%#Eval("CompanyName") %></strong>
                </td>
                <td style="color: Blue;">
                    <%#Eval("ContactName") %>
                </td>
                <td>
                    <%#Eval("Telephone") %>
                </td>
                <td title='<%#Eval("Address") %>' class="vtip">
                    <%#Eval("Address") %>
                </td>
                <td>
                    <%#Eval("MobilePhone") %>
                </td>
                <td>
                    <%#Eval("Email") %>
                </td>
                <td class="manage">
                    <a class="show" href='EditSupplier.aspx?SupplierID=<%#Eval("SupplierID") %>'>编辑</a> 
                    <asp:LinkButton ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("SupplierID") %>' OnCommand="btnDelete_Command" OnClientClick="return confirm('确定要删除此供应商吗？')"></asp:LinkButton>
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
            </tr>
        </tfoot>
    </table>
</asp:Content>
