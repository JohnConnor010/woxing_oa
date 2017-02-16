using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using WX.Model;
namespace WX.Web
{
    public partial class RegisterE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = WX.Soft.Name + " 新员工注册";
        }
        protected void RegisterUser(object sender, EventArgs e)
        {
            //获取用户变量
            string name = this.UserName.Value;
            string pwd = this.PassWord.Value;
            string code = this.GetCode.Value;
            //验证用户变量
            if (HttpContext.Current.Session["CheckCode"] != null && Convert.ToString(HttpContext.Current.Session["CheckCode"])!=code)
            {
                ULCode.Debug.Alert(this,"验证码不对!");
                return;
            }
            if (Membership.GetUser(name) != null)
            {
                ULCode.Debug.Alert(this, "此用户已经存在!");
                return;
            }
            //注册账户
            MembershipUser mu = Membership.CreateUser(name, pwd);
            //添加员工
            Roles.AddUserToRole(name, Convert.ToString(WX.RoleType.Employees));
            //登录到浏览器
            WX.Authentication.LoginIn(name);
            //注册员工默认信息
            WX.Model.User.MODEL usermodel = WX.Model.User.NewDataModel();
            usermodel.UserID.set(mu.ProviderUserKey.ToString());
            usermodel.CompanyID.set(11);
            usermodel.DutyId.set(1000);
            usermodel.DepartmentID.set(0);
            usermodel.RealName.set(name);
            usermodel.ArchiveBySelf.set(true);
            usermodel.State.set(5);
            usermodel.Insert();
            Employee.MODEL employeeNew = Employee.NewDataModel();
            employeeNew.UserID.set(usermodel.UserID.value);
            employeeNew.Insert();
            Session.Clear();
            usermodel.SaveIntoCaches();
            ULCode.Debug.Alert("注册成功！请牢记用户名密码，继续完善档案资料！", "/SubResume.aspx");
        }
    }
}