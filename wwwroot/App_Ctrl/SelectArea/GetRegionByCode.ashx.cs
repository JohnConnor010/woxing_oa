using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using ULCode.QDA;
using System.Data.Linq;
using System.Data;

namespace wwwroot.App_Ctrl.SelectArea
{
    /// <summary>
    /// Summary description for GetRegionByCode
    /// </summary>
    public class GetRegionByCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "json";
            string code = context.Request.QueryString["Code"];
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty("Region"))
            {
                string sSql = string.Empty;
                if (context.Request.QueryString["Region"] == "Province")
                {
                    sSql = "SELECT code,name FROM CRM_City WHERE ProvinceId='" + context.Request.QueryString["code"] + "'";
                }
                if (context.Request.QueryString["Region"] == "City")
                {
                    sSql = "SELECT code,name FROM CRM_Area WHERE CityId='" + context.Request.QueryString["code"] + "'";
                }
                var dataTable = XSql.GetDataTable(sSql);
                var cities = dataTable.AsEnumerable().Select(c => new
                {
                    Code = c.Field<string>("code"),
                    Name = c.Field<string>("name")
                });
                string json = JsonConvert.SerializeObject(cities);
                context.Response.Write(json);
            }
            else
            {
                context.Response.Write("");
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