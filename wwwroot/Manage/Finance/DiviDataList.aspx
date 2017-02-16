<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="DiviDataList.aspx.cs" ClientIDMode="Static" Inherits="wwwroot.Manage.Finance.DiviDataList" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 事业部提成报表 >>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_DiviDataList" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div style="width: 1000px; overflow-x: scroll;<%=(DropDownList1.Items.Count == 0?"display:none;":"")%>" id="datalist">
        <table class="table1" style='text-align: center;' id="table1">
            <tr style="font-weight: bold; background-color: #dddeee;">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <td>
                    <div style="width:80px;">日期</div>
                </td>
            </tr>
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <tr style="">
                        <%# Eval("Valuestr") %>
                        <td>
                            <%# Convert.ToDateTime(Eval("payingTime").ToString()).ToString("yyyy-MM-dd") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <br /><br>
    <script type="text/javascript">        
    document.getElementById("datalist").style.width = document.getElementById("interface_quick").clientWidth - 10 + "px";

        function round2(Num1, Num2) {
            if (isNaN(Num1) || isNaN(Num2)) {
                return (0);
            } else {
                return (Num1.toFixed(Num2));
            }
        }
        function sum() {
            if (document.getElementById("Label1").value == "") {
                var table = document.getElementById("table1");
                var tr = document.createElement("tr");
                var td = document.createElement("td");

                tr = table.insertRow();
                td = tr.insertCell();
                td.innerHTML = "<b>总计：</b>";

                for (var i = 1; i < table.rows(0).cells.length - 1; i++) {
                    td = tr.insertCell();
                    td.innerHTML = "0";
                }
                td = tr.insertCell();
                td.innerHTML = "";

                for (var i = 1; i < table.rows.length - 1; i++) {
                    for (var j = 1; j < tr.cells.length - 1; j++) {
                        tr.cells[j].innerHTML = round2(parseFloat(tr.cells[j].innerHTML) + parseFloat(table.rows(i).cells(j).innerHTML), 2);
                    }
                }
            }
        }
        sum();
    </script>
</asp:Content>
