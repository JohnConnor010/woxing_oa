<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="wwwroot.Manage.DownLoad.Index" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    表格工具下载 >> 下载专区
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="DownLoad" CurIndex="1"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 500px;">
            请输入关键字：<asp:TextBox ID="tbKeyWords" runat="server" Width="100" BorderStyle="Solid"
                BorderWidth="1"></asp:TextBox>&nbsp;<asp:LinkButton ID="LinkButton1" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="Query" Text="GO" />&nbsp;|&nbsp;<asp:LinkButton ID="LinkButton2" Font-Bold="true" ForeColor="#234323"
                    runat="server" OnClick="QueryAll" Text="ALL" />
        </div>
        <div style="text-align: right; float: right; width: 350px;">
        </div>
       <div style="clear: both;">
       </div>
   </div>
    <table class="table">
        <thead>
            <tr class="">
                <td style="padding-left: 15px;">
                    文件名称
                </td>
                <td>
                    点击次数
                </td>
                <td>
                    上传时间
                </td>
                <td>
                    上传人
                </td>
                <td>
                    下载
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
                <ItemTemplate>
                    <tr class="">
                        <td style="padding-left: 15px;">                       
                            <strong>
                             <asp:HyperLink ID="HyperLink4" runat="server"  Text='<%#Eval("Name") %>' NavigateUrl='<% #Eval("ID","/Manage/DownLoad/DownLoad.aspx?AnnexID={0}&down=1") %>'></asp:HyperLink>
                                </strong>
                        </td>
                        <td>
                            <%#Eval("Count") %>
                        </td>
                        <td>
                            <%#Eval("AddTime") %>
                        </td>
                        <td>
                            <%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>
                        </td>
                        <td class="manage">
                             <asp:HyperLink ID="HyperLink3" runat="server"  Text="下载" NavigateUrl='<% #Eval("ID","/Manage/DownLoad/DownLoad.aspx?AnnexID={0}&down=1") %>'></asp:HyperLink>
                       <asp:HyperLink ID="HyperLink1" runat="server" Text="编辑" Visible='<%#Eval("UserID").ToString()==WX.Main.CurUser.UserID %>' NavigateUrl='<% #Eval("ID","/Manage/DownLoad/UpLoad.aspx?AnnexID={0}") %>'></asp:HyperLink>
                            
                            <asp:HyperLink ID="HyperLink2" runat="server" Visible='<%#Eval("UserID").ToString()==WX.Main.CurUser.UserID %>' Text="删除" NavigateUrl='<% #Eval("ID","/Manage/DownLoad/UpLoad.aspx?AnnexID={0}&del=1") %>'></asp:HyperLink>
                          
                        </td>
                    </tr>
                    <tr><td colspan="5"> <b>描述:</b><%#Eval("Demo")%></td></tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <div style="text-align: center; width: 98%;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
            CssClass="Digg">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>

