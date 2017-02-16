<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Calculator.aspx.cs" Inherits="wwwroot.Manage.Finance.Calculator"
    ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
 
     <script type="text/javascript">

         function subcalculator(valuestr,type) {
             var arry = document.getElementById("HiddenField1").value;
             var arry2 = document.getElementById("HiddenField2").value;
             var newstr = valuestr;
             if(type>0)
             valuestr = valuestr.split("|")[0];
             if (type == 1 && newstr.split("|")[1] == "%") {
                 newstr = (valuestr * 100) + "%";
             }
             else
                 newstr = valuestr;
             switch (valuestr) {
                 case "del": 
                     {
                         arry = arry.lastIndexOf(",") == -1 && arry != "" ? (arry.length == 1 ? "" : arry.substr(0, arry.length - 1)) : arry.substr(0, arry.lastIndexOf(","));
                         arry2 = arry2.lastIndexOf(",") == -1 && arry2 != "" ? (arry2.length == 1 ? "" : arry2.substr(0, arry2.length - 1)) : arry2.substr(0, arry2.lastIndexOf(","));
                     }
                     break;
                 case "clear": arry = ""; arry2 = ""; break;
                 default: arry = arry + "," + valuestr; arry2 = arry2 + "," + newstr; break;
             }
             document.getElementById("HiddenField1").value = arry;
             document.getElementById("HiddenField2").value = arry2;
                 document.getElementById("TextBox1").value = arry2.replace(new RegExp(",", "gmi"), "");
                 document.getElementById("HiddenField3").value = arry.replace(new RegExp(",", "gmi"), "");
             }
            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    财务管理 >> 成本扣化计算器 >>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_Calculator" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div style="width:1000px;">
    <table style="width: 480px;" align="center" class="table3">
    <tr>
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
                类型：
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                公式名称：
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                列标识：
            </td>
            <td>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                可视：
            </td>
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Text="业务员" />
                <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" Text="事业部" />
                <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" Text="其它" />
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                排序：
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" Width="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                公式：
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="280"></asp:TextBox>
                <asp:HiddenField ID="HiddenID" runat="server" />
                <asp:Button ID="Button21" runat="server" Text=" 提交 " OnClick="Button21_Click" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button1" runat="server" Text=" 1 " OnClientClick="subcalculator(1,0);" />
                <asp:Button ID="Button2" runat="server" Text=" 2 " OnClientClick="subcalculator(2,0)" />
                <asp:Button ID="Button3" runat="server" Text=" 3 " OnClientClick="subcalculator(3,0)" />
                <asp:Button ID="Button4" runat="server" Text=" 4 " OnClientClick="subcalculator(4,0)" />
                <asp:Button ID="Button5" runat="server" Text=" 5 " OnClientClick="subcalculator(5,0)" />
                <asp:Button ID="Button6" runat="server" Text=" 6 " OnClientClick="subcalculator(6,0)" />
                <asp:Button ID="Button7" runat="server" Text=" 7 " OnClientClick="subcalculator(7,0)" />
                <asp:Button ID="Button8" runat="server" Text=" 8 " OnClientClick="subcalculator(8,0)" />
                <asp:Button ID="Button9" runat="server" Text=" 9 " OnClientClick="subcalculator(9,0)" />
                <asp:Button ID="Button10" runat="server" Text=" 0 " OnClientClick="subcalculator(0,0)" />
                <asp:Button ID="Button11" runat="server" Text=" + " OnClientClick="subcalculator('+',0)" />
                <asp:Button ID="Button12" runat="server" Text=" - " OnClientClick="subcalculator('-',0)" />
                <asp:Button ID="Button13" runat="server" Text=" * " OnClientClick="subcalculator('*',0)" />
                <asp:Button ID="Button14" runat="server" Text=" / " OnClientClick="subcalculator('/',0)" />
                <asp:Button ID="Button15" runat="server" Text=" . " OnClientClick="subcalculator('.',0)" />
                <asp:Button ID="Button16" runat="server" Text=" ( " OnClientClick="subcalculator('(',0)" />
                <asp:Button ID="Button17" runat="server" Text=" ) " OnClientClick="subcalculator(')',0)" />
                <asp:Button ID="Button18" runat="server" Text=" ← " OnClientClick="subcalculator('del',0)" />
                <asp:Button ID="Button20" runat="server" Text=" 清空 " OnClientClick="subcalculator('clear',0)" />
                <asp:DataList CssClass="table0" ID="DataList1" Width="100%" runat="server" RepeatColumns="6" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <asp:Button ID="Button19" runat="server" Text='<%#Eval("Name") %>' ToolTip='<%#Eval("VarValue").ToString()+"|"+Eval("Suffix").ToString() %>'
                            OnClientClick='subcalculator(this.title,1)' />
                    </ItemTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>"
                    SelectCommand="SELECT * FROM [FD_Variable] order by ID asc"></asp:SqlDataSource>
                   <br />
                <asp:DataList ID="DataList2" Width="100%" runat="server" RepeatColumns="6" >
                    <ItemTemplate>
                        <asp:Button ID="Button19" runat="server" Text='<%#Eval("Name") %>' ToolTip='<%# "&{"+Eval("Mark")+"}" %>'
                            OnClientClick='subcalculator(this.title,2)' />
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    </div>
    <div style="height:100px; overflow:scroll;" id="datalist">
    <asp:DataList ID="DataList3" runat="server"  CssClass="table3"
        RepeatColumns="25" onitemcommand="DataList3_ItemCommand">
        <ItemTemplate>
        <div style="width:100px;">
            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Up" CommandArgument='<%#Eval("ID") %>'><%#Eval("Mark") %><%#Eval("Name") %></asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>'>×</asp:LinkButton>
            <br /><%# WX.Main.ShowFormula(Eval("FormulaShow").ToString()) %>
            </div>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center"/>
    </asp:DataList>
    </div>
    <script type="text/javascript">
        document.getElementById("datalist").style.width = document.getElementById("interface_quick").clientWidth + "px";
    </script>
</asp:Content>
