<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_SingleM_ShowCustomerBusinessClient.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_SingleM_ShowCustomerBusinessClient" %>
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
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Modi-Maintain" CurIndex="3"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   <table class="table" cellspacing="3px">
            <thead>
                <tr class="">
                    <td width="140">
                        <span style="margin-left: 25px;">客户编号</span>
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td width="100">
                        最后跟踪
                    </td>
                    <td width="200">
                        客户分类
                    </td>
                    <td width="100">
                        业务阶段
                    </td>
                    <td width="60">
                        维护人
                    </td>
                    <td width="80">
                        管理
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="CustomerRepeater" runat="server">
                    <ItemTemplate>
                        <tr class='<%#String.Format("Stage{0}",ULCode.Bind.isNull(Eval("StageId"),"0",Convert.ToString(Eval("StageId"))))%>'>
                            <td>
                                <span style="margin-left: 15px;"><img alt="" src="/Images/Customer.Gif" />
                                    <%#Eval("CustomerID") %></span>
                            </td>
                            <td>
                                <%#Eval("CustomerName") %>
                            </td>
                            <td>
                            <%# GetTrackTime(Eval("ID").ToString())%>
                            </td>
                            <td>
                                <%#String.Format("{0}{1}{2}{3}", Eval("CustomerCategory", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}"))%>
                            </td>
                            <td>
                                <%#Eval("StageName") %>
                            </td>
                            <td>
                                <%#Eval("EmployeeUser")%>
                            </td>
                            <td class="manage">
                                <a class="show" href="Crm_SingleM_ShowCustomerBusiness.aspx?CustomerID=<%#Eval("ID") %>">查看业务</a>
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
