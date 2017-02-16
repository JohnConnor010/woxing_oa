using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_CmpManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadPlanLinks();
                this.LoadTitleBar();

                string sSql = "select [ID],[Name] from TE_Departments";
                System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable(sSql);
                int width = dt2.Rows.Count > 5 ? 20 : 100 / dt2.Rows.Count;
                string userPlanImage = "<img alt=\"个人计划\" style=\"width:15px;height:15px;\" src=\"/Images/UserPlan.gif\" />";
                string deptPlanImage = "<img alt=\"部门计划\" style=\"width:15px;height:15px;\" src=\"/Images/DeptPlan.gif\" />";
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    deptday.Text += "<div style='float:left;text-align:center;width:" + width + "%'>" + userPlanImage + "<a " + this.CheckPlan(Convert.ToInt32(dt2.Rows[i]["ID"]), DateTime.Now, 1, 1) + " href=\"Plan_ManagerPlanDay.aspx?type=1&dept=" + dt2.Rows[i]["ID"] + "&rtype=2\">" + dt2.Rows[i]["Name"] + "</a></div>";
                    deptweek.Text += "<div style='float:left;text-align:center;width:" + width + "%'>" + deptPlanImage + "<a " + this.CheckPlan(Convert.ToInt32(dt2.Rows[i]["ID"]), DateTime.Now, 2, 2) + " href=\"Plan_ManagerPlanDay.aspx?type=2&dept=" + dt2.Rows[i]["ID"] + "&rtype=2\">" + dt2.Rows[i]["Name"] + "</a></div>";
                    deptmonth.Text += "<div style='float:left;text-align:center;width:" + width + "%'>" + deptPlanImage + "<a " + this.CheckPlan(Convert.ToInt32(dt2.Rows[i]["ID"]), DateTime.Now, 3, 2) + " href=\"Plan_ManagerPlanDay.aspx?type=3&dept=" + dt2.Rows[i]["ID"] + "&rtype=2\">" + dt2.Rows[i]["Name"] + "</a></div>";
                }
            }
        }
        private void LoadTitleBar()
        {
            this.liToday.Text = String.Format("{0:yyyy年MM月dd日}", DateTime.Now);
            this.liThisWeek.Text = new WX.Model.Plan.Day(DateTime.Now).ToWeekDayStr();
            this.liThisMonth.Text = String.Format("{0:yyyy年MM月}", DateTime.Now);
        }
        /// <summary>
        /// 成立前提，部门计划必须由部门主管来创建，而且部门主管永不变动
        /// 故作废
        /// </summary>
        public string CheckPlan(string UserID, DateTime Starttime, int type, int rtype)
        {
            return WX.Model.Plan.CheckModel(UserID, Starttime, type, rtype) ? "style='text-decoration:underline;'" : "";
        }
        public string CheckPlan(int deptId, DateTime Starttime, int type, int rtype)
        {
            return WX.Model.Plan.CheckModel(deptId, Starttime, type, rtype) ? "style='text-decoration:underline;'" : "";
        }
        private void LoadPlanLinks()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select pp.*,td.Name RealName from PLAN_Plan  pp left join TE_Departments td on pp.DepartmentID=td.id where RangeType>1 and PlanState=1 order by RangeType desc,pp.Addtime desc");
            rptPlanLinks.DataSource = dt;
            rptPlanLinks.DataBind();
        }
        private void LoadTaskLinks()
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select top 5 pt.*,tu.RealName from PLAN_Task pt left join PLAN_Plan pp on pt.PlanId=pp.id left join TU_Users tu on pp.UserID=tu.UserID where pt.State=1 and pp.Type>1 and pp.RangeType>1 order by pt.Statetime");
            rptTaskLinks.DataSource = dt;
            rptTaskLinks.DataBind();        
        }
    }
}