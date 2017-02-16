<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crm_ShowCustomerInfo.aspx.cs"
    Inherits="wwwroot.Manage.CRM.Crm_ShowCustomerInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
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
    <style type="text/css">
        .style1
        {
            width: 265px;
        }
        .style2
        {
            width: 119px;
            background-color: #fff;
            border-width: 1px;
        }
        tr.title
        {
             font-style:italic;
             background-color:#eee;
        }
    </style>
</head>
<body id="C_News">
    <form id="form1" runat="server">
    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                    <td>
                        客户名称
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblCustomerName" runat="server"></asp:Label><asp:Literal ID="lblCustomerZJM"
                            runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblTypeString" runat="server"></asp:Label>
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px;">
                    客户编号：
                </th>
                <td class="style1">
                    <span id="UDepName">
                    <asp:Label ID="lblCustomerID" runat="server"></asp:Label>&nbsp;<a href="#" style="background:#eee; padding:1px 1px 1px 1px;border:dashed 1px #888; font-size:11px; color:#555; ">复制</a>
                    </span>
                </td>
                <td class="style2">
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    所在地址：
                </th>
                <td class="style1">
                <asp:Literal ID="liAddress" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    公司网址：
                </th>
                <td colspan="3">
                    <span id="Status">
                        <asp:HyperLink ID="hyWebSite" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    客户主营产品：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblProducts" runat="server"></asp:Literal>
                </td>
            </tr>
            <thead>
            <tr>
                <th style="width: 135px;">
                    联系人：
                </th>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            </thead>
            <tr>
                <td  colspan="4">
                    <table width="100%">
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
                                    <td><img src="/images/user.gif" alt="" />
                                    <a <%#Eval("IsMain").ToString()=="1"?" style='color:red;' title='主联系人'":"" %> href='<%#Eval("ID","/Manage/CRM/Crm_ShowContact.aspx?ContactId={0}") %>'><%#Eval("ContactName") %></a>
                                       &nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%#String.Format("{0}&nbsp;/&nbsp;{1}",Eval("Dept"),Eval("Duty")) %>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label runat="server" Text='<%#Eval("MobilePhone") %>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("WorkPhone") %>'></asp:Label>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("FamilyPhone") %>'></asp:Label>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <thead>
            <tr>
                <th style="width: 135px;">
                    业务情况：
                </th>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblStage"></asp:Label>
                    <asp:Label runat="server" ID="lblBuyHabbit"></asp:Label>
                </td>
            </tr>
            </thead>
            <tr>
                <td colspan="4">
                    <table style="width: 100%;">
                        <caption style="text-align: left; font-weight: bold;">
                            跟踪记录</caption>
                        <tr class="title">
                            <td style="width:100px;">
                                日期
                            </td>
                            <td style="width:70px;">
                                跟踪行为
                            </td>
                            <td>
                                跟踪详情
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="Repeater1">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# String.Format("{0:yyyy-MM-dd}<br/>{0:HH:mm:ss}", Eval("TrackTime"))%>&nbsp;
                                    </td>
                                    <td><%#WX.CRM.Track.ProcessState[Convert.ToInt32(Eval("ProcessState"))]%>&nbsp;
                                    </td>
                                    <td><b>重点：</b><%# Eval("Remarks").ToString().Split('|')[0]%><br /><b>难点：</b><%# Eval("Remarks").ToString().Split('|')[1]%><br /><b>解决：</b><%# Eval("Remarks").ToString().Split('|')[2] %>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%;">
                        <caption style="text-align: left; font-weight: bold;">
                            签约情况</caption>
                        <tr class="title">
                            <td style="width:100px;">
                                日期
                            </td>
                            <td>
                                签约行为
                            </td>
                            <td>
                                跟踪详情
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="Repeater2">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Convert.ToDateTime(Eval("TrackTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss") %>&nbsp;
                                    </td>
                                    <td><%#WX.CRM.Track.ProcessState[Convert.ToInt32(Eval("ProcessState"))]%>&nbsp;
                                    </td>
                                    <td><b>重点：</b><%# Eval("Remarks").ToString().Split('|')[0]%><br /><b>难点：</b><%# Eval("Remarks").ToString().Split('|')[1]%><br /><b>解决：</b><%# Eval("Remarks").ToString().Split('|')[2] %>&nbsp;
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <table style="width: 100%;">
                        <caption style="text-align: left; font-weight: bold;">
                            维护记录（本年维护累计：6200元，本月维护累计：1200元。）</caption>
                        <tr class="title">
                            <td style="width:100px;">
                                日期
                            </td>
                            <td>
                                维护行为
                            </td>
                            <td>
                                维护描述
                            </td>
                            <td>
                                花销
                            </td>
                        </tr>
                        <tr>
                            <asp:Repeater runat="server" ID="Repeater3">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Convert.ToDateTime(Eval("TrackTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss") %>&nbsp;
                                    </td>
                                    <td><%#WX.CRM.Track.ProcessState[Convert.ToInt32(Eval("ProcessState"))]%>&nbsp;
                                    </td>
                                    <td><b>重点：</b><%# Eval("Remarks").ToString().Split('|')[0]%><br /><b>难点：</b><%# Eval("Remarks").ToString().Split('|')[1]%><br /><b>解决：</b><%# Eval("Remarks").ToString().Split('|')[2] %>&nbsp;
                                    </td>
                            <td>
                                <%#Eval("Fee")%>&nbsp;
                            </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        </tr>
                    </table>
                </td>
            </tr>
            <thead>
                <tr>
                    <th>
                        特殊信息
                    </th>
                    <td colspan="3">
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px;">
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblRemark" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="4" style=" text-align:center;">
                    <div style="width:100px; margin:0 auto;" class="manage">
                    <a href="javascript:parentDialog.close()">关闭本页</a></div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
