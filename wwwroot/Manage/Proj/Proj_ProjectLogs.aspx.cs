using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_ProjectLogs : System.Web.UI.Page
    {
        public int companyId = 11;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable projlist = ULCode.QDA.XSql.GetDataTable("select ID,ProjectName from PRO_Projects order by ID desc");
                for (int i = 0; i < projlist.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(new ListItem(projlist.Rows[i]["ProjectName"].ToString(), projlist.Rows[i]["ID"].ToString()));
                }
                gridviewBind(true);
            }
        }
        private void gridviewBind(bool start)
        {
            string sql = "select log.*,pp.ProjectName from [PRO_Logs] log left join PRO_Projects pp on log.PID=pp.ID";
            if (DropDownList1.SelectedValue != "")
                sql += " where log.PID="+DropDownList1.SelectedValue;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 100;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            DataTable logData = WX.Main.GetPagedRows(sql, 0, " order by Addtime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

            this.Gv_company.DataSource = logData;
            this.Gv_company.DataBind();
            Gv_company.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_company.HeaderStyle.Height = Unit.Pixel(40);
            this.AspNetPager1.AlwaysShow = true;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gridviewBind(false);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridviewBind(true);
        }
    }
}