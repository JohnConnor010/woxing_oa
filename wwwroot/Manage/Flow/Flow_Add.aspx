<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/Master/MasterPage2.Master"
    AutoEventWireup="true" CodeBehind="Flow_Add.aspx.cs" Inherits="wwwroot.Manage.Flow.Flow_Add" %>

<%@ MasterType VirtualPath="~/Manage/Master/MasterPage2.Master" %>
<%@ Register Src="../include/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptPlaceHolder" runat="server">
    <style type="text/css">
         table.table input[type='text'],select {border:solid 1px #777;}
         table.table td{ padding-left:10px;}
    </style>
    <script src="/App_Scripts/popup.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationHolder" runat="server">
    系统管理 >> 流程定义
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="text" runat="server">
    <uc1:MenuBar ID="MenuBar1" runat="server" Key="flow" CurIndex="2" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <table class="table">
        <tbody>
            <tr>
                <th style="width: 135px;">
                    流程名称：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">请输入工作流模型名称 如：请假申请流程
                    </span>&nbsp;<asp:TextBox ID="txtFlowName" runat="server" Width="200px" MaxLength="30"
                        dataType="Chinese" require="true" msg="流程名称必须为汉字"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th >
                    流程分类：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">流程的主要组织方式
                    </span><asp:DropDownList ID="ddlFlowCategory" runat="server" dataType="Require" require="true" msg="流程分类不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    排序号：&nbsp;<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">流程排序号，是流程在自己分类中的排序位置。
                    </span>&nbsp;<asp:TextBox ID="txtSort" runat="server" Width="30px" MaxLength="3" dataType="Custom"
                        regexp="/^\d{1,3}$/" require="true" msg="流程排序号必须为数字"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    流程类型：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">固定流程是公司的主要使用分类。
                    </span><asp:DropDownList ID="ddlFlowType" runat="server" dataType="Require" require="true" msg="流程分类不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;所属部门：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">固定流程所属部门，将决定谁有权利编辑它。
                    </span><asp:DropDownList ID="ddlDepartment" runat="server" dataType="Require" require="true" msg="流程分类不能为空">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;表单：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">每个流程将针对一个表单，表单将是流程的所有经办人工作的舞台!
                    </span><asp:DropDownList ID="ddlForm" runat="server" Width="200px" dataType="Require" require="true" msg="流程分类不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    委托类型：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">
                        <br/>
                        <b>1.自由委托：</b>用户可以在工作委托模块中设置委托规则,可以为委托给任何人。<br/>
                        <b>2.按步骤设置的经办权限委托：</b>仅能委托给流程步骤设置中经办权限范围内的人员<br/>
                        <b>3.按实际经办人委托：</b>仅能委托给步骤实际经办人员。<br/>
                        <b>4.禁止委托：</b>办理过程中不能使用委托功能。<br/>
                        <b>注意：</b>只有自由委托才允许定义委托规则，委托后更新自己步骤为办理完毕，主办人变为经办人
                        </span><asp:DropDownList ID="ddlDelegateType" runat="server" dataType="Require" require="true" msg="流程分类不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    允许附件：<a class="href" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">在签办过程中是否允许附件!
                   </span><asp:DropDownList ID="ddlAllowUpload" runat="server">
                        <asp:ListItem Value="0">是</asp:ListItem>
                        <asp:ListItem Value="1">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    可视：<a class="href" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">
                   </span><asp:DropDownList ID="ddlIsVisible" runat="server">
                        <asp:ListItem Value="0">显式</asp:ListItem>
                        <asp:ListItem Value="1">隐式</asp:ListItem>
                    </asp:DropDownList>显式：员工可以在流程申请中看到并提交申请。隐式：流程申请列表中不可见，应用于某个功能的流程审批。（非技术人员请选显式）
                </td>
            </tr>
            <tr>
                <th>
                    传阅人：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">传阅人选择，在流转过程中是否可以进行传阅给其它人!
                    </span>&nbsp;<asp:CheckBox ID="chkAllowView" runat="server" Text="允许传阅" />
                    <br /><input type="hidden" runat="server" id="hidden_users" name="hidden_users" value="" clientidmode="Static" />
                    &nbsp;<asp:TextBox ID="txtUsers" Font-Size="12px" runat="server" Width="400px" Height="40px" ReadOnly="True" ClientIDMode="Static"
                        Rows="10" TextMode="MultiLine"></asp:TextBox>
                    &nbsp;&nbsp;╋<a href="javascript:void(0)" onclick="PopupIFrame('/App_Ctrl/SelectPeople.aspx?CompanyId=11','选择人员','hidden_users','txtUsers',468,395);">选择</a>
                </td>
            </tr>
            <tr>
                <th>
                    流水号规则：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">流水号规则，具体见流水号定义模块!
                    </span><asp:DropDownList ID="ddlNumberRules" runat="server" dataType="Require" require="true" msg="流程分类不能为空">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th >
                    流程说明：<a class="help" href="#">[?]</a>
                </th>
                <td><span class="note" style="display: none;">流程说明主要是工作相关的说明，可以说明流程的工作细则与注意事项!
                    </span>&nbsp;<asp:TextBox ID="txtDesc" Font-Size="12px" runat="server" width="98%" Rows="10" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <div style="width: 98%; text-align: center;">
        <asp:Button ID="Button2" runat="server" Text="保存" OnClientClick="return Validator.Validate(this.form,3);"
            OnClick="SubmitData" CssClass="button" />
        &nbsp;&nbsp;<input type="reset" class="button" value='重置' /></div>
</asp:Content>
