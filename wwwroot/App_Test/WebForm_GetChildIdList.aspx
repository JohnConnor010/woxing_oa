<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_GetChildIdList.aspx.cs" Inherits="wwwroot.WebForm_GetChildIdList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="ParentID" HeaderText="ParentID" 
                    SortExpression="ParentID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
            SelectCommand="SELECT [ID], [ParentID], [Name] FROM [Ass_Category]">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
