using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Sys
{
    public partial class GetPartnerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pageinit();
        }
        private void pageinit()
        {
            DataTable list;
            list = ULCode.QDA.XSql.GetDataTable("Select tcp.*,te.IDCard,te.Sex,tu.RealName from [TE_Companys_Partner] tcp left join TU_Users tu on tcp.EmployeeID=tu.UserID left join TU_Employees te on tcp.EmployeeID=te.UserID where tcp.State=1 and tcp.CompanyId=" + Request["CompanyId"] + " order by ID asc");
            AssetsRepeater.DataSource = list;
            AssetsRepeater.DataBind();
        }
    }
}