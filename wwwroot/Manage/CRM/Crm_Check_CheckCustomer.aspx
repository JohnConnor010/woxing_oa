<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Crm_Check_CheckCustomer.aspx.cs" Inherits="wwwroot.Manage.CRM.Crm_Check_CheckCustomer" %>

<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <script type="text/javascript" src="/App_Scripts/popup.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    客户管理 >> 客户审核
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="Customer-Check" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <thead>
            <tr class="">
                <td>
                    基本信息审核&nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <td>
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
                        <asp:BoundField HeaderText="客户名称" DataField="CustomerName"></asp:BoundField>
                        <asp:TemplateField HeaderText="客户分类">
                            <ItemTemplate>
                                <%#String.Format("{0}{1}{2}{3}", Eval("CategoryName", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}"))%>
                            </ItemTemplate>
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="业务阶段" DataField="StageName">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="维护人" DataField="RealName">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="管理">
                            <ItemTemplate>
                                <a class="show" href="javascript:PopupIFrame('<%#(Eval("checktype").ToString()=="1"?"Crm_Check_CustomerDetail.aspx?CustomerTempID":"Crm_Check_ContactDetail.aspx?ContactTempID")+"="+Eval("ID") %>&CustomerID=<%#Eval("CustomersID") %>','客户资料详细信息','','',800,<%#Eval("checktype").ToString()=="1"?"995":"600" %>)">
                                    <b>审核</b>&nbsp;(<font color="red"><%#Eval("checktype").ToString() == "1" ? (Eval("CustomersID").ToString() == "" ? "创建信息" : "修改信息") : (Eval("State").ToString() == "-1" ? "删除联系人" : (Eval("ContactID").ToString() == "" ? "添加联系人" : "修改联系人"))%></font>)</a>
                            </ItemTemplate>
                            <ItemStyle Width="110px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tbody> 
        <thead>
            <tr class="">
                <td>
                    废弃审核&nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <td>
             <asp:GridView ID="GridView1" runat="server" CssClass="table tableview" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" 
                    onrowcommand="GridView1_RowCommand" ShowHeader="false">
                    <Columns>
                        <asp:TemplateField HeaderText="客户编号">
                            <ItemTemplate>
                                <img id="Img1" alt="" src='/Images/Customer.Gif' />
                                <%# Eval("CustomerID")%>
                            </ItemTemplate>
                            <ItemStyle Width="120" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="客户名称" DataField="CustomerName"></asp:BoundField>
                        <asp:TemplateField HeaderText="客户分类">
                            <ItemTemplate>
                                <%#String.Format("{0}{1}{2}{3}", Eval("CategoryName", "{0}"), Eval("CompanyNature", "&nbsp;/&nbsp;{0}"), Eval("LevelName", "&nbsp;/&nbsp;{0}"), Eval("IndustryName", "&nbsp;/&nbsp;{0}")) %>
                            </ItemTemplate>
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="业务阶段" DataField="StageName">
                            <ItemStyle Width="100px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="维护人" DataField="RealName">
                            <ItemStyle Width="80px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="管理">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandName="state1" CommandArgument='<%#Eval("ID") %>' runat="server">通过</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" CommandName="state2" CommandArgument='<%#Eval("ID") %>' runat="server">不通过</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="110px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tbody> 
         <thead>
            <tr class="">
                <td>
                    跟踪审核&nbsp;
                </td>
            </tr>
        </thead>
        <tbody>
            <td>
            <asp:GridView ID="GridView2" runat="server" CssClass="table tableview" AllowPaging="True"
        AllowSorting="True" AutoGenerateColumns="False" PageSize="1000" OnRowCommand="Gv_customer_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="详细">
                <ItemTemplate>
                    <fieldset style="padding: 10px; line-height: 180%;">
                        <legend style="font-weight: bold;"><img src='/images/procstate<%# Eval("ProcessState")%>.bmp' />
                        <a href="javascript:PopupIFrame('Crm_SingleM_ShowTrack.aspx?TrackID=<%# Eval("Id")%>','预览跟踪信息','','',850,450)">
                            <%# WX.CRM.Track.ProcessState[Convert.ToInt32(Eval("ProcessState"))]%></a><font color="gray">(<%# WX.Main.GetTimeEslapseStr(Convert.ToDateTime(Eval("TrackTime")),"","") %>)</font>
                           <%# Request["CustomerID"] == null || Request["CustomerID"] == "" ? "——" + Eval("CustomerName") : ""%>——<%# Eval("RealName")%></legend>
                        <asp:Literal runat="server" ID="liMemo" Text='<%#GetMemo(Eval("Remarks")) %>'></asp:Literal>
                        <%# String.IsNullOrEmpty(Convert.ToString(Eval("LogParaments"))) ? "": "<br/>跟踪详情：" + Eval("LogParaments").ToString() %>
                    </fieldset>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <div style="line-height: 180%; vertical-align: top; padding-top: 10px; color: #666666;">
                        状态：<%# Eval("State").ToString()=="1"?"已执行":"未执行"%>&nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton1" CommandArgument='<%# Eval("Id")%>'
                            CommandName="del" runat="server">通过</asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" CommandArgument='<%# Eval("Id")%>'
                            CommandName="del2" runat="server">不通过</asp:LinkButton>
                             <a href="javascript:PopupIFrame('CRM_SingleM_PJTrack.aspx?CustomerID=<%=WX.Request.rCustomerID %>','评价','','',450,300)">
            评价</a>
                        <br />
                        花销：<%# string.Format("{0:C2}",Eval("Fee"))%><br />跟踪次数：<%# Eval("TrackNo").ToString()%><br />跟踪时间：<%# Convert.ToDateTime(Eval("TrackTime")).ToString("yyyy-MM-dd HH:mm:ss")%></div>
                </ItemTemplate>
                <ItemStyle Width="180px" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
            </td>
        </tbody> 
    </table>
</asp:Content>
