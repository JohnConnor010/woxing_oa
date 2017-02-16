<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_CheckPlanDetail.aspx.cs"
    Inherits="wwwroot.Manage.Plan.Plan_CheckPlanDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow-x: hidden; overflow-y: auto; width: 102%; height: 400px;">
        <table class="table">
            <tr>
                <td>
                    <b>计划类型：</b><asp:Literal ID="litype" runat="server"></asp:Literal>
                </td>
                <td>
                    <b>开始时间：</b><asp:Literal ID="liStarttime" runat="server"></asp:Literal>
                </td>
                <td>
                    <b>结束时间：</b><asp:Literal ID="liStoptime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>员工姓名：</b><asp:Literal ID="lirealname" runat="server"></asp:Literal>
                </td>
                <td>
                    <b>预期数量：</b><asp:Literal ID="litotal" runat="server"></asp:Literal>
                </td>
                <td>
                    <b>完成数量：</b><asp:Literal ID="licurr" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="font-weight: bold; color: black; text-align: center; height: 28px;
                    font-size: 14px; padding-left: 10px;">
                    <asp:Literal ID="lititle" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="licontent" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <asp:GridView ID="Gv_duty" runat="server" CssClass="table tableview" AutoGenerateColumns="False"
            PageSize="200" ShowHeader="false" DataKeyNames="id">
            <Columns>
                <asp:TemplateField HeaderText="任务名称">
                    <ItemTemplate>
                        <div title='描述：<%# Eval("Content")%>&#13预计用时：<%# Eval("Etime")%>'>
                            <img src="/images/64.bmp" alt="任务" /><%# Eval("Title")%></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="申请时间">
                    <ItemTemplate>
                        <%#  Convert.ToDateTime(Eval("Statetime")).ToString("yyyy-MM-dd HH:mm")%></ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table class="table"> <tr>
                <td width="50">
                  &nbsp;
                </td>
                <td>
                  &nbsp;
                </td>
                <td width="70">
                    <asp:Button ID="Button1" runat="server" Text="审核通过" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td width="50">
                    <b>原因：</b>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Rows="2" Width="360"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="未通过" OnClick="Button2_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
