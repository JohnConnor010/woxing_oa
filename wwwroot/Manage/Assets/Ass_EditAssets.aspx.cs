using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_EditAssets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
            }
            if (!IsPostBack)
            {
                InitComponent();
                string id = WX.Request.rWarehouseID.ToString();
                WX.Ass.Warehouse.MODEL warehouseModel = WX.Request.rWarehouse;
                this.txtProductID.Text = warehouseModel.ProductID.value.ToString();
                this.txtProductName.Text = warehouseModel.ProductName.value.ToString();
                this.txtQuantity.Text = warehouseModel.Quantity.value.ToString();
                this.ddlCategory.SelectedValue = warehouseModel.CategoryID.value.ToString();
                if (ddlUnit.Items.FindByText(warehouseModel.Unit.value.ToString()) != null)
                {
                    this.ddlUnit.Items.FindByText(warehouseModel.Unit.value.ToString()).Selected = true;
                }
                this.txtPrice.Text = warehouseModel.Price.value.ToString();
                this.ddlSuppliers.SelectedValue = warehouseModel.Suppliers.value.ToString();
                this.txtSpecification.Text = warehouseModel.Specification.value.ToString();
                this.txtColor.Text = warehouseModel.Color.value.ToString();
                this.txtBrand.Text = warehouseModel.Brand.value.ToString();
                this.txtModel.Text = warehouseModel.Model.value.ToString();
                this.hidden_ProductPhoto.Value = warehouseModel.ProductPhoto.value.ToString();
                this.txtProductPhoto.Text = warehouseModel.ProductPhoto.value.ToString();
                this.txtRemarks.Text = warehouseModel.Remark.value.ToString();
            }
        }
        private void InitComponent()
        {
            string productId = NumberRule.NewDataModel(15).GetValue(15);
            this.txtProductID.Text = productId;

            DataTable unitData = XSql.GetDataTable("SELECT ID,UnitName FROM Ass_Unit");
            this.ddlUnit.DataSource = unitData;
            this.ddlUnit.DataTextField = "UnitName";
            this.ddlUnit.DataValueField = "ID";
            this.ddlUnit.DataBind();

            DataTable suppliersData = XSql.GetDataTable("SELECT SupplierID,CompanyName FROM Ass_Suppliers");
            this.ddlSuppliers.DataSource = suppliersData;
            this.ddlSuppliers.DataTextField = "CompanyName";
            this.ddlSuppliers.DataValueField = "SupplierID";
            this.ddlSuppliers.DataBind();

            DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'Ass_Category','ID','Name','ParentID','ID',0,1,5");
            this.ddlCategory.DataSource = categoryData;
            this.ddlCategory.DataTextField = "name";
            this.ddlCategory.DataValueField = "id";
            this.ddlCategory.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }
            //2.获取用户变量
            string id = WX.Request.rWarehouseID.ToString();
            WX.Ass.Warehouse.MODEL warehouse = WX.Request.rWarehouse;
            warehouse.ProductID.value = this.txtProductID.Text.Trim();
            warehouse.ProductName.value = this.txtProductName.Text.Trim();
            warehouse.Quantity.value = this.txtQuantity.Text.Trim();
            warehouse.CategoryID.value = this.ddlCategory.SelectedItem.Value;
            warehouse.UsedQuantity.value = 0;
            warehouse.Unit.value = this.ddlUnit.SelectedItem.Text;
            warehouse.Price.value = this.txtPrice.Text.Trim();
            warehouse.Suppliers.value = this.ddlSuppliers.SelectedItem.Value;
            warehouse.Specification.value = this.txtSpecification.Text.Trim();
            warehouse.Color.value = this.txtColor.Text.Trim();
            warehouse.Brand.value = this.txtBrand.Text.Trim();
            warehouse.Model.value = this.txtModel.Text.Trim();
            warehouse.ProductPhoto.value = this.txtProductPhoto.Text.Trim();
            warehouse.LastTime.value = DateTime.Now.ToString("yyyy-MM-dd");
            warehouse.Remark.value = this.txtRemarks.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row = 0;
            row = warehouse.Update();
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "产品信息修改成功！", null);
            }
            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                ULCode.Debug.Alert("产品信息修改成功！", "Ass_AssetsList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("产品信息修改失败！", "Ass_AssetsList.aspx");
            }
        }
    }
}