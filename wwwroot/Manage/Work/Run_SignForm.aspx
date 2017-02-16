<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Run_SignForm.aspx.cs" Inherits="wwwroot.Manage.Work.Run_SignForm" ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/style_setcondition.css" />
    <link rel="stylesheet" type="text/css" href="/App_EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="/App_EasyUI/themes/icon.css" />
    <!--script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script-->
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/JS/utility.js"></script>
    <!--script language="javascript" type="text/javascript" src="/JS/jquery/jquery.min.js.gz"></script-->
    <script type="text/javascript" src="/JS/jquery/jquery-ui.custom.min.js.gz"></script>
    <script type="text/javascript" src="/JS/jquery/jquery.ui.autocomplete.min.js.gz"></script>
    <script language="javascript" type="text/javascript" src="/JS/jquery/tooltip/jquery.tooltip.min.js"></script>
    <script src="/JS/form.js" type="text/javascript"></script>
    <script src="/JS/ccorrect_btn.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //jQuery.noConflict();
        jQuery(document).ready(function () {
            jQuery("[name^='DATA_']").tooltip();
            jQuery("input[readonly='readonly']").css("background", "#eee");
        });
        //用户自定义js脚本
    </script>
    <script type="text/javascript">
        //印章文件
        function addseal(objectname) {
            var vSealName = "SignInfoseal";
            var vSealPostion = objectname + "Position";
            var vSealSignData = objectname;
            var strData = "我行信息技术有限公司";
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            DWebSignSeal.SetCurrUser("孙战平");
            DWebSignSeal.SetSignData("-");
            DWebSignSeal.SetSignData("+DATA:" + strData);
            DWebSignSeal.SetPosition(10, 10, vSealPostion);
            DWebSignSeal.addSeal("", vSealName);
            DWebSignSeal.SetMenuItem(vSealName, 5);
        }
        //手写签批
        function handwrite(objectname) {
            var vSealName = "SignInfohand";
            var vSealPostion = objectname + "Position";
            var vSealSignData = objectname;
            var strData = "我行信息技术有限公司";
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            DWebSignSeal.SetCurrUser("孙战平");
            DWebSignSeal.SetSignData("-");
            DWebSignSeal.SetSignData("+DATA:" + strData);
            DWebSignSeal.SetPosition(10, 10, vSealPostion);
            DWebSignSeal.HandWritePop(4, 255, 0, 0, 0, vSealName);
        }
        function GetValue_OnSubmit() {
            var seal = document.getElementById("cbSeal");
            if (!seal.checked) {
                return true; 
            }
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            var sing_info_str = "";
            var strObjectName = DWebSignSeal.FindSeal("", 0);
            while (strObjectName != "") {
                if (strObjectName.indexOf(strObjectName + ",") < 0) {
                    sing_info_str += strObjectName + ";";
                }
                strObjectName = DWebSignSeal.FindSeal(strObjectName, 0);
            }
            if (sing_info_str != "") {
                var value = DWebSignSeal.GetStoreDataEx(sing_info_str);
                document.getElementById("txtSealData").value = value;
                return true;
            }
            else {
                alert("提交印章失败");
                return false;
            }
        }
        function SealChange() {
            var seal = document.getElementById("cbSeal");
            document.getElementById("Buttonqz").disabled = document.getElementById("Buttonsx").disabled = !seal.checked;
        }
        function disp_form(id) {
            var cur_tab = "tab_" + id;
            jQuery(".navTab li").each(function () {
                var tab = jQuery(this);
                if (tab.attr("id") == cur_tab) {
                    tab.addClass("navTab_On");
                }
                else {
                    tab.removeClass("navTab_On");
                    if (tab.attr("id") > cur_tab)
                        tab.removeClass("left").addClass("right");
                    else
                        tab.removeClass("right").addClass("left");
                }
            });

            var cur_body = "body" + id;
            jQuery("div[id^='body']").each(function () {
                var body = jQuery(this);
                if (body.attr("id") == cur_body)
                    body.slideDown();
                else {
                    body.hide();
                }
            });
            if (id == 1) {
                jQuery("#body2,#body3").show();
            }
            setBody();
        }
        function deleteAttach(id, run_id) {
            if (!confirm("你真的要删除这个附件吗？")) return;
            jQuery.ajax({
                type: "get",
                url: "/App_Services/DeleteFromDb.ashx?fl_attach_id=" + id + "&run_id=" + run_id + "&rand=" + +Math.random(),
                success: function (result) {
                    if (result < 0) {
                        jQuery.messager.alert("提示信息", "删除失败！", "warning");
                        return false;
                    }
                    else if (result >= 0) {
                        //$.messager.alert("提示信息", "删除成功！", "warning");
                        jQuery("#attach_" + id).remove();
                        return true;
                    }
                }
            });
        }
        function LoadSignData() {
            var DWebSignSeal = document.getElementById("DWebSignSeal");
            var storeData = document.getElementById("txtSealData").value;
            if (storeData == "") return;
            var strData = "我行信息技术有限公司";
            DWebSignSeal.SetStoreData(storeData);
            DWebSignSeal.ShowWebSeals();
            DWebSignSeal.SetSealSignData("SignInfoseal", strData);
            DWebSignSeal.SetSealSignData("SignInfohand", strData);
        }
    </script>
    <script type="text/javascript" src="/App_Scripts/Loadwebsign.js"></script>
    <script src="/App_Scripts/jquery.MultiFile.js" type="text/javascript"></script>
    <script src="/App_Scripts/popup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationHolder" runat="server">
    新建工作测试 >> 签办
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="run_a" CurIndex="1" Param1="{Q:Run_Id}" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="table">
        <div id="overlay">
        </div>
        <div id="personal_sign" class="ModalDialog" style="width: 600px;">
            <div class="header">
                <span id="title" class="title">会签意见(手写或签章)</span><a class="operation" href="javascript:;"
                    onclick="javascript:HideDialog('personal_sign');return false;"><img src="/images/close.png" /></a></div>
            <div id="personal_sign_body" class="body" style="height: 320px; width: 600px; overflow: auto">
            </div>
            <div id="footer" class="footer">
                <input class="BigButton" onclick="HideDialog('personal_sign');" type="button" value="关闭" />
            </div>
        </div>
        <div id="notice_div" style="text-align: center; position: absolute; width: 150px;
            font-size: 11pt; height: 25px; line-height: 25px; right: 20px; top: 30px; display: none;
            z-index: 0; background: #DE7293;">
            <span id="notice_msg" style="color: white; font-weight: bold"></span>
        </div>
        <!--标题区域开始-->
        <div id="form_title">
            <div>
                <ul class="navTab">
                    <li id="tab_1" class="left navTab_On" onclick="disp_form('1')"><span>
                        <img src="/images/form.gif" align="absmiddle" width="16" height="16" border="0" alt="表单" />表单</span></li>
                    <li id="tab_2" class="right" onclick="disp_form('2')"><span>
                        <img src="/images/attach.gif" align="absmiddle" width="16" height="16" border="0"
                            alt="附件" />附件</span></li>
                    <li id="tab_3" class="right" onclick="disp_form('3')"><span>
                        <img src="/images/sign.gif" align="absmiddle" width="16" height="16" border="0" alt="会签" />会签</span></li>
                </ul>
            </div>
            <div id="info_title">
                &nbsp;&nbsp; NO.9&nbsp; 请款(借支)申请(2012-08-03 09:23:20)
            </div>
            <div id="op_flag" style="margin-right: 10px;">
                <span class='color2'>■</span>主办
            </div>
        </div>
        <!--标题区域结束-->
        <!--表单区域开始-->
        <div id="form_body">
            <div id="body1">
                <br />
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <br />
            </div>
            <!-- body1-->
            <div id="body2" style="position: relative; zoom: 1">
                <br>
                <table border="0" align="center" width="95%" class="small">
                    <tr>
                        <td class="Big">
                            <img alt="" src="/images/green_arrow.gif" align="absmiddle" /><span class="big3"> <b>公共附件区</b></span>
                        </td>
                    </tr>
                    <tr>
                        <td><!--上传附件显示-->
                            <asp:Literal ID="liAttachList" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!--上传组件部分-->
                            <asp:FileUpload ID="FileUpload1" runat="server" class="multi" />
                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
                <span id="imsagesstr"></span>
            </div>
            <!-- body2-->
            <div id="body3">
                <br>
                <table border="0" align="center" width="95%" class="small">
                    <tr>
                        <td class="Big">
                            <img src="/images/green_arrow.gif" align="absmiddle" /><span class="big3"> <b>会签意见区</b></span>
                        </td>
                    </tr>
                    <asp:Literal ID="liSignList" runat="server"></asp:Literal>
                </table>
                <div style="clear:both;" />
                <table class="TableBlock" align="center" width="95%" id="qz" runat="server">
                    <tr class="TableContent">
                        <td id="FORM_CONTENTPosition">
                        <div style="width: 85%; float:left;">
                            <asp:TextBox runat="server" ID="FORM_CONTENT" TextMode="MultiLine" Width="100%" Height="150">
                            </asp:TextBox>
                        </div>
                        <div style="width:14%; text-align:left;float:right;">
                             <asp:Literal ID="liCurUserSealButton" runat="server"></asp:Literal>
                             <br/><br/>
                             <%--<asp:Button runat="server" OnClick="ClearSignData" Text="清空图章手写" OnClientClick="return confirm('是否清空图章？')" />--%>
                        </div>
                        <div style="clear:both;" />
                        </td>
                    </tr>
                    <tr class="TableData">
                        <td nowrap>
                            <!--script type="text/javascript">ShowAddFile('1');</script-->
                        </td>
                    </tr>
                   <%-- <tr class="TableControl">
                        <td>
                            <div align="center">
                                <input type="checkbox" id="cbSeal" onchange="SealChange()" />&nbsp;我想签章&nbsp;&nbsp;&nbsp;
                                <input id="Buttonqz" type="button" class="SmallButton" disabled="disabled" value=" 盖 章 "
                                    onclick="addseal('FORM_CONTENT')" />
                                &nbsp;
                                <input id="Buttonsx" type="button" class="SmallButton" disabled="disabled" value=" 手 写 "
                                    onclick="handwrite('FORM_CONTENT')" />
                            </div>
                        </td>
                    </tr>--%>
                </table>
                                <asp:HiddenField ID="txtSealData" runat="server" />
                <!--script type="text/javascript" language="javascript">

                    function set_sign_cookie() {
                        var exp = new Date();
                        if ("" == "1")
                            var flow_sign_flag = 0;
                        else
                            var flow_sign_flag = 1;
                        exp.setTime(exp.getTime() + 24 * 60 * 60 * 1000);
                        document.cookie = "flow_sign_flag=" + flow_sign_flag + ";expires=" + exp.toGMTString() + ";path=/";
                        CheckForm(1);
                    }
                    function btnsub() {
                        var idlist = $('idlist').value;
                        var namelist = $('namelist').value;
                        alert("idlist=" + idlist + ",namelist=" + namelist);
                    }
                </script-->
                <br>
            </div>
            <!-- body3-->
            <div id="body4" style="display: none;">
            </div>
            <div id="focus_div" style="position: absolute; left: 0px; top: 0px; width: 100%;
                padding: 10 20 10 20; color: white; text-align: center; vertical-align: middle;
                display: none; z-index: 1000; background-color: #DE7293;">
                目前没有人关注此工作！
            </div>
        </div>
        <!--表单区域结束-->
        <!--操作区域开始-->
        <div id="form_control" runat="server">
            <table class="small control_tbl" width="100%">
                <tr>
                    <td align="left">
                        &nbsp;<a href="javascript:;" onclick="set_view('0');return false;" title="打印表单"><img
                            width="16" height="16" src="/images/print.gif" align="absmiddle">打印表单</a>&nbsp;
                        <a href="javascript:;" onclick="set_view('1');return false;" title="查看流程图">
                            <img width="16" height="16" src="/images/workflow.gif" align="absmiddle">查看流程图</a>&nbsp;
                        <a id="view" href="javascript:;" class="dropdown" onclick="showMenu(this.id,1);viewMenu(this.id,'30');return false;"
                            hidefocus="true"><span>更多操作</span></a>&nbsp;
                        <div id="view_menu" class="attach_div" align="left" nowrap>
                            <a href="javascript:;" onclick="set_view('4');return false;" title="关注此工作">
                                <img width="16" height="16" src="/images/focus.gif" align="absmiddle">关注此工作</a>
                            <a href="javascript:;" onclick="set_view('3');return false;" title="查看关注情况">
                                <img width="16" height="16" src="/images/focus.gif" align="absmiddle">查看关注情况</a>
                            <a href="javascript:;" onclick="set_view('5');return false;" title="收藏此工作">
                                <img border="0" src="/images/favorites.gif" align="absmiddle">收藏此工作</a>
                        </div>
                    </td>
                    <td align="center" valign="middle">
                        <asp:Button ID="Button1" runat="server" Text="通过" CssClass="BigButton" OnClick="btn_EnterPass"
                            OnClientClick="return GetValue_OnSubmit()" />
                        <asp:Button ID="Button4" runat="server" Text="未通过" CssClass="BigButton" OnClick="btn_Cancel" />
                        <asp:HiddenField ID="hiddrunid" runat="server" />
                        <asp:HiddenField ID="hiddstepno" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        LoadSignData();
    </script>
</asp:Content>
