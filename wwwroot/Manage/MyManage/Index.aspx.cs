using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.MyManage
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                InitMenu(this.Menu1.Items, 0);
            }
        }
        private void InitMenu(MenuItemCollection items, int parentId)
        {
            string queryString = "SELECT ID,Name,ParentID FROM TE_Departments";
            DataSet ds = RunQuery(queryString);
            MenuItem item;
            var rows = ds.Tables[0].Select("ParentID=" + parentId);
            foreach (DataRow row in rows)
            {
                item = new MenuItem();
                item.Text = row["Name"].ToString();
                item.Value = row["ID"].ToString();
                item.NavigateUrl = "SelectDepartmentInfo.aspx?DepartmentID=" + row["ID"].ToString();
                string commandText = string.Format("SELECT UserID,DepartmentID,RealName,State FROM TU_Users WHERE DepartmentID={0} ORDER BY State ASC", row["ID"].ToString());
                DataSet innerDs = RunQuery(commandText);

                InitMenu(item.ChildItems, Convert.ToInt32(row["ID"]));
                if (innerDs != null)
                {
                    if (innerDs.Tables.Count > 0)
                    {
                        foreach (DataRow innerRow in innerDs.Tables[0].Rows)
                        {
                            MenuItem innerItem = new MenuItem();
                            innerItem.Text = innerRow["RealName"].ToString();
                            innerItem.Value = innerRow["UserID"].ToString();
                            innerItem.NavigateUrl = "SelectPersonInfo.aspx?UserID=" + innerRow["UserID"].ToString();
                            if (Convert.ToInt32(innerRow["State"]) == 40)
                            {
                                innerItem.ImageUrl = "images/man_icon_offline.gif";
                            }
                            else
                            {
                                innerItem.ImageUrl = "images/man_icon.gif";
                            }
                            item.ChildItems.Add(innerItem);
                        }
                    }
                }
                items.Add(item);

            }
        }
        private DataSet RunQuery(string queryString)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["WXOAConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(queryString, connection);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds;
                }
                catch (Exception)
                {

                    return null;
                }

            }
        }
    }
}