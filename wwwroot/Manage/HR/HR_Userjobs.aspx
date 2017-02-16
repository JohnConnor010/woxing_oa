<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="HR_Userjobs.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Userjobs" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >> 员工加/撤职
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="6" Param1="{Q:UserId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
    <div style="float:left;">    
    <b style="padding-left:20px;">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></b>
    </div>
     <div style="width:100px; float:right;" class="manage">
     <a href="HR_AddUserjobs.aspx?UserID=<%=userId %>">添加职务</a>
     </div>
     <div style="clear:both"></div>
         <asp:GridView ID="Gv_tfk" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="20"
            onrowcommand="Gv_tfk_RowCommand">
            <Columns>
            <asp:TemplateField HeaderText="职务简称">
                <ItemTemplate><%# Eval("Name") %></ItemTemplate>
                <ItemStyle Width="200" />
            </asp:TemplateField>
                <asp:BoundField HeaderText="部门" DataField="DeptName" />
                <asp:BoundField HeaderText="职务分类" DataField="DutyCatagoryName" ItemStyle-Width="200" />
            <asp:TemplateField HeaderText="级别">
                <ItemTemplate>
                <img alt='<%# Eval("GradeID") %>级' src='/images/<%# Eval("GradeID") %>.jpg'/><%# Eval("GradeID")%>级
                </ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
                <asp:BoundField HeaderText="添加时间" DataField="Addtime" ItemStyle-Width="150" DataFormatString="{0:yyyy年MM月dd日 HH:mm:ss}" />
            <asp:TemplateField HeaderText="撤职">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="del" OnClientClick="return window.confirm('确定撤消此职位吗？');" CommandArgument='<%# Eval("ID") %>'>撤职</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="80" />
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
