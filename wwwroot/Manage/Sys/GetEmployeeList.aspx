<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetEmployeeList.aspx.cs" ClientIDMode="Static"
    Inherits="wwwroot.Manage.Sys.GetEmployeeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
    <style type="text/css">
        #norSearch, #advSearch
        {
            background: url("../../images/search_button.png") no-repeat scroll 0 0 transparent;
            height: 33px;
            margin: 3px;
            width: 107px;
        }
        input.toolBtnA, input.toolBtnB, input.toolBtnC
        {
            background: url("../../images/m_button.png") repeat scroll 0 0 transparent;
            border: 0 none;
            color: #1866F4;
            cursor: pointer;
            font-family: 微软雅黑,宋体,sans-serif;
            font-size: 11pt;
            height: 23px;
            text-decoration: none;
            width: 114px;
        }
    </style>
    <script type="text/javascript" src="../../App_Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function CheckSelect(sender) {
            var container = document.getElementsByName("c1");
            $.each(container, function (i, item) {
                container[i].checked = false;
            });
            sender.checked = true;
            var ID = $('input[@name=c1]:checked').val();
            $.ajax({
                type: "get",
                cache: false,
                url: "/App_Services/GetCompanyEmployee.ashx?ID=" + ID,
                success: function (result) {
                    $('#hidden_json').val(result);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="hidden_json" />
    <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                <td>
                        部门
                    </td>
                    <td>
                        职位
                    </td>
                    <td>
                        姓名
                    </td>
                    <td>
                        性别
                    </td>
                    <td>
                        学历
                    </td>
                    <td>
                        手机
                    </td>
                    <td>
                        邮箱
                    </td>
                    <td>
                        身份证号
                    </td>
                    <td>
                        选择
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="AssetsRepeater" runat="server">
                    <ItemTemplate>
                        <td>
                            <%# Eval("deptname")%>&nbsp;
                        </td>
                        <td>
                            <%# Eval("dutyname")%>&nbsp;
                        </td>
                        <td>
                            <%# Eval("RealName")%>&nbsp;
                        </td>
                        <td>
                            <%# Eval("Sex").ToString() == "1" ? "男" : "女"%>
                        </td>
                        <td>
                            <%# Eval("Edu")%>
                        </td>
                        <td>
                            <%# Eval("Mobile")%>
                        </td>
                        <td>
                            <%# Eval("Email")%>
                        </td>
                        <td>
                            <%# Eval("IDCard")%>
                        </td>
                        <td>
                            <span>
                                <input type="checkbox" value='<%#Eval("UserID") %>' name="c1" onclick="CheckSelect(this);" /></span>
                        </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
