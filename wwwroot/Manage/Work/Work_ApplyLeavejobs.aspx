<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Work_ApplyLeavejobs.aspx.cs" Inherits="wwwroot.Manage.Work.Work_ApplyLeavejobs" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">  我的申请 >> 转正
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="run_apply" CurIndex="4" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <table class="table3" style="text-align: center; line-height: 200%;">
            <tr>
                <td rowspan="2" width="80">
                    姓名
                </td>
                <td rowspan="2">
                    <asp:Literal ID="li_name" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    所属部门
                </td>
                <td>
                <asp:DropDownList ID="ui_demp" runat="server" Width="150" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td>
                    性别
                </td>
                <td>
                    <asp:Literal ID="li_sex" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    学历
                </td>
                <td>
                    <asp:Literal ID="li_edu" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    入职时间
                </td>
                <td>
                    <asp:Literal ID="li_intotime" runat="server"></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    职务
                </td>
                <td>
                <asp:DropDownList ID="ui_duty" runat="server" Width="100" Enabled="false">
                    </asp:DropDownList>
                </td>
                <td>
                    出生日期
                </td>
                <td>
                    <asp:Literal ID="li_age" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    联系方式
                </td>
                <td>
                    <asp:Literal ID="li_Mobile" runat="server"></asp:Literal>&nbsp;
                </td>
                <td>
                    申请时间
                </td>
                <td>
                    <asp:Literal ID="li_addtime" runat="server"></asp:Literal>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    离职原因
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <asp:HiddenField ID="HiddenID" runat="server" />
                 将于即日起<asp:TextBox ID="ui_days" runat="server" Width="50"></asp:TextBox>天后离职，本人最后到职日期为<asp:TextBox ID="ui_lasttime" runat="server" Width="90" CssClass="easyui-datebox"></asp:TextBox>。<br />
                    <asp:RadioButtonList ID="radio_reason" runat="server">
                    <asp:ListItem Value="环境（气氛）不佳、志趣不合" Text="环境（气氛）不佳、志趣不合"></asp:ListItem>
                    <asp:ListItem Value="未毕业实习生，需返校" Text="未毕业实习生，需返校"></asp:ListItem>
                    <asp:ListItem Value="健康原因" Text="健康原因"></asp:ListItem>
                    <asp:ListItem Value="结婚、生育" Text="结婚、生育"></asp:ListItem>
                    <asp:ListItem Value="出国、服役" Text="出国、服役"></asp:ListItem>  
                    <asp:ListItem Value="" Text="其它"></asp:ListItem>                    
                    </asp:RadioButtonList>
                    <div style="float:left;">描述：</div><div style="float:left;"><asp:TextBox ID="ui_reason" runat="server" Width="500" TextMode="MultiLine" Rows="5"></asp:TextBox><br /></div>
                   
                    <div style="float: right; padding-right: 20px;">
                        申请人：<asp:Literal ID="li_sqrname" runat="server"></asp:Literal></div>
                        <div style="color:Red;" runat="server" id="divstr"></div>
                </td>
            </tr> 

            
            <tr runat="server" id="tr_dept" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div>
                        <b>部门意见：</b><asp:Literal ID="li_dept" runat="server"></asp:Literal></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_dept" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_HR" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div>
                        <b>人资意见：</b><asp:Literal ID="li_hr" runat="server"></asp:Literal></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_hr" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_boss" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div>
                        <b> 中心意见：</b><asp:Literal ID="li_boss" runat="server"></asp:Literal></div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_boss" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_receshow" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                        <b> 交接内容：</b><asp:Literal ID="li_rece" runat="server"></asp:Literal><br />
                    <asp:Literal ID="li_annex" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2"
                        runat="server" onclick="LinkButton2_Click">修改</asp:LinkButton>
                </td>
            </tr>

            <tr runat="server" id="tr_rece" visible="false">
                <td colspan="10" style="text-align: left; padding: 5px;">
                    <div style="float:left;">交接内容：</div><div style="float:left;"><asp:TextBox ID="Text_RECE" runat="server" Width="500" TextMode="MultiLine" Rows="5"></asp:TextBox><br />
                    注：请将交接文件打成一个压缩包上传。<br />
                        <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <asp:Button ID="Button2" runat="server"
                            Text="提交" onclick="Button2_Click" />
                    </div>
                </td>
            </tr>
             <tr>
                <td colspan="10" style="text-align: left; padding: 5px;">
                 <div  id="div_rece" runat="server" visible="false">
                 <b> 请确认部门工作交接内容或回答下列问题：</b><br />
                     <asp:HiddenField ID="hidden_receid" runat="server" />
                     <asp:Literal ID="li_Question" runat="server"></asp:Literal><br />
                     <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Rows="2" Width="700"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button3" runat="server" Text=" 提 交 " OnClick="Button3_Click" /></div>

                    <asp:GridView ID="Gv_Receive" runat="server" CssClass="table" AllowPaging="True"
            AutoGenerateColumns="False" PageSize="1000" onrowcommand="Gv_Receive_RowCommand" ShowHeader="false">
            <Columns>
            <asp:TemplateField HeaderText="部门">
                    <ItemTemplate>
                       <%# WX.CommonUtils.GetDeptNameListByDeptIdList(Eval("DeptID").ToString())%>
                    </ItemTemplate>
                    <ItemStyle Width="120" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="交接内容">
                    <ItemTemplate>
                       <%# Eval("Question") +"&nbsp;&nbsp;&nbsp;&nbsp;"+ Eval("QuestionTime") + WX.CommonUtils.GetRealNameListByUserIdList(Eval("AddUserID").ToString())%><br />
                       <%# Eval("Answer") + "&nbsp;&nbsp;&nbsp;&nbsp;" + Eval("AnswerTime")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                       <%# WX.HR.Receive.Statestr[Convert.ToInt32(Eval("State").ToString())]%>
                       <div style="color:#888;"><%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("ConfirmUserID").ToString())%></div>
                    </ItemTemplate>
                    <ItemStyle Width="60" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CommandName="linkup" CommandArgument='<%# Eval("ID")+"|"+Eval("AnswerTime")+"|"+Eval("State")%>' Visible='<%#Convert.ToInt32(Eval("State").ToString())<3?true:false%>' runat="server">确认/回复</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="60" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </td>
            </tr>
            <tr id="tr_sub" runat="server">
                <td>&nbsp;&nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 申 请 离 职 " OnClientClick="return confirm('确定要提交离职申请吗？');" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
</asp:Content>
