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
    /// Summary description for GetJsonOfConsumingByID
    /// </summary>
    public class GetJsonOfConsumingByID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = context.Request.QueryString["ID"];
            DataTable consumingData = XSql.GetDataTable("SELECT c.ID,c.ProductID,c.Quantity,w.ProductName,c.Unit,c.Price FROM Ass_Equipment AS c INNER JOIN Ass_Warehouse AS w ON c.ProductID=w.ProductID WHERE c.ID=" + id);
            string json = JsonConvert.SerializeObject(consumingData);
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