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
    public partial class Crm_ShowCustomerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //2.取得用户变量
            if (!ULCode.Validation.IsNumber(Request.QueryString["CustomerID"]))
            {
                Response.Write("你没有权限访问此功能！");
                Response.End();
            }
            if (!IsPostBack)
            {
                WX.CRM.Customer.MODEL customer;
                if (Request["type"] != null && Request["type"] != "")
                    customer = WX.Request.rCustomerTotempCID;
                else
                    customer = WX.CRM.Customer.NewDataModel(Request.QueryString["CustomerID"]);

                this.lblCustomerName.Text = customer.CustomerName.ToString();
                this.lblCustomerZJM.Text = customer.CustomerZJM.f("({0})");
                this.lblTypeString.Text = String.Format("{0}/{1}"
                                     ,GetIndustry(Convert.ToInt32(customer.IndustryID.value))
                                     ,GetCompanyNature(Convert.ToInt32(customer.NatureID.value)));
                
                this.lblCustomerID.Text = customer.CustomerID.ToString();

                this.liAddress.Text = String.Format("{0}{1}{2}　{3}"
                                     ,this.GetProvinceName(customer.Province.ToString())
                                     ,this.GetCityName(customer.City.ToString())
                                     ,this.GetAreaName(customer.Area.ToString())
                                     ,customer.Address);

                this.hyWebSite.Text = customer.WebSite.ToString();
                this.hyWebSite.NavigateUrl = customer.WebSite.ToString();
                this.lblProducts.Text = customer.Products.ToString();


                GetContactNameByCustomerID(Request.QueryString["CustomerID"]);

                this.lblRemark.Text = customer.Remarks.ToString();
            }
        }
        public void GetContactNameByCustomerID(string customerId)
        {
            string sSql = "SELECT ID,Dept,Duty,IsMain,ContactName,MobilePhone,WorkPhone,FamilyPhone FROM CRM_Contact WHERE CustomerID=" + customerId + " order by IsMain desc";
            DataTable dataTable = XSql.GetDataTable(sSql);
            this.rptContacts.DataSource = dataTable;
            this.rptContacts.DataBind();
            sSql = "select * from CRM_Track where CustomerID=" + customerId + " and ProcessState<5";
            DataTable dataTable2 = XSql.GetDataTable(sSql);
            this.Repeater1.DataSource = dataTable2;
            this.Repeater1.DataBind();
            sSql = "select * from CRM_Track where CustomerID=" + customerId + " and  ProcessState>4 and ProcessState<9";
            DataTable dataTable3 = XSql.GetDataTable(sSql);
            this.Repeater2.DataSource = dataTable3;
            this.Repeater2.DataBind();
            sSql = "select * from CRM_Track where CustomerID=" + customerId + " and  ProcessState>8";
            DataTable dataTable4 = XSql.GetDataTable(sSql);
            this.Repeater3.DataSource = dataTable4;
            this.Repeater3.DataBind();
        }
        public string GetCustomerCategory(int CategoryID)
        {
            return XSql.GetData("SELECT CategoryName FROM CRM_InnerCategory WHERE ID=" + CategoryID).ToString();
        }
        public string GetCompanyNature(int NatureID)
        {
             return XSql.GetData("SELECT CompanyNature FROM CRM_CompanyNature WHERE ID=" + NatureID).ToString();
        }
        public string GetCusotmerLevel(int LevelID)
        {
            return XSql.GetData("SELECT LevelName FROM CRM_BusinessLevel WHERE ID=" + LevelID).ToString();
        }
        public string GetStage(int StageID)
        {
            return XSql.GetData("SELECT StageName FROM CRM_Stage WHERE ID=" + StageID).ToString();
        }
        public string GetSource(int SourceID)
        {
            return XSql.GetData("SELECT SourceName FROM CRM_Source WHERE ID=" + SourceID).ToString();
        }
        public string GetIndustry(int IndustryID)
        {
            return XSql.GetData("SELECT IndustryName FROM CRM_Industry WHERE ID=" + IndustryID).ToString();
        }
        public string GetProvinceName(string code)
        {
            return XSql.GetData("select name from CRM_Province where code='"+code+"'").ToString();
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