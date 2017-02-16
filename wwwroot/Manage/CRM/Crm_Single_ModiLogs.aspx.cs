using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Single_ModiLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string mode = Convert.ToString(Request.QueryString["PageMode"]);
                if (mode == "my")
                {
                    this.lblTitle.Text = "我的客户";
                    this.MenuBar1.Key = "MyCustomer-Modi";
                }
                else
                {
                    this.lblTitle.Text = "我的管理";
                    this.MenuBar1.Key = "Customer-Modi";
                }
            }
        }
    }
}