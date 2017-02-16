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
    public partial class SelectSupplier : System.Web.UI.Page
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
            DataTable categoryData = XSql.GetDataTable("SELECT * FROM PDT_SupplierCategory");
            this.ddlCategoryID.DataSource = categoryData;
            this.ddlCategoryID.DataValueField = "ID";
            this.ddlCategoryID.DataTextField = "Name";
            this.ddlCategoryID.DataBind();
            this.ddlCategoryID.Items.Insert(0, new ListItem("--请选择供应商类别--", "0"));
        }

        protected void ddlCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string categoryId = this.ddlCategoryID.SelectedItem.Value;
            if (categoryId != "0")
            {
                DataTable supplierData = XSql.GetDataTable("SELECT CompanyName,ContactName FROM Ass_Suppliers WHERE SupplierCategoryID=" + categoryId);
                this.lstSuppliers.DataSource = supplierData;
                this.lstSuppliers.DataTextField = "CompanyName";
                this.lstSuppliers.DataValueField = "CompanyName";
                this.lstSuppliers.DataBind();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "a", "window.parent.SetSelectedTab('选择供应商')", true);
        }
    }
}