using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_DeptPlan : System.Web.UI.Page
    {
        public string typestr = "1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadNote();

                int rtype = Convert.ToInt32(Request["rtype"]);
                if (Request["dept"] != null)
                {
                    this.MenuBar1.Key = "plan_cmp_dept";
                    this.MenuBar1.Param1 = "{Q:dept}";
                }
                if (rtype == 2)
                    this.MenuBar1.Key = "plan_dept";
                else if (rtype == 3)
                    this.MenuBar1.Key = "plan_cmp";

                if (Request["type"] != null && Request["type"] != "")
                {
                    typestr = Request["type"];
                    MenuBar1.CurIndex = Convert.ToInt32(typestr) + 1;
                }
                if (Request["PlanId"] != null && Request["PlanId"] != "")
                {
                    WX.Model.Plan.MODEL planmodel = WX.Request.rPlan;
                    planmodel.PlanState.value = 1;
                    planmodel.Update();
                }
                
                // WX.Model.DutyDetail.MODEL dd = WX.Model.DutyDetail.GetModel("select * from TE_DutyDetail where Id=" + WX.Main.CurUser.UserModel.DutyId.ToString());
                //if (dd != null && dd.GradeID.ToInt32()<30)//领导管理层
                //{
                //    MenuBar1.Key = "plan_cmp";
                //}
            }
        }
        private void LoadNote()
        {
            int type=Convert.ToInt32(Request.QueryString["Type"]);
            int rType = Convert.ToInt32(Request.QueryString["rType"]);
            string note = null;
            if (rType == 0)
            {
                int dept = Convert.ToInt32(Request.QueryString["Dept"]);
                if (dept != 0)
                {
                    string deptName = WX.Model.Department.GetCache(dept).Name.ToString();
                    deptName = String.Format("<font color='red'>{0}</font>",deptName);
                    switch (type)
                    {
                        case 0:
                        case 1: note = "这里是"+deptName+"的所有人员的个人日计划！"; break;
                        case 2: note = "这里是"+deptName+"的周计划与所有的个人周计划！"; break;
                        case 3: note = "这里是"+deptName+"的月计划与所有的个人月计划！"; break;
                    }
                }
            }
            else if (rType == 1)
            {
                ;
            }
            else if (rType == 2)
            {
                switch (type)
                {
                    case 0:
                    case 1: note = "这里是本部门的所有人员的个人日计划！"; break;
                    case 2: note = "这里是本部门的周计划与所有的个人周计划！"; break;
                    case 3: note = "这里是本部门的月计划与所有的个人月计划！"; break;
                }
            }
            else if (rType == 3)
            {
                switch (type)
                {
                    case 0:
                    case 1: note = "这里是公司的所有人员的个人日计划！"; break;
                    case 2: note = "这里是公司周计划与所有部门的周计划！"; break;
                    case 3: note = "这里是公司月计划与所有部门的月计划！"; break;
                }
            }
            this.liNote.Text = note;
        }
    }
}