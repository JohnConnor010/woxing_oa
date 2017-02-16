using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Private.Status
{
    public partial class Priv_Status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadName();
                this.LoadDuty();
            }
        }
        private void LoadName()
        {
            WX.WXUser user = WX.Main.CurUser;
            user.LoadUserModel(false);
            user.LoadEmployeeUser(false);
            if (user.EmployeeUser.Sex.ToBoolean())
                this.imgFace.ImageUrl = "/Images/User/man_icon.gif";
            else
                this.imgFace.ImageUrl = "/Images/User/woman_icon.gif";
            this.lblName.Text = user.UserModel.RealName.ToString();
        }
        private void LoadDuty()
        {
            WX.WXUser user = WX.Main.CurUser;
            user.LoadUserModel(false);
            user.LoadMyDepartment(true);
            user.LoadDutyDetailUser(true);
            user.LoadDutyUser(true);
            user.LoadMyGrade(true);
            int grade = 0;
            if (user.MyGrade != null) grade = user.DutyDetailUser.GradeID.ToInt32(); //grade = user.MyGrade.Sort.ToInt32();
            this.lblDutyState.Text = String.Format("等级：{2}级<br />职务：{1}<br />部门：{0}<br /><span><a>[查看][日志]</a></span>"
                , user.MyDepartMent.Name, user.DutyUser.Name, grade);

        }
    }
}