<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="User_UserList.aspx.cs" Inherits="wwwroot.Manage.Sys.User_UserList1" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="../css/AspnetPager.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 用户管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="account" CurIndex="1"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   <div style="padding-left: 20px; padding-right: 20px; color: #444">
        <div style="text-align: left; float: left; width: 500px;">
            请输入员工姓名进行查询：<asp:TextBox ID="tbKeyWords" runat="server" Width="100" BorderStyle="Solid"
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
                    用户名
                </td>
                <td>
                    员工姓名
                </td>
                <td>
                    所在部门
                </td>
                <td>
                    所属职务
                </td>
                <td>
                    所属级别
                </td>
                <td>
                    创建时间
                </td>
                <td>
                    最后一次登录
                </td>
                <td>
                    最后一次修改密码
                </td>
                <td>
                    账户状态
                </td>
                <td>
                    操作
                </td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'>
                <ItemTemplate>
                    <tr class="">
                        <td style="padding-left: 15px;">
                        <img alt="" src="/images/user.gif" style="width:15px;height:15px;" />
                            <strong>
                                <%#Eval("UserName") %></strong>
                        </td>
                        <td>
                            <%#Eval("RealName") %>
                        </td>
                        <td>
                            <%#Eval("DepartmentName") %>
                        </td>
                        <td>
                            <%#Eval("DutyName") %>
                        </td>
                        <td>
                            <%#Eval("Grade","{0} 级") %>
                        </td>
                        <td>
                            <%#Eval("CreateDate","{0:yyyy-MM-dd HH:mm}") %>
                        </td>
                        <td>
                            <%#Eval("LastLoginDate", "{0:yyyy-MM-dd HH:mm}")%>
                        </td>
                        <td>
                            <%#Eval("LastPasswordChangedDate", "{0:yyyy-MM-dd HH:mm}")%>
                        </td>
                        <td>
                            <%#Convert.ToBoolean(Eval("IsLockedOut"))?"<span style='color:red;'>锁定</span>":"<span style='color:green;'>正常</span>"%>
                        </td>
                        <td class="manage">
                            <asp:HyperLink runat="server" Text="账户状态" NavigateUrl='<% #Eval("UserID","/Manage/Sys/User_AccountState.aspx?UserID={0}&CompanyID="+Request["CompanyID"]) %>'></asp:HyperLink>
                            <asp:HyperLink runat="server" Text="重置密码" NavigateUrl='<% #Eval("UserID","/Manage/Sys/User_ResetPwd.aspx?UserID={0}&CompanyID="+Request["CompanyID"]) %>'></asp:HyperLink>
                            <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('确定要删除此用户吗？')" OnCommand="lnkDelete_Command" CommandArgument='<%#Eval("UserName") %>'>删除用户</asp:LinkButton>
                        </td>
                    </tr>
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

