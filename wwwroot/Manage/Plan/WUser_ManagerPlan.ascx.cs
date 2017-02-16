using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class WUser_ManagerPlan : System.Web.UI.UserControl
    {
        public string typestr = "1";
        public string rTypeName = "DeptId", UserID = "", Stime = DateTime.Now.ToString("yyyy-MM-dd"), rtype = "1";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Main.CurUser.LoadUserModel(false);
                UserID = WX.Main.CurUser.UserModel.DepartmentID.ToString();
                if (Request["type"] != null && Request["type"] != "")
                {
                    typestr = Request["type"];
                }
                string datetimestr = DateTime.Now.ToString("yyy-MM-dd");
                if (Request["date"] != null && Request["date"] != "")
                {
                    datetimestr = Request["date"];
                }

                rtype = Request["rtype"];
                //string typename = typestr == "2" ? "当周" : (typestr == "3" ? "当月" : "当日");
                WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel("select * from TE_DutyDetail where Id=" + WX.Main.CurUser.UserModel.DutyId.ToString());
                WX.Model.DutyDetail.MODEL ddept = WX.Model.DutyDetail.GetModel("select ID from TE_DutyDetail where DepartentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + " and DutyCatagoryID=1 and GradeID<30");
                //System.Data.DataTable dtdept;

                System.Data.DataTable dt2;
                
                if (Request["dept"] != null && Request["dept"] != "")
                {
                    string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + Convert.ToInt32(Request["dept"])).ToColValueList(",", 0);
                    dt2 = ULCode.QDA.XSql.GetDataTable("select 'UserID' rTypeName,UserID,'" + datetimestr + "' Stime,'" + typestr + "' Type,1 rtype from TU_Users where State<40 and DepartmentID in(" + Request["dept"] + (ids != "" ? "," + ids : "") + ") order by Grade desc");
                    UserID = Request["dept"];
                }
                else if (rtype=="3")//领导管理层
                    dt2 = ULCode.QDA.XSql.GetDataTable("select 'DeptId' rTypeName,ID UserID,'" + datetimestr + "' Stime,'" + typestr + "' Type,2 rtype from TE_Departments order by ID asc");
                else//主管
                {
                    string ids = ULCode.QDA.XSql.GetXDataTable("select ID  from TE_Departments where ParentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString()).ToColValueList(",", 0);

                    dt2 = ULCode.QDA.XSql.GetDataTable("select 'UserID' rTypeName,UserID,'" + datetimestr + "' Stime,'" + typestr + "' Type,1 rtype from TU_Users where State<40 and DepartmentID in(" + WX.Main.CurUser.UserModel.DepartmentID.ToString() + (ids != "" ? "," + ids : "") + ") order by  Grade desc");
                }
                DataList1.DataSource = dt2;
                DataList1.DataBind();
            }
        }
    }
}