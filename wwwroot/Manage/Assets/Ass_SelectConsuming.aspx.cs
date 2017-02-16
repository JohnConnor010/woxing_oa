using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using WX;
using WX.Data;
using System.Text;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_SelectConsuming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = Request.QueryString["UserID"];
                string sql = "SELECT * FROM Ass_Equipment WHERE UserID='" + userId + "'";
                InitComponent(true,sql);
            }
        }
        private void InitComponent(bool start, string sql)
        {
            DataTable consumingData = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID DESC", 10, AspNetPager1.CurrentPageIndex);
            var consumings = consumingData.AsEnumerable().Select(c => new
                {
                    ID = c.Field<int>("ID"),
                    ProductID = c.Field<string>("ProductID"),
                    ProductName = GetProductNameByProductID(c.Field<string>("ProductID")),
                    ProductUnit = c.Field<string>("Unit"),
                    Quantity = c.Field<int>("Quantity"),
                    UserName = CommonUtils.GetRealNameListByUserIdList(c.Field<string>("UserID")),
                    Price = c.Field<decimal>("Price"),
                    DepartmentName = CommonUtils.GetDeptNameListByDeptIdList(c.Field<int>("DepartmentID").ToString()),
                    AddDate = c.Field<Nullable<DateTime>>("AddDate")
                });
            this.ConsumingRepeater.DataSource = consumings;
            this.ConsumingRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;

            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 10;
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
            string userId = Request.QueryString["UserID"];
            string sql = "SELECT * FROM Ass_Equipment WHERE UserID='" + userId + "'";
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);
        }
    }
}