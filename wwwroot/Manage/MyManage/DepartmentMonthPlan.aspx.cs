using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vandermotten.Diagnostics;
using wwwroot.App_Ctrl;
using wwwroot.WXDataContext;

namespace wwwroot.Manage.MyManage
{
    public partial class DepartmentMonthPlan : System.Web.UI.Page
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
            string departmentId = Request.QueryString["DepartmentID"];
            string month = Request.QueryString["Month"];
            DateTime startTime = new DateTime(DateTime.Now.Year, int.Parse(month), 1);
            DateTime stopTime = new DateTime(DateTime.Now.Year,int.Parse(month),DateTime.DaysInMonth(DateTime.Now.Year, int.Parse(month)));
            using(WXOADataContext db = new WXOADataContext())
            {
                db.Log = new DebuggerWriter();
                var entity = db.TE_Departments.FirstOrDefault(d => d.ID == int.Parse(departmentId));
                if(entity != null)
                {
                    this.ltlDepartmentName.Text = entity.Name;
                    this.ltlMonthFlag.Text = String.Format("{0}年{1}月", DateTime.Now.Year, Request.QueryString["Month"].ToString());
                }
                var plan = db.PLAN_Plans.FirstOrDefault(p => p.DepartmentID == int.Parse(departmentId) && p.Type==3 && p.RangeType == 2 && p.Starttime == startTime && p.Stoptime == stopTime);
                if(plan != null)
                {
                    this.txtTitle.Value = plan.Title;
                    this.txtTotal.Value = plan.Total.ToString();
                    this.txtCurrent.Value = plan.Current.ToString();
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
                        Content = a.Content,
                        Content1 = SplitString(a.Content.ToString(),4),
                        AddTime = a.AddTime.Value.ToString("yyyy-MM-dd"),
                        a.RealName
                    });
                    this.Repeater2.DataSource = comments;
                    this.Repeater2.DataBind();
                }
                db.Log = new DebuggerWriter();
                var users = db.TU_Users.Where(u => u.DepartmentID == int.Parse(departmentId) && u.State != 40).Select(u => new
                {
                    u.RealName,
                    u.UserID,
                    MonthFlag = String.Format("{0}年{1}月", DateTime.Now.Year, Request.QueryString["Month"].ToString()),
                    title = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Title,
                    total = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Total,
                    current = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Current,
                    content = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Content,
                    summary = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).Summary,
                    PlanID = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Starttime == startTime && p.Stoptime == stopTime).id
                });
                this.Repeater3.DataSource = users;
                this.Repeater3.DataBind();
            }
        }
        public string SplitString(string content,int length)
        {
            if(string.IsNullOrEmpty(content))
            {
                return "";
            }
            else
            {
                if(content.Length > length)
                {
                    return content.Substring(0, length);
                }
                else
                {
                    return content;
                }
            }
        }

        protected void Repeater3_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater repeater = e.Item.FindControl("Repeater3") as Repeater;
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(e.Item.DataItem);
                JToken token = JToken.Parse(json);
                int planId = token.Value<int>("PlanID");
                if (planId != 0)
                {
                    using (WXOADataContext db = new WXOADataContext())
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
                            Content = SplitString(pa.Content, 4),
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