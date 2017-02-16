﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Duty_Catagory.aspx.cs" Inherits="wwwroot.Manage.Sys.Duty_Catagory" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
       table.table input[type='text'],select { width:98%; }
       table.table input[type='text'],select { border:solid 1px #767676; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
系统设置 >> 职务设置
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="duty" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1"
        InsertItemPosition="FirstItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你确定要删除此职务分类吗?')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>                    
                    <asp:DropDownList ID="DropDownList1" runat="server" Enabled="false" SelectedValue='<%# Bind("isone")%>'>
                    <asp:ListItem Value="0" Text="单职"></asp:ListItem>
                    <asp:ListItem Value="1" Text="多职"></asp:ListItem>
                    </asp:DropDownList>
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
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("isone")%>'>
                    <asp:ListItem Value="0" Text="单职"></asp:ListItem>
                    <asp:ListItem Value="1" Text="多职"></asp:ListItem>
                    </asp:DropDownList>
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
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("isone")%>'>
                    <asp:ListItem Value="0" Text="单职"></asp:ListItem>
                    <asp:ListItem Value="1" Text="多职"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真要删除这个职务分类吗?')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Enabled='false' SelectedValue='<%# Bind("isone")%>'>
                    <asp:ListItem Value="0" Text="单职"></asp:ListItem>
                    <asp:ListItem Value="1" Text="多职"></asp:ListItem>
                    </asp:DropDownList>
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
                        分类名称
                    </td>
                    <td id="Td4" runat="server" style="width:120px;">
                        是否多职
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
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你确定要删除此职务分类吗?')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Enabled="false" SelectedValue='<%# Bind("isone")%>'>
                    <asp:ListItem Value="0" Text="单职"></asp:ListItem>
                    <asp:ListItem Value="1" Text="多职"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [TE_DutyCatagory] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [TE_DutyCatagory] ([Name],isone) VALUES (@Name,@isone)" 
        SelectCommand="SELECT [Id], [Name],isone FROM [TE_DutyCatagory]" 
        UpdateCommand="UPDATE [TE_DutyCatagory] SET [Name] = @Name,isone=@isone WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="isone" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="isone" Type="Int32" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
