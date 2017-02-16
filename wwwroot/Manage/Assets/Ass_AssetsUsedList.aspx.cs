using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WX;
using ULCode.QDA;
using WX.Data;
using System.Data;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_AssetsUsedList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "SELECT * FROM Ass_Logs WHERE ID > 0";
                InitComponent(false, sql);
            }
        }
        private void InitComponent(bool start, string sql)
        {
            var category = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'Ass_Category','ID','Name','ParentID','ID',0,1,5");
            this.ddlCategoryID.DataSource = category;
            this.ddlCategoryID.DataTextField = "name";
            this.ddlCategoryID.DataValueField = "id";
            this.ddlCategoryID.DataBind();
            var dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID DESC", 13, this.AspNetPager1.CurrentPageIndex);
            var logsData = dataTable.AsEnumerable().Select(l => new
            {
                ID = l.Field<int>("ID"),
                TypeName = l.Field<string>("Type"),
                Manager = CommonUtils.GetRealNameListByUserIdList(l.Field<string>("OpUserID")),
                OpTime = l.Field<string>("OpTime"),
                UserName = GetUserString(l.Field<string>("UserID")),
                DepartmentName = CommonUtils.GetDeptNameListByDeptIdList(l.Field<Nullable<int>>("DepartmentID").ToString()),
                Quantity = l.Field<int>("Quantity"),
                ProductID = l.Field<string>("ProductID"),
                Price = l.Field<decimal>("Price"),
                UnitName = l.Field<string>("Unit")
            });
            this.LogsView.DataSource = logsData;
            this.LogsView.DataBind();
            this.AspNetPager1.AlwaysShow = true;

            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 13;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            else
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.hidden_ddlProductList.Value))
            {
                sqlBuilder.Append(" AND ProductID='" + this.hidden_ddlProductList.Value + "'");
            }
            string sql = "SELECT * FROM Ass_Logs WHERE ID > 0" + sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);
        }
        private string GetUserString(string userString)
        {
            string str = string.Empty;
            Guid result;
            if (string.IsNullOrEmpty(userString))
            {
                str = "";
            }
            else if (ULCode.Validation.IsNumber(userString))
            {
                str = (string)XSql.GetValue("SELECT CompanyName FROM Ass_Suppliers WHERE SupplierID=" + userString);
            }
            else if (Guid.TryParse(userString, out result))
            {
                str = CommonUtils.GetRealNameListByUserIdList(userString);
            }
            return str;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.hidden_ddlProductList.Value))
            {
                sqlBuilder.Append(" AND ProductID='" + this.hidden_ddlProductList.Value + "'");
            }
            string sql = "SELECT * FROM Ass_Logs WHERE ID > 0" + sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);

        }
    }
}