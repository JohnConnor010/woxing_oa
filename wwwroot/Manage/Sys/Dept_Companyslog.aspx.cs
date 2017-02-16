using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace wwwroot.Manage.Sys
{
    public partial class Dept_Companyslog : System.Web.UI.Page
    {
        public int companyId = 11;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 0; i < WX.Model.Company.logtypearry.Length; i++)
                {
                    DropDownList1.Items.Add(new ListItem(WX.Model.Company.logtypearry[i],i.ToString()));
                }
                if (Request["type"] != null && Request["type"] != "")
                    DropDownList1.SelectedValue = Request["type"];
                    gridviewBind(true);
            }
        }
        private void gridviewBind(bool start)
        {
            string sql = "select log.*,te.RealName ZRName,te2.RealName CZName from TE_Companys_Logs log left join TU_Users te on log.Manage=te.UserID left join TU_Users te2 on log.SetManage=te2.UserID";
            if(DropDownList1.SelectedValue!="")
                sql += " where type=" + DropDownList1.SelectedValue;
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 50;
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
            string str = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label l1 = (Label)e.Row.FindControl("Label1");
                str = l1.Text.Trim();
                l1.Text = "";
                if (str.Split('|')[0] == "1")
                {
                    e.Row.Style.Value = "font-weight:bold;";
                    l1.Text = "";
                    if (ULCode.QDA.XSql.GetDataTable("select * from TE_Companys where LinkID=" + Gv_company.DataKeys[e.Row.RowIndex].Value + " and LinkType=2").Rows.Count <= 0)
                        l1.Text = "<a href='Dept_CompanyInfo.aspx?ltype=2&lid=" + Gv_company.DataKeys[e.Row.RowIndex].Value + "'>母公司</a>&nbsp;&nbsp;";
                    l1.Text += "<a href='Dept_CompanyInfo.aspx?ltype=3&lid=" + Gv_company.DataKeys[e.Row.RowIndex].Value + "'>子公司</a>&nbsp;&nbsp;<a href='Dept_CompanyInfo.aspx?ltype=4&lid=" + Gv_company.DataKeys[e.Row.RowIndex].Value + "'>控股公司</a>";
                    ((LinkButton)e.Row.FindControl("LinkButton2")).Visible = false;
                }
                else if (str.Split('|')[0] == "2" || str.Split('|')[0] == "3" || str.Split('|')[0] == "4")
                {
                    e.Row.Cells[2].Style.Value = "padding-left:20px;";
                }
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridviewBind(true);
        }
    }
}