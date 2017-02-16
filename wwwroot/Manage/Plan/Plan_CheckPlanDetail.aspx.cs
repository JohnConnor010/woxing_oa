using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_CheckPlanDetail : System.Web.UI.Page
    {
        public string mes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Model.Plan.MODEL plan = WX.Request.rPlan;
                lititle.Text = plan.Title.ToString();
                lirealname.Text = WX.CommonUtils.GetRealNameListByUserIdList(plan.UserID.ToString());
                licurr.Text = plan.Current.ToString();
                litotal.Text = plan.Total.ToString();
                licontent.Text = plan.Content.ToString();
                litype.Text = plan.Type.ToString() == "3" ? "月计划" : (plan.Type.ToString() == "2" ? "周计划" : "日计划");
                liStarttime.Text = plan.Starttime.ToDateTime().ToString("yyyy-MM-dd");
                liStoptime.Text = plan.Stoptime.ToDateTime().ToString("yyyy-MM-dd");
                BindTask();
            }
        }
        private void BindTask()
        {
                string sql = "select * from PLAN_Task where PlanID=" + WX.Request.rPlanId + " order by id";
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sql);
                Gv_duty.DataSource = dt;
                Gv_duty.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Main.ExcuteUpdate("PLAN_Plan", "PlanState=2,Reason='" + TextBox3.Text + "'", "id =" +WX.Request.rPlanId);
            WX.Main.CloseDialog_In_EasyUIDialog(this,"审核完毕！");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            WX.Main.ExcuteUpdate("PLAN_Plan", "PlanState=-1,Reason='" + TextBox3.Text + "'", "id =" + WX.Request.rPlanId);
            WX.Main.CloseDialog_In_EasyUIDialog(this, "审核完毕！");
        }
    }
}