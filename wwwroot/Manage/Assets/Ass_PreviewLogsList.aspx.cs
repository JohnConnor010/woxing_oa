using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_PreviewLogsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string type = Request.QueryString["type"];
                string userId = Request.QueryString["UserID"];
                string sql = "SELECT A.*,E.RealName,W.ProductName FROM Ass_Logs AS A LEFT JOIN TU_Users AS E ON A.OpUserID=E.UserID LEFT JOIN Ass_Warehouse AS W ON A.ProductID=W.ProductID WHERE Type='" + type + "' AND A.UserID='" + userId + "'";
                InitComponent(true, sql);
            }
        }

        private void InitComponent(bool start, string sql)
        {
            DataTable consumingData = WX.Main.GetPagedRows(sql, 0, "ORDER BY OpTime DESC", 20, AspNetPager1.CurrentPageIndex);
            var consumings = consumingData.AsEnumerable().Select(e => new
            {
                ID = e.Field<int>("ID"),
                RealName = e.Field<string>("RealName"),
                OpTime = e.Field<string>("OpTime"),
                MaturityDate = e.Field<Nullable<DateTime>>("MaturityDate"),
                Quantity = e.Field<int>("Quantity"),
                ProductName = e.Field<string>("ProductName"),
                ProductID = e.Field<string>("ProductID"),
                UnitName = e.Field<string>("Unit"),
                Price = e.Field<decimal>("Price")

            });
            this.LogsRepeater.DataSource = consumings;
            this.LogsRepeater.DataBind();
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
            string type = Request.QueryString["type"];
            string userId = Request.QueryString["UserID"];
            string sql = "SELECT A.*,E.RealName,W.ProductName FROM Ass_Logs AS A LEFT JOIN TU_Users AS E ON A.OpUserID=E.UserID LEFT JOIN Ass_Warehouse AS W ON A.ProductID=W.ProductID WHERE Type='" + type + "' AND A.UserID='" + userId + "'";
            InitComponent(false, sql);
        }
    }
}