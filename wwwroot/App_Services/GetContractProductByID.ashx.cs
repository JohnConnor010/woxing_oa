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
    /// Summary description for GetContractProductByID
    /// </summary>
    public class GetContractProductByID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.QueryString["ID"];
            DataTable productData = XSql.GetDataTable("SELECT * FROM PDT_Products WHERE ID=" + id);
            string json = JsonConvert.SerializeObject(productData);
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