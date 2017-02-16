<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRM_SingleM_EditTrack.aspx.cs"
    Inherits="wwwroot.Manage.CRM.CRM_SingleM_EditTrack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript">
        function Messages(str) {
            alert(str);
        }

        <%=mess %>
    </script>
    <style type="text/css">
        .inputcontent
        {
            text-indent: 2em;
            width: 100%;
            font-size: 14px;
            border: 0px;
            overflow: hidden;
            line-height: 180%;
        }
        .inputLK
        {
            text-align: right;
            padding-right: 20px;
            width: 100%;
            font-size: 14px;
            border: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div style="width: 850px; height: 450px; overflow-y: auto;">
        <table class="table3" height="350">
            <tr>
                <td width="80">
                    <b>跟踪过程：</b>
                </td>
                <td>
                    <div style="float: left;">
                        <asp:DropDownList ID="ddlProcessState" runat="server" OnSelectedIndexChanged="ddlProcessState_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div style="float: left;">
                        <asp:RadioButtonList ID="rblstate" runat="server" RepeatColumns="2">
                            <asp:ListItem Value="0" Text="未执行" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="已执行"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </td>
            </tr>
            <tr id="tr1" runat="server" visible="false">
                <td>
                    <b>计划时间：</b>
                </td>
                <td>
                    <asp:Label ID="jhdate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>跟踪时间：</b>
                </td>
                <td>
                    <asp:TextBox ID="txtTrackTime" runat="server" class="easyui-datetimebox" Width="150"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text="提前一天">
                    </asp:Button>&nbsp;&nbsp;<asp:Button ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"
                        Text="当前时间"></asp:Button>&nbsp;&nbsp;<asp:Button ID="LinkButton3" runat="server"
                            OnClick="LinkButton3_Click" Text="推迟一天"></asp:Button>
                </td>
            </tr>
            <tr id="ccp" runat="server" visible="false">
                <td colspan="2">
                    <table class="table3" style="width: 18cm; border: 1px solid #888888;">
                        <tr style="text-align: center;">
                            <td valign="top" style="padding: 1.5cm;">
                                <asp:TextBox ID="txtcustomername" runat="server" Font-Size="18" Font-Bold="true"
                                    Width="100%" BorderStyle="None" CssClass="input_1"></asp:TextBox>
                                <br />
                                <br />
                                <asp:TextBox ID="txtContent" Text="方案内容。。。。" runat="server" CssClass="inputcontent"
                                    TextMode="MultiLine" onpropertychange="this.style.posHeight=this.scrollHeight+2"></asp:TextBox>
                                <br />
                                <table class="table3">
                                    <tr style="text-align: center; font-weight: bold;">
                                        <td width="120">
                                            合作形式
                                        </td>
                                        <td>
                                            描述
                                        </td>
                                        <td width="60">
                                            报价
                                        </td>
                                        <td width="60">
                                            最低价格
                                        </td>
                                        <td width="80">
                                            其它补充
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="CustomerRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="left">
                                                    <asp:CheckBox ID="CheckBox1" ToolTip='<%#Eval("Id") %>' runat="server" Checked='<%#Eval("ccpID").ToString()!=""?true:false %>' /><%#Eval("ProductName")%>
                                                </td>
                                                <td width="30%">
                                                    <%#Eval("Remark")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SalesPrice")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="zdfee1" runat="server" Text='<%#Eval("ZDFee")%>' Width="50"></asp:TextBox>
                                                </td>
                                                <td width="100">
                                                    <asp:TextBox TextMode="MultiLine" Rows="2" Columns="12" ID="Remarks1" runat="server"
                                                        Text='<%#Eval("Remarks")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <br />
                                <br />
                                <asp:TextBox ID="txtLK" Text="中国经济网山东频道" BorderStyle="None" runat="server" CssClass="inputLK"></asp:TextBox>
                                <br />
                                <asp:TextBox ID="txtTime" Text="" runat="server" BorderStyle="None" CssClass="inputLK"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div style="float: left; width: 18cm;">
                        <b>电子版上传：</b>&nbsp;&nbsp;<asp:FileUpload ID="tfile" runat="server" /></div>
                </td>
            </tr>
            <tr id="Tr2" runat="server" visible="false">
                <td colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" class="table5" width="100%">
                        <tr>
                            <td>
                               <b>促成方案：</b>
                                <asp:DropDownList ID="DropProgram" runat="server">
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <b>余额：</b><asp:TextBox ID="txtOverFee" runat="server" Width="50"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <b>余额时间：</b><asp:TextBox ID="txtOverTime" runat="server" class="easyui-datebox" Width="90"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <b>已开发票金额：</b><asp:TextBox ID="txtInvoice" runat="server" Width="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               <b>签订时间：</b>
                                <asp:TextBox ID="txtAddtime" runat="server" class="easyui-datebox" Width="90"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <b>生效时间：</b><asp:TextBox ID="txtStartTime" runat="server" class="easyui-datebox" Width="90"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <b>结束时间：</b><asp:TextBox ID="txtStopTime" runat="server" class="easyui-datebox" Width="90"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <table class="table3">
                                    <tr style="text-align: center; font-weight: bold;">
                                        <td width="120">
                                            合作形式
                                        </td>
                                        <td>
                                            描述
                                        </td>
                                        <td width="60">
                                            报价
                                        </td>
                                        <td width="60">
                                            协议价格
                                        </td>
                                        <td width="80">
                                            其它补充
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="left">
                                                    <asp:CheckBox ID="CheckBox2" ToolTip='<%#Eval("Id") %>' runat="server" Checked='<%#Eval("ccpID").ToString()!=""?true:false %>' /><%#Eval("ProductName")%>
                                                </td>
                                                <td width="50%">
                                                    <%#Eval("Remark")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SalesPrice")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="zdfee2" runat="server" Text='<%#Eval("ZDFee")%>' Width="50"></asp:TextBox>
                                                </td>
                                                <td width="100">
                                                    <asp:TextBox TextMode="MultiLine" Rows="2" Columns="12" ID="Remarks2" runat="server"
                                                        Text='<%#Eval("Remarks")%>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <b>跟踪次数：</b>
                </td>
                <td>
                    <asp:TextBox ID="txtTrackNo" runat="server" Width="40" Text="0" MaxLength="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>本次花销：</b>
                </td>
                <td>
                    <asp:TextBox ID="txtFee" runat="server" Width="40" Text="0" MaxLength="8"></asp:TextBox>元
                </td>
            </tr>
            <tr>
                <td>
                    <b>情况汇总：</b>
                </td>
                <td>
                    目标预测：<asp:TextBox ID="txtremark" runat="server" Width="500"></asp:TextBox>
                    <br />
                    难&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;点：<asp:TextBox ID="txtremark2" runat="server"
                        Width="500"></asp:TextBox>
                    <br />
                    解决方法：<asp:TextBox ID="txtremark3" runat="server" Width="500"></asp:TextBox>
                    <br />
                    目标达成：<asp:TextBox ID="txtremark4" runat="server" Width="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>跟踪详情：</b>
                </td>
                <td>
                    <asp:TextBox ID="txtLogParaments" runat="server" Width="560" TextMode="MultiLine"
                        Rows="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>&nbsp;</b>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
