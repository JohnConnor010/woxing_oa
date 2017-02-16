using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class GetEmployeeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pageinit();
        }
        private void pageinit()
        {
            DataTable list;
            list = ULCode.QDA.XSql.GetDataTable("Select te.UserID,te.RealName,te.Sex,te.Mobile,te.Email,te.IDCard,te.Edu,td.Name deptname,tduty.Name dutyname from TU_Employees te left join TE_Departments td on te.DepartmentID=td.ID left join TE_Duties tduty on te.dutyID=tduty.ID where te.CompanyId=" + Request["CompanyId"] + " order by te.Sort asc");
            AssetsRepeater.DataSource = list;
            AssetsRepeater.DataBind();
        }
    }
}