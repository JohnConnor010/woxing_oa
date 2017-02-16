using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using ULCode.QDA;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace wwwroot.Manage.Sys
{
    public partial class User_UserList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true);
            }
        }
        private void InitComponent(bool start)
        {
            string con = null;
            if (!String.IsNullOrEmpty(this.tbKeyWords.Text.Trim()))
            {
                con = String.Format(" and RealName like '%{0}%'",this.tbKeyWords.Text);
            }
            string sql = String.Format("SELECT * FROM vw_Users WHERE CompanyID={0} {1}", WX.Request.rCompanyId, con);
            //Response.Write(sql);
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }

            //DataTable dataTable = ULCode.QDA.XSql.GetDataTable(sql);
            DataTable logData = WX.Main.GetPagedRows(sql, 0, "ORDER BY IsLockedOut asc,Grade desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            //Response.Write(logData.Rows.Count);
            this.SupplierRepeater.DataSource = logData;
            this.SupplierRepeater.DataBind(); 
            this.AspNetPager1.AlwaysShow = true;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitComponent(false);
        }
        protected void Query(object sender, EventArgs e)
        {
            this.InitComponent(true);
        }
        protected void QueryAll(object sender, EventArgs e)
        {
            this.tbKeyWords.Text = String.Empty;
            this.InitComponent(true);
        }

        protected void lnkDelete_Command(object sender, CommandEventArgs e)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
            string userName = e.CommandArgument.ToString();
            var users = Membership.GetAllUsers();
            Membership.DeleteUser(userName);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Guid userId = (Guid)new SqlCommand("SELECT UserID FROM TU_Users WHERE RealName='" + userName + "'", connection).ExecuteScalar();
                SqlCommand command = new SqlCommand("DELETE FROM TU_Users WHERE RealName='" + userName + "'", connection);
                int row1 = command.ExecuteNonQuery();
                command = new SqlCommand("DELETE FROM TU_Employees WHERE UserID='" + userId + "'", connection);
                int row2 = command.ExecuteNonQuery();
                if (row1 + row2 == 2)
                {
                    Response.Write("<script>alert('用户删除成功！');window.location.href='" + Request.RawUrl + "'</script>");
                }
            }
        }
    }
}