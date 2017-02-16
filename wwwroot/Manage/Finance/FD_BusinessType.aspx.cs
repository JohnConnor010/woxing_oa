using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Finance
{
    public partial class FD_BusinessType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TypeTextBox.DataSource = SqlDataSource2;
                TypeTextBox.DataBind();
            }
            else
            {
                SqlDataSource1.SelectCommand = "SELECT * FROM [Count_Type] where DivisionID=" + TypeTextBox.SelectedValue + " order by DivisionID asc, ID asc";
            }
        }
        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView1.DataBind();
        }

        protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ListView1.DataBind();
        }
        protected void InsertButton_Click(object sender, EventArgs e)
        {
            string sqlstr = "INSERT INTO [Count_Type] ([Name],[Demo],[DivisionID]) VALUES ('"+NameTextBox.Text+"','"+TextBox2.Text+"',"+TypeTextBox.SelectedValue+")";
            ULCode.QDA.XSql.Execute(sqlstr);
            ListView1.DataBind();
            NameTextBox.Text = "";
            TextBox2.Text = "";
        }
    }
}