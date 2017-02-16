<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="User_Resume.aspx.cs" Inherits="wwwroot.Manage.HR.User_Resume" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server"><style type="text/css">
        .titlestyle
        {
            width: 100px;
            font-size: 14px;
            text-align: center;
            height: 30px;
            font-weight:bold;
        }
        .modu
        {
            height: 35px;
            background-color: #eee;
            font-weight: bold;
            padding-left: 20px;
            line-height: 200%;
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">应聘人员登记表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-new" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table style="margin: 0 auto; text-align: left;" align="center" width="1000" border="1"
        cellpadding="0" cellspacing="0" class="table3">
        <tr>
            <td colspan="7" class="modu">
                <div style="float: left;">
                    重要信息</div>
                <div style="float: right; padding-right: 20px;">
                </div>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                姓名
            </td>
            <td>
                <asp:Label ID="txtRealName" runat="server" ></asp:Label>
            </td>
            <td class="titlestyle">
                身份证号码
            </td>
            <td>
                <asp:Label ID="txtIDCard" runat="server" ></asp:Label>
            </td>
            <td class="titlestyle">
                电子邮件
            </td>
            <td colspan="2" style=" font-size:12px;">
                <asp:Label ID="txtEmail" runat="server" ></asp:Label>               
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                拟入职部门
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="titlestyle">
                拟入职职务
            </td>
            <td>
                <asp:DropDownList ID="ui_jobname" runat="server">
                </asp:DropDownList>
            </td>
            <td class="titlestyle">
                拟薪资待遇
            </td>
            <td colspan="2">
                <asp:TextBox ID="ui_salary" runat="server" required="true" class="easyui-validatebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                性别
            </td>
            <td>
                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">男</asp:ListItem>
                    <asp:ListItem Value="0">女</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="titlestyle">
                出生年月
            </td>
            <td>
                <asp:Label ID="txtBirthday" runat="server"></asp:Label>
            </td>
            <td class="titlestyle">
                民族
            </td>
            <td>
                <asp:Label ID="ui_Ethnic" runat="server" ></asp:Label>
            </td>
            <td rowspan="5" width="150">
                <div style="font-weight: bold;">
                    两寸免冠照片(150*130)</div>
                <div style="text-align: center; padding: 3px 3px 3px 3px;">
                    <div style="width: 150; height: 130px; border: 0px dashed #787878; margin: auto;
                        padding: 1px 1px 1px 1px;">
                        <asp:Literal runat="server" ID="liPreZoomImage"></asp:Literal>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                学历
            </td>
            <td>
                <asp:DropDownList ID="ui_edu" runat="server">
                </asp:DropDownList>
            </td>
            <td class="titlestyle">
                婚否
            </td>
            <td>
                <asp:DropDownList ID="ui_Marital" runat="server">
                    <asp:ListItem Value="0" Text="未婚"></asp:ListItem>
                    <asp:ListItem Value="1" Text="已婚"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="titlestyle">
                健康状况
            </td>
            <td>
                <asp:Label ID="ui_Health" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                专业
            </td>
            <td>
                <asp:Label ID="ui_Prof" runat="server"></asp:Label>
            </td>
            <td class="titlestyle">
                外语语种
            </td>
            <td>
                <asp:Label ID="ui_ForeignL" runat="server"></asp:Label>
            </td>
            <td class="titlestyle">
                等级
            </td>
            <td>
                <asp:Label ID="ui_Rating" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                职称
            </td>
            <td>
                <asp:Label ID="ui_Titles" runat="server"></asp:Label>
            </td>
            <td class="titlestyle">
                户籍所在地
            </td>
            <td colspan="3">
                <asp:Label ID="ui_hkd" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                联系电话
            </td>
            <td>
                <asp:Label ID="txtTelephone" runat="server"></asp:Label>
            </td>
            <td class="titlestyle">
                手机
            </td>
            <td>
                <asp:Label ID="txtMobile" runat="server"></asp:Label>
            </td>
            <td class="titlestyle">
                联系地址
            </td>
            <td>
                <asp:Label ID="txtAddress" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                离职原因
            </td>
            <td colspan="6">
                    <asp:Label ID="txtContent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="7" class="modu">
                <div style="float: left;">
                    个人技能</div>
                <div style="float: right; padding-right: 20px;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="7">
            <asp:Literal ID="li_Skill" runat="server">&nbsp;</asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="7" class="modu">
                <div style="float: left;">
                    教育经历</div>
                <div style="float: right; padding-right: 20px;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="7">
               <asp:Literal ID="li_edu" runat="server">&nbsp;</asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="7" class="modu">
                <div style="float: left;">
                    工作经历</div>
                <div style="float: right; padding-right: 20px;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="7"><asp:Literal ID="li_work" runat="server">&nbsp;</asp:Literal>
            </td>
        </tr> 
         <tr>
            <td class="titlestyle">
                初审意见
            </td>
            <td colspan="6">
                <div style="float: left">
                    <asp:TextBox ID="TextBox1" runat="server" Columns="70" Rows="3" TextMode="MultiLine"></asp:TextBox></div>
                <div style="float: left; padding: 20px;">
                    <input type="submit" runat="server" onserverclick="RegisterUser" value="保存以上信息"
                        id="Submit3" class="button" />                        
                        <input type="submit" runat="server" visible="false" onserverclick="State6User" value="通过" id="Submit1" class="button" /><input type="submit" runat="server" visible="false" onserverclick="State2User" value="未通过" id="Submit2" class="button" />
                        </div>
            </td>
        </tr>
    </table>
</asp:Content>
