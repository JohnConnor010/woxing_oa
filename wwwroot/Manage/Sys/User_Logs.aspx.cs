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
    public partial class User_Logs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userId = WX.Request.rUserId;
                if (!ULCode.Validation.IsGuid(userId))
                {
                    ULCode.Debug.we("你没有权限访问此页面！");
                    return;
                }
                WX.Model.User.MODEL model = WX.Model.User.GetCache(userId);//WX.Model.Employee.GetModel("select * from TU_Employees where UserID='" + Request["UserID"] + "'");
                liUserName.Text = String.Format("员工：{0}&nbsp;&nbsp;&nbsp;&nbsp;用户名：{1}", model.RealName, model.UserName);

                InitComponent(true);
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
            string sql = String.Format("SELECT * FROM TL_AccountLogs where UserID='{0}'{1}{2}",Request.QueryString["UserID"], begin_Con, end_Con);
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
            string sSql = String.Format("Delete from TL_AccountLogs where UserID='{0}'",Request.QueryString["UserID"]);
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