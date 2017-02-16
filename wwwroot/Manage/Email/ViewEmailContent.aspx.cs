using HtmlAgilityPack;
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
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Email
{
    public partial class ViewEmailContent : System.Web.UI.Page
    {
        static string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {            
            string subject = Request.QueryString["subject"];
            string date = Request.QueryString["date"];
            this.hideSubject.Value = subject;
            this.hideDate.Value = date;
            //using (POP3_Client client = new POP3_Client())
            //{
            //    client.Connect(host, port, ssl);
            //    client.Authenticate(userName, password, false);
            //    var messages = client.Messages;
                
            //    foreach(POP3_ClientMessage message in messages)
            //    {
            //        Mail_Message email = Mail_Message.ParseFromByte(message.MessageToByte());
            //        if(email.Subject.Equals(subject) && email.Date == Convert.ToDateTime(date))
            //        {
            //            this.ltlTitle.Text = email.Subject;

            //            this.ltlContent.Text = email.BodyHtmlText;
            //        }
            //    }
                
            //}
        }
        [WebMethod]
        public static string SearchSingleEmail(string subject,string date)
        {
            string userId = new WX.WXUser().UserID;
            DataTable table;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM [TU_EmailPOP] WHERE UserID='" + userId + "'", connection);
                table = new DataTable();
                sda.Fill(table);
            }
            string host = string.Empty;
            string userName = string.Empty;
            string password = string.Empty;
            int port = 995;
            bool ssl = true;
            foreach (DataRow row in table.Rows)
            {
                host = row["Host"].ToString();
                userName = row["UserName"].ToString();
                password = row["Password"].ToString();
                port = int.Parse(row["Port"].ToString());
            }
            string json = string.Empty;
            using (POP3_Client client = new POP3_Client())
            {
                client.Connect(host, port, ssl);
                client.Authenticate(userName, password, false);
                var messages = client.Messages;

                foreach (POP3_ClientMessage message in messages)
                {
                    Mail_Message email = Mail_Message.ParseFromByte(message.MessageToByte());
                    if (email.Subject.Equals(subject) && email.Date == Convert.ToDateTime(date))
                    {
                        TestItem item = new TestItem
                        {
                            Subject = email.Subject,
                            Body = email.BodyHtmlText
                        };
                        json = JsonConvert.SerializeObject(item);
                    }
                }

            }
            return json;
        }
    }
    public class TestItem
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}