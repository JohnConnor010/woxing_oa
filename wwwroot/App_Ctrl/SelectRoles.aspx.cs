using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.App_Ctrl
{
    public partial class SelectRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindingddlCompany();
                BindRolesRepeater();
            }
        }
        private void BindRolesRepeater()
        {
            int CompanyID = 0;
            if (int.TryParse(Request.QueryString["CompanyID"], out CompanyID))
            {
                DataTable dataTable = XSql.GetDataTable("SELECT ID,Name,DutyCatagoryId FROM TE_Duties WHERE CompanyID=" + CompanyID);
                var duties = dataTable.AsEnumerable().Select((d, index) => new
                {
                    index = index,
                    DutyID = d.Field<Int16>("ID"),
                    DutyName = d.Field<string>("Name"),
                    DutyCatagoryId = d.Field<Int32>("DutyCatagoryId")
                });
                this.RolesReapeter.DataSource = duties;
                this.RolesReapeter.DataBind();
            }
        }
        private void BindingddlCompany()
        {
            int CompanyID = 0;
            if (int.TryParse(Request.QueryString["CompanyID"], out CompanyID))
            {
                WX.Data.Dict.BindListCtrl_Companys(this.ddlCompany, null, null, CompanyID.ToString());
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CompanyID = this.ddlCompany.SelectedItem.Value;
            WX.Data.Dict.BindListCtrl_Companys(this.ddlCompany,null, null, CompanyID);
        }
        public string GetCheckedString(object dutyId)
        {
            string Params = Request.QueryString["Params"];
            if (Params == "*")
                return "checked='checked'";
            else
                return !String.IsNullOrEmpty(Params) && Params.Contains(Convert.ToString(dutyId)) ? "checked='checked'" : String.Empty;
        }
    }
}