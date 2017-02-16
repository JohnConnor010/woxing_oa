<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Flow_Prcs_PassSet.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Prcs_PassSet" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <!--link rel="stylesheet" type="text/css" href="../css/style_setcondition.css" /-->
    <script type="text/javascript">
        function signChange() {
            var v = document.getElementById("FEEDBACK").value;
            if (v != 0)
                document.getElementById("divSIGNLOOK").style.display = "";
            else
                document.getElementById("divSIGNLOOK").style.display = "none";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
流程管理 >> 流程定义 >> 步骤设计
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="flow-prcs-modi" CurIndex="10" Param1="{Q:FlowId}" Param2="{Q:Id}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 
<table id="turn" class="table">
    <tr>
      <th style="width:150px;">会签选项：</th>
      <td>
        <b>&nbsp;是否允许会签：</b><br/>
          <asp:DropDownList ID="FEEDBACK" onchange="javascript:signChange();" CssClass="SmallSelect" runat="server" ClientIDMode="Static">
          </asp:DropDownList>&nbsp;<a href="#" title="说明：如设置强制会签，则不会签不能进行办理完毕操作">说明</a>
        <div id="divSIGNLOOK">
        <b>&nbsp;会签意见可见性：</b><br/><asp:DropDownList ID="SIGNLOOK" CssClass="SmallSelect" runat="server">
    
        </asp:DropDownList>
        </div>
      </td>
    </tr>
    <tr>
    	<th>强制转交：</th>
    	<td>
    		<b>&nbsp;经办人未办理完毕时是否允许主办人强制转交：</b><br/>
    	 <asp:DropDownList ID="TURN_PRIV" CssClass="SmallSelect" runat="server">
       	<asp:ListItem Value="1">允许</asp:ListItem>
       	<asp:ListItem Value="0" >不允许</asp:ListItem>
      	</asp:DropDownList>
      	</td>
    </tr>
    <tr>
      <th>回退选项：</th>
      <td>
      	<b>&nbsp;是否允许回退：</b><br/>
      	<asp:DropDownList ID="ALLOW_BACK" CssClass="SmallSelect" runat="server">
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <th>并发相关选项：</th>
      <td>
      	<b>&nbsp;是否允许并发：</b><br/>
        <asp:DropDownList ID="SYNC_DEAL" CssClass="SmallSelect" runat="server">
        </asp:DropDownList>&nbsp;<br/>
        <b>&nbsp;并发合并选项：</b><br/>
         <asp:DropDownList ID="GATHER_NODE" CssClass="SmallSelect" runat="server">
        </asp:DropDownList>
        <a href="#" title="非强制合并：此步骤主办人在并发分支中任意分支转至后即可进行转交<br>强制合并：所有可能直接转至此步骤的并发步骤都已转至后方可转交下一步">说明</a>
      </td>
    </tr>
    
        <tr align="center" class="TableControl">
            <td colspan="2" nowrap>
                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="button" OnClick="Button1_Click" />
                <%--<input type="button" class="BigButton" value="返回" onclick="location='index.php?FLOW_ID=8'">--%>
            </td>
        </tr>
</table>
     <script type="text/javascript">
         signChange();
    </script>
</asp:Content>
