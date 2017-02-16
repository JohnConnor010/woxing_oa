<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCompany.aspx.cs"
    Inherits="wwwroot.Manage.CTR.SelectCompany" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" href="../css/css.css" />
    <script type="text/javascript" src="../../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ddlCompany').change(function () {
                var company = $('#ddlCompany').find("option:selected").text();
                if (company != "--请选择公司名称--") {
                    $('#hidden_value', window.parent.document).val(company);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="margin: 5px">
            <tr>
                <td>
                    公司名称：
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompany" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
