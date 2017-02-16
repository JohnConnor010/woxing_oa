using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Sys
{
    public partial class Priv_CredentialsDetail : System.Web.UI.Page
    {
        int cid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cid = Convert.ToInt32(Request["id"]);
            }
            catch
            {
            }
            if (cid > 0)
            {
                WX.Model.EmployeeCredential.MODEL model = WX.Model.EmployeeCredential.GetModel("Select * from [TU_Employees_Credentials] where Id="+cid);
                if (model != null)
                {
                    ui_name.Text = model.Name.ToString();
                    ui_unit.Text = model.Unit.ToString();
                    ui_ctime.Text = ((DateTime)model.Ctime.value).ToString("yyyy-MM-dd");
                    ui_content.Text = model.Content.ToString();
                    Image1.ImageUrl = model.Annex.ToString();

                }
            }
        }
    }
}