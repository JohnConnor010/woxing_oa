using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wwwroot.Manage.Finance
{
    public partial class FD_UserList : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageinit("");
            }
        }
        private void pageinit(string orderBy)
        {
            string vwnmae = "vw_Employees_FD10";
            string where =" State=20";
            DataTable dt = ULCode.QDA.XSql.GetDataTable("select * FROM " + vwnmae + " WHERE " + where + orderBy);
            Gv_intojobs.DataSource = dt;
            Gv_intojobs.DataBind();
            if (Gv_intojobs.Rows.Count > 0)
            {
                Gv_intojobs.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_intojobs.HeaderStyle.Height = Unit.Pixel(40);
            }
            if (Request["mes"] != null)
            {
                WX.Main.MessageToHistory_where(String.Format("SendToUserId='{0}' and Title like'%FD_UserList.aspx%'", WX.Main.CurUser.UserID));
            }    
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text == "True" ? "男" : "女";
            }
        }

        protected void Gv_intojobs_Sorting(object sender, GridViewSortEventArgs e)
        {
            Literal li = (Literal)Gv_intojobs.Parent.FindControl("liHidden_" + e.SortExpression);
            this.pageinit(String.Format("order by {0} {1}", e.SortExpression, li.Text));
            if (li.Text == "")
                li.Text = "Desc";
            else if (li.Text.EndsWith("Asc"))
                li.Text = li.Text.Replace("Asc", "Desc");
            else
                li.Text = li.Text.Replace("Desc", "Asc");
        }
    }
}