<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="WorkOrder_Add.aspx.cs" Inherits="wwwroot.Manage.WorkOrder.WorkOrder_Add" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    工作单管理 >> 下工作单
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="WorkOrder_add" CurIndex="2" Param1="{Q:xUser}" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table1">
        <tr>
            <td width="100">
                <b>任务名称：</b>
            </td>
            <td>
                <asp:TextBox ID="Title_txt" runat="server" Columns="80"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>项目：</b>
            </td>
            <td>
                <asp:DropDownList ID="Proj_drop" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="Type_drop" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateRenderMode="Inline">
                    <ContentTemplate>
                        <b>
                            <asp:DropDownList ID="otherDept" runat="server" AutoPostBack="True" OnSelectedIndexChanged="otherDept_SelectedIndexChanged">
                                <asp:ListItem Text="本部门" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="其它部门" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </b>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateRenderMode="Inline">
                    <ContentTemplate>
                        <asp:CheckBoxList ID="Dept_check" runat="server" RepeatColumns="4">
                        </asp:CheckBoxList>
                        <asp:DataList align="left" ID="DataList2" CssClass="table1" runat="server" Width="300">
                            <HeaderTemplate>
                                <thead>
                                    <td>
                                        姓名
                                    </td>
                                    <td style="width: 150px">
                                        工作量
                                    </td>
                                </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("ID")%>' />
                                <asp:CheckBox ID="CheckBox1" runat="server" ToolTip='<%#Eval("UserID")%>' Checked='<%# Eval("ID").ToString()==""?false:true%>' />
                                <b>
                                    <%# Eval("RealName")%></b> </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("cCount")%>'></asp:TextBox>
                                
                            </ItemTemplate>
                        </asp:DataList>
                    </ContentTemplate>
                   
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <b>任务要求：</b>
            </td>
            <td>
                <asp:TextBox ID="Remarks_txt" runat="server" TextMode="MultiLine" Rows="5" Columns="80"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100">
                <b>任务期限：</b>
            </td>
            <td>
                <asp:TextBox ID="YJTime_txt" runat="server" CssClass="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="text-align: center;">
        <asp:Button ID="Button1" runat="server" Text="暂时保存" OnClick="Button1_Click" />&nbsp;
        <asp:Button ID="Button2" runat="server" Text="保存并下发" OnClick="Button2_Click" /></div>
</asp:Content>
