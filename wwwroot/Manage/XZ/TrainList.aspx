<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="TrainList.aspx.cs" Inherits="wwwroot.Manage.XZ.TrainList" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server"> 考核培训管理 >> 信息列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="xz_train" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound" onrowdatabound="GridView1_RowDataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="发布人">
                <ItemTemplate><%# Eval("RealName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类型">
                <ItemTemplate><%# Eval("TypeName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href='AddTrain.aspx?TrainID=<%# Eval("id") %>'><%# Eval("Title") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="参与人员" DataField="ID" ItemStyle-Width="300" />
             <%--<asp:TemplateField HeaderText="参与人员">
                <ItemTemplate>
                    <%#Eval("UsersName")%>
                </ItemTemplate>
                <ItemStyle Width="300" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="地点">
                <ItemTemplate><%# Eval("Addr")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="开始时间">
                <ItemTemplate><%#  Convert.ToDateTime(Eval("Runtime")).ToString("yyyy-MM-dd")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发布时间">
                <ItemTemplate><%# Convert.ToDateTime(Eval("Addtime")).ToString("yyyy-MM-dd HH:mm:ss")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作">
                <ItemTemplate>
                    <asp:HyperLink
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑"
                        NavigateUrl='<% #String.Format("AddTrain.aspx?TrainID={0}",Eval("ID")) %>'></asp:HyperLink>         
                        <asp:Literal ID="Literal1" runat="server" Visible='<% #this.Master.A_Edit %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton1" runat="server" Visible='<% #this.Master.A_Edit %>' CausesValidation="False"
                        OnClick="Sendmes" CommandName='<% #Eval("id") %>'
                        Text="向参与人员发送消息" />           
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton3" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False"
                        OnClick="Del" OnClientClick="return confirm('是否真的要删除这条信息？');" CommandName='<% #Eval("id") %>'
                        Text="删除" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
