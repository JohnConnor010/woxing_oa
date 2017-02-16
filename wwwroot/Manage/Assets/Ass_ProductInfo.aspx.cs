using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_ProductInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productId = Request.QueryString["ProductID"];
                WX.Ass.Warehouse.MODEL model = WX.Ass.Warehouse.GetModel("SELECT * FROM Ass_Warehouse WHERE ProductID='" + productId + "'");
                this.lblProductName.Text = model.ProductName.value.ToString();
                this.lblProductID.Text = model.ProductID.value.ToString();
                this.lblQuantity.Text = model.Quantity.value.ToString();
                this.lblProductType.Text = GetCategoryByID(model.CategoryID.value.ToString());
                this.lblUnit.Text = model.Unit.value.ToString();
                this.lblPrice.Text = model.Price.value.ToString();
                this.lblSpecification.Text = model.Specification.value.ToString();
                this.lblSupplier.Text = GetSupplierByID(model.Suppliers.value.ToString());
                this.lblColor.Text = model.Color.value.ToString();
                this.lblBrand.Text = model.Brand.value.ToString();
                this.lblModel.Text = model.Model.value.ToString();
                this.lblRemark.Text = model.Remark.value.ToString();
                if (string.IsNullOrEmpty(model.ProductPhoto.value.ToString()))
                {
                    this.Image1.ImageUrl = "~/Manage/images/no picture.jpg";
                }
                else
                {
                    this.Image1.ImageUrl = "../../" + model.ProductPhoto.value.ToString();
                }
            }
        }
        private string GetSupplierByID(string supplierId)
        {
            return (string)XSql.GetValue("SELECT CompanyName FROM Ass_Suppliers WHERE SupplierID=" + supplierId);
        }
        private string GetCategoryByID(string categoryId)
        {
            return (string)XSql.GetValue("SELECT Name FROM Ass_Category WHERE ID=" + categoryId);
        }
    }
}