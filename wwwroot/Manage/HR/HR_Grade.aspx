<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="HR_Grade.aspx.cs" Inherits="wwwroot.Manage.HR.HR_Grade" %>
<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register src="../include/MenuBar.ascx" tagname="MenuBar" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
<style type="text/css">
       table.table input[type='text'],select { width:98%; }
       table.table input[type='text'],select { border:solid 1px #767676; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
人力资源 >> 人事档案 >> 级别管理
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
<uc1:MenuBar ID="MenuBar1" runat="server" Key="HR-Grade" CurIndex="1" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<div id="PanelManage">
       <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1"
        InsertItemPosition="FirstItem">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" Visible='<% #this.Master.A_Del %>' runat="server" CommandName="Delete" Text="删除" OnClientClick="return confirm('级别删除后不可恢复，您确定要删除吗？')" />
                    <asp:Button ID="EditButton" Visible='<% #this.Master.A_Edit %>' runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <%# Eval("Sort") %>
                </td>
                <td style="width:80px">
                    <%# Eval("Name") %>
                </td>  
                <td><%# Eval("Class")%></td>
                <td><%# Eval("Grade")%></td>
                <td><%# Eval("Wage")%></td>
                <td><%# Eval("Wage_Jobs")%></td>
                <td><%# Eval("Allowance_Hous")%></td>
                <td><%# Eval("Allowance_Traffic")%></td>
                <td><%# Eval("Allowance_Repast")%></td>
                <td><%# Eval("Allowance_Newsletter")%></td>
                <td><%# Eval("Wage_Score")%></td>
                <td><%# Eval("Allowance_SpecialPost")%></td>
                <td><%# Eval("MonthBonus")%></td>
                <td><%# Eval("OptionsBonus").ToString()=="0"?"☆":"★"%></td>
                <td><%# Eval("MidYearBonus").ToString() == "0" ? "☆" : "★"%></td>
                <td><%# Eval("EndYearBonus").ToString() == "0" ? "☆" : "★"%></td>
                <td><%# Eval("Wagesum")%></td>              
                <td style="width:100px">
                    <%# getdutyname( Eval("Sort").ToString()) %>
                </td>
                <%--<td>
                     <%# this.Master.A_Edit?"<a href='HR_GradeBind.aspx?id="+Eval("Sort")+"' >绑定职务</a>":"" %>
                </td>--%>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="更新" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="取消" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Sort") %>' Width="20" />
                </td>
                <td style="width:80px">
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="70" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Class") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Grade") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Wage") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Wage_Jobs") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Allowance_Hous") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Allowance_Traffic") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Allowance_Repast") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("Allowance_Newsletter") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Wage_Score") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("Allowance_SpecialPost") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("MonthBonus") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("OptionsBonus") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("MidYearBonus") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("EndYearBonus") %>' Width="20" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
               <%-- <td>
                    &nbsp;
                </td>--%>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr>
                    <td>
                        未返回数据。
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="插入" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="清除" />
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Sort") %>' Width="20" />
                </td>
                <td style="width:80px">
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' Width="70" />
                </td>
                <td>
                
                   <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Class") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Grade") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Wage") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Wage_Jobs") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Allowance_Hous") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Allowance_Traffic") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Allowance_Repast") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("Allowance_Newsletter") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Wage_Score") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("Allowance_SpecialPost") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("MonthBonus") %>' Width="40" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("OptionsBonus") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("MidYearBonus") %>' Width="20" />
                </td>
                <td>
                   <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("EndYearBonus") %>' Width="20" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <%--<td>
                    &nbsp;
                </td>--%>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" Visible='<% #this.Master.A_Del %>' CommandName="Delete" Text="删除" OnClientClick="return confirm('级别删除后不可恢复，您确定要删除吗？')" />
                    <asp:Button ID="EditButton" runat="server" Visible='<% #this.Master.A_Edit %>' CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <%# Eval("Sort") %>
                </td>
                <td style="width:80px">
                    <%# Eval("Name") %>
                </td>   
                <td><%# Eval("Class")%></td>
                <td><%# Eval("Grade")%></td>
                <td><%# Eval("Wage")%></td>
                <td><%# Eval("Wage_Jobs")%></td>
                <td><%# Eval("Allowance_Hous")%></td>
                <td><%# Eval("Allowance_Traffic")%></td>
                <td><%# Eval("Allowance_Repast")%></td>
                <td><%# Eval("Allowance_Newsletter")%></td>
                <td><%# Eval("Wage_Score")%></td>
                <td><%# Eval("Allowance_SpecialPost")%></td>
                <td><%# Eval("MonthBonus")%></td>
                <td><%# Eval("OptionsBonus").ToString()=="0"?"☆":"★"%></td>
                <td><%# Eval("MidYearBonus").ToString() == "0" ? "☆" : "★"%></td>
                <td><%# Eval("EndYearBonus").ToString() == "0" ? "☆" : "★"%></td>
                <td><%# Eval("Wagesum")%></td>               
                <td style="width:100px">
                    <%# getdutyname( Eval("Sort").ToString()) %>
                </td>
                <%--<td>
                    <%# this.Master.A_Edit ? "<a href='HR_GradeBind.aspx?id=" + Eval("Sort") + "' >绑定职务</a>" : ""%>
                </td>--%>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="itemPlaceholderContainer" runat="server" border="0" class="table" cellpadding="3" style="">
                <tr>
                    <td style="width:80px">
                    操作
                    </td>
                    <td>
                        编号
                    </td>
                    <td style="width:80px">
                        描述
                    </td>
                    <td>
                        级别
                    </td>
                    <td>
                        等次
                    </td>
                    
                <td>基本工资</td>
                <td>岗位工资</td>
                <td>住房补助</td>
                <td>交通补助</td>
                <td>餐饮补助</td>
                <td>通讯补助</td>
                <td>绩效工资</td>
                <td>特岗补贴</td>
                <td>月奖励</td>
                <td>期权奖励</td>
                <td>年中奖</td>
                <td>年终奖</td>
                <td>合计</td>
                    <td style="width:100px;">
                        职务
                    </td>
                    <%--<td>
                        关联操作
                    </td>--%>
                </tr>
                <tr id="itemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" runat="server" Visible='<% #this.Master.A_Del %>' CommandName="Delete" Text="删除" OnClientClick="return confirm('级别删除后不可恢复，您确定要删除吗？')" />
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="编辑" />
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("Sort") %>' />
                </td>
                <td>
                    <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:WXOAConnectionString %>" 
        DeleteCommand="DELETE FROM [HR_Grade] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [HR_Grade] ([Sort],[Name],[Addtime],[Class]
		,[Grade]
		,[Wage]
		,[Wage_Jobs]
		,[Allowance_Hous]
		,[Allowance_Traffic]
		,[Allowance_Repast]
		,[Allowance_Newsletter]
		,[Wage_Score]
		,[Allowance_SpecialPost]
		,[MonthBonus]
		,[OptionsBonus]
		,[MidYearBonus]
		,[EndYearBonus]) VALUES (@Sort,@Name,getdate(),@Class
		,@Grade
		,@Wage
		,@Wage_Jobs
		,@Allowance_Hous
		,@Allowance_Traffic
		,@Allowance_Repast
		,@Allowance_Newsletter
		,@Wage_Score
		,@Allowance_SpecialPost
		,@MonthBonus
		,@OptionsBonus
		,@MidYearBonus
		,@EndYearBonus)" 
        SelectCommand="SELECT *,([Wage]+[Wage_Jobs]+[Allowance_Hous]+[Allowance_Traffic]+[Allowance_Repast]+[Allowance_Newsletter]+[Wage_Score]+[Allowance_SpecialPost]+[MonthBonus]) Wagesum FROM [HR_Grade] order by Sort asc" 
        UpdateCommand="UPDATE [HR_Grade] SET [Name] = @Name,[Sort]=@Sort,[Class]=@Class,[Grade]=@Grade,[Wage]=@Wage
		,[Wage_Jobs]=@Wage_Jobs
		,[Allowance_Hous]=@Allowance_Hous
		,[Allowance_Traffic]=@Allowance_Traffic
		,[Allowance_Repast]=@Allowance_Repast
		,[Allowance_Newsletter]=@Allowance_Newsletter
		,[Wage_Score]=@Wage_Score
		,[Allowance_SpecialPost]=@Allowance_SpecialPost
		,[MonthBonus]=@MonthBonus
		,[OptionsBonus]=@OptionsBonus
		,[MidYearBonus]=@MidYearBonus
		,[EndYearBonus]=@EndYearBonus WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Sort" Type="Int32" />
            <asp:Parameter Name="Addtime" Type="Datetime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
            <asp:Parameter Name="Sort" Type="Int32" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    </div>
</asp:Content>