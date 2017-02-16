using Aspose.Email.Mail;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Email
{
    public partial class SendEmail : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string to = Request.Form["ctl00$ContentPlaceHolder$combobox1"];
            string subject = this.txtSubject.Text.Trim();
            string body = Request.Form["ctl00$ContentPlaceHolder$txtContent"];
            string from = "sdcecn01@126.com";
            string attach = this.hideFiles.Value;
            MailMessage message = new MailMessage();
            message.To = to;
            if (this.chkCC.Checked)
            {
                string cc = Request.Form["ctl00$ContentPlaceHolder$combobox2"];
                MailAddressCollection addresses = new MailAddressCollection();
                foreach (var address in cc.Split(','))
                {
                    addresses.Add(address);
                }
                message.CC = addresses;
            }
            if (!string.IsNullOrEmpty(attach))
            {
                if (attach.Split(',').Count() > 0)
                {
                    AttachmentCollection attachments = message.Attachments;
                    foreach (var attachFile in attach.Split(','))
                    {
                        attachments.Add(new Attachment(Server.MapPath(attachFile.Trim())));
                    }
                }
            }
            message.From = from;
            message.Subject = subject;
            SmtpClient client = GetSmtpClient();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(body);
            HtmlNode rootNode = document.DocumentNode;
            var urls = rootNode.Descendants("img").Select(a => a.GetAttributeValue("src", null)).Where(s => !string.IsNullOrEmpty(s)).ToArray();

            for (int i = 0; i < urls.Count(); i++)
            {
                body = body.Replace(@urls[i], "cid:pic" + i);
            }
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            List<LinkedResource> resources = new List<LinkedResource>();
            for (int i = 0; i < urls.Count(); i++)
            {
                string picPath = Server.MapPath(urls[i]);
                string mediaType = GetMediaType(Path.GetExtension(picPath));
                LinkedResource resource = new LinkedResource(picPath, mediaType);
                resource.ContentId = "pic" + i;
                resources.Add(resource);
                htmlBody.LinkedResources.Add(resource);
            }

            message.AlternateViews.Add(htmlBody);
            
            try
            {
                client.Send(message);
                Response.Write("<script>alert('邮件发送成功！');window.location.href='" + Request.RawUrl + "'</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('邮件发送失败，失败原因：" + ex.Message + "');window.location.href='" + Request.RawUrl + "'</script>");

            }
        }
        public string GetMediaType(string extension)
        {
            switch(extension)
            {
                case ".gif":
                    return "image/gif";
                case ".jpg":
                    return "image/jpeg";
                default:
                    return "image/jpeg";
            }
        }
        public static SmtpClient GetSmtpClient()
        {
            SmtpClient client = new SmtpClient("smtp.126.com", 25, "sdcecn01@126.com", "325111");
            client.SecurityMode = SmtpSslSecurityMode.Explicit;
            client.EnableSsl = true;
            return client;
        }
    }
}