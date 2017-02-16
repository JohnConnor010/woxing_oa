<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="DepartmentDayPlan.aspx.cs" Inherits="wwwroot.Manage.MyManage.DepartmentDayPlan" ClientIDMode="Static" %>
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
    <div style=" text-align:right; padding-right:100px; font-weight:bold;">【<a href="DepartmentDayPlan.aspx?DepartmentID=<%=Request["DepartmentID"] %>&Starttime=<%=Convert.ToDateTime(Request.QueryString["Starttime"]).AddDays(-1).ToString("yyyy-MM-dd") %>">昨天</a>】【<a href="DepartmentDayPlan.aspx?DepartmentID=<%=Request["DepartmentID"] %>&Starttime=<%=DateTime.Now.ToString("yyyy-MM-dd") %>">今天</a>】【<a href="DepartmentDayPlan.aspx?DepartmentID=<%=Request["DepartmentID"] %>&Starttime=<%=Convert.ToDateTime(Request.QueryString["Starttime"]).AddDays(1).ToString("yyyy-MM-dd") %>">明天</a>】</div>
    <div class="myplan1">
        <div class="plan_top">
            <table>
                <tr>
                    <td style="padding-left: 10px;">
                        <%=image1Url %>&nbsp;<%=image2Url %>
                    </td>
                    <td>
                        <h3>
                            <asp:Literal ID="ltlDepartmentName" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;<asp:Literal
                                ID="liDatetime" runat="server"></asp:Literal></h3>
                        <ul>
                            <asp:HiddenField ID="hiplanid" runat="server" />
                        </ul>
                        <h5>日计划</h5>
                    </td>
                </tr>
            </table>

        </div>
        <div class="plan_bodyl">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input_1_1" MaxLength="28" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="txtTotal" runat="server" CssClass="input_2" MaxLength="3" ReadOnly="true"></asp:TextBox>
            <asp:TextBox ID="txtCurrent" runat="server" CssClass="input_3" MaxLength="3" ReadOnly="true"></asp:TextBox>
            <div class="clear">
            </div>
            <asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" CssClass="textarea1" ReadOnly="true"></asp:TextBox>
            <div class="clear">
            </div>
            
        </div>
        <div class="plan_bodyc">
            <asp:TextBox ID="txtSummary" TextMode="MultiLine" runat="server" CssClass="textarea2"></asp:TextBox>
            <table>
                <tr>
                    <td width="260">
                        <div class="tip2">
                        </div>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="plan_bodyr">
            <div class="comment">
                <asp:Literal ID="ltlComment" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <hr />
    <!------------------------部门日计划--------------------------->
    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
        <ItemTemplate>
            <div class="myplan1">
                <div class="plan_top">
                    <table>
                        <tr>
                            <td style="padding-left: 10px;">
                                <%=image1Url %>&nbsp;<%=image2Url %></td>
                            <td>
                                <h3>
                                    <%#Eval("RealName") %>&nbsp;&nbsp;&nbsp;
                                    <%=String.Format("{0:yyyy年MM月dd日 dddd}", Convert.ToDateTime(Request.QueryString["Starttime"])) %>
                                </h3>
                                <ul>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </ul>
                                <h5>
                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>计划</h5>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="plan_bodyl">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="input_1" MaxLength="28" Text='<%#Eval("Title") %>'>"></asp:TextBox>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="input_2" MaxLength="3" Text='<%#Eval("Total") %>'></asp:TextBox>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="input_3" MaxLength="3" Text='<%#Eval("Current") %>'></asp:TextBox>
                    <div class="clear">
                    </div>
                    <asp:TextBox ID="TextBox4" TextMode="MultiLine" runat="server" CssClass="textarea1" Text='<%#Eval("Content") %>'></asp:TextBox>
                    <div class="clear">
                    </div>

                </div>
                <div class="plan_bodyc">
                    <asp:TextBox ID="TextBox5" TextMode="MultiLine" runat="server" CssClass="textarea2" Text='<%#Eval("Summary") %>'></asp:TextBox>

                </div>
                <div class="plan_bodyr">
                    <div class="comment">
                        <ul>
                            <asp:Repeater ID="CommentRepeater" runat="server">
                                <ItemTemplate>
                                    <li><a title="太好了"><%#Eval("RealName") %>：<%#Eval("Content") %>&nbsp;</a><img src="/images/Appraise<%#Eval("Appraise") %>.jpg"><span><%#String.Format("{0:d}",Eval("AddTime")) %></span></li>
                                </ItemTemplate>
                            </asp:Repeater>                            
                        </ul>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
