<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTextTemplate.aspx.cs" Inherits="wwwroot.Manage.Main.EditTextTemplate" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function hide() {
            window.parent.document.getElementById("dialogCase").style.display = 'none'; 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <FCKeditorV2:FCKeditor ID="ui_content" ToolbarSet="Default" runat="server" Height="350"
            Width="830">
        </FCKeditorV2:FCKeditor>
    </div>
    <div style="text-align: center; padding-top:5px;">
        <asp:Button runat="server" ID="btnSubmit" Text="确定修改" OnClick="Modi" OnClientClick="hide();" />
    </div>
    </form>
</body>
</html>
