<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_UserStatus.aspx.cs" Inherits="wwwroot.Manage.HR.HR_UserStatus" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 考勤管理 >> 员工状态
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <div style="float: left;">
        <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-kq" CurIndex="2" />
    </div>
    <div style="float: right;">
        <font color="red" id="mes" runat="server"></font>&nbsp;&nbsp;年月：<asp:TextBox ID="ui_ctime"
            runat="server" Width="80"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="搜索" OnClick="Button1_Click" /></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table width="100%">
        <tr>
            <td width="60%" valign="top">
           <h2> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本月考勤情况</h2>
                <asp:GridView ID="GridView1" runat="server" CssClass="table tableview" AutoGenerateColumns="False"
                    OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="姓名">
                            <ItemTemplate>
                                <%# Eval("RealName") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="状态" DataField="State" />
                        <asp:BoundField HeaderText="上班签到" DataField="Ontime" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField HeaderText="下班签到" DataField="Offtime" DataFormatString="{0:HH:mm:ss}" />
                        <asp:TemplateField HeaderText="其它">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Belate")+"|"+Eval("Leaveearly")+"|"+Eval("starttime")+"|"+Eval("stoptime") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="备注" DataField="demo"/>
                        <asp:BoundField HeaderText="状态时间" DataField="Addtime" DataFormatString="{0:yyyy-MM-dd}"
                            ItemStyle-Width="120" />
                    </Columns>
                </asp:GridView>
            </td>
            <td width="40%" valign="top">
             <h2> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;本月日志情况</h2>
                <asp:GridView ID="GridView2" runat="server" CssClass="table tableview" AutoGenerateColumns="False"
                    OnRowDataBound="GridView2_RowDataBound">
                    <Columns>
                        <asp:BoundField HeaderText="状态" DataField="State" />
                        <asp:BoundField HeaderText="记录" DataField="Conent" />
                        <asp:BoundField HeaderText="时间" DataField="Addtime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                            ItemStyle-Width="120" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
