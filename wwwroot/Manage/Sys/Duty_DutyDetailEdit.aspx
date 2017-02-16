<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Duty_DutyDetailEdit.aspx.cs"
    ClientIDMode="Static" Inherits="wwwroot.Manage.Sys.Duty_DutyDetailEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="table">
            <tr>
                <td style="width: 80px; text-align: right; font-weight: bold;">
                    部门:
                </td>
                <td>
                    <asp:DropDownList ID="ddlParentId" runat="server" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; font-weight: bold;">
                    职务:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDuty" runat="server" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; font-weight: bold;">
                    职务分类:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDutyCatagory" runat="server" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; font-weight: bold;">
                    级别:
                </td>
                <td>
                    <asp:DropDownList ID="ui_grade" runat="server" Style="width: 250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; font-weight: bold;">
                    简称:
                </td>
                <td>
                    <asp:TextBox ID="ui_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; font-weight: bold;">
                    限制人数:
                </td>
                <td>
                    <asp:TextBox ID="ui_persons" runat="server" Text="0" Width="40"></asp:TextBox>人
                </td>
            </tr>
            <tr id="users" runat="server" visible="false">
                <td style="text-align: right; font-weight: bold;">
                    人员列表:
                </td>
                <td>
                    <asp:Label ID="ui_users" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; font-weight: bold;">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="确定" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript" src="/App_Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="/App_Scripts/QueryString.js"></script>
    <script type="text/javascript">
        var url="javascript:PopupIFrame('Duty_DutyDetailEdit.aspx?DutyDetailID=<%=id %>','编辑具体职务','sdf','sdf',468,240)";
        var label = "li_" + document.getElementById("ddlParentId").value + "_" + document.getElementById("ddlDutyCatagory").value;
    function sub() {
        var dll = "ddl_" + document.getElementById("ddlParentId").value +"_"+ document.getElementById("ddlDutyCatagory").value;
        var but="but_" + document.getElementById("ddlParentId").value +"_"+ document.getElementById("ddlDutyCatagory").value;;
        if(document.getElementById("ddlDutyCatagory").value!="5")
        {
        window.parent.document.getElementById(dll).style.display = 'none';
        window.parent.document.getElementById(but).style.display = 'none';
        }
        window.parent.document.getElementById(label).innerHTML ="<%=getdutylist() %>";
        window.parent.document.getElementById("dialogCase").style.display = 'none';
    }
    function sub2() {
        window.parent.document.getElementById(label).innerHTML = "<%=getdutylist() %>";
       window.parent.document.getElementById("dialogCase").style.display = 'none';
    }
        $('#btnClose').click(function () {
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        });
        <%=mes %>
    </script>
</body>
</html>
