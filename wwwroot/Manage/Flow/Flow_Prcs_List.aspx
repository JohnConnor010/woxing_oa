<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_Prcs_List.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_List" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-modi" CurIndex="5"  Param1="{Q:FlowID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="text-align:right;width:98%; font-weight:bold;"><a href="Flow_Prcs_New.aspx?id=<%=Request.QueryString["FlowId"] %>&index=5">新建步骤</a></div>
    <asp:GridView ID="GridView1" DataKeyNames="Id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:BoundField DataField="StepNo" HeaderText="步骤" ItemStyle-Width="40" ReadOnly="true">
                <ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Name" HeaderText="名称" ItemStyle-Width="90">
                <ItemStyle Width="90px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Next_Nodes" HeaderText="下一步" ItemStyle-Width="130">
                <ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="基本属性" NavigateUrl='<% #GetUrl("flow_prcs_modi.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal2" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink2" runat="server" Visible='<% #this.Master.A_Edit %>' Text="经办权限" NavigateUrl='<% #GetUrl("Flow_Prcs_Priv.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal7" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink7" runat="server" Visible='<% #this.Master.A_Edit %>' Text="经办设置" NavigateUrl='<% #GetUrl("Flow_Prcs_OpSet.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink3" runat="server" Visible='<% #this.Master.A_Edit %>' Text="可写字段" NavigateUrl='<% #GetUrl("Flow_Prcs_EditableFlds.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal4" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink4" runat="server" Visible='<% #this.Master.A_Edit %>' Text="隐藏字段" NavigateUrl='<% #GetUrl("Flow_Prcs_HiddenFlds.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal5" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink5" runat="server" Visible='<% #this.Master.A_Edit %>' Text="进出条件" NavigateUrl='<% #GetUrl("Flow_Prcs_Conditions.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal6" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink6" runat="server" Visible='<% #this.Master.A_Edit %>' Text="输入输出插件" NavigateUrl='<% #GetUrl("Flow_Prcs_Plugs.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal8" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink ID="HyperLink8" runat="server" Visible='<% #this.Master.A_Edit %>' Text="流转设置" NavigateUrl='<% #GetUrl("Flow_Prcs_PassSet.aspx",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal1" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton ID="LinkButton1" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False" OnClick="Del"
                        OnClientClick="return confirm('是否真的要删除这条记录？');" CommandName='<% #Eval("Id") %>'
                        Text="删除" />
                </ItemTemplate>               
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
