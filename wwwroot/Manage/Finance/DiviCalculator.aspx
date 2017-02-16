<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="DiviCalculator.aspx.cs" Inherits="wwwroot.Manage.Finance.DiviCalculator" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 提成计算器 >>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_DiviDataList" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
    <table style='width: 420px; <%=(DropDownList1.Items.Count == 0&&DropDownList2.Visible==false?"display:none;": "")%>'
        align="center" class="table3">
        <tr style='<%=(DropDownList2.Visible==false?"display:none;": "")%>'>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                事业部：
            </td>
            <td>
                 <asp:DropDownList ID="DropDownList2" runat="server" 
                    onselectedindexchanged="DropDownList2_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                业务类型：
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:DropDownList ID="Norm" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                销售额：
            </td>
            <td>
                <asp:TextBox ID="Fee" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr style='display:<%=WX.Model.TypeColum.VarIsNone(DropDownList1.SelectedValue,"Mamount") %>'>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                发稿量：
            </td>
            <td>
                <asp:TextBox ID="Mamount" runat="server" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr style='display:<%=WX.Model.TypeColum.VarIsNone(DropDownList1.SelectedValue,"yqczfee") %>'>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                舆情处置费：
            </td>
            <td>
                <asp:TextBox ID="yqczfee" runat="server" Text="0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="计算" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div style="width: 1000px; overflow-x: scroll; <%=(DropDownList1.Items.Count == 0?"display:none;": "")%>"
        id="datalist">
        <table class="table1" style="text-align: center;" id="table1">
            <tr style="font-weight: bold; background-color: #dddeee;">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </tr>
            <tr style="">
                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        document.getElementById("datalist").style.width = document.getElementById("interface_quick").clientWidth - 10 + "px";

    </script>
</asp:Content>
