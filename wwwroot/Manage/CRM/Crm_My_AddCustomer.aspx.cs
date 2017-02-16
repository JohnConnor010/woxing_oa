using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_My_AddCustomer : System.Web.UI.Page
    {
        public string mess = "";
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
            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel();
            string customerId = (string)XSql.GetValue("SELECT TOP 1 CustomerID FROM CRM_Customers ORDER BY ID DESC");
            this.txtCustomerID.Text = customer.CreateCustomerID(customerId);
            
            WX.Data.Dict.BindListCtrl_InnerCategory(this.ddlCustomerCategory, null, "#--请选择客户内部类型--", null);
            WX.Data.Dict.BindListCtrl_CompanyNature(this.ddlCompanyNature, null, "#--请选择客户性质--", null);
            WX.Data.Dict.BindListCtrl_Source(this.ddlSource, null, "#--请选择客户来源--", null);
            WX.Data.Dict.BindListCtrl_Industry(this.ddlIndustry, null, "#--请选择客户行业--", null);
           
            var province = XSql.GetDataTable("SELECT code,name FROM CRM_Province");            
            this.ddlProvince.DataSource = province;
            this.ddlProvince.DataTextField = "name";
            this.ddlProvince.DataValueField = "code";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, new ListItem("--请选择--", "0"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //1.验证用户权限
            if (!this.Master.A_Edit)
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
                return;
            }

            WX.CRM.Customer.MODEL cmodel = WX.CRM.Customer.GetModel("select * from CRM_Customers where  CustomerName='" + this.txtCustomerName.Text.Trim() + "'");
            if (cmodel != null && cmodel.ID.ToInt32() > 0)
            {
                mess = "alert('客户名称已被" + WX.CommonUtils.GetRealNameListByUserIdList(cmodel.EmployeeID.ToString()) + "录入，提交失败！');";
                return;
            }
            //2.取得用户变量

            WX.CRM.Customer.MODEL customer = WX.CRM.Customer.NewDataModel();
            //---客户基本信息
            customer.CustomerID.value = this.txtCustomerID.Text.Trim();
            customer.CustomerName.value = this.txtCustomerName.Text.Trim();
            customer.CustomerZJM.value = this.txtCustomerZJM.Text.Trim();
            customer.CategoryID.value = this.ddlCustomerCategory.SelectedItem.Value;
            customer.NatureID.value = this.ddlCompanyNature.SelectedItem.Value;
            customer.SourceID.value = this.ddlSource.SelectedItem.Value;
            customer.IndustryID.value = this.ddlIndustry.SelectedItem.Value;
            customer.Province.value = this.ddlProvince.SelectedItem.Value;
            customer.City.value = this.hidden_city.Value;
            customer.Area.value = this.hidden_area.Value;
            customer.Address.value = this.txtAddress.Text.Trim();
            customer.WebSite.value = this.txtWebSite.Text.Trim();
            //customer.ImagePath.value = this.hidden_imagePath.Value;
            //---客户营业信息
            if (ULCode.Validation.IsDateTime(this.txtEstablishmentDate.Text))
                customer.EstablishmentDate.value = this.txtEstablishmentDate.Text.Trim();
            customer.RealName.value = this.txtRealName.Text.Trim();
            customer.BankName.value = this.txtBankName.Text.Trim();
            customer.BankAccount.value = this.txtBankAccount.Text.Trim();
            customer.BusinessCircles.value = this.txtBusinessCircles.Text.Trim();
            customer.Products.value = this.txtProducts.Text.Trim();
           
            //---客户其它信息
            customer.Remarks.value = this.txtRemarks.Text.Trim();
            customer.SpecialDesc.value = this.txtSpecialDesc.Text.Trim();
            //---OA维护信息
            customer.CreateUserId.value = WX.Main.CurUser.UserID;
            customer.CreateDate.value = DateTime.Now;
            customer.EmployeeID.value = WX.Main.CurUser.UserID;
            WX.Main.CurUser.LoadUserModel(false);
            customer.DeptId.value = WX.Main.CurUser.UserModel.DepartmentID.value;
            customer.State.set(0);
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程
            int row = customer.Insert(true);

            WX.CRM.CustomerTemp.MODEL customertemp = WX.CRM.CustomerTemp.GetModel("select * from CRM_Customers where ID="+row);
            customertemp.CustomersID.value = row;
            customertemp.Insert();
            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            //7.返回处理结果或返回其它页面。
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "客户信息添加成功！", null);
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(),customer.CreateUserId.ToString(),0,"");
                ULCode.Debug.Alert("客户添加成功，还没有联系人请完善联系人！", "/Manage/CRM/Crm_Single_AddContact.aspx?PageMode=my&Action=Add&CustomerID="+row);
                //ULCode.Debug.Confirm("客户信息添加成功！，您还没有添加联系人信息，是否添加联系人信息？", "Crm_AddContact.aspx?Action=Add&CustomerID=" + WX.Request.rCustomerID, "Crm_CustomerList.aspx");
            }
            else
            {
                ULCode.Debug.Alert("客户信息添加失败！", null);
            }
        }
    }
}