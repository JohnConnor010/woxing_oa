<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicTable.aspx.cs" Inherits="wwwroot.Manage.include.DynamicTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <link type="text/css" href="/Manage/css/style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <style type="text/css">
        table.table input[type='text'], select
        {
            width: 98%;
        }
        table.table input[type='text'], select
        {
            border: solid 1px #767676;
        }
        table td {height:15px; padding:0px,0px,0px,0px;}
        table td input[type='button'],input[type='submit']{width:40px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="table3" style='font-weight:bold; text-align:center;' cellspacing="0" cellpadding="0">
    <%=pagetitle+ pagestr %>
    </table>
    </div>
    </form>
</body>
</html>
