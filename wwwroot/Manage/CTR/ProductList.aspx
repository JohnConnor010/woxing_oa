<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="wwwroot.Manage.CTR.ProductList" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="/App_Scripts/zDialog.js"></script>
<script type="text/javascript">
    function personView(ud) {
        var diag = new Dialog();
        diag.Width = 585;
        diag.Height = 765;
        diag.Title = "产品详细信息";
        diag.URL = 'ProductShow.aspx?ProductID=' + ud;
        diag.show();
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
   销售管理 >> 产品管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Product" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div style="padding-left:20px; font-weight:bold;">部门：<asp:DropDownList ID="ProductDeptID" runat="server">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;产品分类：<asp:DropDownList ID="ddlProductCategory" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="button" Text="查询"
                    OnClick="btnSearch_Click" /></div>
 <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="产品编号">
                <ItemTemplate>
                    <img alt='<%#Eval("IsEnable").ToString()=="1"?"启用":"禁用"%>' src='/images/productenble<%# Eval("IsEnable")%>.bmp' /><%# Eval("ProductID")%>
                </ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="产品名称">
                <ItemTemplate>
                    <a class="show" onclick='personView("<%#Eval("ID") %>")' href="javascript:void(0)"><%# Eval("ProductName")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="销售价格">
                <ItemTemplate>
                    ¥<%# Eval("SalesPrice")%></ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="优惠价格">
                <ItemTemplate>
                    ¥<%# Eval("DiscountedPrice")%></ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="成本价格">
                <ItemTemplate>
                    ¥<%# Eval("CostPrice")%></ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%# Eval("IsEnable").ToString()=="1"?"启用":"禁用"%>
                </ItemTemplate>
                <ItemStyle Width="60" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门">
                <ItemTemplate>
                <%# GetDept(Eval("id").ToString()) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                [<a class="show" onclick='personView("<%#Eval("ID") %>")' href="javascript:void(0)">预览</a>] &nbsp;| &nbsp;
                    <asp:HyperLink
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑"
                        NavigateUrl='<% #String.Format("AddProduct.aspx?ProductID={0}",Eval("ID")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton3" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False"
                        OnClick="Del" OnClientClick="return confirm('确定操作吗？');" CommandArgument='<% #Eval("IsEnable").ToString()=="1"?"0":"1" %>' CommandName='<% #Eval("id") %>'
                        Text='<%# Eval("IsEnable").ToString()=="1"?"禁用":"启用"%>' />
                </ItemTemplate>
                <ItemStyle Width="120" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
