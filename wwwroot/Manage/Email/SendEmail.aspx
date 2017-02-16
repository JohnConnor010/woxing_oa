<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" ClientIDMode="Static" CodeBehind="SendEmail.aspx.cs" Inherits="wwwroot.Manage.Email.SendEmail" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script src="easyui1.4/jquery.min.js"></script>
    <script src="easyui1.4/jquery.easyui.min.js"></script>
    <link href="easyui1.4/themes/default/easyui.css" rel="stylesheet" />
    <link href="easyui1.4/themes/icon.css" rel="stylesheet" />
    <script src="kindeditor-4.1.10/kindeditor-min.js"></script>
    <link href="kindeditor-4.1.10/themes/default/default.css" rel="stylesheet" />
    <script type="text/javascript">
        
        KindEditor.ready(function (K) {
            window.editor = K.create('#txtContent');

            K('#insertfile').click(function () {
                editor.loadPlugin('insertfile', function () {
                    editor.plugin.fileDialog({
                        fileUrl: K('#url').val(),
                        clickFn: function (url, title) {
                            //K('#url').val(url);
                            var random = Math.ceil(Math.random() * 1000);
                            $("#lst_files").append("<li id='" + random + "'><label>" + url + "</label>&nbsp;&nbsp;<a href='javascript:confirmDelete(" + random + ");' style='color:red'>删除</a></li>");
                            editor.hideDialog();
                        }
                    });
                });
            });
        });
        $(function () {
            $("#chkCC").click(function () {
                if ($(this).is(":checked")) {
                    $("#showCC").show();
                } else {
                    $("#showCC").hide();
                }
            });
            $("#btnSend").click(function () {
                if ($("#combobox1").combo("getValue") == "") {
                    $.messager.alert("提示信息", "请选择邮件接收人！");
                    return false;
                }
                if ($("#chkCC").is(":checked")) {
                    if ($("#combobox2").combo("getValues") == "") {
                        $.messager.alert("提示信息", "请选择邮件抄送人！");
                        return false;
                    }
                }
                if ($("#txtSubject").val() == "") {
                    $.messager.alert("提示信息", "请输入邮件主题！");
                    return false;
                }
                if (window.editor.html() == "") {
                    $.messager.alert("提示信息", "请输入邮件内容！");
                    return false;
                }
                var length = $("#lst_files li").length;
                var filesArray = new Array();
                if (length > 0) {
                    $("#lst_files li").each(function (index, item) {
                        var file = $(this).text().replace("删除","");
                        filesArray.push(file);
                    })
                    $("#hideFiles").val(filesArray.toString());
                }
            })

        });
        function confirmDelete(random) {
            $("#" + random).remove();
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    邮件管理 >> 发送邮件
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="send_email" CurIndex="1" Param1="{Q:Run_Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <input type="hidden" id="hideFiles" runat="server" />
    <table class="table1">
        <tr>
            <td width="100">
                <b>收件人：</b>
            </td>
            <td>
                <select class="easyui-combogrid" id="combobox1" runat="server" style="width: 250px" data-options="
			panelWidth: 250,
			idField: 'Email',
			textField: 'UserName',
			url: 'GetUserEmail.ashx',
			method: 'get',
			columns: [[
				{field:'UserName',title:'用户名',width:30},
				{field:'Email',title:'电子邮件',width:90}
			]],
			fitColumns: true
		">
                </select>
                &nbsp;&nbsp;
                <asp:CheckBox ID="chkCC" runat="server" Text="抄送" />
            </td>
        </tr>
        <tr id="showCC" style="display:none">
            <td><b>抄送人：</b></td>
            <td>
                <select class="easyui-combogrid" id="combobox2" runat="server" style="width: 250px" data-options="
			panelWidth: 250,
            multiple: true,
			idField: 'Email',
			textField: 'UserName',
			url: 'GetUserEmail.ashx',
			method: 'get',
			columns: [[
				{field:'UserName',title:'用户名',width:30},
				{field:'Email',title:'电子邮件',width:90}
			]],
			fitColumns: true
		">
                </select>
            </td>
        </tr>
        <tr>
            <td>
                <b>主题：</b>
            </td>
            <td>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="easyui-textbox" Width="430"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>附件：</b>
            </td>
            <td>
                <a href="javascript:;" id="insertfile">添加附件</a><hr />
                <ul id="lst_files">

                </ul>
            </td>
        </tr>
        <tr>
            <td>
                <b>邮件内容：</b>
            </td>
            <td>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="970" Height="300"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="text-align: center;">
        <asp:Button ID="btnSend" runat="server" Text="确定发送" OnClick="btnSend_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="取消发送" />
    </div>
</asp:Content>
