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
    public partial class SupplierList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "SELECT * FROM Ass_Suppliers ";
                InitComponent(true, sql);
            }
        }
        private void InitComponent(bool start,string sql)
        {
            //DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'Ass_Category','ID','Name','ParentID','ID',1,1,5");
            //this.ddlCategory.DataSource = categoryData;
            //this.ddlCategory.DataTextField = "name";
            //this.ddlCategory.DataValueField = "id";
            //this.ddlCategory.DataBind();
            //this.ddlCategory.Items.Insert(0, new ListItem("--请选择--", "1"));
            
            var supplierData = WX.Main.GetPagedRows(sql, 0, "ORDER BY SupplierID DESC", 20, AspNetPager1.CurrentPageIndex);
            var query = supplierData.AsEnumerable().Select(p => new
            {
                SupplierID = p.Field<int>("SupplierID"),
                CompanyName = p.Field<string>("CompanyName"),
                ContactName = p.Field<string>("ContactName"),
                Telephone = p.Field<string>("Telephone"),
                Address = p.Field<string>("Address"),
                MobilePhone = p.Field<string>("MobilePhone"),
                Email = p.Field<string>("Email")
            });
            this.SupplierRepeater.DataSource = query;
            this.SupplierRepeater.DataBind();
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
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string supplierId = e.CommandArgument.ToString();
            int row = XSql.Execute("DELETE FROM Ass_Suppliers WHERE SupplierID=" + supplierId);
            if (row > 0)
            {
                ULCode.Debug.Alert("供应商信息删除成功！", "SupplierList.aspx");
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Ass_Suppliers ";
            InitComponent(false, sql);
        }
    }
}