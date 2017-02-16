<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_TestAreaSelect.aspx.cs" Inherits="wwwroot.App_Test.WebForm_TestAreaSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        select.area {width:100px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="ddlProv" CssClass="area" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlProv_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlCity" CssClass="area" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlArea" CssClass="area" runat="server" dataType="Require"
                    msg="必填!">
                </asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
