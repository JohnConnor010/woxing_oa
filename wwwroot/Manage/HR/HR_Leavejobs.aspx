<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="HR_Leavejobs.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Leavejobs" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 人事档案 >> 离职员工
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
                    所属部门
                </td>
                <td>
                <asp:DropDownList ID="ui_demp" runat="server" Width="150" Enabled="false">
                    </asp:DropDownList>
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
                <td>
                <asp:DropDownList ID="ui_duty" runat="server" Width="100" Enabled="false">
                    </asp:DropDownList>
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
                <td>
                    离职原因
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                       <asp:Literal ID="li_reason" runat="server"></asp:Literal><br />
                    将于即日起<asp:TextBox ID="ui_days" runat="server" Width="50"></asp:TextBox>天后离职，本人最后到职日期为<asp:TextBox ID="ui_lasttime" runat="server" Width="90" CssClass="easyui-datebox"></asp:TextBox>。
                    <div style="float: right; padding-right: 20px;">
                        申请人：<asp:Literal ID="li_sqrname" runat="server"></asp:Literal></div>
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
            <tr id="tr_sub" runat="server" visible="false">
                <td>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        <asp:TextBox ID="ui_dempop" runat="server" TextMode="MultiLine" Rows="4" Width="700"></asp:TextBox></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="li_dempname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>           
            <tr id="tr_sub2" runat="server" visible="false">
                <td>&nbsp;&nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 通 过 " OnClick="Button1_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text=" 不 通 过 " OnClick="Button2_Click" />
                </td>
            </tr>
            
            <tr runat="server" id="tr_receshow" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                        <b> 交接内容：</b><asp:Literal ID="li_rece" runat="server"></asp:Literal><br />
                    <asp:Literal ID="li_annex" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="text-align: left; padding: 5px;">
                 <div  id="div_rece" runat="server" visible="false">
                 <b> 部门工作交接内容或问题：</b><br />
                     <asp:HiddenField ID="hidden_receid" runat="server" />
                     <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="2" Width="700"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text=" 提 交 " OnClick="Button3_Click" /></div>
                    <asp:GridView ID="Gv_Receive" runat="server" CssClass="table tableview" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000"  ShowHeader="false"
                        OnRowDataBound="Gv_Receive_RowDataBound" 
                        onrowcommand="Gv_Receive_RowCommand">
            <Columns><asp:TemplateField HeaderText="部门">
                    <ItemTemplate>
                       <%# WX.CommonUtils.GetDeptNameListByDeptIdList(Eval("DeptID").ToString())%>
                    </ItemTemplate>
                    <ItemStyle Width="120" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="交接内容">
                    <ItemTemplate>
                       <%# Eval("Question") + "&nbsp;&nbsp;&nbsp;&nbsp;" + Eval("QuestionTime") + WX.CommonUtils.GetRealNameListByUserIdList(Eval("AddUserID").ToString())%><br />
                       <%# Eval("Answer") + "&nbsp;&nbsp;&nbsp;&nbsp;" + Eval("AnswerTime") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                       <%# WX.HR.Receive.Statestr[Convert.ToInt32(Eval("State").ToString())]%>
                       <div style="color:#888;"><%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("ConfirmUserID").ToString())%></div>
                    </ItemTemplate>
                    <ItemStyle Width="60" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="linkup" CommandArgument='<%# Eval("ID")+"|"+Eval("AnswerTime")+"|"+Eval("State")%>' runat="server">修改</asp:LinkButton>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton2" CommandName="linkdel" CommandArgument='<%# Eval("ID")+"|"+Eval("AnswerTime")+"|"+Eval("State")%>' runat="server">删除</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="80" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>