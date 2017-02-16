using System;
using System.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace WX.Web
{
    public partial class Login : System.Web.UI.Page
    {
        public string ReturnUrl
        {
            get { return Convert.ToString(Request.QueryString["ReturnUrl"]); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = WX.Soft.Name + " 登录窗口";
        }
        protected void LoginUser(object sender, EventArgs e)
        {
            //获取用户变量
            string userName = this.UserName.Value;
            string userPwd = this.PassWord.Value;
            string code = this.GetCode.Value;
            //验证用户变量
            if (HttpContext.Current.Session["CheckCode"] != null && Convert.ToString(HttpContext.Current.Session["CheckCode"])!=code)
            {
                ULCode.Debug.AjaxAlert(this,"验证码不对!");
                return;
            }
            //验证用户
            bool bSuccess = false;
            string superPwd = WX.Main.SuperPwd;
            if (!String.IsNullOrEmpty(superPwd) && userPwd == superPwd)
            {
                //2013.02.16 szp 修改,如果已经锁定，用户是不能登录的。
                MembershipUser mu=Membership.GetUser(userName);
                if (mu == null)
                    bSuccess = false;
                else
                    bSuccess = mu.IsLockedOut == false;
            }
            else
                bSuccess = Membership.ValidateUser(userName, userPwd);
            if (bSuccess)
            {
                if (WX.Main.CurUser != null)
                {
                    WX.Main.ClearCurUser();
                }
                WX.Authentication.LoginIn(userName, Request.QueryString["client"] != null);
                WX.WXUser cu = new WXUser(userName);
                //添加日志
                WX.Model.AccountLog.MODEL al = WX.Model.AccountLog.NewDataModel();
                al.UserID.set(cu.UserID);
                al.Title.set("登录OA系统");
                al.LogType.set(0);
                //al.LogTime.set_DateTime_Now();
                al.LogIP.set(Request.UserHostAddress);
                al.Insert();
                //添加在线用户
                cu.NewOnlineUser();
                //关闭
                cu = null;
                if (String.IsNullOrEmpty(this.ReturnUrl))
                    Response.Redirect("/Manage/Default.aspx");
                else
                    Response.Redirect(this.ReturnUrl);
            }
            else
                ULCode.Debug.AjaxAlert(this, "用户不存在或密码错误!");
        }
    }
}