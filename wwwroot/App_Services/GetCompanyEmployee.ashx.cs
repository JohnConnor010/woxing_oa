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
    /// GetCompanyEmployee 的摘要说明
    /// </summary>
    public class GetCompanyEmployee : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dataTable = XSql.GetDataTable("Select UserID,RealName,Sex,Edu,IDCard from TU_Employees where UserID='" + context.Request.QueryString["ID"]+"'");
            string json = JsonConvert.SerializeObject(dataTable);
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