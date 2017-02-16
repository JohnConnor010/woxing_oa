<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Menu_List.aspx.cs" Inherits="wwwroot.Manage.Sys.Menu_List" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 菜单管理
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="menu" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table tableview" 
            runat="server" AutoGenerateColumns="False"
            OnDataBound="GridView1_DataBound" OnRowCommand="GridView1_RowCommand" 
            onrowdatabound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;序" ReadOnly="true">
                    <ItemStyle Width="60px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False" HeaderText="菜单名称">
                    <ItemTemplate>
                       <asp:Label runat="server" ID="lbl_node_level" Text='<%#Eval("node_level") %>' Visible="false"></asp:Label>
                       <%# (Eval("node_level").ToString() == "1" ? "" : (Eval("node_level").ToString() == "2" ? " ├" : "│ ├ "))%><img alt="" src='/Manage/icon/<%# Eval("icon") %>' /><%# Eval("name") %>
                    </ItemTemplate>
                        <Itemstyle Font-Bold="true"></Itemstyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="状态">
                    <ItemTemplate>
                       <asp:LinkButton ID="LinkButton2" ForeColor='<%# Eval("State").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server" Visible='<%# this.Master.A_Edit %>'
                            CausesValidation="False" CommandName="editstate" OnClientClick='return confirm("关闭后菜单将不可使用，确定要修改此菜单状态吗?");'
                            CommandArgument='<%# Eval("id")+"|"+Eval("State") %>' Text='<%# Eval("State").ToString()=="1"?"打开":"关闭" %>' />
                        <asp:Label ID="Label1" ForeColor='<%# Eval("State").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server" Visible='<%# !this.Master.A_Edit %>' Text='<%# Eval("State").ToString()=="1"?"打开":"关闭" %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="60px"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="标题" />
                <asp:TemplateField ShowHeader="False" HeaderText="页面URL">
                    <ItemTemplate>
                        <%# Eval("Url").ToString()%>
                    </ItemTemplate>
                    <ItemStyle Width="400px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False" HeaderText="管理" ItemStyle-Width="120">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" runat="server" Visible='<%# this.Master.A_Edit %>' Text="编辑" NavigateUrl='<%# String.Format("Menu_Edit.aspx?MenuId={0}",Eval("id")) %>'></asp:HyperLink>
                        <asp:LinkButton ID="LinkButton1" runat="server" Visible='<%# this.Master.A_Del %>'
                            CausesValidation="False" CommandName="del" OnClientClick="return confirm('要删除该菜单吗?');"
                            CommandArgument='<%# Eval("id") %>' Text="删除" />
                    </ItemTemplate>
                    <ItemStyle Width="80px" CssClass="manage"></ItemStyle>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
