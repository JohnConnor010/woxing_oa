<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Ass_MyAssets.aspx.cs" Inherits="wwwroot.Manage.Assets.Ass_MyAssets" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <link type="text/css" href="../css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link type="text/css" rel="Stylesheet" href="../css/AspnetPager.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    我的工作 >> 我的装备 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="MyAssetsList" CurIndex="1"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <div id="PanelShow">
        <table class="table">
            <thead>
                <tr>
                    <td>
                        产品编号
                    </td>
                    <td>
                        产品名称
                    </td>
                    <td>
                        类别
                    </td>
                    <td>
                        领用数量
                    </td>
                    <td>
                        领用时间
                    </td>
                    <td>
                        产品单价
                    </td>
                    <td>
                        计量单位
                    </td>
                    <td>
                        备注信息
                    </td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="EquipmentRepeater" runat="server">
                <ItemTemplate>
                <tr>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("ProductID") %>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("ProductName") %>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("CategoryName")%>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("Quantity") %>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("AddDate") %>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("Price") %>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("UnitName") %>
                    </td>
                    <td style='<%#Eval("color") %>'>
                        <%#Eval("Remark") %>
                    </td>
                </tr>
                </ItemTemplate>
                
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CssClass="badoo">
                    </webdiyer:AspNetPager>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
