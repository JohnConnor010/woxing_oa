<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage3.Master" AutoEventWireup="true" CodeBehind="messagelist.aspx.cs" Inherits="wwwroot.Manage.messagelist" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage3.Master" %>
<%@ Register src="/Manage/include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">
        function checkHasSel() {
            var selCount = 0;
            $(".checkdelete").each(function () {
                if ($(this).attr("checked") == true) selCount++;
            });
            if (selCount == 0) {
                alert("你没有选择记录");
                return false;
            }
            else {
                if (confirm("你选择了" + (selCount) + "项，是否要继续进行操作，此操作不可恢复！")) {
                    return true;
                }
                else {
                    return false; 
                }
            }
        }
    </script>
    <link href="../Manage/css/AspnetPager.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的消息
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="myMsg" CurIndex="1" />
</asp:Content>
<asp:content id="Content4" contentplaceholderid="ContentPlaceHolder" runat="server">
    <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 500px;">
            请输入关键字进行查询：<asp:TextBox ID="tbKeyWords" runat="server" Width="300" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;<asp:LinkButton ID="LinkButton1" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="Query" Text="GO" />&nbsp;&nbsp;<asp:LinkButton ID="LinkButton3" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="QueryAll" Text="ALL" />
        </div>
        <div style="text-align: right; float: right; width: 200px;">
            <asp:LinkButton runat="server" ID="LinkButton4" OnClick="ReadSel" Font-Bold="true" ForeColor="#234323"
                OnClientClick="return checkHasSel()" Text="设为已读" />
            <asp:LinkButton runat="server" ID="lbDelSel" OnClick="DelSel" Font-Bold="true" ForeColor="#234323"
                OnClientClick="return checkHasSel()" Text="删除" /></div>
        <div style="clear: both;">
        </div>
    </div>
    <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField ItemStyle-Width="20">
                <HeaderTemplate>
                    <input class="checkall" type="checkbox" onclick='$(".checkdelete").attr("checked", $("input[class=checkall]").attr("checked"));' />
                </HeaderTemplate>
                <ItemTemplate>
                    <input name="checksel" type="checkbox" class="checkdelete" id="checksel" value='<%#Eval("id") %>' />
                </ItemTemplate>
                <ItemStyle Width="20px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发件人">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Role").ToString()=="0"?"系统":gRealName(Eval("FromUserId")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="主题">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<% #String.Format("showmessage.aspx?id={0}",Eval("ID")) %>'><asp:Image runat="server" ImageUrl='<% #Convert.ToInt32(Eval("State"))==0?"/img/mail_noread.gif":"/img/mail_isread.gif" %>' /></asp:HyperLink>
                    <asp:HyperLink runat="server" NavigateUrl='<% #String.Format("showmessage.aspx?id={0}",Eval("ID")) %>'  Text='<% #Eval("Title") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" ToolTip='<% #Eval("SendTime") %>' Text='<%# getEslapseStr(Eval("SendTime")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="130px" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="操作" ItemStyle-Width="120">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="查看" NavigateUrl='<% #String.Format("showmessage.aspx?id={0}",Eval("ID")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal2" runat="server" Visible='<% #(this.Master.A_Del&&Eval("Role").ToString()!="0")?true:false %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton ID="LinkButton2" runat="server" Visible='<% # (this.Master.A_Del&&Eval("Role").ToString()!="0")?true:false %>' CausesValidation="False" OnClick="Del"
                        OnClientClick="return confirm('是否真的要删除这条记录？');" CommandName='<% #Eval("ID") %>'
                        Text="删除" />
                </ItemTemplate>
                <ItemStyle Width="120px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:content>
