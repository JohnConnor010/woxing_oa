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
    public partial class Ass_AddAssets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1.验证当前用户页面权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();

                return;
            }
            if (!IsPostBack)
            {
                InitData();                
            }
        }
        private void InitData()
        {
            //string productId = NumberRule.NewDataModel(15).GetValue(15);
            //this.txtProductID.Text = productId;

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
            //2.取得用户变量
            if (this.SelectedProduct.Value == "false")
            {
                WX.Ass.Warehouse.MODEL warehouse = WX.Ass.Warehouse.NewDataModel();
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
                int singleRow = 0;
                row = warehouse.Save();
                //填写主要业务逻辑代码
                //5.（用户及业务对象）统计与状态

                //6.登记日志
                if (row > 0)
                {
                    string type = "入库";
                    string opUserID = WX.Authentication.GetUserID();
                    string opTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string opIP = Request.UserHostAddress;
                    string userId = this.ddlSuppliers.SelectedItem.Value;
                    string departmentId = "";
                    string quantity = this.txtQuantity.Text.Trim();
                    string productID = this.txtProductID.Text.Trim();
                    string content = this.txtRemarks.Text.Trim();
                    string unit = this.ddlUnit.SelectedItem.Text;
                    string price = this.txtPrice.Text.Trim();
                    WX.Ass.Log.MODEL logModel = WX.Ass.Log.NewDataModel();
                    logModel.Type.value = type;
                    logModel.OpUserID.value = opUserID;
                    logModel.OpTime.value = opTime;
                    logModel.OpIP.value = opIP;
                    logModel.UserID.value = userId;
                    logModel.DepartmentID.value = departmentId;
                    logModel.Quantity.value = quantity;
                    logModel.ProductID.value = productID;
                    logModel.Content.value = content;
                    logModel.Unit.value = unit;
                    logModel.Price.value = price;
                    singleRow = logModel.Save();
                    if (singleRow > 0)
                    {
                        WX.Main.AddLog(WX.LogType.Default, "产品日志添加成功！", null);
                        WX.Main.AddLog(WX.LogType.Default, "产品信息添加成功！", null);
                    }

                }
                //7.返回处理结果或返回其它页面。
                if (singleRow > 0)
                {
                    ULCode.Debug.Confirm("产品信息添加成功！", "Ass_AddAssets.aspx", "Ass_AssetsList.aspx");
                }
                else
                {
                    ULCode.Debug.Alert("产品信息添加失败！", "Ass_AddAssets.aspx");
                }
            }
            else
            {
                WX.Ass.Warehouse.MODEL warehouse = WX.Ass.Warehouse.NewDataModel(this.SelectedID.Value);
                warehouse.Quantity.value = Convert.ToInt32(warehouse.Quantity.value) + Convert.ToInt32(this.txtQuantity.Text);
                int row = warehouse.Update();
                int singleRow = 0;
                //6.登记日志
                if (row > 0)
                {
                    string type = "入库";
                    string opUserID = WX.Authentication.GetUserID();
                    string opTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string opIP = Request.UserHostAddress;
                    string userId = this.ddlSuppliers.SelectedItem.Value;
                    string departmentId = "";
                    string quantity = this.txtQuantity.Text.Trim();
                    string productID = this.txtProductID.Text.Trim();
                    string content = this.txtRemarks.Text.Trim();
                    string unit = this.ddlUnit.SelectedItem.Text;
                    string price = this.txtPrice.Text.Trim();
                    WX.Ass.Log.MODEL logModel = WX.Ass.Log.NewDataModel();
                    logModel.Type.value = type;
                    logModel.OpUserID.value = opUserID;
                    logModel.OpTime.value = opTime;
                    logModel.OpIP.value = opIP;
                    logModel.UserID.value = userId;
                    logModel.DepartmentID.value = departmentId;
                    logModel.Quantity.value = quantity;
                    logModel.ProductID.value = productID;
                    logModel.Content.value = content;
                    logModel.Unit.value = unit;
                    logModel.Price.value = price;
                    singleRow = logModel.Save();
                    if (singleRow > 0)
                    {
                        WX.Main.AddLog(WX.LogType.Default, "产品日志添加成功！", null);
                        WX.Main.AddLog(WX.LogType.Default, "产品信息添加成功！", null);
                    }

                }
                //7.返回处理结果或返回其它页面。
                if (singleRow > 0)
                {
                    ULCode.Debug.Confirm("产品信息添加成功！", "Ass_AddAssets.aspx", "Ass_AssetsList.aspx");
                }
                else
                {
                    ULCode.Debug.Alert("产品信息添加失败！", "Ass_AddAssets.aspx");
                }
            }
            
            
            
        }
    }
}