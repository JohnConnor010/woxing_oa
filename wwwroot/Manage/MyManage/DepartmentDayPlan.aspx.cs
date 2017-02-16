using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wwwroot.WXDataContext;

namespace wwwroot.Manage.MyManage
{
    public partial class DepartmentDayPlan : System.Web.UI.Page
    {
        protected string image1Url = "<img id=\"Image1\" src=\"/images/rtype1.png\" title=\"个人\">";
        protected string image2Url = "<img id=\"Image2\" src=\"/images/type1.png\" title=\"日计划\">";
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
            if(!string.IsNullOrEmpty(departmentId))
            {
                using(WXOADataContext db = new WXOADataContext())
                {
                    this.ltlDepartmentName.Text = db.TE_Departments.FirstOrDefault(d => d.ID == int.Parse(departmentId)) == null ? "" : db.TE_Departments.FirstOrDefault(d => d.ID == int.Parse(departmentId)).Name;
                    var entity = db.PLAN_Plans.Where(pp => pp.Type == 1 && pp.RangeType == 2 && pp.DepartmentID == int.Parse(departmentId) && pp.Starttime == Convert.ToDateTime(Request.QueryString["Starttime"])).FirstOrDefault();
                    this.liDatetime.Text = String.Format("{0:yyyy-MM-dd dddd}", Convert.ToDateTime(Request.QueryString["Starttime"]));
                    if(entity != null)
                    {
                        this.txtTitle.Text = entity.Title;
                        this.txtTotal.Text = entity.Total.ToString();
                        this.txtCurrent.Text = entity.Current.ToString();
                        this.txtContent.Text = entity.Content;
                        this.txtSummary.Text = entity.Summary;
                        this.ltlComment.Text = db.PLAN_Appraises.FirstOrDefault(ap => ap.PlanID == entity.id) == null ? "" : db.PLAN_Appraises.FirstOrDefault(ap => ap.PlanID == entity.id).Content;
                    }
                    var users = db.TU_Users.Where(u => u.DepartmentID == int.Parse(departmentId) && u.State != 40).Select(u => new
                        {
                            u.RealName,
                            Title = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])).Title,
                            Total = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])).Total,
                            Current = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])).Current,
                            Content = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])).Content,
                            Summary = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])) == null ? "" : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])).Summary,
                            PlanID = db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])) == null ? 0 : db.PLAN_Plans.FirstOrDefault(p => p.UserID == u.UserID && p.Type == 1 && p.RangeType == 1 && p.Starttime == Convert.ToDateTime(Request.QueryString["StartTime"])).id,
                            
                        });
                    this.Repeater1.DataSource = users;
                    this.Repeater1.DataBind();
                    
                    
                    
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string addDateTime = Convert.ToDateTime(Request.QueryString["Starttime"]).ToString("yyyy-MM-dd");
                Repeater repeater = (Repeater)e.Item.FindControl("CommentRepeater");
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(e.Item.DataItem);
                JToken token = JToken.Parse(json);
                int planId = token["PlanID"].Value<int>();
                if(planId != 0)
                {
                    using(WXOADataContext db = new WXOADataContext())
                    {
                        var comments = db.PLAN_Appraises.Join(db.TU_Users, o => o.UserID, i => i.UserID, (o, i) => new
                            {
                                o.PlanID,
                                o.Appraise,
                                Content = new DepartmentMonthPlan().SplitString(o.Content,4),
                                o.AddTime,
                                i.RealName
                            }).ToList().Where(pa => pa.PlanID == planId && pa.AddTime.Value.ToString("yyyy-MM-dd").Equals(addDateTime)).ToList();
                        repeater.DataSource = comments;
                        repeater.DataBind();
                    }
                }
            }
        }
    }
}