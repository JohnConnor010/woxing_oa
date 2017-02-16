<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="AddTemp.aspx.cs" Inherits="wwwroot.Manage.CTR.AddTemp" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    销售管理 >> 模板管理 >> 添加模板
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Temp" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table1">
        <tr>
            <td width="80"><b>选择分类：</b>
            </td>
            <td><asp:DropDownList ID="txtType" runat="server" onselectedindexchanged="txtType_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;<b>模板标题：</b><asp:TextBox ID="txtTitle" runat="server" Width="400"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="float:left; width:85%;">
                    <FCKeditorV2:FCKeditor ID="fckContent" runat="server" Height="500" Width="100%" Value="">
                    </FCKeditorV2:FCKeditor>
                    </div>
                    <div style="float:left;">
                    <br /><br /><br /><br /><b>选择标签：</b>
                    <asp:DataList ID="DataList1" BackColor="White" runat="server" Style="width: 100%">
                                <ItemTemplate>
                                               <div>
                                                   <input id="Button1" onclick="exec_cmd('lag_add')" type="button" value='<%# Eval("Title")%>' title='<%# Eval("Name")%>' /></div>
                                </ItemTemplate>
                                <ItemStyle CssClass="contentcss" />
                            </asp:DataList></div>
                            
<script type="text/javascript">
//--- 单行输入框（新） ---
        function exec_cmd(cmd) {
            var FCK = FCKeditorAPI.GetInstance('fckContent'); //获得表单设计器的顶层对象FCK，该方法定义位置fckeditorapi.js第47行 by dq 090521
            FCK.Focus();
            FCK.Commands.GetCommand(cmd).Execute(); //仿照fcktoolbarbutton.js第71行的写法 by dq 090521
           // return false;
        }
    function Add_Label(value,text) {
        var content = FCKeditorAPI.GetInstance('fckContent').EditorDocument.body.innerHTML;
        var start = document.getElementById("fckContent").selectionStart;
        var end = document.getElementById("ContentPlaceHolder_fckContent").selectionEnd;
        FCKeditorAPI.GetInstance('ContentPlaceHolder_fckContent').EditorDocument.body.innerHTML = content + "<lag id='" + value + "' value='" + text + "'>" + text + "</lag>";
    }
</script>
            </td>
        </tr>
        <tr>
            <td> &nbsp;
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="保存" onclick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
