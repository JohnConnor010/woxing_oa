using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using System.Text;
using System.Web.Configuration;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_AssetsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sSql = null;
                //1.init category
                sSql = "exec [dbo].[sp_get_tree_table] 'Ass_Category','ID','Name','ParentID','ID',0,1,5";
                //DataTable categoryData = XSql.GetDataTable("exec [dbo].[sp_get_tree_table] 'Ass_Category','ID','Name','ParentID','ID',0,1,5");
                //this.ddlCategory.DataSource = categoryData;
                //this.ddlCategory.DataTextField = "name";
                //this.ddlCategory.DataValueField = "id";
                //this.ddlCategory.DataBind();
                //this.ddlCategory.Items.Insert(0, new ListItem("--请选择--", "0"));
                WX.Data.Dict.BindListCtrl(sSql, this.ddlCategory, null, "0#--请选择--", null);
                
                //2.init priceScope
                string priceScope = WebConfigurationManager.AppSettings["PriceScopes"];
                this.ddlPriceScope.Items.Clear();
                var items = priceScope.Split('|').Select((item, index) => new ListItem()
                {
                    Text = item.ToString(),
                    Value = (index + 1).ToString()
                });
                this.ddlPriceScope.Items.Add(new ListItem("所有价格", "0"));
                foreach (var item in items)
                {
                    this.ddlPriceScope.Items.Add(item);
                }

                //3.init warehouse data
                this.InitWarehouseData(true);
            }
        }
        private void InitWarehouseData(bool start)
        {
            string categoryId = this.ddlCategory.SelectedItem.Value;
            string scope = this.ddlPriceScope.SelectedItem.Text;
            StringBuilder sqlBuilder = new StringBuilder();
            if (categoryId != "0")
            {
                sqlBuilder.Append(" AND CategoryID=" + categoryId);
            }
            if (scope != "所有价格")
            {
                sqlBuilder.Append(GetPriceScopeString(scope));
            }
            string sql = "SELECT w.*,s.CompanyName,u.ID AS UnitID,c.Name FROM Ass_Warehouse AS w LEFT JOIN Ass_Suppliers AS s ON w.Suppliers=s.SupplierID LEFT JOIN Ass_Unit AS u ON w.Unit=u.UnitName LEFT JOIN Ass_Category AS c ON w.CategoryID=c.ID WHERE w.ID > 0" + sqlBuilder.ToString();
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            var dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ProductID DESC", this.AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            var query = dataTable.AsEnumerable().Select(p => new
            {
                ID = p.Field<int>("ID"),
                ProductID = p.Field<string>("ProductID"),
                ProductName = p.Field<string>("ProductName"),
                Unit = p.Field<string>("Unit"),
                Quantity = p.Field<int>("Quantity"),
                CategoryName = p.Field<string>("Name"),
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
                ProductPhoto = string.IsNullOrEmpty(p.Field<string>("ProductPhoto")) ? "../images/no picture.jpg" : "../../" + p.Field<string>("ProductPhoto")

            });
            this.WarehouseRepeater.DataSource = query;
            this.WarehouseRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitWarehouseData(false);
        }
        private string GetPriceScopeString(string scope)
        {
            string sql = string.Empty;
            if (scope != "所有价格")
            {
                if (scope.Contains("以下"))
                {
                    string price = scope.Replace("以下", "");
                    sql = " AND Price < " + price;
                }
                else if (scope.Contains("以上"))
                {
                    string price = scope.Replace("以上", "");
                    sql = " AND Price > " + price;
                }
                else if(scope.Split('-').Count() > 0)
                {
                    sql = String.Format(" AND Price BETWEEN {0} AND {1}", scope.Split('-')[0], scope.Split('-')[1]);
                }
            }
            return sql;
        }
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            string productId = e.CommandArgument.ToString();
            if (ULCode.QDA.XSql.IsHasRow("SELECT * FROM Ass_Logs WHERE ProductID='" + productId + "'"))
            {
                ULCode.Debug.Alert(this,"此产品已经应用不能删除！","Ass_AssetsList.aspx");
                return;
                    
            }
            string id = e.CommandArgument.ToString();
            int row = XSql.Execute(String.Format("DELETE FROM Ass_Warehouse WHERE ID='{0}'", id));
            if (row > 0)
            {
                WX.Main.AddLog(WX.LogType.Default, "产品信息删除成功！", null);
                ULCode.Debug.Alert(this,"产品信息删除成功！", "Ass_AssetsList.aspx");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string categoryId = this.ddlCategory.SelectedItem.Value;
            //string scope = this.ddlPriceScope.SelectedItem.Text;
            //StringBuilder sqlBuilder = new StringBuilder();
            //if (categoryId != "1")
            //{
            //    string sSql = String.Format("exec sp_get_tree_table 'Ass_CateGory','Id','Name','ParentId','Id',{0},1,4", 2);
            //    string str = "2," + XSql.GetXDataTable(sSql).ToColValueList();
            //    sqlBuilder.Append(" AND CategoryID IN (" + str + ")");
            //}
            //if (scope != "所有价格")
            //{
            //    sqlBuilder.Append(GetPriceScopeString(scope));
            //}
            //string sql = "SELECT w.*,s.CompanyName,u.ID AS UnitID,c.Name FROM Ass_Warehouse AS w LEFT JOIN Ass_Suppliers AS s ON w.Suppliers=s.SupplierID LEFT JOIN Ass_Unit AS u ON w.Unit=u.UnitName LEFT JOIN Ass_Category AS c ON w.CategoryID=c.ID WHERE w.ID > 0" + sqlBuilder.ToString();
            //int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitWarehouseData(true);
        }
    }
}