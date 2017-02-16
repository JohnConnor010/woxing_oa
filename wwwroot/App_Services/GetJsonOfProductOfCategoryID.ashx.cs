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
    /// Summary description for GetJsonOfProductOfCategoryID
    /// </summary>
    public class GetJsonOfProductOfCategoryID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "json";
            string categoryId = context.Request.QueryString["CategoryID"];
            DataTable productData = XSql.GetDataTable("SELECT ProductID,ProductName FROM Ass_Warehouse WHERE CategoryID=" + categoryId);
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