<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_ApplyTransferKong.aspx.cs" Inherits="wwwroot.Manage.Work.Work_ApplyTransferKong" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">我的申请 >> 调岗
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="run_apply" CurIndex="3" />
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
            <td colspan="2" align="right">根据上述综合表现由原职：</td>
                <td colspan="8" style="text-align: left; padding: 15px;">
                        <asp:DropDownList ID="ui_demp" runat="server" Width="150" Enabled="false">
                    </asp:DropDownList><asp:DropDownList ID="ui_duty" runat="server" Width="100" Enabled="false">
                    </asp:DropDownList>                    
                                    <asp:DropDownList Enabled="false" ID="DropDownList1" runat="server" Style="width: 250px">
                                    </asp:DropDownList><br />
                       
                </td>
            </tr>
            <tr>
            <td colspan="2" align="right"><asp:Literal ID="li_center" runat="server"></asp:Literal>为：</td>
                <td colspan="8" style="text-align: left; padding: 15px;">
                     
                        <asp:DropDownList ID="ui_demp2" runat="server" Width="150" AutoPostBack="True" 
                                        onselectedindexchanged="ui_demp_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ui_duty2" runat="server" Width="100">
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
                <td colspan="2">&nbsp;
                </td>
                <td colspan="8" style="text-align: left; padding: 5px;">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 申 请 " OnClientClick="return confirm('确定要提交申请吗？');"  OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
</asp:Content>
