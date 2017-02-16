<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotifyFileDetail.aspx.cs"
    Inherits="wwwroot.Manage.XZ.NotifyFileDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
</head>
<body>
<form id="form1" runat="server">
    <div style="width: 100%; overflow-y: auto; height: 420px;">
        <table class="table" style="line-height: 200%; height: 380px; width: 96%;">
            <tr>
                <td style="background: #dddddd; font-weight: bold; text-align: center; height: 25px;"
                    colspan="2" align="center">
                    <asp:Literal ID="ui_title" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Literal ID="ui_Code" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="background: #eeeeee; text-align: right; height: 20px;" colspan="2">
                    范围：<asp:Literal ID="ui_Area" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;拟写人：<asp:Literal ID="ui_username" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal
                        ID="li_Addtime" runat="server"></asp:Literal>
                </td>
            </tr>
        <tr>
            <td style="padding:10px; text-indent:24px; vertical-align:top;" colspan="2"> 
               <asp:Literal ID="li_content" runat="server"></asp:Literal>
                    <br />
                    <asp:Literal ID="Annex_li" runat="server"></asp:Literal>
                    <br />
                       <center><asp:Button ID="Button2" Visible="false" runat="server" Text="确认发布" CssClass="BigButton" OnClick="btn_Send" /></center> 
            </td>
        </tr>
             <tr>
               <td valign="top" width="80%">
                <table border="0" align="center" width="95%" class="small">
                    <tr>
                        <td class="Big">
                            <img src="/images/green_arrow.gif" align="absmiddle" /><span class="big3"> <b>会签意见区</b></span>
                        </td>
                    </tr>
                    <asp:Literal ID="liSignList" runat="server"></asp:Literal>
                </table>
                <div style="clear:both;" />
                <table class="TableBlock" align="center" width="95%" id="qz" runat="server">
                    <tr class="TableContent">
                        <td id="FORM_CONTENTPosition">
                        <div style="width: 85%; float:left;">
                            <asp:TextBox runat="server" ID="txt_sign" TextMode="MultiLine" Width="100%" Height="150">
                            </asp:TextBox>
                        </div>
                        <div style="width:14%; text-align:left;float:right;">
                             <asp:Literal ID="liCurUserSealButton" runat="server"></asp:Literal>
                             <br/><br/>
                        </div>
                        <div style="clear:both;" />
                        </td>
                    </tr>
                    <tr><td> <asp:Button ID="Button1" runat="server" Text="通过" CssClass="BigButton" OnClick="btn_EnterPass"
                            OnClientClick="return GetValue_OnSubmit()" />
                        <asp:Button ID="Button4" runat="server" Text="未通过" CssClass="BigButton" OnClick="btn_Cancel" /></td></tr>
                </table>
                                <asp:HiddenField ID="txtSealData" runat="server" />
               </td>
                <td valign="top">
                    <table class="table">
                        <thead>
                            <tr class="">
                                <td align="center">
                                    流程步骤
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="ProcessRepeater" runat="server">
                            <ItemTemplate>
                            <tr class="">
                                <td style='text-align:center;<%#Eval("StepNo").ToString()==stepno.ToString()?"color:Red;":"" %>'>
                                    <span style="margin-left: 25px;"><%#Eval("StepNo") %></span><span style=" font-weight: bold;"><%#Eval("Name") %></span><br />
                                    ↓
                                </td>
                            </tr>
                            </ItemTemplate>
                            </asp:Repeater>
                            
                            <tr class="">
                                <td style=" text-align:center">
                                    <span style=" font-weight: bold;">审批完成</span><br />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
