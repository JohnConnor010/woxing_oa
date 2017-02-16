using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;
using Wuqi.Webdiyer;
using WX;
using System.Text;
using WX.Data;

namespace wwwroot.Manage.Assets
{
    public partial class Ass_LogsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sql = "SELECT A.*,W.ProductName FROM Ass_Logs AS A INNER JOIN Ass_Warehouse AS W ON A.ProductID=W.ProductID WHERE A.ID > 0";
                InitComponent(true,sql);
            }
        }
        private void InitComponent(bool start,string sql)
        {
            Dict.BindListCtrl_DeptList(this.ddlDepartment, null, "0#--所有部门--", null);
            var dataTable  = WX.Main.GetPagedRows(sql, 0, "ORDER BY OpTime DESC", 20, this.AspNetPager1.CurrentPageIndex);
            var logsData = dataTable.AsEnumerable().Select(l => new
                {
                    ID  = l.Field<int>("ID"),
                    TypeName = l.Field<string>("Type"),
                    Manager = CommonUtils.GetRealNameListByUserIdList(l.Field<string>("OpUserID")),
                    OpTime = l.Field<string>("OpTime"),
                    UserInfo = GetUserString(l.Field<Nullable<int>>("DepartmentID").ToString(),l.Field<string>("UserID")),
                    ProductInfo = GetProductInfo(l.Field<string>("ProductID"),l.Field<string>("ProductName"),l.Field<int>("Quantity"),l.Field<decimal>("Price"))                    
                });
            this.LogsView.DataSource = logsData;
            this.LogsView.DataBind();
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
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlType.SelectedItem.Value != "所有类型")
            {
                sqlBuilder.Append(" AND A.Type='" + this.ddlType.SelectedItem.Value + "'");
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text) && string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sqlBuilder.Append(" AND OpTime > '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtStartDate.Text)) + "'");
            }
            if (string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sqlBuilder.Append(" AND OpTime < '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtEndDate.Text)) + "'");
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sqlBuilder.Append(" AND OpTime BETWEEN '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtStartDate.Text)) + "' AND '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtEndDate.Text)) + "'");
            }
            if (this.ddlDepartment.SelectedItem.Value != "0")
            {
                sqlBuilder.Append(" AND A.DepartmentID=" + this.ddlDepartment.SelectedItem.Value);
            }
            if (this.hidden_ddlUserID.Value != "")
            {
                sqlBuilder.Append(" AND A.UserID='" + this.hidden_ddlUserID.Value + "'");
            }
            string sql = "SELECT A.*,W.ProductName FROM Ass_Logs AS A INNER JOIN Ass_Warehouse AS W ON A.ProductID=W.ProductID WHERE A.ID > 0" + sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);
        }
        private string GetUserString(string departmentId,string userString)
        {
            string userInfo = string.Empty;
            Guid result;
            if (string.IsNullOrEmpty(userString))
            {
                userInfo = "";
            }
            else if (ULCode.Validation.IsNumber(userString))
            {
                userInfo = "供应商：" + (string)XSql.GetValue("SELECT CompanyName FROM Ass_Suppliers WHERE SupplierID=" + userString);
                
            }
            else if (Guid.TryParse(userString, out result))
            {
                userInfo = CommonUtils.GetDeptNameListByDeptIdList(departmentId.ToString()) + "   " + CommonUtils.GetRealNameListByUserIdList(userString);
            }
            return userInfo;
        }
        private string GetProductInfo(string productId, string productName,int quantity,decimal price)
        {
            string info = "产品编号：" + productId + "&nbsp;&nbsp;&nbsp;&nbsp;产品名称：" + productName + "&nbsp;&nbsp;&nbsp;&nbsp;总价：" + (quantity * price);
            return "<a href='javascript:void(0)' onclick=\"PreviewProductInfo('" + productId + "')\">" + info + "</a>";
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            if (this.ddlType.SelectedItem.Value != "所有类型")
            {
                sqlBuilder.Append(" AND A.Type='" + this.ddlType.SelectedItem.Value + "'");
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text) && string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sqlBuilder.Append(" AND OpTime > '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtStartDate.Text)) + "'");
            }
            if (string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sqlBuilder.Append(" AND OpTime < '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtEndDate.Text)) + "'");
            }
            if (!string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                sqlBuilder.Append(" AND OpTime BETWEEN '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtStartDate.Text)) + "' AND '" + string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(this.txtEndDate.Text)) + "'");
            }
            if (this.ddlDepartment.SelectedItem.Value != "0")
            {
                sqlBuilder.Append(" AND A.DepartmentID=" + this.ddlDepartment.SelectedItem.Value);
            }
            if (this.hidden_ddlUserID.Value != "")
            {
                sqlBuilder.Append(" AND A.OpUserID='" + this.hidden_ddlUserID.Value + "'");
            }
            string sql = "SELECT A.*,W.ProductName FROM Ass_Logs AS A INNER JOIN Ass_Warehouse AS W ON A.ProductID=W.ProductID WHERE A.ID > 0" + sqlBuilder.ToString();
            int pageIndex = this.AspNetPager1.CurrentPageIndex;
            InitComponent(false, sql);
            
        }
    }
}