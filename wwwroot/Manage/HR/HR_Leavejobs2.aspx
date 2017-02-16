<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" ClientIDMode="Static" CodeBehind="HR_Leavejobs2.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Leavejobs21" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>


    
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 人事档案 >> 离职员工
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="7" Param1="{Q:UserID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
       <b>您当前的操作：员工离职</b>
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
                    <asp:RadioButtonList ID="radio_reason" runat="server">
                    <asp:ListItem Value="环境（气氛）不佳、志趣不合" Text="环境（气氛）不佳、志趣不合"></asp:ListItem>
                    <asp:ListItem Value="未毕业实习生，需返校" Text="未毕业实习生，需返校"></asp:ListItem>
                    <asp:ListItem Value="健康原因" Text="健康原因"></asp:ListItem>
                    <asp:ListItem Value="结婚、生育" Text="结婚、生育"></asp:ListItem>
                    <asp:ListItem Value="出国、服役" Text="出国、服役"></asp:ListItem>  
                    <asp:ListItem Value="" Text="其它"></asp:ListItem>                    
                    </asp:RadioButtonList>
                    其它：<asp:TextBox ID="ui_reason" runat="server" Width="390"></asp:TextBox><br />
                    将于即日起<asp:TextBox ID="ui_days" runat="server" Width="50"></asp:TextBox>天后离职，本人最后到职日期为<asp:TextBox ID="ui_lasttime" runat="server" Width="90" CssClass="easyui-datebox"></asp:TextBox>。
                    <div style="float: right; padding-right: 20px;">
                        申请人：<asp:Literal ID="li_sqrname" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td>
                    部门意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        <asp:TextBox ID="ui_dempop" runat="server" TextMode="MultiLine" Rows="4" Width="700"></asp:TextBox></div>
                    <div style="float: right; padding-right: 20px;">
                        部门经理：<asp:TextBox ID="li_dempname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_dempuser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_dempuser','li_dempname',468,395);" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                     财务统计部意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        <asp:TextBox ID="ui_adminop" runat="server" TextMode="MultiLine" Rows="4" Width="700"></asp:TextBox></div>
                    <div style="float: right; padding-right: 20px;">
                     经办人：<asp:TextBox ID="li_financialHandleManagername" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_financialHandleManager" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择经办人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_financialHandleManager','li_financialHandleManagername',468,395);" />
                        &nbsp;&nbsp;&nbsp;&nbsp;部门经理：<asp:TextBox ID="li_adminname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_adminuser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_adminuser','li_adminname',468,395);" />
                    </div>
                </td>
            </tr>
             <tr>
                <td>
                    人力资源部意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        <asp:TextBox ID="ui_hrop" runat="server" TextMode="MultiLine" Rows="4" Width="700"></asp:TextBox></div>
                    <div style="float: right; padding-right: 20px;">
                        部门经理：<asp:TextBox ID="li_hrname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_hruser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_hruser','li_hrname',468,395);" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    总经理批示
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        <asp:TextBox ID="ui_bossop" runat="server" TextMode="MultiLine" Rows="4" Width="700"></asp:TextBox></div>
                    <div style="float: right; padding-right: 20px;">
                        总经理：<asp:TextBox ID="li_bossname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_bossuser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_bossuser','li_bossname',468,395);" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 离 职 " OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>