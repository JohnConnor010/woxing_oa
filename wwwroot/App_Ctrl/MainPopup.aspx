<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPopup.aspx.cs" Inherits="wwwroot.App_Ctrl.MainPopup" ClientIDMode="Static" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript" src="../App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="../App_Scripts/popup.js"></script>
    <script type="text/javascript">/*
        function PopupIFrame(url,title,hidden,output,width,height) {
            var pop = new Popup({ contentType: 1, scrollType: 'no', isReloadOnClose: false, width: width, height: height });
            if (url.indexOf("?") > 0) {
                if (hidden != "" && hidden != undefined) {
                    url += "&HiddenField=" + hidden;
                }
                if (output != "" && output != undefined) {
                    url += "&OutputField=" + output;
                }
                if ($('#' + hidden).val() != "") {
                    url += "&Params=" + $('#' + hidden).val();
                }
            }
            else {
                if (hidden != "" && hidden != undefined) {
                    url += "?HiddenField=" + hidden;
                }
                if (output != "" && output != undefined) {
                    url += "&OutputField=" + output;
                }
                if ($('#' + hidden).val() != "") {
                    url += "&Params=" + $('#' + hidden).val();
                }
            }
            pop.setContent("contentUrl", url);
            pop.setContent("title", title);
            pop.build();
            pop.show();
        }*/
        $(function () {
            $('#Button1').click(function () {
                var value = $('#hidden_users').val();
                alert(value);
            });
            $('#Button2').click(function () {
                var value = $('#hidden_departments').val();
                alert(value);
            });
            $('#Button3').click(function () {
                var value = $('#hidden_roles').val();
                alert(value);
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    授权范围(人员)：</td>
                <td>
                    <input type="hidden" id="hidden_users" name="hidden_users" value="" />
                    <asp:TextBox ID="txtUsers" runat="server" Columns="40" ReadOnly="True" 
                        Rows="10" TextMode="MultiLine"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('SelectPeople.aspx?CompanyId=11','选择人员','hidden_users','txtUsers',468,395);">选择</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button1" type="button" value="获取Hidden的值" /></td>
            </tr>
            <tr>
                <td>
                    授权范围(部门)：</td>
                <td>
                    <input type="hidden" id="hidden_departments" name="hidden_departments" />
                    <asp:TextBox ID="txtDepartments" runat="server" Columns="40" ReadOnly="True" 
                        Rows="10" TextMode="MultiLine"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('SelectDepartment.aspx?CompanyId=11','选择部门','hidden_departments','txtDepartments',430,300)">选择</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="获取Hidden的值" /></td>
            </tr>
            <tr>
                <td>
                    授权范围（角色）：</td>
                <td>
                    <input type="hidden" id="hidden_roles" name="hidden_roles" />
                    <asp:TextBox ID="txtRoles" runat="server" Columns="40" ReadOnly="True" 
                        Rows="10" TextMode="MultiLine"></asp:TextBox>
&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('SelectRoles.aspx?CompanyID=11','选择角色','hidden_roles','txtRoles',430,300)">选择</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button3" type="button" value="获取Hidden的值" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
