using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_PlanDetail : System.Web.UI.Page
    {
        public double imgwidth = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.Model.Plan.MODEL plan = this.GetRequestedPlan();
                if (plan != null)
                {
                    this.LoadProgressBarState(plan);
                    this.gridviewBind(plan);
                }
                else
                {
                    div1.Visible = false;
                    Response.Write("<br/><center><font style='color:black;font-weight:bold;'>计划还没有创建！</font></center><br/>");
                }
            }
        }
        private WX.Model.Plan.MODEL GetRequestedPlan()
        {
            WX.Model.Plan.MODEL plan = null;
            if (Request["PlanId"] != null)
                plan = WX.Request.rPlan;
            else if (Request["type"] != null && Request["starttime"] != null && Request["DeptId"] != null && Request["DeptID"] != "")
            {
                int rtype = 1;
                if (Request["rtype"] != null && Request["rtype"] != "")
                {
                    rtype = Convert.ToInt32(Request["rtype"]);
                }
                plan = WX.Model.Plan.GetModel(WX.Request.rDeptId, Convert.ToDateTime(Request["starttime"]), Convert.ToInt32(Request["type"]), rtype);
            }
            else if (Request["type"] != null && Request["starttime"] != null && Request["UserID"] != null && Request["UserID"] != "")
            {
                int rtype = 1;
                if (Request["rtype"] != null && Request["rtype"] != "")
                {
                    rtype = Convert.ToInt32(Request["rtype"]);
                }
                plan = WX.Model.Plan.GetModel(WX.Request.rUserId, Convert.ToDateTime(Request["starttime"]), Convert.ToInt32(Request["type"]), rtype);
            }
            return plan;
        }
        private void LoadProgressBarState(WX.Model.Plan.MODEL plan)
        {
            //日计划
            if (plan.Type.ToInt32() == 1)
            {
                //隐然总结与评价
                this.Label3.Visible = false;
                this.Label4.Visible = false;
                //过了时间就完成
                if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days > 0)
                {
                    this.SetProgressBarState(5);
                }
                //如果计划不过期，状态为-1，则计划为未通过。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days <= 0
                    && plan.PlanState.ToInt32() == -1)
                {
                    this.Label2.Text = "未通过";
                    this.SetProgressBarState(2);
                }
                //如果计划不过期，状态为2，则计划为执行中。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days <= 0
                    && plan.PlanState.ToInt32() == 2)
                {
                    this.Label2.Text = "执行中";
                    this.SetProgressBarState(2);
                }
                //如果计划不过期，状态为1, 则计划为审核中。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days <= 0
                    && plan.PlanState.ToInt32() == 1)
                {
                    this.SetProgressBarState(1);
                }
                //如果计划不过期，状态为0，则计划为创建中。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days <= 0
                    && plan.PlanState.ToInt32() == 0)
                {
                    this.SetProgressBarState(0);
                }
            }
            //周计划与月计划
            else
            {
                //1.如果计划过了期，总结也有，评价也有，则计划为完成。
                if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days >= -2
                    && !plan.Summary.isEmpty
                    && !plan.Appraise.isEmpty)
                {
                    this.SetProgressBarState(5);
                }
                //2.如果计划过了期，总结也有，评价没有，则计划为待评价。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days >= -2
                    && !plan.Summary.isEmpty
                    && plan.Appraise.isEmpty)
                {
                    this.SetProgressBarState(4);
                }
                //3.如果计划过了期，总结也有，评价没有，则计划为待总结。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days >= -2
                    && plan.Summary.isEmpty
                    && plan.Appraise.isEmpty)
                {
                    this.SetProgressBarState(3);
                }
                //4.如果计划不过期，状态为-1,则计划为未通过。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days < 0
                    && plan.PlanState.ToInt32() == -1)
                {
                    this.Label2.Text = "未通过";
                    this.SetProgressBarState(2);
                }
                //5.如果计划不过期，状态为2，则计划为执行中。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days < 0
                    && plan.PlanState.ToInt32() == 2)
                {
                    this.Label2.Text = "执行中";
                    this.SetProgressBarState(2);
                }
                //6.如果计划不过期，状态为1, 则计划为审核中。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days < 0
                    && plan.PlanState.ToInt32() == 1)
                {
                    this.SetProgressBarState(1);
                }
                //7.如果计划不过期，状态为0，则计划为创建中。
                else if ((DateTime.Now - plan.Stoptime.ToDateTime()).Days < 0
                    && plan.PlanState.ToInt32() == 0)
                {
                    this.SetProgressBarState(0);
                }
               
            }
        }
        private void SetProgressBarState(int iState)
        {
            Label lbl=(Label)Label0.Parent.FindControl(String.Format("Label{0}",iState));
            lbl.Font.Bold = true;
            lbl.ForeColor = System.Drawing.Color.Goldenrod;
        }
        private void gridviewBind(WX.Model.Plan.MODEL plan)
        {
                lititle.Text = plan.Title.ToString();
                lititle.NavigateUrl = "#";
                imgwidth = (Convert.ToDouble(plan.Current.ToString()) / Convert.ToDouble(plan.Total.ToString())) * 100;
                lijindu.Text = " 进度：" + imgwidth.ToString("f0") + "%";
                licontent.Text = plan.Content.ToString().Replace("\n","<br/>");
                System.Data.DataTable dtApp = WX.Model.Appraise.GetList(plan.id.ToInt32());
                Apptable.Text = "";
                for (int i = 0; i < dtApp.Rows.Count; i++)
                {
                    Apptable.Text += "<li><a title=\"" + dtApp.Rows[i]["Content"] + "\">" + WX.CommonUtils.GetRealNameListByUserIdList(dtApp.Rows[i]["UserID"].ToString()) + "：" +dtApp.Rows[i]["Content"] + "&nbsp;</a><img src=\"/images/Appraise" + dtApp.Rows[i]["Appraise"] + ".jpg\" /><span>" + Convert.ToDateTime(dtApp.Rows[i]["AddTime"]).ToString("yyyy-MM-dd") + "</span></li>";
                }
                string now = DateTime.Now.ToString("yyyy-MM-dd");
                WX.Main.CurUser.LoadDutyDetailUser();
                if (plan.PlanState.ToInt32() == -1 && plan.UserID.ToString() == WX.Main.CurUser.UserID)
                    liPlanState.Text = "<b style='color:red;'>审核未通过</b>。原因：" + plan.Reason.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<请重新编辑然后提交><br/>";
                //if (plan.Type.ToString() == "2")
                    liSummary.Text = plan.Summary.ToString() == "" ? (DateTime.Now>=plan.Stoptime.ToDateTime().AddDays(-2) && plan.UserID.ToString() == WX.Main.CurUser.UserID ? "<b>总结：</b><a href='Plan_EditSummary.aspx?PlanId=" + plan.id.ToString() + "' style='color:blue;'>创建总结</a>" : "") : "<b>总结：</b>" + plan.Summary.ToString() + (plan.Appraise.ToString() == "" && plan.UserID.ToString() == WX.Main.CurUser.UserID ? "<a href='Plan_EditSummary.aspx?PlanId=" + plan.id.ToString() + "' style='color:blue;'>编辑</a>" : "");
                //else if (plan.Type.ToString() == "3")
                  //  liSummary.Text = plan.Summary.ToString() == "" ? (DateTime.Now >= plan.Stoptime.ToDateTime().AddDays(-2) && plan.UserID.ToString() == WX.Main.CurUser.UserID ? "<b>总结：</b><a href='Plan_EditSummary.aspx?PlanId=" + plan.id.ToString() + "' style='color:blue;'>创建总结</a>" : "") : "<b>总结：</b>" + plan.Summary.ToString() + (plan.Appraise.ToString() == "" && plan.UserID.ToString() == WX.Main.CurUser.UserID ? "<a href='Plan_EditSummary.aspx?PlanId=" + plan.id.ToString() + "' style='color:blue;'>编辑</a>" : "");
                liAppraise.Text = plan.Appraise.ToString() == "" ? 
                    (!plan.Summary.isEmpty && WX.Main.CurUser.DutyDetailUser.DutyCatagoryID.ToInt32() == 1 
                           ? "<br/><b>评价：</b><a href='Plan_EditAppraise.aspx?PlanId=" + plan.id.ToString() + "' style='color:blue;'>现在点评</a>" 
                             : "") 
                    : "<br/><b>评价：</b>" + plan.Appraise.ToString();
                string wherestr = " where PlanID=" + plan.id.ToString();
                System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from PLAN_Task" + wherestr + " order by id");
                Gv_duty.DataSource = dt;
                Gv_duty.DataBind();
                if (Request["estate"] != null)
                    Gv_duty.Columns[2].Visible = true;
                if (plan.Type.ToInt32() < 3)
                    divjd.Visible = false;
                if (plan.PlanState.ToInt32() <2)
                    Gv_duty.Columns[2].Visible = false;
        }
        private bool IsInSameWeek(DateTime dtmS, DateTime dtmE)
        {
            TimeSpan ts = dtmE - dtmS;
            int dbl = Convert.ToInt32(ts.TotalDays);
            int intDow = Convert.ToInt32(dtmE.DayOfWeek);
            if (intDow == 0) intDow = 7;
            if (dbl >= 7 || dbl > intDow) return false;
            else return true;
        }

        /// <summary>  
        /// 某日期是本月的第几周  
        /// </summary>  
        /// <param name="dtSel"></param>  
        /// <param name="sundayStart"></param>  
        /// <returns></returns>  
        private int WeekOfMonth(DateTime dtSel, bool sundayStart)
        {
            //如果要判断的日期为1号，则肯定是第一周了   
            if (dtSel.Day == 1) return 1;
            else
            {
                //得到本月第一天   
                DateTime dtStart = new DateTime(dtSel.Year, dtSel.Month, 1);
                //得到本月第一天是周几   
                int dayofweek = (int)dtStart.DayOfWeek;
                //如果不是以周日开始，需要重新计算一下dayofweek，详细风DayOfWeek枚举的定义   
                if (!sundayStart)
                {
                    dayofweek = dayofweek - 1;
                    if (dayofweek < 0) dayofweek = 7;
                }
                //得到本月的第一周一共有几天   
                int startWeekDays = 7 - dayofweek;
                //如果要判断的日期在第一周范围内，返回1   
                if (dtSel.Day <= startWeekDays) return 1;
                else
                {
                    int aday = dtSel.Day + 7 - startWeekDays;
                    return aday / 7 + (aday % 7 > 0 ? 1 : 0);
                }
            }
        }

        protected void Gv_List_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //1.验证用户权限
            //2.取得主键变量            
            string id = e.CommandArgument.ToString();

            if (e.CommandName == "editstate")
            {
                WX.Main.ExcuteUpdate("PLAN_Task", "State=1,Statetime=getdate()", "id=" + id);
                WX.Main.AddLog(WX.LogType.Default, String.Format("任务状态({0})修改成功！", id), "");

            }

            //5.（用户及业务对象）统计与状态

            //7.返回处理结果或返回其它页面。

            WX.Model.Plan.MODEL plan = this.GetRequestedPlan();
            if (plan != null)
            {
                this.LoadProgressBarState(plan);
                this.gridviewBind(plan);
            }
        }

        protected void Gv_duty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                switch (e.Row.Cells[1].Text)
                {
                    case "0": e.Row.Cells[1].Text = "<img src='/Manage/icon/cancel.png' alt='未完成'/>"; break;
                    case "1": e.Row.Cells[1].Text = "<img src='/Manage/icon/user.png' alt='审批中'/>"; e.Row.Cells[2].Visible = false; break;
                    case "2": e.Row.Cells[1].Text = "<img src='/Manage/icon/icon2_089.png' alt='已完成'/>"; e.Row.Cells[2].Visible = false; break;
                    default: e.Row.Cells[1].Text = "<img src='/Manage/icon/cancel.png' alt='未完成'/>"; break;
                }
            }
        }
    }
}