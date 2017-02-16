using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_ManagerPlanDay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                MenuBar1.CurIndex = 1 + Convert.ToInt32(Request["type"]);
            }
            catch { }
            if (Request["rtype"] == "3" || (Request["dept"] != null && Request["dept"] != ""))
            {
                MenuBar1.Key = "plan_cmp";
            }
        }
    }
}