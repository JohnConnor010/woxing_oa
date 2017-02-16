using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.App_Ctrl
{
    public partial class ShowSignData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Context.Request.QueryString["id"]);
                    string sSql = String.Format("Select SignData from FL_RunFeedBack where Id={0}", id);
                    string sdata = ULCode.QDA.XSql.GetData(sSql).ToStr();
                    //Response.Write(sdata);
                    if (!String.IsNullOrEmpty(sdata))
                    {
                        this.signData.Value = sdata;
                    }
                }
            }
        }
    }
}