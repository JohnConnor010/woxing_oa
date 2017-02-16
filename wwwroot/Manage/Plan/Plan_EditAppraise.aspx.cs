using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_EditAppraise : System.Web.UI.Page
    {
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
                lisummary.Text = plan.Summary.ToString();
                string wherestr = " where PlanID=" + plan.id.ToString();
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from PLAN_Task" + wherestr + " order by id");
                Gv_duty.DataSource = dt;
                Gv_duty.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Model.Plan.MODEL plan = WX.Request.rPlan;
            plan.Appraise.value = TextBox1.Text.Trim();
            plan.Update();
            Response.Redirect("Plan_PlanDetail.aspx?PlanId=" + plan.id.ToString());
        }
    }
}