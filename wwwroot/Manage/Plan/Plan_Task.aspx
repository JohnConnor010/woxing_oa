<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_Task.aspx.cs" Inherits="wwwroot.Manage.Plan.Plan_Task" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    </title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"  media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="float:right; font-weight:bold;" id="div1" runat="server"><a href="javascript:PopupIFrame('Plan_EditTask.aspx?PlanId=<%=WX.Request.rPlanId %>','编辑任务','','',500,160)">添加任务</a></div>
    <asp:GridView ID="Gv_duty" runat="server" CssClass="table3 tableview"
            AutoGenerateColumns="False" PageSize="200"
            OnRowCommand="Gv_List_RowCommand" ShowHeader="false" 
        onrowdatabound="Gv_duty_RowDataBound">
            <Columns>
            <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                    <div title='描述：<%# Eval("Content")%>&#13预计用时：<%# Eval("Etime")%>&#13评价：<%# Eval("Appraise")%>'><%# Eval("Title")%></div>
                    </ItemTemplate>
                    </asp:TemplateField>
                <asp:BoundField HeaderText="状态" DataField="State" ItemStyle-Width="40" />
                <asp:TemplateField HeaderText="申请审核">
                    <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("State").ToString()=="0" %>' CommandName="editstate" runat="server">申请审核</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="60px" CssClass="manage" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理">
                    <ItemTemplate>
                    <a href="javascript:PopupIFrame('Plan_EditTask.aspx?PlanId=<%=WX.Request.rPlanId %>&TaskId=<%# Eval("ID") %>','编辑任务','','',500,160)" >修改</a>
                            <asp:LinkButton ID="LinkButton2" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="return window.confirm('你确定要删除这一项吗？');"
                                            runat="server">删除</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="80px" CssClass="manage" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView><br />
    </form>
</body>
</html>
