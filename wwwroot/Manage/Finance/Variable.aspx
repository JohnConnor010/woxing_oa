<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Variable.aspx.cs" Inherits="wwwroot.Manage.Finance.Variable" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
 财务管理 >> 成员变量 >>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_Calculator" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
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
                    <asp:TextBox ID="NameTextBox3" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox2" runat="server" Text='<%# Bind("EnName") %>' />
                </td>
                <td>
                   
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("Type")%>'>
                    <asp:ListItem Value="0" Text="变量"></asp:ListItem>
                    <asp:ListItem Value="1" Text="常量"></asp:ListItem>
                    <asp:ListItem Value="2" Text="区间变量"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox4" runat="server" Text='<%# Bind("VarValue") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Width="40" Text='<%# Bind("Suffix") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server" Width="300" Text='<%# Bind("Demo") %>' />
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
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EnName") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server"  SelectedValue='<%# Bind("Type")%>' >
                    <asp:ListItem Value="0" Text="变量"></asp:ListItem>
                    <asp:ListItem Value="1" Text="常量"></asp:ListItem>
                    <asp:ListItem Value="2" Text="区间变量"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("VarValue") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="40" Text='<%# Bind("Suffix") %>' />
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="300" Text='<%# Bind("Demo") %>' />
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
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel1" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel2" runat="server" Text='<%# Eval("EnName") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel3" runat="server" Text='<%# Eval("Type").ToString()=="0"?"变量":(Eval("Type").ToString()=="1"?"常量":"区间变量") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel4" runat="server" Text='<%# Eval("VarValue") %>' />
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Suffix") %>' />
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Demo") %>' />
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
                    <td style="width:80px;">
                        中文名称
                    </td>
                    <td style="width:80px;">
                        英文名称
                    </td>
                    <td style="width:80px;">
                        类型
                    </td>
                    <td>
                        值
                    </td>
                    <td style="width:80px;">
                        后辍
                    </td>
                    <td>
                        其它描述
                    </td>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [FD_Variable] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [FD_Variable] ([Name],[EnName],[Type],[VarValue],[Suffix],[Demo]) VALUES (@Name,@EnName,@Type,@VarValue,@Suffix,@Demo);" 
        SelectCommand="SELECT * FROM [FD_Variable] order by ID asc" 
        UpdateCommand="UPDATE [FD_Variable] SET [Name] = @Name,[EnName]=@EnName,[Type]=@Type,[VarValue]=@VarValue,[Suffix]=@Suffix,[Demo]=@Demo WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="EnName" Type="String" />
            <asp:Parameter Name="Type" Type="Int32" />
            <asp:Parameter Name="VarValue" Type="String" />
            <asp:Parameter Name="Suffix" Type="String" />
            <asp:Parameter Name="Demo" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="EnName" Type="String" />
            <asp:Parameter Name="Type" Type="Int32" />
            <asp:Parameter Name="VarValue" Type="String" />
            <asp:Parameter Name="Suffix" Type="String" />
            <asp:Parameter Name="Demo" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </div>
</asp:Content>
