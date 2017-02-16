<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_Dict_Category.aspx.cs" Inherits="wwwroot.Manage.Flow.Crm_Category" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
       table.table input[type='text'],select { width:98%; }
       table.table input[type='text'],select { border:solid 1px #767676; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 表单定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-dict" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1"
        InsertItemPosition="FirstItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你确定要删除此客户分类吗！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("CategoryName") %>' />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                </td>
                <td>
                    <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("CategoryName") %>' />
                </td>
                <td>
                    &nbsp;
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
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("CategoryName") %>' />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真要删除这个客户目录吗！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("CategoryName") %>' />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="itemPlaceholderContainer" runat="server" border="0" class="table" cellpadding="3" style="">
                <tr id="Tr1" runat="server" style="">
                    <td id="Td1" runat="server" style="width:100px">
                    操作
                    </td>
                    <td id="Td2" runat="server" style="width:50px;">
                        编号
                    </td>
                    <td id="Td3" runat="server" style="width:120px;">
                        客户分类
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你确定要删除此客户分类吗！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("CategoryName") %>' />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [CRM_InnerCategory] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [CRM_InnerCategory] ([CategoryName]) VALUES (@CategoryName)" 
        SelectCommand="SELECT [Id], [CategoryName] FROM [CRM_InnerCategory]" 
        UpdateCommand="UPDATE [CRM_InnerCategory] SET [CategoryName] = @CategoryName WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
