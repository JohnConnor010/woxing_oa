<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyNotifyFiles.aspx.cs" Inherits="wwwroot.Manage.XZ.MyNotifyFiles" %>

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
                    <%# Eval("CategoryName")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="范围">
                <ItemTemplate><%# WX.XZ.NotifyFiles.Areaarry[Convert.ToInt32(Eval("Area"))]%>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href="Notifyfilesshow.aspx?NotifyFileId=<%# Eval("id") %>">
                        <%# Eval("Title") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="时间">
                <ItemTemplate>
                    <%#  Convert.ToDateTime(Eval("PublishTime")).ToString("yyyy-MM-dd")%></ItemTemplate>
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
