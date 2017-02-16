using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace wwwroot.App_Services
{
    /// <summary>
    /// Summary description for SavePosition
    /// </summary>
    public class SavePosition : IHttpHandler
    {

        private string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string sql = context.Request.QueryString["sql"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string cmdText = sql;
                SqlCommand command = new SqlCommand(sql, connection);
                int row = command.ExecuteNonQuery();
                context.Response.Write(row);

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