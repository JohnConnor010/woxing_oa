using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using System.Data;

namespace wwwroot.Manage.CTR
{
    public partial class ProductShow : System.Web.UI.Page
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
            DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'PDT_ProductCategory','ID','Name','ParentID','ID',0,1,5");
            this.ddlProductCategory.DataSource = categoryData;
            this.ddlProductCategory.DataTextField = "name";
            this.ddlProductCategory.DataValueField = "id";
            this.ddlProductCategory.DataBind();

            DataTable unitData = XSql.GetDataTable("SELECT * FROM Ass_Unit");
            this.ddlUnits.DataSource = unitData;
            this.ddlUnits.DataValueField = "ID";
            this.ddlUnits.DataTextField = "UnitName";
            this.ddlUnits.DataBind();
            if (Request["ProductID"] != null && Request["ProductID"] != "")
            {
                WX.PDT.Product.MODEL product = WX.Request.rProduct;
                this.rIsEnable.SelectedValue = product.IsEnable.ToString();
                this.txtProductID.Text = product.ProductID.ToString();
                this.txtProductName.Text = product.ProductName.ToString();
                this.ddlProductCategory.SelectedValue = product.CategoryID.ToString();
                this.txtSpecification.Text = product.Specification.ToString();
                this.ddlUnits.SelectedValue = product.Units.ToString();
                this.txtSalesPrice.Text = product.SalesPrice.ToString();
                this.txtDiscountedPrice.Text = product.DiscountedPrice.ToString();
                this.txtCostPrice.Text = product.CostPrice.ToString();
                this.txtRemark.Text = product.Remark.ToString();
                this.txtServices.Text = product.Services.ToString();
            }
            if (WX.Main.GetConfigItem("Product_ISDept") == "1")
            {
                pdept.Visible = true;
                string sSql = "select pp.*,td.Name DeptName from PDT_ProductDept pp left join TE_Departments td on pp.DeptID=td.ID where pp.ProductID=" + WX.Request.rProductId;
                DataList1.DataSource = ULCode.QDA.XSql.GetDataTable(sSql);
                DataList1.DataBind();
            }
        }
    }
}