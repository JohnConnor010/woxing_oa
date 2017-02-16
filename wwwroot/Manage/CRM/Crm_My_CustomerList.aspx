<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_My_CustomerList.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_My_CustomerList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
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
        .Stage0 td{ color:#aaa;}
        .Stage1 td{ color:Green;}
        .Stage2 td{ color:Red;}
        .Stage3 td{ color:Gold;}
        .Stage4 td{ color:Fuchsia;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 我的客户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="MyCustomer" CurIndex="2" />
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
    <asp:DropDownList ID="ddlStage" runat="server" Width="100">
    </asp:DropDownList>&nbsp;
    <asp:Button ID="btnSearch" runat="server" Text=" 搜索 " OnClick="btnSearch_Click" />
    <asp:Button ID="btnSearchAll" runat="server" Text=" 所有 " OnClick="btnSearchAll_Click" />
    </div>
        <table class="table" cellspacing="3px">
            <thead>
                <tr class="">
                    <td width="140">
                        客户编号
                    </td>
                    <td>
                        客户名称
                    </td>
                    <td width="260">
                        客户分类
                    </td>
                    <td width="60">
                        合作分类
                    </td>
                    <td width="60">
                        业务阶段
                    </td>
                    <td width="220">
                        管理
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="CustomerRepeater" runat="server">
                    <ItemTemplate>
                        <tr class='<%#String.Format("Stage{0}",ULCode.Bind.isNull(Eval("StageId"),"0",Convert.ToString(Eval("StageId"))))%>'>
                            <td>
                                <img alt="" src="/Images/Customer.Gif" />
                                    <%#Eval("CustomerID") %>
                            </td>
                            <td>
                                <%#Eval("CustomerName") %>
                            </td>
                            <td>
                                <%#String.Format("{0}{1}{2}{3}", Eval("CustomerCategory", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}"))%>
                            </td>
                            <td>
                                <%#Eval("LevelName")%>
                            </td>
                            <td>
                                <%#Eval("StageName") %>
                            </td>
                            <td class="manage">
                                <a class="show" onclick='personView("<%#Eval("ID") %>")' href="javascript:void(0)">快速浏览</a>
                                <a class="show" href="Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=my&CustomerID=<%#Eval("ID") %>">客户跟踪</a>
                                <a class="show" href="Crm_Single_ShowCustomer.aspx?PageMode=my&CustomerID=<%#Eval("ID") %>">信息维护</a>
                                <asp:LinkButton Visible='<%#Eval("compstate").ToString()=="2"%>' ID="btnDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                    Text="申请废弃" OnClientClick="return confirm('确定要申请废弃此客户吗？')" OnCommand="btnDelete_Command"></asp:LinkButton>
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
