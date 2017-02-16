<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Dept_CompanyWebSite.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_CompanyWebSite" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 域名管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="com" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div style=" text-align:right; padding-right:20px; font-weight:bold;"><a href="Dept_CompanyWebSiteEdit.aspx?companyID=<%=companyId %>">添加域名</a></div>
<asp:GridView ID="Gv_company" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="ID" PageSize="100"
            OnRowCommand="Gv_List_RowCommand" onrowdatabound="GridView1_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <img src="/Images/domain.gif" style="width:16px;height:16px;" alt="" />
                    </ItemTemplate>
                    <ItemStyle Width="18px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="域名" DataField="Url" />
                <asp:BoundField HeaderText="网站名称" DataField="Name" />
                <asp:BoundField HeaderText="IP" DataField="IP" />
                <asp:BoundField HeaderText="备案号" DataField="RecordNo" />
                <asp:BoundField HeaderText="上次续费时间" DataField="Feetime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField HeaderText="域名到期时间" DataField="Valid" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:BoundField HeaderText="责任人" DataField="ManageName" />
                 <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                    <%#getState(Eval("state"),Eval("Valid"),Eval("Warn")) %>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="设置">
                    <ItemTemplate>
                        <asp:LinkButton ForeColor="DarkBlue" Visible='<% #this.Master.A_Edit %>' ID="LinkButton3"
                            CommandArgument='<%# Eval("ID") %>' CommandName="editc" runat="server">修改</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="80px" CssClass="manage" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>
