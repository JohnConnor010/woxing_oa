using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wwwroot.App_Ctrl;
using wwwroot.WXDataContext;

namespace wwwroot.Manage.MyManage
{
    public partial class DepartmentWeekPlan : System.Web.UI.Page
    {
        protected int currentWeekIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            currentWeekIndex = ConvertDateTime.GetWeekIndex(DateTime.Now);
            string departmentId = Request.QueryString["DepartmentID"];
            string weekIndex = Request.QueryString["WeekIndex"];
            using(WXOADataContext db = new WXOADataContext())
            {
                var entity = db.TE_Departments.FirstOrDefault(d => d.ID == int.Parse(departmentId));
                if(entity != null)
                {
                    this.ltlDepartmentName.Text = entity.Name;
                    this.ltlDateTime.Text = String.Format("第{0}周", weekIndex);                    
                }
                string dtStart = ConvertDateTime.GetWeekRange(int.Parse(weekIndex)).Split('～')[0];
                string dtEnd = ConvertDateTime.GetWeekRange(int.Parse(weekIndex)).Split('～')[1];
                DateTime startTime = Convert.ToDateTime(dtStart).AddDays(-1);
                DateTime stopTime = Convert.ToDateTime(dtEnd).AddDays(-1);
                var plan = db.PLAN_Plans.FirstOrDefault(pp => pp.DepartmentID == int.Parse(departmentId) && pp.Starttime == startTime && pp.Stoptime == stopTime && pp.Type == 2 && pp.RangeType == 2);
                if(plan != null)
                {
                    this.txtTitle.Text = plan.Title;
                    this.txtTotal.Text = plan.Total.ToString();
                    this.txtCurrent.Text = plan.Current.ToString();
                    this.txtContent.Value = plan.Content;
                    this.txtSummary.Value = plan.Summary;
                    var comments = db.PLAN_Appraises.Join(db.TU_Users, o => o.UserID, i => i.UserID, (o, i) => new
                        {
                            o.PlanID,
                            o.Appraise,
                            o.Content,
                            o.AddTime,
                            i.RealName
                        }).ToList().Where(a => a.PlanID == plan.id).Select(a => new
                        {
                            a.PlanID,
                            a.Appraise,
                            Content = new DepartmentMonthPlan().SplitString(a.Content, 4),
                            Content1 = a.Content,
                            AddTime = a.AddTime.Value.ToString("yyyy-MM-dd"),
                            a.RealName
                        });
                    this.Repeater1.DataSource = comments;
                    this.Repeater1.DataBind();
                    
                }
                var users = db.TU_Users.Where(u => u.DepartmentID == int.Parse(departmentId) && u.State != 40).Select(u => new
                    {
                        u.RealName,
                        u.UserID,
                        WeekFlag = String.Format("{0}周({1})",weekIndex,ConvertDateTime.GetWeekRange(int.Parse(weekIndex))),
                        title = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Title,
                        total = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Total,
                        current = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Current,
                        content = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Content,
                        summary = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Summary,
                        PlanID = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).id
                    });
                this.Repeater2.DataSource = users;
                this.Repeater2.DataBind();                
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater repeater = e.Item.FindControl("Repeater3") as Repeater;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(e.Item.DataItem);
                JToken token = JToken.Parse(json);
                int planId = token.Value<int>("PlanID");
                if(planId != 0)
                {
                    var splitContent = new DepartmentMonthPlan();
                    using(WXOADataContext db = new WXOADataContext())
                    {
                        var appraises = db.PLAN_Appraises.Join(db.TU_Users, o => o.UserID, i => i.UserID, (o, i) => new
                            {
                                o.PlanID,
                                o.Appraise,
                                o.Content,
                                o.AddTime,
                                i.RealName
                            }).ToList().Where(pa => pa.PlanID == planId).Select(pa => new
                            {
                                pa.PlanID,
                                pa.Appraise,
                                Content = new DepartmentMonthPlan().SplitString(pa.Content,4),
                                Content1 = pa.Content,
                                pa.RealName,
                                AddTime = pa.AddTime.Value.ToString("yyyy-MM-dd")
                            });
                        repeater.DataSource = appraises;
                        repeater.DataBind();
                    }
                }
            }
        }
    }
}