using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;
namespace wwwroot.App_Ctrl
{
	public partial class SelectPeople : System.Web.UI.Page
	{
        protected int count;
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                WX.Data.Dict.BindListCtrl_DeptList(this.ddlDept,null, null, null);
                this.ddlDept.Items.Insert(0, new ListItem("--所有人员--", "0"));
                BindEmployeeByDeptId();
                if (!string.IsNullOrEmpty(Request.QueryString["Params"]) && Request.QueryString["Params"] != "undefined")
                {
                    InitSelectedEmployee(Request.QueryString["Params"]);
                }
            }
		}
        private void InitSelectedEmployee(string Params)
        {
            if (!String.IsNullOrEmpty(Params))
            {
                string[] peoples = Params.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string userId in peoples)
                {
                    string name = XSql.GetData("SELECT RealName FROM TU_Users WHERE UserID='" + userId + "'").ToString();
                    ListItem item = new ListItem(name, userId);
                    this.lbRight.Items.Add(item);
                }
            }
        }
        private void BindEmployeeByDeptId(int deptId = 0)
        {
            string companyId = Request.QueryString["CompanyId"];
            StringBuilder selList = new StringBuilder();
            for (int i = 0; i < lbRight.Items.Count; i++)
            {
                if (selList.Length > 0) selList.Append(",");
                selList.AppendFormat("'{0}'", lbRight.Items[i].Value);
            }
            String con=String.Empty;
            if (selList.Length > 0)
                con = String.Format(" and userid not in ({0})", selList);
            DataTable dataTable = XSql.GetDataTable("SELECT UserID,RealName,DepartmentID FROM vw_Employees WHERE CompanyID=" + companyId + con+" and State>=10 and State<40");
            if (deptId == 0)
            {
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
            DataTable dataTable = XSql.GetDataTable("SELECT * FROM vw_Employees WHERE CompanyID=" + companyId + " and State>=10 and State<40");
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