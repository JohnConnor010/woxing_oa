<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectEmployee.aspx.cs" Inherits="wwwroot.Manage.CTR.SelectEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#lstEmployees').change(function () {
                var employee = $('#lstEmployees').find("option:selected").text();
                $('#hidden_value', window.parent.document).val(employee);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="margin:5px">
            <tr>
                <td>部门：</td>
                <td>
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="160" 
                        AutoPostBack="True" onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>人员：</td>
                <td>
                    <asp:ListBox ID="lstEmployees" runat="server" Width="160" Height="180"></asp:ListBox>

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
