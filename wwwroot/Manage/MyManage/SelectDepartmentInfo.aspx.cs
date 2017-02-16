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
    public partial class SelectDepartmentInfo : System.Web.UI.Page
    {
        protected string departmentName;
        protected string content = "无部门简介";
        protected int weekIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            weekIndex = ConvertDateTime.GetWeekIndex(DateTime.Now.ToString());
            string departmentId = Request.QueryString["DepartmentID"];
            using(WXOADataContext db = new WXOADataContext())
            {
                var entity = db.TE_Departments.FirstOrDefault(d => d.ID == int.Parse(departmentId));
                if(entity != null)
                {
                    departmentName = entity.Name;
                    content = entity.Content;
                }
                var query1 = db.TU_Users.Where(u => u.DepartmentID == int.Parse(departmentId)).OrderBy(u => u.State).Select(u => new
                    {
                        u.RealName,
                        PositionName = db.TE_DutyDetails.FirstOrDefault(d => d.ID == Convert.ToInt64(u.DutyId)).Name,
                        u.Grade
                    });
                this.Repeater1.DataSource = query1;
                this.Repeater1.DataBind();
            }
        }
    }
}