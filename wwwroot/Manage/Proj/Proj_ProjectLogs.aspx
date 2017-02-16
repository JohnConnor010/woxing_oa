<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_ProjectLogs.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectLogs" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 项目日志
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj_log" CurIndex="1" />
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
                        &nbsp;
                    </ItemTemplate>
                    <ItemStyle Width="5px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="项目名称" DataField="ProjectName" />
                 <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                    <%#  WX.PRO.Log.logtype[Convert.ToInt32(Eval("Type"))]%>
                    </ItemTemplate>
                </asp:TemplateField>
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
