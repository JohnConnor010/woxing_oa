<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Crm_My_CustomerToCheck.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_My_CustomerToCheck" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
<script type="text/javascript" src="../../App_Scripts/zDialog.js"></script>
<script type="text/javascript">
    function personView(ud,flag) {
        var diag = new Dialog();
        diag.Width = 585;
        diag.Height = 765;
        diag.Title = "客户资料详细信息";
        diag.URL = 'Crm_ShowCustomerInfo.aspx?CustomerID=' + ud + (flag==1?"&type=1":"");
        diag.show();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
客户管理 >> 我的客户
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="MyCustomer" CurIndex="4" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
            AllowSorting="True" AutoGenerateColumns="False" PageSize="1000">
            <Columns>
                <asp:TemplateField HeaderText="客户编号">
                    <ItemTemplate>
                        <img id="Img1" alt="" src='/Images/Customer.Gif' />
                            <%# Eval("CustomerID")%>
                    </ItemTemplate>
                    <ItemStyle Width="120" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="客户名称" DataField="CustomerName">
                </asp:BoundField>
                 <asp:TemplateField HeaderText="客户分类">
                    <ItemTemplate>
                       <%#String.Format("{0}{1}{2}{3}", Eval("CategoryName", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}"))%>
                    </ItemTemplate>
                    <ItemStyle Width="300px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                       <%#Eval("checktype").ToString() == "1" ? "<b>基本信息</b>" : "<b>联系人信息（" + (Eval("State").ToString() == "-1" ? "删除" : (Eval("ContactID").ToString() != "" ? "修改" : "添加")) + "）</b>"%>
                    </ItemTemplate>
                    <ItemStyle Width="120px" />
                </asp:TemplateField>          
                <asp:BoundField HeaderText="业务阶段" DataField="StageName">
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>                
                 <asp:TemplateField HeaderText="当前状态">
                    <ItemTemplate>
                       <%#Eval("CheckState").ToString()=="-1"?"<font color='red'>未通过</font>":"等待审核"%>
                    </ItemTemplate>
                    <ItemStyle Width="70px" />
                </asp:TemplateField>              
                 <asp:TemplateField HeaderText="审核人">
                    <ItemTemplate>
                       <%#Eval("CheckUser")%>
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理">
                    <ItemTemplate>
                       <a class="show" onclick='personView("<%#Eval("checktype").ToString() == "1" ?Eval("ID"):Eval("CustomersID") %>",<%#Eval("checktype").ToString() == "1" ?1:0 %>)' href="javascript:void(0)">快速浏览</a>
                       <a class="show" href='<%#Eval("checktype").ToString() == "1" ? "Crm_Single_EditCustomer.aspx?PageMode=my&CustomerID="+Eval("ID")+"&type=1" : "Crm_Single_AddContact.aspx?PageMode=my&Action=Edit&&ContactTempID="+Eval("ID")+"&CustomerID="+Eval("CustomersID")%>'>信息维护</a>
                    </ItemTemplate>
                    <ItemStyle Width="120px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>