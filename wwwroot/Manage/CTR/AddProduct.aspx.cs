using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Flow.Model;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CTR
{
    public partial class AddProduct : System.Web.UI.Page
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
                InitComponent();
            }
        }
        private void InitComponent()
        {
            string productId = NumberRule.NewDataModel(16).GetValue(16);
            this.txtProductID.Text = productId;

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
            if (WX.Main.GetConfigItem("Product_ISDept") == "1" && WX.Main.GetConfigItem("Product_OneDept") == "1")
                BindDropList();
            if (Request["ProductID"] != null && Request["ProductID"] != "")
            {
                MenuBar1.Key = "Sale_Product_Edit";
                MenuBar1.Param1 = "{Q:ProductID}";
                MenuBar1.CurIndex = 2;
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
                if (WX.Main.GetConfigItem("Product_ISDept") == "1" && WX.Main.GetConfigItem("Product_OneDept") == "1")
                {
                    WX.PDT.ProductDept.MODEL productdept = WX.PDT.ProductDept.GetModel("select top 1 * from PDT_ProductDept where ProductID="+WX.Request.rProductId+" order by ID desc");
                    if (productdept != null)
                    {
                        ProductDeptID.SelectedValue = productdept.DeptID.ToString();
                        MonthFee.Text = productdept.MonthFee.ToString();
                        Fee.Text = productdept.Fee.ToString();
                        Feetype1.SelectedValue = productdept.MonthFeeType.ToString();
                        Feetype2.SelectedValue = productdept.FeeType.ToString();
                        txtDeptRemarks.Text = productdept.Remarks.ToString();
                    }
                }
            }
            if (WX.Main.GetConfigItem("Product_ISDept") == "1" && WX.Main.GetConfigItem("Product_OneDept") == "1")
                pdept.Visible = true;
        }
        private void BindDropList()
        {
            string[] str = WX.PDT.ProductDept.FeeTypestr;
            for (int i = 0; i < str.Length; i++)
            {
                Feetype1.Items.Add(new ListItem(str[i], i.ToString()));
                Feetype2.Items.Add(new ListItem(str[i], i.ToString()));
            }

            WX.Data.Dict.BindListCtrl_DeptList(this.ProductDeptID, null, null, null);
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
            WX.PDT.Product.MODEL productModel;
            if (Request["ProductID"] != null && Request["ProductID"] != "")
                productModel = WX.Request.rProduct;
            else
                productModel= WX.PDT.Product.NewDataModel();
            productModel.ProductID.value = this.txtProductID.Text.Trim();
            productModel.ProductName.value = this.txtProductName.Text.Trim();
            productModel.IsEnable.value = this.rIsEnable.SelectedValue;
            productModel.CategoryID.value = this.ddlProductCategory.SelectedItem.Value;
            productModel.Specification.value = this.txtSpecification.Text.Trim();
            productModel.Units.value = this.ddlUnits.SelectedItem.Value;
            productModel.SalesPrice.value = this.txtSalesPrice.Text.Trim();
            productModel.DiscountedPrice.value = this.txtDiscountedPrice.Text.Trim();
            productModel.CostPrice.value = this.txtCostPrice.Text.Trim();
            productModel.Remark.value = this.txtRemark.Text.Trim();
            productModel.Services.value = this.txtServices.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row;
            if (Request["ProductID"] != null && Request["ProductID"] != "")
                row = productModel.Update();
            else
                row = productModel.Insert(true);
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            if (row > 0)
            {
                //6.登记日志
                if (Request["ProductID"] != null && Request["ProductID"] != "")
                    WX.Main.AddLog(WX.LogType.Default, "产品信息修改成功！", null);
                else
                    WX.Main.AddLog(WX.LogType.Default, "产品信息添加成功！", null);
                if (WX.Main.GetConfigItem("Product_ISDept") == "1")
                {
                    if (WX.Main.GetConfigItem("Product_OneDept") == "1")
                    {
                        WX.PDT.ProductDept.MODEL productdept;
                        if (Request["ProductID"] != null && Request["ProductID"] != "")
                        {
                            productdept = WX.PDT.ProductDept.GetModel("select top 1 * from PDT_ProductDept where ProductID=" + WX.Request.rProductId + " order by ID desc");
                            productdept.DeptID.value = ProductDeptID.SelectedValue;
                            try
                            {
                                productdept.MonthFee.value = Convert.ToDecimal(MonthFee.Text.Trim());
                            }
                            catch { }
                            productdept.MonthFeeType.value = Feetype1.SelectedValue;
                            try
                            {
                                productdept.Fee.value = Convert.ToDecimal(Fee.Text.Trim());
                            }
                            catch { }
                            productdept.FeeType.value = Feetype2.SelectedValue;
                            productdept.Remarks.value = txtDeptRemarks.Text;
                            productdept.Update();
                        }
                        else
                        {
                            productdept = WX.PDT.ProductDept.NewDataModel();
                            productdept.ProductID.value = row;
                            productdept.DeptID.value = ProductDeptID.SelectedValue;
                            try
                            {
                                productdept.MonthFee.value = Convert.ToDecimal(MonthFee.Text.Trim());
                            }
                            catch { }
                            productdept.MonthFeeType.value = Feetype1.SelectedValue;
                            try
                            {
                                productdept.Fee.value = Convert.ToDecimal(Fee.Text.Trim());
                            }
                            catch { }
                            productdept.FeeType.value = Feetype2.SelectedValue;
                            productdept.Remarks.value = txtDeptRemarks.Text;
                            productdept.Insert();
                        }
                    }
                    else if( Request["ProductID"] == null)
                        ULCode.Debug.Alert("产品信息提交成功，请添加产品部门！", "AddProductDept.aspx?ProductID=" + row);
                }
            
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert( "产品信息提交成功！", "ProductList.aspx");
            }
            else
            {
                //7.返回处理结果或返回其它页面。
                ULCode.Debug.Alert("产品信息提交失败！");
            }
        }
    }
}