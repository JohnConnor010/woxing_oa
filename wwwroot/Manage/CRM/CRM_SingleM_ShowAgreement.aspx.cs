using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class CRM_SingleM_ShowAgreement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WX.CRM.CustomerAgreement.MODEL agreement = WX.CRM.CustomerAgreement.NewDataModel(Request["AgreementID"]);
                Literal1.Text = agreement.Content.ToString();
            }
        }
    }
}