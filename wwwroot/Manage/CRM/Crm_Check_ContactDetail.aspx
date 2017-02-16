<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crm_Check_ContactDetail.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Check_ContactDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
     <script type="text/javascript">
    function butsumit()
        {
        alert("信息审核完成！");
            window.parent.location="Crm_Check_CheckCustomer.aspx";
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        }
        <%=mes %>
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
            font-style: italic;
            background-color: #eee;
        }
        .txtinput
        {
            border: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
 <table class="table3">
        <thead>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <b>
                        <center>
                            原始数据</center>
                    </b>
                </td>
                <td>
                    <b>
                        <center>
                            审核数据</center>
                    </b>
                </td>
            </tr>
        </thead>
        <tr>
            <td>
                <b>客户名称：</b>
            </td>
            <td colspan="2">
                <asp:Label ID="liCustomerName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>联系人：</b>
            </td>
            <td>
                <asp:Label ID="liContactName" runat="server"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtContactName" runat="server"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>性别：</b>
            </td>
            <td>
                <asp:Label ID="liSex" runat="server"></asp:Label>
            </td>
            <td>
               <asp:RadioButtonList ID="radSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True">男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                <b>部门：</b>
            </td>
            <td>
                <asp:Label ID="liDept" runat="server"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtDept" runat="server"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>职务：</b>
            </td>
            <td>
                <asp:Label ID="liDuty" runat="server"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtDuty" runat="server"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>电子邮箱：</b>
            </td>
            <td>
                <asp:Label ID="liEmail" runat="server"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtEmail" runat="server"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>家庭电话：</b>
            </td>
            <td>
                <asp:Label ID="liFamilyPhone" runat="server"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtFamilyPhone" runat="server"> </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>手机号码：</b>
            </td>
            <td>
                <asp:Label ID="liMobilePhone" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMobilePhone" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>传真号码：</b>
            </td>
            <td>
                <asp:Label ID="liFax" runat="server"></asp:Label>
            </td>
            <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>工作电话：</b>
            </td>
            <td>
                <asp:Label ID="liWorkPhone" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWorkPhone" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>出生日期：</b>
            </td>
            <td>
                <asp:Label ID="liBirthday" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBirthday" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>个人爱好：</b>
            </td>
            <td>
                <asp:Label ID="liHobby" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtHobby" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>子女情况：</b>
            </td>
            <td>
                <asp:Label ID="liBabySex" runat="server"></asp:Label>
            </td>
            <td>
                 <asp:DropDownList ID="ddlBabySex" runat="server">
                        <asp:ListItem>暂无子女</asp:ListItem>
                        <asp:ListItem>男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtBabyBirthday" runat="server" class="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>工作地点：</b>
            </td>
            <td>
                <asp:Label ID="liWorkAddress" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtWorkAddress" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>家庭地址：</b>
            </td>
            <td>
                <asp:Label ID="liFamilyAddress" runat="server"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFamilyAddress" runat="server" Width="300"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>电子名片：</b>
            </td>
            <td>
                <asp:Label ID="liCardPath" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="tempCardPath" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>个人头像：</b>
            </td>
            <td>
                <asp:Label ID="liPhotoPath" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="tempPhotoPath" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>其它信息：</b>
            </td>
            <td colspan="2">
                <asp:Label ID="liRemarks" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <center>
                    <asp:Button ID="Button1" runat="server" Text="通过" onclick="Button1_Click" />&nbsp;&nbsp;<asp:Button ID="Button2"
                        runat="server" Text="未通过" onclick="Button2_Click" /></center>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
