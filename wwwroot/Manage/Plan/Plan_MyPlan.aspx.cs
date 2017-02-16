using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_MyPlan : System.Web.UI.Page
    {
        public string userid = "", deptuserid = "", rtype = "2";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadTitleBar();
                this.Pageinit();
            }
        }
        private void LoadTitleBar()
        {
            this.liToday.Text = String.Format("{0:yyyy年MM月dd日}", DateTime.Now);
            this.liThisWeek.Text = this.liThisWeek1.Text = new WX.Model.Plan.Day(DateTime.Now).ToWeekDayStr();
            this.liThisMonth.Text = this.liThisMonth1.Text = String.Format("{0:yyyy年MM月}", DateTime.Now);
        }
        public string GetPlanID(int type,int rtype)
        {
            WX.Model.Plan.MODEL plan = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID,DateTime.Now, type, rtype);
            if (plan != null)
                return plan.id.ToString();
            return "";
        }
        private void Pageinit()
        {
            if (Request["PlanId"] != null && Request["PlanId"] != "")
            {
                WX.Model.Plan.MODEL planmodel = WX.Request.rPlan;
                planmodel.PlanState.value = 1;
                planmodel.Update();
            }
            userid = WX.Main.CurUser.UserID;
            WX.Main.CurUser.LoadDutyDetailUser();
            if (WX.Main.CurUser.DutyDetailUser.GradeID.ToInt32() >= 30)//领导管理层
            {
                rtype = "3";
                deptuserid = WX.Main.CurUser.UserID;
                Div3.InnerHtml = "<img alt=\"个人计划\" src=\"/Images/UserPlan.gif\" />公司本周计划";
                Div4.InnerHtml = "<img alt=\"部门计划\" src=\"/Images/DeptPlan.gif\" />公司本月计划";
            }
            else
            {
                string sSql="select ID from TE_DutyDetail where DepartentID=" + WX.Model.User.GetCache(userid).DepartmentID.ToString() + " and DutyCatagoryID=1 and GradeID<30";
                WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel(sSql);
                if (dd != null)
                {
                    WX.Model.User.MODEL user = WX.Model.User.GetModel("select * from TU_Users where DutyId=" + dd.ID.ToString());
                    if (user != null)
                        deptuserid = user.UserID.ToString();
                }
            }
            WX.Model.Plan.MODEL planday = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, DateTime.Now, 1, 1);
            dayedit.Text = planday != null ?(planday.PlanState.ToInt32()==0? "<a href='?PlanId=" + planday.id.ToString() + "'>提交审核</a>&nbsp;&nbsp;":"") +(planday.PlanState.ToInt32()<1?"<a href='Plan_EditPlan.aspx?PlanId=" + planday.id.ToString() + "'>编辑</a>":""): "<a href='Plan_EditPlan.aspx?type=1'>创建</a>";
            WX.Model.Plan.MODEL planweek = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, DateTime.Now, 2, 1);
            weekedit.Text = planweek != null ? (planweek.PlanState.ToInt32() == 0 ? "<a href='?PlanId=" + planweek.id.ToString() + "'>提交审核</a>&nbsp;&nbsp;" : "") + (planweek.PlanState.ToInt32() <1 ? "<a href='Plan_EditPlan.aspx?PlanId=" + planweek.id.ToString() + "'>编辑</a>":"") : "<a href='Plan_EditPlan.aspx?type=2'>创建</a>";
            WX.Model.Plan.MODEL planmonth = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, DateTime.Now, 3, 1);
            monthedit.Text = planmonth != null ? (planmonth.PlanState.ToInt32() == 0 ? "<a href='?PlanId=" + planmonth.id.ToString() + "'>提交审核</a>&nbsp;&nbsp;" : "") + (planmonth.PlanState.ToInt32() <1? "<a href='Plan_EditPlan.aspx?PlanId=" + planmonth.id.ToString() + "'>编辑</a>":"") : "<a href='Plan_EditPlan.aspx?type=3'>创建</a>";
        }
    }
}