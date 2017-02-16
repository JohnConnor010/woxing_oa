using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX;
namespace wwwroot.App_Test
{
    public partial class WebForm_TestCurUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //MembershipUser mu = Membership.GetUser("9F2B74B2-C586-42D1-99EB-031A2C72D72E");
            //Response.Write(mu.UserName);  new pass:*QkxtK(#RfOHui
            WXUser cu = new WXUser("455868D6-3BD9-486B-BF37-75387CD5FC9E");
            //Response.Write(cu.AspNetUser.ResetPassword());
            Response.Write(cu.AspNetUser.UnlockUser().ToString());
        }
    }
}