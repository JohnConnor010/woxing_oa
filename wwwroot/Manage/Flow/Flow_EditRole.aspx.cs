using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using WX.Data;
using WX;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_EditRole : System.Web.UI.Page
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
                if (!ULCode.Validation.IsNumber(Request.QueryString["Id"]) && !ULCode.Validation.IsNumber(Request.QueryString["ManageId"]))
                {
                    Response.Write("你没有权限访问此功能！");
                    Response.End();
                }
                string flowId = Request.QueryString["Id"];
                string manageId = Request.QueryString["ManageId"];
                string cmdText = "SELECT * FROM Fl_FlowManage WHERE FlowId=" + flowId + " AND Id=" + manageId;
                DataTable dataTable = XSql.GetDataTable(cmdText);
                foreach (DataRow row in dataTable.Rows)
                {
                    Dict.BindListCtrl_enum_ManageType(this.ddlManageType, null, null, row["ManageType"].ToString());
                    this.hidden_UserList.Value = row["UserList"].ToString();
                    this.txtUserList.Text = CommonUtils.GetRealNameListByUserIdList(row["UserList"].ToString());

                    this.hidden_DepartmentList.Value = row["DeptList"].ToString();
                    this.txtDepartmentList.Text = CommonUtils.GetDeptNameListByDeptIdList(row["DeptList"].ToString());

                    this.hidden_RoleList.Value = row["DutyList"].ToString();
                    this.txtRoleList.Text = CommonUtils.GetDutyNameListByDutyIdList(row["DutyList"].ToString());

                    this.ddlScope.SelectedValue = row["Scope"].ToString();
                    if (row["Scope"].ToString() == "CUSTOM")
                    {
                        this.hidden_AllDepartment.Value = row["DeptList"].ToString();
                        this.txtAllDepartment.Text = CommonUtils.GetDeptNameListByDeptIdList(row["DeptList"].ToString());
                    }
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
            string cmdText = "UPDATE Fl_FlowManage SET ManageType=" + manageType + ",Scope='" + scope + "',UserList='" + userList + "',DeptList='" + deptList + "',DutyList='" + dutyList + "' WHERE Id=" + Request.QueryString["ManageId"];
            int row = 0;
            row = XSql.Execute(cmdText);

            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "管理权限修改成功！", null);
            }

            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                ULCode.Debug.Alert("管理权限修改成功！", "Flow_Priv.aspx?id=" + flowId);
            }
        }
    }
}