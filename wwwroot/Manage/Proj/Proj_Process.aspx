<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_Process.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_Process" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
       <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 
    <asp:Literal ID="Literal1" runat="server"></asp:Literal> >> 步骤管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<b>您当前操作的是：<asp:Literal ID="Literal2" runat="server"></asp:Literal> >> 步骤管理</b>
        <table class="table3" style=" line-height: 200%;">
            <tr>
                <td>
                    <asp:HiddenField ID="ui_id" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>排序：</b><asp:TextBox ID="ui_NO" Columns="2" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;&nbsp;&nbsp; 
                    <b>参与人数：</b><asp:TextBox ID="ui_Persons" Columns="5" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;&nbsp;&nbsp; 
                    <b>完成天数：</b><asp:TextBox ID="ui_Days" Columns="5" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>&nbsp;&nbsp;&nbsp; 
                    <b>占总体百分比：</b><asp:TextBox ID="ui_Percnt" Columns="5" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>%&nbsp;&nbsp;&nbsp; 
                    <b>总体时间百分比：</b><asp:TextBox ID="ui_Percnttime" Columns="5" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>%
                </td>
            </tr>
            <tr>
                <td>
                    <b>详细部署：</b><asp:TextBox ID="ui_demo" runat="server" TextMode="MultiLine" Rows="2" Columns="110"></asp:TextBox>&nbsp;&nbsp; <asp:Button ID="Button1" runat="server" Text="保存步骤" OnClick="Button1_Click" />
                </td>
            </tr>
        </table><br />
        <table class="table">
        <thead>
            <tr class="">
                <td width="80">
                    排序
                </td>
                <td>
                    参与人数
                </td>
                <td>
                    占用天数
                </td>
                <td width="100">
                    开始时间
                </td>
                <td width="100">
                    结束时间
                </td>
                <td>
                    占项目总体百分比
                </td>
                <td>
                    占时间总体百分比</td>
                <td width="80">
                    管理
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
            <ItemTemplate>
            <tr>
               <td style=" background:#eeeeee;">
                    第<%#Eval("NO") %>步
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Persons")%>
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Days")%>
                </td>
                <td style=" background:#eeeeee;">
                     <%# Eval("Starttime").ToString()!=""?Convert.ToDateTime(Eval("Starttime").ToString()).ToString("yyyy-MM-dd"):"" %>
                </td>
                <td style=" background:#eeeeee;">
                      <%# Eval("Stoptime").ToString()!=""?Convert.ToDateTime(Eval("Stoptime").ToString()).ToString("yyyy-MM-dd"):"" %>
                </td>
                <td style=" background:#eeeeee;">
                     <%#Eval("Percnt")%>%
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Percnttime")%>%</td>
                <td class="manage" style=" background:#eeeeee;">
                   <asp:LinkButton ID="LinkButton1" CommandName="edi" Visible='<%# Eval("State").ToString()=="0" %>' runat="server" Text="编辑" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" CommandName="del" Visible='<%# Eval("State").ToString()=="0" %>' runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command" OnClientClick="return confirm('删除后信息不可恢复，确定要删除吗？')"></asp:LinkButton>
                </td>
            </tr>
            <tr><td height="35"> &nbsp;&nbsp;<b>详细部署：</b></td><td colspan="6"><%#Eval("Demo")%></td></tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    </div>
</asp:Content>
