<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="WorkOrder_Fenpei.aspx.cs"
    Inherits="wwwroot.Manage.WorkOrder.WorkOrder_Fenpei" %>    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
    <script type="text/javascript" src="/App_Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="/App_Scripts/QueryString.js"></script>
    
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div style="width: 850px; height: 440px; overflow-y: auto; padding-left:5px;">
        <div style="width: 815px; height: 80px; border: 1px solid #aaa; padding: 10px; background-color:#eeefff;">
            <table height="80" width="100%">
                <tr>
                    <td width="80">
                        <b>任务名称：</b>
                    </td>
                    <td colspan="2">
                        <asp:Literal ID="Title_li" runat="server"></asp:Literal>
                    </td>
                    <td style="text-align:right; color:#999;"><asp:Literal ID="StateTime_li" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>工作项目：</b>
                    </td>
                    <td width="100">
                        <asp:Literal ID="Proj_li" runat="server"></asp:Literal>
                    </td>
                    <td width="200">
                        <b>工作分类：</b><asp:Literal ID="Type_li" runat="server"></asp:Literal>
                    </td>
                    <td width="220">
                        <b>任务期限：</b><asp:Literal ID="YJTime_li" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>任务要求：</b>
                    </td>
                    <td colspan="3">
                        <asp:Literal ID="Remarks_li" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div><div style="width: 815px; height:10px;"></div>
        <div style="width: 815px; border: 1px solid #aaa; padding: 10px;">
       <div style="width: 815px; height:10px;"></div>
        <asp:DataList ID="DataList2" CssClass="table1" runat="server">
        <HeaderTemplate>
            <thead>
                <td>
                    姓名
                </td>
                <td style="width: 150px">
                    工作量
                </td>
                <td style="width: 80px;">
                    编辑
                </td>
            </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("ID")%>' />
            <asp:CheckBox ID="CheckBox1" runat="server" ToolTip='<%#Eval("UserID")%>' Checked='<%# Eval("ID").ToString()==""?false:true%>' /> <b><%# Eval("RealName")%></b>
            </td>            
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("cCount")%>'></asp:TextBox>
            </td>
            <td><a href='WorkOrder_FenpeiDetail.aspx?OrderID=<%=Request["OrderID"] %><%# Eval("ID").ToString()==""?"":"&POrderID="+Eval("ID")%>&UserID=<%# Eval("UserID")%>'>详细</a>
        </ItemTemplate>
    </asp:DataList>
    &nbsp;&nbsp;<asp:Button
                        ID="Button1" runat="server" Text="提交" onclick="Button1_Click" />
        </div>
    </div>
    </form>
</body>
</html>
