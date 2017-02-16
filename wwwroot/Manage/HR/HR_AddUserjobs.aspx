<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="HR_AddUserjobs.aspx.cs" Inherits="wwwroot.Manage.HR.HR_AddUserjobs" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >> 添加职务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="6" Param1="{Q:UserId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">  
    <b style="padding-left:20px;">
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></b>
     <div style="clear:both"></div>
         <table class="table3" style="text-align: center; line-height: 200%;">
                            <tr>
                                <td width="100">
                                    部门
                                </td>
                                <td align="left">
                                  &nbsp;<asp:DropDownList 
                                        ID="ddlDepartment" runat="server" Style="width: 250px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    职位
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ui_jobname" runat="server" Style="width: 250px">
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                            </tr>

                            <tr>
                                <td>
                                     &nbsp;
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button1" runat="server" CssClass="button" Text=" 提 交 " OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>

    </div>
</asp:Content>

