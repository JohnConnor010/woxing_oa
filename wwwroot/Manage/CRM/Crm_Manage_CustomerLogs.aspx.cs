using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_Manage_CustomerLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.pageInit(true);
            }

        }
        private void pageInit(bool start)
        {
            WX.Main.CurUser.LoadDutyDetailUser();
            string sql = "select CRM_Logs.* from CRM_Logs left join Tu_Users on CRM_Logs.UserID=Tu_Users.UserID  where Tu_Users.DepartmentID=" + WX.Main.CurUser.UserModel.DepartmentID.ToString();
             if (WX.Main.CurUser.DutyDetailUser.DutyID.ToInt32() >= 900)                 
            sql = "select * from CRM_Logs";
            var supplierData = WX.Main.GetPagedRows(sql, 0, "ORDER BY ID desc", 50, AspNetPager1.CurrentPageIndex);
            System.Data.DataTable dataTable = supplierData;
            Gv_customer.DataSource = dataTable;
            Gv_customer.DataBind();
            if (Gv_customer.Rows.Count > 0)
            {
                Gv_customer.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_customer.HeaderStyle.Height = Unit.Pixel(40);
            }

            this.AspNetPager1.AlwaysShow = true;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 50;
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
            pageInit(false);
        }
    }
}