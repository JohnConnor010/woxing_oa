<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="DepartmentWeekPlan.aspx.cs" Inherits="wwwroot.Manage.MyManage.DepartmentWeekPlan" %>

<%@ Register Src="~/Manage/include/MenuBar.ascx" TagPrefix="uc1" TagName="MenuBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link rel="Stylesheet" href="../Style/planstyle.css" type="text/css" rev="stylesheet" media="all" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar runat="server" ID="MenuBar" Key="MyManage" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <div style="text-align: right; padding-right: 100px; font-weight: bold;">【<a href="DepartmentWeekPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&WeekIndex=<%=Convert.ToInt32(Request.QueryString["WeekIndex"]) - 1 %>">上一周</a>】【<a href="DepartmentWeekPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&WeekIndex=<%= currentWeekIndex %>">本周</a>】
            <%
                if (Convert.ToInt32(Request.QueryString["WeekIndex"]) == 52)
                {
                    %>
            【下一周】
            <%
                }
                else
                {
                    %>
            【<a href="DepartmentWeekPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&WeekIndex=<%=Convert.ToInt32(Request.QueryString["WeekIndex"]) + 1 %>">下一周</a>】
                <%
                } %>
            

        </div>
        <div class="myplan1">
            <div class="plan_top">
                <table>
                    <tr>
                        <td style="padding-left: 10px;">
                            <img id="Image1" src="/images/rtype1.png" title="个人">&nbsp;<img id="Image2" src="/images/type2.png" title="周计划">
                        </td>
                        <td>
                            <h3>
                                <asp:Literal ID="ltlDepartmentName" runat="server"></asp:Literal>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Literal ID="ltlDateTime" runat="server"></asp:Literal>
                            </h3>
                            <ul>
                                <input type="hidden" name="ctl00$ContentPlaceHolder$hiplanid" id="hiplanid" />
                            </ul>
                            <h5>周计划</h5>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="plan_bodyl">
                <asp:TextBox ID="txtTitle" runat="server" ReadOnly="true" CssClass="input_1_1"></asp:TextBox>
                <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" CssClass="input_2"></asp:TextBox>
                <asp:TextBox ID="txtCurrent" runat="server" ReadOnly="true" CssClass="input_3"></asp:TextBox>
                <div class="clear">
                </div>
                <textarea name="txtContent" rows="2" cols="20" readonly="readonly" id="txtContent" class="textarea1" runat="server"></textarea>
                <div class="clear">
                </div>

            </div>
            <div class="plan_bodyc">
                <textarea name="txtSummary" rows="2" cols="20" id="txtSummary" class="textarea2" runat="server"></textarea>
                <table>
                    <tr>
                        <td width="260">
                            <div class="tip2">
                            </div>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="plan_bodyr">
                <div class="comment">
                    <ul>
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <li><a title="<%#Eval("Content1") %>"><%#Eval("RealName") %>：<%#Eval("Content") %>&nbsp;</a><img src="/images/Appraise<%#Eval("Appraise") %>.jpg"><span><%#Eval("AddTime") %></span></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <hr />
        <!------------------------员工周计划--------------------------->
        <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
            <ItemTemplate>
                <div class="myplan1">
                    <div class="plan_top">
                        <table>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <img id="Image1" src="/images/rtype1.png" title="个人">&nbsp;<img id="Image2" src="/images/type2.png" title="周计划"></td>
                                <td>
                                    <h3><%#Eval("RealName") %>&nbsp;                                        
                                    <%#Eval("WeekFlag") %>
                                    </h3>
                                    <ul>
                                        
                                    </ul>
                                    <h5>周计划</h5>
                                </td>
                            </tr>
                        </table>

                    </div>
                    <div class="plan_bodyl">
                        <input name="TextBox1" type="text" maxlength="28" id="TextBox1" class="input_1" value="<%#Eval("Title") %>" />
                        <input name="TextBox2" type="text" maxlength="3" id="TextBox2" class="input_2" value="<%#Eval("total") %>" />
                        <input name="TextBox3" type="text" maxlength="3" id="TextBox3" class="input_3" value="<%#Eval("current") %>" />
                        <div class="clear">
                        </div>
                        <textarea name="TextBox4" rows="2" cols="20" id="TextBox4" class="textarea1"><%#Eval("content") %></textarea>
                        <div class="clear">
                        </div>

                    </div>
                    <div class="plan_bodyc">
                        <textarea name="TextBox5" rows="2" cols="20" id="TextBox5" class="textarea2"><%#Eval("summary") %></textarea>
                    </div>
                    <div class="plan_bodyr">
                        <div class="comment">
                            <ul>
                                <asp:Repeater ID="Repeater3" runat="server">
                                    <ItemTemplate>
                                        <li><a title="<%#Eval("Content1") %>"><%#Eval("RealName") %>：<%#Eval("Content") %>&nbsp;</a><img src="/images/Appraise<%#Eval("Appraise") %>.jpg"><span><%#Eval("AddTime") %></span></li>
                                    </ItemTemplate>
                                </asp:Repeater>                                
                            </ul>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>        
    </div>
</asp:Content>
