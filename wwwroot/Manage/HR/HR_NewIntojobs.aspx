<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_NewIntojobs.aspx.cs" Inherits="wwwroot.Manage.HR.HR_NewIntojobs" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-new" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="note">请各部门主管面试！</div>
    <div id="PanelDefault">
        <asp:GridView ID="Gv_intojobs" runat="server" CssClass="table tableview" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000"
            DataKeyNames="UserID" onrowcommand="Gv_intojobs_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <img alt="" runat="server" src='<%#Eval("Sex").ToString()=="1"?"/Images/User/Man_icon.gif":"/Images/User/Woman_icon.gif" %>' />
                        <a href='/Manage/HR/User_Resume.aspx?CompanyID=11&UserId=<%# Eval("UserID") %>'>
                            <%# Eval("RealName") %></a>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="性别">
                    <ItemTemplate>
                    <%# Eval("Sex").ToString() == "1" ? "男" : "女"%>
                    </ItemTemplate>
                    <ItemStyle Width="40" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="出生日期" DataField="Birthday" ItemStyle-Width="120" DataFormatString="{0:yyyy年MM月dd日}">
                    <ItemStyle Width="120px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="手机号" DataField="Mobile" ItemStyle-Width="120">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="拟入职部门" DataField="DepartmentName"
                    ItemStyle-Width="140">
                    <ItemStyle Width="140px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="拟入职职位" DataField="DutyName"
                    ItemStyle-Width="140"></asp:BoundField>
                <asp:BoundField HeaderText="拟入职薪资" DataField="Salary"></asp:BoundField>
                <asp:TemplateField HeaderText="级别">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Grade","{0} 级") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="审核">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="state1" CommandArgument='<%# Eval("UserID") %>'>通过</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="state2" CommandArgument='<%# Eval("UserID") %>'>未通过</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                </Columns>
        </asp:GridView>
    </div>
</asp:Content>
