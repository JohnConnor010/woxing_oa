<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyNotify.aspx.cs" Inherits="wwwroot.Manage.XZ.MyNotify" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet"
        media="all" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView1" DataKeyNames="id" CssClass="table" runat="server" AutoGenerateColumns="False"
        OnDataBound="GridView1_DataBound">
        <HeaderStyle HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="发布人">
                <ItemTemplate>
                    <%# Eval("RealName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="分类">
                <ItemTemplate>
                    <%# Eval("CategoryName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a style='<%# Eval("Istop").ToString()=="1"?"color:Red;font-weight:bold;": "color:Blue;" %>'
                        href="NotifyDetail.aspx?NotifyID=<%# Eval("id") %>">
                        <%# Eval("Title") %></a><%# Eval("Istop").ToString() == "1" ? "<img src='/images/arrow_up.gif' alt='置顶'/>" : ""%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间">
                <ItemTemplate>
                    <%#  Convert.ToDateTime(Eval("Starttime")).ToString("yyyy-MM-dd")%></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div style="text-align: center;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="flickr" OnPageChanged="AspNetPager1_PageChanged">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
