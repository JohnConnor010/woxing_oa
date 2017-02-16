using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace wwwroot.Manage.DownLoad
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitComponent(true);
            }
        }
        private void InitComponent(bool start)
        {
            string con = null;
            if (!String.IsNullOrEmpty(this.tbKeyWords.Text.Trim()))
            {
                con = String.Format(" where Name like '%{0}%'", this.tbKeyWords.Text);
            }
            string sql = String.Format("SELECT * FROM Down_Annex{0}", con);
            //Response.Write(sql);
            if (start)
            {
                this.AspNetPager1.AlwaysShow = true;
                this.AspNetPager1.RecordCount = WX.Main.GetPagedRowsCount(sql);
                this.AspNetPager1.PageSize = 20;
                this.AspNetPager1.CurrentPageIndex = 1;
            }

            //DataTable dataTable = ULCode.QDA.XSql.GetDataTable(sql);
            DataTable logData = WX.Main.GetPagedRows(sql, 0, "ORDER BY Addtime desc", AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex);
            //Response.Write(logData.Rows.Count);
            this.SupplierRepeater.DataSource = logData;
            this.SupplierRepeater.DataBind();
            this.AspNetPager1.AlwaysShow = true;
        }
        public string Addcount(int id)
        {
            return "123";
        }
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            InitComponent(false);
        }
        protected void Query(object sender, EventArgs e)
        {
            this.InitComponent(true);
        }
        protected void QueryAll(object sender, EventArgs e)
        {
            this.tbKeyWords.Text = String.Empty;
            this.InitComponent(true);
        }
    }
}