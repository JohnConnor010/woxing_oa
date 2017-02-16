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
    public partial class SelectDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDepartments();
            }
        }
        private void BindDepartments()
        {
            DataTable dataTable = XSql.GetDataTable("exec [dbo].[sp_get_tree_multi_table] 'TE_Departments','ID','Name','ParentID','ID',0,1,5");
            var departments = dataTable.AsEnumerable().Select((item, index) => new
                {
                    DepartmentId = item.Field<int>("ID"),
                    DepartmentName = item.Field<string>("Name"),
                    index = index,
                    Node_Name = item.Field<string>("node_name")
                });
            this.DepartmentRepeater.DataSource = departments;
            this.DepartmentRepeater.DataBind();
        }
        public string GetCheckedString(object departmentId)
        {
            string Params = Request.QueryString["Params"];
            if (Params == "*")
                return "checked='checked'";
            else
                return !String.IsNullOrEmpty(Params) && Params.Contains(Convert.ToString(departmentId)) ? "checked='checked'" : String.Empty;
        }
    }
}