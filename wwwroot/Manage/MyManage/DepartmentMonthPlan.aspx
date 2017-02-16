<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="DepartmentMonthPlan.aspx.cs" Inherits="wwwroot.Manage.MyManage.DepartmentMonthPlan" %>

<%@ Register Src="~/Manage/include/MenuBar.ascx" TagPrefix="uc1" TagName="MenuBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link rel="Stylesheet" href="../Style/planstyle.css" type="text/css" rev="stylesheet"
        media="all" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar runat="server" ID="MenuBar" Key="MyManage" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <div style="text-align: right; padding-right: 100px; font-weight: bold;">【<a href="DepartmentMonthPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&Month=<%=Convert.ToInt32(Request.QueryString["Month"]) - 1 %>">上月</a>】【<a href="DepartmentMonthPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&Month=<%=DateTime.Now.Month %>">本月</a>】
            <%
                if (Convert.ToInt32(Request.QueryString["Month"]) == 12)
                {
                    %>
            【下月】
<%
                }
                else
                {
                    %>
            【<a href="DepartmentMonthPlan.aspx?DepartmentID=<%=Request.QueryString["DepartmentID"] %>&Month=<%=Convert.ToInt32(Request.QueryString["Month"]) + 1 %>">下月</a>】
            <%
                }
                 %>
            

        </div>
        <div class="myplan1">
            <div class="plan_top">
                <table>
                    <tr>
                        <td style="padding-left: 10px;">
                            <img id="Image1" src="/images/rtype3.png" title="个人">&nbsp;<img id="Image2" src="/images/type3.png" title="月计划">
                        </td>
                        <td>
                            <h3><asp:Literal ID="ltlDepartmentName" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;<asp:Literal ID="ltlMonthFlag" runat="server"></asp:Literal></h3>
                            <ul>
                                
                            </ul>
                            <h5>月计划</h5>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="plan_bodyl">
                <input name="txtTitle" type="text" maxlength="28" readonly="readonly" id="txtTitle" class="input_1_1" runat="server" />
                <input name="txtTotal" type="text" maxlength="3" readonly="readonly" id="txtTotal" class="input_2" runat="server" />
                <input name="txtCurrent" type="text" maxlength="3" readonly="readonly" id="txtCurrent" class="input_3" runat="server" />
                <div class="clear">
                </div>
                <textarea name="txtContent" rows="2" cols="20" readonly="readonly" id="txtContent" class="textarea1" runat="server">
</textarea>
                <div class="clear">
                </div>

            </div>
            <div class="plan_bodyc">
                <textarea name="txtSummary" rows="2" cols="20" id="txtSummary" class="textarea2" runat="server">
</textarea>
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
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <li><a title="<%#Eval("Content") %>"><%#Eval("RealName") %>：<%#Eval("Content1") %>&nbsp;</a><img src="/images/Appraise<%#Eval("Appraise") %>.jpg"><span><%#Eval("AddTime") %></span></li>
                            </ItemTemplate>
                        </asp:Repeater>                        
                    </ul>
                </div>
            </div>
        </div>

        <hr />
        <!------------------------个人月计划--------------------------->
        <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
            <ItemTemplate>
                <div class="myplan1">
                    <div class="plan_top">
                        <table>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <img id="Image1" src="/images/rtype3.png" title="个人">&nbsp;<img id="Image2" src="/images/type3.png" title="月计划"></td>
                                <td>
                                    <h3><%#Eval("RealName") %>&nbsp;&nbsp;&nbsp;
                                    <%#Eval("MonthFlag") %>
                                    </h3>
                                    <ul>
                                    </ul>
                                    <h5>月计划</h5>
                                </td>
                            </tr>
                        </table>

                    </div>
                    <div class="plan_bodyl">
                        <input name="TextBox1" type="text" maxlength="28" id="TextBox1" class="input_1" value="<%#Eval("title") %>" />
                        <input name="TextBox2" type="text" value="<%#Eval("total") %>" maxlength="3" id="TextBox2" class="input_2" />
                        <input name="TextBox3" type="text" value="<%#Eval("current") %>" maxlength="3" id="TextBox3" class="input_3" />
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
