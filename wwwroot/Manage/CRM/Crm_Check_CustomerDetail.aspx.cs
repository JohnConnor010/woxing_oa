using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Check_CustomerDetail : System.Web.UI.Page
    {
        public string mes = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //2.取得用户变量
            if (!ULCode.Validation.IsNumber(Request.QueryString["CustomerTempID"]))
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
            }
            if (!IsPostBack)
            {
                try
                {

                    WX.Data.Dict.BindListCtrl_CompanyNature(this.txtNatureID, null, null, null);
                    WX.Data.Dict.BindListCtrl_Industry(this.txtIndustry, null, null, null);
                    WX.Data.Dict.BindListCtrl_InnerCategory(this.txtCategory, null, null, null);
                    WX.Data.Dict.BindListCtrl_Source(this.txtSource, null, null, null);
                    WX.Data.Dict.BindListCtrl_BusinessLevel(this.ddlCoop, null, "#无", null);
                    WX.Data.Dict.BindListCtrl_Stage(this.ddlStage, null, "#无", null);
                    //WX.Data.Dict.BindListCtrl_Province(this.txtProvince, null, null, null);

                    WX.CRM.CustomerTemp.MODEL customertemp = WX.Request.rCustomerTempToTempID;
                    WX.CRM.Customer.MODEL customer;
                    if (Request.QueryString["CustomerID"] != "")
                        customer = WX.Request.rCustomer;
                    else
                        customer = WX.CRM.Customer.NewDataModel();
                    if (customer == null)
                    {
                        customertemp.Delete();
                        mes = "butnull('信息已不存在！');";
                        return;
                    }
                    liCustomerID.Text = customer.CustomerID.ToString();
                    liCustomerZJM.Text = customer.CustomerZJM.ToString();
                    liCustomerName.Text = customer.CustomerName.ToString();
                    try
                    {
                        liNatureID.Text = customer.NatureID.ToString() != "" ? txtNatureID.Items[customer.NatureID.ToInt32()].Text : "";
                    }
                    catch { } try
                    {
                        liIndustry.Text = customer.IndustryID.ToString() != "" ? txtIndustry.Items[customer.IndustryID.ToInt32()].Text : "";
                    }
                    catch { } try
                    {
                        liCategory.Text = customer.CategoryID.ToString() != "" ? txtCategory.Items[customer.CategoryID.ToInt32()].Text : "";
                    }
                    catch { } try
                    {
                        liSource.Text = customer.SourceID.ToString() != "" ? txtSource.Items[customer.SourceID.ToInt32()].Text : "";
                    }
                    catch { }
                    this.liAddress.Text = String.Format("{0}{1}{2}　{3}"
                          , this.GetProvinceName(customer.Province.ToString())
                          , this.GetCityName(customer.City.ToString())
                          , this.GetAreaName(customer.Area.ToString())
                          , customer.Address.ToString());

                    liWebSite.Text = customer.WebSite.ToString();
                    //liimagePath.Text = customer.ImagePath.ToString();
                    liEstablishmentDate.Text = customer.EstablishmentDate.ToString();
                    liRealName.Text = customer.RealName.ToString();
                    liBankName.Text = customer.BankName.ToString();
                    liBankAccount.Text = customer.BankAccount.ToString();
                    liBusinessCircles.Text = customer.BusinessCircles.ToString();
                    liProducts.Text = customer.Products.ToString();
                    liRemarks.Text = customer.Remarks.ToString();
                    liSpecialDesc.Text = customer.SpecialDesc.ToString();
                    txtCustomerID.Text = customertemp.CustomerID.ToString();
                    txtCustomerID.Enabled = customer.CustomerID.ToString() == customertemp.CustomerID.ToString() ? false : true;
                    txtCustomerZJM.Text = customertemp.CustomerZJM.ToString();
                    txtCustomerZJM.Enabled = customer.CustomerZJM.ToString() == customertemp.CustomerZJM.ToString() ? false : true;
                    txtCustomerName.Text = customertemp.CustomerName.ToString();
                    txtCustomerName.Enabled = customer.CustomerName.ToString() == customertemp.CustomerName.ToString() ? false : true;
                    txtNatureID.SelectedValue = customertemp.NatureID.ToString();
                    txtNatureID.Enabled = customer.NatureID.ToString() == customertemp.NatureID.ToString() ? false : true;
                    txtIndustry.SelectedValue = customertemp.IndustryID.ToString();
                    txtIndustry.Enabled = customer.IndustryID.ToString() == customertemp.IndustryID.ToString() ? false : true;
                    txtCategory.SelectedValue = customertemp.CategoryID.ToString();
                    txtCategory.Enabled = customer.CategoryID.ToString() == customertemp.CategoryID.ToString() ? false : true;
                    txtSource.SelectedValue = customertemp.SourceID.ToString();
                    txtSource.Enabled = customer.SourceID.ToString() == customertemp.SourceID.ToString() ? false : true;
                    txtWebSite.Text = customertemp.WebSite.ToString();
                    txtWebSite.Enabled = customer.WebSite.ToString() == customertemp.WebSite.ToString() ? false : true;
                    txtAddress.Text = customertemp.Address.ToString();
                    txtAddress.Enabled = customer.Address.ToString() == customertemp.Address.ToString() ? false : true;
                    //txtImagePath.Text = customertemp.ImagePath.ToString();
                    //txtImagePath.Enabled = customer.ImagePath.ToString() == customertemp.ImagePath.ToString() ? false : true;
                    txtEstablishmentDate.Text = customertemp.EstablishmentDate.ToString();
                    txtEstablishmentDate.Enabled = customer.EstablishmentDate.ToString() == customertemp.EstablishmentDate.ToString() ? false : true;
                    txtRealName.Text = customertemp.RealName.ToString();
                    txtRealName.Enabled = customer.RealName.ToString() == customertemp.RealName.ToString() ? false : true;
                    txtBankName.Text = customertemp.BankName.ToString();
                    txtBankName.Enabled = customer.BankName.ToString() == customertemp.BankName.ToString() ? false : true;
                    txtBankAccount.Text = customertemp.BankAccount.ToString();
                    txtBankAccount.Enabled = customer.BankAccount.ToString() == customertemp.BankAccount.ToString() ? false : true;
                    txtBusinessCircles.Text = customertemp.BusinessCircles.ToString();
                    txtBusinessCircles.Enabled = customer.BusinessCircles.ToString() == customertemp.BusinessCircles.ToString() ? false : true;
                    txtProducts.Text = customertemp.Products.ToString();
                    txtProducts.Enabled = customer.Products.ToString() == customertemp.Products.ToString() ? false : true;
                    txtRemarks.Text = customertemp.Remarks.ToString();
                    txtRemarks.Enabled = customer.Remarks.ToString() == customertemp.Remarks.ToString() ? false : true;
                    txtSpecialDesc.Text = customertemp.SpecialDesc.ToString();

                    txtSpecialDesc.Enabled = customer.SpecialDesc.ToString() == customertemp.SpecialDesc.ToString() ? false : true;
                    WX.Data.Dict.BindListCtrl_Province(this.txtProvince, null, "0#--选择省份--", customertemp.Province.ToString());
                    txtProvince.Enabled = customer.Province.ToString() == customertemp.Province.ToString() ? false : true;
                    //Response.Write(String.Format(",{0},{1},{2},",customertemp.Province,customertemp.City,customer.Area));
                    if (!customertemp.Province.isEmpty) WX.Data.Dict.BindListCtrl_City(this.txtCity, null, "0#--城市--", customertemp.City.ToString(), customertemp.Province.ToString());
                    if (!customertemp.City.isEmpty) WX.Data.Dict.BindListCtrl_Area(this.txtArea, null, "0#--区县--", customertemp.Area.ToString(), customertemp.City.ToString());
                    //hidden_imagePath.Value = customer.ImagePath.ToString();

                    //----------------客户业务正式上线后删除或注释掉
                    ddlCoop.SelectedValue = customer.BusinessLevel.ToString();
                    liCoop.Text = ddlCoop.SelectedItem.Text;
                    ddlCoop.SelectedValue = customertemp.BusinessLevel.ToString();
                    ddlCoop.Enabled = customer.BusinessLevel.ToString() == customertemp.BusinessLevel.ToString() ? false : true;

                    ddlStage.SelectedValue = customer.StageID.ToString();
                    liStage.Text = ddlStage.SelectedItem.Text;
                    ddlStage.SelectedValue = customertemp.StageID.ToString();
                    ddlStage.Enabled = customer.StageID.ToString() == customertemp.StageID.ToString() ? false : true;

                    liLastConsumptionMoney.Text = customer.LastConsumptionMoney.ToString() == "" ? "0" : customer.LastConsumptionMoney.ToString();
                    txtLastConsumptionMoney.Text = customertemp.LastConsumptionMoney.ToString() == "" ? "0" : customertemp.LastConsumptionMoney.ToString();
                    txtLastConsumptionMoney.Enabled = customer.LastConsumptionMoney.ToString() == customertemp.LastConsumptionMoney.ToString() ? false : true;

                    liCoolRecentStart.Text = customer.LastBegin.ToString() + "-" + customer.LastEnd.ToString();
                    txtCoolRecentStart.Text = customertemp.LastBegin.ToString();
                    txtCoolRecentEnd.Text = customertemp.LastEnd.ToString();
                    txtCoolRecentStart.Enabled = customer.LastBegin.ToString() == customertemp.LastBegin.ToString() ? false : true;
                    txtCoolRecentEnd.Enabled = customer.LastEnd.ToString() == customertemp.LastEnd.ToString() ? false : true;

                    liConsumptionMoney.Text = customer.ConsumptionMoney.ToString() == "" ? "0" : customer.ConsumptionMoney.ToString();
                    txtConsumptionMoney.Text = customertemp.ConsumptionMoney.ToString() == "" ? "0" : customertemp.ConsumptionMoney.ToString();
                    txtConsumptionMoney.Enabled = customer.ConsumptionMoney.ToString() == customertemp.ConsumptionMoney.ToString() ? false : true;

                    liLastMaintainMoney.Text = customer.LastMaintainMoney.ToString() == "" ? "0" : customer.LastMaintainMoney.ToString();
                    txtLastMaintainMoney.Text = customertemp.LastMaintainMoney.ToString() == "" ? "0" : customertemp.LastMaintainMoney.ToString();
                    txtLastMaintainMoney.Enabled = customer.LastMaintainMoney.ToString() == customertemp.LastMaintainMoney.ToString() ? false : true;

                    liPreMoney.Text = customer.PreMoney.ToString() == "" ? "0" : customer.PreMoney.ToString();
                    txtPreMoney.Text = customertemp.PreMoney.ToString() == "" ? "0" : customertemp.PreMoney.ToString();
                    txtPreMoney.Enabled = customer.PreMoney.ToString() == customertemp.PreMoney.ToString() ? false : true;

                    liAskPreMoneyDate.Text = customer.UrgerDate.ToString();
                    txtAskPreMoneyDate.Text = customertemp.UrgerDate.ToString();
                    txtAskPreMoneyDate.Enabled = customer.UrgerDate.ToString() == customertemp.UrgerDate.ToString() ? false : true;

                    liMaintainMoney.Text = customer.MaintainMoney.ToString() == "" ? "0" : customer.MaintainMoney.ToString();
                    txtMaintainMoney.Text = customertemp.MaintainMoney.ToString() == "" ? "0" : customertemp.MaintainMoney.ToString();
                    txtMaintainMoney.Enabled = customer.MaintainMoney.ToString() == customertemp.MaintainMoney.ToString() ? false : true;
                }
                catch (Exception ex){
                    mes = "butnull('"+ex.Message+"');";
                }
            }
        }
        private string GetProvinceName(string code)
        {
            return ULCode.QDA.XSql.GetData("select name from CRM_Province where code='" + code + "'").ToString();
        }
        private string GetCityName(string code)
        {
            return ULCode.QDA.XSql.GetData("select name from CRM_City where code='" + code + "'").ToString();
        }
        private string GetAreaName(string code)
        {
            return ULCode.QDA.XSql.GetData("select name from CRM_Area where code='" + code + "'").ToString();
        }
        private WX.CRM.Customer.MODEL getnew()
        {
            WX.CRM.CustomerTemp.MODEL customertemp = WX.Request.rCustomerTempToTempID;
            WX.CRM.Customer.MODEL customer;

            if (customertemp.CustomersID.ToInt32() > 0)
            customer= WX.CRM.Customer.NewDataModel(customertemp.CustomersID.ToInt32());
            else
                customer = WX.CRM.Customer.NewDataModel();
            customer.CustomerID.value = txtCustomerID.Text;
            customer.CustomerZJM.value = txtCustomerZJM.Text;
            customer.CustomerName.value = txtCustomerName.Text;
            customer.NatureID.value = txtNatureID.SelectedValue;
            customer.IndustryID.value = txtIndustry.SelectedValue;
            customer.CategoryID.value = txtCategory.SelectedValue;
            customer.SourceID.value = txtSource.SelectedValue;

            customer.Province.value = this.txtProvince.SelectedValue;
            customer.City.value = this.txtCity.SelectedValue;
            customer.Area.value = this.txtArea.SelectedValue;
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
            customer.Remarks.value = this.txtRemarks.Text.Trim();
            customer.SpecialDesc.value = this.txtSpecialDesc.Text.Trim();
            //---客户合作信息
            customer.BusinessLevel.value = customertemp.BusinessLevel.value ;
            customer.StageID.value = customertemp.StageID.value;
            customer.LastConsumptionMoney.value = customertemp.LastConsumptionMoney.value;
            customer.LastMaintainMoney.value = customertemp.LastMaintainMoney.value;
            customer.ConsumptionMoney.value = customertemp.ConsumptionMoney.value;
            customer.MaintainMoney.value = customertemp.MaintainMoney.value;
            customer.PreMoney.value = customertemp.PreMoney.value;
                customer.LastBegin.value = customertemp.LastBegin.value;
                customer.LastEnd.value = customertemp.LastEnd.value;
                customer.UrgerDate.value = customertemp.UrgerDate.value;
            //---OA维护信息
            //customer.StartDate.value = DateTime.Now.ToString();
            customer.EmployeeID.value = customertemp.EmployeeID.value;
            customer.CreateUserId.value=customertemp.CreateUserId.value;
            customer.DeptId.value = customertemp.DeptId.value;
            return customer;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WX.CRM.Customer.MODEL customer = getnew();
            customer.State.set(2);
            customer.CheckUserId.value = WX.Main.CurUser.UserID;
            customer.UpTime.value = DateTime.Now;
            if (customer.ID.ToInt32()>0)
                customer.Update();
            else
                customer.Insert();

            WX.CRM.CustomerTemp.MODEL customertemp = WX.Request.rCustomerTempToTempID;
            customertemp.Delete();
            mes = "butsumit();";
            WX.CRM.Customer.AddLog(customer.ID.ToInt32(),customer.CustomerName.ToString(), customer.CheckUserId.ToString(), 2, "通过");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            WX.CRM.CustomerTemp.MODEL customertemp = WX.Request.rCustomerTempToTempID;
            customertemp.State.set(-1);
            customertemp.CheckUserId.value = WX.Main.CurUser.UserID;
            customertemp.Update();
            mes = "butsumit();";
            WX.CRM.Customer.AddLog(customertemp.CustomersID.ToString()!=""? customertemp.CustomersID.ToInt32():customertemp.ID.ToInt32(),customertemp.CustomerName.ToString(), customertemp.CheckUserId.ToString(), 2, "未通过");
        }

        protected void txtProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prov = this.txtProvince.SelectedValue;
            WX.Data.Dict.BindListCtrl_City(this.txtCity, null, "0#--选择城市--", null,prov);
            WX.Data.Dict.BindListCtrl_Area(this.txtArea, null, "0#--选择区县--", null, "0");
        }

        protected void txtCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string city = this.txtCity.SelectedValue;
            WX.Data.Dict.BindListCtrl_Area(this.txtArea, null, "0#--选择区县--", null, city);
        }
    }
}