<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_Single_ShowCustomer.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Single_ShowCustomer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript">
        function ViewContact(contactId) {
            var diag = new Dialog();
            diag.Width = 600;
            diag.Height = 500;
            diag.Title = "联系人详细信息";
            diag.URL = 'Crm_ShowContact.aspx?ContactID=' + contactId;
            diag.show();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("a.help").hide();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >>
    <asp:Label runat="server" ID="lblTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Modi" CurIndex="3" Param1="{Q:CustomerID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    客户基本信息&nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    * 客户编号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtCustomerID" runat="server" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    * 客户全称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td style="width: 250px;">
                    <asp:Label ID="txtCustomerName" runat="server"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    &nbsp;客户简称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtCustomerZJM" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    客户企业性质：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">客户企业性质 </span>
                    <asp:Label ID="ddlCompanyNature" runat="server"></asp:Label>
                    &nbsp;
                </td>
                <th style="width: 135px; font-weight: bold;">
                    客户行业：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">客户行业 </span>
                    <asp:Label ID="ddlIndustry" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    客户分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="ddlCustomerCategory" runat="server"></asp:Label>
                    &nbsp;
                </td>
                <th style="width: 135px; font-weight: bold;">
                    客户来源：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="ddlSource" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    所在区域：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                    <asp:Label ID="txtAddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    公司网址：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:Label ID="txtWebSite" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    客户企业照片：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <span class="note" style="display: none;">员工的个人照片 </span>
                    <asp:HiddenField ID="hidden_imagePath" runat="server" />
                    <asp:Label ID="txtImagePath" runat="server" Columns="50" ReadOnly="true"></asp:Label>
                </td>
            </tr>
        </tbody>
        <thead>
            <tr style="height: 40px; vertical-align: bottom;">
                <td colspan="4">
                    客户身份信息
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    公司成立日期：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtEstablishmentDate" runat="server"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    法人代表：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">请选择员工 相应的职能部门 </span>
                    <asp:Label ID="txtRealName" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    开户银行：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtBankName" runat="server"></asp:Label>
                </td>
                <th style="width: 135px; font-weight: bold;">
                    银行账户：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtBankAccount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    工商税务登记号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:Label ID="txtBusinessCircles" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold;">
                    主营产品：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:Label ID="txtProducts" runat="server"></asp:Label>(以逗号隔开)
                </td>
            </tr>
        </tbody>
        <thead>
            <tr style="height: 40px; vertical-align: bottom;">
                <td colspan="4">
                    联系人
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td colspan="4">
                    <table width="100%" class="table1">
                        <tr class="title">
                            <td>
                                联系人
                            </td>
                            <td>
                                部门/职务
                            </td>
                            <td>
                                个人电话
                            </td>
                            <td>
                                工作电话
                            </td>
                            <td>
                                家庭电话
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptContacts">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <img src="/images/user.gif" alt="" />
                                        <a <%#Eval("IsMain").ToString()=="1"?" style='color:red;' title='主联系人'":"" %> onclick='ViewContact("<%#Eval("ID") %>")'
                                            href="javascript:void(0)">
                                            <%#Eval("ContactName") %></a> &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label113" runat="server" Text='<%#String.Format("{0}&nbsp;/&nbsp;{1}",Eval("Dept"),Eval("Duty")) %>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label112" runat="server" Text='<%#Eval("MobilePhone") %>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label111" runat="server" Text='<%#Eval("WorkPhone") %>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label110" runat="server" Text='<%#Eval("FamilyPhone") %>'></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </tbody>
        <thead>
            <tr style="height: 40px; vertical-align: bottom;">
                <td colspan="4">
                    客户业务信息
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    业务合作分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="ddlCoop" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:CheckBoxList runat="server" ID="cblCoop" RepeatLayout="Table" RepeatDirection="Horizontal"
                        RepeatColumns="6" CellPadding="6" BorderStyle="Dashed">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    客户跟踪阶段：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="ddlStage" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:CheckBoxList runat="server" ID="cblStage" RepeatLayout="Table" RepeatDirection="Horizontal"
                        RepeatColumns="6" CellPadding="6" BorderStyle="Dashed">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    近期消费总额：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtLastConsumptionMoney" runat="server"></asp:Label>
                    &nbsp;元
                </td>
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    <a style=" color: #008800;" href="/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=<%=Request["PageMode"]  %>&CustomerID=<%=WX.Request.rCustomerID  %>&fee=1">近期维护费用：</a>&nbsp;
                </th>
                <td>
                    <asp:Label ID="txtLastMaintainMoney" runat="server"></asp:Label>
                    &nbsp;元
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    近期合作时间：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    &nbsp;从&nbsp;<asp:Label ID="tCoolRecentStart" runat="server"></asp:Label>&nbsp;到&nbsp;
                    <asp:Label ID="tCoolRecentEnd" runat="server"></asp:Label>
                    &nbsp;
                </td>
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    应收账款：
                </th>
                <td>
                    <asp:Label ID="txtPreMoney" runat="server"></asp:Label>&nbsp;元 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span  style="width: 135px; font-weight: bold; color: #008800;">催缴时间：</span><asp:Label
                        ID="tAskPreMoneyDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="">
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    累计消费总额：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <asp:Label ID="txtConsumptionMoney" runat="server"></asp:Label>
                    &nbsp;元
                </td>
                <th style="width: 135px; font-weight: bold; color: #008800;">
                    <a style=" color: #008800;" href="/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=<%=Request["PageMode"]  %>&CustomerID=<%=WX.Request.rCustomerID  %>&fee=1">累计维护费用：</a>&nbsp;
                </th>
                <td>
                    <asp:Label ID="txtMaintainMoney" runat="server"></asp:Label>
                    &nbsp;元
                </td>
            </tr>
            </body>
            <thead>
                <tr style="height: 40px; vertical-align: bottom;">
                    <td colspan="4">
                        <div style="float: left; width: 90%;">
                            近期跟踪</div>
                        <div style="float: left; text-align: right;">
                            <a style="border: 0px; color: #666;" href="/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=my&CustomerID=<%=WX.Request.rCustomerID %>">
                                >>更多</a></div>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="4">
                        <table width="100%" class="table1">
                            <asp:Repeater runat="server" ID="Repeater1">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <img src='/images/procstate<%# Eval("ProcessState")%>.bmp' />
                                            <a href="javascript:PopupIFrame('Crm_SingleM_ShowTrack.aspx?TrackID=<%# Eval("Id")%>','提交跟踪信息','','',700,450)">
                                                <%# WX.CRM.Track.ProcessState[Convert.ToInt32(Eval("ProcessState"))]%></a><font color="gray">(<%# WX.Main.GetTimeEslapseStr(Convert.ToDateTime(Eval("TrackTime")),"","") %>)</font>&nbsp;
                                        </td>
                                        <td>
                                            <%# string.Format("{0:C2}",Eval("Fee"))%>&nbsp;
                                        </td>
                                        <td>
                                            <%# Eval("State").ToString()=="1"?"已执行":"未执行"%>&nbsp;
                                        </td>
                                        <td>
                                            <%# Convert.ToDateTime(Eval("TrackTime")).ToString("yyyy-MM-dd HH:mm:ss")%>&nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
            </tbody>
            <thead>
                <tr style="height: 40px; vertical-align: bottom;">
                    <td colspan="4">
                        其它信息
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    公司简介&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:Literal ID="txtRemarks" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px; font-weight: bold;">
                    特殊说明&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td colspan="3">
                    <asp:Literal ID="txtSpecialDesc" runat="server"></asp:Literal>
                    &nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
