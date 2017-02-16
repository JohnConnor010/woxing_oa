<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crm_SingleM_ShowTrack.aspx.cs"
    Inherits="wwwroot.Manage.CRM.Crm_SingleM_ShowTrack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript">

        function printView(id) {
            alert(id);
            var sprnhtml = document.getElementById(id).outerHTML;
            alert(id);
            //            var selfhtml = window.document.body.innerHTML; //获取当前页的html

            //            window.document.body.innerHTML = sprnhtml;
            //            window.print();
            //            window.document.body.innerHTML = selfhtml;
            bdhtml = window.document.body.innerHTML;

            window.document.body.innerHTML = sprnhtml;
            window.print();
            window.document.body.innerHTML = bdhtml;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div style="width: 850px; height: 435px; overflow-y: auto;">
        <table class="table3" height="350">
            <tr>
                <td style="width: 80px;">
                    <b>跟踪过程：</b>
                </td>
                <td>
                    <asp:Literal ID="LiProcessState" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>跟踪时间：</b>
                </td>
                <td>
                    <asp:Literal ID="LiTrackTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr id="ccp" runat="server" visible="false">
                <td colspan="2">
                    <table class="table3" id="print" style="width: 18cm; border: 1px solid #888888;">
                        <tr>
                            <td valign="top" style="padding: 1.5cm;">
                                <div style="font-size: 18px; font-weight: bold; text-align: center;">
                                    <asp:Literal ID="liProgramTitle" runat="server"></asp:Literal></div>
                                <br />
                                <br />
                                <div style="font-size: 14px; line-height: 180%; text-indent: 2em;">
                                    <asp:Literal ID="liProgramContent" runat="server"></asp:Literal></div>
                                <br />
                                <table class="table3">
                                    <tr style="text-align: center; font-weight: bold;">
                                        <td>
                                            合作形式
                                        </td>
                                        <td>
                                            描述
                                        </td>
                                        <td>
                                            报价
                                        </td>
                                        <td>
                                            最低价格
                                        </td>
                                        <td>
                                            其它补充
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="CustomerRepeater" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td align="left">
                                                    <%#Eval("ProductName")%>
                                                </td>
                                                <td width="200">
                                                    <%#Eval("PRemark")%>
                                                </td>
                                                <td>
                                                    <%#Eval("SalesPrice")%>
                                                </td>
                                                <td>
                                                    <%#Eval("ZDFee")%>
                                                </td>
                                                <td width="120">
                                                    <%#Eval("Remarks")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <br />
                                <br />
                                <div style="font-size: 14px; text-align: right;">
                                    <asp:Literal ID="liProgramLK" runat="server"></asp:Literal></div>
                                <br />
                                <div style="font-size: 14px; text-align: right;">
                                    <asp:Literal ID="liProgramTime" runat="server"></asp:Literal></div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div style="float: left; width: 120px;">
                        <b>电子版：</b></div>
                    <div style="float: left; width: 560px;">
                        <asp:Literal ID="liProgramPath" runat="server"></asp:Literal></div>
                    <div style="float: left;">
                        <a href="javascript:void(0)" onclick="printView('print');">打印方案</a></div>
                </td>
            </tr>
            <tr>
                <td>
                    <b>跟踪次数：</b>
                </td>
                <td>
                    <asp:Literal ID="LiTrackNo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>本次花销：</b>
                </td>
                <td>
                    <asp:Literal ID="LiFee" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>情况汇总：</b>
                </td>
                <td>
                    <asp:Literal ID="Liremark" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <b>其它信息：</b>
                </td>
                <td>
                    <asp:Literal ID="LiLogParaments" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
