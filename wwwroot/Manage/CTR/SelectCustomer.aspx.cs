using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CTR
{
	public partial class SelectCustomer : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                InitComponent();
            }
		}
        private void InitComponent()
        {
            DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'CRM_Category','ID','CategoryName','ParentID','ID',0,1,5");
            this.ddlCategoryID.DataSource = categoryData;
            this.ddlCategoryID.DataValueField = "id";
            this.ddlCategoryID.DataTextField = "name";
            this.ddlCategoryID.DataBind();
        }

        protected void ddlCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryId = this.ddlCategoryID.SelectedItem.Value;
            if (categoryId != "1")
            {
                DataTable customerData = XSql.GetDataTable("SELECT CustomerName FROM CRM_Customers WHERE CategoryID=" + categoryId);
                this.lstCustomers.DataSource = customerData;
                this.lstCustomers.DataTextField = "CustomerName";
                this.lstCustomers.DataValueField = "CustomerName";
                this.lstCustomers.DataBind();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "a", "window.parent.SetSelectedTab('选择客户')", true);
        }
	}
}