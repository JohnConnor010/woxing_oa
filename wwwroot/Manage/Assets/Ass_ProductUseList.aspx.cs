using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using WX;
using ULCode.QDA;
namespace wwwroot.Manage.Assets
{
    public partial class Ass_ProductUseList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ProductID"]))
                {
                    string sql = "SELECT * FROM Ass_Logs WHERE ProductID='" + Request.QueryString["ProductID"] + "'";
                    InitComponent(true, sql);
                }
            }
        }
        private void InitComponent(bool start, string sql)
        {
            DataTable dataTable = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID DESC", 20, this.AspNetPager1.CurrentPageIndex);
            var logsData = dataTable.AsEnumerable().Select(l => new
            {
                ID = l.Field<int>("ID"),
                TypeName = l.Field<string>("Type"),
                Manager = CommonUtils.GetRealNameListByUserIdList(l.Field<string>("OpUserID")),
                OpTime = l.Field<string>("OpTime"),
                UserName = GetUserString(l.Field<string>("UserID")),
                DepartmentName = CommonUtils.GetDeptNameListByDeptIdList(l.Field<Nullable<int>>("DepartmentID").ToString()),
                Quantity = l.Field<int>("Quantity"),
                Price = l.Field<decimal>("Price"),
                UnitName = l.Field<string>("Unit")
            });
            this.ProductRepeater.DataSource = logsData;
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
                this.AspNetPager1.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Ass_Logs WHERE ProductID='" + Request.QueryString["ProductID"] + "'";
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
    }
}