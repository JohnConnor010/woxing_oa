using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.DownLoad
{
    public partial class DownLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["AnnexID"] != null && Request["AnnexID"] != "")
            {
                WX.Down.Model.Annex.MODEL annexmodel = WX.Down.Model.Annex.NewDataModel(Request["AnnexID"]);
                annexmodel.Count.value = annexmodel.Count.ToInt32() + 1;
                annexmodel.Update();
                Response.Redirect(annexmodel.Annex.ToString());
            }
        }
    }
}