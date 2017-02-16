using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ULCode.QDA;
using Newtonsoft.Json;
using System.Data;

namespace wwwroot.App_Services
{
    /// <summary>
    /// GetCompanyPartner 的摘要说明
    /// </summary>
    public class GetCompanyPartner : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dataTable = XSql.GetDataTable("Select tcp.*,te.RealName,te2.RealName ManageName,te.Sex,te.Edu from [TE_Companys_Partner] tcp left join TU_Users te on tcp.EmployeeID=te.UserID left join TU_Users te2 on tcp.Manage=te2.UserID where tcp.Id=" + context.Request.QueryString["ID"]);
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