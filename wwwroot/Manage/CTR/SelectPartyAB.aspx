<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectPartyAB.aspx.cs"
    Inherits="wwwroot.Manage.CTR.SelectPartyAB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#tt').tabs({
                border: false,
                onSelect: function (title) {
                    $('#hidden_title').val(title);
                }
            });
        });
        function SetSelectedTab(title) {
            if ($('#tt').tabs('exists', title)) {
                $('#tt').tabs('select', title);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hidden_title" />
    <input type="hidden" id="hidden_value" />
    <div id="tt" class="easyui-tabs" style="width: 350px; height: 260px;">
        <div title="选择公司" data-options="closable:true" style="overflow:hidden">
            <iframe  frameborder="0" src="SelectCompany.aspx" style="width: 100%; height: 100%;" id="CompanyFrame"></iframe>
        </div>
        <div title="选择人员" data-options="closable:true" style="overflow:hidden">
            <iframe  frameborder="0" src="SelectEmployee.aspx" style="width: 100%; height: 100%;" id="EmployeeFrame"></iframe>
        </div>
        <div title="选择供应商" data-options="iconCls:'icon-reload',closable:true" style="overflow:hidden">
            <iframe  frameborder="0" src="SelectSupplier.aspx" style="width: 100%; height: 100%;" id="SupplierFrame"></iframe>
        </div>
        <div title="选择客户" data-options="iconCls:'icon-reload',closable:true" style="overflow:hidden">
            <iframe  frameborder="0" src="SelectCustomer.aspx" style="width: 100%; height: 100%;" id="CustomerFrame"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
