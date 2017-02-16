using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.HR
{
    public partial class HR_DutyLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageinit();
            }
        }
        private void pageinit()
        {
            string userId = WX.Request.rUserId;
            WX.Model.User.MODEL usermodel = WX.Request.rUser;
            li_username.Text = usermodel.RealName.ToString();
            MenuBar1.Param2 = usermodel.State.ToString();
            System.Data.DataTable dt = WX.HR.DutyLog.GetList(userId);
            Gv_tfk.DataSource = dt;
            Gv_tfk.DataBind();
            if (Gv_tfk.Rows.Count > 0)
            {
                Gv_tfk.HeaderRow.TableSection = TableRowSection.TableHeader;
                Gv_tfk.HeaderStyle.Height = Unit.Pixel(40);
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string[] startarray0 = ((Label)e.Row.FindControl("Label3")).Text.Trim().Split('|');
                string[] startarray = ((Label)e.Row.FindControl("Label1")).Text.Trim().Split('|');
                string[] startarray2 = ((Label)e.Row.FindControl("Label2")).Text.Trim().Split('|');
                e.Row.Cells[4].Text="";
                DateTime startt= Convert.ToDateTime(startarray0[0]);
                DateTime stopt = startarray0[1] == "" ? DateTime.Now : Convert.ToDateTime(startarray0[1]);
                TimeSpan ts =stopt -startt;
                //e.Row.Cells[3].Text = (stopt.Year - startt.Year > 0 ? (stopt.Year - startt.Year).ToString() + "年" : "") + (stopt.Year - startt.Year > 0 ? ((stopt.Year - startt.Year) * 12).ToString() + "月" : "") + (ts.Days > 0 ? ts.Days + "天" : "") + (ts.Hours > 0 ? ts.Hours + "小时" : "") + (ts.Minutes > 0 ? ts.Minutes + "分钟" : "");
                e.Row.Cells[4].Text = (stopt.Year - startt.Year).ToString() + "年" +((stopt.Year - startt.Year) * 12).ToString() + "月" +(ts.Days==0?stopt.Day-startt.Day:ts.Days)+ "天";
                if (startarray[0] != "")
                {
                    e.Row.Cells[5].Text +=Convert.ToDateTime(startarray[3]).ToString("yyyy-MM-dd HH:mm:ss")+"<a href='" + WX.HR.DutyLog.tablesurlarry[Convert.ToInt32(startarray[0])] + "?UserID=" + startarray[2] + (startarray[1]!=""?"&id="+startarray[1]:"") + "'>" + WX.HR.DutyLog.tablesarry[Convert.ToInt32(startarray[0])] + "</a>";
                }
                e.Row.Cells[6].Text = "";
                if (startarray2[0] != "")
                {
                    e.Row.Cells[6].Text += (startarray2[3] == "" ? "" : Convert.ToDateTime(startarray2[3]).ToString("yyyy-MM-dd HH:mm:ss")) + "<a href='" + WX.HR.DutyLog.tablesurlarry[Convert.ToInt32(startarray2[0])] + "?UserID=" + startarray2[2] + (startarray2[1] != "" ? "&id=" + startarray2[1] : "") + "'>" + WX.HR.DutyLog.tablesarry[Convert.ToInt32(startarray2[0])] + "</a>";
                }
            }
        }
    }
}