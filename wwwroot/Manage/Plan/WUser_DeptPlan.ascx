<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUser_DeptPlan.ascx.cs" Inherits="wwwroot.Manage.Plan.WUser_DeptPlan" %>
<asp:DataList ID="DataList1" runat="server" RepeatColumns="2" CssClass="table3">
        <ItemTemplate>
            <table style="line-height: 180%; width: 100%;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="BACKGROUND-COLOR: #D8DCF1;">
                    <div id="Div1" style="float: left; text-align:center; width: 80%;">
                    <%#Eval("RealName")%></div>
                <div style="float: left;">
                    <%#((Eval("RangeType").ToString() == "2" && Eval("newrtype").ToString() == "2") || (Eval("RangeType").ToString() == "3" && Eval("newrtype").ToString() == "3") ? (Eval("PlanState").ToString() == "0" ? "<a href='?type=" + Eval("Type") + "&PlanId=" + Eval("id") + "'>提交审核</a>&nbsp;&nbsp;" : "") + (Eval("PlanState").ToString() == "1" || Eval("PlanState").ToString() == "2" ? "" : "<a href='Plan_EditPlan.aspx?PlanId=" + Eval("id") + "'>编辑</a>") : "")%></div>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <iframe src='Plan_PlanDetail.aspx?UserID=<%#Eval("UserID") %>&starttime=<%#Eval("Stime") %>&type=<%#Eval("Type") %>&rtype=<%#Eval("rtype") %><%#Eval("UserID").ToString()==WX.Main.CurUser.UserID?"&estate=1":"" %>' onload="Javascript:SetWinHeight(this,'0')"
                            id="iframe1" width="98%" frameborder="no" border="0" marginwidth="0" marginheight="0"
                            scrolling="no" allowtransparency="yes"></iframe>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
        <ItemStyle VerticalAlign="Top" Width="50%" />
    </asp:DataList>