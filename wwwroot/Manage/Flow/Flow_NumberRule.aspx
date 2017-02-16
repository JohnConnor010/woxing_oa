<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_NumberRule.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_NumberRule" %>
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
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-other" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1"
        InsertItemPosition="FirstItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真要删除这个流水号规则吗？如果删除应用到它的OA流程流水号将不能自动编辑流水号！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="FormatLabel" runat="server" Text='<%# Eval("Format") %>' />
                </td>
                <td>
                    <asp:Label ID="AutoLengthLabel" runat="server" Text='<%# Eval("AutoLength") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Eval("AutoMode") %>'
                        Enabled="false" DataSourceID="ods_enum_NumberAutoMode" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Eval("UserMode") %>'
                        Enabled="false" DataSourceID="ods_enum_NumberUserMode" DataTextField="Text" DataValueField="Value" />
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
                    <asp:TextBox ID="FormatTextBox" runat="server" Text='<%# Bind("Format") %>' />
                </td>
                <td>
                    <asp:TextBox ID="AutoLengthTextBox" runat="server" Text='<%# Bind("AutoLength") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("AutoMode") %>'
                        DataSourceID="ods_enum_NumberAutoMode" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("UserMode") %>'
                        DataSourceID="ods_enum_NumberUserMode" DataTextField="Text" DataValueField="Value" />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
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
                    --
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="FormatTextBox" runat="server" Text='<%# Bind("Format") %>' />
                </td>
                <td>
                    <asp:TextBox ID="AutoLengthTextBox" runat="server" Text='<%# Bind("AutoLength") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Bind("AutoMode") %>'
                        DataSourceID="ods_enum_NumberAutoMode" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("UserMode") %>'
                        DataSourceID="ods_enum_NumberUserMode" DataTextField="Text" DataValueField="Value" />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真要删除这个流水号规则吗？如果删除应用到它的OA流程流水号将不能自动编辑流水号！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="FormatLabel" runat="server" Text='<%# Eval("Format") %>' />
                </td>
                <td>
                    <asp:Label ID="AutoLengthLabel" runat="server" Text='<%# Eval("AutoLength") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Eval("AutoMode") %>'
                        Enabled="false" DataSourceID="ods_enum_NumberAutoMode" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Eval("UserMode") %>'
                        Enabled="false" DataSourceID="ods_enum_NumberUserMode" DataTextField="Text" DataValueField="Value" />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="itemPlaceholderContainer" runat="server" border="0" class="table" cellpadding="3" style="">
                <tr runat="server" style="">
                    <td runat="server" style="width:50px">
                    操作
                    </td>
                    <td runat="server" style="width:30px;">
                        编号
                    </td>
                    <td runat="server" style="width:90px;">
                        名称
                    </td>
                    <td runat="server" style="width:250px;">
                        格式
                    </td>
                    <td runat="server" style="width:80px;">
                        自动变量长度
                    </td>
                    <td runat="server" style="width:90px;">
                        自动编号模式
                    </td>
                    <td runat="server" style="width:90px;">
                        用户修改模式
                    </td>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
            <div style="text-align: center;">
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
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('你真要删除这个流水号规则吗？如果删除应用到它的OA流程流水号将不能自动编辑流水号！')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="FormatLabel" runat="server" Text='<%# Eval("Format") %>' />
                </td>
                <td>
                    <asp:Label ID="AutoLengthLabel" runat="server" Text='<%# Eval("AutoLength") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server" SelectedValue='<%# Eval("AutoMode") %>'
                        Enabled="false" DataSourceID="ods_enum_NumberAutoMode" DataTextField="Text" DataValueField="Value" />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Eval("UserMode") %>'
                        Enabled="false" DataSourceID="ods_enum_NumberUserMode" DataTextField="Text" DataValueField="Value" />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [FL_NumberRules] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [FL_NumberRules] ([Name], [Format], [AutoLength], [AutoMode],[UserMode]) VALUES (@Name, @Format, @AutoLength, @AutoMode,@UserMode)" 
        SelectCommand="SELECT [Id], [Name], [Format], [AutoLength], [AutoMode], [UserMode] FROM [FL_NumberRules]" 
        UpdateCommand="UPDATE [FL_NumberRules] SET [Name] = @Name, [Format] = @Format, [AutoLength] = @AutoLength, [AutoMode] = @AutoMode, [UserMode] = @UserMode WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Format" Type="String" />
            <asp:Parameter Name="AutoLength" Type="Byte" />
            <asp:Parameter Name="AutoMode" Type="Byte" />
            <asp:Parameter Name="UserMode" Type="Byte" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Format" Type="String" />
            <asp:Parameter Name="AutoLength" Type="Byte" />
            <asp:Parameter Name="AutoMode" Type="Byte" />
            <asp:Parameter Name="UserMode" Type="Byte" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:ObjectDataSource ID="ods_enum_NumberUserMode" runat="server" SelectMethod="GetListItems_enum_NumberUserMode"
        TypeName="WX.Data.Dict" />
        <asp:ObjectDataSource ID="ods_enum_NumberAutoMode" runat="server" SelectMethod="GetListItems_enum_NumberAutoMode"
            TypeName="WX.Data.Dict" />
</asp:Content>
