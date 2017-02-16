<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Run_NewForm.aspx.cs" Inherits="wwwroot.Manage.Work.Run_NewForm" ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#clear').click(function () {
                $('#txtSerialNumber').val("");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    新建工作测试 >> 新建工作
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="run" CurIndex="1" Param1="" />
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
                    <asp:TextBox ID="txtSerialNumber" runat="server" Width="200px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp; <a href="javascript:void(0)" id="clear">清空</a><br />
                    <asp:Button ID="btnSubmit" runat="server" Text="创建并办理" CssClass="button" 
                        onclick="btnSubmit_Click" /></td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    * 说明文档：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td>
                    <span class="note" style="display: none;">流程表单的说明文档</span>
                        <asp:TextBox ID="txtDescription" BorderStyle="Dashed" BorderWidth="1" ReadOnly="true" runat="server" Width="504" Height="120"></asp:TextBox>
                </td>
            </tr>
            <tr class="">
                <th style="width: 140px; font-weight: bold;">
                    &nbsp;流程说明步骤列表：<a class="help" href="#">[?]</a>
                </th>
                <td style="overflow-y: auto; text-align:left">
                    <span class="note" style="display: none;">流程步骤</span>
                    <table class="table" style="width: 60%; margin-left:0px">
                        <thead>
                            <tr class="">
                                <td width="50" align="center">
                                    步骤序号
                                </td>
                                <td width="120" align="center">
                                    名称
                                </td>
                                <td width="275" align="center">
                                    流程可选方向
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
                                    <%#Eval("NextNode") %>
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
                    <input type="button" class="button" id="save_bt" onclick="PopupIFrame('/App_Ctrl/FlowGraphic.aspx?FlowID=<%=rFlowId %>','流程设计图',null,null,700,400)"
                        value="查看流程设计图" name="save_bt" style="width: 120px" />&nbsp;&nbsp;
                    <input type="button" value="查看表单模版" class="button" onclick="PopupIFrame(&#39;/Manage/Flow/Form_Preview.aspx?Id=<%=FormId %>&#39;,&#39;预览表单&#39;,&#39;&#39;,&#39;&#39;,650,700)" style="width: 120px" /></td>
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
