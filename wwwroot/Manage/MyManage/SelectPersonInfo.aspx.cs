using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wwwroot.WXDataContext;

namespace wwwroot.Manage.MyManage
{
    public partial class SelectPersonInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            using(WXOADataContext db = new WXOADataContext())
            {
                string userId = Request.QueryString["UserID"];
                if(!string.IsNullOrEmpty(userId))
                {
                    var entity = db.TU_Users.FirstOrDefault(u => u.UserID == Guid.Parse(userId));
                    if(entity != null)
                    {
                        this.ltlName.Text = entity.RealName;
                    }
                }
            }
        }
    }
}