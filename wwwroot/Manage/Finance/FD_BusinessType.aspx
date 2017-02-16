<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="FD_BusinessType.aspx.cs" Inherits="wwwroot.Manage.Finance.FD_BusinessType" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
 财务管理 >> 成本扣化 >> 业务类型
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_Calculator" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
 <table  border="0" class="table" cellpadding="3" style="">
 
<tr style="">
  <td id="Td1" runat="server" style="width:100px">
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" 
                        onclick="InsertButton_Click" />
      <input id="Reset1" type="reset" value="清除" />
                    </td>
                    <td style="width:60px;">
                        +
                    </td>
                    <td>
                        <asp:DropDownList ID="TypeTextBox" runat="server"
                        AutoPostBack="True" OnSelectedIndexChanged="ddl_SelectedIndexChanged"  DataTextField="Name" DataValueField="ID" />
                    </td>
                    <td style="width:200px;">
                        <asp:TextBox ID="NameTextBox" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="2" Columns="60" />
                    </td>
                    <td style="width:80px;">
                        &nbsp;
                    </td>
            </tr>
</table>
       <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" 
        onitemediting="ListView1_ItemEditing"  DataSourceID="SqlDataSource1">
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
                <asp:DropDownList ID="TypeTextBox2" runat="server" SelectedValue='<%# Bind("DivisionID") %>'
                        DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="ID" />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox3" runat="server" Text='<%# Bind("Name") %>' />
                </td>
                <td>
                    <asp:TextBox ID="NameTextBox2" TextMode="MultiLine" Rows="2" Columns="60" runat="server" Text='<%# Bind("Demo") %>' />
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
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" CommandArgument='<%# Eval("ID") %>' Visible='<% #this.Master.A_Del %>' CommandName="Delete" Text="删除" OnClientClick="return confirm('您确定要删除吗？')" />
                    <asp:Button ID="EditButton" runat="server" Visible='<% #this.Master.A_Edit %>' CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("ID") %>' />
                </td>
                <td>
                    <asp:DropDownList ID="TypeTextBox" runat="server"  SelectedValue='<%#Bind("DivisionID") %>'
                        Enabled="false" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="ID" />
                </td>
                <td>
                    <asp:Label ID="NameLabel1" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel2" runat="server" Text='<%# Eval("Demo") %>' />
                </td>
                <td>
                    <a href="javascript:PopupIFrame('FD_BusinessNorm.aspx?TypeID=<%# Eval("ID") %>','查看详细','','',800,400)">规格</a>&nbsp;
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
                        事业部
                    </td>
                    <td style="width:200px;">
                        业务名称
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
    
                    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        SelectCommand="SELECT * FROM [Count_Type] order by DivisionID asc, ID asc" 
        DeleteCommand="DELETE FROM [Count_Type] WHERE [Id] =@Id"
        UpdateCommand="UPDATE [Count_Type] SET [Name] = @Name,[Demo]=@Demo,DivisionID=@DivisionID WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Demo" Type="String" />
            <asp:Parameter Name="DivisionID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Demo" Type="String" />
            <asp:Parameter Name="DivisionID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        SelectCommand="select ID,Name from TE_Departments  where ParentID=0" >        
    </asp:SqlDataSource>
    </div>
</asp:Content>
