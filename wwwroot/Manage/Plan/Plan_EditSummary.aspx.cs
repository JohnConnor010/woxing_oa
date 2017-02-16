using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_EditSummary : System.Web.UI.Page
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
                TextBox1.Text = plan.Summary.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Model.Plan.MODEL plan = WX.Request.rPlan;
            plan.Summary.value = TextBox1.Text.Trim();
            plan.Update();
            Response.Redirect("Plan_PlanDetail.aspx?PlanId="+plan.id.ToString());
        }
    }
}