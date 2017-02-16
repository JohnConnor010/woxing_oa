<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_CheckTask.aspx.cs"
    Inherits="wwwroot.Manage.Plan.Plan_CheckTask" %>

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
    <div style="overflow-x:hidden;overflow-y:auto; width:102%; height: 400px;">
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
            <td colspan="3">
                <b>计划标题：</b><asp:Literal ID="lititle" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <b>描述：</b><asp:Literal ID="licontent" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <table class="table3">
        <tr>
            <td colspan="2" style="background-color: #83b1ca; height: 28px; text-align: center;
                font-weight: bold;">
                任务列表
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Gv_duty" runat="server" CssClass="table tableview" AutoGenerateColumns="False"
                    PageSize="200" ShowHeader="false" DataKeyNames="id">
                    <Columns>
                    <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <input id="Checkbox1" name="Checkbox1" type="checkbox" value='<%# Eval("id")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="任务名称">
                            <ItemTemplate>
                                <div title='描述：<%# Eval("Content")%>&#13预计用时：<%# Eval("Etime")%>'>
                                    <%# Eval("Title")%></div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="申请时间">
                            <ItemTemplate>
                                <%#  Convert.ToDateTime(Eval("Statetime")).ToString("yyyy-MM-dd HH:mm")%></ItemTemplate>
                            <ItemStyle Width="100" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="50">
                <b>评语：</b>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Rows="2" Width="360"></asp:TextBox>
                <asp:Button
                    ID="Button1" runat="server" Text="选中通过" onclick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="关闭" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
