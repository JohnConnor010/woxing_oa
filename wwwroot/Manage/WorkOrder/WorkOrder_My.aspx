<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="WorkOrder_My.aspx.cs" Inherits="wwwroot.Manage.WorkOrder.WorkOrder_My" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/JS/WorkOrder.js"></script>
    <style type="text/css">
.deptstyle{ background-color:#CFD6DD;}
.pstyle{ background-color:#E0E7EE;}
.state3{ font-weight:bold !important;}
.state4{color:Green !important;}
.state5{color:Red !important;}
.state6{color:Fuchsia !important;}
.state7{color:Blue !important;}
.state8{color:Maroon !important;}
.state9{color:#666 !important;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    工作单管理 >> 工作单列表
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="WorkOrder_My" CurIndex="1"  Param1="{Q:xUser}"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <table class="table1" style="background-image:url(/images/mywork8.jpg)">
    <asp:Repeater ID="Repeater2" runat="server">
        <HeaderTemplate>
            <thead>
                <td>
                    我的工作台
                </td>
                <td style="width: 60px;">
                    工作量
                </td>
                <td style="width: 120px">
                    工作项目
                </td>
                <td style="width: 60px;">
                    提交人
                </td>
                <td style="width: 50px;">
                    状态
                </td>
                <td style="width: 80px;">
                    到期时间
                </td>
                <td style="width: 100px;">
                    排班时间
                </td>
                <td style="width: 75px;">
                    周期
                </td>
            </thead>
        </HeaderTemplate>
        <ItemTemplate>
        <tr ondblclick="ZD(document.getElementById('img<%#Eval("ID")%>'),'item<%#Eval("ID")%>');">
        <td style="padding-left:10px;">
            <%# Container.ItemIndex + 1%>&nbsp;&nbsp;<a style="color:#5C1400;" href="javascript:PopupIFrame('<%#WX.Main.DealWithUrlForClient("WorkOrder_my_Show.aspx?OrderID="+Eval("ID").ToString())%>','查看任务','','',850,550)"><%# Eval("Title")%></a></td>
            <td><%# Eval("Count")%></td>
            <td>
                <%# WX.WorkOrder.Order.ProjStr[Convert.ToInt32(Eval("Proj"))]+"（"+WX.WorkOrder.Order.TypeStr[Convert.ToInt32(Eval("Type"))]+"）"%>
            </td>
            <td>
                <%#WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>
            </td>
                  
                    <td class='state<%# Eval("State") %>'>
                    <%# WX.WorkOrder.Order.StateStr[Convert.ToInt32(Eval("State"))]%>
                    </td>
            <td>
                <%#Eval("YJTime").ToString() != "" ? Convert.ToDateTime(Eval("YJTime").ToString()).ToString("yyyy-MM-dd") : ""%>
            </td>
            <td>
                <%#Eval("pl").ToString()%>
            </td>
                    <td><div style="float:left;">
                        <asp:TextBox ID="TextBox1" Width="50" OnTextChanged="TextBox1_TextChanged" ToolTip='<%# Eval("ID")%>' AutoPostBack="true" runat="server" Text='<%# Eval("TopCount")%>'></asp:TextBox>
                    </div>
                    <div style="width:22px; float:left;">
                        <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="置顶" CommandName='<%#Container.ItemIndex%>' CommandArgument='<%# Eval("ID")%>' Visible='<%#Container.ItemIndex + 1!=1%>' ImageUrl="/images/up.gif" OnClick="ImageButton1_Click" />
                    </div>
                    </td>
            </tr>
            </ItemTemplate>
   </asp:Repeater>
   </table><br />
    <div style=" padding-left:20px; font-weight:bold;"><asp:Label ID="Label1" runat="server" Text=""></asp:Label></div>
        <table class="table1">
    <asp:Repeater ID="Repeater1" runat="server">
        <HeaderTemplate>
            <thead>
                <td>
                    任务名称
                </td>
                <td style="width: 120px">
                    工作项目
                </td>
                <td style="width: 60px;">
                    提交人
                </td>
                <td style="width: 100px;">
                    到期时间
                </td>
                <td style="width: 40px;">
                    提交
                </td>
                <td style="width: 40px;">
                    分配
                </td>
                <td style="width: 40px;">
                    执行
                </td>
                <td style="width: 40px;">
                    验收
                </td>
                <td style="width: 80px;">
                    状态
                </td>
            </thead>
        </HeaderTemplate>
        <ItemTemplate>
        <tr ondblclick="ZD(document.getElementById('img<%#Eval("ID")%>'),'item<%#Eval("ID")%>');"><td class="pstyle">
            <img src="/images/jia.jpg" id='img<%#Eval("ID")%>' onclick="ZD(this,'item<%#Eval("ID")%>');" /><img src="/images/dept<%#Eval("DeptWorkID")%>.gif" alt="<%#Eval("DeptWorkID").ToString()=="-1"?"本部门":"其它部门"%>" /><b><a <%#Eval("State").ToString()=="8"?"style='text-decoration:line-through;'":""%> href="javascript:PopupIFrame('<%#WX.Main.DealWithUrlForClient("WorkOrder_Show.aspx?OrderID="+Eval("ID").ToString())%>','查看任务','','',850,550)"><%#Eval("Title")%></a></b>
            </td>
            <td class="pstyle">
                <%# WX.WorkOrder.Order.ProjStr[Convert.ToInt32(Eval("Proj"))]+"（"+WX.WorkOrder.Order.TypeStr[Convert.ToInt32(Eval("Type"))]+"）"%>
            </td>
            <td class="pstyle">
                <%#WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>
            </td>
            <td class="pstyle">
                <%#Eval("YJTime").ToString() != "" ? Convert.ToDateTime(Eval("YJTime").ToString()).ToString("MM-dd HH:mm") : ""%>
            </td>
                    <td class="pstyle">
                        <%# GetTimeimg(Eval("SubTime").ToString(), Eval("AddTime").ToString(), 1, Convert.ToInt32(Eval("ID")), 1, 0)%>
                    </td>
                    <td class="pstyle">
                        <%#GetTimeimg(Eval("FPTime").ToString(), Eval("SubTime").ToString(), 2, Convert.ToInt32(Eval("ID")), 1, 0)%>
                    </td>
                    <td class="pstyle">
                     <%#GetTimeimg(Eval("YSTime").ToString(), Eval("FPTime").ToString(), 3, Convert.ToInt32(Eval("ID")), 1, 0)%>
                    </td>
                    <td class="pstyle">
                    <%#GetTimeimg(Eval("StopTime").ToString(), Eval("YSTime").ToString(), 4, Convert.ToInt32(Eval("ID")), 1, 0)%>
                    </td>
            <td class="pstyle">
            <div class='state<%# Eval("State") %>'>
                <%# WX.WorkOrder.Order.StateStr[Convert.ToInt32(Eval("State"))]%></div>
            </td>
            </tr>
            <tr style=" display:none;" class="item<%#Eval("ID")%>">
            <%# Getchilds(Convert.ToInt32(Eval("ID")))%>
            </tr>
            </ItemTemplate>
   </asp:Repeater>
   </table>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>
