using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
            string userName = this.txtUserName.Text;
            var users = Membership.GetAllUsers();
            foreach(MembershipUser user in users)
            {
                if(user.UserName == userName)
                {
                    Membership.DeleteUser(userName);
                }
            }
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string userId = (string )new SqlCommand("SELECT UserID FROM TU_Users WHERE RealName='" + userName + "'", connection).ExecuteScalar();
                SqlCommand command = new SqlCommand("DELETE FROM TU_Users WHERE RealName='" + userName + "'",connection);
                int row1 = new SqlCommand("DELETE FROM TU_Employees WHERE UserID='" + userId + "'", connection).ExecuteNonQuery();
                int row = command.ExecuteNonQuery();
                if(row1 + row > 2)
                {
                    Response.Write("用户删除成功");
                }
            }
        }
    }
}