using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Sys
{
    public partial class Priv_ModiPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!this.Master.A_Edit)
                {
                    Response.Write("你没有权限访问此功能！");
                    Response.End();
                    return;
                }
                if (WX.Main.CurUser.IsEmployeeUser)
                {
                    this.MenuBar1.Key = "priv";
                    this.MenuBar1.CurIndex = 8;
                }
                else
                {
                    this.MenuBar1.Key = "priv-admin";
                    this.MenuBar1.CurIndex = 1;
                }
            }
        }
    }
}