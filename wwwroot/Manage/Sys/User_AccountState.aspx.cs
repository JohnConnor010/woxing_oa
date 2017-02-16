using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Sys
{
    public partial class User_AccountState : System.Web.UI.Page
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
                string userId = WX.Request.rUserId;
                if (!ULCode.Validation.IsGuid(userId))
                {
                    ULCode.Debug.we("你无权访问此页面！");
                    return;
                }
                WX.WXUser cu = WX.Request.rCurUser;
                cu.LoadUserModel(false);
                this.lblUserId.Text = cu.UserID;
                this.liUserName.Text = String.Format("员工：{0}&nbsp;&nbsp;&nbsp;&nbsp;用户名：{1}", cu.UserModel.RealName, cu.UserName);
                this.lblUserName.Text = cu.UserName;
                this.lblEmail.Text = cu.AspNetUser.Email;
                this.lblCreateDate.Text = String.Format("{0:yyyy-MM-dd HH:mm:ss}", cu.AspNetUser.CreationDate);
                this.lblLoginDate.Text = String.Format("{0:yyyy-MM-dd HH:mm:ss}", cu.AspNetUser.LastLoginDate);
                this.lblLastUpdatePwd.Text = String.Format("{0:yyyy-MM-dd HH:mm:ss}", cu.AspNetUser.LastPasswordChangedDate);
                this.lblLastLock.Text = String.Format("{0:yyyy-MM-dd HH:mm:ss}", cu.AspNetUser.LastLockoutDate);
                string sSql = String.Format("Select * from TU_OnlineUsers where UserId='{0}'", userId);
                bool online = ULCode.QDA.XSql.IsHasRow(sSql);
                if (online)
                {
                    this.lblOnlineState.ForeColor = System.Drawing.Color.Gold;
                    this.lblOnlineState.Text = "在线";
                }
                else
                {
                    this.lblOnlineState.ForeColor = System.Drawing.Color.Gray;
                    this.lblOnlineState.Text = "离线";
                }

                if (cu.AspNetUser.IsLockedOut)
                {
                    this.lblState.Text = "锁定";
                    this.lblState.ForeColor = System.Drawing.Color.Red;
                    this.btnLock.Enabled = false;
                    this.btnUnlock.Enabled = true;
                }
                else
                {
                    this.lblState.Text = "正常使用";
                    this.lblState.ForeColor = System.Drawing.Color.Green;
                    this.btnLock.Enabled = true;
                    this.btnUnlock.Enabled = false;
                }
            }
        }
        protected void Lock(object sender, EventArgs e)
        {
           
            //1.验证用户权限

            //2.取得用户变量
            string userid = Request["UserID"];
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form
            
            //4.业务处理过程
            
            bool bDeal = false;
            //填写主要业务逻辑代码
            if (WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=1", "UserID='" + userid + "'") > 0)
            {                
                bDeal = true;
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("成功锁定用户状态({0})！", userid), "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Confirm(this, "成功锁定用户状态！是否返回用户列表页？", "User_UserList.aspx?CompanyID=11", this.Request.RawUrl);
            }
            else
            {
                ULCode.Debug.Alert("修改用户状态失败,请重试！");
            }
        }
        protected void Unlock(object sender, EventArgs e)
        {

            //1.验证用户权限

            //2.取得用户变量
            string userid = WX.Request.rUserId;
            //以下代码由后台开发人员填写
            //3.验证用户变量，包含Request.QueryString及Request.Form

            //4.业务处理过程

            bool bDeal = false;
            //填写主要业务逻辑代码
            if (WX.Main.ExcuteUpdate("aspnet_Membership", "IsLockedOut=0", "UserID='" + userid + "'") > 0)
            {
                bDeal = true;
            }
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (bDeal)
            {
                WX.Main.AddLog(WX.LogType.Default, String.Format("成功解锁用户状态({0})！",userid), "");
            }

            //7.返回处理结果或返回其它页面。
            if (bDeal)
            {
                ULCode.Debug.Confirm(this, "成功解锁用户状态！是否返回用户列表页？", "User_UserList.aspx?CompanyID=11", this.Request.RawUrl);
            }
            else
            {
                ULCode.Debug.Alert("修改用户状态失败,请重试！");
            }
        }
    }
}