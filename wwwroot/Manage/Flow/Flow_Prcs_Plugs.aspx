<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Flow_Prcs_Plugs.aspx.cs"
    Inherits="wwwroot.Manage.Flow.Flow_Prcs_Plugs" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link rel="stylesheet" type="text/css" href="../css/style_setcondition.css">
    <script type="text/javascript" src="../../JS/utility.js"></script>
    <script language="javascript" type="text/javascript">
        function my_tip() {
            if ($('tip').style.display == "none")
                $('tip').style.display = "";
            else
                $('tip').style.display = "none";
        }
    </script>
    <style type="text/css">
    SmallButtonA {
    background: url("/images/big_btn_a.png") no-repeat scroll 0 0 transparent;
    border: 0 none;
    color: #36434E;
    cursor: pointer;
    height: 21px;
    width: 50px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="9" Param1="{Q:FlowId}"
        Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="padding-left: 10px;">
        <table border="0" width="100%" cellspacing="0" cellpadding="3" class="small">
            <tr>
                <td>
                    <img src="/images/edit.gif" width="22" height="20" align="absmiddle"><span class="big3">
                        条件设置</span> &nbsp;<a href="javascript:my_tip();">使用说明<span style="font-family: webdings">6</span></a>
                </td>
            </tr>
        </table>
        <table border="0" width="90%" align="center" class="TableList">
            <tr id="tip">
                <td class="TableData" colspan="2">
                    <b>插件设置使用说明：</b><br>
                    插件程序将在本步骤转入或转出后被自动调用执行。<a class="help" href="#">[?]</a>
                </td>
            </tr>
            <tr>
                <td height="30" class="TableContent">
                    <img src="/images/green_arrow.gif" align="absmiddle"><b> 插件生成器</b>
                </td>
                <td class="TableContent">
                </td>
            </tr>
            <tr>
                <td nowrap class="TableContent" width="150">
                    转入插件程序名称：
                </td>
                <td class="TableData">
                    <asp:DropDownList ID="PLUG_IN_type" runat="server">
                        <asp:ListItem Value="Url" Text="URL"></asp:ListItem>
                        <asp:ListItem Value="Proc" Text="存储过程"></asp:ListItem>
                    </asp:DropDownList>
                    <input type="text" id="PLUG_IN" name="PLUG_IN" size="50" maxlength="500" value=""
                        runat="server">&nbsp;<a href="javascript:;" title="如没有软件开发商特殊定制开发的的插件程序，请勿填写。插件程序将在本步骤转入后被自动调用执行。如：/blugin.aspx?flowId=[flowId]&id=[Id]，方括号内为QueryString参数名">说明</a>&nbsp;&nbsp;解析后：<%=plugin %>
                </td>
            </tr>
            <tr>
                <td nowrap class="TableContent">
                    转入插件说明：
                </td>
                <td class="TableData">
                    <asp:TextBox ID="PLUG_IN_demo" runat="server" TextMode="MultiLine" Rows="4" Columns="80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap class="TableContent">
                    转出插件程序名称：
                </td>
                <td class="TableData">
                    <asp:DropDownList ID="PLUG_OUT_type" runat="server">
                        <asp:ListItem Value="Url" Text="URL"></asp:ListItem>
                        <asp:ListItem Value="Proc" Text="存储过程"></asp:ListItem>
                    </asp:DropDownList>
                    <input type="text" id="PLUG_OUT" name="PLUG_OUT" size="50" maxlength="500" value=""
                        runat="server">
                    &nbsp;<a href="javascript:;" title="如没有软件开发商特殊定制开发的的插件程序，请勿填写。插件程序将在本步骤执行完毕后被自动调用执行。如：sp_get_tree_multi_table [flowId],'[name]'，方括号内为QueryString参数名">说明</a>解析后：<%=plugout %>
                </td>
            </tr>
            <tr>
                <td nowrap class="TableContent">
                    转出插件说明：
                </td>
                <td class="TableData">
                    <asp:TextBox ID="PLUG_OUT_demo" runat="server" TextMode="MultiLine" Rows="4" Columns="80"></asp:TextBox>
                </td>
            </tr>
            <tr align="center" class="TableControl">
                <td colspan="2" nowrap>
                    <asp:Button ID="Button1" runat="server" Text="保存" CssClass="SmallButtonA" OnClick="Button1_Click" />
                    <%--<input type="button" class="BigButton" value="返回" onclick="location='index.php?FLOW_ID=8'">--%>
                </td>
            </tr>
        </table>
        <input type="text" id="edit" onblur="update_edit(this);" class="SmallInput" size="50"
            style="display: none;" />
    </div>
</asp:Content>
