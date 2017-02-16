using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_EditPlan : System.Web.UI.Page
    {
        public string planid = "",mes="";
        private int rDay { get { return Convert.ToInt32(Request.QueryString["Day"]); } }
        private DateTime rDate { get { return DateTime.Now.AddDays(rDay); } }
        private int rRType { get { int iR = Convert.ToInt32(Request.QueryString["RType"]); return iR == 0 ? 1 : iR ; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //添加功能标题
                this.LoadTitleObject();
                this.LoadRBType();
                if (Request["PlanId"] != null && Request["PlanId"] != "")
                {
                    WX.Model.Plan.MODEL plan = WX.Request.rPlan;
                    
                    //plantype.Items.Add(new ListItem("月计划", "3"));
                    //plantype.Items.Add(new ListItem("周计划", "2"));
                    //plantype.Items.Add(new ListItem("日计划", "1"));
                    //plantype.Enabled = false;
                    //plantype.SelectedValue = plan.Type.ToString();
                    this.rbPlanType.SelectedValue = plan.Type.ToString();
                    this.rbPlanType.Enabled = false;

                    table2.Visible = true;

                    plantitle.Text = plan.Title.ToString();
                    planCurrent.Text = plan.Current.ToString();
                    planTotle.Text = plan.Total.ToString();
                    planContent.Text = plan.Content.ToString();
                    planid = plan.id.ToString();
                    if (plan.RangeType.ToInt32() > 1)
                    {
                        planrtype.Visible = true;
                        planrtype.SelectedValue = plan.RangeType.ToString();
                    }

                    MenuBar1.Key = plan.RangeType.ToString() == "2" ? "plan_dept" : (plan.RangeType.ToString() == "3" ? "plan_cmp" : MenuBar1.Key);
                    MenuBar1.CurIndex = 0;// plan.RangeType.ToInt32() > 1 ? 6 : MenuBar1.CurIndex;
                }
                else
                {
                    int rtype = this.rRType;
                    
                    //WX.Model.Plan.MODEL planmonth = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate, 3, rtype);
                    //if (planmonth == null)
                    //    plantype.Items.Add(new ListItem("月计划", "3"));
                    //WX.Model.Plan.MODEL planweek = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate, 2, rtype);
                    //if (planweek == null)
                    //    plantype.Items.Add(new ListItem("周计划", "2"));
                    //if (rtype == 1)
                    //{
                    //    WX.Model.Plan.MODEL planday = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate, 1, rtype);
                    //    if (planday == null)
                    //        plantype.Items.Add(new ListItem("日计划", "1"));
                    //}
                    if (this.rbPlanType.SelectedIndex == -1)
                    {
                        Button1.Enabled = false;
                        //Button1.Text = (rtype == 1 ? "今天的计划已经创建，请明天再来" : "本周计划已经创建，请下周再来");
                    }
                    else if (Request["type"] != null && Request["type"] != "" && this.rbPlanType.Items.FindByValue(Request["type"]) != null)
                    {
                        this.rbPlanType.SelectedValue = Request["type"];
                    }
                    if (Request["rtype"] != null && Request["rtype"] != "")
                    {
                        planrtype.Visible = true;
                        planrtype.SelectedValue = Request["rtype"];
                        MenuBar1.Key = Request["rtype"] == "2" ? "plan_dept" : "plan_cmp";
                        MenuBar1.CurIndex = 6;
                    }
                    //mes = "settitle()";
                }
            }
        }
        private void LoadTitleObject()
        {
            string titleMode = Convert.ToInt32(Request.QueryString["Plan"]) > 0 ? "修改" : "创建";
            string titleObject = null;
            switch (Convert.ToInt32(Request.QueryString["rtype"]))
            {
                case 0:
                case 1: titleObject = "个人"; break;
                case 2: titleObject = "部门"; break;
                case 3: titleObject = "公司"; break;
            }
            this.liTitle.Text = String.Format("{0}{1}计划", titleMode, titleObject);
        }
        private void LoadRBType()
        {
            int rtype = this.rRType;

            WX.Model.Plan.MODEL planmonth = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate, 3, rtype);
            if (true)
            {
                ListItem li = new ListItem("月计划", "3");
                li.Enabled = (planmonth == null);
                if (!li.Enabled) li.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                this.rbPlanType.Items.Add(li);
            }
            WX.Model.Plan.MODEL planweek = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate, 2, rtype);
            if (true)
            {
                ListItem li = new ListItem("周计划", "2");
                li.Enabled = (planweek == null);
                if (!li.Enabled) li.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                this.rbPlanType.Items.Add(li);
            }
            if (rtype == 1)
            {
                WX.Model.Plan.MODEL planday = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate, 1, rtype);
                if (true)
                {
                    ListItem li = new ListItem("日计划", "1");
                    li.Enabled = planday == null;
                    if (!li.Enabled) li.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    this.rbPlanType.Items.Add(li);
                }
            }
            WX.Model.Plan.MODEL planmonth1 = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate.AddMonths(1), 3, rtype);
            if (true)
            {
                ListItem li = new ListItem("下月计划", "6");
                li.Enabled = planmonth1 == null;
                if (!li.Enabled) li.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                this.rbPlanType.Items.Add(li);
            }
            WX.Model.Plan.MODEL planweek1 = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate.AddDays(7), 2, rtype);
            if (true)
            {
                ListItem li = new ListItem("下周计划", "5");
                li.Enabled = planweek1 == null;
                if (!li.Enabled) li.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                this.rbPlanType.Items.Add(li);
            }
            if (rtype == 1)
            {
                WX.Model.Plan.MODEL planday1 = WX.Model.Plan.GetModel(WX.Main.CurUser.UserID, this.rDate.AddDays(1), 1, rtype);
                if (true)
                {
                    ListItem li = new ListItem("明天计划", "4");
                    li.Enabled = planday1 == null && rtype == 1;
                    if (!li.Enabled) li.Attributes.CssStyle.Add(HtmlTextWriterStyle.TextDecoration, "line-through");
                    this.rbPlanType.Items.Add(li);
                }
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
                    return WX.Main.CurUser.MyDepartMent.Name.ToString();
                if (Request["rtype"] == "3")
                    return "公司";
            }
            return WX.Main.CurUser.MyDepartMent.Name.ToString() + WX.Main.CurUser.UserModel.RealName.ToString();
        }
        public int getweetno()
        {
            int no = 1;
            DateTime dt = Convert.ToDateTime(rDate.ToString("yyyy-MM-dd"));
            DateTime date01 = Convert.ToDateTime(dt.ToString("yyyy-MM-01"));
            if (Convert.ToInt32(date01.DayOfWeek) > 1)
            {
                date01 = date01.AddDays(7 - Convert.ToInt32(date01.DayOfWeek));
                if (dt <= date01)
                {
                    return 1;
                }
            }
            for (int i = 1; i < 6; i++)
            {
                no = i;
                if (dt > date01 && dt <= date01.AddDays(7))
                {
                    break;
                }
                date01 = date01.AddDays(7);
            }
            return no;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //获取用户变量
            string title = this.plantitle.Text;
            string total = this.planTotle.Text;
            string current = this.planCurrent.Text;
            string content = this.planContent.Text;
            //验证
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(content))
            {
                ULCode.Debug.Alert(this,"标题与内容不能为空！");
                return;
            }
            if (!ULCode.Validation.IsNumber(total) || !ULCode.Validation.IsNumber(current))
            {
                ULCode.Debug.Alert(this,"预期数量与完成数量必须为数字");
                return;
            }            
            //操作
            WX.Model.Plan.MODEL plan = null;
            if (Request["PlanId"] != null && Request["PlanId"] != "")
                plan = WX.Request.rPlan;
            else
                plan = WX.Model.Plan.NewDataModel();
            plan.Title.value = plantitle.Text;
            plan.Total.value = planTotle.Text;
            plan.Current.value = planCurrent.Text;
            plan.Content.value = planContent.Text;
            plan.PlanState.value = 0;
            if (Request["PlanId"] != null && Request["PlanId"] != "")
            {
                plan.Update();
                ULCode.Debug.Alert(this, "提交成功！", (plan.RangeType.ToInt32() == 1 ? "Plan_MyPlan.aspx" : "Plan_DeptPlan.aspx?type="+plan.Type.ToString()));
            }
            else
            {
                WX.Main.CurUser.LoadUserModel(false);
                plan.UserID.value = WX.Main.CurUser.UserModel.UserID.ToString();
                plan.DepartmentID.value = WX.Main.CurUser.UserModel.DepartmentID.ToString();
                switch (this.rbPlanType.SelectedValue)
                {
                    case "6": plan.Starttime.value = rDate.AddMonths(1).ToString("yyyy-MM-01"); plan.Stoptime.value = DateTime.Parse(plan.Starttime.ToString()).AddDays(DateTime.DaysInMonth(rDate.AddMonths(1).Year, rDate.AddMonths(1).Month) - 1); break;
                    case "5": DateTime now = rDate.AddDays(7); now = now.AddDays(-(Convert.ToInt32(now.DayOfWeek) > 0 ? Convert.ToInt32(now.DayOfWeek) : 7)); plan.Starttime.value = now.ToString("yyyy-MM-dd"); plan.Stoptime.value = now.AddDays(6).ToString("yyyy-MM-dd"); break;
                    case "4": plan.Starttime.value = rDate.AddDays(1).ToString("yyyy-MM-dd"); plan.Stoptime.value = plan.Starttime.value; break;
                    case "3": plan.Starttime.value = rDate.ToString("yyyy-MM-01"); plan.Stoptime.value = DateTime.Parse(plan.Starttime.ToString()).AddDays(DateTime.DaysInMonth(rDate.Year, rDate.Month) - 1); break;
                    case "2": DateTime now1 = rDate; now1 = now1.AddDays(-(Convert.ToInt32(now1.DayOfWeek) > 0 ? Convert.ToInt32(now1.DayOfWeek) : 7)); plan.Starttime.value = now1.ToString("yyyy-MM-dd"); plan.Stoptime.value = now1.AddDays(6).ToString("yyyy-MM-dd"); break;
                    case "1": plan.Starttime.value = rDate.ToString("yyyy-MM-dd"); plan.Stoptime.value = plan.Starttime.value; break;
                    default: break;
                }
                plan.Type.value = this.rbPlanType.SelectedValue;
                if (plan.Type.ToInt32() > 3)
                {
                    plan.Type.set(plan.Type.ToInt32() - 3);
                }
                if (Request["rtype"] != null && Request["rtype"] != "")
                {
                    plan.RangeType.value = planrtype.SelectedValue;
                }
                else
                {
                    plan.RangeType.value = 1;
                }
                int id = plan.Insert(true);
                ULCode.Debug.Alert(this, "计划提交成功！请添加计划任务,完成后在[我的计划]默认页提交审核！", "Plan_EditPlan.aspx?PlanId=" + id);
            }
        }

        protected void rbPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(rbPlanType.SelectedValue))
            {
                case 1: this.plantitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月dd日}", this.GetTitleTemp(), rDate, WX.Date.Season.GetSeasonFromMonth(rDate.Month)); break;
                case 2: this.plantitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM}-{3}周", this.GetTitleTemp(), rDate, WX.Date.Season.GetSeasonFromMonth(rDate.Month), WX.Date.Week.GetWeekOfYear(rDate)); break;
                case 3: this.plantitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月}", this.GetTitleTemp(), rDate, WX.Date.Season.GetSeasonFromMonth(rDate.Month)); break;
                case 4: this.plantitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月dd日}", this.GetTitleTemp(), rDate.AddDays(1), WX.Date.Season.GetSeasonFromMonth(rDate.AddDays(1).Month)); break;
                case 5: this.plantitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM}-{3}周", this.GetTitleTemp(), rDate.AddDays(7), WX.Date.Season.GetSeasonFromMonth(rDate.AddDays(1).Month), WX.Date.Week.GetWeekOfYear(rDate.AddDays(7))); break;
                case 6: this.plantitle.Text = String.Format("{0} {1:yyyy}年Q{2}-{1:MM月}", this.GetTitleTemp(), rDate.AddMonths(1), WX.Date.Season.GetSeasonFromMonth(rDate.AddDays(1).Month)); break;
            }
            this.Button1.Enabled = this.rbPlanType.SelectedIndex != -1;
        }

    }
}