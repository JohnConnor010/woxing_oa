<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_Prcs_VmlList.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_VmlList" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-modi" CurIndex="4"  Param1="{Q:FlowID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="table">
        <div style="width: 85%; float: left;">
            <iframe src="/App_Ctrl/FlowGraphic.aspx?FlowID=<%=Request.QueryString["FlowId"] %>" style="width:100%;height:500px;" id="set_main" name="set_main"></iframe>
        </div>
        <div id="buttons" style="float: left; margin-top: 2px; width: 13px; padding-left:3px;">
            <input type="button" id="button0" value="新建步骤" class="SmallButton" onclick="location.href='Flow_Prcs_New.aspx?Id=<%=Request.QueryString["Id"] %>&index=4'" />&nbsp;
            <br />
            <input type="button" id="button1" value="保存布局" class="SmallButton" onclick="window.set_main.SavePosition()" />&nbsp;
            <br />
            <input type="button" value="刷新布局" class="SmallButton" onclick="window.set_main.location.reload();" />&nbsp;
            <br />
            <input type="button" value="打印布局" class="SmallButton" onclick="window.set_main.document.execCommand('Print');"
                title="直接打印流程页面" />&nbsp;
            <br />
            <!--input type="button" value="关闭布局" class="SmallButton" onclick="parent.close();" /-->
        </div>
        <div style="clear:both;"></div>
    </div>
</asp:Content>
