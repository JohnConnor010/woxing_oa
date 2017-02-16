<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_SignTransferKong.aspx.cs" Inherits="wwwroot.Manage.HR.HR_SignTransferKong" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    员工申请 >> 调岗/升职
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table3" style="text-align: center; line-height: 200%;">
            <tr>
                <td rowspan="2" width="80">
                    姓名
                </td>
                <td rowspan="2">
                    <asp:Literal ID="li_name" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    性别
                </td>
                <td>
                    <asp:Literal ID="li_sex" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    学历
                </td>
                <td>
                    <asp:Literal ID="li_edu" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    专业
                </td>
                <td>
                    <asp:Literal ID="li_Prof" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    入职时间
                </td>
                <td>
                    <asp:Literal ID="li_intotime" runat="server"></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    外语
                </td>
                <td>
                    <asp:Literal ID="li_fl" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    出生日期
                </td>
                <td>
                    <asp:Literal ID="li_age" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    联系方式
                </td>
                <td>
                    <asp:Literal ID="li_Mobile" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    申请时间
                </td>
                <td>
                    <asp:Literal ID="li_addtime" runat="server"></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="10" style="padding:15px;" align="left">由原职：
                    <asp:DropDownList ID="ui_demp" runat="server" Width="150" Enabled="false">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ui_duty" runat="server" Width="100" Enabled="false">
                    </asp:DropDownList>
                    <asp:DropDownList Enabled="false" ID="DropDownList1" runat="server" Style="width: 250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="padding:15px;" align="left"><asp:Literal ID="li_center" runat="server" Text="调岗"></asp:Literal>为：
                    <asp:DropDownList ID="ui_demp2" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="ui_demp_SelectedIndexChanged" Enabled="false">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ui_duty2" runat="server" Width="100" Enabled="false">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server" Style="width: 250px" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
             <tr runat="server" id="tr_dept" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div>
                        <b>部门意见：</b><asp:Literal ID="li_dept" runat="server"></asp:Literal></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_dept" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_HR" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div>
                        <b>人资意见：</b><asp:Literal ID="li_hr" runat="server"></asp:Literal></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_hr" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_boss" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div>
                        <b> 中心意见：</b><asp:Literal ID="li_boss" runat="server"></asp:Literal></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_boss" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr id="tr_sub" runat="server">
                <td colspan="10" style="text-align: left; padding: 15px;">
                    <div>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>评价意见：</div>
                    <div>
                        <asp:TextBox ID="ui_dempop" runat="server" TextMode="MultiLine" Rows="4" Width="800"></asp:TextBox></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人<asp:TextBox ID="li_sqname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr id="tr_sub2" runat="server">
                <td>
                    &nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 提 交 " OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
