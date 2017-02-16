<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Form_Module.aspx.cs" Inherits="wwwroot.Manage.Flow.Form_Module" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <style type="text/css">
        html, body
        {
            width: 100%;
            margin: 0;
        }
        .btnTool
        {
            margin: 0;
            width: 120px;
            height: 24px;
            text-align: left;
            font-size: 9pt;
            background-repeat: no-repeat;
            padding-left: 20px;
        }
        
        table.btnControl
        {
            margin-top: 5px;
        }
    </style>
    <script language="JavaScript">
        var form_id = <%=fid %>;
        var inputcount=0;
        function myclose() {
            var msg = '关闭表单设计器前，保存对表单的修改？';
            if (window.confirm(msg)) {
                document.form1.CLOSE_FLAG.value = "1";
                send();
            }
            else
                window.close();
        }

        function setHeight() {
            var editor = document.getElementById("fckeditor");
            if ((document.body) && (document.body.clientHeight)) {
                height = document.body.clientHeight;
                editor.style.height = height - 40;
            }
        }

        function Load_Do() {
            self.moveTo(0, 0);
            self.resizeTo(screen.availWidth, screen.availHeight);
            self.focus();
            setHeight();
        }

        //--- 单行输入框（新） ---
        function exec_cmd(cmd) {
            var FCK = FCKeditorAPI.GetInstance('FORM_CONTENT'); //获得表单设计器的顶层对象FCK，该方法定义位置fckeditorapi.js第47行 by dq 090521
            FCK.Focus();
            FCK.Commands.GetCommand(cmd).Execute(); //仿照fcktoolbarbutton.js第71行的写法 by dq 090521
           // return false;
        }

//        function viewForm() {
//            window.open("/general/workflow/form_view.php?FORM_ID=1", "form_view_1", "menubar=0,toolbar=0,status=0,resizable=1,left=0,top=0,scrollbars=1,width=" + (screen.availWidth - 10) + ",height=" + (screen.availHeight - 50) + "\"");
//        }

        function checkClose() {
            if (event.clientX > document.body.clientWidth - 20 && event.clientY < 0 || event.altKey)
                window.event.returnValue = '您确定退出表单设计器吗';
        }
        function show() {

           // var FCK = FCKeditorAPI.GetInstance('FORM_CONTENT'); //获得表单设计器的顶层对象FCK，该方法定义位置fckeditorapi.js第47行 by dq 090521
           
            //var FORM_HTML = FCK.EditingArea.Window.document.body.innerHTML;alert("sf");
           // document.getElementById('FORM_CONTENT').value = FCK.EditingArea.Window.document.body.innerHTML;
            //document.forms[0].action = 'Form_Preview.aspx?id=<%=fid %>';
            //document.forms[0].target = "_blank"; 
            //document.forms[0].submit();
            PopupIFrame('Form_Preview.aspx?FormId=<%=fid %>','预览表单','','',650,700)
        }
        
        function add()
        {
         var FCK = FCKeditorAPI.GetInstance('FORM_CONTENT'); //获得表单设计器的顶层对象FCK，该方法定义位置fckeditorapi.js第47行 by dq 090521
            var FORM_HTML = FCK.GetXHTML(true);
            
            document.forms[0].action = '';
            document.forms[0].target = "_self";
            if (FORM_HTML == "") {
                alert("表单内容不能为空！");
                return (false);
            }
            return (true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 表单定义 >> 智能生成器
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="form-modi" CurIndex="3" Param1="{Q:FormId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tr>
            <td>
                <FCKeditorV2:FCKeditor ID="FORM_CONTENT" runat="server" Height="500" Width="850px">
                </FCKeditorV2:FCKeditor>
            </td>
            <td>
                <table width="130" border="0" cellpadding="0" cellspacing="0" class="TableBlock"
                    align="center">
                    <tr class="TableHeader">
                        <td align="center">
                            表单控件
                        </td>
                    </tr>
                    <tr class="TableData">
                        <td align="center">
                            <input type="button" style="background-image: url(/images/form/textfield.gif);" class="btnTool"
                                onclick="exec_cmd('td_textfield')" value="单行输入框" />
                            
                            <input type="button" style="background-image: url(/images/form/textarea.gif);" class="btnTool"
                                onclick="exec_cmd('td_textarea')" value="多行输入框" />
                            
                            <input type="button" style="background-image: url(/images/form/listmenu.gif);" class="btnTool"
                                onclick="exec_cmd('td_listmenu')" value="下拉菜单" />
                            
                            <input type="button" style="background-image: url(/images/form/radio.gif);" class="btnTool"
                                onclick="exec_cmd('td_radio')" value="单选框" />
                           
                            <input type="button" style="background-image: url(/images/form/checkbox.gif);" class="btnTool"
                                onclick="exec_cmd('td_checkbox')" value="复选框" />
                            
                            <input type="button" style="background-image: url(/images/form/listview.gif);" class="btnTool"
                                onclick="exec_cmd('td_listview')" value="列表控件" />
                            
                            <input type="button" style="background-image: url(/images/form/auto.gif);" class="btnTool"
                                onclick="exec_cmd('td_auto')" value="宏控件" />
                            
                            <input type="button" style="background-image: url(/images/form/calendar.gif);" class="btnTool"
                                onclick="exec_cmd('td_calendar')" value="日历控件" />
                            
                            <input type="button" style="background-image: url(/images/form/calc.gif);" class="btnTool"
                                onclick="exec_cmd('td_calcu')" value="计算控件" />
                            
                            <input type="button" style="background-image: url(/images/form/user.gif);" class="btnTool"
                                onclick="exec_cmd('td_user')" value="部门人员控件" />
                            
                            <input type="button" style="background-image: url(/images/form/sign.gif);" class="btnTool"
                                onclick="exec_cmd('td_sign')" value="签章控件" />
                            
                            <input type="button" style="background-image: url(/images/form/data.gif);" class="btnTool"
                                onclick="exec_cmd('td_data_select')" value="数据选择控件" />
                            
                            <input type="button" style="background-image: url(/images/form/data.gif);" class="btnTool"
                                onclick="exec_cmd('td_data_fetch')" value="表单数据控件" />
                            
                            <input type="button" style="background-image: url(/images/form/progressbar.gif);"
                                class="btnTool" onclick="exec_cmd('td_progressbar')" value="进度条控件" />
                            
                            <input type="button" style="background-image: url(/images/form/imgupload.gif);" class="btnTool"
                                onclick="exec_cmd('td_imgupload')" value="图片上传控件" />
                            
                            <input type="button" style="background-image: url(/images/form/qrcode.gif);" class="btnTool"
                                onclick="exec_cmd('td_qrcode')" value="二维码控件" />
                            
                        </td>
                    </tr>
                </table>
                <table width="120" border="0" cellpadding="0" cellspacing="0" class="TableBlock btnControl"
                    align="center">
                    <tr class="TableHeader">
                        <td align="center">
                            保存与退出
                        </td>
                    </tr>
                    <tr class="TableData">
                        <td align="center">
                            <asp:Button ID="Button1" OnClientClick="return add();" CssClass="btnTool" runat="server" Text="保存表单"
                                OnClick="Button1_Click" />
                            <input type="button" class="btnTool" onclick="show();" value="预览表单" />
                           <input type="button"  class="btnTool" onclick="myclose();" value="关闭设计器" />
                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
