<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_CompanysPartner.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_CompanysPartner" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 法人/股东
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="comp" CurIndex="5" Param1="{Q:companyID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- 内容模块 -->
    <table class="table tableview" width="100%">
    <caption style="text-align:left; font-style:italic;"><asp:Literal runat="server" ID="liTitle"></asp:Literal></caption>
        <thead>
            <td style="font-weight: bold; width: 10%;">
                <asp:DropDownList ID="ui_type" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="" Text="全部"></asp:ListItem>
                    <asp:ListItem Value="Legal" Text="法人"></asp:ListItem>
                    <asp:ListItem Value="Shareholder" Text="股东"></asp:ListItem>
                    <asp:ListItem Value="Directors" Text="董事会"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="font-weight: bold; width: 10%;">
                姓名
            </td>
            <td style="font-weight: bold; width: 5%;">
                性别
            </td>
            <td style="font-weight: bold; width: 8%;">
                政治面貌
            </td>
            <td style="font-weight: bold; width: 15%;">
                身份证号
            </td>
            <td style="font-weight: bold; width: 8%;">
                投资比例
            </td>
            <td style="font-weight: bold; width: 8%;">
                股份比例
            </td>
            <td style="font-weight: bold;">
                加入时间
            </td>
            <% if (this.Master.A_Edit)
               { %>
            <td style="font-weight: bold; width: 15%;">
                <a href="Dept_CompanysPartnerEdit.aspx?companyID=<%=Request["CompanyId"] %>">添加</a>
            </td>
            <%} %>
        </thead>
    </table>
    <asp:DataList ID="DataList1" runat="server" Width="100%" OnItemCommand="DataList1_ItemCommand">
        <ItemTemplate>
            <table class="table" width="100%">
                <tr>
                    <td style="width: 10%;">
                        <%# WX.Model.Company_Partner.Legalarray[Convert.ToInt32(Eval("Legal").ToString())] + "&nbsp;" + WX.Model.Company_Partner.Shareholderarray[Convert.ToInt32(Eval("Shareholder").ToString())] + "&nbsp;" + WX.Model.Company_Partner.Directorsarray[Convert.ToInt32(Eval("Directors").ToString())]%>&nbsp;
                    </td>
                    <td style="width: 10%;">
                        <b><a href="User_EditUser.aspx?id=<%# Eval("EmployeeID") %>&companyid=<%# Eval("CompanyId") %>">
                            <%# Eval("RealName")%></a></b>&nbsp;
                    </td>
                    <td style="width: 5%;">
                        <%# Eval("Sex").ToString() == "1" ? "男" : "女"%>
                    </td>
                    <td style="width: 8%;">
                        <%# Eval("PoliticalL")%>
                    </td>
                    <td style="width: 15%;">
                        <%# Eval("IDCard")%>
                    </td>
                    <td style="width: 8%;">
                        <%# Eval("Assets")%>%
                    </td>
                    <td style="width: 8%;">
                        <%# Eval("Share")%>%
                    </td>
                    <td>
                        <%# Convert.ToDateTime(Eval("Starttime").ToString()).ToString("yyyy-MM-dd")%>
                    </td>
                    <% if (this.Master.A_Edit)
                       { %>
                    <td style="width: 15%;">
                        <a href="User_EditUser.aspx?id=<%# Eval("EmployeeID") %>&companyid=<%# Eval("CompanyId") %>">
                            档案信息</a> | <a href="Dept_CompanysPartnerEdit.aspx?companyID=<%# Eval("CompanyId") %>&id=<%# Eval("Id") %>">
                                编辑</a> |
                        <asp:LinkButton ID="LinkButton1" OnClientClick="return window.confirm('删除后不可恢复，你确定要删除吗？');"
                            CommandName="del" CommandArgument='<%# Eval("Id") %>' runat="server">删除</asp:LinkButton>
                    </td>
                    <%} %>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <!-- 内容模块 -->
</asp:Content>
