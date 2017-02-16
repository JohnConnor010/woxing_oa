<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewListPage_GridView.aspx.cs" Inherits="wwwroot.NewListPage_GridView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width:100%; text-align:left; font-size:12px;"><a href="#">删除</a></div>
        <asp:GridView ID="GridView2" Width="100%" runat="server" AutoGenerateColumns="false">                
           <Columns>
              <asp:TemplateField HeaderText="">
              <HeaderTemplate>
              <asp:CheckBox runat="server" />
              </HeaderTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="id" HeaderText="编号" />
              <asp:BoundField DataField="name" HeaderText="名称" />
              <asp:BoundField DataField="tel" HeaderText="电话" />              
           </Columns>            
        </asp:GridView>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
    </div>
    </form>
</body>
</html>
