<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Duty_Func.aspx.cs" Inherits="wwwroot.Manage.Sys.Duty_Func" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 职务管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="duty-modi" CurIndex="3" Param1="{Q:companyID}" Param2="{Q:DutyId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table">
            <thead>
                <tr>
                    <td colspan="2">
                        <a href="#" title='查看所有帮助' class="helpall">你正在修改职务权限（<asp:Literal runat="server" ID="dutyId"></asp:Literal>-<asp:Literal runat="server" ID="dutyName"></asp:Literal>）&nbsp;[?]</a>
                        &nbsp;
                        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                            CssClass="button" OnClick="Button2_Click" />
                        &nbsp;&nbsp;<input type="reset" class="button" value='重置' />
                    </td>
                </tr>
            </thead>
            <tr>
                <th style="width: 135px; font-weight: bold;" valign="top">
                    * 职务权限&nbsp;<a href="#" class="help">[?]</a>
                </th>
                <td>
                    <span class="note">请选择该职务所具备的权限</span>
                    <div style="width: 500px;">
                        <asp:GridView ID="Gv_duty" runat="server" CssClass="table tableview" AllowPaging="True"
                            AutoGenerateColumns="False" PageSize="1000" Width="500" OnRowDataBound="Gv_duty_RowDataBound">
                            <Columns>
                                 <asp:TemplateField ShowHeader="False" HeaderText="功能名称">
                                    <ItemTemplate>
                                        <%# (Eval("node_level").ToString() == "1" ? "" : (Eval("node_level").ToString() == "2" ? " ├" : "│ ├ "))%><img
                                            src='/Manage/icon/<%# Convert.ToInt32(Eval("node_level"))==2?"globe.png":"ico_browsefolder.gif" %>' /><%# Eval("name") %>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="true"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="状态">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" Visible="false" runat="server" Text='<%# Eval("ID")+"|"+Eval("node_level")+"|"+Eval("State")+"|"+Eval("node_path")%>'></asp:Label>  
                                        <asp:DropDownList ID="ddlFlag" runat="server" Width="200px"></asp:DropDownList>                                      
                                        </ItemTemplate>
                                    <ItemStyle Width="200px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
