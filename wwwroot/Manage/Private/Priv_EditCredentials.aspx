<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Priv_EditCredentials.aspx.cs"
    Inherits="wwwroot.Manage.Private.Priv_EditCredentials" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript">
        function checkinput() {
            if ($("#ui_name").val() == "") {
                alert("证书名称不能为空！"); return false;
            }
            if ($("#ui_unit").val() == "") {
                alert("发证单位不能为空！"); return false;
            }
            if ($('#ui_ctime').datebox('getValue') == "") {
                alert("发证时间不能为空！"); return false;
            }
            if ($("#hid_id").val() == "" && $("#FileUpload1").val() == "") {
                alert("请选择证书电子版文件！"); return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 员工管理 >> 证书
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="priv" CurIndex="6" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table" style="line-height:180%;">
            <tr>
                <td style="width: 80px;">
                    <b>证件名称：</b>
                </td>
                <td>
                    <asp:HiddenField ID="hid_id" runat="server" />
                    <asp:TextBox ID="ui_name" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;<b>发证单位：</b>&nbsp;
                    <asp:TextBox ID="ui_unit" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;<b>发证时间：</b>&nbsp;
                    <asp:TextBox ID="ui_ctime" CssClass="easyui-datebox" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;<b>电子版：</b>&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px;">
                    <b>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注：</b>
                </td>
                <td valign="top">
                    <asp:TextBox ID="ui_content" runat="server" Width="790" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="button"
                        Text="提交保存" OnClientClick="return checkinput();" OnClick="btnSubmit_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center" style="padding:10px 10px 10px 10px;">
                    <asp:DataList ID="DataList1" BackColor="White" runat="server" Width="100%" OnItemCommand="DataList1_ItemCommand"
                        RepeatColumns="4">
                        <ItemTemplate>
                                        <a href="javascript:PopupIFrame('Priv_CredentialsDetail.aspx?Id=<%# Eval("Id") %>','查看详细','','',1000,800)"><img src="<%# Eval("Annex") %>" alt="<%# Eval("Content") %>" width="220" height="150" /></a>
                                  <br />
                                        <b><%# Eval("Name") %></b>&nbsp;[
                                        <%# ((DateTime)Eval("Ctime")).ToString("yyyy-MM-dd") %>]
                                    <br />
                                        <asp:LinkButton ID="LinkButton1" CommandName="del" CommandArgument='<%# Eval("Id") %>'
                                            runat="server">删除</asp:LinkButton>
                                        |
                                        <asp:LinkButton ID="LinkButton2" CommandName="edi" CommandArgument='<%# Eval("Id") %>'
                                            runat="server">编辑</asp:LinkButton>
                                    
                        </ItemTemplate>
                        <ItemStyle Width="250" Height="300" />
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
