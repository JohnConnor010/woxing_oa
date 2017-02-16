using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ULCode.QDA;
using WX;

namespace wwwroot.App_Services
{
    /// <summary>
    /// Summary description for DeleteEmployee
    /// </summary>
    public class DeleteEmployee : IHttpHandler
    {
        private int PermissionNum = 0;
        private bool A_Del = true;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            this.PermissionNum = WX.Main.GetPermission(true);
            if (this.PermissionNum == -1)
            {
                context.Response.Write("-1");
                context.Response.End();
                return;
            }
            this.A_Del = this.PermissionNum >= Convert.ToInt32(PermissionType.Del);

            //1.验证当前用户页面权限
            if (!A_Del)
            {
                context.Response.Write("-2");
                context.Response.End();
                return;
            }
            else
            {
                //2.获取用户变量
                string param = "";
                int companyID = 0;
                //3.验证用户变量
                if (string.IsNullOrEmpty(context.Request["param"]))
                {
                    return;
                }
                if (!int.TryParse(context.Request.QueryString["companyId"], out companyID))
                {
                    return;
                }
                //4.处理业务
                param = context.Request.QueryString["param"];
                string[] myParam = param.Split(',');
                int row = 0;
                foreach (string str in myParam)
                {
                    if (Membership.DeleteUser(WXUser.GetUserNameByUserID(str)))
                    {
                        string cmdText = "DELETE FROM TU_Users WHERE UserID IN ('" + str + "')";
                        //System.Diagnostics.Debug.WriteLine(cmdText);
                        int iR= XSql.Execute(cmdText);
                        //if (iR > 0)
                        //{
                            XSql.Execute("DELETE FROM TU_Employees WHERE UserID IN ('" + str + "')");
                            //WX.Model.Employee.GetModelToID(str).RemoveFromCaches();
                        //}
                        row += iR;
                    }
                }
                if (row > 0)
                {
                    //6.登记日志
                    WX.Main.AddLog(LogType.Default, String.Format("员工信息删除成功！共选择{0}个，删除{1}个", myParam.Length, row), param);
                    //7.返回页面
                    context.Response.Write("1");
                }
                else
                {
                    context.Response.Write("0");
                }
            }
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