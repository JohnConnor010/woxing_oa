using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Work
{
    public partial class Work_Apply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Button3.PostBackUrl = "/Manage/HR/HR_AddTransferKong.aspx?type=2&UserID=" + WX.Main.CurUser.UserID;
                //Button7.PostBackUrl = "/Manage/HR/HR_AddTransferKong.aspx?type=1&UserID=" + WX.Main.CurUser.UserID;
                //Button11.PostBackUrl = "/Manage/HR/HR_Official.aspx?UserID=" + WX.Main.CurUser.UserID;
                //Button13.PostBackUrl = "/Manage/HR/HR_Userjobs.aspx?UserId=" + WX.Main.CurUser.UserID;
                if (WX.Main.CurUser.UserModel.State.ToInt32() >=20)
                {
                    Button11.Visible = false;
                }
            }
        }
    }
}