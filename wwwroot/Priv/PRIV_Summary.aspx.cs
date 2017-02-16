using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Priv
{
    public partial class PRIV_Summary : System.Web.UI.Page
    {
        //代码接口
        private int SumUpFlag = 1;
        public int rID
        {
            get
            {
                if (Request.QueryString["ID"] != null)
                {
                    int id = 0;
                    if (int.TryParse(Convert.ToString(Request.QueryString["ID"]), out id))
                    {
                        return id;
                    }
                    else
                        return 1;
                }
                else
                    return 1;
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
                this.BindSummaryCatagory();
                this.BindSummary();
            }
        }
        private void BindSummaryCatagory()
        {
            //string sSql = String.Format("select ProgramId"
            //           + ",(Select [Name] from PRIV_Programs Where ID=A.ProgramId and UserId='{0}' and SumUpFlag={1}) as [Name]"
            //           + ",COUNT(*) as CC "
            //           + ",Max([Date]) as MaxDate "
            //           + " from PRIV_SummaryLogDetails A Where UserId='{0}' and SumUpFlag={1} group by ProgramId"
            //           , this.CurUserId, this.SumUpFlag);
            string sSql=String.Format("select A.ID as ProgramId,A.Name,B.CC,B.MaxDate from PRIV_Programs A"
                       +" left join "
                       +" (select ProgramId,COUNT(*) as CC,MAX(Date) as MaxDate from PRIV_SummaryLogDetails "
                       +"   where SumUpFlag={1} and UserId='{0}'"
                       +"   group by ProgramId) B on A.ID=B.ProgramId"
                       +" where A.SumUpFlag={1} and A.UserId='{0}' order by A.Sort"
                       , this.CurUserId, this.SumUpFlag);
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            this.rptNav.DataSource = dt;
            this.rptNav.DataBind();
        }
        private void BindSummary()
        {
            string sSql = String.Format("select Date,SummaryText from PRIV_SummaryLogDetails where ProgramId={0} and UserId='{1}' and SumUpFlag={2} order by Date desc"
                , this.rID, this.CurUserId, this.SumUpFlag);
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            this.rptSummary.DataSource = dt;
            this.rptSummary.DataBind();
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
            if (evalDate == null || evalDate == Convert.DBNull)
                return "无";
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
            string s_dt = String.Format("PRIV_SummaryLog.aspx?Date={0:yyyy-MM-dd}", dt);
            Response.Redirect(s_dt, true);
        }
    }
}