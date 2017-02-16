using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wwwroot.Manage.ashx
{
    /// <summary>
    /// online 的摘要说明
    /// </summary>
    public class online : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {            
            //更新在线状态
            context.Response.ContentType = "text/plain";
            #region //此处被改动，由于效率不高可删除
            //WX.CurUser cu = WX.Main.NewCurUser(); //new WX.CurUser(WX.Authentication.GetUserName());            
            //cu.LoadOnlineUser();
            //cu.OnlineUser.LastUpdateTime.set_DateTime_Now();
            //int no =cu.OnlineUser.Update();
            #endregion
            //登出时退出
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Write("LOGIN_OUT");
                return;
            }
            //测试是否连接数据库
            if (ULCode.QDA.XSql.GetData("Select 1").ToInt32() != 1)
            {
                context.Response.Write("CN_ERROR");
                return;
            }
            string userName=WX.Authentication.GetUserName();
            //string sSql = String.Format("if(Select * from TU_OnlineUsers where UserID=)update tu_onlineUsers set LastUpdateTime=GetDate() "
            //        + " where UserId=(select UserId from aspnet_users where UserName='{0}') ", userName);
            string sSql = String.Format("declare @UserId uniqueIdentifier;"
                + " select @UserId=UserID from aspnet_Users where UserName='{0}';"
                + " if exists(select * from TU_OnlineUsers where UserId=@UserId)"
                + "    update TU_OnlineUsers set LastUpdateTime=GetDate() where UserId=@UserId;"
                + " else"
                + "    insert into TU_OnlineUsers(LoginID,UserID,LoginIP) values('{1}',@UserId,'{2}');"
                + " delete from TU_OnlineUsers where Datediff(Minute,LastUpdateTime,getdate()) >30;", userName, Guid.NewGuid().ToString(), WX.Main.getIp(context));
            int no = ULCode.QDA.XSql.Execute(sSql);
            if (no == 0)
            {
                WX.Authentication.LoginOut();
                context.Response.Write("LOGIN_OUT");
                return;
            }
            //检查公告消息并生成提醒
            WX.XZ.Notify.CheckMess();
            WX.CRM.Customer.CheckCompMess(WX.CommonUtils.GetBossUserID);
            //在线人数统计
            sSql = "Select Count(*) from TU_OnlineUsers";
            int countOfOnlineUsers = Convert.ToInt32(ULCode.QDA.XSql.GetValue(sSql));
            //List<WX.Model.OnlineUser.MODEL> list = WX.Model.OnlineUser.GetModels("select * from TU_OnlineUsers");
            context.Response.Write("<a onclick=addTab('在线用户','/Manage/Work/Users_OnLine.aspx','icon-home') href='#'>在线用户：<strong>" + countOfOnlineUsers + "</strong> 人</a>");
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}