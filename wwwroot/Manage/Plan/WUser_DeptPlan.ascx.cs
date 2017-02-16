using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class WUser_DeptPlan : System.Web.UI.UserControl
    {
        public string typestr = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["type"] != null && Request["type"] != "")
                {
                    typestr = Request["type"];
                }
                string datetimestr = DateTime.Now.ToString("yyy-MM-dd");
                if (Request["date"] != null && Request["date"] != "")
                {
                    datetimestr = Request["date"];
                }
                //string typename = typestr == "2" ? "当周" : (typestr == "3" ? "当月" : "当日");
                WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel("select * from TE_DutyDetail where Id=" + WX.Main.CurUser.UserModel.DutyId.ToString());
                WX.Model.DutyDetail.MODEL ddept = WX.Model.DutyDetail.GetModel("select ID from TE_DutyDetail where DepartentID=" +WX.Main.CurUser.UserModel.DepartmentID.ToString()+ " and DutyCatagoryID=1 and GradeID<30");
                //System.Data.DataTable dtdept;
                
                System.Data.DataTable dt2;
                string userPlanImage = "<img alt=个人计划 src=/Images/UserPlan.gif />";
                string deptPlanImage = "<img alt=部门计划 src=/Images/DeptPlan.gif />";
                if (Request["dept"] != null && Request["dept"] != "")
                {
                    string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" +Convert.ToInt32(Request["dept"])).ToColValueList(",", 0);

                    dt2 = ULCode.QDA.XSql.GetDataTable("select pp.id,pp.RangeType,pp.UserID,pp.PlanState,case RangeType when 2 then '" + deptPlanImage + "部门计划' else '" + userPlanImage + "'+tu.RealName end RealName,'" + datetimestr + "' Stime,'" + typestr + "' Type,RangeType rtype,3 newrtype from PLAN_Plan pp left join TU_Users tu on pp.UserID=tu.UserID where pp.DepartmentID in(" + Request["dept"] + (ids != "" ? "," + ids : "") + ") and pp.Type=" + typestr + " and datediff(" + (typestr == "2" ? "week" : (typestr == "3" ? "month" : "dd")) + ",pp.Starttime,'" + datetimestr + "')=0 order by tu.Grade desc");
                }
                else if (Request["rtype"]=="3")//领导管理层
                    dt2 = ULCode.QDA.XSql.GetDataTable("select pp.id,pp.RangeType,UserID,pp.PlanState,td.Name RealName,'" + datetimestr + "' Stime,'" + typestr + "' Type,RangeType rtype,3 newrtype from PLAN_Plan  pp left join TE_Departments td on pp.DepartmentID=td.ID where RangeType>1 and pp.Type=" + typestr + " and datediff(" + (typestr == "2" ? "week" : (typestr == "3" ? "month" : "dd")) + ",pp.Starttime,'" + datetimestr + "')=0 order by RangeType desc,pp.Addtime desc");
                else//主管
                {

                    string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString()).ToColValueList(",", 0);
                    dt2 = ULCode.QDA.XSql.GetDataTable("select pp.id,pp.RangeType,pp.UserID,pp.PlanState,case RangeType when 2 then '" + deptPlanImage + "部门计划' else '" + userPlanImage + "'+tu.RealName end RealName,'" + datetimestr + "' Stime,'" + typestr + "' Type,RangeType rtype,2 newrtype from PLAN_Plan pp left join TU_Users tu on pp.UserID=tu.UserID where pp.DepartmentID in(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + (ids != "" ? "," + ids : "") + ") and pp.Type=" + typestr + " and datediff(" + (typestr == "2" ? "week" : (typestr == "3" ? "month" : "dd")) + ",pp.Starttime,'" + datetimestr + "')=0 order by tu.Grade desc");
                }
                    DataList1.DataSource = dt2;
                DataList1.DataBind();
            }
        }
    }
}