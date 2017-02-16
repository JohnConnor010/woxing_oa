using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Priv
{
    public partial class PRIV_Default : System.Web.UI.Page
    {
        //代码接口
        private int SumUpFlag = 2;
        private DateTime ThisDate
        {
            get
            {
                if (Request.QueryString["Date"] != null)
                {
                    DateTime dt = DateTime.Now;
                    if (DateTime.TryParse(Convert.ToString(Request.QueryString["Date"]), out dt))
                    {
                        return dt;
                    }
                    else
                        return DateTime.Now;
                }
                else
                    return DateTime.Now;
            }
        }
        private String CurUserId
        {
            get
            {
                return WX.Main.CurUser.UserID;
            }
        }
        public String CurUserName
        {
            get
            {
                return WX.Main.CurUser.UserName;
            }
        }
        private bool IsLogined()
        {
            if (!WX.Authentication.IsAuthenticated)
            {
                Response.Redirect(String.Format("{0}?returnUrl={1}", this.LoginUrl, this.Request.Url), true);
                return false;
            }
            else
                return true;
        }
        private string LoginUrl
        {
            get 
            {
                return "/Login.aspx";
            }
        }
        //代码主体
        private string AllDateList = String.Empty;       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogined()) return;
            this.AllDateList = this.GetDateList();
            if (!this.IsPostBack)
            {
                this.BindSummary();
            }
        }
        private void BindSummary()
        {
            DateTime d = this.ThisDate;
            for (int i = 0; i <= 3; i++)
            {
                HyperLink lbl = (HyperLink)this.lblCol0.Parent.FindControl(String.Format("lblCol{0}", i));
                lbl.Text = String.Format("{0:yyyy年MM月dd日}", d);
                lbl.NavigateUrl = String.Format("PRIV_DefaultLog.aspx?Date={0:yyyy-MM-dd}",d);
                string sSql = String.Format("select A.[Date] as Date,A.SummaryText,B.Name as [Name] from PRIV_SummaryLogDetails A "
                                                  +" inner join PRIV_Programs B on A.ProgramId=B.ID "
                                                  +" where A.Date='{0:yyyy-MM-dd}' and A.UserId='{1}' and A.SumUpFlag={2} "
                                                            +" order by B.Sort"
                    , d, this.CurUserId, this.SumUpFlag);
                DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
                Repeater rpt = (Repeater)this.rptSummary0.Parent.FindControl(String.Format("rptSummary{0}", i));
                rpt.DataSource = dt;
                rpt.DataBind();
                d = d.AddDays(1);
            }

        }
        private string GetDateList()
        {
            string sSql = String.Format("select distinct [Date] from PRIV_SummaryLogDetails where UserId='{0}' and SumUpFlag={1} order by [Date]"
                , this.CurUserId, this.SumUpFlag);
            string s = ULCode.QDA.XSql.GetXDataTable(sSql).ToColValueList();
            return s; 
        }
        public string GetRelativeDateStr(object evalDate)
        {
            DateTime dt = Convert.ToDateTime(evalDate);
            return WX.Main.GetTimeEslapseStr(dt,null,null,true);
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime dt = e.Day.Date;
            string s_dt = String.Format("{0:yyyy-MM-dd}", dt);
            e.Cell.BackColor = !this.AllDateList.Contains(s_dt) ? System.Drawing.Color.White : System.Drawing.Color.YellowGreen;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            DateTime dt = this.Calendar1.SelectedDate;
            string s_dt = String.Format("PRIV_Default.aspx?Date={0:yyyy-MM-dd}", dt);
            Response.Redirect(s_dt, true);
        }
    }
}