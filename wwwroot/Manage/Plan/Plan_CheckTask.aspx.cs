using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_CheckTask : System.Web.UI.Page
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
                litype.Text=plan.Type.ToString()=="3"?"月计划":(plan.Type.ToString()=="2"?"周计划":"日计划");
                liStarttime.Text = plan.Starttime.ToDateTime().ToString("yyyy-MM-dd");
                liStoptime.Text = plan.Stoptime.ToDateTime().ToString("yyyy-MM-dd");
                Button2.OnClientClick = "butsumit('" + Request["page"] + ".aspx');";
                BindTask();
            }
        }
        private void BindTask()
        {
            string sql = "select * from PLAN_Task where State=1 and PlanID=" + WX.Request.rPlanId + " order by Statetime";
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable(sql);
            Gv_duty.DataSource = dt;
            Gv_duty.DataBind();
            int iCount=4;
            int iAll=10;
            String r = String.Format("完成{0}个，总计{1}个",iCount,iAll);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string tidList = Request.Form["Checkbox1"];
            if (String.IsNullOrEmpty(tidList))
            {
                ULCode.Debug.Alert(this,"请选择中申请完成的任务！");
                return;
            }
            WX.Main.ExcuteUpdate("PLAN_Task", "State=2,Statetime=getdate(),Appraise='"+TextBox3.Text+"'", "id in(" + tidList + ")");
            this.BindTask();
            WX.Main.CloseDialog_In_EasyUIDialog(this, "审核完毕！");
        }
    }
}