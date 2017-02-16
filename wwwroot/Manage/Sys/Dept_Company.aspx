<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Dept_Company.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_Company" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 公司列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="com" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="width: 98%;text-align: right; padding-bottom:2px;">
    <div style="width:200px; float:right;" class="manage">
        <asp:HyperLink runat="server" ID="liAddMotherCompany" Text="+母公司" />
        <asp:HyperLink runat="server" ID="liAddChildCompany" Text="+子公司" />
       <asp:HyperLink runat="server" ID="liAddHoldingCompany" Text="+控股公司" />
     </div>
     <div style="clear:both"></div>
    </div>
    <asp:GridView ID="Gv_company" runat="server" CssClass="table tableview" AllowPaging="True"
        AutoGenerateColumns="False" DataKeyNames="ID" PageSize="100" OnRowCommand="Gv_List_RowCommand"
        OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        &nbsp;
                    </ItemTemplate>
                    <ItemStyle Width="5px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="编号" DataField="NO" Visible="false" />
                 <asp:TemplateField HeaderText="名称" SortExpression="GradeID">
                    <ItemTemplate>
                    <img src="/Images/comp.gif" style="width:16px;height:16px;" alt="" />
                    <span style="font-weight:bold;color:Red;"><%#Eval("LinkType").ToString() == "2" ? "(母公司)" : (Eval("LinkType").ToString() == "3" ? "(子公司)" : (Eval("LinkType").ToString() == "4" ? "(控股公司)" : ""))%></span>
                    <a href='Dept_CompanyDetail.aspx?CompanyID=<%# Eval("ID") %>'><%# Eval("Name")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="类型" DataField="Ctype" />
                <asp:BoundField HeaderText="地址" DataField="Address" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton Visible='<% #this.Master.A_Edit %>' ID="LinkButton3" CommandArgument='<%# Eval("ID") %>'
                            CommandName="editc" runat="server">变更</asp:LinkButton>
                        <asp:LinkButton Visible='<% #this.Master.A_Del && GetDelVisible(Eval("LinkType")) %>' ID="LinkButton2" CommandArgument='<%# Eval("ID") %>'
                            CommandName="del" runat="server">删除</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="200px" CssClass="manage" />
                </asp:TemplateField>
            </Columns>
 </asp:GridView>
</asp:Content>
