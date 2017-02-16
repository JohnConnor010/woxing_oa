<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_ProjectUser.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectUser" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 用户状态
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj_state" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server"> 
 <asp:GridView ID="Gv_company" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="UserID" PageSize="100" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        &nbsp;
                    </ItemTemplate>
                    <ItemStyle Width="5px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="所在部门" DataField="DepartmentName" ItemStyle-Width="100" />
                <asp:BoundField HeaderText="员工姓名" DataField="RealName" ItemStyle-Width="100" />
                <asp:BoundField HeaderText="职位" DataField="DutyName" ItemStyle-Width="200" />
                <asp:BoundField HeaderText="当前项目" DataField="" ItemStyle-CssClass="manage" />
            </Columns>
        </asp:GridView>
</asp:Content>
