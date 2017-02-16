<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_FillIn.aspx.cs" Inherits="wwwroot.Manage.Work.Work_FillIn" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server"> 新建工作测试 >> 新建工作
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server"><uc1:MenuBar ID="MenuBar1" runat="server" Key="run" CurIndex="1" Param1="{Q:Flow_ID}" Param2="{Q:RunID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tbody>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 收文/编号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <div><span id="tooltip" runat="server"></span></div>
                    <asp:TextBox ID="txtSerialNumber" runat="server" Width="400px" Enabled="false"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;<br />
                   </td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 说明文档：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">流程表单的说明文档</span>
                    <asp:Label ID="labDescription" ForeColor="Red" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr><td colspan="2"><br />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <br />
            </td></tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    &nbsp;流程说明步骤列表：<a class="help" href="#">[?]</a>
                </th>
                <td style="overflow-y: auto; text-align:left">
                    <span class="note" style="display: none;">流程步骤</span>
                    <table class="table" style="width: 80%; margin-left:0px">
                        <thead>
                            <tr class="">
                                <td width="80" align="center">
                                    步骤序号
                                </td>
                                <td width="120" align="center">
                                    名称
                                </td>
                                <td align="center">
                                    会签意见
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="ProcessRepeater" runat="server">
                            <ItemTemplate>
                            <tr class="">
                                <td>
                                    <span style="margin-left: 25px;"><%#Eval("StepNo") %></span>
                                </td>
                                <td>
                                    <span style="color: #666; font-weight: bold;"><%#Eval("Name") %></span>
                                </td>
                                <td>
                                    <%#Eval("username")%>
                                </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <th style="width: 140px; font-weight: bold;">
                    &nbsp;</th>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="申请办理" CssClass="button" 
                        onclick="btnSubmit_Click" /> &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <th style="width: 140px;">
                    &nbsp;
                </th>
                <td>
                    &nbsp;
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>