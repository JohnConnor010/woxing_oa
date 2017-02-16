<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Flow_Prcs_Conditions.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_Conditions" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link rel="stylesheet" type="text/css" href="../css/style_setcondition.css">
    <script type="text/javascript" src="../../JS/ccorrect_btn.js"></script>
    <script type="text/javascript" src="../../JS/utility.js"></script>
    <script type="text/javascript">
        function upvalue(obj) {
            if (obj.value == "") {
                $('ITEM_VALUE').style.display = "block";
            }
            else {
                $('ITEM_VALUE').style.display = "none";
            }
        }
        function add_condition(flag) {
            var tab;
            var img;
            var trClass;
            if (flag == 1) {
                tab = $('PRCS_IN_TAB');
                img = '<image style="cursor:pointer" src="/images/edit.gif" align="absmiddle" onclick="upedit(this,1)"> <image style="cursor:pointer" src="/images/delete.gif" align="absmiddle" onclick="delRule(this,1)">';
            }
            else {
                tab = $('PRCS_OUT_TAB');
                img = '<image style="cursor:pointer" src="/images/edit.gif" align="absmiddle" onclick="upedit(this,0)"> <image style="cursor:pointer" src="/images/delete.gif" align="absmiddle" onclick="delRule(this,0)">';
            }
            var vITEM_NAME = $('ITEM_NAME').value.split("-")[0];
            var vCONDITION = $('CONDITION').value;
            var vITEM_VALUE = ($('ITEM_VALUE').style.display == "none" ? $('ITEM_VALUE2').value : $('ITEM_VALUE').value);
            if (vITEM_VALUE.indexOf("'") >= 0) {
                alert("值中不能含有'号");
                return;
            }



            if (($('CONDITION').value == "=" ||
      $('CONDITION').value == "<>")) {
                if (vCONDITION == "=")
                    vCONDITION = "=";
                else
                    vCONDITION = "!=";

            }
            var str = "'" + vITEM_NAME + "'" + vCONDITION + "'" + vITEM_VALUE + "'";
            for (var i = 1; i < tab.rows.length; i++) {

                if (tab.rows[i].cells[1].innerText.indexOf(str) >= 0) {
                    alert("条件重复！");
                    return;
                }
            }

            if (tab.rows.length % 2 == 1) trClass = "TableLine1";
            else trClass = "TableLine2";

            var newRow = tab.insertRow(-1);
            newRow.setAttribute("className", trClass);
            var first = newRow.insertCell(-1);
            first.setAttribute("align", "center");
            first.innerText = "[" + (tab.rows.length - 1) + "]";
            var second = newRow.insertCell(-1);
            second.innerText = str;
            var third = newRow.insertCell(-1);
            third.setAttribute("align", "center");
            third.innerHTML = img;
        }

        function upedit(obj, flag) {
            var td = obj.parentNode.parentNode.cells[1];
            var obj_edit = $("edit");
            var content = td.innerText;
            obj_edit.value = content;
            td.innerHTML = "";
            td.appendChild(obj_edit);
            obj_edit.style.display = "";
            obj_edit.select()
            obj_edit.focus();
        }
        function update_edit(edit) {
            edit.style.display = "none";
            var td = edit.parentNode;
            document.body.appendChild(edit);
            td.innerText = edit.value;

        }

        function my_tip() {
            if ($('tip').style.display == "none")
                $('tip').style.display = "";
            else
                $('tip').style.display = "none";
        }
        function change_condition() {
            if ($('CONDITION').value == "=" || $('CONDITION').value == "<>")
                $('div_check').style.display = "inline";
            else
                $('div_check').style.display = "none";
            change_type();
        }

        function change_type() {
            if ($('div_check').style.display == "inline" && $('CHECK_TYPE').checked == true) {
                $('div_type').style.display = "inline";
                $('div_value').style.display = "none";
            }
            else {
                $('div_type').style.display = "none";
                $('div_value').style.display = "inline";
            }
        }
        function delRule(obj, flag) {
            var tab;
            if (flag == 1) tab = $('PRCS_IN_TAB');
            else tab = $('PRCS_OUT_TAB');
            var tr = obj.parentNode.parentNode;
            var no = tr.rowIndex;
            tab.deleteRow(tr.rowIndex);
            //obj.parentNode.parentNode.removeNode(true);

            //更新后面的序号
            for (var i = no; i < tab.rows.length; i++) {
                tab.rows[i].cells[0].innerText = "[" + (tab.rows[i].rowIndex) + "]";
            }
        }

        function mysubmit() {

            var tab_in = $("PRCS_IN_TAB");
            var tab_out = $("PRCS_OUT_TAB");
            for (var i = 1; i < tab_in.rows.length; i++) {
                $("PRCS_IN").value += tab_in.rows[i].cells[1].innerText + "\n";
            }
            for (var j = 1; j < tab_out.rows.length; j++) {
                $("PRCS_OUT").value += tab_out.rows[j].cells[1].innerText + "\n";
            }
            //if ($("PRCS_IN").value == "" && $("PRCS_OUT").value == "") {
            //    return false;
            //}
            if ($("PRCS_IN").value != "") {
                if (!check_exp($("PRCS_IN_SET").value))
                    return false;
            }
            if ($("PRCS_OUT").value != "") {
                if (!check_exp($("PRCS_OUT_SET").value))
                    return false;
            }


            return true;
        }

        function check_exp(s) {
            //检查公式
            if (s == "") {
                alert("条件表达式不能为空！");
                return false;
            }
            if (s.indexOf("(") >= 0 || s.indexOf(")") >= 0) {
                var num1 = s.split("(").length - 1;
                var num2 = s.split(")").length - 1;
                if (num1 != num2) {
                    alert("条件表达式书写错误,请检查括号匹配！");
                    return false;
                }
            }
            if (s.indexOf("[") >= 0 || s.indexOf("]") >= 0) {
                var number1 = s.split("[").length - 1;
                var number2 = s.split("]").length - 1;
                if (number1 != number2) {
                    alert("条件表达式书写错误,请检查括号匹配！");
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="8" Param1="{Q:FlowId}"
        Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div style="padding-left:10px;">
<table border="0" width="100%" cellspacing="0" cellpadding="3" class="small">
        <tr>
            <td>
                <img src="/images/edit.gif" width="22" height="20" align="absmiddle"><span class="big3">
                    条件设置</span> &nbsp;<a href="javascript:my_tip();">使用说明<span style="font-family: webdings">6</span></a><a class="help" href="#">[?]</a>

            </td>
        </tr>
    </table>
    <table border="0" width="90%" align="center" class="TableList">
        <tr id="tip" style="display: none">
            <td class="TableData">
                <b>条件设置使用说明：</b><br>
                条件列表处用于存储全部条件，每一行为一个条件。<br>
                如果不设置条件公式，所有条件之间均为“与”的关系。<br>
                如果设置条件公式，条件公式中需要引用条件列表中的条件，引用方法为使用条件编号。<br>
                <b>例如：</b><br>
                “满足条件1或者条件2”的条件公式为：<b>[1] or [2]</b><br>
                “满足条件1或者条件2，且满足条件3”的条件公式为：<b>([1] or [2]) and [3]</b><br>
                “满足条件1，且不满足条件2”的条件公式为：<b>[1] and ![2]</b><br>
            </td>
        </tr>
        <tr>
            <td height="30" class="TableContent">
                <img src="/images/green_arrow.gif" align="absmiddle"><b> 条件生成器</b>
            </td>
        </tr>
        <tr>
            <td class="TableData">
                <b>
                <table width="100%">
                  <tr>
                   <td width="350">字段
                    <asp:DropDownList ID="ITEM_NAME" runat="server" CssClass="SmallSelect">
                    </asp:DropDownList>
                    条件
                    <select id="CONDITION" onchange="change_condition();" class="SmallSelect">
                        <option value="=">等于</option>
                        <option value="<>">不等于</option>
                        <option value=">">大于</option>
                        <option value="<">小于</option>
                        <option value=">=">大于等于</option>
        	            <option value="<=">小于等于</option>
                        <option value="include">包含</option>
                        <option value="exclude">不包含</option>
                    </select>
            </td>
            <td>
                <div id="div_value" style="display: inline">
                    <table width="100%" align="left" border="0">
                        <tr>
                            <td width="20">
                                值
                            </td>
                            <td width="150">
                                    <asp:DropDownList onchange="upvalue(this);" ID="ITEM_VALUE2" runat="server" CssClass="SmallSelect">
                                    </asp:DropDownList> 
                            </td>
                            <td width="150">                            
                                    <input type="text" class="SmallInput" id="ITEM_VALUE" size="20" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                            </tr>
                    </table>
                </div>
                 </td>
                        </tr>
                    </table>
                <div align="center" style="margin: 5px 0 5px 0">
                    <input type="button" class="BigButton" value="添加到转入条件列表" onclick="add_condition(1);">&nbsp;&nbsp;
                    <input type="button" class="BigButton" value="添加到转出条件列表" onclick="add_condition(0)">
                </div>
                </b>
            </td>
        </tr>
        <tr>
            <td height="30" class="TableContent">
                <img src="/images/green_arrow.gif" align="absmiddle" onclick="upedit(this,1)"><b> 转入条件列表</b>
            </td>
        </tr>
        <tr>
            <td style="background-color: #ffffff">
                <b>合理设定转入条件，可形成流程的条件分支，但数据满足转入条件，才可转入本步骤</b>
                <table id="PRCS_IN_TAB" width="100%" align="center" class="TableList" border="0">
                    <tr class="TableHeader">
                        <td nowrap align="center" width="50">
                            编号
                        </td>
                        <td nowrap align="center">
                            条件描述
                        </td>
                        <td nowrap align="center" width="100">
                            操作
                        </td>
                    </tr>
                    <%=prcsinstr %>
                </table>
                <b>转入条件公式(条件与逻辑运算符之间需空格，如[1] AND [2])</b><br>
                <input type="text" class="BigInput" size="71" name="PRCS_IN_SET" id="PRCS_IN_SET"
                    value="" runat="server"><br>
                <b>不符合条件公式时，给用户的文字描述：<b>
                    <input type="text" class="BigInput" size="71" name="PRCS_IN_DESC" id="PRCS_IN_DESC" value="" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="30" class="TableContent">
                <img src="/images/green_arrow.gif" align="absmiddle"><b> 转出条件列表</b>
            </td>
        </tr>
        <tr>
            <td style="background-color: #ffffff">
                <b>合理设定转出条件，可对表单数据进行校验</b>
                <table id="PRCS_OUT_TAB" width="100%" align="center" class="TableList" border="0">
                    <tr class="TableHeader">
                        <td nowrap align="center" width="50">
                            编号
                        </td>
                        <td nowrap align="center">
                            条件描述
                        </td>
                        <td nowrap align="center" width="100">
                            操作
                        </td>
                    </tr>
                    <%=prcsoutstr %>
                </table>
                <b>转出条件公式(条件与逻辑运算符之间需空格，如[1] AND [2])</b><br>
                <input type="text" class="BigInput" size="71" name="PRCS_OUT_SET" id="PRCS_OUT_SET"
                    value="" runat="server"><br />
                <b>不符合条件公式时，给用户的文字描述：<b>
                    <input type="text" class="BigInput" size="71" name="PRCS_OUT_DESC" id="PRCS_OUT_DESC" value="" runat="server">
            </td>
        </tr>
        <tr align="center" class="TableControl">
            <td colspan="2" nowrap>
                <input type="hidden" name="PRCS_IN" id="PRCS_IN" value="" runat="server">
                <input type="hidden" name="PRCS_OUT" id="PRCS_OUT" value="" runat="server">
                <input type='hidden' value="8" name="FLOW_ID">
                <input type="hidden" value="19" name="ID">
                <input type='hidden' value="" name="GRAPH">
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="BigButton" OnClientClick="return mysubmit();"
                    OnClick="Button1_Click" />
                <%--<input type="button" class="BigButton" value="返回" onclick="location='index.php?FLOW_ID=8'">--%>
            </td>
        </tr>
    </table>
    <input type="text" id="edit" onblur="update_edit(this);" class="SmallInput" size="50"
        style="display: none;" />
</div>
</asp:Content>
