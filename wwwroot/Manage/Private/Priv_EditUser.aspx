<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Priv_EditUser.aspx.cs" Inherits="wwwroot.Manage.Private.Priv_EditUser" ClientIDMode="Static" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 员工管理 >> 修改员工信息
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="priv" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table" runat="server" id="tableInit">
        <caption style="text-align:left; font-style:normal;">员工入职必填（面试入职后自动消失）</caption>      
        <tr>
                <th style="width: 135px; font-weight: bold;">
                    拟入职部门&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择入职部门。</span>
                   <asp:DropDownList  ID="ddlDepartment" runat="server" Style="width: 250px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>  
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    拟入职职务&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择拟入职后职务。</span>
                    <asp:DropDownList ID="ui_jobname" runat="server" Style="width: 250px">
                                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr> 
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    拟薪资待遇&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请填写拟薪资待遇。</span>
                    <asp:TextBox ID="ui_salary" runat="server" Width="280"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table class="table">
            <caption style="text-align: left; font-style: normal;">
                用户重要信息</caption>
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
                    <asp:TextBox ID="txtIDCard" runat="server" onchange="getbrigthday();" Columns="30" MaxLength="90" class="easyui-validatebox"
                        validType="idcard" required="true" invalidMessage="请输入有效的身份证号码！"></asp:TextBox>
                    &nbsp;<asp:FileUpload ID="FileUpload2" runat="server" /><asp:Literal ID="Literal1" Text="身份证扫描件"
                        runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    电子邮件&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">电子邮箱是OA系统与员工通讯的方式之一</span>
                    <asp:TextBox ID="txtEmail" runat="server" Columns="30" MaxLength="100" class="easyui-validatebox"
                        validType="email" invalidMessage="您输入的电子邮件格式不正确，请重新输入！" required="true"></asp:TextBox><asp:Button
                            ID="Button1" runat="server" Text="邮件验证" OnClientClick="PopupIFrame('Priv_CheckEmail.aspx','邮箱验证','txtEmail','txtEmail',500,300); return false;" />
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table class="table">
            <caption style="text-align: left; font-style: normal;">
                用户基本信息</caption>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    出生日期&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">务必填写员工的出生日期。</span>
                    <asp:TextBox ID="txtBirthday" runat="server" class="easyui-validatebox" MaxLength="10"
                       required="true" Style="width: 150px"></asp:TextBox>
                    &nbsp;
                </td>
                <td style="width: 150px; border:1px solid #BDDBEF" rowspan="6">
                    <div style="font-weight:bold;">
                        两寸免冠照片(150*130)</div>
                    <div style="text-align:center; padding:3px 3px 3px 3px;">
                        <div style="width:150; height: 130px; border: 0px dashed #787878; margin:auto; padding:1px 1px 1px 1px;">
                            <asp:Literal runat="server" ID="liPreZoomImage"></asp:Literal>
                            </div>
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
                        <asp:ListItem Value="1">男</asp:ListItem>
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
                    <asp:TextBox ID="txtTelephone" runat="server" Columns="30" MaxLength="20" CssClass="easyui-validatebox"
                        validType="phone" required="true"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    职称&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">职称。<span style="padding-left:150px;">学历。</span></span>
                  <asp:TextBox Width="162" ID="ui_Titles" runat="server" MaxLength="20"
                        ></asp:TextBox>  
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>学历</b>&nbsp;&nbsp; <asp:DropDownList ID="ui_edu" runat="server">
                    </asp:DropDownList>&nbsp;<asp:FileUpload ID="FileUpload3" runat="server" /><asp:Literal ID="Literal2" Text="学历证扫描件"
                        runat="server"></asp:Literal>          
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    民族&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">民族。<span style="padding-left:150px;">所学专业。</span></span>
                    <asp:TextBox Width="162" ID="ui_Ethnic" runat="server" MaxLength="20"
                        required="true"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>专业</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_Prof" runat="server" MaxLength="20" required="true"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <th style="width: 135px; font-weight: bold;">
                    外语语种&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">第一外语。<span style="padding-left:122px;">外语等级。</span></span>
                   <asp:TextBox Width="162" ID="ui_ForeignL" runat="server" MaxLength="20"
                         required="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>等级</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_Rating" runat="server" MaxLength="20" required="true"></asp:TextBox>
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
                    </asp:DropDownList><b style="padding-left:105px;">健康状况</b>&nbsp;&nbsp;<asp:TextBox Width="70" ID="ui_Health" runat="server" MaxLength="20" required="true"></asp:TextBox>&nbsp;<asp:FileUpload ID="FileUpload4" runat="server" /><asp:Literal ID="Literal3" Text="健康证扫描件"
                        runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    籍贯&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">籍贯。<span style="padding-left:112px;">户口所在地。</span></span>
                   <asp:TextBox Width="124" ID="ui_jg" runat="server" MaxLength="20"  required="true"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>户口所在地</b>&nbsp;&nbsp;<asp:TextBox Width="162" ID="ui_hkd" runat="server" MaxLength="20"  required="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    居住地址&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">员工的现在居住地址。</span>
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="30" Columns="60" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>&nbsp;&nbsp;<b>邮编</b>&nbsp;&nbsp;<asp:TextBox Width="80" ID="txtaddresscode" runat="server" MaxLength="20"  required="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    家庭地址&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">员工的家庭地址。</span>
                    <asp:TextBox ID="txtaddress2" runat="server" MaxLength="30" Columns="60" CssClass="easyui-validatebox"
                        required="true"></asp:TextBox>&nbsp;&nbsp;<b>邮编</b>&nbsp;&nbsp;<asp:TextBox Width="80" ID="txtaddress2code" runat="server" MaxLength="20"
                        validType="phone" required="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    员工备注&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">员工备注是关于员工的特殊经验与经历。</span> &nbsp;<asp:TextBox ID="txtContent" runat="server"
                        Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox>
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