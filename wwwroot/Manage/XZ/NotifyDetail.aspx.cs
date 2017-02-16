using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.XZ
{
    public partial class NotifyDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.XZ.Notify.MODEL model = WX.Request.rNotify;
            li_title.Text = model.Title.ToString();
            li_user.Text = WX.CommonUtils.GetRealNameListByUserIdList(model.UserID.ToString());
            li_starttime.Text = Convert.ToDateTime(model.Starttime.ToString()).ToString("yyyy-MM-dd");
            li_content.Text = model.Content.ToString();
            try
            {
                string[] annexs = model.Annex.ToString().Split('|');
                li_content.Text += annexs.Length == 2 && annexs[0] != "" ? "<br/>查看附件：<a href='" + annexs[0] + "'>" + annexs[1] + "</a><br/><br/>" : "";
            }
            catch { }
            if (Request["id"] != null && Request["id"] != "")
            {
                try
                {
                    WX.Main.MessageToHistory("'" + Request["id"] + "'");
                }
                catch
                {
                }
            }
        }
    }
}