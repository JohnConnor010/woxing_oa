<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_List.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_List" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../Manage/css/AspnetPager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 500px;">
            请选择流程类型：<asp:DropDownList runat="server" id="ddlType" Width="100"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;关键字： <asp:TextBox ID="tbKeyWords" runat="server" Width="150" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;<asp:LinkButton ID="LinkButton1" Font-Bold="true"
                    ForeColor="#234323" runat="server" OnClick="Query" Text="GO" />
        </div>
        <div style="text-align: right; float: right; width: 200px;">
            </div>
        <div style="clear: both;">
        </div>
    </div>
    <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="编号" Visible="false" ItemStyle-Width="40" ReadOnly="true">
                <ItemStyle Width="40px"></ItemStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <img alt="" src="/images/form/flow.gif" />
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Type" HeaderText="类型" Visible="false" ItemStyle-Width="130">
                <ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Dept" HeaderText="部门" Visible="false" />
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑"
                        NavigateUrl='<% #String.Format("Flow_Modi.aspx?FlowID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal1" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink2" runat="server" Visible='<% #this.Master.A_Edit %>' Text="流程设计(列表)"
                        NavigateUrl='<% #String.Format("Flow_Prcs_List.aspx?FlowID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink3" runat="server" Visible='<% #this.Master.A_Edit %>' Text="流程设计(图形)"
                        NavigateUrl='<% #String.Format("Flow_Prcs_VMLList.aspx?FlowID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal4" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink4" runat="server" Visible='<% #this.Master.A_Edit %>' Text="管理权限"
                        NavigateUrl='<% #String.Format("Flow_Priv.aspx?FlowID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal5" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:HyperLink
                        ID="HyperLink5" runat="server" Visible='<% #this.Master.A_Edit %>' Text="定时设置"
                        NavigateUrl='<% #String.Format("Flow_Timer.aspx?FlowID={0}",Eval("Id")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal2" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton2" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False"
                        OnClick="Del" OnClientClick="return confirm('是否真的要删除这条流程吗？');" CommandName='<% #Eval("Id") %>'
                        Text="删除" />
                </ItemTemplate>
                <ItemStyle Width="450px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
