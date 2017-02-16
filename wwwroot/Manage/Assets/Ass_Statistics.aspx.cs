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
    public partial class Ass_Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "SELECT ProductID,ProductName,Unit,Price,Specification,Model,Color,Brand,CompanyName"
                            + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='入库') as 入库数量"
                            + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='入库') AS 入库价格"
                            + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='领用') as 领用数量"
                            + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='领用') AS 领用价格" 
                            + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='出售') as 出售数量"
                            + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='出售') AS 出售价格" 
                            + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='销毁') as 销毁数量"
                            + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='销毁') AS 销毁价格"
                            + " FROM ASS_Warehouse LEFT JOIN Ass_Suppliers AS S ON Suppliers = S.SupplierID WHERE ID > 0 ";
                InitComponent(true,sql);
            }
        }
        private void InitComponent(bool start, string sql)
        {
            this.ddlMonth.Items.Clear();
            var items = Enumerable.Range(1, 12).Select(month => new ListItem
            {
                Text = month + "月份",
                Value = month.ToString(),
                Selected = month == DateTime.Now.Month
            });
            foreach (ListItem item in items)
            {
                this.ddlMonth.Items.Add(item);
            }
            this.ddlMonth.Items.Insert(0, new ListItem("所有月份", "0"));
            var dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ProductID DESC", 20, AspNetPager1.CurrentPageIndex);
            this.ProductRepeater.DataSource = dataTable;
            this.ProductRepeater.DataBind();
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
        protected int GetValue(object value)
        {
            if (value == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlYear.SelectedItem.Value != "所有年")
            {
                sqlBuilder.Append(" AND DATEPART(Year,LastTime)='" + this.ddlYear.SelectedItem.Value + "'");
            }
            if (this.ddlMonth.SelectedItem.Value != "0")
            {
                sqlBuilder.Append(" AND DATEPART(MONTH,LastTime)='" + this.ddlMonth.SelectedItem.Value + "'");
            }
            string sql = "SELECT ProductID,ProductName,Unit,Price,Specification,Model,Color,Brand,CompanyName"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='入库') as 入库数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='入库') AS 入库价格"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='领用') as 领用数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='领用') AS 领用价格"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='出售') as 出售数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='出售') AS 出售价格"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='销毁') as 销毁数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='销毁') AS 销毁价格"
                           + " FROM ASS_Warehouse LEFT JOIN Ass_Suppliers AS S ON Suppliers = S.SupplierID WHERE ID > 0 " +sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlYear.SelectedItem.Value != "所有年")
            {
                sqlBuilder.Append(" AND DATEPART(Year,LastTime)='" + this.ddlYear.SelectedItem.Value + "'");
            }
            if (this.ddlMonth.SelectedItem.Value != "0")
            {
                sqlBuilder.Append(" AND DATEPART(MONTH,LastTime)='" + this.ddlMonth.SelectedItem.Value + "'");
            }
            string sql = "SELECT ProductID,ProductName,Unit,Price,Specification,Model,Color,Brand,CompanyName"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='入库') as 入库数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='入库') AS 入库价格"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='领用') as 领用数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='领用') AS 领用价格"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='出售') as 出售数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='出售') AS 出售价格"
                           + ",(SELECT SUM(Quantity) FROM Ass_logs WHERE ProductID=ass_Warehouse.ProductID AND Type='销毁') as 销毁数量"
                           + ",(SELECT (SUM(Quantity) * SUM(price)) FROM Ass_Logs WHERE ProductID=ass_Warehouse.ProductID AND Type='销毁') AS 销毁价格"
                           + " FROM ASS_Warehouse LEFT JOIN Ass_Suppliers AS S ON Suppliers = S.SupplierID WHERE ID > 0 " +sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);
        }
    }
}