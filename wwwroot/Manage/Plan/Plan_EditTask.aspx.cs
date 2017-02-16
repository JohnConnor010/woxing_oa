using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_EditTask : System.Web.UI.Page
    {
        public string mes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["TaskId"] != null)
                {
                    WX.Model.Task.MODEL task = WX.Request.rTask;
                    TextBox1.Text = task.Title.ToString();
                    TextBox2.Text = task.Etime.ToString();
                    TextBox3.Text = task.Content.ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.Model.Task.MODEL task = WX.Model.Task.NewDataModel();
            if (Request["TaskId"] != null)
            {
                task = WX.Request.rTask;
            }
            task.Title.value = TextBox1.Text;
            task.Etime.value = TextBox2.Text;
            task.Content.value = TextBox3.Text;
            if (Request["TaskId"] != null)
            {
                task.Update();
            }
            else
            {
                task.PlanID.value = WX.Request.rPlanId;
                task.State.value = 0;
                task.Statetime.value = DateTime.Now;
                task.Insert();
            }
            mes = "butsumit();";
        }
    }
}