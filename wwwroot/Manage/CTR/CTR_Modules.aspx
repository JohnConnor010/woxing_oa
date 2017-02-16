<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="CTR_Modules.aspx.cs" Inherits="wwwroot.Manage.CTR.CTR_Modules" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
销售管理 >> 模板管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Temp" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList>
<asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="10000">
        <Columns>
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <%#Eval("Title")%>
                </ItemTemplate>
                <ItemStyle/>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="分类">
                <ItemTemplate>
                    <%#WX.CRM.Customer_Temp.TypeStr[Convert.ToInt32(Eval("Type").ToString())]%>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建人">
                <ItemTemplate>                    
                    <%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间">
                <ItemTemplate>
                    <%#Eval("Addtime").ToString() != "" ? Convert.ToDateTime(Eval("Addtime")).ToString("yyyy-MM-dd HH:ss:mm") : ""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href='AddTemp.aspx?TempID=<%# Eval("id") %>'>编辑</a>
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
        CssClass="badoo">
    </webdiyer:AspNetPager>
</asp:Content>
