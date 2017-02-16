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
    public partial class Priv_Logs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true);
                if (WX.Main.CurUser.IsEmployeeUser)
                {
                    this.MenuBar1.Key = "priv";
                    this.MenuBar1.CurIndex = 9;
                }
                else
                {
                    this.MenuBar1.Key = "priv-admin";
                    this.MenuBar1.CurIndex = 2;
                }
            }
        }
        protected void Query(object sender, EventArgs e)
        {
            this.InitComponent(true);
        }
        protected void QueryAll(object sender, EventArgs e)
        {
            this.txtBeginTime.Text = String.Empty;
            this.txtEndTime.Text = String.Empty;
            this.InitComponent(true);
        }
        private void InitComponent(bool start)
        {
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
            string sql = String.Format("SELECT * FROM TL_AccountLogs where UserID='{0}'{1}{2}",WX.Main.CurUser.UserID, begin_Con, end_Con);
            //Response.Write(sql);
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }
            DataTable logData = WX.Main.GetPagedRows(sql, 0, "ORDER BY LogTime DESC", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);

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
            string sSql = String.Format("Delete from TL_AccountLogs where UserID='{0}'",WX.Main.CurUser.UserID);
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