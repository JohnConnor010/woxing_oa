using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Demo
{
    public partial class WebSignDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string content = this.txtContent.Text.Trim();
            string signData = this.txtSealData.Value;
            if (!string.IsNullOrEmpty(signData))
            {
                Session["SignData"] = signData;
                Response.Redirect("ShowSignData.aspx");
            }
        }
    }
}