<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Duty_List.aspx.cs" Inherits="wwwroot.Manage.Sys.Func_List" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 基础设置 >> 职务管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="duty" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelDefault">
        <div style="width: 98%; ">
            <span style="float: left; font-style:italic;padding-left:30px; font-size:+1;">
                <asp:Literal ID="liCompanyName" runat="server">我行技术有限公司</asp:Literal></span> 
                <span style="float: right;">
                    <a href="Duty_Manage.aspx" style="font-weight: bolder; color: #336;">添加职务</a></span>
        </div>
        <div style="clear:both;"/>
        <asp:GridView ID="Gv_duty" runat="server" CssClass="table tableview" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="20" OnPageIndexChanging="Gv_List_PageIndexChanging"
            OnRowCommand="Gv_List_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <img alt="" src="/images/duty.gif" style="width:15px;height:15px;" />
                    </ItemTemplate>
                    <ItemStyle Width="16px" HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="编号" DataField="NO" ItemStyle-Width="120" />
                <asp:BoundField HeaderText="职务名称" DataField="Name" ItemStyle-Width="120" />
                <asp:BoundField HeaderText="职务分类" DataField="DutyCatagoryName" ItemStyle-Width="120" />
                <asp:TemplateField HeaderText="默认级别" SortExpression="GradeID">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0}({1})",Eval("GradeID","{0} 级"),Eval("GradeName")) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="120px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理">
                    <ItemTemplate>
                        <asp:LinkButton Visible='<% #this.Master.A_Edit && Convert.ToInt32(Eval("ID"))!=0 %>' ID="LinkButton3" CommandArgument='<%# Eval("ID") %>'
                            CommandName="ledit" runat="server">修改职务</asp:LinkButton><asp:LinkButton Visible='<% #this.Master.A_Edit %>'
                                ID="LinkButton4" CommandArgument='<%# Eval("ID") %>' CommandName="dutyPriv" runat="server">职务权限</asp:LinkButton><asp:LinkButton
                                    Visible='<% #this.Master.A_Edit %>' ID="LinkButton1" CommandArgument='<%# Eval("ID") %>'
                                    CommandName="dutyMenu" runat="server">职务菜单</asp:LinkButton><asp:LinkButton Visible='<% #this.Master.A_Edit %>'
                                        ID="LinkButton5" CommandArgument='<%# Eval("ID") %>' CommandName="buildMenu"
                                        runat="server">生成菜单</asp:LinkButton><asp:LinkButton Visible='<% #this.Master.A_Del && Convert.ToInt32(Eval("ID"))!=0 %>'
                                            ID="LinkButton2" CommandArgument='<%# Eval("ID") %>' CommandName="del" OnClientClick="return window.confirm('你确定要删除这一项吗？');"
                                            runat="server">删除</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="500px" CssClass="manage" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
    </div>
</asp:Content>
