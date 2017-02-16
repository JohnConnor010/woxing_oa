<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="wwwroot.Manage.CTR.AddProduct" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    销售管理 >> 添加产品
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Product" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table1">
        <thead>
            <tr class="">
                <td>
                    产品基本信息&nbsp;
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品编号：
                </th>
                <td>
                    <div style="float: left">
                        &nbsp;<asp:TextBox ID="txtProductID" runat="server" ReadOnly="True"></asp:TextBox></div>
                    <div style="float: left">
                        <asp:RadioButtonList ID="rIsEnable" runat="server" RepeatColumns="2">
                            <asp:ListItem Value="1" Text="启用" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品名称：
                </th>
                <td>
                    <asp:TextBox ID="txtProductName" runat="server" Columns="30" dataType="Require" msg="产品名称为必填项！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;* 产品分类：
                </th>
                <td>
                    <asp:DropDownList ID="ddlProductCategory" runat="server">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    &nbsp;产品规格：
                </th>
                <td>
                    <asp:TextBox ID="txtSpecification" runat="server"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    计量单位：
                </th>
                <td>
                    <asp:DropDownList ID="ddlUnits" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    销售价格：
                </th>
                <td>
                    <asp:TextBox ID="txtSalesPrice" runat="server" Columns="10" dataType="Currency" msg="销售价格的格式不正确！">0</asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    优惠价格：
                </th>
                <td>
                    <asp:TextBox ID="txtDiscountedPrice" runat="server" Columns="10" dataType="Currency"
                        msg="优惠价格的格式不正确！">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    成本价格：
                </th>
                <td>
                    <asp:TextBox ID="txtCostPrice" runat="server" Columns="10" dataType="Currency" msg="成本价格的格式不正确！">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    产品说明：
                </th>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" Columns="80" Rows="4" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    服务内容：
                </th>
                <td>
                    <asp:TextBox ID="txtServices" runat="server" Columns="80" Rows="4" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
        </tbody>
        <body id="pdept" runat="server" visible="false">
            <thead>
                <tr class="">
                    <td>
                        产品部门信息&nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <th style="width: 145px; font-weight: bold;">
                        选择部门：
                    </th>
                    <td>
                        <asp:DropDownList ID="ProductDeptID" runat="server">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <th style="width: 145px; font-weight: bold;">
                        月维护费用：
                    </th>
                    <td>
                        <asp:DropDownList ID="Feetype1" runat="server" onchange='Settip(this.value, "span1")'>
                        </asp:DropDownList>
                        <asp:TextBox ID="MonthFee" runat="server" Width="80"></asp:TextBox>
                        &nbsp;<span id="span1"></span>
                    </td>
                </tr>
                <tr>
                    <th style="width: 145px; font-weight: bold;">
                        制作费用：
                    </th>
                    <td>
                        <asp:DropDownList ID="Feetype2" runat="server" onchange='Settip(this.value, "span2")'>
                        </asp:DropDownList>
                        <asp:TextBox ID="Fee" runat="server" Width="80"></asp:TextBox>
                        &nbsp;<span id="span2"></span>
                    </td>
                </tr>
                <tr>
                    <th style="width: 145px; font-weight: bold;">
                        部门服务内容：
                    </th>
                    <td>
                        <asp:TextBox ID="txtDeptRemarks" runat="server" Columns="80" Rows="4" TextMode="MultiLine"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
            </thead>
        </body>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
                    OnClick="btnSave_Click" />
                &nbsp;&nbsp;&nbsp;<input type="reset" value="重置" class="button">
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function Settip(valuestr, idstr) {
            if (valuestr == "0")
                document.getElementById(idstr).innerHTML = "元";
            else
                document.getElementById(idstr).innerHTML = "%";
        }
        Settip(document.getElementById("ContentPlaceHolder_Feetype1").value, "span1");
        Settip(document.getElementById("ContentPlaceHolder_Feetype2").value, "span2");
</script>
</asp:Content>
