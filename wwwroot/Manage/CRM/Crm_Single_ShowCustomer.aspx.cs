using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ULCode.QDA;
namespace wwwroot.Manage.CRM
{
    public partial class Crm_Single_ShowCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //初始化标题
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
                //装载客户数据
                this.LoadData();
            }
        }
        private void LoadData()
        {
            string id = WX.Request.rCustomerID.ToString();
            WX.CRM.Customer.MODEL customer = WX.Request.rCustomer;
            //---客户基本信息
            this.txtCustomerID.Text = customer.CustomerID.ToString();
            this.txtCustomerName.Text = customer.CustomerName.ToString();
            this.txtCustomerZJM.Text = customer.CustomerZJM.ToString();
            if (!customer.CategoryID.isEmpty)
                this.ddlCustomerCategory.Text = XSql.GetData(customer.CategoryID.f("Select CategoryName from CRM_InnerCategory Where ID={0}")).ToString();
            if (!customer.NatureID.isEmpty)
                this.ddlCompanyNature.Text = XSql.GetData(customer.NatureID.f("Select CompanyNature from CRM_CompanyNature Where ID={0}")).ToStr();
            if(!customer.SourceID.isEmpty)
                this.ddlSource.Text = XSql.GetData(customer.SourceID.f("Select SourceName from CRM_Source Where ID={0}")).ToStr();
            if(!customer.IndustryID.isEmpty)
                this.ddlIndustry.Text = XSql.GetData(customer.IndustryID.f("Select IndustryName from CRM_Industry Where ID={0}")).ToStr();
            this.txtAddress.Text = String.Format("{0}{1}{2}　{3}"
                      , this.GetProvinceName(customer.Province.ToString())
                      , this.GetCityName(customer.City.ToString())
                      , this.GetAreaName(customer.Area.ToString())
                      , customer.Address);
            this.txtWebSite.Text = customer.WebSite.ToString();
            this.hidden_imagePath.Value = customer.ImagePath.ToString();
            //---客户营业信息
            this.txtEstablishmentDate.Text = customer.EstablishmentDate.ToString();
            this.txtRealName.Text = customer.RealName.ToString();
            this.txtBankName.Text = customer.BankName.ToString();
            this.txtBankAccount.Text = customer.BankAccount.ToString();
            this.txtBusinessCircles.Text = customer.BusinessCircles.ToString();
            this.txtProducts.Text = customer.Products.ToString();
            string sSql = "SELECT ID,Dept,Duty,IsMain,ContactName,MobilePhone,WorkPhone,FamilyPhone FROM CRM_Contact WHERE CustomerID=" + WX.Request.rCustomerID + " order by IsMain desc";
            System.Data.DataTable dataTable = XSql.GetDataTable(sSql);
            this.rptContacts.DataSource = dataTable;
            this.rptContacts.DataBind();
            sSql = "select top 5 * from CRM_Track where CustomerID=" + WX.Request.rCustomerID + " order by TrackTime desc";
            this.Repeater1.DataSource = XSql.GetDataTable(sSql);
            this.Repeater1.DataBind();
            //---客户合作信息
            if (!customer.BusinessLevel.isEmpty)
                this.ddlCoop.Text = "<a href=\"Crm_My_CustomerList.aspx?bl="+customer.BusinessLevel.ToString()+"\">"+XSql.GetData(customer.BusinessLevel.f("Select LevelName from CRM_BusinessLevel Where Id={0}")).ToStr()+"</a>";
            customer.CoopFlag.Read(this.cblCoop, 1);
            if(!customer.StageID.isEmpty)
                this.ddlStage.Text = "<a href=\"Crm_My_CustomerList.aspx?StageID=" + customer.StageID.ToString() + "\">" + XSql.GetData(customer.StageID.f("Select StageName from CRM_Stage Where ID={0}")).ToStr() + "</a>";
            customer.BuyHabbit.Read(this.cblStage, 1);
            this.txtLastConsumptionMoney.Text = string.Format("{0:C2}",customer.LastConsumptionMoney.value);
            this.txtLastMaintainMoney.Text = "<a href=\"/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=" + Request["PageMode"] + "&CustomerID=" + customer.ID.ToString() + "&fee=1\">" + string.Format("{0:C2}", customer.LastMaintainMoney.value) + "</a>";
            this.txtConsumptionMoney.Text = string.Format("{0:C2}", customer.ConsumptionMoney.value);
            this.txtMaintainMoney.Text = "<a href=\"/Manage/CRM/Crm_SingleM_ShowCustomerBusiness.aspx?PageMode=" + Request["PageMode"] + "&CustomerID=" + customer.ID.ToString() + "&fee=1\">" + string.Format("{0:C2}", customer.MaintainMoney.value) + "</a>";
            this.txtPreMoney.Text = string.Format("{0:C2}", customer.PreMoney.value);

            this.tCoolRecentStart.Text = customer.LastBegin.ToString();
            this.tCoolRecentEnd.Text = customer.LastEnd.ToString();
            this.tAskPreMoneyDate.Text = customer.UrgerDate.ToString();
            //---客户其它信息
            this.txtRemarks.Text = customer.Remarks.ToString();
            this.txtSpecialDesc.Text = customer.SpecialDesc.ToString();
            //---OA维护信息
        }
        public string GetProvinceName(string code)
        {
            return XSql.GetData("select name from CRM_Province where code='" + code + "'").ToString();
        }
        public string GetCityName(string code)
        {
            return XSql.GetData("select name from CRM_City where code='" + code + "'").ToString();
        }
        public string GetAreaName(string code)
        {
            return XSql.GetData("select name from CRM_Area where code='" + code + "'").ToString();
        }
    }
}