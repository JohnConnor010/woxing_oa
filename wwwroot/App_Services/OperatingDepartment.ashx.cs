using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ULCode.QDA;
using WX;

namespace wwwroot.App_Services
{
    /// <summary>
    /// Summary description for OperatingDepartment
    /// </summary>
    public class OperatingDepartment : IHttpHandler
    {
        private int PermissionNum;
        private bool A_Del;
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
                string action = context.Request.QueryString["action"];

                //3.验证用户变量               
                int id = WX.Request.rDepartmentId;
                int companyID=WX.Request.rCompanyId;
                //4.处理业务
                    
                if (action == "delete")
                {
                    string sql = String.Format("Select * from tu_Employees where DepartmentID={0}",id);
                    if (ULCode.QDA.XSql.IsHasRow(sql))
                    {
                        context.Response.Write("-3");
                        context.Response.End();
                        return;
                    }
                    sql = "DELETE FROM TE_Departments WHERE CompanyId=" + companyID + " AND ID=" + id;
                    System.Diagnostics.Debug.WriteLine(sql);
                    int row = XSql.Execute(sql);
                    //5.（用户及业务对象）统计与状态
                    if (row > 0)
                    {
                        WX.Model.Department.GetCache(id).RemoveFromCaches();
                        //6.登记日志
                        WX.Main.AddLog(LogType.Default, "部门信息删除成功！", "");
                        //7.返回页面
                        context.Response.Write("0");
                    }
                    else
                    {
                        context.Response.Write("1");
                    }
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