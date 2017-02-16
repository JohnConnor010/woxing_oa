<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage/Master/MasterPage2.Master" CodeBehind="PlanView.aspx.cs" Inherits="wwwroot.Manage.MyManage.PlanView" %>

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
    <table>
        <tr>
            <td align="center">

                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" NextPrevFormat="ShortMonth" Width="330px" OnSelectionChanged="Calendar1_SelectionChanged" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                    <DayStyle BackColor="#CCCCCC" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                    <OtherMonthDayStyle ForeColor="#999999" />
                    <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                    <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                    <TodayDayStyle BackColor="#999999" ForeColor="White" />
                </asp:Calendar>

            </td>
        </tr>
        <tr>
            <td>
                <div id="PanelManage">

                    <div class="myplan1">
                        <div class="plan_top">
                            <table>
                                <tr>
                                    <td style="padding-left: 10px;">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="/images/rtype3.png" ToolTip="个人" />&nbsp;
                            <asp:Image ID="Image2" runat="server" ImageUrl="/images/type1.png" ToolTip="月计划" />
                                    </td>
                                    <td>
                                        <h3>
                                            <asp:Literal ID="ltlPersonName" runat="server" Text="杨金勇"></asp:Literal>&nbsp;&nbsp;&nbsp;
                                <asp:Literal ID="ltlDateTime" runat="server" Text="2012-01-01"></asp:Literal>

                                        </h3>
                                        <ul>
                                        </ul>
                                        <h5>
                                            <asp:Literal ID="ltlPlanType" runat="server" Text="日计划"></asp:Literal>
                                        </h5>
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
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

