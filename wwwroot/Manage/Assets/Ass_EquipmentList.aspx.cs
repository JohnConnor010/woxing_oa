using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WX;
using ULCode.QDA;
using WX.Data;
using System.Text;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_EquipmentList : System.Web.UI.Page
    {
        string sql = "SELECT tuuser.UserID,tuuser.RealName,Sex,Mobile,QQ,Email,tuuser.State FROM TU_Users tuuser left join TU_Employees tuemp on tuuser.UserID=tuemp.UserID where tuuser.State>=10";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true, sql);
            }
        }
        private void InitComponent(bool start, string ssql)
        {
            Dict.BindListCtrl_DeptList(this.ddlDepartment, null, "0#--所有部门--", null);
            DataTable consumingData = WX.Main.GetPagedRows(ssql, 0, " order by State asc", 20, AspNetPager1.CurrentPageIndex);
            var consumings = consumingData.AsEnumerable().Select(e => new
            {
                UserID = e.Field<Guid>("UserID"),
                RealName = e.Field<string>("RealName"),
                Sex = e.Field<bool>("Sex") == true ? "男" : "女",
                Mobile = e.Field<string>("Mobile"),
                QQ = e.Field<string>("QQ"),
                Email = e.Field<string>("Email")
            });
            this.EquipmentRepeater.DataSource = consumings;
            this.EquipmentRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;

            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            else
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;
            }
        }
        public string GetProductNameByProductID(string ProductID)
        {
            return XSql.GetValue("SELECT ProductName FROM Ass_Warehouse WHERE ProductID='" + ProductID + "'").ToString();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlDepartment.SelectedItem.Value != "0")
            {
                sqlBuilder.Append(" and tuuser.DepartmentID=" + this.ddlDepartment.SelectedItem.Value);
            }
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql + sqlBuilder.ToString());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlDepartment.SelectedItem.Value != "0")
            {
                sqlBuilder.Append(" WHERE tuuser.DepartmentID=" + this.ddlDepartment.SelectedItem.Value);
            }
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql+ sqlBuilder.ToString() );
        }
    }
}