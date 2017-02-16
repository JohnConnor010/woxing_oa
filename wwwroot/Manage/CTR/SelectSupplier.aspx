<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectSupplier.aspx.cs" Inherits="wwwroot.Manage.CTR.SelectSupplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#lstSuppliers').change(function () {
                var supplier = $('#lstSuppliers').find("option:selected").text();
                $('#hidden_value', window.parent.document).val(supplier);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="margin:5px">
            <tr>
                <td>类别：</td>
                <td>
                    <asp:DropDownList ID="ddlCategoryID" runat="server" Width="240px" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlCategoryID_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>名称：</td>
                <td>
                    <asp:ListBox ID="lstSuppliers" runat="server" Width="240px" Height="170"></asp:ListBox>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
