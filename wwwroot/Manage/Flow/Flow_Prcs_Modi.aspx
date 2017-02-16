<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_Prcs_Modi.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_Modi" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
         table.table input[type='text'],select {border:solid 1px #777;}
         table.table td{ padding-left:10px;}
         input.SmallButtonA {
                background: url("/Manage/images/btn_a.png") no-repeat scroll 0 0 transparent;
                border: 0 none;
                color: #36434E;
                cursor: pointer;
                height: 21px;
                width: 50px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="3" Param1="{Q:FlowId}" Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="table">
            <tr>
                <th style="width: 100px">
                    借点序号：</th>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtNodeId" runat="server" Width="100" MaxLength="2"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <th>
                    借点类型：</th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlNodeType" runat="server">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <th>
                    借点名称：</th>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtNodeName" runat="server" Width="312px" MaxLength="50"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <asp:UpdatePanel ID="UpatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <th>
                            下一步借点：</th>
                        <td align="left" class="style2" valign="middle" width="250px">
                            <asp:ListBox ID="ListBox1" runat="server" Height="200" Width="100%" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td style=" text-align:center;" valign="middle" width="30">
                            <asp:Button ID="btnAddList" runat="server" CssClass="SmallButtonA" Text=">>" OnClick="btnAddList_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnRemoveList" runat="server"  CssClass="SmallButtonA" Text="<<" OnClick="btnRemoveList_Click" />
                        </td>
                        <td align="left" class="style2" valign="middle" width="250px">
                            <asp:ListBox ID="ListBox2" runat="server" Height="200" Width="100%" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td>&nbsp;</td>                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            <tr style="height:200px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <th>
                            不通过返回到：</th>
                        <td align="left" class="style2" valign="middle" width="250px">
                            <asp:ListBox ID="ListBox3" runat="server" Height="200" Width="100%" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td style=" text-align:center;" valign="middle" width="30">
                            <asp:Button ID="Button1" runat="server" CssClass="SmallButtonA" Text=">>" OnClick="btnAddList2_Click" />
                            <br />
                            <br />
                            <asp:Button ID="Button2" runat="server" CssClass="SmallButtonA" Text="<<" OnClick="btnRemoveList2_Click" />
                        </td>
                        <td align="left" class="style2" valign="middle" width="250px">
                            <asp:ListBox ID="ListBox4" runat="server" Height="200" Width="100%" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td>&nbsp;</td>                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            <tr>
                <td colspan="4">
                    <div style="text-align: center; width: 98%;">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text=" 提交 "
                            OnClick="btnSubmit_Click" />
                        &nbsp;&nbsp;&nbsp;<input type="reset" class="button" value=" 重 置 " /></div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
