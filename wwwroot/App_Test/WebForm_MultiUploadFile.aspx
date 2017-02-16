<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_MultiUploadFile.aspx.cs" Inherits="wwwroot.App_Test.WebForm_MultiUploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../App_Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../App_Scripts/jquery.MultiFile.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" class="multi" />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload All" OnClick="btnUpload_Click" />
        </div>
    </div>
    </form>
</body>
</html>
