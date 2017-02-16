<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkOrder_Assign_Show.aspx.cs"
    Inherits="wwwroot.Manage.WorkOrder.WorkOrder_Assign_Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <style type="text/css">
    .hrstyle{ border-bottom:1px #888 dashed;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 850px; height: 540px; overflow-y: auto; overflow-x:hidden; padding-left: 5px;">
        <div style="width: 815px; border: 1px solid #aaa; padding: 10px; background-color: #eeefff;">
            <table height="100" width="100%">
                <tr>
                    <td width="80">
                        <b>任务名称：</b>
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Title_li" runat="server"></asp:Literal>
                    </td>
                    <td style="text-align: right; color: #999;">
                        下单人：<asp:Label ID="StateTime_la" runat="server"></asp:Label>
                    </td>
                </tr>  <tr>
                    <td>
                        <b>工作项目：</b>
                    </td>
                    <td>
                        <asp:Literal ID="Proj_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>工作分类：</b><asp:Literal ID="Type_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>任务期限：</b><asp:Literal ID="YJTime_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>当前状态：</b><asp:Literal ID="State_li" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                
                    <td>
                        <b>提交时间：</b>
                    </td>
                    <td>
                        <asp:Literal ID="SubTime_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>分配时间：</b><asp:Literal ID="FPTime_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>待验收时间：</b><asp:Literal ID="YSTime_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>完成时间：</b><asp:Literal ID="StopTime_li" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <b>任务描述：</b>
                    </td>
                    <td colspan="4">
                        <asp:Literal ID="Remarks_li" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 815px; height: 10px;">
        </div>
        <div style="width: 815px; height: 55px; border: 1px solid #aaa; padding: 10px;">
            <table height="55" width="100%">
                <tr>
                    <td width="200">
                        <b>任务量：</b><asp:Literal ID="Count_my" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>任务期限：</b><asp:Literal ID="YJTime_my" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <b>任务状态：</b><asp:Literal ID="State_my" runat="server"></asp:Literal>
                    </td>
                </tr>
               <tr>

                    <td>
                        <b>创建时间：</b><asp:Literal ID="AddTime_my" runat="server"></asp:Literal>
                    </td>
                    <td><b>待验收时间：</b><asp:Literal ID="YSTime_my" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <b>完成时间：</b><asp:Literal ID="StopTime_my" runat="server"></asp:Literal>
                    </td>
                    <td style="text-align: right; color: #999;">
                        分配人：<asp:Literal ID="FPTime_my" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 815px; height: 10px;">
        </div>
        <div>
            <table>
                <tr>
                    <td id="mess" runat="server" visible="false" style="width: 815px;">
                        <table id="messfs" runat="server">
                            <tr>
                                <td>
                                    <asp:TextBox ID="MessContent_txt" runat="server" Rows="3" Columns="90" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="FS_drop" runat="server">
                                    </asp:DropDownList>
                                    <asp:Button ID="Button1" runat="server" Text=" 发送 " OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        <fieldset style="background-color: #eee;height:240px;
                            overflow-y: auto; overflow-x: hidden; width: 88%; padding: 5px; padding-left: 15px;
                            padding-right: 10px; line-height: 180%;">
                            <legend style="font-weight: bold;">
                                <img src="/images/4.gif" />消息记录 </legend>
                            <asp:DataList ID="DataList1" runat="server" Width="98%">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="padding: 5px; padding-left: 5px; padding-right: 5px;">
                                                <div style="<%# Eval("ToUserID").ToString()==WX.Main.CurUser.UserID?"color:Blue !important;": (Eval("FromUserID").ToString()==WX.Main.CurUser.UserID?"color:Green !important;":"")%>">
                                                    <%# Eval("FromUserID").ToString()==WX.Main.CurUser.UserID?"我":(Eval("FromUserID").ToString()==""?"系统消息":WX.CommonUtils.GetRealNameListByUserIdList(Eval("FromUserID").ToString()))%>
                                                @<%# Eval("ToUserID").ToString() == WX.Main.CurUser.UserID ? "我" : (Eval("ToUserID").ToString()==""?"大家":WX.CommonUtils.GetRealNameListByUserIdList(Eval("ToUserID").ToString()))%>
                                                    <%#Eval("AddTime")%>&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1" CommandArgument='<%#Eval("FromUserID")%>'
                                                        Visible='<%#Eval("FromUserID").ToString()==WX.Main.CurUser.UserID?false:true %>'
                                                        OnClick="Button2_Click" runat="server">回复</asp:LinkButton>&nbsp;&nbsp;<%# (Eval("ToUserID").ToString() == WX.Main.CurUser.UserID && Eval("State").ToString()=="0"?"<img style='padding-top:5px;' src='/images/procstate1.bmp' alt='新消息'/>":"")%></div>
                                                <div style="color: #333; padding-left: 20px; font-weight: bold;">
                                                    <%#Eval("Remarks")%></div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle CssClass="pstyle" />
                            </asp:DataList>
                        </fieldset>
                    </td>
                    <td id="pingjiafs" runat="server" visible="false" style="width:815px;">
                        <fieldset style="background-color: #eee;height:240px;
                            overflow-y: auto; overflow-x: hidden; width: 88%; padding: 5px; padding-left: 15px;
                            padding-right: 10px; line-height: 150%;">
                            <legend style="font-weight: bold;">
                                <img src="/images/edit.gif" />评价 </legend>
                            <asp:DataList ID="DataList2" runat="server" Width="98%">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td style="padding: 5px; padding-left: 5px; padding-right: 5px;">
                                            <div style="color: #333;">
                                                    <%#Eval("Remarks")%></div>
                                                <div style="color:#888 !important; float:right;">
                                                    <%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>&nbsp;&nbsp;
                                                    <%#Eval("AddTime")%></div>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle CssClass="hrstyle" />
                            </asp:DataList>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <table id="pingjia" runat="server" visible="false">
                <tr>
                    <td>
                        <asp:TextBox ID="AppContent_txt" runat="server" Rows="3" Columns="90" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text=" 评价 " OnClick="Button3_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
