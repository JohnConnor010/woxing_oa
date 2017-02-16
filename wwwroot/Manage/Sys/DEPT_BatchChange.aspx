<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage3.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="DEPT_BatchChange.aspx.cs" Inherits="wwwroot.Manage.Sys.DEPT_BatchChange" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage3.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        function checkHasSel() {
            var selCount = 0;
            $(".checkdelete").each(function () {
                if ($(this).attr("checked") == true) selCount++;
            });
            if (selCount == 0) {
                alert("你没有选择记录");
                return false;
            }
            else
                return true;
        }</script>
    <link href="../Manage/css/AspnetPager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 部门管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="dept" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="note">
（注：）此功能只能在系统初始化时使用，平时请系统管理员关掉此功能！
</div>
   <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 500px;">
            请输入员工姓名进行查询：<asp:TextBox ID="tbKeyWords" runat="server" Width="100" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;<asp:LinkButton ID="LinkButton1" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="Query" Text="GO" />&nbsp;|&nbsp;<asp:LinkButton ID="LinkButton2" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="QueryAll" Text="ALL" />
        </div>
        <div style="text-align: right; float: right; width: 350px;">
            从<asp:DropDownList runat="server" ID="ddlFrom" Width="120px" 
                AutoPostBack="true" onselectedindexchanged="ddlFrom_SelectedIndexChanged">
            </asp:DropDownList>
            到
            <asp:DropDownList runat="server" ID="ddlTo" Width="120px">
            </asp:DropDownList>
            <asp:LinkButton runat="server" ID="lbDelSel" OnClick="Transfer" Font-Bold="true" ForeColor="#234323"
                OnClientClick="return checkHasSel()" Text="转移" /></div>
       <div style="clear: both;">
       </div>
   </div>
    <asp:GridView ID="GridView1" DataKeyNames="UserId" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField ItemStyle-Width="50">
                <HeaderTemplate>
                    <input class="checkall" type="checkbox" onclick='$(".checkdelete").attr("checked", $("input[class=checkall]").attr("checked"));' />
                </HeaderTemplate>
                <ItemTemplate>
                    <input name="checksel" type="checkbox" class="checkdelete" id="checksel" value='<%#Eval("UserId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserName" HeaderText="用户名" ReadOnly="true">
                <ItemStyle Width="90px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="RealName" HeaderText="员工姓名">
                <ItemStyle Width="90px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DepartmentName" HeaderText="员工部门">
                <ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="DutyName" HeaderText="员工职务">
                <ItemStyle Width="130px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="80px" DataField="Grade" HeaderText="员工级别" DataFormatString="{0} 级" />
            <asp:TemplateField>
                <ItemTemplate>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
