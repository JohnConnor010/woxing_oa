<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Plan_EditPlan.aspx.cs" ClientIDMode="Static" Inherits="wwwroot.Manage.Plan.Plan_EditPlan" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/JS/iframe.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的任务 >> 查看任务
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="plan_my" CurIndex="3" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <table class="table3">
        <tr>
            <td colspan="20" style="font-weight: bold; text-align: center; background-color: #D8DCF1;
                height: 30px;">
                    <asp:Literal runat="server" ID="liTitle"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="80">
                <b>类型：</b>
            </td>
            <td style="padding-left: 10px; ">
                <asp:RadioButtonList runat="server" ID="rbPlanType" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    RepeatColumns="10" AutoPostBack="true" OnSelectedIndexChanged="rbPlanType_SelectedIndexChanged"
                    Width="400">
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td width="80">
                <b>标题：</b>
            </td>
            <td>
                <asp:TextBox ID="plantitle" runat="server" Width="389px"></asp:TextBox>
                <asp:DropDownList ID="planrtype" runat="server" Enabled="false" Visible="false">
                <asp:ListItem Value="2" Text="部门"></asp:ListItem>
                <asp:ListItem Value="3" Text="公司"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="planperiod" Width="80" runat="server" Visible="false"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="80">
                <b>预期数量：</b>
            </td>
            <td>
                <asp:TextBox ID="planTotle" Text="1" runat="server" Width="50" MaxLength="4"></asp:TextBox>&nbsp;&nbsp;<b style="">完成数量：</b><asp:TextBox ID="planCurrent" runat="server" Text="0" Width="50"  MaxLength="4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="80">
                <b>描述：</b>
            </td>
            <td>
                <asp:TextBox ID="planContent" runat="server" TextMode="MultiLine" Rows="4" Width="600"></asp:TextBox>
                <asp:Button
                    ID="Button1" runat="server" Text="提交" onclick="Button1_Click" />
            </td>
        </tr>
    </table>
    <table id="table2" class="table3" runat="server" visible="false">
        <tr>
            <td style="font-weight: bold; background-color: #ccc; height: 30px; text-align: left;">
                <div style="width:300px; float:left;">任务列表</div>
                <div style="width:100px; float:left;"><img alt="" src="/img/arrow_red.png" style="width:40px;height:30px;" /></div>
                <div style="clear:both;"></div>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="line-height: 180%;" valign="top">
            <iframe src="Plan_Task.aspx?PlanId=<%=planid %>&methed=1&estate=1" onload="Javascript:SetWinHeight(this,'1')" id="iframe" width="98%" height="300" frameborder="no" border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes"></iframe> 
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <script type="text/javascript">        
        <%=mes %>;
        function settitle() {
        var myDate = new Date();
        var year = myDate.getFullYear();
        var month = parseInt(myDate.getMonth().toString()) + 1;
        var date = myDate.getDate();
        switch (document.getElementById("plantype").value) {
            case "6": document.getElementById("plantitle").value = "<%=GetTitleTemp() %>" + year + "年" + month + "月份计划（修改）"; break;
            case "5": document.getElementById("plantitle").value = "<%=GetTitleTemp() %>" + year + "年" + month + "月第<%=getweetno() %>周计划（修改）"; break;
            case "4": document.getElementById("plantitle").value = "<%=GetTitleTemp() %>" + year + "年" + month + "月" + date + "日计划（修改）"; break;
            case "3": document.getElementById("plantitle").value = "<%=GetTitleTemp() %>" + year + "年" + month + "月份计划（修改）"; break;
            case "2": document.getElementById("plantitle").value = "<%=GetTitleTemp() %>" + year + "年" + month + "月第<%=getweetno() %>周计划（修改）"; break;
            case "1": document.getElementById("plantitle").value = "<%=GetTitleTemp() %>" + year + "年" + month + "月" + date + "日计划（修改）"; break;
            default: break;
            }
       }
    </script>
</asp:Content>
