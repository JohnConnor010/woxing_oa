﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="DataList.aspx.cs" ClientIDMode="Static"  Inherits="wwwroot.Manage.Finance.DataList" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    财务管理 >> 数据报表 >>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_DataList" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
  
    <table width="90%">
        <tr>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right" style='<%=(DropDownList1.Items.Count == 0?"display:none;":"")%>'>
                <input type="button" value="打印预览" onclick="PrintSubPage('datalist')" />
            </td>
        </tr>
    </table>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div style="width: 1000px; overflow-x: scroll;<%=(DropDownList1.Items.Count == 0?"display:none;":"")%>" id="datalist">
        <table style="text-align: center;" id="table1" align="center" class="table1">
            <tr style="font-weight: bold; background-color: #dddeee;">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <td>
                    <div style="width: 80px;">
                        日期</div>
                </td>
            </tr>
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <tr style="">
                        <%# Eval("Valuestr3") %>
                        <td>
                            <%# Convert.ToDateTime(Eval("payingTime").ToString()).ToString("yyyy-MM-dd") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
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
                td.innerHTML = "";
                td = tr.insertCell();
                td.innerHTML = "";
                td = tr.insertCell();
                td.innerHTML = "<b>总计：</b>";

                for (var i = 3; i < table.rows(0).cells.length - 1; i++) {
                    td = tr.insertCell();
                    td.innerHTML = "0";
                }
                td = tr.insertCell();
                td.innerHTML = "";

                for (var K = 1; K < table.rows.length - 1; K++) {
                    for (var j = 3; j < tr.cells.length - 1; j++) {
                        tr.cells[j].innerHTML = round2(parseFloat(tr.cells[j].innerHTML) + parseFloat(table.rows(K).cells(j).innerHTML), 2); ;
                    }
                }
            }
        }
        sum();
    </script>
</asp:Content>
