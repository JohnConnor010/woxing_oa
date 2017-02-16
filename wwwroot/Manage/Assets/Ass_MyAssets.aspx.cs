using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_MyAssets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = Request.QueryString["UserID"];
                if (userId == null)
                    userId = WX.Main.CurUser.UserID;
                string sql = "SELECT E.*,W.ProductName,AC.Name CategoryName,(case E.Quantity when 0 then 'color:#999;' else '' end) color FROM Ass_Equipment AS E LEFT JOIN Ass_Warehouse AS W ON E.ProductID=W.ProductID left join Ass_Category AC on W.CategoryID=AC.ID WHERE E.UserID='" + userId + "'";
                InitComponent(true, sql);
            }
        }
        private void InitComponent(bool start, string sql)
        {
            DataTable consumingData = WX.Main.GetPagedRows(sql, 0, "ORDER BY AddDate DESC", 20, AspNetPager1.CurrentPageIndex);
            var consumings = consumingData.AsEnumerable().Select(e => new
            {
                ProductID = e.Field<string>("ProductID"),
                ProductName = e.Field<string>("ProductName"),
                CategoryName = e.Field<string>("CategoryName"),
                Quantity = e.Field<int>("Quantity"),
                AddDate = e.Field<DateTime>("AddDate").ToString("yyyy-MM-dd"),
                Price = e.Field<decimal>("Price"),
                UnitName = e.Field<string>("Unit"),
                color = e.Field<string>("color"),
                Remark = e.Field<string>("Remark")
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            string userId = Request.QueryString["UserID"];
            string sql = "SELECT E.*,W.ProductName FROM Ass_Equipment AS E LEFT JOIN Ass_Warehouse AS W ON E.ProductID=W.ProductID WHERE E.UserID='" + userId + "'";
            InitComponent(false, sql);
        }
    }
}