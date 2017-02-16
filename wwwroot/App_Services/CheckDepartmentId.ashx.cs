using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ULCode.QDA;

namespace wwwroot.Manage.ashx
{
    /// <summary>
    /// Summary description for CheckDepartmentId
    /// </summary>
    public class CheckDepartmentId : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int departmentId = 0;
            int companyId = WX.Main.DefaultCompanyId;
            if (int.TryParse(context.Request["Id"], out departmentId))
            {
                //int count = (int)XSql.GetValue("SELECT COUNT(ID) FROM TE_Departments WHERE ID=" + departmentId);
                if (WX.Model.Department.GetCache(departmentId) != null)
                {
                    context.Response.Write("false");
                }
                else
                {
                    context.Response.Write("true");
                }
            }
            else
            {
                context.Response.Write("false");
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