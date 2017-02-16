<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEmailContent.aspx.cs" Inherits="wwwroot.Manage.Email.ViewEmailContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="easyui1.4/jquery.min.js"></script>
    <style type="text/css">
        #loading {
            position: fixed;
            _position: absolute;
            top: 50%;
            left: 50%;
            width: 124px;
            height: 124px;
            overflow: hidden;
            background: url(loaderc.gif) no-repeat;
            z-index: 7;
            margin: -62px 0 0 -62px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hideSubject" runat="server" />
        <input type="hidden" id="hideDate" runat="server" />
        <div>
            <div style="text-align: center">
                <h2>
                    <code id="subject"></code>
                </h2>
            </div>
            <hr style="display: none" id="hr1"/>
            <div style="height:450px;overflow:auto;">
                <p id="content"></p>
            </div>
        </div>
    </form>

    <div id="loading"></div>
    <script src="easyui1.4/jquery.postback.js"></script>
    <script type="text/javascript">
        $(function () {
            var subject = $("#hideSubject").val();
            var date = $("#hideDate").val();
            var object = new Object();
            object.Subject = subject;
            object.Date = date;
            var json = JSON.stringify(object);
            $.doPostback("SearchSingleEmail", { subject:subject,date:date }, function (result) {
                if (result.d != "") {
                    var json = $.parseJSON(result.d);
                    $("#subject").text(json.Subject);
                    $("#hr1").show();
                    $("#content").html(json.Body);
                    jQuery.noConflict();
                    jQuery("#loading").fadeOut();
                }
            }, {
                show_loading:false
            })
        });
    </script>
</body>
</html>
