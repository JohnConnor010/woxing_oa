<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_BusinessNorm.aspx.cs" Inherits="wwwroot.Manage.Finance.FD_BusinessNorm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
</head>
<body>
    <form id="form1" runat="server">
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
                    <asp:TextBox ID="NameTextBox1" runat="server" Text='<%# Bind("Cost") %>' Width="60" />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox2" TextMode="MultiLine" Rows="2" Columns="30" runat="server" Text='<%# Bind("Demo") %>' />
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
                    <asp:TextBox ID="NameTextBox1" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Cost") %>' Width="60" />
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="2" Columns="30" Text='<%# Bind("Demo") %>' />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('您确定要删除吗？')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel1" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel3" runat="server" Text='<%# Eval("Cost") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel2" runat="server" Text='<%# Eval("Demo") %>' />
                </td>
                <td>
                    <a href="javascript:PopupIFrame('NotifyDetail.aspx?NotifyID=<%# Eval("ID") %>','查看详细','','',600,400)">规格</a>&nbsp;
                    <a href='Calculator.aspx?Type=<%# Eval("ID") %>'>公式</a>
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
                    <td>
                        业务名称
                    </td>
                    <td>
                        成本
                    </td>
                    <td>
                        业务描述
                    </td>
                    <td style="width:80px;">
                        &nbsp;
                    </td>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [Count_TypeNorm] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [Count_TypeNorm] ([Name],[Demo],[TypeID],[Cost]) VALUES (@Name,@Demo,@TypeID,@Cost)" 
        SelectCommand="SELECT * FROM [Count_TypeNorm] where TypeID=@TypeID order by Cost asc" 
        UpdateCommand="UPDATE [Count_TypeNorm] SET [Name] = @Name,[Demo]=@Demo,Cost=@Cost WHERE [Id] = @Id">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="TypeID" 
                QueryStringField="TypeID" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Demo" Type="String" />
             <asp:QueryStringParameter DefaultValue="0" Name="TypeID" 
                QueryStringField="TypeID" Type="Int32" />
            <asp:Parameter Name="Cost" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Demo" Type="String" />
            <asp:Parameter Name="TypeID" Type="Int32" />
            <asp:Parameter Name="Cost" Type="Decimal" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
