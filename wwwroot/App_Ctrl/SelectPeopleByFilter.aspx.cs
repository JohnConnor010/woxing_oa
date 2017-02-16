using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;
using WX.Flow.Model;
namespace wwwroot.App_Ctrl
{
    public partial class SelectPeopleByFilter : System.Web.UI.Page
	{
        protected int count;
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDept,null, null, null);
                this.ddlDept.Items.Insert(0, new ListItem("--所有人员--", "0"));
                if (!string.IsNullOrEmpty(Request.QueryString["Params"]))
                {
                    InitSelectedEmployee(Request.QueryString["Params"]);
                }
                BindEmployeeByDeptId();
            }
		}
        private void InitSelectedEmployee(string Params)
        {
            if (!String.IsNullOrEmpty(Params))
            {
                string[] peoples = Params.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string userId in peoples)
                {
                    string name = (string)XSql.GetValue("SELECT RealName FROM TU_Employees WHERE UserID='" + userId + "'");
                    ListItem item = new ListItem(name, userId);
                    this.lbRight.Items.Add(item);
                }
            }
        }
        private void BindEmployeeByDeptId(int deptId = 0)
        {
            string companyId = Request.QueryString["CompanyId"];
            //获取RunId
            string runId = Request.QueryString["RunId"];
            //获取StepId
            string stepId = Request.QueryString["StepId"];

            StringBuilder selList = new StringBuilder();

            for (int i = 0; i < lbRight.Items.Count; i++)
            {
                if (selList.Length > 0) selList.Append(",");
                selList.AppendFormat("'{0}'", lbRight.Items[i].Value);
            }
            String con_Sel = String.Empty;
            if (selList.Length > 0)
                con_Sel = String.Format(" and userid not in ({0})", selList);

            Run.MODEL run = Run.NewDataModel(runId);
            //步骤编号
            string filterList = run.GetAllOpList(Convert.ToInt32(stepId));
            string con_Filter = string.Empty;
            //if (!string.IsNullOrEmpty(filterList))
            //{
            //    con_Filter = " and UserId in ('" + filterList.Replace(",","','")+ "')";
            //}
            string query = String.Format("SELECT UserID,RealName,DepartmentID FROM vw_Employees_HR where 1=1{0}{1}", con_Sel, con_Filter);
            DataTable dataTable = XSql.GetDataTable(query);
            if (deptId == 0)
            {   //所有人员
                var employees = dataTable.AsEnumerable().Select((row, index) => new
                {
                    index = index,
                    UserId = row.Field<Guid>("UserID"),
                    RealName = row.Field<string>("RealName")
                });
                this.lbLeft.DataSource = employees;
                this.lbLeft.DataTextField = "RealName";
                this.lbLeft.DataValueField = "UserId";
                this.lbLeft.DataBind();
            }
            else
            {
                var employees = dataTable.AsEnumerable().Where(e => e.Field<int>("DepartmentID") == deptId).Select((row, index) => new
                {
                    index = index,
                    UserId = row.Field<Guid>("UserID"),
                    RealName = row.Field<string>("RealName")
                });
                this.lbLeft.DataSource = employees;
                this.lbLeft.DataTextField = "RealName";
                this.lbLeft.DataValueField = "UserId";
                this.lbLeft.DataBind();
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deptId = 0;
            if (int.TryParse(this.ddlDept.SelectedItem.Value, out deptId))
            {
                BindEmployeeByDeptId(deptId);
            }
        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["SelOnlyOne"] != null)
            {
                if (lbRight.Items.Count == 1)
                {
                    ULCode.Debug.Alert("只允许添加一个人！");
                    return;
                }
            }
            for (int i = 0; i < lbLeft.Items.Count; i++)
            {
                if (lbLeft.Items[i].Selected == true)
                {
                    string value = lbLeft.Items[i].Value;
                    if (lbRight.Items.FindByValue(value) != null)
                    {
                        ULCode.Debug.Alert("此员工姓名已经存在，不能重复添加！");
                        return;
                    }
                    else
                    {
                        this.lbRight.Items.Add(lbLeft.Items[i]);
                    }
                }
            }
            for (int i = lbLeft.Items.Count - 1; i >= 0; i--)
            {
                if (lbLeft.Items[i].Selected == true)
                {
                    this.lbLeft.Items.Remove(lbLeft.Items[i]);
                }
            }
            count = this.lbRight.Items.Count;
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbRight.Items.Count; i++)
            {
                if (lbRight.Items[i].Selected == true)
                {
                    this.lbLeft.Items.Add(lbRight.Items[i]);
                }
            }
            for (int i = lbRight.Items.Count - 1; i >= 0; i--)
            {
                if (lbRight.Items[i].Selected == true)
                {
                    this.lbRight.Items.Remove(lbRight.Items[i]);
                }
            }
            count = this.lbRight.Items.Count;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.lbRight.Items.Clear();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string companyId = Request.QueryString["CompanyId"];
            string keyword = this.txtKeyword.Text.Trim();
            DataTable dataTable = XSql.GetDataTable("SELECT * FROM vw_Employees WHERE CompanyID=" + companyId);
            var employees = dataTable.AsEnumerable().Where(em => em.Field<string>("RealName").ToString().StartsWith(keyword)).Select((row, index) => new
            {
                index = index,
                UserId = row.Field<Guid>("UserID"),
                RealName = row.Field<string>("RealName")
            });
            this.lbLeft.DataSource = employees;
            this.lbLeft.DataTextField = "RealName";
            this.lbLeft.DataValueField = "UserId";
            this.lbLeft.DataBind();
        }
	}
}