<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectAreaCtrl.ascx.cs" EnableViewState="true"
    Inherits="wwwroot.App_Ctrl.SelectArea.SelectAreaCtrl" ClientIDMode="Static"  %>
<script src="/App_Ctrl/SelectArea/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="/App_Ctrl/SelectArea/default.js" type="text/javascript"></script>
<span>
    <asp:HiddenField ID="hidden_province" runat="server" Value="0" />
    <asp:DropDownList ID="ddlProvince" runat="server">
    </asp:DropDownList>
</span><span>
    <asp:HiddenField ID="hidden_city" runat="server" Value="0" />
    <select id="ddlCity" disabled="disabled" runat="server">
        <option value="0">--请选择--</option>
    </select>
</span><span>
    <asp:HiddenField ID="hidden_area" runat="server" Value="0" />
    <select id="ddlArea" disabled="disabled" runat="server">
        <option value="0">--请选择--</option>
    </select>
</span>