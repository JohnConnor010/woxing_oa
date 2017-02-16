using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Dates;
namespace wwwroot.Manage.Plan
{
    public partial class Plan_DeptManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadTitleBar();
                this.LoadCheckTaskLinks();

                WX.Main.CurUser.LoadMyDepartment();
                string deptName=WX.Main.CurUser.MyDepartMent.Name.ToString();
                int deptId = WX.Main.CurUser.MyDepartMent.ID.ToInt32();
                string userPlanImage = "<img alt=\"个人计划\" style=\"width:15px;height:15px;\" src=\"/Images/UserPlan.gif\" />";
                string deptPlanImage = "<img alt=\"部门计划\" style=\"width:15px;height:15px;\" src=\"/Images/DeptPlan.gif\" />";
                string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + deptId.ToString()).ToColValueList(",", 0);
                string ids2 = ULCode.QDA.XSql.GetXDataTable("select DepartentID  from TU_User_X_DutyDetail tu left join TE_DutyDetail te on te.ID=tu.DutyDetailID where tu.UserID='" + WX.Main.CurUser.UserID + "'").ToColValueList(",", 0);

                this.LoadCheckPlanLinks((ids != "" ? "," + ids : "") + (ids2 != "" ? "," + ids2 : ""));
                System.Data.DataTable dt2 = ULCode.QDA.XSql.GetDataTable("select UserID,RealName from TU_Users where DepartmentID in(" + deptId.ToString() + (ids != "" ? "," + ids : "") + (ids2 != "" ? "," + ids2 : "") + ") and State>=10 and State<40 order by Grade desc"); //WX.Main.GetDeptUsersAll(); //
                int width = 100; //dt2.Rows.Count < 10 ? 100 / dt2.Rows.Count : 10;
                userweek.Text +=  "<div style='float:left;text-align:center;width:" + width + "px'>"+deptPlanImage+"<a " + this.CheckPlan(deptId, DateTime.Now, 2, 2) + " href=\"javascript:PopupIFrame('Plan_PlanDetail.aspx?DeptID=" + deptId + "&starttime=" + DateTime.Now.ToString("yyyy-MM-dd") + "&type=2&rtype=2&estate=1','" + deptName + "的本周计划','','',600,400)\">" + deptName + "</a></div>";
                usermonth.Text +=  "<div style='float:left;text-align:center;width:" + width + "px'>"+deptPlanImage+"<a " + this.CheckPlan(deptId, DateTime.Now, 3, 2) + " href=\"javascript:PopupIFrame('Plan_PlanDetail.aspx?DeptID=" + deptId + "&starttime=" + DateTime.Now.ToString("yyyy-MM-01") + "&type=3&rtype=2&estate=1','" + deptName + "的本月计划','','',600,400)\">" + deptName + "</a></div>";
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    userday.Text += "<div style='float:left;text-align:center;width:" + width + "px'>"+userPlanImage+"<a  " + this.CheckPlan(Convert.ToString(dt2.Rows[i]["UserID"]), DateTime.Now, 1, 1) + " href=\"javascript:PopupIFrame('Plan_PlanDetail.aspx?UserID=" + dt2.Rows[i]["UserID"] + "&starttime=" + DateTime.Now.ToString("yyyy-MM-dd") + "&type=1','" + dt2.Rows[i]["RealName"] + "的今日计划','','',600,400)\">" + dt2.Rows[i]["RealName"] + "</a></div>";
                    userweek.Text += "<div style='float:left;text-align:center;width:" + width + "px'>" + userPlanImage + "<a " + this.CheckPlan(Convert.ToString(dt2.Rows[i]["UserID"]), DateTime.Now, 2, 1) + " href=\"javascript:PopupIFrame('Plan_PlanDetail.aspx?UserID=" + dt2.Rows[i]["UserID"] + "&starttime=" + DateTime.Now.ToString("yyyy-MM-dd") + "&type=2&other=1','" + dt2.Rows[i]["RealName"] + "的本周计划','','',600,400)\">" + dt2.Rows[i]["RealName"] + "</a></div>";
                    usermonth.Text += "<div style='float:left;text-align:center;width:" + width + "px'>" + userPlanImage + "<a " + this.CheckPlan(Convert.ToString(dt2.Rows[i]["UserID"]), DateTime.Now, 3, 1) + " href=\"javascript:PopupIFrame('Plan_PlanDetail.aspx?UserID=" + dt2.Rows[i]["UserID"] + "&starttime=" + DateTime.Now.ToString("yyyy-MM-01") + "&type=3&other=1','" + dt2.Rows[i]["RealName"] + "的本月计划','','',600,400)\">" + dt2.Rows[i]["RealName"] + "</a></div>";
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
            return WX.Model.Plan.CheckModel(UserID,Starttime,type,rtype) ? "style='text-decoration:underline;'" : "";
        }
        public string CheckPlan(int deptId, DateTime Starttime, int type, int rtype)
        {
            return WX.Model.Plan.CheckModel(deptId, Starttime, type, rtype) ? "style='text-decoration:underline;'" : "";
        }
        private void LoadCheckPlanLinks(string ids)
        {
            string sSql = "select pp.*,tu.RealName from PLAN_Plan  pp left join TU_Users tu on pp.UserID=tu.UserID where RangeType=1 and PlanState=1 and tu.DepartmentID in(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + (ids != "" ? ids : "") + ") and tu.State>=10 and tu.State<40 order by tu.Grade desc";
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            this.rptCheckPlan.DataSource = dt;
            this.rptCheckPlan.DataBind();
        }
        private void LoadCheckTaskLinks()
        {
            string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString()).ToColValueList(",", 0);
            string sSql = "select top 5 pt.*,pp.Type,tu.RealName from PLAN_Task pt left join PLAN_Plan pp on pt.PlanId=pp.id left join TU_Users tu on pp.UserID=tu.UserID where pt.State=1 and tu.DepartmentID in(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + (ids != "" ? "," + ids : "") + ") and RangeType=1 and tu.State>=10 and tu.State<40 order by pt.Statetime";
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);// and pp.UserID!='"+WX.Main.CurUser.UserID+"'
            this.rptCheckTask.DataSource = dt;
            this.rptCheckTask.DataBind();
        }
        public string GetCheckPlanText(object oName,object type)
        {
            string periodName = null;
            switch (Convert.ToInt32(type))
            {
                case 1: periodName = "日计划"; break;
                case 2: periodName = "周计划"; break;
                case 3: periodName = "月计划"; break;
            }
            return String.Format("{0}{1}({2})","<img style='width:10;height:10' src='/Images/Form/Circle_Green.gif' />",oName,periodName);
        }
        public string GetCheckPlanUrl(object oEval)
        {
            return String.Format("javascript:PopupIFrame('/Manage/Plan/Plan_CheckPlanDetail.aspx?PlanId={0}','计划审核','','',600,400)", oEval);
        }
        public string GetCheckTaskText(object oName, object type)
        {
            string periodName = null;
            switch (Convert.ToInt32(type))
            {
                case 1: periodName = "日任务"; break;
                case 2: periodName = "周任务"; break;
                case 3: periodName = "月任务"; break;
            }
            return String.Format("{0}{1}({2})", "<img style='width:10;height:10' src='/Images/Form/Circle_Red.gif' />", oName, periodName);
        }
        public string GetCheckTaskUrl(object oEval)
        {
            return String.Format("javascript:PopupIFrame('Plan_CheckTask.aspx?PlanId={0}','任务审核','','',600,400)", oEval);
        }
    }
}