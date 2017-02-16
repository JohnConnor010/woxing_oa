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
    /// Summary description for GetJsonOfSupplierBySupplierID
    /// </summary>
    public class GetJsonOfSupplierBySupplierID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string json = string.Empty;
            if (!string.IsNullOrEmpty(context.Request.QueryString["SupplierID"]))
            {
                DataTable data = XSql.GetDataTable("SELECT CompanyName,ContactName FROM Ass_Suppliers WHERE SupplierID=" + context.Request.QueryString["SupplierID"]);
                json = JsonConvert.SerializeObject(data);
            }
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