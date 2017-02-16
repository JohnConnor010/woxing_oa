using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;

namespace wwwroot.Manage.CTR
{
    public partial class SelectCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            Dict.BindListCtrl_Companys(this.ddlCompany, null, "0#--请选择公司名称--", null);
        }
    }
}