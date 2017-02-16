using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vandermotten.Diagnostics;
using wwwroot.App_Ctrl;
using wwwroot.WXDataContext;

namespace wwwroot.Manage.MyManage
{
    public partial class PlanView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            string userId = Request.QueryString["UserID"];
            string type = Request.QueryString["type"];
            switch(type)
            {
                case "1":
                    string dateTime = Request.QueryString["DateTime"];                
                    this.Calendar1.TodaysDate= DateTime.Parse(dateTime);
                    this.Image1.ImageUrl = "/images/rtype3.png";
                    this.Image2.ImageUrl = "/images/type1.png";
                    this.Image2.ToolTip = "日计划";
                    this.ltlPlanType.Text = "日计划";
                    using(WXOADataContext db = new WXOADataContext())
                    {
                        var entity = db.TU_Users.FirstOrDefault(U => U.UserID == Guid.Parse(userId));
                        if(entity != null)
                        {
                            this.ltlPersonName.Text = entity.RealName;
                            this.ltlDateTime.Text = dateTime;                        
                            var plan = db.PLAN_Plans.FirstOrDefault(p =>p.UserID == Guid.Parse(userId) && p.Starttime == DateTime.Parse(dateTime) && p.Type == 1 && p.RangeType == 1);
                            if(plan != null)
                            {
                                this.txtTitle.Value = plan.Title;
                                this.txtTotal.Value = plan.Total.ToString();
                                this.txtCurrent.Value = plan.Current.ToString();
                                this.txtContent.Value = plan.Content;
                                this.txtSummary.Value = plan.Summary;

                                var appraises = db.PLAN_Appraises.Join(db.TU_Users, o => o.UserID, i => i.UserID, (o, i) => new
                                {
                                    o.PlanID,
                                    o.Appraise,
                                    o.Content,
                                    o.AddTime,
                                    i.RealName
                                }).ToList().Where(pa => pa.PlanID == plan.id).Select(pa => new
                                {
                                    pa.PlanID,
                                    pa.Appraise,
                                    Content = new DepartmentMonthPlan().SplitString(pa.Content, 4),
                                    Content1 = pa.Content,
                                    pa.RealName,
                                    AddTime = pa.AddTime.Value.ToString("yyyy-MM-dd")
                                });
                                this.Repeater1.DataSource = appraises;
                                this.Repeater1.DataBind();
                            }
                        
                        }
                    }
                    break;
                case "3":
                    string year = Request.QueryString["Year"];
                    string month = Request.QueryString["Month"];
                    this.Image1.ImageUrl = "/images/rtype3.png";
                    this.Image2.ImageUrl = "/images/type3.png";
                    this.Image2.ToolTip = "月计划";
                    this.ltlPlanType.Text = "月计划";
                    using(WXOADataContext db = new WXOADataContext())
                    {
                        var entity = db.TU_Users.FirstOrDefault(U => U.UserID == Guid.Parse(userId));
                        if (entity != null)
                        {
                            this.ltlPersonName.Text = entity.RealName;
                            this.ltlDateTime.Text = String.Format("{0}年{1}月", year, month);
                            DateTime startTime = Convert.ToDateTime(String.Format("{0}-{1}-{2}", year, month, "01"));
                            DateTime stopTime = ConvertDateTime.GetLastDayOfMonth(startTime);
                            var plan = db.PLAN_Plans.FirstOrDefault(p => p.UserID == Guid.Parse(userId) && p.Starttime == startTime && p.Stoptime == stopTime);
                            if(plan != null)
                            {
                                this.txtTitle.Value = plan.Title;
                                this.txtTotal.Value = plan.Total.ToString();
                                this.txtCurrent.Value = plan.Current.ToString();
                                this.txtContent.Value = plan.Content;
                                this.txtSummary.Value = plan.Summary;
                                this.Calendar1.TodaysDate = plan.Addtime.Value;
                                var appraises = db.PLAN_Appraises.Join(db.TU_Users, o => o.UserID, i => i.UserID, (o, i) => new
                                {
                                    o.PlanID,
                                    o.Appraise,
                                    o.Content,
                                    o.AddTime,
                                    i.RealName
                                }).ToList().Where(pa => pa.PlanID == plan.id).Select(pa => new
                                {
                                    pa.PlanID,
                                    pa.Appraise,
                                    Content = new DepartmentMonthPlan().SplitString(pa.Content, 4),
                                    Content1 = pa.Content,
                                    pa.RealName,
                                    AddTime = pa.AddTime.Value.ToString("yyyy-MM-dd")
                                });
                                this.Repeater1.DataSource = appraises;
                                this.Repeater1.DataBind();
                            }
                        }
                    }
                    break;
            }
            
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DateTime dateTime in Calendar1.SelectedDates)
            {
                Response.Redirect(String.Format("PlanView.aspx?UserID={0}&DateTime={1}&type=1", Request.QueryString["UserID"], dateTime.ToString("yyyy-MM-dd")));
            }
        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            Response.Redirect(String.Format("PlanView.aspx?UserID={0}&Year={1}&Month={2}&type=3", Request.QueryString["UserID"], e.NewDate.Year, e.NewDate.Month));
        }
    }
}