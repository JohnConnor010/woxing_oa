<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan_TempEditPlan.aspx.cs"
    Inherits="wwwroot.Manage.Plan.Plan_TempEditPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="/manage/Style/planstyle.Css" rel="stylesheet" rev="stylesheet"
        media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="myplan1">
        <div class="plan_top">
            <table>
                <tr>
                    <td style="padding-left:10px;">
                        <asp:Image ID="Image1" runat="server" />&nbsp;<asp:Image ID="Image2" runat="server" />
                    </td>
                    <td>
                        <h3>
                            <asp:Literal ID="RealName" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;<asp:Literal
                                ID="liDatetime" runat="server"></asp:Literal></h3>
                                <ul>
            <asp:HiddenField ID="hiplanid" runat="server" />
            </ul>
            <h5>
                <asp:Literal ID="PlanType" runat="server"></asp:Literal>计划</h5>
                    </td>
                </tr>
            </table>
            
        </div>
    <div class="plan_bodyl">
        <asp:TextBox ID="txtTitle" runat="server" CssClass="input_1" MaxLength="28"></asp:TextBox>
        <asp:TextBox ID="txtTotal" runat="server" CssClass="input_2" MaxLength="3"></asp:TextBox>
        <asp:TextBox ID="txtCurrent" runat="server" CssClass="input_3" MaxLength="3"></asp:TextBox>
        <div class="clear">
        </div>
        <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" CssClass="textarea1"></asp:TextBox>
        <div class="clear">
        </div>
        <div class="tip">
            提醒：<asp:Literal ID="litip" runat="server"></asp:Literal></div>
    </div>
    <div class="plan_bodyc">
        <asp:TextBox ID="txtSummary" TextMode="MultiLine" runat="server" CssClass="textarea2"></asp:TextBox>
        <table>
            <tr>
                <td width="260">
                    <div class="tip2">
                        提醒：<asp:Literal ID="litip2" runat="server"></asp:Literal></div>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" CssClass="button1" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="plan_bodyr">
        <div id="appraisal" runat="server">
            <ul class="bodyr_ul">
                <li class="common">
                    <input type="radio" value="1" name="pingjia" checked="checked" /></li>
                <li class="good"><span>好评</span></li>
                <li class="common">
                    <input type="radio" value="2" name="pingjia" /></li>
                <li class="middle"><span>中评</span></li>
                <li class="common">
                    <input type="radio" value="3" name="pingjia" /></li>
                <li class="bad"><span>差评</span></li>
            </ul>
            <div class="rsubmit">
                <asp:TextBox ID="txtAppraiseContent" TextMode="MultiLine" runat="server" CssClass="textarea3"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" CssClass="button2" Text="提&nbsp;交" OnClick="Button2_Click" />
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="comment">
            <ul>
                <asp:Literal ID="Apptable" runat="server"></asp:Literal>
            </ul>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
