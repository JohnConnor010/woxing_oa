<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_SingleM_ShowCustomerBusinessUser.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_SingleM_ShowCustomerBusinessUser" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 我的客户 >> 根据客户查看业务 
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Modi-Maintain" CurIndex="4"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   <table class="table" cellspacing="3px">
            <thead>
                <tr class="">
                    <td>
                        维护人
                    </td>
                    <td width="150">
                        上月跟踪数
                    </td>
                    <td width="150">
                        本月跟踪数
                    </td>
                    <td width="100">
                        管理
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="CustomerRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("EmployeeUser")%>
                            </td>
                            <td>
                                <%#Eval("Lcount")%>
                            </td>
                            <td>
                                <%#Eval("Ncount")%>
                            </td>
                            <td class="manage">
                                <a class="show" href="Crm_SingleM_ShowCustomerBusiness.aspx?CustomerID=&UserID=<%#Eval("EmployeeID") %>">查看业务</a>
                            </td>
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
