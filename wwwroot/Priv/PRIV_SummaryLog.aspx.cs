using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wwwroot.Priv
{
    public partial class PRIV_SumaryLog : System.Web.UI.Page
    {
        //模块接口
        private int SumUpFlag = 1;
        private int DefaultDay = -1;
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
                        return DateTime.Now.AddDays(this.DefaultDay);
                }
                else
                    return DateTime.Now.AddDays(this.DefaultDay);
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
        //其它程序
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsLogined()) return;
            if (!this.IsPostBack)
            {
                this.lblDate.Text = String.Format("{0:yyyy年MM月dd日 ddd}", this.ThisDate);
                this.Title = String.Format("{0:yyyy年MM月dd日}&nbsp;总结日志", this.ThisDate);
                if (String.Format("{0:yyyy-MM-dd}", this.ThisDate) == String.Format("{0:yyyy-MM-dd}", DateTime.Now))
                {
                    this.btnToday.ForeColor = System.Drawing.Color.Red;
                    this.btnToday.Font.Bold = true;
                }
                this.BindAllSummary();
            }
        }
        private void BindAllSummary()
        {
            string sSql = String.Format("Select [ID],[Name] from PRIV_Programs Where UserId='{0}' and SumUpFlag={1} order by Sort", this.CurUserId, this.SumUpFlag);
            DataTable dt = ULCode.QDA.XSql.GetDataTable(sSql);
            this.rptSummaryLog.DataSource = dt;
            this.rptSummaryLog.DataBind(); 
        }
        public string GetSummaryText(object eval_Id)
        {
            int id = Convert.ToInt32(eval_Id);
            string sSql = String.Format("Select SummaryText from PRIV_SummaryLogDetails Where Date='{0:yyyy-MM-dd}' and ProgramId='{1}' and UserId='{2}' and SumUpFlag={3}"
                , this.ThisDate, id, this.CurUserId, this.SumUpFlag);
            string value = ULCode.QDA.XSql.GetData(sSql).ToStr();
            string color = null;
            if (String.IsNullOrEmpty(value))
                color = "#efefef";
            else
                color = "#ffffff";
            string text = String.Format("<textarea name='tt_{0}' style=\"width:95%;background-color:{2}\" rows=\"5\" cols=\"\" class=\"summaryText\">{1}</textarea>"
                , id, value, color);
            return text;
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.Request.Url.ToString());
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            String sSql = "";
            int updateCount = 0;
            foreach(String tt in Request.Form.AllKeys)
            {
                if (tt.StartsWith("tt_")) //过滤掉非总结内容文本框
                {
                    string ttId_s = tt.Substring(3);
                    int ttId = 0;
                    if (int.TryParse(ttId_s, out ttId)) //过滤掉非法ID的提交数据
                    {
                        string summary = Convert.ToString(Request.Form[tt]);
                        summary = ULCode.Security.GetSafeText(summary);
                        //添加日志
                        if (String.IsNullOrEmpty(summary))
                            sSql = String.Format("if exists(Select * from PRIV_SummaryLogDetails Where Date='{0:yyyy-MM-dd}' and ProgramId='{1}' and UserId='{2}' and SumUpFlag={3}) "
                                            + " delete PRIV_SummaryLogDetails Where Date='{0:yyyy-MM-dd}' and ProgramId='{1}' and UserId='{2}' and SumUpFlag={3} "
                                  , this.ThisDate, ttId, this.CurUserId, this.SumUpFlag);
                        else
                            sSql = String.Format("if exists(Select * from PRIV_SummaryLogDetails Where Date='{0:yyyy-MM-dd}' and ProgramId='{1}' and UserId='{2}' and SumUpFlag={3} ) "
                                            + " Update PRIV_SummaryLogDetails set SummaryText='{4}' Where Date='{0:yyyy-MM-dd}' and ProgramId='{1}' and UserId='{2}' and SumUpFlag={3}  "
                                            + " else "
                                            + " Insert PRIV_SummaryLogDetails(Date,ProgramId,UserId,SumUpFlag,SummaryText) Values('{0:yyyy-MM-dd}','{1}','{2}','{3}','{4}') "
                                  , this.ThisDate, ttId, this.CurUserId,this.SumUpFlag, summary.Replace("'", "''"));

                        int iR = ULCode.QDA.XSql.Execute(sSql);
                        updateCount += iR;
                    }
                }
            }
            if (updateCount > 0)
            {
                this.BindAllSummary();
            }
        }

        protected void btnNextDay_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PRIV_SummaryLog.aspx?Date={0:yyyy-MM-dd}",this.ThisDate.AddDays(1)));
        }

        protected void btnPreviousDay_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PRIV_SummaryLog.aspx?Date={0:yyyy-MM-dd}", this.ThisDate.AddDays(-1)));
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect(String.Format("PRIV_SummaryLog.aspx?Date={0:yyyy-MM-dd}", DateTime.Now));
        }
    }
}