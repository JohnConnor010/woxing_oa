<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="FD_UserList.aspx.cs" Inherits="wwwroot.Manage.Finance.FD_UserList" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    财务管理 >> 正式员工 >>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_User" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelDefault">
        <asp:GridView ID="Gv_intojobs" runat="server" CssClass="table tableview" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" OnRowDataBound="GridView1_RowDataBound"
            DataKeyNames="UserID" OnSorting="Gv_intojobs_Sorting">
            <Columns>
                <asp:TemplateField HeaderText="姓名">
                    <ItemTemplate>
                        <img id="Img1" alt="" runat="server" src='<% #Convert.ToBoolean(Eval("Sex"))?"/Images/User/Man_icon.gif":"/Images/User/Woman_icon.gif" %>' />
                        <%# Eval("RealName") %>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="性别" DataField="Sex" ItemStyle-Width="60">
                    <ItemStyle Width="40px"></ItemStyle>
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
                        <%# Eval("GradeName","{0}") %><%# Eval("Grade","({0})") %>
                    </ItemTemplate>
                    <ItemStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="保险">
                    <ItemTemplate>
                        <%# Eval("IsInsurance").ToString() == "1" ? "<b>有</b>" : "无"%>
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="身份证">
                    <ItemTemplate>
                    <a style=" display:<%# Eval("Annex").ToString()==""?"none":"block" %>;" href="javascript:PopupIFrame('/Manage/HR/User_CredentialsDetail.aspx?Id=<%# Eval("AnnexId") %>','查看详细','','',900,800)">查看</a>
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Literal runat="server" Visible="false" ID="liHidden_Grade"></asp:Literal>
        <asp:Literal runat="server" Visible="false" ID="liHidden_DepartmentName"></asp:Literal>
        <asp:Literal runat="server" Visible="false" ID="liHidden_DutyName"></asp:Literal>
    </div>
</asp:Content>