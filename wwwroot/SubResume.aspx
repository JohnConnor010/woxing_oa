<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubResume.aspx.cs" ClientIDMode="Static"
    Inherits="wwwUser.SubResume" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我行信息有限公司简历提交页</title>
    



    <link rel="stylesheet" type="text/css" href="/Manage/images/skin.css" />
    <link rel="stylesheet" type="text/css" href="/Manage/images/style_login.css" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
        function getbrigthday() {
            var idcard = $('#txtIDCard').val();
            document.getElementById("txtBirthday").value = idcard.substring(6, 10) + "-" + idcard.substring(10, 12) + "-" + idcard.substring(12, 14);
        }
    </script>
    <style type="text/css">
        .titlestyle
        {
            width: 100px;
            font-size: 14px;
            text-align: center;
            height: 30px;
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
</head>
<body>
    <form id="form1" runat="server" name="form1">
    <br />
    <br />
    <center>
        <h1>
            应聘人员登记表</h1>
    </center>
    <br />
    <br />
    <table style="margin: 0 auto; text-align: left;" align="center" width="1000" border="1"
        cellpadding="0" cellspacing="0">
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
                <asp:TextBox ID="txtRealName" runat="server" MaxLength="20" required="true" validtype="chinese" invalidMessage="必须为有效的中文汉字" class="easyui-validatebox"></asp:TextBox>
            </td>
            <td class="titlestyle">
                身份证号码
            </td>
            <td>
                <asp:TextBox ID="txtIDCard" runat="server" onchange="getbrigthday();" required="true"
                    class="easyui-validatebox" validType="idcard" invalidMessage="请输入有效的身份证号码！" MaxLength="90"></asp:TextBox>
            </td>
            <td class="titlestyle">
                电子邮件
            </td>
            <td colspan="2" style=" font-size:12px;">
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" required="true" validType="email" invalidMessage="您输入的电子邮件格式不正确，请重新输入！" class="easyui-validatebox"></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='if(document.getElementById("txtEmail").value==""){alert("请输入电子邮件"); return false;}' OnClick="LinkButton1_Click">获取验证码</asp:LinkButton>
                <span class="titlestyle">验证码</span>
                <asp:TextBox ID="txtGetCode" runat="server" MaxLength="5" Width="50" required="true"
                    class="easyui-validatebox"></asp:TextBox>
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
                <asp:TextBox ID="txtBirthday" runat="server" MaxLength="10" required="true" class="easyui-validatebox"></asp:TextBox>
            </td>
            <td class="titlestyle">
                民族
            </td>
            <td>
                <asp:TextBox ID="ui_Ethnic" runat="server" MaxLength="20" required="2" class="easyui-validatebox"></asp:TextBox>
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
                <div>
                    <asp:FileUpload ID="FileUpload1" Width="100%" runat="server" onchange="displayImages(this)" /></div>
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
                <asp:TextBox ID="ui_Health" runat="server" MaxLength="20" required="true"
                    class="easyui-validatebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                专业
            </td>
            <td>
                <asp:TextBox ID="ui_Prof" runat="server" MaxLength="20" required="true"
                    class="easyui-validatebox"></asp:TextBox>
            </td>
            <td class="titlestyle">
                外语语种
            </td>
            <td>
                <asp:TextBox ID="ui_ForeignL" runat="server" MaxLength="20"
                    class="easyui-validatebox"></asp:TextBox>
            </td>
            <td class="titlestyle">
                等级
            </td>
            <td>
                <asp:TextBox ID="ui_Rating" runat="server" MaxLength="20"
                    class="easyui-validatebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                职称
            </td>
            <td>
                <asp:TextBox ID="ui_Titles" runat="server" MaxLength="20"
                    class="easyui-validatebox"></asp:TextBox>
            </td>
            <td class="titlestyle">
                户籍所在地
            </td>
            <td colspan="3">
                <asp:TextBox ID="ui_hkd" runat="server" Width="300" MaxLength="20"
                    required="true" class="easyui-validatebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                联系电话
            </td>
            <td>
                <asp:TextBox ID="txtTelephone" runat="server" MaxLength="20" validType="phone"></asp:TextBox>
            </td>
            <td class="titlestyle">
                手机
            </td>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" required="true" validType="mobile" class="easyui-validatebox"></asp:TextBox>
            </td>
            <td class="titlestyle">
                联系地址
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server" MaxLength="30" required="true" class="easyui-validatebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="titlestyle">
                离职原因
            </td>
            <td colspan="6">
                <div style="float: left">
                    <asp:TextBox ID="txtContent" runat="server" Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox></div>
                <div style="float: left; padding: 20px;">
                    <input type="submit" runat="server" onserverclick="RegisterUser" name="Submit1" value="保存以上信息"
                        onclick="return Pre_Validate(this.form,1);" id="Submit1" class="button" /></div>
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
                <iframe height="150" frameborder="0" width="90%" src="<%=skillstr %>"></iframe>
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
                <iframe height="150" frameborder="0" width="90%" src="<%=edustr %>"></iframe>
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
            <td colspan="7">
                <iframe height="150" frameborder="0" width="90%" src="<%=workstr %>"></iframe>
            </td>
        </tr>
        <tr>
            <td colspan="7" height="38">
                <center>
                    <input type="submit" runat="server" onserverclick="RegisterUserother" name="Submit1"
                        value="提交简历" onclick="return Pre_Validate(this.form,1);" id="Submit2" class="button" /></center>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
