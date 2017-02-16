<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="wwwroot.Manage.MyManage.Index" %>

<%@ Register Src="~/Manage/include/MenuBar.ascx" TagPrefix="uc1" TagName="MenuBar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
        .level1 {
            width: 180px;
            border-left: 5px solid #0076df;
            text-align: center;
            border-bottom: 1px solid #278ee9;
        }

            .level1 li {
                height: 50px;
                font: 14px/50px "微软雅黑";
                border-top: 1px solid #ddd;
                background: #278ee9;
                color: #fff;
                position: relative;
            }

                .level1 li a {
                    color: #fff;
                }

        .level2 {
            width: 180px;
            text-align: center;
            border-bottom: 1px solid #52a0e5;
        }

            .level2 li {
                height: 50px;
                font: 14px/50px "微软雅黑";
                border-top: 1px solid #ddd;
                background: #52a0e5;
                color: #fff;
                position: relative;
            }

                .level2 li a {
                    color: #fff;
                }

        .level3 {
            width: 180px;
            text-align: center;
            border-bottom: 1px solid #99cfff;
        }

            .level3 li {
                height: 50px;
                font: 14px/50px "微软雅黑";
                border-top: 1px solid #ddd;
                background: #99cfff;
                color: #fff;
            }

                .level3 li a {
                    color: #fff;
                }

        .hoverstyle {
            font-weight: bold;
        }

        .level4 {
            width: 180px;
            text-align: center;
            border-bottom: 1px solid #99cfff;
        }
         .level4 li {
                height: 50px;
                font: 14px/50px "微软雅黑";
                border-top: 1px solid #ddd;
                background: #b1d9fc;
                color: #fff;
            }

                .level4 li a {
                    color: #fff;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar runat="server" ID="MenuBar" Key="MyManage" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Menu ID="Menu1" runat="server" Height="420" Style="margin-left: 20%">
        <LevelMenuItemStyles>
            <asp:MenuItemStyle CssClass="level1" />
            <asp:MenuItemStyle CssClass="level2" />
            <asp:MenuItemStyle CssClass="level3" />
            <asp:MenuItemStyle CssClass="level4" />
        </LevelMenuItemStyles>
        <StaticHoverStyle CssClass="hoverstyle" />
    </asp:Menu>
</asp:Content>
