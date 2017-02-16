using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_Task : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridviewBind();
            }
        }
        public void gridviewBind()
        {
            if (Request["PlanId"] == null || Request["PlanId"] == "")
            {
                Response.Write("<br/><center><b>任务不存在，请创建！</b></center><br/>");
                div1.Visible = false;
            }
            else
            {
                string wherestr = " where PlanID=" + WX.Request.rPlanId;
                DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from PLAN_Task" + wherestr + " order by id");
                Gv_duty.DataSource = dt;
                Gv_duty.DataBind();
                if (Request["methed"] == null)
                {
                    Gv_duty.Columns[3].Visible = false;
                    div1.Visible = false;
                }
                if (Request["estate"] == null)
                {
                    Gv_duty.Columns[2].Visible = false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //1.验证用户权限
            //2.取得主键变量            
            string id = e.CommandArgument.ToString();

            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (e.CommandName == "del")
            {
                WX.Main.ExecuteDelete("PLAN_Task", "id", id);
                WX.Main.AddLog(WX.LogType.Default, String.Format("删除任务({0})成功！", id), "");
            }
            else if (e.CommandName == "editstate")
            {
                WX.Main.ExcuteUpdate("PLAN_Task", "State=1,Statetime=getdate()", "id=" + id);
                WX.Main.AddLog(WX.LogType.Default, String.Format("任务状态({0})修改成功！", id), "");
            }
            
            //5.（用户及业务对象）统计与状态

            //7.返回处理结果或返回其它页面。

            gridviewBind();
        }

        protected void Gv_duty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[1].Text)
                {
                    case "0": e.Row.Cells[1].Text = "<img src='/Manage/icon/cancel.png' alt='未完成'/>"; break;
                    case "1": e.Row.Cells[1].Text = "<img src='/Manage/icon/user.png' alt='审批中'/>"; e.Row.Cells[3].Visible = false; break;
                    case "2": e.Row.Cells[1].Text = "<img src='/Manage/icon/icon2_089.png' alt='已完成'/>"; e.Row.Cells[3].Visible = false; break;
                    default: e.Row.Cells[1].Text = "<img src='/Manage/icon/cancel.png' alt='未完成'/>"; break;
                }
            }
        }
    }
}