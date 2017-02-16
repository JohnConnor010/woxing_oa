using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using ULCode.QDA;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace wwwroot.Manage.ashx
{
    /// <summary>
    /// Summary description for GetJsonOfDepartment
    /// </summary>
    public class GetJsonOfDepartment : IHttpHandler
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "json";
            DataTable data = XSql.GetDataTable("Select 0 As ID,'--最顶级--' As Name,-1 as ParentID Union SELECT ID,Name,ParentID FROM TE_Departments");
            //DataRow dr = data.Rows.Add("0","--最顶级部门--");
            //data.Rows.Remove(dr);
            //data.Rows.InsertAt(dr, 0);
            string json = GetJsonTree(data, -1);
            if (json.Length > 12)
            {
                json = json.Substring(12);
            }
            context.Response.Write(json);
        }
        public string GetJsonTree(DataTable data, int parentId)
        {
            StringBuilder json = new StringBuilder();
            string filterExpression = "ParentID=" + parentId;
            DataRow[] rows = data.Select(filterExpression);
            if (rows.Length < 1)
            {
                return "";
            }
            json.Append(",\"children\":[");
            foreach (DataRow row in rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                json.Append("{");
                json.AppendFormat("\"id\":\"{0}\",", row["ID"].ToString());
                json.AppendFormat("\"text\":\"{0}\",", row["Name"].ToString());
                json.Append("\"state\":\"open\"");
                json.Append(GetJsonTree(data, id).TrimEnd(','));
                json.Append("},");
            }
            if (json.ToString().EndsWith(","))
            {
                json.Remove(json.Length - 1, 1);
            }
            json.Append("]");
            return json.ToString();
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