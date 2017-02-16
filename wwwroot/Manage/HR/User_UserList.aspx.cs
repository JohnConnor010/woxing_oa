using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class User_UserList : System.Web.UI.Page
    {
        protected bool edit;
        protected bool delete;
        protected int companyId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["companyId"], out companyId))
            {
                edit = this.Master.A_Edit;
                delete = this.Master.A_Del;
            }
            else
            {
                Response.Write("公司编号不正确！");
                Response.End();
                return;
            }
        }
    }
}