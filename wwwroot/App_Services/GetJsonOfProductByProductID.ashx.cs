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
    /// Summary description for GetJsonOfProductByProductID
    /// </summary>
    public class GetJsonOfProductByProductID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DataTable dataTable = XSql.GetDataTable("SELECT w.*,s.CompanyName,u.ID AS UnitID,u.UnitName FROM Ass_Warehouse AS w LEFT JOIN Ass_Suppliers AS s ON w.Suppliers=s.SupplierID LEFT JOIN Ass_Unit AS u ON w.Unit=u.UnitName WHERE w.ID=" + context.Request.QueryString["ProductID"]);
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