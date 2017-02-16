
using iNethinkCMS.Command;
using LumiSoft.Net.Mail;
using LumiSoft.Net.POP3.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;

namespace wwwroot.Manage.Email
{
    /// <summary>
    /// Summary description for FetchEmail
    /// </summary>
    public class FetchEmail : IHttpHandler
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userId = context.Request.QueryString["UserID"];
            DataTable table;
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [TU_EmailPOP] WHERE UserID='" + userId + "'", connection);
                table = new DataTable();
                sda.Fill(table);
            }
            string host = string.Empty;
            string userName = string.Empty;;
            string password = string.Empty;;
            int port = 993;
            bool ssl = true;
            foreach(DataRow row in table.Rows)
            {
                host = row["Host"].ToString();
                userName = row["UserName"].ToString();
                password = row["Password"].ToString();
                port = int.Parse(row["Port"].ToString());
            }
            string json = string.Empty;
            List<Item> items = new List<Item>();
            using(POP3_Client client = new POP3_Client())
            {
                client.Connect(host, port, ssl);
                client.Authenticate(userName, password, false);
                var messages = client.Messages;
                foreach(POP3_ClientMessage message in messages)
                {
                    Mail_Message email = Mail_Message.ParseFromByte(message.MessageToByte());
                    Item item = new Item
                    {
                        Sender = email.From.ToString().Replace("\"",""),
                        Subject = email.Subject,
                        SendDate = email.Date.ToString(),
                        Date = email.Date.ToString(),
                    };
                    items.Add(item);
                }
            }
            items = items.OrderByDescending(i => i.SendDate).ToList<Item>();
            json = JsonConvert.SerializeObject(items);
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
    public class Item
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string SendDate { get; set; }
        public string Date { get; set; }
    }
}