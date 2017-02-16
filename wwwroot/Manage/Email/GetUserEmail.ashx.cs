using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ULCode.QDA;

namespace wwwroot.Manage.Email
{
    /// <summary>
    /// Summary description for GetUserEmail
    /// </summary>
    public class GetUserEmail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
            context.Response.ContentType = "text/json";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM View_UserEmail WHERE IsLockedOut=0 AND Email IS NOT NULL", connection);
                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                sda.Fill(table);
                string json = JsonConvert.SerializeObject(table);
                context.Response.Write(json);
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