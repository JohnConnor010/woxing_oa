using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Sys
{
    public partial class User_ResetPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            if (!IsPostBack)
            {
            WX.Model.User.MODEL usermodel = WX.Request.rUser;//WX.Model.Employee.GetModel("select * from TU_Employees where UserID='" + Request["UserID"] + "'");
            liUserName.Text = String.Format("员工：{0}&nbsp;&nbsp;&nbsp;&nbsp;用户名：{1}", usermodel.RealName, usermodel.UserName);
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            WX.WXUser cuser = WX.Request.rCurUser;
            this.lblNewPwd.Text = cuser.AspNetUser.ResetPassword();
            cuser = null;
        }
    }
}