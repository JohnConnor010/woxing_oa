<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUser_ManagerPlan.ascx.cs"
    Inherits="wwwroot.Manage.Plan.WUser_ManagerPlan" %>
<iframe src='Plan_TempEditPlan.aspx?<%=rTypeName %>=<%=UserID %>&starttime=<%=Stime %>&type=<%=typestr %>&rtype=<%=rtype+(Request["dept"] != null && Request["dept"] != ""?"":"&estate=1") %>'
    onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="98%" frameborder="no"
    border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
</iframe>
<asp:DataList ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <iframe src='Plan_TempEditPlan.aspx?<%#Eval("rTypeName") %>=<%#Eval("UserID") %>&starttime=<%#Eval("Stime") %>&type=<%#Eval("Type") %>&rtype=<%#Eval("rtype") %><%#Eval("UserID").ToString()==WX.Main.CurUser.UserID?"&estate=1":"" %>&rUserID=<%#Eval("UserID") %>'
            onload="Javascript:SetWinHeight(this,'0')" id="iframe1" width="98%" frameborder="no"
            border="0" marginwidth="0" marginheight="0" scrolling="no" allowtransparency="yes">
        </iframe>
    </ItemTemplate>
    <ItemStyle VerticalAlign="Top" />
</asp:DataList>