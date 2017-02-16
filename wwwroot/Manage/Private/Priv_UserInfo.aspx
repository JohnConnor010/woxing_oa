<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Priv_UserInfo.aspx.cs" Inherits="wwwroot.Manage.Sys.Priv_UserInfo" ClientIDMode="Static" EnableEventValidation="false" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    个人资料
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="priv" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
        <caption style=""><div style="float:left;width:100px;">用户重要信息</div><div style="float:right;width:100px;"><asp:HyperLink runat="server" ID="hlInit" NavigateUrl="Priv_EditUser.aspx">修改用户信息</asp:HyperLink></div></caption>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    所在公司：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblCompanyName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    所在部门：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDeptName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    所属职务：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblDutyName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    真实姓名：&nbsp;
                </th>
                <td><asp:Label runat="server" ID="lblRealName"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    身份证号：&nbsp;
                </th>
                <td>
                   <asp:Label runat="server" ID="lblIdCard"></asp:Label>
                   <asp:Label ID="cardannex" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    电子邮件：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblEmail"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table class="table">
            <caption style="text-align: left; font-style: normal;">
                用户基本信息</caption>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    出生日期：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblBirthday"></asp:Label>
                </td>
                <td style="width: 150px; border:1px solid #BDDBEF" rowspan="6">
                    <div style="font-weight:bold;">
                        用户照片</div>
                    <div style="text-align:center; padding:3px 3px 3px 3px;">
                        <div style="width:150; height: 130px; border: 0px dashed #787878; margin:auto; padding:1px 1px 1px 1px;">
                            <asp:Literal runat="server" ID="liPreZoomImage"></asp:Literal>
                            </div>
                    </div>
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    移动电话：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblMoblie"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    性别：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblSex"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    QQ号码：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblQQ"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    工作电话：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblTelephone"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    现居地址：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblAddress"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    家庭地址：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblAddress2"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    员工备注：&nbsp;
                </th>
                <td>
                    <asp:Label runat="server" ID="lblContent"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

