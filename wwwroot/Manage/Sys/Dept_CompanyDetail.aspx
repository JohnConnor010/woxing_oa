<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_CompanyDetail.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_CompanyDetail" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content0" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 基本信息
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="comp" CurIndex="2" Param1="{Q:CompanyID}" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- 内容模块 -->
    <table class="table" style="line-height: 150%;">
    <caption style="text-align:left; font-style:italic;"><asp:Literal runat="server" ID="liTitle"></asp:Literal></caption>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 公司编号<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">您所在公司编号 2位数字组成</span>
                <asp:Literal ID="txtCompanyNO" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 单位名称<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">您所在公司的名称，比如：我行信息技术有限公司</span>
                <asp:Literal ID="txtCompanyName" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 维护管理责任人<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司信息的维护人员，由指定人员负责修改</span>
                <asp:Literal ID="li_Manage" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 信息更新时间<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司信息上一次修改时间</span>
                <asp:Literal ID="txtUptime" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 法人代表<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <div style="float: left; width: 300px;">
                    <asp:Literal ID="txtFRManage" runat="server"></asp:Literal></div>
                <span><b>公司类型：</b>
                    <asp:Literal ID="txtCtype" runat="server"></asp:Literal></span> &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 董事会成员<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">董事会成员列表</span>
                <asp:Literal ID="txtDSHList" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 成立时间<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <div style="float: left; width: 300px;">
                    <asp:Literal ID="txtSetuptime" runat="server"></asp:Literal></div>
                <span><b>经营期限：</b>
                    <asp:Literal ID="txtOperatetime" runat="server"></asp:Literal></span> &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                经营范围<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司的经营范围</span>
                <asp:Literal ID="txtOperate" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                * 常用银行户名<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <div style="float: left; width: 300px;">
                    <asp:Literal ID="txtbankname" runat="server"></asp:Literal></div>
                <span><b>帐号：</b>
                    <asp:Literal ID="txtbankaccount" runat="server"></asp:Literal></span> &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold; vertical-align:top;"><br />
                证件列表<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">证件列表</span>
                <asp:DataList ID="DataList1" CssClass="table" runat="server" Width="100%">
                    <HeaderTemplate>
                            <b>证件名称</b>
                        </td>
                        <td style="font-weight: bold; width: 15%;">
                            有效期
                        </td>
                        <td style="font-weight: bold; width: 15%;">
                            年审时间
                        </td>
                        <td style="font-weight: bold; width: 10%;">
                            年审状态
                        </td>
                        <td style="font-weight: bold; width: 10%;">
                            年审责任人
                        </td>
                        <% if (this.Master.A_Edit)
                           { %>
                        <td style="font-weight: bold; width: 8%;">
                            下载
                        </td>
                        <%} %>
                    </HeaderTemplate>
                    <ItemTemplate>
                            <a href='Dept_CompanyslicenseEdit.aspx?companyID=<%# Eval("CompanyId")%>&id=<%# Eval("ID")%>'>
                                <%# Eval("Title")%></a>&nbsp;
                        </td>
                        <td style="width: 20%;">
                            <%# Convert.ToDateTime(Eval("Valid").ToString()).ToString("yyyy-MM-dd")%>
                            -
                            <%# Convert.ToDateTime(Eval("Validstop").ToString()).ToString("yyyy-MM-dd")%>
                        </td>
                        <td style="width: 20%;">
                            <%# Convert.ToDateTime(Eval("CheckTime").ToString()).ToString("yyyy-MM-dd") %>
                            -
                            <%# Convert.ToDateTime(Eval("CheckstopTime").ToString()).ToString("yyyy-MM-dd")%>
                        </td>
                        <td style="width: 10%;">
                            <%# Eval("Ischeck").ToString()=="1"?"已审":"未审" %>&nbsp;
                        </td>
                        <td style="width: 10%;">
                            <%# Eval("RealName")%>&nbsp;
                        </td>
                        <% if (this.Master.A_Edit)
                           { %>
                        <td style="width: 8%;">
                            <a href='Dept_AnnexDetail.aspx?id=<%# Eval("Id")%>&aid=0&companyID=<%# Eval("CompanyId")%>'>点击下载</a>
                        </td>
                        <%} %>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                单位简介<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">对您所在的单位的简要介绍</span>
                <asp:Literal ID="txtIntroduction" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                单位电话<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司联系电话，区号-8位号码的形式填写</span>
                <asp:Literal ID="txtTelephone" runat="server"></asp:Literal>
               
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                <a href="#">单位传真</a><a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司传真号码</span>
                <asp:Literal ID="txtFax" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                邮政编码<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">您所在地区的邮编</span>
                <asp:Literal ID="txtZip" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                公司网址<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司网站主页，以http开始</span>
                <asp:Literal ID="txtSite" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                公司邮箱<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司的邮箱地址</span>
                <asp:Literal ID="txtEmail" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                公司地址<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">您所在公司的详细地址</span>
                <asp:Literal ID="txtAddress" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                银行账户<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">公司的银行账户</span>
                <asp:Literal ID="txtAccount" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                乘车路线<a href="#" class="help">[?]</a>&nbsp;：
            </th>
            <td>
                <span class="note">达到公司的乘车路线</span>
                <asp:Literal ID="txtRoute" runat="server"></asp:Literal>
                &nbsp;
            </td>
        </tr>
    </table>
   
    <!-- 内容模块 -->
</asp:Content>
