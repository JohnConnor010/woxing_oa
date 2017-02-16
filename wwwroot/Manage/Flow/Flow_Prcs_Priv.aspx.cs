using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using ULCode.QDA;
using System.Data;
using WX;

namespace wwwroot.Manage.Flow
{
    public partial class Flow_Prcs_Priv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!ULCode.Validation.IsNumber(Convert.ToString(Request.QueryString["flowid"]))
                    || !ULCode.Validation.IsNumber(Convert.ToString(Request.QueryString["id"])))
                {
                    ULCode.Debug.we("你没有权限访问此功能！");
                    return;
                }
                string flowId = Request.QueryString["flowId"];
                string nodeId = Request.QueryString["id"];

                Process.MODEL process = Process.GetCache(Convert.ToInt32(nodeId)); //Process.NewDataModel(nodeId);
                if (process.Priv_UserList.value != null)
                {
                    string userList = string.Empty;
                    this.hidden_UserList.Value = process.Priv_UserList.value.ToString();

                    this.txtUserList.Text = CommonUtils.GetRealNameListByUserIdList(process.Priv_UserList.value.ToString());
                }
                if (process.Priv_DutyList.value != null)
                {
                    string dutyList = string.Empty;
                    string dutyValue = process.Priv_DutyList.value.ToString();
                    this.hidden_RoleList.Value = dutyValue;
                    this.txtRoleList.Text = CommonUtils.GetDutyNameListByDutyIdList(process.Priv_DutyList.value.ToString());
                }
                if (process.Priv_DeptList.value != null)
                {
                    string deptList = string.Empty;
                    string deptValue = process.Priv_DeptList.value.ToString();
                    this.hidden_DepartmentList.Value = deptValue;

                    this.txtDepartmentList.Text = CommonUtils.GetDeptNameListByDeptIdList(process.Priv_DeptList.value.ToString());
                }
                if (process.UpdateTable.ToString() != "")
                    drop_UpdateTable.SelectedValue = process.UpdateTable.ToString();
                keyvalue.Text = process.UpdateKeyValue.ToString();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //获取id
            //*******************************************************
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.取得用户变量
            string flowId = Request.QueryString["flowId"];
            string nodeId = Request.QueryString["id"];
            string userList = this.hidden_UserList.Value;
            string dutyList = this.hidden_RoleList.Value;
            string departmentList = this.hidden_DepartmentList.Value;
            //下面语句是UI开发人员的语句，后台开发人员需删除掉。


            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            if (string.IsNullOrEmpty(flowId))
            {
                return;
            }
            if (string.IsNullOrEmpty(nodeId))
            {
                return;
            }
            //4.业务处理过程
            Process.MODEL process = Process.GetCache(Convert.ToInt32(nodeId)); //Process.NewDataModel(nodeId);
            process.Priv_UserList.set(userList);
            process.Priv_DutyList.set(dutyList);
            process.Priv_DeptList.set(departmentList);
            process.UpdateTable.set(drop_UpdateTable.SelectedValue);
            process.UpdateKeyValue.set(keyvalue.Text);
            int row = process.Update();
            //填写主要业务逻辑代码
            
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row != 0)
            {
                //WX.Main.AddLog(WX.LogType.Default, "经办权限设置成功！", "");
                ULCode.Debug.Alert("经办权限设置成功！", "Flow_Prcs_List.aspx?FlowId=" + flowId);
            }
            else
            {
                ULCode.Debug.Alert(this, "经办权限设置设置失败！");
            }
        }
    }
}