using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage
{
    public partial class IntoJobList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["mes"] != null)
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%IntoJobList.aspx%'", WX.Main.CurUser.UserID));
           
        }
    }
}