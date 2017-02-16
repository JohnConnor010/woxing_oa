<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Flow_Prcs_HiddenFlds.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_HiddenFlds" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link rel="stylesheet" type="text/css" href="../css/style_setcondition.css">
    <script type="text/javascript" src="../../JS/ccorrect_btn.js"></script>    
    <script type="text/javascript" src="../../JS/utility.js"></script>
    <script type="text/javascript">

        function func_color(select_obj) {
            font_color = "red";
            option_text = "";
            for (j = 0; j < select_obj.options.length; j++) {
                str = select_obj.options[j].text;
                if (str.indexOf(option_text) < 0) {
                    if (font_color == "red")
                        font_color = "blue";
                    else
                        font_color = "red";
                }
                select_obj.options[j].style.color = font_color;

                pos = str.indexOf("] ") + 1;
                option_text = str.substr(0, pos);
            } //for

            return j;
        }

        function func_insert() {
            var oSelect1 = $('select1');
            var oSelect2 = $('select2');
            for (i = oSelect2.options.length - 1; i >= 0; i--) {
                if (oSelect2.options[i].selected) {
                    option_text = oSelect2.options[i].text;
                    option_value = oSelect2.options[i].value;
                    option_style_color = oSelect2.options[i].style.color;

                    var my_option = document.createElement("OPTION");
                    my_option.text = option_text;
                    my_option.value = option_value;
                    my_option.style.color = option_style_color;

                    oSelect1.options.add(my_option);
                    oSelect2.remove(i);
                }
            } //for

            func_init();
        }

        function func_delete() {
            var oSelect1 = $('select1');
            var oSelect2 = $('select2');
            for (i = oSelect1.options.length - 1; i >= 0; i--) {
                if (oSelect1.options[i].selected) {
                    option_text = oSelect1.options[i].text;
                    option_value = oSelect1.options[i].value;

                    var my_option = document.createElement("OPTION");
                    my_option.text = option_text;
                    my_option.value = option_value;

                    oSelect2.options.add(my_option);
                    oSelect1.remove(i);
                }
            } //for

            func_init();
        }

        function func_select_all1() {
            var oSelect1 = $('select1');
            for (i = oSelect1.options.length - 1; i >= 0; i--)
                oSelect1.options[i].selected = true;
        }

        function func_select_all2() {
            var oSelect2 = $('select2');
            for (i = oSelect2.options.length - 1; i >= 0; i--)
                oSelect2.options[i].selected = true;
        }

        function func_init() {
            func_color($('select2'));
            func_color($('select1'));
        }

        function mysubmit() {
            var oSelect1 = $('select2');
            fld_str = "";
            for (i = 0; i < oSelect1.options.length; i++) {
                fld_str += oSelect1.options[i].value + "|" + oSelect1.options[i].text + ",";
            }
            $('FLD_STR').value = fld_str;

            document.forms[0].submit();
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="7" Param1="{Q:FlowId}" Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div style="padding-left:10px;">
<table class="TableTop" width="500" align="center">
   <tr>
      <td class="left"></td>
      <td class="center"><img src="/images/edit.gif" WIDTH="22" HEIGHT="20" align="absmiddle" /><span class="big3">编辑隐藏字段 -  步骤<asp:Literal ID="Literal1" runat="server"></asp:Literal></span></td>
      <td class="right"></td>
   </tr>
</table>
<table width="500" border="0" align="center" class="TableList no-top-border">
  <tr class="TableContent">
    <td align="center"><b>本步骤可写字段</b></td>
    <td align="center">&nbsp;</td>
    <td align="center" valign="top"><b>备选字段</b></td>
  </tr>
  <tr class="TableContent">
    <td valign="top" align="center" >
        <asp:ListBox ID="select1" runat="server" Height="220" Width="200" SelectionMode="Multiple"></asp:ListBox>
   
    <input type="button" value=" 全选 " onclick="func_select_all1();" class="SmallButton" />
    </td>

    <td align="center" valign="middle" width=100>
      <input type="button" class="SmallButton" value=" ← " onclick="func_insert();">
      <br><br>
      <input type="button" class="SmallButton" value=" → " onclick="func_delete();">
    </td>

    <td align="center" valign="top" bgcolor="#CCCCCC">
        <asp:ListBox ID="select2" runat="server" Height="220" Width="200" SelectionMode="Multiple"></asp:ListBox>
    <input type="button" value=" 全选 " onclick="func_select_all2();" class="SmallButton">
    </td>
  </tr>
</table>
<table width="500" border="0" align="center" class="TableList no-top-border">
  <tr class="TableContent">
  	<td align=center>
  	点击条目时，可以组合CTRL或SHIFT键进行多选<a class="help" href="#">[?]</a></td>
  </tr>
  
  <tr>
    <td align="center" class="TableFooter">
        <asp:Button ID="Button1" runat="server" CssClass="BigButton" Text="保存" 
            OnClientClick="mysubmit();" onclick="Button1_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;

      <input type='hidden' value="" name="GRAPH">
      <input type="hidden" name="FLD_STR" id="FLD_STR" value="" runat="server">
    </td>
  </tr>
</table>
<script type="text/javascript">
    func_init();
</script>
</div>
</asp:Content>
