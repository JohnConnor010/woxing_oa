using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Model;

namespace wwwroot.Manage.HR
{
    public partial class HR_TransferKong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            string userId = WX.Request.rUserId;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            MenuBar1.Param2 = usermodel.State.ToString();
            System.Data.DataTable dt =WX.HR.TransferKong.GetList(userId);
            Gv_tfk.DataSource = dt;
            Gv_tfk.DataBind();
            if (Gv_tfk.Rows.Count > 0)
            {
                Gv_tfk.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_tfk.HeaderStyle.Height = Unit.Pixel(40);
            }
        }
        
    }
}