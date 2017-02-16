<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_Official.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Official"
    ClientIDMode="Static" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" rel="Stylesheet" type="text/css" />
    <link href="/App_EasyUI/themes/icon.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="/App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="/App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
        <script type="text/javascript">

            function ChkFile() {
                if (document.getElementById("FileUpload1").value.length == 0)
                { alert("请选择上传文件"); return false; }
                return true;
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >> 员工转正
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="4" Param1="{Q:UserId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table class="table3" style="text-align: center; line-height: 200%;">
        <caption style="font-weight:bold; text-align:left;">您当前的操作：员工转正</caption>
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
                <td align="left">
                <asp:DropDownList ID="ui_demp" runat="server" Width="150" AutoPostBack="True" 
                                        onselectedindexchanged="ui_demp_SelectedIndexChanged">
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
                <td align="left">
                <asp:DropDownList ID="ui_duty" runat="server" Width="150">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList1" runat="server" Style="width: 250px">
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
                    <asp:Literal ID="li_time" runat="server"></asp:Literal> &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    工作设想
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        员工转正申请（内容包括对工作的回顾、总结；自己在工作中的优点及不足，如何改进存在的不足；对今后工作的设想：</div>
                    <asp:TextBox ID="ui_imagine" runat="server" TextMode="MultiLine" Rows="10" Width="700"></asp:TextBox><br />
                    <div style="float: left; width: 805px;">
                        注： 1、试用员工调用期结束前5日将转正申请表报行政部（员工试用期间请假时间过长需延长试用期）。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、员工试用期间有严重违反公司管理规章制度或给公司造成极坏影响的不予转正。
                    </div>
                    <div style="float: left; padding-right: 20px;">
                        申请人：<asp:Literal ID="li_sqrname" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr runat="server" id="tr_dept" visible="false">
                <td>
                    部门意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div style="font-weight:bold;">
                        <asp:Literal ID="li_dept" runat="server"></asp:Literal></div>
                    <div style="float: left">
                        同意其：</div>
                    <div style="float: left">
                        <asp:CheckBoxList ID="Check_dept" runat="server" RepeatColumns="3" BorderStyle="None">
                            <asp:ListItem Value="1" Text="提前转正"></asp:ListItem>
                            <asp:ListItem Value="2" Selected="True" Text="按期转正"></asp:ListItem>
                            <asp:ListItem Value="3" Text="延期转正&nbsp;&nbsp;&nbsp;&nbsp;（在您认为的项上打“√”）"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_dept" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_HR" visible="false">
                <td>
                    人资意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div style="font-weight:bold;">
                        <asp:Literal ID="li_hr" runat="server"></asp:Literal></div>
                    <div style="float: left">
                        同意其：</div>
                    <div style="float: left">
                        <asp:CheckBoxList ID="Check_hr" runat="server" RepeatColumns="3" BorderStyle="None">
                            <asp:ListItem Value="1" Text="提前转正"></asp:ListItem>
                            <asp:ListItem Value="2" Selected="True" Text="按期转正"></asp:ListItem>
                            <asp:ListItem Value="3" Text="延期转正&nbsp;&nbsp;&nbsp;&nbsp;（在您认为的项上打“√”）"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_hr" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_CA" visible="false">
                <td>
                    综管意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div style="font-weight:bold;">
                        <asp:Literal ID="li_ca" runat="server"></asp:Literal></div>
                    <div style="float: left">
                        同意其：</div>
                    <div style="float: left">
                        <asp:CheckBoxList ID="Check_ca" runat="server" RepeatColumns="3" BorderStyle="None">
                            <asp:ListItem Value="1" Text="提前转正"></asp:ListItem>
                            <asp:ListItem Value="2" Selected="True" Text="按期转正"></asp:ListItem>
                            <asp:ListItem Value="3" Text="延期转正&nbsp;&nbsp;&nbsp;&nbsp;（在您认为的项上打“√”）"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_ca" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_boss" visible="false">
                <td>
                    中心意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div style="font-weight:bold;">
                        <asp:Literal ID="li_boss" runat="server"></asp:Literal></div>
                    <div style="float: left">
                        同意其：</div>
                    <div style="float: left">
                        <asp:CheckBoxList ID="Check_boss" runat="server" RepeatColumns="3" BorderStyle="None">
                            <asp:ListItem Value="1" Text="提前转正"></asp:ListItem>
                            <asp:ListItem Value="2" Selected="True" Text="按期转正"></asp:ListItem>
                            <asp:ListItem Value="3" Text="延期转正&nbsp;&nbsp;&nbsp;&nbsp;（在您认为的项上打“√”）"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="Text_boss" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_sub">
                <td>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>意见
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                    <div>
                        对申请人及自身指导的评述：</div>
                    <div>
                        <asp:TextBox ID="ui_dempop" runat="server" TextMode="MultiLine" Rows="4" Width="700"></asp:TextBox></div>
                    <div style="float: left">
                        同意其：</div>
                    <div style="float: left">
                        <asp:CheckBoxList ID="ui_demptype" runat="server" RepeatColumns="3" BorderStyle="None">
                            <asp:ListItem Value="1" Text="提前转正"></asp:ListItem>
                            <asp:ListItem Value="2" Selected="True" Text="按期转正"></asp:ListItem>
                            <asp:ListItem Value="3" Text="延期转正&nbsp;&nbsp;&nbsp;&nbsp;（在您认为的项上打“√”，单选项）"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div style="float: right; padding-right: 20px;">
                        负责人：<asp:TextBox ID="li_sqname" runat="server" Width="60" Enabled="false"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr runat="server" id="tr_sub2">
                <td>&nbsp;
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text=" 通 过 " OnClick="Button1_Click" Enabled="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" Text=" 不通过 " OnClick="Button2_Click" Enabled="false" />
                </td>
            </tr>
            <tr runat="server" id="tr_ht" visible="false">
                <td>
                    合同保险
                </td>
                <td colspan="9" style="text-align: left; padding: 5px;">
                      <div id="div_ht" runat="server" Visible="false">劳动保险：<asp:FileUpload ID="FileUpload1" runat="server" /><asp:ImageButton ID="ImageButton1"
                                        OnClientClick="return ChkFile();" runat="server" ImageUrl="/Manage/icon/icon2_004.png"
                                        OnClick="ImageButton1_Click" /></div>
                                    <asp:GridView ID="GridView1" ShowHeader="false" runat="server" AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" Width="500">
                                        <Columns><asp:TemplateField>
                                                <ItemTemplate>
                                                <a href='<%# Eval("Annex")%>' target="_blank"><%# Eval("Name")%></a>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" OnClientClick="return window.confirm('确定删除？')" CommandArgument='<%# Eval("Id")%>' runat="server" ImageUrl="/Manage/icon/ico_false.gif" />
                                                </ItemTemplate><ItemStyle Width="40" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    保险情况：<asp:CheckBox ID="CheckBox1" runat="server" /><br />
                                    <asp:Button ID="Button3" runat="server" Text=" 提 交 " Visible="false" OnClick="Button3_Click" />

                </td>
            </tr>
        </table>
        
    </div>
</asp:Content>
