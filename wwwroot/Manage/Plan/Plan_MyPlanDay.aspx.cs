using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_MyPlanDay : System.Web.UI.Page
    {
        public string userid = "", deptuserid = "", rtype = "2";
        protected void Page_Load(object sender, EventArgs e)
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
            }
            else
            {
                string sSql = "select ID from TE_DutyDetail where DepartentID=" + WX.Model.User.GetCache(userid).DepartmentID.ToString() + " and DutyCatagoryID=1 and GradeID<30";
                WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel(sSql);
                if (dd != null)
                {
                    WX.Model.User.MODEL user = WX.Model.User.GetModel("select * from TU_Users where DutyId=" + dd.ID.ToString());
                    if (user != null)
                        deptuserid = user.UserID.ToString();
                }
            }
        }
    }
}