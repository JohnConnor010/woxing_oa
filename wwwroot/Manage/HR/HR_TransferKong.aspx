<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="HR_TransferKong.aspx.cs" Inherits="wwwroot.Manage.HR.HR_TransferKong" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 人事档案 >> 调岗升职
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="5" Param1="{Q:UserID}"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
       <asp:GridView ID="Gv_tfk" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="20" DataKeyNames="ID">
            <Columns>
            <asp:TemplateField HeaderText="姓名">
                <ItemTemplate><a href='HR_AddTransferKong.aspx?UserID=<%# Eval("UserID") %>&id=<%# Eval("id") %>'><%# Eval("RealName") %></a>
                </ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
                <asp:BoundField HeaderText="原职务" DataField="backduty" />
                <asp:BoundField HeaderText="现职务" DataField="nowduty"  />
                <asp:BoundField HeaderText="类型" DataField="typename" ItemStyle-Width="100" />
                <asp:BoundField HeaderText="时间" DataField="Addtime" ItemStyle-Width="150" DataFormatString="{0:yyyy年MM月dd日 HH:mm:ss}" />
            <asp:TemplateField HeaderText="查看">
                <ItemTemplate><a href='HR_AddTransferKong.aspx?UserID=<%# Eval("UserID") %>&id=<%# Eval("id") %>'>详细</a>
                </ItemTemplate>
                <ItemStyle Width="80" />
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
