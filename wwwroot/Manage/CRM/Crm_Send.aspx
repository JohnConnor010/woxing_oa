<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_Send.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Send" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
<!--
*{margin:0 auto;padding:0;}
.dialog{width:593px;height:517px;overflow:hidden; text-align:left;}
.dialog_tt{width:593px;height:36px;background:url(/images/ttbg.jpg) no-repeat;text-align:center;}
.dialog_tt h3{background:url(/images/sendiocn.jpg) no-repeat 225px 12px;font:16px/36px Microsoft YaHei;color:#fff;}
.dialog_main{width:591px;height:480px;border-left:1px solid #95b8e7;border-right:1px solid #95b8e7;border-bottom:1px solid #95b8e7;background:#fff;}
.dialog_main form{text-align:center;padding-top:15px;}
.com_select{height:25px;}
.dialog_left{width:192px;float:left;margin:15px 0 0 6px;display:inline;}
.leftin{width:190px;height:330px;border-left:1px solid #b7b7b7;border-right:1px solid #b7b7b7;border-bottom:1px solid #b7b7b7;}
.dialog_left img{display:block;}
.leftin h5{background:url(/images/icon2.jpg) no-repeat 20px 20px;font:14px/26px Microsoft YaHei;padding:10px 0 0 35px;cursor:pointer;}
.leftin ul{margin:5px 0 0 5px;}
.leftin ul li{font:14px/22px Microsoft YaHei;color:#666;list-style:none}
.dialog_right{width:381px;float:left;margin:15px 0 0 6px;display:inline;}
.textarea000{width:379px;height:349px;border:1px solid #b7b7b7}
.sendbtn{width:61px;height:28px;background:url(/images/sendbtn.jpg) no-repeat;border:0px;margin:10px 0 0 0;cursor:pointer;}
.resetbtn{width:61px;height:28px;background:url(/images/resetbtn.jpg) no-repeat;border:0px;margin:10px 0 0 10px;cursor:pointer;}
-->
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 我的客户 >> 短信群发
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="MyCustomer" CurIndex="9" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="width: 100%; text-align: center;">
        <div class="dialog">
            <div class="dialog_tt">
                <h3>
                    发送短消息</h3>
            </div>
            <div class="dialog_main">
                <form action="" method="post">
                <br />
                &nbsp;&nbsp;<asp:DropDownList ID="ddlCustomerCategory" runat="server" Width="80"
                    CssClass="com_select" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCategory_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlCompanyNature" runat="server" Width="80" CssClass="com_select"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCategory_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlSource" runat="server" Width="80" CssClass="com_select"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCategory_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlIndustry" runat="server" Width="100" CssClass="com_select"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCategory_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlBusinessLevel" runat="server" Width="80" CssClass="com_select"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerCategory_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="ddlStage" runat="server" Width="80" CssClass="com_select" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlCustomerCategory_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;
                </form>
                <div class="dialog_left">
                    <img src="/images/lttbg.jpg" />
                    <div class="leftin" id="contact_person">
                        <asp:Repeater ID="CustomerRepeater" runat="server">
                            <ItemTemplate>
                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("MobilePhone") %>' />
                                <ul id="contact_con1">
                                    <li>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" /><%#Eval("CustomerName") %>-<%#Eval("ContactName")%></li>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="dialog_right">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="textarea000" TextMode="MultiLine"></asp:TextBox>
            
            <asp:Button ID="Button1" runat="server" CssClass="sendbtn" Text="" 
                        onclick="Button1_Click" /><input type="reset" class="resetbtn"
                        value="&nbsp;" />单条短信最长为70个字，超出70个字的短信称为长短信。长短信自动按每条67个字将短信内容拆分为多条短信发送。
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var flag = 1;
        function showhiden(title, content) {
            var title = document.getElementById(title);
            var content = document.getElementById(content);
            title.onclick = function () {
                if (flag == 1) {
                    title.style.background = "url(/images/icon1.jpg) no-repeat 19px 20px";
                    content.style.display = "block";
                    flag = 0;
                } else if (flag == 0) {
                    title.style.background = "url(/images/icon2.jpg) no-repeat 20px 19px";
                    content.style.display = "none";
                    flag = 1;
                }
            }
        }
        showhiden("contact_tt1", "contact_con1");
        showhiden("contact_tt2", "contact_con2");
    </script>
</asp:Content>
