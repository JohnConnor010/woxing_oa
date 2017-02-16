using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class CompanyNature : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
        }
    }
}