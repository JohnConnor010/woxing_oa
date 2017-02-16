using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Demo
{
    public partial class ShowSignData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SignData"] != null)
                {
                    this.signData.Value = Session["SignData"].ToString();
                }
            }
        }
    }
}