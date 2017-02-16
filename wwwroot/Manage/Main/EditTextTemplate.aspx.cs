using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Main
{
    public partial class EditTextTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["ID"]);
                string sSql = String.Format("Select Template from TE_Text_Templates where ID={0}", id);
                this.ui_content.Value = ULCode.QDA.XSql.GetData(sSql).ToString();
            }
        }
        protected void Modi(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["ID"]);
            string value = this.ui_content.Value.Replace("'", "''");
            string sSql = String.Format("Update TE_Text_Templates set Template='{1}' where ID={0}", id, value);
            ULCode.QDA.XSql.Execute(sSql);
        }
    }
}