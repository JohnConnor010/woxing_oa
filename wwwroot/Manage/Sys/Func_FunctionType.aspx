<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Func_FunctionType.aspx.cs" Inherits="wwwroot.Manage.Sys.Func_FunctionType" %><%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../Style/sys_ui.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 功能管理 >> 功能分类
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="func" CurIndex="3"  />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1"
        InsertItemPosition="FirstItem">
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                </td>
                <td>
                   <asp:Label ID="TextBox1" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("TypeName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="MoneyTextBox" runat="server" Text='<%# Bind("Money") %>' />
                </td>
                <td>
                    <asp:TextBox ID="demoTextBox" runat="server" TextMode="MultiLine" Rows="2" Columns="80" Text='<%# Bind("demo") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr>
                    <td>
                        未返回数据。
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
                </td>
                <td>
                    +
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("TypeName") %>' />
                </td>
                <td>
                    <asp:TextBox ID="MoneyTextBox" Columns="10" runat="server" Text='<%# Bind("Money") %>' />
                </td>
                <td>
                    <asp:TextBox ID="demoTextBox" TextMode="MultiLine" Rows="2" Columns="80" runat="server" Text='<%# Bind("demo") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" Visible='<% #this.Master.A_Del %>' CommandName="Delete" Text="删除" OnClientClick="return confirm('您确定要删除吗？')" />
                    <asp:Button ID="EditButton" runat="server" Visible='<% #this.Master.A_Edit %>' CommandName="Edit" Text="编辑" />
                </td>
                <td>
                   <%# Eval("ID") %>
                </td>
                <td>
                   <%# Eval("TypeName") %>
                </td>
                <td>
<%# Eval("Money").ToString() == "" || Eval("Money").ToString() == "0" ? "免费" : Eval("Money").ToString() + "元"%>
                </td>
                <td>
                   <%# Eval("demo") %>
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="itemPlaceholderContainer" runat="server" border="0" class="table" cellpadding="3" style="">
                <tr id="Tr1" runat="server" style="">
                    <td id="Td1" runat="server" style="width:100px">
                    操作
                    </td>
                    <td style="width:60px;">
                        编号
                    </td>
                    <td id="Td3" runat="server" style="width:80px;">
                       名称
                    </td>
                    <td style="width:100px;">
                        金额
                    </td>
                    <td>
                        备注
                    </td>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [TE_FunctionType] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [TE_FunctionType] ([TypeName],[Money],[demo]) VALUES (@TypeName,@Money,@demo)" 
        SelectCommand="SELECT * FROM [TE_FunctionType] order by Money asc" 
        UpdateCommand="UPDATE [TE_FunctionType] SET [TypeName] = @TypeName,[Money]=@Money,[demo]=@demo WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="TypeName" Type="String" />
            <asp:Parameter Name="Money" Type="Int32" />
            <asp:Parameter Name="demo" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="TypeName" Type="String" />
            <asp:Parameter Name="Money" Type="Int32" />
            <asp:Parameter Name="demo" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </div>
</asp:Content>
