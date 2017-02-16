using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_CheckPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel("select * from TE_DutyDetail where Id=" + WX.Main.CurUser.UserModel.DutyId.ToString());
                System.Data.DataTable dt2;
                if (Request["rtype"] == "3")//领导管理层
                {
                    dt2 = ULCode.QDA.XSql.GetDataTable("select pp.*,tu.RealName from PLAN_Plan  pp left join TU_Users tu on pp.UserID=tu.UserID where RangeType=1 and PlanState=1  order by RangeType desc");
                    MenuBar1.Key = "plan_cmp";
                }
                else//主管
                {
                    string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString()).ToColValueList(",", 0);
                    string ids2 = ULCode.QDA.XSql.GetXDataTable("select DepartentID  from TU_User_X_DutyDetail tu left join TE_DutyDetail te on te.ID=tu.DutyDetailID where tu.UserID='" + WX.Main.CurUser.UserID + "'").ToColValueList(",", 0);

                    dt2 = ULCode.QDA.XSql.GetDataTable("select pp.*,tu.RealName from PLAN_Plan  pp left join TU_Users tu on pp.UserID=tu.UserID where RangeType=1 and PlanState=1 and tu.DepartmentID  in(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + (ids != "" ? "," + ids : "") + (ids2 != "" ? "," + ids2 : "") + ") order by RangeType desc");
                }
                Gv_duty.DataSource = dt2;
                Gv_duty.DataBind();
                if (Gv_duty.Rows.Count > 0)
                {
                    Gv_duty.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Gv_duty.HeaderStyle.Height = Unit.Pixel(40);
                }
            }
        }
        public string GetTask(int PlanId)
        {
            return "sdf";
        }
    }

}