<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="CTR_Label.aspx.cs" Inherits="wwwroot.Manage.CTR.CTR_Label" %>
    
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    销售管理 >> 模板管理 >> 标签管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Sale_Temp" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table1">
        <tr>
            <td width="100"><b>标签中文说明：</b>
            </td>
            <td><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp; <b>选择分类：</b><asp:DropDownList ID="txtType" runat="server" onselectedindexchanged="txtStyle_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr> <td><b>标签英文名称：</b>
            </td>
            <td>  <asp:TextBox ID="txtName" runat="server"></asp:TextBox> 
           &nbsp;&nbsp;&nbsp;&nbsp; <b>类型：</b><asp:DropDownList ID="txtStyle" runat="server" 
                    onselectedindexchanged="txtStyle_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>                
                <asp:DropDownList ID="txtVarP" runat="server" Visible="false">
                <asp:ListItem Value="Title" Text="方案标题"></asp:ListItem>
                <asp:ListItem Value="Remarks" Text="方案落款"></asp:ListItem>
                <asp:ListItem Value="ZDFee" Text="方案价格"></asp:ListItem>
                <asp:ListItem Value="ProgramTime" Text="递交时间"></asp:ListItem>
                <asp:ListItem Value="PProducts" Text="产品列表"></asp:ListItem>
                </asp:DropDownList>             
                <asp:DropDownList ID="txtVarA" runat="server" Visible="false">
                <asp:ListItem Value="CustomerID" Text="客户名称"></asp:ListItem>
                <asp:ListItem Value="AllFee" Text="协议总价"></asp:ListItem>
                <asp:ListItem Value="Fee" Text="已付金额"></asp:ListItem>
                <asp:ListItem Value="OverFee" Text="余额"></asp:ListItem>
                <asp:ListItem Value="OverTime" Text="余额时间"></asp:ListItem>
                <asp:ListItem Value="Invoice" Text="已开发票金额"></asp:ListItem>
                <asp:ListItem Value="OverInvoice" Text="剩余发票金额"></asp:ListItem>
                <asp:ListItem Value="StartTime" Text="协议生效时间"></asp:ListItem>
                <asp:ListItem Value="StopTime" Text="协议结束时间"></asp:ListItem>
                <asp:ListItem Value="AProducts" Text="产品列表"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtContent" runat="server" Visible="false" Width="400"></asp:TextBox>  
            </td>
        </tr>
        <tr>
            <td><b>标签格式：</b>
            </td>
            <td>
                <div style="float:left;">
                    <FCKeditorV2:FCKeditor ID="fckFormat" ToolbarSet="Basic" ToolbarCanCollapse="true" ToolbarStartExpanded="false" runat="server" Height="160" Width="450" Value="">
                    </FCKeditorV2:FCKeditor></div>
               <div style="padding-left:15px;">&nbsp;&nbsp;<b>说明：</b><br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1、变量：例<%=Server.HtmlEncode("<div>{标签英文名称}</div>") %>，留空默认直接输出变量。<br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、sql语句：例<br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%=Server.HtmlEncode("<ul>") %><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{do}<br />               
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=Server.HtmlEncode("<li>{$字段名}</li>") %><br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{/do}<br />               
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=Server.HtmlEncode("</ul>") %><br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、纯内容：例<%=Server.HtmlEncode("<div>文字</div>") %><br />
               </div>
               <script language="javascript" type="text/javascript">
                   function FCKeditor_OnComplete(editorInstance) {
                       editorInstance.SwitchEditMode();
                   }
</script>
            </td>
        </tr>
        <tr>
            <td> &nbsp;
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="保存" onclick="Button1_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="Gv_customer" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="10000" empt>
        <Columns>
            <asp:TemplateField HeaderText="名称">
                <ItemTemplate>
                    <%#Eval("Title")%>
                </ItemTemplate>
                <ItemStyle />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标签">
                <ItemTemplate>
                    <%#Eval("Name").ToString()%>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" onselectedindexchanged="GVType_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </HeaderTemplate>
                <ItemTemplate>
                    <%#WX.CRM.Customer_Temp.TypeStr[Convert.ToInt32(Eval("Type").ToString())]%>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建人">
                <ItemTemplate>
                    <%# WX.CommonUtils.GetRealNameListByUserIdList(Eval("UserID").ToString())%>
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间">
                <ItemTemplate>
                    <%#Eval("Addtime").ToString() != "" ? Convert.ToDateTime(Eval("Addtime")).ToString("yyyy-MM-dd HH:ss:mm") : ""%>
                </ItemTemplate>
                <ItemStyle Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href='?LableID=<%# Eval("id") %>'>编辑</a>
                </ItemTemplate>
                <ItemStyle Width="60px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
        CssClass="badoo">
    </webdiyer:AspNetPager>
</asp:Content>
