using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;
using ULCode.QDA;
using System.Data;

namespace wwwroot.Manage.Work
{
    public partial class Work_AddDelegate : System.Web.UI.Page
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
                Dict.BindListCtrl_Flows(this.ddlFlowName, null, null, null);
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
            string flowId = string.Empty;
            if (chkAllFlow.Checked == true)
            {
                flowId = "0";
            }
            else
            {
                flowId = this.ddlFlowName.SelectedItem.Value;
            }
            string principal = this.hidden_Principal.Value;
            string beThePrincipal = this.hidden_BeThePrincipal.Value;
            string startTime = "null";
            string endTime = "null";
            int status = 1;                                             //1表示委托状态 0表示不是委托状态
            if (!string.IsNullOrEmpty(this.txtStartTime.Text))
            {
                startTime = String.Format("'{0}'",this.txtStartTime.Text);
            }
            if (!string.IsNullOrEmpty(this.txtEndTime.Text))
            {
                endTime = String.Format("'{0}'", this.txtEndTime.Text);
            }

            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程
            string cmdText = "INSERT INTO FL_FlowAuthorization (FlowId,FromUserId,ToUserId,BeginDate,EndDate,Status) VALUES (" + flowId + ",'" + principal + "','" + beThePrincipal + "'," + startTime + "," + endTime + "," + status + ")";
            int row = XSql.Execute(cmdText);
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                XSql.Execute("exec dbo.Main_RefreshFlowAuthorizationState");
                WX.Main.AddLog(WX.LogType.Default, "委托添加成功！",null);
            }
            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                ULCode.Debug.Alert("委托添加成功！", "Work_DelegateList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("委托添加失败！", "Work_DelegateList.aspx");
            }
        }
    }
}