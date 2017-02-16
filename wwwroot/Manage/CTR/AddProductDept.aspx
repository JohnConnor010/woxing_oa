<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="AddProductDept.aspx.cs" Inherits="wwwroot.Manage.CTR.AddProductDept" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
   销售管理 >> 产品管理 >> 产品部门
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Product_Edit" CurIndex="3" Param1="{Q:ProductID}" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound" visible="false">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="部门">
                <ItemTemplate>
                    <%# Eval("DeptName")%>
                </ItemTemplate>
                <ItemStyle Width="150" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="月维护费用">
                <ItemTemplate>
                    <%# WX.PDT.ProductDept.FeeTypestr[Convert.ToInt32(Eval("MonthFeeType"))] + (Eval("MonthFeeType").ToString() == "0" ? "：" : "的") + Eval("MonthFee") + (Eval("MonthFeeType").ToString() == "0" ? "元" : "%")%></ItemTemplate>
                <ItemStyle Width="150" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="制作费用">
                <ItemTemplate>
                <%# WX.PDT.ProductDept.FeeTypestr[Convert.ToInt32(Eval("FeeType"))] + (Eval("FeeType").ToString() == "0" ? "：" : "的") + Eval("Fee") + (Eval("FeeType").ToString() == "0" ? "元" : "%")%>
                </ItemTemplate>
                <ItemStyle Width="150" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="部门服务内容">
                <ItemTemplate>
                    <%# Eval("Remarks")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="">
            <HeaderTemplate><a href="?ProductID=<%=WX.Request.rProductId %>">添加</a></HeaderTemplate>
                <ItemTemplate>
                    <asp:HyperLink
                        ID="HyperLink1" runat="server" Visible='<% #this.Master.A_Edit %>' Text="编辑"
                        NavigateUrl='<%# String.Format("?ProductID={0}&ProductDeptID={1}",WX.Request.rProductId,Eval("ID")) %>'></asp:HyperLink>
                    <asp:Literal ID="Literal3" runat="server" Visible='<% #this.Master.A_Del %>'>&nbsp;|&nbsp;</asp:Literal><asp:LinkButton
                        ID="LinkButton3" runat="server" Visible='<% #this.Master.A_Del %>' CausesValidation="False"
                        OnClick="Del" OnClientClick="return confirm('信息删除后不可恢复，确定删除？');" CommandName='<% #Eval("id") %>'
                        Text="删除" />
                </ItemTemplate>
                <ItemStyle Width="80" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
    <div  id="divdept" runat="server" visible="false">
        <table class="table1">   
            <thead>
            <tr class="">
                <td>
                    产品名称：<asp:Literal ID="liproductName" runat="server"></asp:Literal>&nbsp;
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>  
        </thead>
        <tbody>
             <tr>
                <th style="width: 145px; font-weight: bold;">
                    选择部门：
                </th>
                <td>
                    <asp:DropDownList ID="ProductDeptID" runat="server">
                    </asp:DropDownList>
&nbsp;</td>
            </tr>
             <tr>
                <th style="width: 145px; font-weight: bold;">
                    月维护费用：
                </th>
                <td>
                    <asp:DropDownList ID="Feetype1" runat="server" onchange='Settip(this.value, "span1")'>
                    </asp:DropDownList>
                    <asp:TextBox ID="MonthFee" runat="server" Width="80"></asp:TextBox>
&nbsp;<span id="span1"></span></td>
            </tr>
             <tr>
                <th style="width: 145px; font-weight: bold;">
                    制作费用：
                </th>
                <td>
                    <asp:DropDownList ID="Feetype2" runat="server" onchange='Settip(this.value, "span2")'>
                    </asp:DropDownList>
                    <asp:TextBox ID="Fee" runat="server" Width="80"></asp:TextBox>
&nbsp;<span id="span2"></span></td>
            </tr>
            <tr>
                <th style="width: 145px; font-weight: bold;">
                    部门服务内容：
                </th>
                <td>
                    <asp:TextBox ID="txtDeptRemarks" runat="server" Columns="80" Rows="4" 
                        TextMode="MultiLine"></asp:TextBox>
&nbsp;</td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="添加" OnClientClick="return Validator.Validate(this.form,3);" 
                        onclick="btnSave_Click" />
                </td>
            </tr>
            </tbody>
    </table>
    </div>
    
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
