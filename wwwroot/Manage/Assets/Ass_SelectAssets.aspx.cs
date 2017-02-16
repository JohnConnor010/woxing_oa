using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_SelectAssets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'Ass_Category','ID','Name','ParentID','ID',0,1,5");
                this.ddlCategory.DataSource = categoryData;
                this.ddlCategory.DataTextField = "name";
                this.ddlCategory.DataValueField = "id";
                this.ddlCategory.DataBind();
                this.ddlCategory.Items.Insert(0, new ListItem("--请选择--", "0"));
                string sql = "SELECT w.*,s.CompanyName,u.ID AS UnitID FROM Ass_Warehouse AS w LEFT JOIN Ass_Suppliers AS s ON w.Suppliers=s.SupplierID LEFT JOIN Ass_Unit AS u ON w.Unit=u.UnitName WHERE w.ID > 0";
                InitWarehouseData(true, sql);
            }
        }
        private void InitWarehouseData(bool start,string sql)
        {
            var dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ProductID DESC", 8, AspNetPager1.CurrentPageIndex);
            var query = dataTable.AsEnumerable().Select(p => new
                {
                    ID = p.Field<int>("ID"),
                    ProductID = p.Field<string>("ProductID"),
                    ProductName = p.Field<string>("ProductName"),
                    Unit = p.Field<string>("Unit"),
                    Quantity = p.Field<int>("Quantity"),
                    UsedQuantity = p.Field<int>("UsedQuantity"),
                    Price = p.Field<decimal>("Price"),
                    Supplier = p.Field<string>("Suppliers"),
                    CompanyName = p.Field<string>("CompanyName"),
                    Specification = p.Field<string>("Specification"),
                    Color = p.Field<string>("Color"),
                    Brand = p.Field<string>("Brand"),
                    Model = p.Field<string>("Model"),
                    LastTime = p.Field<Nullable<DateTime>>("LastTime") == null ? "" : p.Field<DateTime>("LastTime").ToString("yyyy-MM-dd"),
                    Remark = p.Field<string>("Remark"),
                    ProductPhoto = string.IsNullOrEmpty(p.Field<string>("ProductPhoto")) ?  "../images/no picture.jpg" : "../../" + p.Field<string>("ProductPhoto")

                });
            this.AssetsRepeater.DataSource = query;
            this.AssetsRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;

            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 8;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            else
            {
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.CurrentPageIndex = this.AspNetPager1.CurrentPageIndex;
            }

        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.txtProductName.Text))
            {
                sqlBuilder.Append(" AND ProductName='" + this.txtProductName.Text + "'");
            }
            if (!string.IsNullOrEmpty(this.ddlCategory.SelectedItem.Value) && this.ddlCategory.SelectedItem.Value != "1")
            {
                sqlBuilder.Append(" AND CategoryID=" + this.ddlCategory.SelectedItem.Value);
            }
            string sql = "SELECT w.*,s.CompanyName,u.ID AS UnitID FROM Ass_Warehouse AS w LEFT JOIN Ass_Suppliers AS s ON w.Suppliers=s.SupplierID LEFT JOIN Ass_Unit AS u ON w.Unit=u.UnitName WHERE w.ID > 0";
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitWarehouseData(false, sql);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.txtProductName.Text))
            {
                sqlBuilder.Append(" AND ProductName LIKE '%" + this.txtProductName.Text + "%'");
            }
            if (!string.IsNullOrEmpty(this.ddlCategory.SelectedItem.Value) && this.ddlCategory.SelectedItem.Value != "0")
            {
                //string sSql = String.Format("exec sp_get_tree_table 'Ass_CateGory','Id','Name','ParentId','Id',{0},1,4", 2);
                //string str = "2," + XSql.GetXDataTable(sSql).ToColValueList();
                sqlBuilder.Append(" AND CategoryID = " + this.ddlCategory.SelectedItem.Value);
            }
            //Response.Write(sqlBuilder);
            string sql = "SELECT w.*,s.CompanyName,u.ID AS UnitID FROM Ass_Warehouse AS w LEFT JOIN Ass_Suppliers AS s ON w.Suppliers=s.SupplierID LEFT JOIN Ass_Unit AS u ON w.Unit=u.UnitName WHERE w.ID > 0" + sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitWarehouseData(false, sql);
        }
    }
}