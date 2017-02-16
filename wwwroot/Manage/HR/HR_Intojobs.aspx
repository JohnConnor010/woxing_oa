<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_Intojobs.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Intojobs" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-da" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelDefault">
        <asp:GridView ID="Gv_intojobs" runat="server" CssClass="table tableview" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" OnRowDataBound="GridView1_RowDataBound"
            DataKeyNames="UserID" OnSorting="Gv_intojobs_Sorting">
            <Columns>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <img alt="" runat="server" src='<%#Eval("Sex").ToString()=="1"?"/Images/User/Man_icon.gif":"/Images/User/Woman_icon.gif" %>' />
                        <a href='<%# Eval("State").ToString()=="5"?"User_Resume":"HR_DutyLogs" %>.aspx?UserId=<%# Eval("UserID") %>'>
                            <%# Eval("RealName") %></a>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="性别" DataField="Sex" ItemStyle-Width="60">
                    <ItemStyle Width="40px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="出生日期" DataField="Birthday" ItemStyle-Width="120" DataFormatString="{0:yyyy年MM月dd日}">
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="手机号" DataField="Mobile" ItemStyle-Width="120">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="所在部门" DataField="DepartmentName" SortExpression="DepartmentName" ItemStyle-Width="200">
                    <ItemStyle Width="140px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="所属职位" DataField="DutyName" SortExpression="DutyName" ItemStyle-Width="290">
                </asp:BoundField>
                <asp:TemplateField HeaderText="级别" SortExpression="Grade">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Grade","{0} 级") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="管理" ItemStyle-CssClass="manage" ItemStyle-ForeColor="#333" DataField="State" />
            </Columns>
        </asp:GridView>
        <asp:Literal runat="server" Visible="false" ID="liHidden_Grade"></asp:Literal>
        <asp:Literal runat="server" Visible="false" ID="liHidden_DepartmentName"></asp:Literal>
        <asp:Literal runat="server" Visible="false" ID="liHidden_DutyName"></asp:Literal>
    </div>
</asp:Content>
