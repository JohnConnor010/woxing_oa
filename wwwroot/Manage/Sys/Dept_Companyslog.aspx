<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Dept_Companyslog.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_Companyslog" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 公司信息变更记录
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="com" CurIndex="4" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <div style="width:98%; margin:0 auto;">查看：<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    <asp:ListItem Value="" Text="全部"></asp:ListItem>
    </asp:DropDownList></div>
 <asp:GridView ID="Gv_company" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" DataKeyNames="ID" PageSize="100">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <img alt="" src="/Images/Sign.gif" style="width:16px;height:16px;" />
                    </ItemTemplate>
                    <ItemStyle Width="18px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="类型" SortExpression="GradeID">
                    <ItemTemplate>
                    <%#  WX.Model.Company.logtypearry[Convert.ToInt32(Eval("Type"))]%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="操作人" DataField="CZName" />
                <asp:BoundField HeaderText="责任人" DataField="ZRName" />
                <asp:BoundField HeaderText="详细" DataField="Content" />
                <asp:BoundField HeaderText="IP" DataField="IP" />
                <asp:BoundField HeaderText="时间" DataField="Addtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"/>
            </Columns>
        </asp:GridView>
         <table class="table">
          <tfoot>
            <tr>
                <td colspan="3">
                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="badoo">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
