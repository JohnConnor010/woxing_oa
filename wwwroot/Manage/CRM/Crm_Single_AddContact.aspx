<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_Single_AddContact.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Single_AddContact"
    ClientIDMode="Static" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.datebox.js"></script>
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Modi" CurIndex="5" Param1="{Q:CustomerID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr>
            <td colspan="12"><span style="float: left; font-style: italic; padding-left: 0px; font-size: +1;">联系人列表</span></td>
            </tr>
            <tr class="">
                <td style="width:80px;">
                    姓名
                </td>
                <td style="width:40px;">
                    性别
                </td>
                <td style="width:80px;">
                    部门
                </td>
                <td style="width:80px;">
                    职务
                </td>
                <td width="100">
                    家庭电话
                </td>
                <td width="100">
                    手机号码
                </td>
                <td width="100">
                    工作电话
                </td>
                <td width="80">
                    出生日期
                </td>
                <td width="60">
                    主联系人
                </td>
                <td width="100">
                状态
                </td>
                <td style="width: 120px">
                    管理
                </td>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="ContactTempRepeater" runat="server">
            <ItemTemplate>
            <tr class="">
                <td class="vtip">
                    <img alt="" src="/images/user.gif" />
                    <span><a href="#"><strong><%#Eval("ContactName") %></strong></a></span>
                </td>
                <td class="vtip">
                    <%#Eval("Sex") %>
                </td>
                <td class="vtip">
                    <%#Eval("Dept") %>
                </td>
                <td class="vtip">
                    <%#Eval("Duty") %>
                </td>
                <td>
                    <%#Eval("FamilyPhone") %>
                </td>
                <td>
                    <%#Eval("MobilePhone") %>
                </td>
                <td>
                    <%#Eval("WorkPhone") %>
                </td>
                <td>
                    <%#String.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(Eval("Birthday")))%>
                </td>
                <td>
                    <%#Convert.ToInt32(Eval("IsMain")) == 0 ? "否" : "是" %>
                </td>
                <td>
                    <%#"审核中（"+(Eval("State").ToString()=="-1"?"删除":(Eval("State").ToString()=="2"?"修改":"新添"))+"）" %>
                </td>
                <td class="manage">
                    <a href="?PageMode=my&Action=Edit&&ContactTempID=<%#Eval("ID") %>&CustomerID=<%#Request.QueryString["CustomerID"] %>" class="show">编辑</a> 
                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('删除后不可恢复，确定要删除吗？')" Text="删除" OnCommand="btnDelete_Command"></asp:LinkButton>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            
            <asp:Repeater ID="ContactRepeater" runat="server">
            <ItemTemplate>
            <tr class="">
                <td class="vtip"><img alt="" src="/images/user.gif" />
                    <span><a href="#"><strong><%#Eval("ContactName") %></strong></a></span>
                </td>
                <td class="vtip">
                    <%#Eval("Sex") %>
                </td>
                <td class="vtip">
                    <%#Eval("Dept") %>
                </td>
                <td class="vtip">
                    <%#Eval("Duty") %>
                </td>
                <td>
                    <%#Eval("FamilyPhone") %>
                </td>
                <td>
                    <%#Eval("MobilePhone") %>
                </td>
                <td>
                    <%#Eval("WorkPhone") %>
                </td>
                <td>
                    <%#Eval("Birthday")==Convert.DBNull?"":String.Format("{0:yyyy-MM-dd}",Convert.ToDateTime(Eval("Birthday")))%>
                </td>
                <td>
                    <%#Convert.ToInt32(Eval("IsMain")) == 0 ? "否" : "是" %>
                </td>
                <td>
                    有效
                </td>
                <td class="manage">
                    <a href="?PageMode=my&Action=Edit&&ContactID=<%#Eval("ID") %>&CustomerID=<%#Request.QueryString["CustomerID"] %>" class="show">编辑</a> 
                    <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("ID") %>' Text="申请删除" OnCommand="btnDeletetemp_Command"></asp:LinkButton>
                </td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
            
        </tbody>
    </table>
    <br />
    <table class="table">
        <thead>
            <tr class="">
                <td colspan="2">
                    客户编号：
                    <asp:Literal ID="lblCustomerID" runat="server"></asp:Literal>
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 135px;">
                    &nbsp;* 姓名：
                </th>
                <td>
                    <span class="note" style="display: none;">联系人姓名</span>
                    <asp:TextBox ID="txtContactName" runat="server" dataType="Require" msg="联系人为必填项！"></asp:TextBox>
                &nbsp;
                    <asp:CheckBox ID="cbIsMain" runat="server" Text="主联系人" />
                    <asp:Label ID="Label1" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    &nbsp;* 性别：
                </th>
                <td>
                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True">男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    &nbsp;* 部门：
                </th>
                <td>
                    <asp:TextBox ID="txtDept" runat="server" Width="100px" Require="true" dataType="Require" msg="部门为必填项！"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label3" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    &nbsp;* 职务：
                </th>
                <td>
                    <asp:TextBox ID="txtDuty" runat="server" Width="100px" Require="true" dataType="Require" msg="职务为必填项！"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label4" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    电子邮件：
                </th>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Columns="30" Require="false" dataType="Email" msg="电子邮件格式不正确！"></asp:TextBox>
                    
                    <asp:Label ID="Label5" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    家庭电话：
                </th>
                <td>
                    <asp:TextBox ID="txtFamilyPhone" runat="server" Require="false" dataType="Phone" msg="电话号码格式不正确！"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    手机号码：
                </th>
                <td>
                    <asp:TextBox ID="txtMobilePhone" runat="server" Require="false" dataType="Mobile" msg="手机号码格式不正确！"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    传真号码：
                </th>
                <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    工作电话：
                </th>
                <td>
                    <asp:TextBox ID="txtWorkPhone" runat="server" Require="false" dataType="Phone" msg="电话号码格式不正确！"></asp:TextBox>
                    <asp:Label ID="Label9" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    出生日期：
                </th>
                <td>
                    <asp:TextBox ID="txtBirthday" runat="server" class="easyui-datebox"></asp:TextBox>
                    <asp:Label ID="Label10" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    个人兴趣爱好：
                </th>
                <td>
                    <asp:TextBox ID="txtHobby" runat="server" Columns="70"></asp:TextBox>
                    <asp:Label ID="Label11" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    子女性别及出生日期：
                </th>
                <td>
                    <asp:DropDownList ID="ddlBabySex" runat="server">
                        <asp:ListItem>暂无子女</asp:ListItem>
                        <asp:ListItem>男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtBabyBirthday" runat="server" class="easyui-datebox"></asp:TextBox>
                    <asp:Label ID="Label12" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    工作地点：
                </th>
                <td>
                    <asp:TextBox ID="txtWorkAddress" runat="server" Columns="60"></asp:TextBox>
                    <asp:Label ID="Label13" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    家庭地址：
                </th>
                <td>
                    <asp:TextBox ID="txtFamilyAddress" runat="server" Columns="100"></asp:TextBox>
                    <asp:Label ID="Label14" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    名片图片：
                </th>
                <td>
                    <asp:HiddenField ID="hidden_CardPath" runat="server" />
                    <asp:TextBox ID="txtCardPath" runat="server" Columns="50"></asp:TextBox>
                    &nbsp;&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SingleFileUpload.aspx','上传名片图片','hidden_CardPath','txtCardPath',430,110);">上传</a>
                    
                    <asp:Label ID="Label15" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    个人头像：
                </th>
                <td>
                    <asp:HiddenField ID="hidden_PhotoPath" runat="server" />
                    <asp:TextBox ID="txtPhotoPath" runat="server" Columns="50"></asp:TextBox>
                    &nbsp;&nbsp; ╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SingleFileUpload.aspx','上传个人头像图片','hidden_PhotoPath','txtPhotoPath',430,110);">上传</a>
                    
                    <asp:Label ID="Label16" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    其他信息&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">备注</span>
                    <asp:TextBox ID="txtRemarks" runat="server" Columns="60" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="Label17" runat="server" Text="" CssClass="tip"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px;">
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" OnClick="btnSubmit_Click" OnClientClick="return Validator.Validate(this.form,3);"
                        Text="确定添加" />
                    &nbsp;
                    <asp:Button ID="btnEdit" runat="server" CssClass="button" Text="修改联系人" 
                        onclick="btnEdit_Click" />
&nbsp;
                    <input id="Reset1" class="button" type="reset" value="取消添加" />
                </td>
            </tr>
        </tbody>
    </table>
    
</asp:Content>
