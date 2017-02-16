using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ULCode.QDA;

namespace wwwroot.Manage.Sys
{
    public partial class Logs_System : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true);
            }
        }
        protected void Query(object sender, EventArgs e)
        {
            this.InitComponent(true);
        }
        protected void QueryAll(object sender, EventArgs e)
        {
            this.tbKeyWords.Text = String.Empty;
            this.txtBeginTime.Text = String.Empty;
            this.txtEndTime.Text = String.Empty;
            this.InitComponent(true);
        }
        private void InitComponent(bool start)
        {
            string key = tbKeyWords.Text.Trim();
            string key_con = String.IsNullOrEmpty(key) ? String.Empty : " and RealName like '%" + key + "%'";
            string begin_Con = String.Empty;
            if (ULCode.Validation.IsDateTime(txtBeginTime.Text.Trim()))
            {
                begin_Con = String.Format(" and LogTime >= '{0:yyyy-MM-dd 00:00:00}'", Convert.ToDateTime(txtBeginTime.Text.Trim()));
            }
            string end_Con = String.Empty;
            if (ULCode.Validation.IsDateTime(txtEndTime.Text.Trim()))
            {
                end_Con = String.Format(" and LogTime <= '{0:yyyy-MM-dd 23:59:59}'", Convert.ToDateTime(txtEndTime.Text.Trim()));
            }
            string sql = String.Format("SELECT log.*,emp.RealName FROM TL_Logs log left join TU_Users emp on log.UserID=emp.UserID where 1=1{0}{1}{2}", key_con, begin_Con, end_Con);
            //Response.Write(sql);
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            var logData = WX.Main.GetPagedRows(sql, 0, "ORDER BY LogTime DESC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

            this.SupplierRepeater.DataSource = logData;
            this.SupplierRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitComponent(false);
        }
        public void ClearLogs(object sender, EventArgs e)
        {
            string sSql = "Delete from TL_AccountLogs";
            int iR = ULCode.QDA.XSql.Execute(sSql);
            if (iR > 0)
            {
                this.InitComponent(true);
            }
        }
        public string getIP(object oEval)
        {
            string eval = Convert.ToString(oEval);
            if (eval == "::1")
                return "开发测试IP";
            else
                return eval;
        }
    }
}