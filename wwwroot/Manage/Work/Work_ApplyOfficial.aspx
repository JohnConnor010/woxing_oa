<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_ApplyOfficial.aspx.cs" Inherits="wwwroot.Manage.Work.Work_ApplyOfficial" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">  我的申请 >> 转正
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="run_apply" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <table class="table3" style="text-align: center; line-height: 200%;">
            <tr>
                <td rowspan="2" width="80">
                    姓名
                </td>
                <td rowspan="2">
                    <asp:Literal ID="li_name" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    所属部门
                </td>
                <td align="left">
                    <asp:Literal ID="ui_demp" runat="server"></asp:Literal>
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
                    入职时间
                </td>
                <td>
                    <asp:Literal ID="li_intotime" runat="server"></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    职务
                </td>
                <td align="left">
                    <asp:Literal ID="ui_duty" runat="server"></asp:Literal>
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
                <asp:Literal ID="li_endtime" runat="server" ></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    工作设想
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                    <div style="color:Red;">申请被退回后1个月内不可重复申请，请仔细填写！！</div>
                        员工转正申请（内容包括对工作的回顾、总结；自己在工作中的优点及不足，如何改进存在的不足；对今后工作的设想：</div>
                    <asp:TextBox ID="ui_imagine" runat="server" TextMode="MultiLine" Rows="10" Width="700"></asp:TextBox><br />
                    <div style="float: left; width: 805px;">
                        注： 1、试用员工调用期结束前5日将转正申请表报行政部（员工试用期间请假时间过长需延长试用期）。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、员工试用期间有严重违反公司管理规章制度或给公司造成极坏影响的不予转正。<br /><br />
                        <div style="color:Red;" runat="server" id="divstr"></div>
                    </div>
                    <div style="float: left; padding-right: 20px;">
                        申请人：<asp:Literal ID="li_sqrname" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr runat="server" id="tr_dept" visible="false">
                <td>&nbsp;部门意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px; color:#555;">
                    <asp:Literal ID="li_dept" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr runat="server" id="tr_hr" visible="false">
                <td>&nbsp;人资意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px; color:#555;">
                    <asp:Literal ID="li_HR" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr runat="server" id="tr_ca" visible="false">
                <td>&nbsp;综管意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px; color:#555;">
                    <asp:Literal ID="li_CA" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr runat="server" id="tr_boss" visible="false">
                <td>&nbsp;中心意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px; color:#555;">
                    <asp:Literal ID="li_boss" runat="server"></asp:Literal>
                </td>
            </tr>
            
            <tr>
                <td>&nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 申 请 " OnClick="Button1_Click" Visible="false" />
                </td>
            </tr>
        </table>
        
</asp:Content>
