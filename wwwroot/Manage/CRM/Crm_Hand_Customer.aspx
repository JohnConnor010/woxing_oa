<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_Hand_Customer.aspx.cs" ClientIDMode="Static" Inherits="wwwroot.Manage.CRM.Crm_Hand_Customer" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
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
        function checkall() {
            var allstr = $("input[class=checkall]").attr("checked");
            if (typeof (allstr) == "undefined") {
                $(".checkdelete").attr("checked", false);
            }
            else {
                $(".checkdelete").attr("checked", true);
            }
        }
        function checkHasSel() {
            var selCount = 0;
            $(".checkdelete").each(function () {
            if ($(this).attr("checked") == "checked" || $(this).attr("checked") == true) selCount++;
            });
            if (selCount == 0) {
                alert("你没有选择记录");
                return false;
            }
            else
                return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 客户移交
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Hand" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" DataKeyNames="ID">
        <Columns>
            <asp:TemplateField ItemStyle-Width="20">
                <HeaderTemplate>
                    <input class="checkall" type="checkbox" onclick='checkall();' />
                </HeaderTemplate>
                <ItemTemplate>
                    <input name="checksel" type="checkbox" class="checkdelete" id="checksel" value='<%#Eval("ID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户编号">
                <ItemTemplate>
                    <img id="Img1" alt="" src='/Images/Customer.Gif' />
                    <%# Eval("CustomerID")%>
                </ItemTemplate>
                <ItemStyle Width="120" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="客户名称" DataField="CustomerName"></asp:BoundField>
            <asp:TemplateField HeaderText="客户分类">
                <ItemTemplate>
                    <%#String.Format("{0}{1}{2}{3}", Eval("CategoryName", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}"))%>
                </ItemTemplate>
                <ItemStyle Width="300px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="业务阶段" DataField="StageName">
                <ItemStyle Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="所属人">
                <ItemTemplate>
                    <a href='?UserID=<%#Eval("EmployeeID")%>'><%#Eval("EmployeeName")%></a>
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="管理">
                <ItemTemplate>
                    <a class="show" onclick='personView("<%#Eval("ID") %>")' href="javascript:void(0)">快速浏览</a>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table>
        <tfoot>
            <tr>
                <td>
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="badoo">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
    <b>将选中客户转交给：
        <asp:DropDownList runat="server" ID="ddlUser">
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="确认转交" 
        onclick="Button1_Click" OnClientClick="return checkHasSel()" />
    </b>
</asp:Content>
