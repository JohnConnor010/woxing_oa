<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Flow_Prcs_OpSet.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_OpSet"
    ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <!--link rel="stylesheet" type="text/css" href="../css/style_setcondition.css" /-->
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript">
        function auto_set() {
            var auto_type = document.getElementById("AUTO_TYPE").value;
            //            if (auto_type != "")
            //                document.getElementById("lock_info").innerHTML = "是否允许修改主办人相关选项及默认经办人：";
            //            else
            //                document.getElementById("lock_info").innerHTML = "是否允许修改主办人相关选项：";
            if (auto_type == "7")
                document.getElementById("auto_user_set").style.display = "";
            else
                document.getElementById("auto_user_set").style.display = "none";

            if (auto_type == "2" || auto_type == "4" || auto_type == "6" || auto_type == "3" || auto_type == "9" || auto_type == "10" || auto_type == "11") {
                document.getElementById("base_user").style.display = "";
            }
            else
                document.getElementById("base_user").style.display = "none";


            if (auto_type == "8") {
                document.getElementById("item_list").style.display = "";
            } else {
                document.getElementById("item_list").style.display = "none";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="5" Param1="{Q:FlowId}"
        Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table id="auto" class="table" border="0">
        <tr>
            <th style="width:150px;">
                主办人相关选项：
            </th>
            <td>
                <asp:DropDownList runat="server" ID="TOP_DEFAULT" CssClass="SmallSelect">

                </asp:DropDownList>
                &nbsp;<a href="#" title="默认设置为：明确指定主办人。">说明</a>
                <div id="lock">
                    <span id="lock_info" style="font-weight: bold;">&nbsp;经办人未办理完毕时是否允许主办人强制转交：</span>
                    <asp:DropDownList runat="server" ID="USER_LOCK" CssClass="SmallSelect">
                        <asp:ListItem Value="1" Text="允许"></asp:ListItem>
                        <asp:ListItem Value="0" Text="不允许"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <th>
                选人过滤规则：
            </th>
            <td>
                <asp:DropDownList runat="server" ID="USER_FILTER" CssClass="SmallSelect">
                </asp:DropDownList>
                <br>
                &nbsp;说明：选人过滤规则在流程转交选择经办人时生效。默认设置为允许选择全部指定的经办人。
            </td>
        </tr>
        <tr>
            <th>
                自动选人规则：
            </th>
            <td>
                <asp:DropDownList runat="server" ID="AUTO_TYPE" CssClass="SmallSelect" onchange="auto_set();">
                </asp:DropDownList>
                <div id="item_list" style="display: none">
                    <b>&nbsp;根据表单字段决定默认办理人(第一个作为主办人)：</b><asp:DropDownList ID="drop_items" runat="server">
                    </asp:DropDownList>
                </div>
                <div id="base_user" style="display: none">
                    <b>&nbsp;部门针对对象:</b>
                    <asp:DropDownList ID="AUTO_PRCS_USER" runat="server" CssClass="SmallSelect">
                    </asp:DropDownList>
                    &nbsp;<a href="#" title="默认设置为：针对当前步骤主办人。">说明</a>
                </div>
                <div id="auto_user_set" style="display: none">
                    <b>主办人：</b>
                    <asp:HiddenField ID="AUTO_USER_OP" runat="server" />
                    <asp:TextBox ID="AUTO_USER_OP_NAME" Width="80" runat="server" CssClass="SmallStatic" ReadOnly="true"></asp:TextBox>
                  ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','AUTO_USER_OP','AUTO_USER_OP_NAME',468,395);">选择</a>
                     
                    <font color="red">&nbsp;主办人是某步骤的负责人，只允许主办人编辑表单、公共附件和转交流程</font><br>
                    <b>经办人：</b>
                    <asp:HiddenField ID="AUTO_USER" runat="server" />
                    <asp:TextBox ID="AUTO_USER_NAME" TextMode="MultiLine" Rows="4" Columns="40" runat="server" CssClass="BigStatic" ReadOnly="true"></asp:TextBox>
                    ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','AUTO_USER','AUTO_USER_NAME',468,395);">选择</a>
                     
                </div>
                <br>
                说明：通过自动选人规则,使流程经办人通过指定规则智能选择。默认设置为：不进行自动选择。注意，请同时设置好经办权限，自动选人规则才能生效。
            </td>
        </tr>
        <tr align="center" class="TableControl">
            <td colspan="2" nowrap>
                <asp:Button ID="Button1" runat="server" Text=" 保存 " CssClass="button" OnClick="Button1_Click" />
                <%--<input type="button" class="BigButton" value="返回" onclick="location='index.php?FLOW_ID=8'">--%>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        auto_set();
    </script>
</asp:Content>
