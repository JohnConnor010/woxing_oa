<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="HR_DutyLogs.aspx.cs" Inherits="wwwroot.Manage.HR.HR_DutyLogs" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 人事档案 >> 档案记录
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="2" Param1="{Q:UserId}"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
<b>&nbsp;&nbsp;当前员工姓名：<asp:Literal ID="li_username" runat="server"></asp:Literal></b>
       <asp:GridView ID="Gv_tfk" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" onrowdatabound="GridView1_RowDataBound" PageSize="20" DataKeyNames="ID">
            <Columns>
                <asp:BoundField HeaderText="部门" DataField="dempName" />
                <asp:BoundField HeaderText="职务" DataField="dutyName" />
                <asp:BoundField HeaderText="级别" DataField="gradeName"  />
                <asp:BoundField HeaderText="描述" DataField="Content"  />
            <asp:TemplateField HeaderText="在职期">
                <ItemTemplate><asp:Label ID="Label3" runat="server" Text='<%# Eval("Starttime")+"|"+  Eval("stoptime")   %>'></asp:Label></ItemTemplate>
                <ItemStyle Width="100" />
            </asp:TemplateField>
                <asp:TemplateField HeaderText="开始时间">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Backtableid") + "|" + Eval("Backcolumid")+"|" + Eval("UserID")+"|"+  Eval("Starttime") %>'></asp:Label></ItemTemplate>
                <ItemStyle Width="180" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="结束时间">
                <ItemTemplate><asp:Label ID="Label2" runat="server" Text='<%# Eval("Nowtableid") + "|" + Eval("Nowcolumid")+"|" + Eval("UserID")+"|"+  Eval("stoptime")   %>'></asp:Label></ItemTemplate>
                <ItemStyle Width="180" />
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
