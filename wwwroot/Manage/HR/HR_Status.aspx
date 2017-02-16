<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_Status.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Status" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 考勤管理 >> 员工状态
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <div style="float:left;"><uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-kq" CurIndex="2" /></div>
    <div style="float:right;"><font color="red" id="mes" runat="server"></font>&nbsp;&nbsp;部门：
    <asp:DropDownList ID="ddlDepartment" runat="server" dataType="Require" require="true">
                    </asp:DropDownList>
    <asp:Button
        ID="Button1" runat="server" Text="搜索" onclick="Button1_Click" /></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <asp:GridView ID="GridView1" runat="server" CssClass="table tableview"  AutoGenerateColumns="False" onrowdatabound="GridView1_RowDataBound" >
        <Columns>
         <asp:TemplateField HeaderText="部门">
                <ItemTemplate><a title="部门日考勤" href='HR_DempStatus.aspx?DempId=<%# Eval("DepartmentID") %>'><%# Eval("DempName")%></a></ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="姓名">
                <ItemTemplate><a title="个人月考勤" href='HR_UserStatus.aspx?UserId=<%# Eval("UserID") %>'><%# Eval("RealName") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="上午状态">
                <ItemTemplate>
                <%#  Eval("Ontime").ToString() == "" ? "未签" : WX.AT.Signin.statearray[Convert.ToInt32(Eval("State"))]%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="下午状态">
                <ItemTemplate><%# Eval("Offtime").ToString()==""?"未签": WX.AT.Signin.statearray[Convert.ToInt32(Eval("NoonState"))]%></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="上班签到" DataField="Ontime" DataFormatString="{0:MM-dd HH:mm}"/>
            <asp:BoundField HeaderText="下班签到" DataField="Offtime" DataFormatString="{0:MM-dd HH:mm}"/>
            <asp:TemplateField HeaderText="描述">
                <ItemTemplate><asp:Label ID="Label2" runat="server" Text='<%# Eval("Demo") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>           
            <asp:BoundField HeaderText="状态时间" DataField="Uptime" ItemStyle-Width="120" DataFormatString="{0:MM-dd HH:mm:ss}" />
            <asp:TemplateField HeaderText="管理">
                <ItemTemplate>
                    <a href='HR_SetStatus.aspx?UserId=<%# Eval("UserID") %>'>行政执行</a>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
