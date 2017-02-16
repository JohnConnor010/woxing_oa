using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;
namespace wwwroot.Manage.HR
{
    public partial class User_Set : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String userID = WX.Request.rUserId;
            if (!ULCode.Validation.IsGuid(userID))
            {
                ULCode.Debug.we("你没有权利访问本页！");
                return;
            }
            if (!Page.IsPostBack)
            {
                this.LoadData();
            }
        }
        private void LoadData()
        {
            String userID = WX.Request.rUserId;
            WX.Model.User.MODEL user = WX.Model.User.GetCache(userID);
            cbArchiveBySelf.Checked = user.ArchiveBySelf.ToBoolean();
        }
        protected void ModiArchiveBySelf(object sender, EventArgs e)
        {
            String userID = WX.Request.rUserId;
            WX.Model.User.MODEL user = WX.Model.User.GetCache(userID);
            user.ArchiveBySelf.set(cbArchiveBySelf.Checked);
            user.Update();
        }
    }
}