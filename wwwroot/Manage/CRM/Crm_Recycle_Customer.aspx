<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Recycle_Customer.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Recycle_Customer" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 废弃中心
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Recycle" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="margin-left:10px; margin-bottom:10px;">
    请输入关键字进行查询:&nbsp;<asp:TextBox ID="txtCustomerName" Columns="20" MaxLength="10" runat="server"></asp:TextBox>&nbsp;
    <asp:DropDownList ID="ddlCustomerCategory" runat="server" Width="100">
    </asp:DropDownList>&nbsp;
    <asp:DropDownList ID="ddlCompanyNature" runat="server" Width="100">
    </asp:DropDownList>&nbsp;
    <asp:DropDownList ID="ddlSource" runat="server" Width="80">
    </asp:DropDownList>&nbsp;
    <asp:DropDownList ID="ddlIndustry" runat="server" Width="100">
    </asp:DropDownList>&nbsp;
    <asp:DropDownList ID="ddlBusinessLevel" runat="server" Width="100">
    </asp:DropDownList>&nbsp;
    <asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" />
    <asp:Button ID="btnSearchAll" runat="server" Text=" 所有 " OnClick="btnSearchAll_Click" />
    </div>
        <table class="table" cellspacing="3px">
            <thead>
                <tr class="">
                    <td width="120">
                        <span style="margin-left: 25px;">客户编号</span>
                    </td>
                    <td width="100">
                        客户名称
                    </td>
                    <td width="200">
                        客户分类
                    </td>
                    <td width="100">
                        业务阶段
                    </td>
                    <td width="70">
                        前维护人
                    </td>
                    <td width="40">
                        状态
                    </td>
                    <td width="120">
                        废弃时间
                    </td>
                    <td width="100">
                        操作
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
                                <%#String.Format("{0}{1}{2}{3}", Eval("CustomerCategory", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}"))%>
                            </td>
                            <td>
                                <%#Eval("StageName") %>
                            </td>
                            <td>
                                <%#Eval("EmployeeUser")%>
                            </td>
                            <td>
                                废弃
                            </td>
                            <td>
                                <%#Eval("UpTime")%>
                            </td>
                            <td class="manage">
                                <a class="show" onclick='personView("<%#Eval("ID") %>")' href="javascript:void(0)">快速浏览</a>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                    Text="回收" OnClientClick="return confirm('确定要回收此客户吗？')" OnCommand="btnEdit_Command"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="15">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            CssClass="badoo">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
            </tfoot>
        </table>
</asp:Content>