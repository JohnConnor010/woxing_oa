using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;
using System.Data.SqlClient;
using ULCode.QDA;
using System.Data;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    Dict.BindListCtrl_enum_ManageType(this.ddlManageType, null, null, null);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string flowId = Request.QueryString["Id"];
            string manageType = this.ddlManageType.SelectedItem.Value;
            string userList = this.hidden_UserList.Value;
            string deptList = string.Empty;
            string dutyList = this.hidden_RoleList.Value;
            string scope = ddlScope.SelectedItem.Value;
            if (ddlScope.SelectedItem.Value == "CUSTOM")
            {
                deptList = this.hidden_AllDepartment.Value;
            }
            else
            {
                deptList = this.hidden_DepartmentList.Value;
            }
            //3.验证用户变量，包含Request.QueryString及Request.Form
            if (!ULCode.Validation.IsNumber(Request.QueryString["Id"]))
            {
                ULCode.Debug.we("你没有权限访问此功能！");
                return;
            }

            //4.业务处理过程
            string cmdText = "INSERT INTO Fl_FlowManage(FlowId,ManageType,Scope,UserList,DeptList,DutyList) VALUES (" + flowId + "," + manageType + ",'" + scope + "','" + userList + "','" + deptList +"','" + dutyList + "')";
            int row = 0;
            bool b = false;
            DataTable table = XSql.GetDataTable("SELECT ManageType FROM Fl_FlowManage WHERE FlowId=" + flowId);
            b = table.AsEnumerable().Any(f => f.Field<Byte>("ManageType") == Convert.ToByte(this.ddlManageType.SelectedItem.Value));
            if (b == true)
            {
                ULCode.Debug.Alert("此授权类型已经添加过，不能重复添加！", "Flow_Priv.aspx?id=" + flowId);
                return;
            }
            else
            {
                row = XSql.Execute(cmdText);
            }

            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "管理权限添加成功！", null);
            }

            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                ULCode.Debug.Alert("管理权限添加成功！", "Flow_Priv.aspx?id=" + flowId);
            }
        }
    }
}