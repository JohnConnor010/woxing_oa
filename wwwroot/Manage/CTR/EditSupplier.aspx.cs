using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using WX;

namespace wwwroot.Manage.CTR
{
    public partial class EditSupplier : System.Web.UI.Page
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
            DataTable categoryData = XSql.GetDataTable("SELECT * FROM PDT_SupplierCategory");
            this.ddlCateogryID.DataSource = categoryData;
            this.ddlCateogryID.DataValueField = "ID";
            this.ddlCateogryID.DataTextField = "Name";
            this.ddlCateogryID.DataBind();
            this.ddlCateogryID.Items.Insert(0, new ListItem("--请选择--", ""));
            string supplierID = WX.Request.rSupplierID.ToString();
            WX.Ass.Supplier.MODEL model = WX.Request.rSupplier;
            this.txtCompanyName.Text = model.CompanyName.value.ToString();
            this.txtTelephone.Text = model.Telephone.ToString();
            this.txtFax.Text = model.Fax.ToString();
            this.txtZipCode.Text = model.ZipCode.ToString();
            this.txtQQNumber.Text = model.QQNumber.ToString();
            this.txtEmail.Text = model.Email.ToString();
            this.txtWebSite.Text = model.WebSite.ToString();
            this.txtMobilePhone.Text = model.MobilePhone.ToString();
            this.txtAddress.Text = model.Address.ToString();
            this.txtContactName.Text = model.ContactName.ToString();
            this.ddlCateogryID.SelectedValue = model.SupplierCategoryID.ToString();
            this.txtRemark.Text = model.Remark.ToString();

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
            string supplierID = WX.Request.rSupplierID.ToString();
            WX.Ass.Supplier.MODEL supplierModel = WX.Request.rSupplier;
            supplierModel.CompanyName.value = this.txtCompanyName.Text.Trim();
            supplierModel.Telephone.value = this.txtTelephone.Text.Trim();
            supplierModel.Fax.value = this.txtFax.Text.Trim();
            supplierModel.MobilePhone.value = this.txtMobilePhone.Text.Trim();
            supplierModel.ZipCode.value = this.txtZipCode.Text.Trim();
            supplierModel.QQNumber.value = this.txtQQNumber.Text.Trim();
            supplierModel.Email.value = this.txtEmail.Text.Trim();
            supplierModel.WebSite.value = this.txtWebSite.Text.Trim();
            supplierModel.Address.value = this.txtAddress.Text.Trim();
            supplierModel.ContactName.value = this.txtContactName.Text.Trim();
            supplierModel.SupplierCategoryID.value = this.ddlCateogryID.SelectedItem.Value;
            supplierModel.Remark.value = this.txtRemark.Text.Trim();
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row = supplierModel.Update();
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(LogType.Default, "供应商信息修改成功！", null);
            }
            if (row > 0)
            {
                ULCode.Debug.Alert(this, "供应商信息修改成功！", "SupplierList.aspx");
            }
            else
            {
                ULCode.Debug.Alert(this, "供应商信息修改失败！", "SupplierList.aspx");
            }
            //7.返回处理结果或返回其它页面。
        }
    }
}