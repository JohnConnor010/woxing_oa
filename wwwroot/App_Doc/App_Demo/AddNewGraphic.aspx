<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewGraphic.aspx.cs" Inherits="wwwroot.App_Demo.AddNewGraphic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 342px;
        }
        .style3
        {
            width: 74px;
        }
        .style4
        {
            height: 342px;
            width: 74px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>添加节点名称<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        </div>
        <table class="style1" 
            style="border-style: groove; border-width: thin; width: 700px">
            <tr>
                <td class="style3" style="width: 30%">
                    借点序号：</td>
                <td colspan="3" style="width: 50%">
                    <asp:TextBox ID="txtNodeId" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    借点类型：</td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlNodeType" runat="server">
                        <asp:ListItem Value="1">常规借点</asp:ListItem>
                        <asp:ListItem Value="2">子节点</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style3">
                    借点名称：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtNodeName" runat="server" Width="312px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <asp:UpdatePanel ID="UpatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <td class="style4">
                            下一步借点：</td>
                        <td align="left" class="style2" valign="middle" width="200">
                            <asp:ListBox ID="ListBox1" runat="server" Height="100%" Width="200px" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>
                        <td align="center" class="style2" valign="middle" width="80">
                            <asp:Button ID="btnAddList" runat="server" Text=" >> " OnClick="btnAddList_Click" />
                            <br />
                            <br />
                            <asp:Button ID="btnRemoveList" runat="server" Text=" << " OnClick="btnRemoveList_Click" />
                        </td>
                        <td align="left" class="style2" valign="middle" width="40%">
                            <asp:ListBox ID="ListBox2" runat="server" Height="100%" Width="200px" SelectionMode="Multiple">
                            </asp:ListBox>
                        </td>                    
                    </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" Width="100px" 
                        onclick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
