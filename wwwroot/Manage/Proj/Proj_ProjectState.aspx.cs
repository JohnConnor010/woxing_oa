using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_ProjectState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true);
            }
        }
        private void InitComponent(bool start)
        {
            string sql = "SELECT ps.*,pp.ProjectName,pp.Persons FROM [PRO_State] ps left join [PRO_Projects] pp on ps.ProjID=pp.ID WHERE ps.State<=1";
            //Response.Write(sql);
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 50;
                this.AspNetPager1.CurrentPageIndex = 1;
            }

            //DataTable dataTable = ULCode.QDA.XSql.GetDataTable(sql);
            DataTable logData = WX.Main.GetPagedRows(sql, 0, "ORDER BY State desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            //Response.Write(logData.Rows.Count);
            this.Gv_company.DataSource = logData;
            this.Gv_company.DataBind();
            Gv_company.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_company.HeaderStyle.Height = Unit.Pixel(40);
            this.AspNetPager1.AlwaysShow = true;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ULCode.QDA.XDataTable xdt = ULCode.QDA.XSql.GetXDataTable("select pu.UserID,te.RealName from PRO_User pu left join TU_Users te on pu.UserID=te.UserID where pu.type=1 and pid=" + e.Row.Cells[4].Text);
               e.Row.Cells[4].Text = xdt.ToColValueList("，", 1);
            }
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitComponent(false);
        }
    }
}