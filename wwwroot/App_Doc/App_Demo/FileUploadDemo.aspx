<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadDemo.aspx.cs" Inherits="wwwroot.App_Demo.FileUploadDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../App_Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btnSubmit').click(function () {
                var idlist = $('#idlist').val();
                var namelist = $('#namelist').val();
                alert("idlist=" + idlist + ",namelist=" + namelist);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>上传demo</h1>
        <input type="hidden" id="idlist" name="idlist" />
        <input type="hidden" id="namelist" name="namelist" />
        <iframe src="../Manage/include/FileUpload.htm" width="700" height="200" scrolling="auto" frameborder="no"></iframe>
        <input type="button" id="btnSubmit" value="提交" />
    </div>
    </form>
</body>
</html>
