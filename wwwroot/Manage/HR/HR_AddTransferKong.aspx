<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="HR_AddTransferKong.aspx.cs" Inherits="wwwroot.Manage.HR.HR_AddTransferKong" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 人事档案 >> 调岗升职
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="5" Param1="{Q:UserId}"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
       <b>您当前的操作：员工<asp:Literal ID="Literal1" Text="调岗" runat="server"></asp:Literal></b>
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
                <td colspan="10" style="text-align: left; padding: 15px;">   
                部门评价意见:<br />     
                 <div>           
                        <asp:TextBox ID="ui_dempop" runat="server" TextMode="MultiLine" Rows="4" Width="800"></asp:TextBox></div>                    
                   
                </td>
            </tr>
            <tr>
                <td colspan="10" style="text-align: left; padding: 15px;">
                    <div>
                        根据上述综合表现由原职：<asp:DropDownList ID="ui_demp" runat="server" Width="150" Enabled="false">
                    </asp:DropDownList><asp:DropDownList ID="ui_duty" runat="server" Width="100" Enabled="false">
                    </asp:DropDownList>                    
                                    <asp:DropDownList Enabled="false" ID="DropDownList1" runat="server" Style="width: 250px">
                                    </asp:DropDownList>
                        <asp:Literal ID="li_center" runat="server"></asp:Literal>为
                        <asp:DropDownList ID="ui_demp2" runat="server" Width="150" AutoPostBack="True" 
                                        onselectedindexchanged="ui_demp_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ui_duty2" runat="server" Width="100">
                    </asp:DropDownList>
                                    <asp:DropDownList ID="DropDownList2" runat="server" Style="width: 250px">
                                    </asp:DropDownList></div>                    
                     <div style="float: right; padding-right: 20px;">
                        部门经理：<asp:TextBox ID="li_dempname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_dempuser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_dempuser','li_dempname',468,395);" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="text-align: left; padding: 15px;">
                    <div>
                        人力资源部（行政部）评价意见：</div>
                    <div>
                        <asp:TextBox ID="ui_hrop" runat="server" TextMode="MultiLine" Rows="4" Width="800"></asp:TextBox></div>                    
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="li_hrname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_hruser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_hruser','li_hrname',468,395);" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="text-align: left; padding: 15px;">
                    <div>
                       总经理批示：</div>
                     <div>
                        <asp:TextBox ID="ui_bossop" runat="server" TextMode="MultiLine" Rows="4" Width="800"></asp:TextBox></div>                    
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="li_bossname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <asp:HiddenField ID="ui_bossuser" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择负责人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_bossuser','li_bossname',468,395);" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 提 交 " OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
