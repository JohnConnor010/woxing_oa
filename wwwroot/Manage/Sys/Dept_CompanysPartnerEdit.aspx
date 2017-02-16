<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" ClientIDMode="Static" AutoEventWireup="true" CodeBehind="Dept_CompanysPartnerEdit.aspx.cs" Inherits="wwwroot.Manage.Sys.Dept_CompanysPartnerEdit" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link href="/App_EasyUI/themes/default/easyui.css" type="text/css" rel="Stylesheet" />
    <link href="/App_EasyUI/themes/icon.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript" src="../../App_Scripts/popup.js"></script>
    <script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
    <script type="text/javascript" src="/App_EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../App_EasyUI/plugins/jquery.extend.validatebox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#form1').submit(function () {
                var b = $('#form1').form("validate");
                return b;
            });
        });

        function setspan(span) {
            document.getElementById(span).innerHTML = "";
        }
        function ViewAllProducts() {
            var diag = new Dialog();
            diag.Width = 1000;
            diag.Height = 407;
            diag.Title = "选择已有人员";
            diag.URL = '../Sys/GetPartnerList.aspx?CompanyId=<%=Request["companyID"] %>';
            diag.OKEvent = function () {
                var text = diag.innerFrame.contentWindow.document.getElementById('hidden_json').value;
                if (text == "") {
                    alert("请选择要添加的人员！");
                    return false;
                }
                else {
                    var json = eval(text);
                    $.each(json, function (index, item) {
                        $('#ui_ID').val(item.Id);
                        if (item.Legal == "1")
                            $('#ui_Legal').attr("checked", true);
                        if (item.Shareholder == "1")
                            $('#ui_Shareholder').attr("checked", true);
                        if (item.Directors == "1")
                            $('#ui_Directors').attr("checked", true);
                        $('#ui_Share').val(item.Share);
                        $('#ui_Assets').val(item.Assets);
                        $('#ui_starttime').val(item.Starttime);
                        $('#ui_RealName').val(item.RealName);
                        $('#ui_sex').val(item.Sex);
                        $('#ui_PoliticalL').val(item.PoliticalL);
                        $('#ui_edu').val(item.Edu);
                        $('#ui_content').val(item.Content);
                        $('#ui_DepartentID').val(item.DepartentID);
                        $('#li_Manage').val(item.ManageName);
                        $('#ui_Manage').val(item.Manage);
                        $('#ui_title').val(item.Title);
                        $('#ui_LNO').val(item.LNO);
                        $('#HiddenField1').val("");
                       // $('#HiddenField1').val(item.Annex);
//                        var annexarry = item.Annex.split(",");
//                        alert(annexarry.Length);
//                        for (var i = 0; i < annexarry.Length; i++) {
//                            if (annexarry[i] != "") {
//                                $('#Literal' + i).val("<a href='Dept_AnnexDetail.aspx?id=" + Request["id"] + "&aid=" + i + "&companyID=" + Request["companyID"] + "'>" + annexarry[i].Split('|')[0] + "</a>")
//                            }
//                        }
//                        // alert("getannex(" + item.Annex + ")");
//                        alert(annexstr);

                    });
                }
                diag.close();
            }
            diag.show();

        }
        function ViewAllProducts2() {
            var diag = new Dialog();
            diag.Width = 1000;
            diag.Height = 407;
            diag.Title = "选择已有人员";
            diag.URL = '../Sys/GetEmployeeList.aspx?CompanyId=<%=Request["companyID"] %>';
            diag.OKEvent = function () {
                var text = diag.innerFrame.contentWindow.document.getElementById('hidden_json').value;
                if (text == "") {
                    alert("请选择要添加的人员！");
                    return false;
                }
                else {
                    var json = eval(text);
                    $.each(json, function (index, item) {
                       
                        $('#ui_RealName').val(item.RealName);
                        $('#ui_sex').val(item.Sex);
                        $('#ui_edu').val(item.Edu);
                        $('#ui_title').val("身份证");
                        $('#ui_LNO').val(item.IDCard);
                        $('#HiddenField1').val(item.UserID);
                        $('#ui_ID').val("");
                    });
                }
                diag.close();
            }
            diag.show();

        }
    </script>
    
<style type="text/css"> 
#norSearch, #advSearch {
    background: url("../../images/search_button.png") no-repeat scroll 0 0 transparent;
    height: 33px;
    margin: 3px;
    width: 107px;
}
input.toolBtnA, input.toolBtnB, input.toolBtnC {
    background: url("../../images/m_button.png") repeat scroll 0 0 transparent;
    border: 0 none;
    color: #1866F4;
    cursor: pointer;
    font-family: 微软雅黑,宋体,sans-serif;
    font-size: 11pt;
    height: 23px;
    text-decoration: none;
    width: 114px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 公司信息 >> 资料编辑
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="comp" CurIndex="5" Param1="{Q:companyID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:HiddenField ID="ui_ID" runat="server" />
    <table class="table" width="600" align="center">
        <tr>
            <td style="font-weight: bold; width: 100px;">
                类型:<a href="#" class="help">[?]</a>
            </td>
            <td>
                  <span class="note">选择当前信息是法人还是股东</span> &nbsp;
                <asp:CheckBox ID="ui_Legal" runat="server" />法人 &nbsp; &nbsp; <asp:CheckBox ID="ui_Shareholder" runat="server" />股东 &nbsp; &nbsp;<asp:CheckBox ID="ui_Directors" runat="server" />董事会
            </td>
        </tr>
         <tr>
            <td style="font-weight: bold;">
                股份比例:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">所占公司股份比例</span> &nbsp;<asp:TextBox ID="ui_Share" runat="server"></asp:TextBox>%
            </td>
        </tr>
         <tr>
            <td style="font-weight: bold;">
                资产比例:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">所占公司资产比例</span> &nbsp;<asp:TextBox ID="ui_Assets" runat="server"></asp:TextBox>%
            </td>
        </tr>        
         <tr>
            <td style="font-weight: bold;">
                加入时间:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">加入公司的起始时间</span> &nbsp;<asp:TextBox ID="ui_starttime" runat="server" CssClass="easyui-datebox"></asp:TextBox>
            </td>
        </tr>
        </table>
        <table class="table" width="600" align="center" id="table2" runat="server">
        <tr>
            <td style="font-weight: bold;width: 100px;">
                姓名:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">真实姓名</span> &nbsp;<asp:TextBox ID="ui_RealName" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox><input id="btnSelect1" type="button" value="选择" runat="server" class="toolBtnB" onclick="ViewAllProducts();" />&nbsp;&nbsp;<input id="btnSelect2" runat="server" type="button" value="选择内部人员" class="toolBtnB" onclick="ViewAllProducts2();" />
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
                性别:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">性别 </span><asp:RadioButtonList ID="ui_sex" runat="server" RepeatColumns="2" Width="150">
                <asp:ListItem Value="1" Text="男" Selected></asp:ListItem>
                <asp:ListItem Value="0" Text="女"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>        
        <tr>
            <td style="font-weight: bold;">
                政治面貌:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">政治面貌</span> &nbsp;<asp:TextBox ID="ui_PoliticalL" runat="server" CssClass="easyui-validatebox" required="true"></asp:TextBox>
            </td>
        </tr>  
        <tr>
            <td style="font-weight: bold;">
                学历:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">最高学历</span><asp:DropDownList ID="ui_edu" runat="server">
                    </asp:DropDownList>
            </td>
        </tr>  
        </table>
        <table class="table" width="600" align="center">
        
        <tr>
            <td style="font-weight: bold; width: 100px;">
                其它:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">资料内容，介绍</span> &nbsp;<asp:TextBox ID="ui_content" runat="server"
                    TextMode="MultiLine" Rows="5" Columns="60"></asp:TextBox>
            </td>
        </tr>
       
        <tr>
            <td style="font-weight: bold;">
                原件保存部门:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件原件保存部门</span> &nbsp;<asp:DropDownList ID="ui_DepartentID" runat="server">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <b>责任人：</b> <asp:TextBox ID="li_Manage" runat="server" Width="60" Enabled="false"  CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="ui_Manage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择负责人','ui_Manage','li_Manage',468,395);" />
            </td>
        </tr>
         <tr>
            <td style="font-weight: bold;">
                证件名称:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件名称</span> &nbsp;<asp:TextBox ID="ui_title" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; width: 100px;">
                证件号码:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">证件号码</span> &nbsp;<asp:TextBox MaxLength="18" ID="ui_LNO" runat="server"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td style="font-weight: bold;">
                附件:<a href="#" class="help">[?]</a>
            </td>
            <td>
                <span class="note">资料相关附件，目前支持上传5个，较多资料请打包上传</span>
                <asp:FileUpload ID="FileUpload0" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal0"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but0" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal1"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but1" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload2" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal2"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but2" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload3" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal3"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but3" runat="server" Text="删除"
                        OnClick="but2_Click" /><br />
                <asp:FileUpload ID="FileUpload4" runat="server" />&nbsp;&nbsp;<asp:Label ID="Literal4"
                    runat="server"></asp:Label>&nbsp;&nbsp;<asp:Button ID="but4" runat="server" Text="删除"
                        OnClick="but2_Click" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </td>
        </tr>
         <tr>
            <th style="width: 140px; font-weight: bold;">
                变更责任人：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更的责任人</span>
                <asp:TextBox ID="li_logmanage" runat="server" Width="60" Enabled="false"  CssClass="easyui-validatebox" required="true"></asp:TextBox>
                        <asp:HiddenField ID="ui_logmanage" runat="server" />
                        <input type="button" class="SmallButtonB" value="选择责任人" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11&SelectModel=Single','选择责任人','ui_logmanage','li_logmanage',468,395);" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 140px; font-weight: bold;">
                变更备注：&nbsp;<a href="#" class="help">[?]</a>
            </th>
            <td>
                <span class="note">信息变更简单说明：如变更的原因、谁提出的变更、变更内容一句话描述等</span>
                <asp:TextBox ID="ui_logcontent" runat="server" Columns="150"
                    MaxLength="50" CssClass="easyui-validatebox" required="true"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold;">
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" /> &nbsp;&nbsp;&nbsp;<asp:Button ID="Button2" Visible="false" runat="server" CssClass="button" OnClick="Button1_Click"
                    Text="删除" />
            </td>
        </tr>
    </table>
</asp:Content>
