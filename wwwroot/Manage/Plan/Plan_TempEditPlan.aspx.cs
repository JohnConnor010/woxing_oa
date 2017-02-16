using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_TempEditPlan : System.Web.UI.Page
    {
        public double imgwidth = 0;
        private int rDay { get { return Convert.ToInt32(Request.QueryString["Day"]); } }
        private DateTime rDate { get { return Convert.ToDateTime(Request["starttime"]).AddDays(rDay); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
        }
        private void PageInit()
        {
            Image1.ImageUrl = "/images/rtype" + Request["rtype"] + ".png";
            Image2.ImageUrl = "/images/type" + Request["type"] + ".png";
            if (Request["rtype"] == "3")
            {
                RealName.Text = Image1.ToolTip = "公司";
            }
            else if (Request["DeptId"] != null && Request["DeptId"] != "")
            {
                RealName.Text = WX.CommonUtils.GetDeptNameListByDeptIdList(Request["DeptId"]).Trim();
                Image1.ToolTip = "部门";
            }
            else
            {
                Image1.ToolTip = "个人";
                RealName.Text = WX.CommonUtils.GetRealNameListByUserIdList(WX.Request.rUserId);
            }
            if (Request["type"] != null)
            {
                switch (Request["type"])
                {
                    case "1": PlanType.Text = Image2.ToolTip = "日"; this.liDatetime.Text = String.Format("{0:yyyy年MM月dd日 dddd}", Convert.ToDateTime(Request["starttime"])); Button1.CssClass = rDate.Date >= DateTime.Now.AddDays(-1).Date ? Button1.CssClass : "button0"; litip.Text = (rDate.Date >= DateTime.Now.AddDays(-1).Date ? "请提交日计划。" : "日计划已过期！！"); litip2.Text = (rDate.Day >= DateTime.Now.Day ? "请提交日总结。" : "日总结已过期！！"); break;
                    case "2": PlanType.Text = Image2.ToolTip = "周"; this.liDatetime.Text = new WX.Model.Plan.Day(Convert.ToDateTime(Request["starttime"])).ToWeekDayStr(); Button1.CssClass = WX.Date.Week.GetWeekOfYear(rDate) >= WX.Date.Week.GetWeekOfYear(DateTime.Now) ? Button1.CssClass : "button0"; litip.Text = "周计划-请在每周周一前提交。"; litip2.Text = "周总结-请在每周周五后提交。"; break;
                    case "3": PlanType.Text = Image2.ToolTip = "月"; this.liDatetime.Text = String.Format("{0:yyyy年MM月}", Convert.ToDateTime(Request["starttime"])); Button1.CssClass = rDate.Month >= DateTime.Now.Month ? Button1.CssClass : "button0"; litip.Text = "月计划-请在27日-3日前提交月计划。"; litip2.Text = "月总结-请在27日后提交月总结。"; break;
                    case "4": PlanType.Text = Image2.ToolTip = "年"; this.liDatetime.Text = String.Format("{0:yyyy年}", Convert.ToDateTime(Request["starttime"])); litip.Text = "年计划-请提交本年计划。"; litip2.Text = "年总结-请提交本年总结。"; break;
                    //case "5": PlanType.Text = "下周"; break;
                    //case "6": PlanType.Text = "下月"; break;
                    default: break;
                }
                Image2.ToolTip += "计划";
            }
            WX.Model.Plan.MODEL plan = this.GetRequestedPlan();
            int plantype;

            if (plan != null)
            {
                // radioType.Enabled = false;
                hiplanid.Value = plan.id.ToString();
                txtTotal.Text = plan.Total.ToString();
                txtCurrent.Text = plan.Current.ToString();
                txtSummary.Text = plan.Summary.ToString();
                txtContent.Text = plan.Content.ToString();
                txtTitle.Text = plan.Title.ToString();
                //radioType.SelectedValue = plan.Type.ToString();
                plantype = plan.Type.ToInt32();
                DateTime nowdt = DateTime.Now;
                if (plan.Type.ToInt32() == 3)
                {
                    txtTitle.ReadOnly = txtTotal.ReadOnly = txtContent.ReadOnly = nowdt.Day > 4;
                    txtSummary.ReadOnly = nowdt.Day < 26;
                }
                if (plan.Type.ToInt32() == 2)
                {
                    txtTitle.ReadOnly = txtTotal.ReadOnly = txtContent.ReadOnly = Convert.ToInt32(nowdt.DayOfWeek.ToString("d")) >= 2;
                    txtSummary.ReadOnly = Convert.ToInt32(nowdt.DayOfWeek.ToString("d")) <= 4;
                }
                if (txtContent.ReadOnly && txtSummary.ReadOnly)
                    Button1.CssClass = "button0";

                AppraiseBind(plan.id.ToInt32());
            }
            else
            {
                plantype = Convert.ToInt32(Request["type"]);
                //radioType.SelectedValue = plantype.ToString();
               // txtSummary.ReadOnly = true;
                switch (plantype)
                {
                    case 1: this.txtTitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月dd日}", this.GetTitleTemp(), rDate, WX.Date.Season.GetSeasonFromMonth(rDate.Month)); break;
                    case 2: this.txtTitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM}-{3}周", this.GetTitleTemp(), rDate, WX.Date.Season.GetSeasonFromMonth(rDate.Month), WX.Date.Week.GetWeekOfYear(rDate)); break;
                    case 3: this.txtTitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月}", this.GetTitleTemp(), rDate, WX.Date.Season.GetSeasonFromMonth(rDate.Month)); break;
                    //case 4: this.txtTitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月dd日}", this.GetTitleTemp(), rDate.AddDays(1), WX.Date.Season.GetSeasonFromMonth(rDate.AddDays(1).Month)); break;
                    //case 5: this.txtTitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM}-{3}周", this.GetTitleTemp(), rDate.AddDays(7), WX.Date.Season.GetSeasonFromMonth(rDate.AddDays(1).Month), WX.Date.Week.GetWeekOfYear(rDate.AddDays(7))); break;
                    //case 6: this.txtTitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月}", this.GetTitleTemp(), rDate.AddMonths(1), WX.Date.Season.GetSeasonFromMonth(rDate.AddDays(1).Month)); break;
                }
                appraisal.Visible = false;
            }
            //Button1.CssClass = "button1"; txtContent.ReadOnly = txtSummary.ReadOnly = false;
            if (Request["estate"] == null || Request["estate"] == "")
                Button1.CssClass = "button0";
            else
                appraisal.Visible = false;

           // Button1.Enabled = Button1.CssClass == "button1";
        }
        private void AppraiseBind(int PlanID)
        {
            System.Data.DataTable dt = WX.Model.Appraise.GetList(PlanID);
            Apptable.Text = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Apptable.Text += "<li><a title=\"" + dt.Rows[i]["Content"] + "\">" + WX.CommonUtils.GetRealNameListByUserIdList(dt.Rows[i]["UserID"].ToString()) + "：" + (dt.Rows[i]["Content"].ToString().Length > 5 ? dt.Rows[i]["Content"].ToString().Substring(0, 5) : dt.Rows[i]["Content"]) + "&nbsp;</a><img src=\"/images/Appraise" + dt.Rows[i]["Appraise"] + ".jpg\" /><span>" + Convert.ToDateTime(dt.Rows[i]["AddTime"]).ToString("yyyy-MM-dd") + "</span></li>";
            }
        }
        public string GetTitleTemp()//1部门，2名字，3部门+名字
        {
            WX.Main.CurUser.LoadMyDepartment();
            if (Request["PlanId"] != null && Request["PlanId"] != "")
            {
                WX.Model.Plan.MODEL plan = WX.Request.rPlan;
                if (plan != null && plan.RangeType.ToInt32() == 2)
                    return WX.Main.CurUser.MyDepartMent.Name.ToString();
                if (plan != null && plan.RangeType.ToInt32() == 3)
                    return "公司";
            }
            else if (Request["rtype"] != null && Request["rtype"] != "")
            {
                if (Request["rtype"] == "2")
                    return WX.CommonUtils.GetDeptNameListByDeptIdList(Request["DeptId"]).Trim();//WX.Main.CurUser.MyDepartMent.Name.ToString();
                if (Request["rtype"] == "3")
                    return "公司";
            }
            if (Request["UserID"] != "")
            {
                WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel(Request["UserID"]);
                return WX.CommonUtils.GetDeptNameListByDeptIdList(usermodel.DepartmentID.ToString()).Trim() + usermodel.RealName.ToString();
            }
            return WX.Main.CurUser.MyDepartMent.Name.ToString() + WX.Main.CurUser.UserModel.RealName.ToString();
        }
        private WX.Model.Plan.MODEL GetRequestedPlan()
        {
            WX.Model.Plan.MODEL plan = null;
            if (hiplanid.Value != "")
                plan = WX.Model.Plan.GetModel(Convert.ToInt32(hiplanid.Value));
            else if (Request["PlanId"] != null)
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (hiplanid.Value != "")
            {
                WX.Model.Plan.MODEL plan = this.GetRequestedPlan();
                try
                {
                    plan.Total.value = txtTotal.Text.Trim() == "" ? 0 : Convert.ToInt32(txtTotal.Text);
                }
                catch { plan.Total.value = 0; } 
                try
                {
                    plan.Current.value = txtCurrent.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCurrent.Text);
                }
                catch { plan.Current.value = 0; }
                plan.Title.value = txtTitle.Text.Trim();
                plan.Content.value = txtContent.Text.Trim();
                plan.Summary.value = txtSummary.Text.Trim();
                plan.Update();
                WX.Main.AddLog(WX.LogType.Default, "修改计划！", String.Format("{0}", plan.Title.ToString()));
            }
            else
            {
                WX.Model.Plan.MODEL plan = WX.Model.Plan.NewDataModel(); 
                try
                {
                    plan.Total.value = txtTotal.Text.Trim() == "" ? 0 : Convert.ToInt32(txtTotal.Text);
                }
                catch { plan.Total.value = 0; }
                try
                {
                    plan.Current.value = txtCurrent.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCurrent.Text);
                }
                catch { plan.Current.value = 0; }
                plan.Title.value = txtTitle.Text.Trim();
                plan.Content.value = txtContent.Text.Trim();
                plan.Summary.value = txtSummary.Text.Trim();
                if (Request["UserID"] != null && Request["UserID"] != "")
                {
                    WX.Model.User.MODEL user = WX.Request.rUser;
                    plan.UserID.value = user.UserID.value;
                    plan.DepartmentID.value = user.DepartmentID.value;
                }
                else
                {
                    plan.UserID.value = WX.Main.CurUser.UserID;
                    plan.DepartmentID.value = Request["DeptId"];
                }
                plan.Type.value = Request["type"];
                plan.RangeType.value = Request["rtype"] != null && Request["rtype"] != "" ? Request["rtype"] : "1";
                switch (Request["type"])
                {
                    //case "6": plan.Starttime.value = rDate.AddMonths(1).ToString("yyyy-MM-01"); plan.Stoptime.value = DateTime.Parse(plan.Starttime.ToString()).AddDays(DateTime.DaysInMonth(rDate.AddMonths(1).Year, rDate.AddMonths(1).Month) - 1); break;
                    //case "5": DateTime now = rDate.AddDays(7); now = now.AddDays(-(Convert.ToInt32(now.DayOfWeek) > 0 ? Convert.ToInt32(now.DayOfWeek) : 7)); plan.Starttime.value = now.ToString("yyyy-MM-dd"); plan.Stoptime.value = now.AddDays(6).ToString("yyyy-MM-dd"); break;
                    //case "4": plan.Starttime.value = rDate.AddDays(1).ToString("yyyy-MM-dd"); plan.Stoptime.value = plan.Starttime.value; break;
                    case "3": plan.Starttime.value = rDate.ToString("yyyy-MM-01"); plan.Stoptime.value = DateTime.Parse(plan.Starttime.ToString()).AddDays(DateTime.DaysInMonth(rDate.Year, rDate.Month) - 1); break;
                    case "2": DateTime now1 = rDate; now1 = now1.AddDays(-(Convert.ToInt32(now1.DayOfWeek) > 0 ? Convert.ToInt32(now1.DayOfWeek) : 7)); plan.Starttime.value = now1.ToString("yyyy-MM-dd"); plan.Stoptime.value = now1.AddDays(6).ToString("yyyy-MM-dd"); break;
                    case "1": plan.Starttime.value = rDate.ToString("yyyy-MM-dd"); plan.Stoptime.value = plan.Starttime.value; break;
                    default: break;
                }
                if (plan.Type.ToInt32() > 3)
                {
                    plan.Type.set(plan.Type.ToInt32() - 3);
                }
                int id = plan.Insert(true);
                hiplanid.Value = id.ToString(); ;
                WX.Main.AddLog(WX.LogType.Default, "创建计划！", String.Format("{0}", plan.Title.ToString()));
            }
            ULCode.Debug.Alert(this, "保存成功！");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (hiplanid.Value != "")
            {
                WX.Model.Appraise.MODEL appraise = WX.Model.Appraise.NewDataModel();
                appraise.PlanID.value = hiplanid.Value;
                appraise.Appraise.value = Request.Form["pingjia"];
                appraise.Content.value = txtAppraiseContent.Text;
                appraise.UserID.value = WX.Main.CurUser.UserID;
                appraise.Addtime.value = DateTime.Now;
                appraise.Insert();
                AppraiseBind(Convert.ToInt32(hiplanid.Value));
            }
        }
    }
}