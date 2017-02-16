<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_MyCheck.aspx.cs" Inherits="wwwroot.Manage.Work.Work_MyCheck" %><%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >>
    <asp:Literal ID="Literal1" runat="server" Text="经我审批"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="work_mywork" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:GridView ID="GridView1" DataKeyNames="ID" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="申请人">
                <ItemTemplate>
                    &nbsp;&nbsp;<%# Eval("RealName") %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工作名称/文号">
                <ItemTemplate>
                    <a href='Work_FillIn.aspx?RunID=<%# Eval("ID") %>'><asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="步骤名称">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("StepName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="申请时间">
                <ItemTemplate>
                    <%#Eval("BeginTime") %>
                </ItemTemplate>
                <ItemStyle Width="120px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工作状态">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# getState(Eval("Deal_Flag")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="查看流程图">
                <ItemTemplate>
                 <a href="javascript:PopupIFrame('/App_Ctrl/FlowGraphic.aspx?FlowID=<%#Eval("FlowId") %>','查看流程图',null,null,700,400)">查看流程图</a>
                </ItemTemplate>
                <ItemStyle Width="80px"></ItemStyle>
                </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="审批"
                        NavigateUrl='<% #String.Format("Run_SignForm.aspx?Run_Id={0}",Eval("ID")) %>'></asp:HyperLink>
                </ItemTemplate>
                <ItemStyle Width="60px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
