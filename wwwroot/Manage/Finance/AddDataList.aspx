<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="AddDataList.aspx.cs" Inherits="wwwroot.Manage.Finance.AddDataList" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/default/spinner.css" rel="Stylesheet" type="text/css" />
      <script type="text/javascript" src="/App_EasyUI/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>

    <script type="text/javascript" src="/App_Scripts/zDialog.js"></script>
     <script type="text/javascript">
         function checksubmit() {
             if ($('#ddlDepartment').val() == "101") {
                 alert("请选择部门！");
                 return false;
             }
             if ($('#DropDownList3').val() == "") {
                 alert("请选择销售人！");
                 return false;
             }
             if ($('#Fee').val() == "") {
                 alert("请输入销售额！");
                 return false;
             }
             return true;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    财务管理 >> 新增数据 >>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Finance_DataList" CurIndex="4" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <table style="width: 420px;" align="center" class="table3">
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
                销售人：
            </td>
            <td>
                <asp:DropDownList ID="DropDownList3" runat="server" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
                业务类型：
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
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
                <asp:TextBox ID="Mamount" runat="server"></asp:TextBox>
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
                日期：
            </td>
            <td>                
                <asp:TextBox ID="TextBox5" runat="server" Columns="30" class="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 80px; text-align: right; font-weight: bold;">
            &nbsp;
            </td>
            <td>                
                <asp:Button
                    ID="Button1" runat="server" Text="提交" onclick="Button1_Click" OnClientClick="return checksubmit();" />
            </td>
        </tr>
    </table>
    <div style="width: 1000px; overflow-x: scroll;" id="datalist">
        <table  style="text-align: center;" id="table1" class="table1">
            <tr style="font-weight: bold; background-color: #dddeee;">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <td>
                    <div style="width:80px;">日期</div>
                </td>
                <td>
                    <div style="width:50px;">操作</div>
                </td>
            </tr>
            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="Repeater2_ItemCommand">
                <ItemTemplate>
                    <tr style="">
                        <%# Eval("Valuestr3") %>
                        <td>
                            <%# Convert.ToDateTime(Eval("payingTime").ToString()).ToString("yyyy-MM-dd") %>
                        </td>
                        <td>
                            <asp:Button ID="DeleteButton" runat="server" Visible='<% #this.Master.A_Del %>' CommandArgument='<%# Eval("ID") %>'
                                CommandName="Delete" Text="×" OnClientClick="return confirm('您确定要删除吗？')" />
                            <a href='?id=<%# Eval("ID") %>'>编辑</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <script type="text/javascript">
        document.getElementById("datalist").style.width = document.getElementById("interface_quick").clientWidth - 10 + "px";

    </script>
</asp:Content>
