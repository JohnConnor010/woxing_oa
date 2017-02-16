<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_EditTask.aspx.cs"
    Inherits="wwwroot.Manage.Plan.Plan_EditTask" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <script type="text/javascript">
        function butsumit()
        {
            alert("任务提交成功！");
            window.parent.location="Plan_Task.aspx?PlanId=<%=WX.Request.rPlanId %>&methed=1";
            window.parent.document.getElementById("dialogCase").style.display = 'none';
        }
        <%=mes %>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="table3">
        <tr>
            <td width="50">
                <b>标题:</b>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="360"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>用时:</b>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>&nbsp;<font color="#888888">
                    例：5天、2小时</font>
            </td>
        </tr>
        <tr>
            <td>
                <b>描述：</b>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Rows="4" Width="360"></asp:TextBox><asp:Button
                    ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
