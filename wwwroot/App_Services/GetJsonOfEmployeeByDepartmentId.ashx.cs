using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ULCode.QDA;
using Newtonsoft.Json;

namespace wwwroot.App_Services
{
    /// <summary>
    /// Summary description for GetJsonOfEmployeeByDepartmentId
    /// </summary>
    public class GetJsonOfEmployeeByDepartmentId : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "json";
            string departmentID= context.Request.QueryString["DepartmentID"];
            DataTable employeeData = XSql.GetDataTable("SELECT UserID,RealName FROM TU_Users WHERE" + (departmentID != "" ? " DepartmentID=" + departmentID + " and" : "") + " State>=10 and State<40 order by DepartmentID");
            var employees = employeeData.AsEnumerable().Select(e => new
                {
                    UserID = e.Field<Guid>("UserID"),
                    UserName = e.Field<string>("RealName")
                });
            string json = JsonConvert.SerializeObject(employees);
            context.Response.Write(json);
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