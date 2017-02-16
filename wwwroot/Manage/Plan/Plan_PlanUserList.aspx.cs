using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.Plan
{
    public partial class Plan_PlanUserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.DataColumn col=new System.Data.DataColumn("date");
                dt.Columns.Add(col);
                System.Data.DataColumn col2 = new System.Data.DataColumn("UserID");
                dt.Columns.Add(col2);
                System.Data.DataColumn col3 = new System.Data.DataColumn("type");
                dt.Columns.Add(col3);
                if (Request["type"] == "3")
                {for (int i = 0; i < 12; i++)
                    {
                        System.Data.DataRow row = dt.NewRow();
                        row["date"] = DateTime.Now.AddMonths(-i).ToString("yyyy-MM-dd");
                        row["UserID"] = Request["UserID"];
                        row["type"] = Request["type"];
                        dt.Rows.Add(row);
                    }
                    
                }else if (Request["type"] == "2")
                {
                    for (int i = 0; i < 4; i++)
                    {
                        System.Data.DataRow row = dt.NewRow();
                        row["date"] = DateTime.Now.AddDays(-(i*7)).ToString("yyyy-MM-dd");
                        row["UserID"] = Request["UserID"];
                        row["type"] = Request["type"];
                        dt.Rows.Add(row);
                    }
                }
                else
                {
                    for (int i = 0; i < 31; i++)
                    {
                        System.Data.DataRow row = dt.NewRow();
                        row["date"] = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                        row["UserID"] = Request["UserID"];
                        row["type"] = Request["type"];
                        dt.Rows.Add(row);
                    }
                }

                
                DataList1.DataSource = dt;
                DataList1.DataBind();
            }
        }
    }
}