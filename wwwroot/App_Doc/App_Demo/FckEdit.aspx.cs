using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Demo
{
    public partial class FckEdit : System.Web.UI.Page
    {
        
        public int fid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["id"] != null && Request["id"] != "")
                {
                    try
                    {
                        fid = Convert.ToInt32(Request["id"]);
                    }
                    catch { }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<b>内容如下：</b><br/>" + FORM_CONTENT.Value);

        }
    }
}