<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="User_AddUser.aspx.cs" Inherits="wwwroot.Manage.HR.User_AddUser"
    ClientIDMode="Static" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ddlDepartment').combotree({
                onSelect: function (node) {
                    $('#departmentId').val(node.id);
                }
            });
            $('#ddlDepartment').combotree("setValue", "101");
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
        function displayImages(fu) {
            $("#preZoomImage").css("display", "block");
            $("#preZoomImage").attr("src", fu.value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 添加新员工
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="user" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div class="note">此功能暂时停止，此功能原被是人事部内部入职专用！</div>
    <div id="PanelManage">
        <table class="table">
        <caption style="text-align:left;font-style:normal;">用户账户信息</caption>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    登录名&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td><span class="note">用户名是用于登录的名称，必须唯一且容易记忆。</span>
                    <asp:TextBox ID="userName" runat="server" MaxLength="20" Columns="30" class="easyui-validatebox"
                        required="true" validtype="regex[/^\w{6,20}$/]" invalidMessage="用户名必须用6-20个英文或字母数字组成"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    密码&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td><span class="note">密码必须用6-20个字符组成</span>
                    <asp:TextBox ID="password" runat="server" MaxLength="20" Columns="30" class="easyui-validatebox"
                        required="true" validtype="regex[/^[\w\W]{6,20}$/]" invalidMessage="密码必须用6-20个字符组成"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    电子邮件&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">电子邮箱是OA系统与员工通讯的方式之一</span>
                    <asp:TextBox ID="txtEmail" runat="server" Columns="30" MaxLength="100" class="easyui-validatebox"
                        validType="email" invalidMessage="您输入的电子邮件格式不正确，请重新输入！" required="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>      
         </table>
         <br/>
        <table class="table">
        <caption style="text-align:left; font-style:normal;">用户重要信息</caption>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    所在公司&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">本系统支持多个公司同时运行，部门隶属于公司。</span>
                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="easyui-combobox" Width="200"
                        panelHeight="auto">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    所在部门&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择员工所在的部门。</span>
                    <input type="hidden" id="departmentId" runat="server" />
                    <asp:DropDownList ID="ddlDepartment" runat="server" class="easyui-combotree"
                        url="/App_Services/GetJsonOfDepartment.ashx" valueField="id" textField="text"
                        panelHeight="auto" Style="width: 200px">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    所属职务&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择员工的职务</span>
                    <asp:DropDownList ID="ddlPosition" runat="server" CssClass="easyui-combobox" panelHeight="auto"
                        required="true" Height="17px" Width="150px">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    真实姓名&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td><span class="note">必须填写员工的真实姓名（必须为汉字）。</span>
                    <asp:TextBox ID="txtRealName" runat="server" Columns="30" MaxLength="20" class="easyui-validatebox"
                        required="true" validtype="chinese" invalidMessage="必须为有效的中文汉字"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    身份证号&nbsp;<a href="#" class="help">[?]
                </th>
                <td>
                    <span class="note">务必输入员工的真实有效的身份证号码。</span>
                    <asp:TextBox ID="txtIDCard" runat="server" Columns="30" MaxLength="90" class="easyui-validatebox"
                        validType="idcard" required="true" invalidMessage="请输入有效的身份证号码！"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
        </table>
         <br/>
        <table class="table">
            <caption style="text-align: left; font-style: normal;">
                用户基本信息</caption>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    出生日期&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">务必填写员工的出生日期。</span>
                    <asp:TextBox ID="txtBirthday" runat="server" class="easyui-datebox" MaxLength="10"
                        showSeconds="false" Style="width: 150px"></asp:TextBox>
                    &nbsp;
                </td>
                <td style="width: 150px; border:1px solid #BDDBEF" rowspan="6">
                    <div style="font-weight:bold;">
                        用户照片</div>
                    <div style="text-align:center; padding:3px 3px 3px 3px;">
                        <div style="width:130; height: 130px; border: 1px dashed #787878; margin:auto;">
                            <img id="preZoomImage" src="" alt="" style="width: 100%; height: 100%; display: none;" /></div>
                    </div>
                    <div>
                        <asp:FileUpload ID="FileUpload1" Width="100%" runat="server" onchange="displayImages(this)" /></div>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    移动电话&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">务必填写员工的最新手机号码，并用于与系统通讯。</span>
                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="15" class="easyui-validatebox"
                        validType="mobile" required="true" invalidMessage="请输入有效的手机号码！"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    性别&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">务必填写员工的性别</span>
                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                        <asp:ListItem Value="0">女</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    QQ号码&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请填写员工的有效QQ号码</span>
                    <asp:TextBox ID="txtQQNumber" runat="server" MaxLength="15" CssClass="easyui-validatebox"
                        validType="qq" invalidMessage="您输入的QQ号码不正确！" required="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    工作电话&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">工作电话是员工的联系方式。</span>
                    <asp:TextBox ID="txtTelephone" runat="server" Columns="30" MaxLength="15" CssClass="easyui-validatebox"
                        validType="phone" required="true"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    职称&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">职称。<span style="padding-left:150px;">民族。</span></span>
                  <asp:TextBox Width="162" ID="ui_Titles" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>  
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>民族</b>&nbsp;&nbsp; <asp:TextBox Width="162" ID="ui_Ethnic" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>                
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    学历&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">最高学历。<span style="padding-left:122px;">所学专业。</span></span>
                    <asp:DropDownList ID="ui_edu" runat="server">
                    </asp:DropDownList><b style="padding-left:116px;">专业</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_Prof" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    外语语种&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">第一外语。<span style="padding-left:122px;">外语等级。</span></span>
                   <asp:TextBox Width="162" ID="ui_ForeignL" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>等级</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_Rating" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    婚否&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">婚姻状况。<span style="padding-left:105px;">健康状况。</span></span>
                    <asp:DropDownList ID="ui_Marital" runat="server">
                      <asp:ListItem Value="0" Text="未婚"></asp:ListItem>
                      <asp:ListItem Value="1" Text="已婚"></asp:ListItem>
                    </asp:DropDownList><b style="padding-left:105px;">健康状况</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_Health" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    籍贯&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">籍贯。<span style="padding-left:112px;">户口所在地。</span></span>
                   <asp:TextBox Width="124" ID="ui_jg" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>户口所在地</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_hkd" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    居住地址&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">工作电话是员工的联系方式。</span>
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="30" Columns="60" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    员工备注&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">员工备注是关于员工的特殊经验与经历。</span> &nbsp;<asp:TextBox ID="txtContent" runat="server"
                        Columns="60" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    排序&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">排序号是在部门内部的排序数，主要用于显示排列。</span>
                    <asp:TextBox ID="txtSort" runat="server" MaxLength="3" Columns="10" class="easyui-validatebox"
                        validType="number" required="true" validMessage="请输入数字！"></asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交保存" OnClick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="button" Text="取消重填" />
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
