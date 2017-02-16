<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crm_ShowContact.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_ShowContact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
<script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>

</head>
<body id="C_News">
    <form runat="server" id="form1">


    <div id="PanelShow">
        <table class="table">
                <tr class="">
                    <td colspan="3" align="right" style="font-weight:bold;">
                        <u><asp:Literal ID="lblCustomerName" runat="server"></asp:Literal></u>
                    </td>
                </tr>
                <tr>
                    <th style="width: 135px;">
                        <b>联系人名称：</b>
                    </th>
                    <td colspan="3">
                        <span id="UUserName"></span>
                        <b><asp:Literal ID="lblContactName" runat="server"></asp:Literal></b>
                    </td>
                </tr>
            <tr>
                <th style="width: 135px;">
                    性别：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblSex" runat="server"></asp:Literal>
                </td>
                <td class="style2" rowspan="6" style="width:140px; height:158px">
                    <asp:Image ImageUrl="/images/nophoto.gif" runat="server" ID="imgPhoto" style="border-width: 0px; border: solid 1px #999999" Width="140" Height="158" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    年龄：
                </th>
                <td class="style1">
                    <span id="PositionName">
                    
                    </span>
                    <asp:Literal ID="lblAge" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    工作电话：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblWorkPhone" runat="server"></asp:Literal>
&nbsp;</td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    电子邮件：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblEmail" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    家庭电话：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblFamilyPhone" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    手机号码：
                </th>
                <td class="style1">
                    <asp:Literal ID="lblMobilePhone" runat="server"></asp:Literal>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    传真号码：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblFax" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    出生日期：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblBirthday" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    个人兴趣爱好：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblHobby" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    子女性别及出生日期：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblBabySex" runat="server"></asp:Literal>
&nbsp;
                    <asp:Literal ID="lblBabyBirthday" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    工作地点：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblWorkAddress" runat="server"></asp:Literal>
&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    家庭地址：
                </th>
                <td colspan="3">
                    <asp:Literal ID="lblFamilyAddress" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <th style="width: 135px;">
                    其他信息： 
                </th>
                <td colspan="3">
                    <p style="height: 61px">
                        <span id="Notes"></span>
                        
                        <asp:Literal ID="lblRemarks" runat="server"></asp:Literal>
                        
                    </p>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

