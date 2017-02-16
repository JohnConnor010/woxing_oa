<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRM_SingleM_EditAgreement.aspx.cs"
    Inherits="wwwroot.Manage.CRM.CRM_SingleM_EditAgreement" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 18cm; border: 0px solid #888888;" align="center">
            <tr style="text-align: center;">
                <td valign="top">
                    <FCKeditorV2:FCKeditor ID="FORM_CONTENT" runat="server" Height="400" Width="100%" Value="">
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr><td>
                <asp:Button ID="Button1" runat="server" Text="保存并预览" onclick="Button1_Click" />
            </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
