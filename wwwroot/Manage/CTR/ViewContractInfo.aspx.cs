using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CTR
{
    public partial class ViewContractInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent();
            }
        }
        private void InitComponent()
        {
            string id = Request.QueryString["ID"];
            //WX.CTR.Contract.MODEL model = WX.CTR.Contract.NewDataModel(id);
            DataTable contractData = XSql.GetDataTable("SELECT CO.*,CA.Name,PR.ProjectName,DE.Name AS DepartmentName FROM CTR_Contracts AS CO LEFT JOIN CTR_Category AS CA ON CO.CategoryID=CA.ID LEFT JOIN PRO_Projects AS PR ON CO.ProjectID=PR.ID LEFT JOIN TE_Departments AS DE ON CO.DepartmentID=DE.ID WHERE CO.ID=" + id);
            this.ltlContractName.Text = contractData.Rows[0]["ContractName"].ToString();
            this.ltlContractID.Text = contractData.Rows[0]["ContractID"].ToString();
            this.ltlCateogry.Text = contractData.Rows[0]["Name"].ToString();
            this.ltlProject.Text = contractData.Rows[0]["ProjectName"].ToString();
            this.ltlAmount.Text = contractData.Rows[0]["ContractAmount"].ToString();
            this.ltlCurrency.Text = contractData.Rows[0]["Currency"].ToString();
            this.ltlSignedDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(contractData.Rows[0]["SignedDate"].ToString()));
            this.ltlDepartment.Text = contractData.Rows[0]["DepartmentName"].ToString();
            this.ltlEmployee.Text = WX.WXUser.GetRealNameByUserID(contractData.Rows[0]["EmployeeID"].ToString());
            this.ltlPaymentType.Text = contractData.Rows[0]["PaymentType"].ToString();
            this.ltlStartDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(contractData.Rows[0]["StartDate"].ToString()));
            this.ltlEndDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(contractData.Rows[0]["EndDate"].ToString()));
            this.txtContractContent.Text = contractData.Rows[0]["ContractContent"].ToString();
            this.txtContractAbnormal.Text = contractData.Rows[0]["ContractAbnormal"].ToString();
            this.ltlPartyA.Text = contractData.Rows[0]["PartyA"].ToString();
            this.ltlPartyAPerson.Text = contractData.Rows[0]["PartyAPerson"].ToString();
            this.ltlPartyB.Text = contractData.Rows[0]["PartyB"].ToString();
            this.ltlPartyBPerson.Text = contractData.Rows[0]["PartyBPerson"].ToString();
            this.ltlDigitPath.Text = contractData.Rows[0]["DigitPath"].ToString();
            this.ltlImplementation.Text = contractData.Rows[0]["Implementation"].ToString();
            this.ltlInputDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(contractData.Rows[0]["InputDate"].ToString()));
            this.ltlManager.Text = contractData.Rows[0]["Managers"].ToString();
        }
    }
}