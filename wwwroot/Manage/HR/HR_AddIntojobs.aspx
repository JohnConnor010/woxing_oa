<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="HR_AddIntojobs.aspx.cs" EnableEventValidation="false"
    ClientIDMode="Static" Inherits="wwwroot.Manage.HR.HR_AddIntojobs" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript">

        function ChkFile() {
            if (document.getElementById("FileUpload1").value.length == 0)
            { alert("请选择上传文件"); return false; }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    人力资源 >> 人事档案 >> 员工入职
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-show" CurIndex="3" Param1="{Q:UserId}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div id="PanelManage">
        <table width="100%">
            <tr>
                <td>
                    <table class="table3" style="text-align: center;">
                        <tr>
                            <td rowspan="2" width="80">
                                <b>姓名</b>
                            </td>
                            <td rowspan="2">
                                <asp:Literal ID="li_name" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="60" style="height: 30px;">
                                <b>性别</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_sex" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="60">
                                <b>年龄</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_age" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="80">
                                <b>学历</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_edu" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="80">
                                <b>外语语种</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_ForeignL" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="60">
                                <b>等级</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_Rating" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td rowspan="4" style="width: 135px;">
                                <div style="font-weight: bold;">
                                    两寸免冠照片(150*130)</div>
                                <div style="text-align: center; padding: 3px 3px 3px 3px;">
                                    <div style="width: 150; height: 130px; border: 0px dashed #787878; margin: auto;
                                        padding: 1px 1px 1px 1px;">
                                        <asp:Literal ID="li_face" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 35px;">
                                <b>民族</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_Ethnic" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td>
                                <b>婚否</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_Marital" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td>
                                <b>健康状况</b>
                            </td>
                            <td>
                                <asp:Literal ID="li_Health" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td>
                                <b>籍贯</b>
                            </td>
                            <td colspan="3">
                                <asp:Literal ID="li_jg" runat="server"></asp:Literal>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 35px;">
                                <b>户籍所在地</b>
                            </td>
                            <td colspan="4">
                                <asp:Literal ID="li_hjd" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="80">
                                <b>身份证</b>
                            </td>
                            <td colspan="3">
                                <asp:Literal ID="li_IDCard" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="80">
                                <b>联系方式</b>
                            </td>
                            <td colspan="2">
                                <asp:Literal ID="li_Mobile" runat="server"></asp:Literal>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 35px;">
                                <b>拟入职部门</b>
                            </td>
                            <td colspan="4">
                                <asp:Literal ID="li_dept" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="80">
                                <b>拟入职职务</b>
                            </td>
                            <td colspan="3">
                                <asp:Literal ID="li_duty" runat="server"></asp:Literal>&nbsp;
                            </td>
                            <td width="80">
                                <b>拟薪资待遇</b>
                            </td>
                            <td colspan="2">
                                <asp:Literal ID="li_salary" runat="server"></asp:Literal>&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 66%; float: left; padding-left: 5px;">
                        <asp:Literal ID="li_left" runat="server"></asp:Literal>
                    </div>
                    <div style="width: 33%; float: left;">
                        <table class="table3" style="text-align: center; line-height: 200%;">
                            <tr>
                                <td colspan="2">
                                    <b>入职情况</b>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    应聘部门
                                </td>
                                <td align="left">
                                    &nbsp;<asp:DropDownList ID="ddlDepartment" runat="server" Style="width: 250px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    应聘职位
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ui_jobname" runat="server" Style="width: 250px">
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试用工资
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Style="width: 250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    转正工资
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="DropDownList2" runat="server" Style="width: 250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    入职时间
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="ui_addtime" runat="server" Width="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试用协议
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" /><asp:ImageButton ID="ImageButton1"
                                        OnClientClick="return ChkFile();" runat="server" ImageUrl="/Manage/icon/icon2_004.png"
                                        OnClick="ImageButton1_Click" />
                                    <asp:GridView ID="GridView1" ShowHeader="false" runat="server" AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" Width="100%">
                                        <Columns><asp:TemplateField>
                                                <ItemTemplate>
                                                <a href='<%# Eval("Annex")%>' target="_blank"><%# Eval("Name")%></a>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" OnClientClick="return window.confirm('确定删除？')" CommandArgument='<%# Eval("Id")%>' runat="server" ImageUrl="/Manage/icon/ico_false.gif" />
                                                </ItemTemplate><ItemStyle Width="40" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试用意见
                                </td>
                                <td>
                                    <asp:TextBox ID="ui_content" runat="server" TextMode="MultiLine" Rows="4" Width="280"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;经办人
                                </td>
                                <td align="left">
                                    <asp:Literal ID="li_SignUserID" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button1" runat="server" Text=" 提 交 " OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="li_linkman" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
