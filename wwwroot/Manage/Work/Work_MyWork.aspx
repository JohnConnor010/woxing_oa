<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_MyWork.aspx.cs" Inherits="wwwroot.Manage.Work.Work_MyWork" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >>
    <asp:Literal ID="Literal1" runat="server" Text="我的申请"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="work_mywork" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="流程名称">
                <ItemTemplate>
                    &nbsp;&nbsp;<%# Eval("FlowName") %>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工作名称/文号">
                <ItemTemplate>
                    <a href='Work_FillIn.aspx?RunID=<%# Eval("Id") %>'><%# Eval("Name") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="步骤名称">
                <ItemTemplate>
                    <%# Eval("PName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="工作状态">
                <ItemTemplate>
                    <%# getState(Eval("Deal_Flag")) %>
                </ItemTemplate>
                <ItemStyle Width="80px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请时间">
                <ItemTemplate>
                    <%#Eval("BeginTime") %>
                </ItemTemplate>
                <ItemStyle Width="120px"></ItemStyle>
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
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="委托"
                        NavigateUrl='<% #String.Format("Flow_Modi.aspx?id={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal1" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink2" runat="server" Visible='<% #this.Master.A_Edit %>' Text="挂起"
                        NavigateUrl='<% #String.Format("Flow_Prcs_List.aspx?ID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink3" runat="server" Visible='<% #this.Master.A_Edit %>' Text="批注"
                        NavigateUrl='<% #String.Format("Flow_Prcs_VMLList.aspx?ID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal4" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink4" runat="server" Visible='<% #this.Master.A_Edit %>' Text="导出"
                        NavigateUrl='<% #String.Format("Flow_Priv.aspx?ID={0}",Eval("Id")) %>'></asp:HyperLink>                   
                    <asp:Literal ID="Literal2" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton2" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False"
                        OnClick="Del" OnClientClick="return confirm('是否真的要删除这条流程吗？');" CommandName='<% #Eval("Id") %>'
                        Text="删除" />
                </ItemTemplate>
                <ItemStyle Width="240px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
