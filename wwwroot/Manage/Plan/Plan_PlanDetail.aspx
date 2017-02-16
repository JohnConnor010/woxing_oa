<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_PlanDetail.aspx.cs"
    Inherits="wwwroot.Manage.Plan.Plan_PlanDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/InterFace.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <style type="text/css">
       table.table3{width:100%;}
       div.progressBar{ padding-top:0px; margin-top:0px; line-height:100%;}
       div.progressBar span{ margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; font-style:italic; line-height:100%;color:#15428B;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="line-height: 180%; overflow-x: hidden; overflow-y: auto; width: 100%; max-height:400px; "
        id="div1" runat="server">
        <table class="table3" cellpadding="0" cellspacing="0">
            <tr>
                <td style="margin-bottom:0px;padding-top:0px; padding-right:0px;">
                    <div class="progressBar" style="height: 15px; font-size: 11px; font-style: italic;">
                        <div style="width: 240px; float: right; border-bottom: 0px solid #BDDBEF; border-left: 0px solid #BDDBEF; text-align:right; padding:1px 3px 1px 1px; margin-top:0px; margin-right:0px;">
                            <asp:Label ID="Label0" runat="server">创建中</asp:Label>
                            <asp:Label ID="Label1" runat="server">审核中</asp:Label>
                            <asp:Label ID="Label2" runat="server">执行中</asp:Label>
                            <asp:Label ID="Label3" runat="server">待总结</asp:Label>
                            <asp:Label ID="Label4" runat="server">待评价</asp:Label>
                            <asp:Label ID="Label5" runat="server">完成</asp:Label></div>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div style="font-weight: bold; color: black; text-align: center; height: 28px; font-size: 14px;
                        padding-left: 10px;">
                    <asp:HyperLink runat="server" ID="lititle"></asp:HyperLink>
                    </div>
                    <div style="text-align: center; font-size: 10px; color: #555;" id="divjd" runat="server">
                        <table class="table5">
                            <tr>
                                <td>
                                    <asp:Literal ID="lijindu" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <div style="width: 205px; text-align: left; height: 13px; background: url(/images/back.gif) no-repeat;">
                                        <img src="/images/jindu.gif" width='<%=imgwidth %>%' height="10" /></div>
                                </td>
                            </tr>
                        </table>
                    </div><asp:Literal ID="liPlanState" runat="server"></asp:Literal>
                     <div style=" padding:10px;"><asp:Literal ID="licontent" runat="server"></asp:Literal>
                     </div>
                </td>
            </tr>
            <tr>
            <td> <asp:Literal ID="Apptable" runat="server"></asp:Literal>
            </td></tr>
        </table>
        <asp:GridView ID="Gv_duty" runat="server" CssClass="table3 tableview" AutoGenerateColumns="False"
            PageSize="200" ShowHeader="false" OnRowDataBound="Gv_duty_RowDataBound" OnRowCommand="Gv_List_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="名称">
                    <ItemTemplate>
                        <div title='描述：<%# Eval("Content")%>&#13预计用时：<%# Eval("Etime")%>&#13评价：<%# Eval("Appraise")%>'>
                            <img src="/images/64.bmp" alt="任务" /><%# Eval("Title")%></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="状态" DataField="State" ItemStyle-Width="20" />
                <asp:TemplateField HeaderText="申请审核" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("ID") %>' Visible='<%# Eval("State").ToString()=="0" %>'
                            CommandName="editstate" runat="server">申请审核</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="60px" CssClass="manage" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    <table class="table3">
        <tr>
            <td>
                <asp:Literal ID="liSummary" runat="server"></asp:Literal>
                <asp:Literal ID="liAppraise" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
