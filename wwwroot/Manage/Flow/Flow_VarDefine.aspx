<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_VarDefine.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_VarDefine" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
       table.table input[type='text'],select { width:98%; }
       table.table input[type='text'],select { border:solid 1px #767676; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 其它定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-other" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="ID" 
        DataSourceID="SqlDataSource1" InsertItemPosition="FirstItem">
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真的删除这个变量吗？删除后用到此变量的OA流程将无法使用！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                </td>                
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Eval("Type") %>' Enabled="false" DataSourceID="ods_enum_VarType" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:Label ID="ValueLabel" runat="server" Text='<%# Eval("Value") %>' />
                </td>
                <td>
                    <asp:Label ID="Param1Label" runat="server" Text='<%# Eval("Param1") %>' />
                </td>
                <td>
                    <asp:Label ID="Param2Label" runat="server" Text='<%# Eval("Param2") %>' />
                </td>
                <td>
                    <asp:Label ID="Param3Label" runat="server" Text='<%# Eval("Param3") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                </td>
                <td>
                    <asp:Label ID="NameLabel1" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                </td>
                <td>
                     <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("Type") %>' DataSourceID="ods_enum_VarType" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Param1TextBox" runat="server" Text='<%# Bind("Param1") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Param2TextBox" runat="server" Text='<%# Bind("Param2") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Param3TextBox" runat="server" Text='<%# Bind("Param3") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server">
                <tr>
                    <td>
                        未返回数据。</td>
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
                    --
                </td>                
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TitleTextBox" runat="server" Text='<%# Bind("Title") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("Type") %>' DataSourceID="ods_enum_VarType" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Param1TextBox" runat="server" Text='<%# Bind("Param1") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Param2TextBox" runat="server" Text='<%# Bind("Param2") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Param3TextBox" runat="server" Text='<%# Bind("Param3") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真的删除这个变量吗？删除后用到此变量的OA流程将无法使用！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                </td>                
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Eval("Type") %>' Enabled="false" DataSourceID="ods_enum_VarType" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:Label ID="ValueLabel" runat="server" Text='<%# Eval("Value") %>' />
                </td>
                <td>
                    <asp:Label ID="Param1Label" runat="server" Text='<%# Eval("Param1") %>' />
                </td>
                <td>
                    <asp:Label ID="Param2Label" runat="server" Text='<%# Eval("Param2") %>' />
                </td>
                <td>
                    <asp:Label ID="Param3Label" runat="server" Text='<%# Eval("Param3") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
                        <table id="itemPlaceholderContainer" runat="server" border="1" cellpadding="3" class="table">
                            <thead>
                                <tr runat="server" style="text-align:left;">
                                    <td runat="server" style="width:50px">
                                        操作
                                    </td>
                                    <td runat="server" style="width:30px">
                                        编号
                                    </td>
                                    <td runat="server" style="width:90px">
                                        变量名
                                    </td>
                                    <td runat="server" style="width:90px">
                                        描述
                                    </td>
                                    <td runat="server" style="width:80px">
                                        变量类型
                                    </td>
                                    <td runat="server" style="width:200px">
                                        变量值
                                    </td>
                                    <td runat="server" style="width:50px">
                                        参数一
                                    </td>
                                    <td runat="server" style="width:50px">
                                        参数二
                                    </td>
                                    <td runat="server" style="width:50px">
                                        参数三
                                    </td>
                                </tr>
                            </thead>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                        <div style="text-align:center;">
                            <asp:DataPager ID="DataPager1" runat="server">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False"
                                        ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </div>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr>
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真的删除这个变量吗？删除后用到此变量的OA流程将无法使用！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                </td>                
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("Title") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Eval("Type") %>' Enabled="false" DataSourceID="ods_enum_VarType" DataTextField="Text" DataValueField="Value" />                </td>
                <td>
                    <asp:Label ID="ValueLabel" runat="server" Text='<%# Eval("Value") %>' />
                </td>
                <td>
                    <asp:Label ID="Param1Label" runat="server" Text='<%# Eval("Param1") %>' />
                </td>
                <td>
                    <asp:Label ID="Param2Label" runat="server" Text='<%# Eval("Param2") %>' />
                </td>
                <td>
                    <asp:Label ID="Param3Label" runat="server" Text='<%# Eval("Param3") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [TE_VarDefine] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [TE_VarDefine] ([Title],[Name], [Type],[Value], [Param1], [Param2], [Param3]) VALUES (@Title,@Name, @Type,@Value, @Param1, @Param2, @Param3)" 
        SelectCommand="SELECT [ID],[Name],[Title], [Type],[Value], [Param1], [Param2], [Param3] FROM [TE_VarDefine]" 
        UpdateCommand="UPDATE [TE_VarDefine] SET [Name]=@Name,[Title]=@Title,[Type] = @Type,[Value]=@Value, [Param1] = @Param1, [Param2] = @Param2, [Param3] = @Param3 WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Type" Type="Byte" />
            <asp:Parameter Name="Value" Type="String" />
            <asp:Parameter Name="Param1" Type="String" />
            <asp:Parameter Name="Param2" Type="String" />
            <asp:Parameter Name="Param3" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Type" Type="Byte" />
            <asp:Parameter Name="Value" Type="String" />
            <asp:Parameter Name="Param1" Type="String" />
            <asp:Parameter Name="Param2" Type="String" />
            <asp:Parameter Name="Param3" Type="String" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Title" Type="String" />
            <asp:Parameter Name="ID" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:ObjectDataSource ID="ods_enum_VarType" runat="server" 
        SelectMethod="GetListItems_enum_VarType" TypeName="WX.Data.Dict">
    </asp:ObjectDataSource>
</asp:Content>
