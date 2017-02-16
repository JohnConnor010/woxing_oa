using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Proj
{
    public partial class Proj_ProjectUser : System.Web.UI.Page
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
            string sql = "select TU_Users.RealName,TU_Users.UserID,dbo.TE_Departments.Name AS DepartmentName, dbo.TE_Duties.Name AS DutyName from TU_Users LEFT JOIN dbo.TE_Departments ON dbo.TU_Users.DepartmentID = dbo.TE_Departments.ID LEFT JOIN dbo.TE_Duties ON dbo.TU_Users.DutyId = dbo.TE_Duties.ID  where TU_Users.UserID in(SELECT distinct pu.UserID FROM [PRO_User] pu left join PRO_State ps on pu.PID=ps.ProjID where ps.State<=1 )";
            DataTable logData = ULCode.QDA.XSql.GetDataTable(sql);
            this.Gv_company.DataSource = logData;
            this.Gv_company.DataBind();
            Gv_company.HeaderRow.TableSection = TableRowSection.TableHeader;
            Gv_company.HeaderStyle.Height = Unit.Pixel(40);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = ULCode.QDA.XSql.GetDataTable("select pu.PID,pp.ProjectName from [PRO_User] pu left join PRO_Projects pp on pu.PID=pp.ID where pp.State in(2,4) and pu.UserID='"+Gv_company.DataKeys[e.Row.RowIndex].Value+"'");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ProjectName"].ToString().Trim() != "")
                        e.Row.Cells[4].Text += "<a href='Proj_ProjectCheck.aspx?ProjectId=" + dt.Rows[i]["PID"] + "'>" + dt.Rows[i]["ProjectName"] + "</a>";
                }
            }

        }
    }
}