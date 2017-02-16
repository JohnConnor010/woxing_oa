<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_SetStatus.aspx.cs" Inherits="wwwroot.Manage.HR.HR_SetStatus" ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 考勤管理 >> 员工状态
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-kq" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 姓名&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">员工姓名</span>
                   &nbsp;<asp:Literal ID="ui_name" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 状态&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note"><div id="div1">销假：保存后员工状态为正常“出勤”</div><div id="div2">返回：保存后员工状态为正常“出勤”</div><div id="div3">返差：保存后员工状态为正常“出勤”</div>
                    <div id="div4">迟到签到：保存后员工状态为“迟到”，并计算出迟到了多长时间</div><div id="div5">早退签到：保存后员工状态为“早退”，并计算出早退了多长时间</div><div id="div6">外出：保存后员工状态为“外出”，外出状态分为“因公”和“因私”，截止时间必须填</div>
                    <div id="div7">出差：保存后员工状态为“出差”，截止时间必须填</div><div id="div8">事假：保存后员工状态为“请事假”，截止时间必须填</div><div id="div9">病假：保存后员工状态为“请病假”，截止时间必须填</div>
                    <div id="div10">旷工：保存后员工状态为“旷工”</div></span>
                    <asp:DropDownList ID="ui_state" runat="server" onchange="check(this);">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr1">
                <th style="width: 135px; font-weight: bold;">
                    * 类型&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">请假或外出类型</span>
                    &nbsp;<asp:DropDownList ID="ui_type" runat="server">
                    <asp:ListItem Value="1" Text="因公"></asp:ListItem>
                    <asp:ListItem Value="2" Text="因私"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr2">
                <th style="width: 135px; font-weight: bold;">
                    * 截止时间&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">状态的截止时间，此项为必填项。选中超期设为“请假”或“旷工”处理，超期后系统自动处理，不选则超期后继续按之前的状态处理。</span>
                    &nbsp;<asp:TextBox ID="ui_stoptime" CssClass="easyui-datetimebox" runat="server"></asp:TextBox>&nbsp;<asp:CheckBox
                        ID="ui_isset" runat="server" />超期设为“请假”或“旷工”处理
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    * 备注&nbsp;<a href="#" class="help">[?]</a>：
                </th>
                <td>
                    <span class="note">一句话简单备注，50字以内</span>
                   &nbsp;<asp:TextBox ID="ui_demo" runat="server" MaxLength="50" Width="300"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                   &nbsp;
                </th>
                <td>
                   <asp:Button ID="Button1" runat="server" Text="提交" onclick="Button1_Click"  />
                </td>
            </tr>
        </table>
       
    </div>
    <script type="text/javascript">
        check(document.getElementById("ui_state"));
        tips(document.getElementById("ui_state"));
        function check(obj) {
            var opvalue = obj.options[obj.selectedIndex].value;
            document.getElementById("tr1").style.display = "block";
            document.getElementById("tr2").style.display = "block";
            if (opvalue != "6") {
                document.getElementById("tr1").style.display = "none";
            }
            if (opvalue == "8" || opvalue == "0" || opvalue == "9" || opvalue == "10") {
                document.getElementById("tr1").style.display = "none";
                document.getElementById("tr2").style.display = "none";
            }
        }
        function tips(obj) {
            for (var i = 1; i <= 10; i++) {
                document.getElementById("div"+i).style.display = "none";
            }
                for (var i = 0; i < obj.options.length; i++) {
                    if (obj.options[i].text == "销假")
                        document.getElementById("div1").style.display = "block";
                    if (obj.options[i].text == "返回")
                        document.getElementById("div2").style.display = "block";
                    if (obj.options[i].text == "返差")
                        document.getElementById("div3").style.display = "block";
                    if (obj.options[i].text == "迟到签到")
                        document.getElementById("div4").style.display = "block";
                    if (obj.options[i].text == "早退签到")
                        document.getElementById("div5").style.display = "block";
                    if (obj.options[i].text == "外出签到")
                        document.getElementById("div6").style.display = "block";
                    if (obj.options[i].text == "出差签到")
                        document.getElementById("div7").style.display = "block";
                    if (obj.options[i].text == "事假")
                        document.getElementById("div8").style.display = "block";
                    if (obj.options[i].text == "病假")
                        document.getElementById("div9").style.display = "block";
                    if (obj.options[i].text == "旷工")
                        document.getElementById("div10").style.display = "block";

                }

            switch (textstr) {
                case "销假": document.getElementById().style.display = "block"; break;
            }
        }
    </script>
</asp:Content>
