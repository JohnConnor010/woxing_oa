﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.CRM
{
    public partial class Crm_SingleM_Logs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.pageInit(true);
            }
        }
        private void pageInit(bool start)
        {
            string sql = "select * from CRM_Logs where LogType=11 and UserID='" + WX.Main.CurUser.UserID + "' and CustomerID="+WX.Request.rCustomerID;

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