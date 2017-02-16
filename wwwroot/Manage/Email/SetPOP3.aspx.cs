using iNethinkCMS.Command;
using LumiSoft.Net.POP3.Client;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace wwwroot.Manage.Email
{
    public partial class SetPOP3 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
         
        protected void Page_Load(object sender, EventArgs e)
        {
            WX.WXUser user = WX.Main.CurUser;
            if(!this.IsPostBack)
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string cmdText = String.Format("SELECT * FROM [TU_EmailPOP] WHERE UserID='{0}'", user.UserID);
                    SqlCommand command = new SqlCommand(cmdText,connection);
                    SqlDataAdapter sda = new SqlDataAdapter(command);
                    DataTable table1 = new DataTable();
                    sda.Fill(table1);
                    foreach(DataRow row in table1.Rows)
                    {
                        this.txtHostAddress.Text = row["Host"].ToString();
                        this.txtUserName.Text = row["UserName"].ToString();
                        this.txtPassword.Attributes.Add("value", row["Password"].ToString());
                        this.txtPort.Text = row["Port"].ToString();
                    }
                }
            }
        }

        protected void btnTestConnection_Click(object sender, EventArgs e)
        {
            int port = 993;
            WX.WXUser user = WX.Main.CurUser;
            string host = this.txtHostAddress.Text.Trim();
            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();
            bool b = int.TryParse(this.txtPort.Text, out port);
            bool ssl = this.chkSSL.Checked;
            using (POP3_Client client = new POP3_Client())
            {
                try
                {
                    client.Connect(host, port, ssl);
                    client.Authenticate(userName, password, false);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string insertText = "INSERT INTO [TU_EmailPOP] ([UserID],[Host],[UserName],[Password],[Port],[SSL]) VALUES (@UserID,@Host,@UserName,@Password,@Port,@SSL)";
                        string updateText = "UPDATE [TU_EmailPOP] SET [UserID]=@UserID,[Host]=@Host,[UserName]=@UserName,[Password]=@Password,[Port]=@Port,[SSL]=@SSL";

                        SqlCommand command;
                        command = new SqlCommand("SELECT COUNT(ID) FROM [TU_EmailPOP] WHERE UserID='" + user.UserID + "'", connection);
                        int row = (int)command.ExecuteScalar();
                        if (row > 0)
                        {
                            command = new SqlCommand(updateText, connection);
                        }
                        else
                        {
                            command = new SqlCommand(insertText, connection);
                        }
                        command.Parameters.AddWithValue("@UserID", user.UserID);
                        command.Parameters.AddWithValue("@Host", host);
                        command.Parameters.AddWithValue("@UserName", userName);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Port", port);
                        command.Parameters.AddWithValue("@SSL", true);
                        command.ExecuteNonQuery();
                        MessageBox.ShowAndRedirect(this, "邮箱配置成功", Request.RawUrl);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.ShowAndRedirect(this, "邮箱配置失败：" + ex.Message, Request.RawUrl);
                }
            }
        }
    }
}