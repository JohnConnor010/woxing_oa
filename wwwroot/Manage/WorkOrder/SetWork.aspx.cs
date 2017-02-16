using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Manage.WorkOrder
{
    public partial class SetWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
                if (Request["sdate"] != null && Request["sdate"] != "")
                {

                    WX.Main.ExcuteUpdate("TE_Schedules", "sHours=case sHours when 7 then 0 else 7  end", "sDate='" + Request["sdate"] + "'");
                    dtime = Convert.ToDateTime(Convert.ToDateTime(Request["sdate"]).ToString("yyyy-MM-01"));
                }
                HiddenYear.Value = dtime.Year.ToString();
                HiddenMonth.Value = dtime.Month.ToString();
                Literal1.Text = HiddenYear.Value + "年" + HiddenMonth.Value + "月"; 
                Initdays(dtime);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DateTime dtime = new DateTime(Convert.ToInt32(HiddenYear.Value), Convert.ToInt32(HiddenMonth.Value), 1);
            dtime = dtime.AddMonths(1);
            HiddenYear.Value = dtime.ToString("yyyy");
            HiddenMonth.Value = dtime.ToString("MM");
            Literal1.Text = HiddenYear.Value + "年" + HiddenMonth.Value + "月";
            Initdays(dtime);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DateTime dtime = new DateTime(Convert.ToInt32(HiddenYear.Value), Convert.ToInt32(HiddenMonth.Value), 1);
            dtime = dtime.AddMonths(-1);
            HiddenYear.Value = dtime.ToString("yyyy");
            HiddenMonth.Value = dtime.ToString("MM");
            Literal1.Text = HiddenYear.Value + "年" + HiddenMonth.Value + "月";
            Initdays(dtime);
        }
        private void Initdays(DateTime dtime)
        {
            string[] Day = new string[] { "周天", "周一", "周二", "周三", "周四", "周五", "周六" };

            System.Data.DataTable dt;
            dt = ULCode.QDA.XSql.GetDataTable("select * from TE_Schedules where year(sDate)=" + HiddenYear.Value + " and month(sDate)=" + HiddenMonth.Value);
                Button1.Visible = dt.Rows.Count==0;
            System.Data.DataTable nowmonth = new System.Data.DataTable();
            nowmonth.Columns.Add("sDate");
            nowmonth.Columns.Add("sDay");
            nowmonth.Columns.Add("sWeekNum");
            nowmonth.Columns.Add("sHours");
            nowmonth.Columns.Add("sColor");
            int maxDate = dtime.AddMonths(1).AddDays(-1).Day;
            for (int i = 1; i <= maxDate; i++)
            {
                System.Data.DataRow drow = nowmonth.NewRow();
                DateTime ss = Convert.ToDateTime(dtime.ToString("yyyy-MM-" + i));
                drow["sDay"] = ss.ToString("dd");
                drow["sDate"] = ss.ToString("yyyy-MM-dd");
                drow["sWeekNum"] = Day[Convert.ToInt32(ss.DayOfWeek.ToString("d"))].ToString();
                System.Data.DataRow[] dtrow= dt.Select("sDate='" + ss.ToString("yyyy-MM-dd") + "'");
                if (dtrow != null && dtrow.Length > 0)
                {
                    drow["sHours"] = dtrow[0]["sHours"];
                    drow["sColor"] = dtrow[0]["sHours"].ToString() == "0" ? "#D0A2A2" : "#B9D5CB";
                }
                else
                {
                    drow["sHours"] = "-1";
                    drow["sColor"] = "#D1D1D1";
                }
                nowmonth.Rows.Add(drow);
            }

            DataList1.DataSource = nowmonth;
            DataList1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DateTime dtime = new DateTime(Convert.ToInt32(HiddenYear.Value), Convert.ToInt32(HiddenMonth.Value), 1);
            int maxDate = dtime.AddMonths(1).AddDays(-1).Day;
            int weekday;
            DateTime ss;
            for (int i = 1; i <= maxDate; i++)
            {
                ss = Convert.ToDateTime(dtime.ToString("yyyy-MM-" + i));
                weekday=Convert.ToInt32(ss.DayOfWeek.ToString("d"))+1;
                ULCode.QDA.XSql.Execute("insert into TE_Schedules values('" + ss.ToString("yyyy-MM-dd") + "'," +weekday + ","+(weekday==1||weekday==7?0:7)+")");
            }
            Initdays(dtime);
        }
    }
}