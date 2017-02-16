using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_Search : System.Web.UI.Page
    {
        public string userid = "";
        public string deptuserid = "";
        public string deptid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["UserId"] != null)
                    userid = WX.Request.rUserId;
                else
                    userid = WX.Main.CurUser.UserID;
               deptid = WX.Model.User.GetCache(userid).DepartmentID.ToString();
               string sSql = "select ID from TE_DutyDetail where DepartentID=" + deptid  + " and DutyCatagoryID=1 and GradeID<30";
               WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel(sSql);
               if (dd != null)
               {
                   sSql = "select * from TU_Users where DutyId=" + dd.ID.ToString();
                   WX.Model.User.MODEL user = WX.Model.User.GetModel(sSql);
                   if (user != null)
                       deptuserid = user.UserID.ToString();
               }
            }
        }
        public string GetMyPlan(int type, string date)
        {
            return Request.Form["Hidmonth"];
        }
    }
}