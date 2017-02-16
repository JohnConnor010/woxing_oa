using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WX.Data;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.CTR
{
    public partial class SelectEmployee : System.Web.UI.Page
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
            Dict.BindListCtrl_DeptList(this.ddlDepartment, null, "0#--请选择部门--", null);
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string departmentId = this.ddlDepartment.SelectedItem.Value;
            if (departmentId != "0")
            {
                DataTable employeeData = XSql.GetDataTable("SELECT UserID,RealName FROM TU_Employees WHERE DepartmentID=" + departmentId);
                this.lstEmployees.DataSource = employeeData;
                this.lstEmployees.DataTextField = "RealName";
                this.lstEmployees.DataValueField = "UserID";
                this.lstEmployees.DataBind();
            }
            ClientScript.RegisterStartupScript(this.GetType(), "a", "window.parent.SetSelectedTab('选择人员')", true);
        }
    }
}