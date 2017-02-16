<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Dept_Companyslicense.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_Companyslicense" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 营业执照
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="comp" CurIndex="3" Param1="{Q:CompanyId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- 内容模块 -->
    <div style="width: 98%; margin-left:10px; text-align: left; font-style: italic;">
        <asp:Literal runat="server" ID="liTitle"></asp:Literal></div>
    <asp:DataList ID="DataList1" runat="server" Width="98%" OnItemCommand="DataList1_ItemCommand">
        <HeaderTemplate>
      
            <% if (Request["type"] == "7")
          { %>
            <table class="table" width="100%">
                <tr>
                    <td style="font-weight: bold;width: 20%;">
                        名称
                    </td>
                    <td style="font-weight: bold;width: 20%;">
                        证件号码
                    </td>
                    <td style="font-weight: bold; width: 10%;">
                        发证日期
                    </td>
                     <td style="font-weight: bold; width: 15%;">
                        原件保存部门
                    </td>                    
                    <td style="font-weight: bold; width: 10%;">
                        责任人
                    </td>
                    <td style="font-weight: bold; width: 10%;">
                        操作
                    </td>
                    <td style="width:15%;">
                         <b><a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%=Request["CompanyId"] %>&type=<%=Request["type"] %>">
                            添加</a></b>
                    </td>
                </tr>
            </table>
            <%}
          else if (Request["type"] == "1" || Request["type"] == "4")
          { %>
              <table class="table" width="100%">
                <tr>
                    <td style="font-weight: bold;width: 40%;">
                        名称
                    </td>
                     <td style="font-weight: bold; width: 15%;">
                        原件保存部门
                    </td>                    
                    <td style="font-weight: bold; width: 10%;">
                        责任人
                    </td>
                    <td style="font-weight: bold; width: 10%;">
                        <%=Request["type"] == "2" ? "添加" : "年检"%>日期
                    </td>
                    <td style="font-weight: bold; width: 10%;">
                        操作&nbsp;&nbsp;
                    </td>
                     <td style="font-weight: bold; width: 15%;">
                        <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%=Request["CompanyId"] %>&type=<%=Request["type"] %>">
                            添加</a>
                    </td>
                </tr>
            </table>
            <%}else { %>
             <table class="table" width="100%">
                <tr>
                    <td style="font-weight: bold;width: 35%;">
                        名称
                    </td>
                    <td style="font-weight: bold; width: 20%;">
                        原件保存部门
                    </td>                    
                    <td style="font-weight: bold; width: 20%;">
                        责任人
                    </td>                 
                    <td style="font-weight: bold; width: 10%;">
                        操作
                    </td>
                    <td style="font-weight: bold; width: 15%;">
                        <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%=Request["CompanyId"] %>&type=<%=Request["type"] %>">
                            添加</b>
                    </td>
                </tr>
            </table>
            <%} %>
        </HeaderTemplate>
        <ItemTemplate>
 
            <%  if (Request["type"] == "7")
   { %>
            <table class="table" width="100%" style=" line-height:180%;">
                <tr>
                    <td style="width:20%;">
                        <b><a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>">
                            <%# Eval("Title") %></a></b>&nbsp;
                    </td>
                    <td style="width:20%;">
                        <%# Eval("LNO") %>&nbsp;
                    </td>
                     <td style="width:10%;">
                        <%# ((DateTime)Eval("Addtime")).ToString("yyyy-MM-dd") %>&nbsp;
                    </td>
                     <td style="width: 15%;">
                         <%# Eval("DeptName") %>
                    </td>                    
                    <td style=" width: 10%;">
                         <%# Eval("RealName") %>
                    </td>
                    <td style="width:10%;">
                        <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>">编辑</a>
                        |
                        <asp:LinkButton ID="LinkButton2"  OnClientClick="return window.confirm('删除后不可恢复，你确定要删除吗？');" CommandName="del" CommandArgument='<%# Eval("Id") %>'
                            runat="server">删除</asp:LinkButton>
                    </td>
                     <td rowspan="2" style="width:15%;">
                        <a href="<%# Eval("Annex").ToString()!=""?"Dept_AnnexDetail.aspx?id="+Eval("Id")+"&aid=0&companyID=" + Request["companyID"]:"" %>" title="查看">
                            <img width="150" alt='<%# Eval("Title") %>' src='<%#Eval("Annex").ToString()!=""?"Dept_AnnexDetail.aspx?id="+Eval("Id")+"&aid=0&zs=1":""  %>' /></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                     附件：<%# GetUrl(Eval("Annex"), Eval("Id"))%><br />内容：<%# Eval("Content") %>&nbsp;
                    </td>
                </tr>
            </table>
            <%}
   else if (Request["type"] == "1" || Request["type"] == "4")
   {%>
            <table class="table" width="100%" style=" line-height:180%;">
                <tr>
                    <td style="width:40%;">
                       <b> <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>">
                            <%# Eval("Title") %></a></b>&nbsp;
                    </td>
                    <td style="width: 15%;">
                         <%# Eval("DeptName") %>
                    </td>                    
                    <td style=" width: 10%;">
                         <%# Eval("RealName") %>
                    </td>
                    <td style="width:10%;">
                        <%# ((DateTime)Eval("Addtime")).ToString("yyyy-MM-dd") %>&nbsp;
                    </td>
                    <td style="width:10%;">
                       <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>&ischeck=1">年审</a>
                        | <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>">编辑</a>
                        |
                        <asp:LinkButton ID="LinkButton3"  OnClientClick="return window.confirm('删除后不可恢复，你确定要删除吗？');" CommandName="del" CommandArgument='<%# Eval("Id") %>'
                            runat="server">删除</asp:LinkButton>
                    </td>
                     <td rowspan="2" style="width:15%;">
                        <a href="<%# Eval("Annex").ToString()!=""?"Dept_AnnexDetail.aspx?id="+Eval("Id")+"&aid=0&companyID=" + Request["companyID"]:"" %>" title="查看">
                            <img width="150" alt='<%# Eval("Title") %>' src='<%#Eval("Annex").ToString()!=""?"Dept_AnnexDetail.aspx?id="+Eval("Id")+"&aid=0&zs=1":""  %>' /></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        附件：<%# GetUrl(Eval("Annex"), Eval("Id"))%><br />内容：<%# Eval("Content") %>&nbsp;
                    </td>
                </tr>
            </table>
            <%}
   else
   { %>
   <table class="table" width="100%">
                <tr>
                    <td style="width: 35%;">
                        <b> <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>">
                            <%# Eval("Title") %></a></b>&nbsp;
                    </td>
                    <td style="width: 20%;">
                         <%# Eval("DeptName") %>
                    </td>                    
                    <td style=" width: 20%;">
                         <%# Eval("RealName") %>
                    </td>
                    <td style=" width: 10%;">
                        <a href="Dept_CompanyslicenseEdit.aspx?CompanyId=<%# Eval("CompanyId") %>&LicenseID=<%# Eval("Id") %>">编辑</a>
                        |
                        <asp:LinkButton ID="LinkButton4"  OnClientClick="return window.confirm('删除后不可恢复，你确定要删除吗？');" CommandName="del" CommandArgument='<%# Eval("Id") %>'
                            runat="server">删除</asp:LinkButton>
                    </td>
                     <td rowspan="2" style="width:15%;">
                        <a href="<%# Eval("Annex").ToString()!=""?"Dept_AnnexDetail.aspx?id="+Eval("Id")+"&aid=0&companyID=" + Request["companyID"]:"" %>" title="查看">
                            <img width="150" alt='<%# Eval("Title") %>' src='<%#Eval("Annex").ToString()!=""?"Dept_AnnexDetail.aspx?id="+Eval("Id")+"&aid=0&zs=1":""  %>' /></a>
                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
                        附件：<%# GetUrl(Eval("Annex"), Eval("Id"))%><br />内容：<%# Eval("Content") %>&nbsp;
                    </td>
                </tr>
            </table>
            <%} %>
        </ItemTemplate>
    </asp:DataList>
    <!-- 内容模块 -->
</asp:Content>
