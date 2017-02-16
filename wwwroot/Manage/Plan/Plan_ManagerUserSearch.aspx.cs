using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_ManagerUserSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Data.DataTable dt = ULCode.QDA.XSql.GetDataTable("select * from Plan_UserSearch");
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }
    }
}