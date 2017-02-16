using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_DeptSearch : System.Web.UI.Page
    {
        public string type = "3";
        public string rtype = "2";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["PlanId"] != null && Request["PlanId"] != "")
            {
                WX.Model.Plan.MODEL planmodel = WX.Request.rPlan;
                planmodel.PlanState.value = 1;
                planmodel.Update();
            }
            if (Request["date"] != null && Request["date"] != "")
            {
                DateTime dt = Convert.ToDateTime(Request["date"]);
                Hidmonth.Value = dt.Month.ToString();
                Hidday.Value = dt.Day.ToString();
            }
            else
            {
                DateTime dt = DateTime.Now;
                Hidmonth.Value = dt.Month.ToString();
                Hidday.Value = dt.Day.ToString();
            }
            if (Request["rtype"] != null && Request["rtype"] != "")
            {
                rtype = Request["rtype"];
                if (rtype == "2")
                    this.MenuBar1.Key = "plan_dept";
                else if (rtype == "3")
                    this.MenuBar1.Key = "plan_cmp";
            }
            if (Request["type"] != null && Request["type"] != "")
            {
                type = Request["type"];
            }
            //WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel("select * from TE_DutyDetail where Id=" + WX.Main.CurUser.UserModel.DutyId.ToString());
            //if (dd != null && dd.GradeID.ToInt32()<30)//领导管理层
            //{
            //    MenuBar1.Key = "plan_cmp";
            //}
        }
        public string GetMyPlan(int type, string date)
        {
            return Request.Form["Hidmonth"];
        }
    }
}