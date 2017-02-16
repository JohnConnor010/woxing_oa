<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="ReceiveEmail.aspx.cs" Inherits="wwwroot.Manage.Email.ReceiveEmail" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="easyui1.4/themes/default/easyui.css" rel="stylesheet" />
    <link href="easyui1.4/themes/icon.css" rel="stylesheet" />
    <script src="easyui1.4/jquery.min.js"></script>
    <script src="easyui1.4/jquery.easyui.min.js"></script>
    <script src="../../JS/zDialog/zDialog.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#tt").datagrid({
                onSelect: function (rowIndex, rowData) {
                    var diag = new Dialog();
                    diag.Width = 1000;
                    diag.Height = 600;
                    diag.CancelEvent = function () { diag.close(); };
                    ShowButtonRow = true;
                    diag.Title = "查看邮件内容";
                    diag.URL = "ViewEmailContent.aspx?subject=" + rowData["Subject"] + "&date=" + rowData["SendDate"];
                    diag.show();
                }
            });
            $("#reloadE").click(function () {
                $("#tt").datagrid("reload");
            })
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    邮件管理 >> 邮件内容
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="send_email" CurIndex="3" Param1="{Q:Run_Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="width: 98%;">
        <span style="float: left; font-style: italic; padding-left: 30px; font-size: +1;">
            我行技术有限公司</span>
        <span style="float: right;"><a href="javascript:;" style="font-weight: bolder;
            color: #336;" id="reloadE">刷新邮件</a></span>
    </div>
    <div style="clear: both;" />
    <div id="PanelManage" style="width: 98%; margin: 0 auto; border: 1px solid #bddbef;
        border-collapse: collapse;">
        <table id="tt" width="100%" class="easyui-datagrid" style="height: 380px"
            data-options="singleSelect:true,collapsible:true,url:'FetchEmail.ashx?UserID=<%= new WX.WXUser().UserID %>',method:'get'"  loadmsg="邮件抓取中.....，抓取时间取决于您邮箱中邮件的数量！如果抓取失败，请点击刷新邮件重新抓取。">
            <thead>
                <tr>
                    <th field="Sender" width="190" align="left">发件人</th>
                    <th field="Subject" width="780" align="left">邮件主题</th>
                    <th field="Date" width="160" align="left">日期</th>                            
                </tr>
            </thead>
        </table>
    </div>
</asp:Content>
