using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
using System.Data;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Single_EditCustomer : System.Web.UI.Page
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
                string mode = Convert.ToString(Request.QueryString["PageMode"]);
                if (mode == "my")
                {
                    this.lblTitle.Text = "我的客户";
                    this.MenuBar1.Key = "MyCustomer-Modi";
                }
                else
                {
                    this.lblTitle.Text = "我的管理";
                    this.MenuBar1.Key = "Customer-Modi";
                }
                //2.取得用户变量
                InitData();
                string id = WX.Request.rCustomerID.ToString();

                WX.CRM.CustomerTemp.MODEL customer;
                if (Request["PageMode"] != null && Request["PageMode"] == "mycheck")
                    customer = WX.Request.rCustomerTempToRCID;
                else
                {
                    customer = WX.Request.rCustomerTempToCID;
                    if (customer == null)
                        customer = WX.Request.rCustomerTempToRCID;
                    if (customer == null)
                        customer = WX.Request.rCustomerTempToCustomer;
                }
                WX.CRM.Customer.MODEL customermodel = WX.Request.rCustomer;
                //---客户基本信息
                this.txtCustomerID.Text = customer.CustomerID.ToString();
                Label0.Text = txtCustomerID.Text != customermodel.CustomerID.ToString() ? "已修改未审核！" : "";
                this.txtCustomerName.Text = customer.CustomerName.ToString();
                Label1.Text = txtCustomerName.Text != customermodel.CustomerName.ToString() ? "已修改未审核！" : "";
                this.txtCustomerZJM.Text = customer.CustomerZJM.ToString();
                Label2.Text = txtCustomerZJM.Text != customermodel.CustomerZJM.ToString() ? "已修改未审核！" : "";
                this.ddlCustomerCategory.SelectedValue = customer.CategoryID.ToString();
                Label5.Text = customer.CategoryID.ToString() != customermodel.CategoryID.ToString() ? "已修改未审核！" : "";
                this.ddlCompanyNature.SelectedValue = customer.NatureID.ToString();
                Label3.Text = customer.NatureID.ToString() != customermodel.NatureID.ToString() ? "已修改未审核！" : "";
                this.ddlSource.SelectedValue = customer.SourceID.ToString();
                Label6.Text = customer.SourceID.ToString() != customermodel.SourceID.ToString() ? "已修改未审核！" : "";
                this.ddlIndustry.SelectedValue = customer.IndustryID.ToString();
                Label4.Text = customer.IndustryID.ToString() != customermodel.IndustryID.ToString() ? "已修改未审核！" : "";
                //this.ddlProvince.SelectedValue = customer.Province.ToString();
                //this.hidden_city.Value = customer.City.ToString();
                //this.hidden_area.Value = customer.Area.ToString();
                //---添加地区
                if (customer.Province.ToString() != "0")
                {
                    this.ddlProvince.SelectedValue = customer.Province.ToString();
                }
                if (customer.City.ToString() != "0" && customer.City.ToString() != "" && customer.Province.ToString() != "0" && customer.Province.ToString() != "")
                {
                    this.hidden_city.Value = customer.City.ToString();
                    this.ddlCity.Attributes.Remove("disabled");
                    this.ddlCity.Items.Clear();
                    this.ddlCity.Items.Add(new ListItem("--请选择--", "0"));
                    DataTable cityData = XSql.GetDataTable("SELECT code,name FROM CRM_City WHERE ProvinceId=" + customer.Province.ToString());
                    var cityItems = cityData.AsEnumerable().Select(city => new ListItem
                    {
                        Text = city.Field<string>("name"),
                        Value = city.Field<string>("code"),
                        Selected = city.Field<string>("code").ToString() == customer.City.ToString() ? true : false
                    });
                    foreach (var item in cityItems)
                    {
                        this.ddlCity.Items.Add(item);
                    }
                }
                if (customer.City.ToString() != "0" && customer.Area.ToString() != "0" && customer.City.ToString() != "" && customer.Area.ToString() != "")
                {
                    this.hidden_area.Value = customer.Area.ToString();
                    this.ddlArea.Attributes.Remove("disabled");
                    this.ddlArea.Items.Clear();
                    this.ddlArea.Items.Add(new ListItem("--请选择--", "0"));
                    DataTable areaData = XSql.GetDataTable("SELECT code,name FROM CRM_Area WHERE CityId=" + customer.City.ToString());
                    var areaItems = areaData.AsEnumerable().Select(area => new ListItem
                    {
                        Text = area.Field<string>("name"),
                        Value = area.Field<string>("code"),
                        Selected = area.Field<string>("code").ToString() == customer.Area.ToString() ? true : false
                    });
                    foreach (var item in areaItems)
                    {
                        this.ddlArea.Items.Add(item);
                    }
                }
                this.txtAddress.Text = customer.Address.ToString();
                Label7.Text = customer.Province.ToString() + customer.City.ToString() + customer.Area.ToString() + txtAddress.Text != customermodel.Province.ToString() + customermodel.City.ToString() + customermodel.Area.ToString() + customermodel.Address.ToString() ? "已修改未审核！" : "";
                this.txtWebSite.Text = customer.WebSite.ToString();
                Label8.Text = txtWebSite.Text != customermodel.WebSite.ToString() ? "已修改未审核！" : "";
                //this.hidden_imagePath.Value = customer.ImagePath.ToString();
                //---客户营业信息
                this.txtEstablishmentDate.Text = customer.EstablishmentDate.ToString();
                Label9.Text = txtEstablishmentDate.Text != customermodel.EstablishmentDate.ToString() ? "已修改未审核！" : "";
                this.txtRealName.Text = customer.RealName.ToString();
                Label10.Text = txtRealName.Text != customermodel.RealName.ToString() ? "已修改未审核！" : "";
                this.txtBankName.Text = customer.BankName.ToString();
                Label11.Text = txtBankName.Text != customermodel.BankName.ToString() ? "已修改未审核！" : "";
                this.txtBankAccount.Text = customer.BankAccount.ToString();
                Label12.Text = customer.BankAccount.ToString() != customermodel.BankAccount.ToString() ? "已修改未审核！" : "";
                this.txtBusinessCircles.Text = customer.BusinessCircles.ToString();
                Label13.Text = txtBusinessCircles.Text != customermodel.BusinessCircles.ToString() ? "已修改未审核！" : "";
                this.txtProducts.Text = customer.Products.ToString();
                Label14.Text = txtProducts.Text != customermodel.Products.ToString() ? "已修改未审核！" : "";
                //---客户合作信息
                this.ddlCoop.SelectedValue = customer.BusinessLevel.ToString();
                Label15.Text = customer.BusinessLevel.ToString() != customermodel.BusinessLevel.ToString() ? "已修改未审核！" : "";
                //customer.CoopFlag.Read(this.cblCoop, 1);
                this.ddlStage.SelectedValue = customer.StageID.ToString();
                Label16.Text = customer.StageID.ToString() != customermodel.StageID.ToString() ? "已修改未审核！" : "";
                //customer.BuyHabbit.Read(this.cblStage, 1);
                this.txtLastConsumptionMoney1.Text = customer.LastConsumptionMoney.ToString() == "" ? "0" :customer.LastConsumptionMoney.ToString();

                Label17.Text = customer.LastConsumptionMoney.ToString() != customermodel.LastConsumptionMoney.ToString() ? "已修改未审核！" : "";
                this.txtLastMaintainMoney1.Text = customer.LastMaintainMoney.ToString() == "" ? "0" : customer.LastMaintainMoney.ToString();
                Label18.Text = customer.LastMaintainMoney.ToString() != customermodel.LastMaintainMoney.ToString() ? "已修改未审核！" : "";
                this.txtConsumptionMoney1.Text = customer.ConsumptionMoney.ToString() == "" ? "0" : customer.ConsumptionMoney.ToString();
                Label21.Text = customer.ConsumptionMoney.ToString() != customermodel.ConsumptionMoney.ToString() ? "已修改未审核！" : "";
                this.txtMaintainMoney1.Text = customer.MaintainMoney.ToString() == "" ? "0" : customer.MaintainMoney.ToString();
                Label22.Text = customer.MaintainMoney.ToString() != customermodel.MaintainMoney.ToString() ? "已修改未审核！" : "";

                this.tCoolRecentStart.Text = customer.LastBegin.ToString();
                this.tCoolRecentEnd.Text = customer.LastEnd.ToString();
                Label19.Text = customer.LastBegin.ToString() + customer.LastEnd.ToString() != customermodel.LastBegin.ToString()+customermodel.LastEnd.ToString() ? "已修改未审核！" : "";
                this.tAskPreMoneyDate.Text = customer.UrgerDate.ToString();
                this.txtPreMoney1.Text = customer.PreMoney.ToString() == "" ? "0" : customer.PreMoney.ToString();
                Label20.Text = customer.PreMoney.ToString() + customer.UrgerDate.ToString() != customermodel.PreMoney.ToString() + customermodel.UrgerDate.ToString() ? "已修改未审核！" : "";
                //---客户其它信息
                this.txtRemarks.Text = customer.Remarks.ToString();
                Label23.Text = txtRemarks.Text != customermodel.Remarks.ToString() ? "已修改未审核！" : "";
                this.txtSpecialDesc.Text = customer.SpecialDesc.ToString();
                Label24.Text = txtSpecialDesc.Text != customermodel.SpecialDesc.ToString() ? "已修改未审核！" : "";
                //---OA维护信息
                //无
               
            }
        }

        private void InitData()
        {
            //WX.CRM.CustomerTemp.MODEL customer = WX.CRM.CustomerTemp.GetModel("SELECT * FROM CRM_CustomersTemp where ");
            //string customerId = (string)XSql.GetValue("SELECT TOP 1 CustomerID FROM CRM_Customers ORDER BY ID DESC");
            //this.txtCustomerID.Text = customer.CreateCustomerID(customerId);

            WX.Data.Dict.BindListCtrl_InnerCategory(this.ddlCustomerCategory, null, "#--请选择客户内部类型--", null);
            WX.Data.Dict.BindListCtrl_CompanyNature(this.ddlCompanyNature, null, "#--请选择客户性质--", null);
            WX.Data.Dict.BindListCtrl_Source(this.ddlSource, null, "#--请选择客户来源--", null);
            WX.Data.Dict.BindListCtrl_Industry(this.ddlIndustry, null, "#--请选择客户行业--", null);
            WX.Data.Dict.BindListCtrl_BusinessLevel(this.ddlCoop, null, "#--请选择业务合作类型--", null);
            //WX.Data.Dict.BindListCtrl_ProductCatagory(this.cblCoop, null, null, null);
            WX.Data.Dict.BindListCtrl_Stage(this.ddlStage, null, "#--请选择跟踪阶段--", null);
            //WX.Data.Dict.BindListCtrl_BuyHabbit(this.cblStage, null, null, null);

            var province = XSql.GetDataTable("SELECT code,name FROM CRM_Province");
            this.ddlProvince.DataSource = province;
            this.ddlProvince.DataTextField = "name";
            this.ddlProvince.DataValueField = "code";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, new ListItem("--请选择--", "0"));
            //this.txtEstablishmentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
            WX.CRM.Customer.MODEL cmodel = WX.CRM.Customer.GetModel("select * from CRM_Customers where ID!=" + WX.Request.rCustomerID + " and CustomerName='"+this.txtCustomerName.Text.Trim()+"'");
            if (cmodel != null && cmodel.ID.ToInt32() > 0)
            {
                ULCode.Debug.Alert("客户名称已被" + WX.CommonUtils.GetRealNameListByUserIdList(cmodel.EmployeeID.ToString()) + "录入，提交失败！", "Crm_Single_EditCustomer.aspx?PageMode=" + Request.QueryString["PageMode"] + "&CustomerID="+WX.Request.rCustomerID);
                return;
            }
            //2.取得用户变量
            bool flag = true;
            string id = WX.Request.rCustomerID.ToString();
            WX.CRM.CustomerTemp.MODEL customer;
                customer = WX.Request.rCustomerTempToCID;
            if (customer == null)
            {
                customer = WX.Request.rCustomerTempToCustomer;
                customer.CustomersID.value = WX.Request.rCustomerID; 
                flag = false;
            }
            //---客户基本信息
            customer.CustomerID.value = this.txtCustomerID.Text.Trim();
            customer.CustomerName.value = this.txtCustomerName.Text.Trim();

            customer.CustomerZJM.value = this.txtCustomerZJM.Text.Trim();
            customer.CategoryID.value = this.ddlCustomerCategory.SelectedValue;
            customer.NatureID.value = this.ddlCompanyNature.SelectedValue;
            customer.SourceID.value = this.ddlSource.SelectedValue;
            customer.IndustryID.value = this.ddlIndustry.SelectedValue;
            customer.Province.value = this.ddlProvince.SelectedValue;
            customer.City.value = this.hidden_city.Value;
            customer.Area.value = this.hidden_area.Value;
            customer.Address.value = this.txtAddress.Text.Trim();
            customer.WebSite.value = this.txtWebSite.Text.Trim();
            //customer.ImagePath.value = this.hidden_imagePath.Value;
            //---客户营业信息
            if (ULCode.Validation.IsDateTime(this.txtEstablishmentDate.Text))
                customer.EstablishmentDate.value =this.txtEstablishmentDate.Text.Trim();
            customer.RealName.value = this.txtRealName.Text.Trim();
            customer.BankName.value = this.txtBankName.Text.Trim();
            customer.BankAccount.value = this.txtBankAccount.Text.Trim();
            customer.BusinessCircles.value = this.txtBusinessCircles.Text.Trim();
            customer.Products.value = this.txtProducts.Text.Trim();
            //---客户合作信息
            customer.BusinessLevel.value = this.ddlCoop.SelectedValue;
            //customer.CoopFlag.Read(this.cblCoop, 1);
            customer.StageID.value = this.ddlStage.SelectedValue;
            //customer.BuyHabbit.Read(this.cblStage, 1);
            if (GetDecimal(this.txtLastConsumptionMoney1.Text))
                customer.LastConsumptionMoney.value = decimal.Parse(this.txtLastConsumptionMoney1.Text);
            else
            {
                Setmes("近期消费总额非数字！");
                return;
            }
            if (GetDecimal(this.txtLastMaintainMoney1.Text))
                customer.LastMaintainMoney.value = decimal.Parse(this.txtLastMaintainMoney1.Text);
            else
            {
                Setmes("近期维护费用非数字！");
                return;
            }
            if (GetDecimal(this.txtConsumptionMoney1.Text))
                customer.ConsumptionMoney.value = decimal.Parse(this.txtConsumptionMoney1.Text);
            else
            {
                Setmes("累计消费总额非数字！");
                return;
            }
            if (GetDecimal(this.txtMaintainMoney1.Text))
                customer.MaintainMoney.value = decimal.Parse(this.txtMaintainMoney1.Text);
            else
            {
                Setmes("累计维护费用非数字！");
                return;
            }
            if (GetDecimal(this.txtPreMoney1.Text))
                customer.PreMoney.value = decimal.Parse(this.txtPreMoney1.Text);
            else
            {
                Setmes("应收账款非数字！");
                return;
            }
            if (ULCode.Validation.IsDateTime(this.tCoolRecentStart.Text))
                customer.LastBegin.value = Convert.ToDateTime(this.tCoolRecentStart.Text);
            if (ULCode.Validation.IsDateTime(this.tCoolRecentEnd.Text))
                customer.LastEnd.value = Convert.ToDateTime(this.tCoolRecentEnd.Text);
            if (ULCode.Validation.IsDateTime(this.tAskPreMoneyDate.Text))
                customer.UrgerDate.value = Convert.ToDateTime(this.tAskPreMoneyDate.Text);
            //---客户其它信息
            customer.Remarks.value = this.txtRemarks.Text.Trim();
            customer.SpecialDesc.value = this.txtSpecialDesc.Text.Trim();
            //---OA维护信息
            //customer.StartDate.value = DateTime.Now.ToString();

            customer.State.set(0);
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //3.验证用户变量，包含Request.QueryString及Request.Form
            //4.业务处理过程

            customer.CreateUserId.value = customer.CreateUserId.ToString()!=""?customer.CreateUserId.value: WX.Main.CurUser.UserID;

            customer.CreateDate.value =customer.CreateDate.ToString()!=""?customer.CreateDate.value: DateTime.Now;
            customer.EmployeeID.value =customer.EmployeeID.ToString()!=""?customer.EmployeeID.value: WX.Main.CurUser.UserID;
            WX.Main.CurUser.LoadUserModel(false);
            customer.DeptId.value =customer.DeptId.ToString()!=""?customer.DeptId.value: WX.Main.CurUser.UserModel.DepartmentID.value;
            int row = 0;
            if (flag)
                row = customer.Update();
            else
                row = customer.Insert();

            //填写主要业务逻辑代码
            //5.（用户及业务对象）统计与状态

            //6.登记日志
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "客户信息修改！", null);
                WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), WX.Main.CurUser.UserID, 1, "");
            }
            //7.返回处理结果或返回其它页面。
            string redericturl = "Crm_My_CustomerList.aspx";
            if(customer.EmployeeID.ToString()!=WX.Main.CurUser.UserID)
                redericturl = "Crm_Manage_CustomerList.aspx";
            if (Request["type"] != null && Request["type"]!= "")
                redericturl = "Crm_My_CustomerToCheck.aspx";
            if (row > 0)
            {
                ULCode.Debug.Alert("客户信息修改成功！", redericturl);
            }
            else
            {
                ULCode.Debug.Alert("客户信息添加失败！", redericturl);
            }
        }
        public string mes = "";
        private void Setmes(string str)
        {
            mes = "alert('"+str+"')";
        }
        private bool GetDecimal(string str)
        {
            try
            {
                
                decimal deno= decimal.Parse(str);
                return true;
            }
            catch { return false; }
        }
    }
}