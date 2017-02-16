<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_ProjectState.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectState" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 项目状态
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj_state" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server"> 
 <asp:GridView ID="Gv_company" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="100" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        &nbsp;
                    </ItemTemplate>
                    <ItemStyle Width="5px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="项目名称">
                    <ItemTemplate>
                       <%#Eval("ProjectName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="当前步骤">
                    <ItemTemplate>
                       第<%#Eval("ProcID")%>步
                    </ItemTemplate>
                    <ItemStyle Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="进度">
                    <ItemTemplate>
                       <%#Eval("Percnt")%>%
                  <div style="width:205px; height:13px; float:left; background:url(/images/back.gif) no-repeat;"> <img src="/images/jindu.gif" width='<%#Eval("Percnt")%>%' height="10" /></div>
                </ItemTemplate>
                    <ItemStyle Width="240px"/>
                </asp:TemplateField>
                <asp:BoundField HeaderText="参与人员" DataField="ProjID"/>
            </Columns>
        </asp:GridView>
         <table class="table">
          <tfoot>
            <tr>
                <td colspan="3"> 
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
            CssClass="Digg">
        </webdiyer:AspNetPager>
                </td>
            </tr>
        </tfoot>
    </table>
    <div style="text-align: center; width: 98%;">
       
    </div>
</asp:Content>
