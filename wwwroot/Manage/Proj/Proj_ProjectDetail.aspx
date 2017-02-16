<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Proj_ProjectDetail.aspx.cs" Inherits="wwwroot.Manage.Proj.Proj_ProjectDetail" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<style type="text/css">
tr.selectproc td
{
	font-weight:bold;
	 color:Red;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    项目管理 >> 项目申请
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="proj2" CurIndex="2" Param1="{Q:ProjectId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table3" style="line-height: 200%;">
            <tr>
                <td>
                    <b>项目名称:</b>
                </td>
                <td>
                    <div style="float:left; width:480px;"><asp:Literal ID="li_name" runat="server"></asp:Literal></div>
                    <div style="float:left;"><b>状态：</b><asp:Literal ID="li_state" runat="server"></asp:Literal>&nbsp;</div>
                </td>
            </tr>
            <tr>
                <td width="100">
                    <b>预计完成天数:</b>
                </td>
                <td>
                   <div style="float:left; width:480px;"> <asp:Literal ID="li_days" runat="server"></asp:Literal>&nbsp;天</div>
                    <div style="float:left; width:240px;"><b>预计投入资金：</b><asp:Literal ID="li_fee" runat="server"></asp:Literal>&nbsp;万元</div>
                </td>
            </tr> 
             <tr runat="server" id="tr1">
                <td>
                    <b>项目负责人:</b>
                </td>
                <td>
                    <asp:Literal ID="li_projmanage" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>项目参与人员:</b>
                </td>
                <td>
                    <asp:Literal ID="li_persons" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>预计到达效果:</b>
                </td>
                <td>
                    <asp:Literal ID="li_Imagine" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>项目方案:</b>
                </td>
                <td>
                <asp:Literal ID="li_content" runat="server"></asp:Literal><br />
                    <b>附件：</b><asp:Literal ID="li_annex" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr runat="server" id="tr2">
                <td>
                    <b>审批相关:</b>
                </td>
                <td>
                <div style="float:left; width:240px;"><b>审批人：</b><asp:Literal ID="li_checkmanage" runat="server"></asp:Literal></div>
                <div style="float:left; width:240px;"><b>审批资金：</b><asp:Literal ID="li_checkfee" runat="server"></asp:Literal>万元</div>
                    <div><b>审批时间：</b><asp:Literal ID="li_checktime" runat="server"></asp:Literal>&nbsp;</div>
                    <div><b>审批意见：</b><asp:Literal ID="li_checkopinion" runat="server"></asp:Literal>&nbsp;</div>
                    <asp:HiddenField ID="hi_procid" runat="server" />
                </td>
            </tr>
        </table><br />
        <table class="table">
        <thead>
            <tr>
                <td style="width:20px;">
                &nbsp;
                    </td>
                <td style="width:80px;">
                    排序
                </td>
                <td>
                    参与人数
                </td>
                <td>
                    占用天数
                </td>
                <td style="width:100px;">
                    开始时间
                </td>
                <td style="width:100px;">
                    结束时间
                </td>
                <td>
                    占项目总体百分比
                </td>
                <td>
                    占时间总体百分比</td>
                <td>
                    &nbsp;</td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="SupplierRepeater" runat='server'  >
            <ItemTemplate>
            <tr class="<%# getclass(Eval("NO")) %>">
                <td style=" background:#eeeeee;">
                    <asp:Image ID="Image1" Visible='<%# getimg(Eval("NO")) %>' ImageUrl="/images/green_arrow.gif" runat="server" />
                    </td>
               <td style=" background:#eeeeee;">
                    第<%#Eval("NO") %>步
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Persons")%>
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Days")%>
                </td>
                <td style=" background:#eeeeee;">
                     <%#Eval("Starttime").ToString()!=""?Convert.ToDateTime(Eval("Starttime").ToString()).ToString("yyyy-MM-dd"):"" %>
                </td>
                <td style=" background:#eeeeee;">
                      <%# Eval("Stoptime").ToString()!=""?Convert.ToDateTime(Eval("Stoptime").ToString()).ToString("yyyy-MM-dd"):"" %>
                </td>
                <td style=" background:#eeeeee;">
                     <%#Eval("Percnt")%>%
                </td>
                <td style=" background:#eeeeee;">
                    <%#Eval("Percnttime")%>%</td>   
                <td style=" background:#eeeeee;">
                    <asp:Button ID="Button1" runat="server" Visible='<%# getimg(Eval("NO")) %>' Text='<%# getbuttonname(Eval("NO"))%>' ToolTip='<%#Eval("NO")%>' OnClick="Button1_Click"  />
                  </td>              
            </tr>
            <tr>
                <td>
                    &nbsp;
                    </td><td height="35"> &nbsp;&nbsp;<b>详细部署：</b></td><td colspan="7"><%#Eval("Demo")%></td></tr>
            </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    </div>
</asp:Content>
